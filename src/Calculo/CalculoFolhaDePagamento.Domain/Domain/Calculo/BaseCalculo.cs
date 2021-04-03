using CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;
using System;
using System.Collections.Generic;

namespace CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    public abstract class BaseCalculo
    {
        private readonly List<IComponenteDeCalculo> componenteDeCalculos;

        protected BaseCalculo()
            => componenteDeCalculos = new List<IComponenteDeCalculo>();

        protected void AdicioneComponenteCalculo<T>()
            where T : IComponenteDeCalculo
        => componenteDeCalculos.Add(FabricaComponentesCalculo.Crie(typeof(T)));

        protected ComponentesCalculados Calcule(Contrato contrato, DateTime referencia)
        {
            var tabela = new ComponentesCalculados(contrato, referencia);

            foreach (var componente in componenteDeCalculos)
            {
                tabela.AdicioneValor(componente, componente.Calcule(contrato, tabela));
            }

            return tabela;
        }
    }
}