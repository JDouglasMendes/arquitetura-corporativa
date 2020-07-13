using System;

namespace Codeizi.Curso.Domain.SharedKernel.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}
