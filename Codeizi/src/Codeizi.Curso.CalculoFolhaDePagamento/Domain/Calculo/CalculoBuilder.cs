using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.Infra.CrossCutting.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    public class CalculoBuilder
    {
        private readonly ICalculo _calculo;
        private readonly ICalculoRepository _calculoRepository;
        private readonly IFeedbackExecucaoCalculoServiceDomain _feedbackExecucaoCalculo;
        private readonly DateTime _referencia;
        private List<Contrato> _contratos;
        private readonly IRabbitMQBus _rabbitMQBus;
        private readonly IUser _user;

        public CalculoBuilder(DateTime referencia,
                              EnumFolhaDePagamento enumFolhaDePagamento,
                              ICalculoRepository calculoRepository,
                              IFeedbackExecucaoCalculoServiceDomain feedbackExecucaoCalculo,
                              IRabbitMQBus rabbitMQBus,
                              IUser user)
        {
            _calculo = FabricaCalculo.Crie(enumFolhaDePagamento, referencia);
            _calculoRepository = calculoRepository;
            _referencia = referencia;
            _feedbackExecucaoCalculo = feedbackExecucaoCalculo;
            _rabbitMQBus = rabbitMQBus;
            _user = user;
        }

        public CalculoBuilder InicieCalculo(List<Contrato> contratos)
        {
            _contratos = contratos;
            _quantidadeTotal = _contratos.Count();
            IdExecucao = Guid.NewGuid();
            _feedbackExecucaoCalculo.IniciarProcessamento(IdExecucao, _quantidadeTotal);
            return this;
        }

        public CalculoBuilder InicieCalculo(IContratoRepository repository)
            => InicieCalculo(repository.ObterContratosVigentes(_referencia));

        private int _quantidadeTotal;
        private int _quantidadeProcessada;

        public Guid IdExecucao { get; private set; }

        public async Task CalculeContratos()
        {
            if (_contratos == null)
                throw new ArgumentException();

            await Task.Run(() =>
            {
                Parallel.For(0, _quantidadeTotal, async index =>
                {
                    var valores = _calculo.Calcule(_contratos[index]);
                    await _calculoRepository.InsiraValoresCalculados(valores);
                    Interlocked.Increment(ref _quantidadeProcessada);
                    _feedbackExecucaoCalculo.AtualizarPercentualExecucao(IdExecucao, _quantidadeProcessada, _quantidadeTotal);
                });
            });

            await _rabbitMQBus.Publisher(FactoryPublishable.Get<object>(IdExecucao,
                "notificar-usuario",
                new
                {
                    UserName = _user.Name,
                    Message = "Processamento do cálculo finalizado",
                }));
        }
    }
}