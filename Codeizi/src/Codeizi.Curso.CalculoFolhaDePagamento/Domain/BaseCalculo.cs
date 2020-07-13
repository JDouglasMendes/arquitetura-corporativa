using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
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

    }
}
