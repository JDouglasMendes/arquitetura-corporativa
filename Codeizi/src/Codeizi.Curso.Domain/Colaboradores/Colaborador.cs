using Codeizi.Curso.Domain.SharedKernel.Entities;
using Codeizi.Curso.Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Codeizi.Curso.Domain.Colaboradores
{
    public class Colaborador : Entity
    {
        private readonly List<Contrato> _contratos = new List<Contrato>();
        public NomePessoa Nome { get; }
        public string ObservacaoContratual { get; set; }

        public Colaborador(Guid id, NomePessoa nome)
        {
            this.Id = id;
            this.Nome = nome;
            _contratos = new List<Contrato>();
        }

        [ExcludeFromCodeCoverage]
        protected Colaborador() { }

        [ExcludeFromCodeCoverage]
        public IReadOnlyCollection<Contrato> Contratos => _contratos.ToList();

        public void AddContrato(DateTime dataInicio, double salarioContratual)
            => _contratos.Add(new Contrato(this, dataInicio, salarioContratual));
    }
}