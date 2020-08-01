using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using FluentValidation;
using FluentValidation.Results;

namespace Codeizi.Curso.RH.Domain.SharedKernel.Commands
{
    public abstract class Command : Message
    {
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}