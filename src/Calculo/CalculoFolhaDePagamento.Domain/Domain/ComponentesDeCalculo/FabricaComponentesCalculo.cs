using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public sealed class FabricaComponentesCalculo
    {
        private static Dictionary<Type, IComponenteDeCalculo> _dicionarioDeComponentes;

        private FabricaComponentesCalculo()
        {
            _dicionarioDeComponentes = new Dictionary<Type, IComponenteDeCalculo>();
            var types = Assembly.GetAssembly(typeof(FabricaComponentesCalculo))
            .GetTypes().Where(x => x.IsClass &&
                  x.GetInterface(typeof(IComponenteDeCalculo).Name, true) != null).ToList();

            types.ForEach(t => _dicionarioDeComponentes.Add(t, (IComponenteDeCalculo)Activator.CreateInstance(t)));
        }

#pragma warning disable CA1822 // Mark members as static

        public IComponenteDeCalculo Crie(Type type)
#pragma warning restore CA1822 // Mark members as static
        {
            if (_dicionarioDeComponentes.ContainsKey(type))
                return _dicionarioDeComponentes[type];

            throw new ArgumentException($"o tipo {type.FullName} não foi mapeado para calculo");
        }

        // singleton
        private static volatile FabricaComponentesCalculo instance;

        private static readonly object SyncRoot = new object();

        public static FabricaComponentesCalculo Singleton
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                            instance = new FabricaComponentesCalculo();
                    }
                }

                return instance;
            }
        }
    }
}