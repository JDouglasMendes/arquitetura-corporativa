using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
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
        private DateTime _referencia;
        private List<Contrato> _contratos;

        public CalculoBuilder(DateTime referencia,
                              EnumFolhaDePagamento enumFolhaDePagamento,
                              ICalculoRepository calculoRepository)
        {
            _calculo = FabricaCalculo.Crie(enumFolhaDePagamento, referencia);
            _calculoRepository = calculoRepository;
            _referencia = referencia;
        }

        public CalculoBuilder InicieCalculo(List<Contrato> contratos)
        {
            _quantidadeProcessada = 0;
            _contratos = contratos;
            _quantidadeTotal = _contratos.Count();
            IdExecucao = Guid.NewGuid();
            EmAndamento = true;
            return this;
        }

        public CalculoBuilder InicieCalculo(IContratoRepository repository)
            => InicieCalculo(repository.ObterContratosVigentes(_referencia));

        private int _quantidadeTotal;
        private int _quantidadeProcessada;

        public double PercentualExecutado
        {
            get
            {
                return _quantidadeProcessada * 100 / _quantidadeTotal;
            }
        }

        public bool EmAndamento { get; private set; }

        public Guid IdExecucao { get; private set; }

        public void CalculeContratos()
        {
            if (IdExecucao == null)
                throw new ArgumentException();

            if (_contratos == null)
                throw new ArgumentException();

            Parallel.For(0, _quantidadeTotal, async index =>
            {
                var valores = _calculo.Calcule(_contratos[index]);
                await _calculoRepository.InsiraValoresCalculados(valores);
                Interlocked.Increment(ref _quantidadeProcessada);
            });

            EmAndamento = false;
        }
    }
}