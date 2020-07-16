using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using System;
using System.Collections.Generic;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    public abstract class BaseCalculo
    {
        private readonly List<IComponenteDeCalculo> componenteDeCalculos;

        protected BaseCalculo()
            => componenteDeCalculos = new List<IComponenteDeCalculo>();

        protected void AdicioneComponenteCalculo<T>()
            where T : IComponenteDeCalculo
        => componenteDeCalculos.Add(FabricaComponentesCalculo.Singleton.Crie(typeof(T)));

        protected IReadOnlyCollection<IComponenteDeCalculo> Componentes
        {
            get
            {
                return componenteDeCalculos;
            }
        }

        protected ComponentesCalculados Calcule(Contrato contrato, DateTime referencia)
        {
            var tabela = new ComponentesCalculados(contrato, referencia);

            foreach (var componente in Componentes)
                tabela.AdicioneValor(componente, componente.Calcule(contrato, tabela));

            return tabela;
        }
    }
}