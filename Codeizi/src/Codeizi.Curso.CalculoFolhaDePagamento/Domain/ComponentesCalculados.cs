using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public class ComponentesCalculados
    {
        private readonly IDictionary<EnumComponentesCalculo, ValorComponenteCalculo> _valoresCalculados;
        private readonly Contrato _contrato;

        public ComponentesCalculados(Contrato contrato)
            : this()
            => _contrato = contrato;

        private ComponentesCalculados()
            => _valoresCalculados = new Dictionary<EnumComponentesCalculo, ValorComponenteCalculo>();

        public void AdicioneValor(IComponenteDeCalculo componente, ValorComponenteCalculo valor)
        {
            if (valor.Valor == default)
                return;

            if (!_valoresCalculados.ContainsKey(componente.IdComponente))
                _valoresCalculados.Add(componente.IdComponente, ValorComponenteCalculo.Zero);

            _valoresCalculados[componente.IdComponente] = valor;
        }

        public ValorComponenteCalculo Valor<T>(T componente)
            where T : IComponenteDeCalculo
            => Valor(componente.IdComponente);

        public ValorComponenteCalculo Valor(EnumComponentesCalculo enumComponentesCalculo)
        {
            if (_valoresCalculados.ContainsKey(enumComponentesCalculo))
                return _valoresCalculados[enumComponentesCalculo];
            return ValorComponenteCalculo.Zero;
        }

        public bool ExisteValores => _valoresCalculados.Keys.Any();

        public Guid IdColaborador => _contrato.IdColaborador;
    }
}
