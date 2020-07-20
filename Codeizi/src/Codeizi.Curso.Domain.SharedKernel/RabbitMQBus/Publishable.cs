using System;
using System.Diagnostics.CodeAnalysis;

namespace Codeizi.Curso.RH.Domain.SharedKernel.RabbitMQBus
{
    [ExcludeFromCodeCoverage]
    public class Publishable
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
    }
}