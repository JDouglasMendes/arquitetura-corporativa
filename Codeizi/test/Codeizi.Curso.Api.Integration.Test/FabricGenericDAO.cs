using Codeizi.Curso.RH.Infra.Data.Context;
using System;
using System.Linq;
using System.Reflection;

namespace Codeizi.Curso.Api.Integration.Test
{
    public static class FabricGenericDAO<T>
        where T : class
    {
        public static T Crie(CodeiziContext codeiziContext)
        {
            var type = Assembly.GetAssembly(typeof(CodeiziContext))
                .GetTypes().Where(x => x.IsClass &&
                                  x.GetInterface(typeof(T).Name, true) != null).FirstOrDefault();

            return Activator.CreateInstance(type, new object[] { codeiziContext }) as T;
        }
    }
}