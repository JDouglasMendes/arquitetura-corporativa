using System;

namespace Codeizi.Curso.Domain.SharedKernel.IMediatorBus
{
    public class ServiceMediatorBusAttribute : Attribute
    {
        public string Queue { get; }

        public ServiceMediatorBusAttribute(string queue)
            => Queue = queue;
    }
}