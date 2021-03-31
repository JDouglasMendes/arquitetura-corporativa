using Domain.SharedKernel.Events;
using FluentValidation.Results;

namespace Domain.SharedKernel.Commands
{
    public abstract class Command : Message
    {
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}