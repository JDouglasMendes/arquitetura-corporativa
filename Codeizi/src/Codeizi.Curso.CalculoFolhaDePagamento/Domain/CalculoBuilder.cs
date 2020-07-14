using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public class CalculoBuilder
    {
        private readonly ICalculo calculo;
        private readonly ICalculoRepository _calculoRepository;
        private List<Contrato> _contratos;
        public CalculoBuilder(EnumFolhaDePagamento enumFolhaDePagamento,
                              ICalculoRepository calculoRepository)
        {
            calculo = FabricaCalculo.Crie(enumFolhaDePagamento);
            _calculoRepository = calculoRepository;
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
        public void CalculeContratosParallelFor()
        {
            if (IdExecucao == null)
                throw new ArgumentException();

            if (_contratos == null)
                throw new ArgumentException();

            Parallel.For(0, _quantidadeTotal, async index =>
            {
                var valores = calculo.Calcule(_contratos[index]);
                await _calculoRepository.InsiraValoresCalculadosAsync(valores);
                _quantidadeProcessada++;

            });

            EmAndamento = false;
        }

    }
}
