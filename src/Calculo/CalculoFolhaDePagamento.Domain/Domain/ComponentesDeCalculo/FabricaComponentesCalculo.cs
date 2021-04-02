using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public static class FabricaComponentesCalculo
    {
        private readonly static Dictionary<Type, IComponenteDeCalculo> _dicionarioDeComponentes = new Dictionary<Type, IComponenteDeCalculo>();
        private static readonly object _lock = new object();

        private static void LoadComponents()
        {
            lock (_lock)
            {
                if (_dicionarioDeComponentes.Any())
                    return;

                var types = Assembly.GetAssembly(typeof(FabricaComponentesCalculo))
                .GetTypes().Where(x => x.IsClass &&
                      x.GetInterface(typeof(IComponenteDeCalculo).Name, true) != null).ToList();

                types.ForEach(t => _dicionarioDeComponentes.Add(t, (IComponenteDeCalculo)Activator.CreateInstance(t)));
            }
        }

        public static IComponenteDeCalculo Crie(Type type)
        {
            LoadComponents();
            if (_dicionarioDeComponentes.ContainsKey(type))
                return _dicionarioDeComponentes[type];

            throw new ArgumentException($"o tipo {type.FullName} não foi mapeado para calculo");
        }
    }
}