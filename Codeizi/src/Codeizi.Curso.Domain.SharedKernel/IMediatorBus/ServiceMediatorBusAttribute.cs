using System;
using System.Diagnostics.CodeAnalysis;

namespace Codeizi.Curso.RH.Domain.SharedKernel.IMediatorBus
{
    [ExcludeFromCodeCoverage]
    public class ServiceMediatorBusAttribute : Attribute
    {
        public string Queue { get; }

        public ServiceMediatorBusAttribute(string queue)
            => Queue = queue;
    }
}