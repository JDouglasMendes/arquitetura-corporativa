using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public class ComponentesCalculados
    {
        private readonly IDictionary<Type, ValorComponenteCalculo> _valoresCalculados;

        public ComponentesCalculados()
            => _valoresCalculados = new Dictionary<Type, ValorComponenteCalculo>();

        public void AdicioneValor(IComponenteDeCalculo componente, ValorComponenteCalculo valor)
        {
            if (valor.Valor == default)
                return;

            if (!_valoresCalculados.ContainsKey(componente.GetType()))
                _valoresCalculados.Add(componente.GetType(), ValorComponenteCalculo.Zero);

            _valoresCalculados[componente.GetType()] = valor;
        }

        public ValorComponenteCalculo Valor<T>()
            where T : IComponenteDeCalculo
        {
            if (_valoresCalculados.ContainsKey(typeof(T)))
                return _valoresCalculados[typeof(T)];
            return ValorComponenteCalculo.Zero;
        }

        public bool ExisteValores => _valoresCalculados.Keys.Any();
    }
}
