using System;
using System.Diagnostics.CodeAnalysis;

namespace Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ
{
    [ExcludeFromCodeCoverage]
    public sealed class ServiceMediatorBusAttribute : Attribute
    {
        public string Queue { get; }

        public ServiceMediatorBusAttribute(string queue)
            => Queue = queue;
    }
}