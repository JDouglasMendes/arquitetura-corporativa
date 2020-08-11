using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.BusModel;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.Infra.CrossCutting.Configuration;
using System.Threading.Tasks;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain
{
    // [ServiceMediatorBus("add-contrato")]
    public class NovoContratoServicoBus : IConsumerServiceBus
    {
        private readonly IContratoRepository _contratoRepository;
        private readonly ICodeiziConfiguration _codeiziConfiguration;
        public NovoContratoServicoBus(IContratoRepository contratoRepository,
                                      ICodeiziConfiguration codeiziConfiguration)
            => (_contratoRepository, _codeiziConfiguration) = (contratoRepository, codeiziConfiguration);

        public string RoutingKey => _codeiziConfiguration.CalculoFolhaDePagamentoQueue;

        public static Contrato ConvertTo(ContratoBusModel x)
        {
            var contrato = new Contrato(x.IdColaborador,
                             x.IdContrato,
                             new Vigencia(x.DataInicio),
                             new ValorComponenteCalculo(x.SalarioContratual));

            if (x.DataFim.HasValue)
                contrato.FinalizeContrato(x.DataFim.Value);

            return contrato;
        }

        public Task Handle(Publishable publishable)
        {
            var contrato = publishable.ToObject<ContratoBusModel>();
            _contratoRepository.InsiraNovoContrato(contrato, ConvertTo);
            return Task.CompletedTask;
        }
    }
}