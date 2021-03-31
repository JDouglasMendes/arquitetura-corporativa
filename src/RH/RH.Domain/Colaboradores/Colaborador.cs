using Domain.SharedKernel.Entities;
using Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RH.Domain.Colaboradores
{
    public class Colaborador : Entity
    {
        private readonly List<Contrato> _contratos = new List<Contrato>();
        public NomePessoa Nome { get; }
        public DateTime DataDeNascimento { get; private set; }
        public string ObservacaoContratual { get; set; }

        public Colaborador(Guid id, NomePessoa nome, DateTime dataDeNascimento)
            : this()
        {
            Id = id;
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
        }

        [ExcludeFromCodeCoverage]
        protected Colaborador()
            => _contratos = new List<Contrato>();

        [ExcludeFromCodeCoverage]
        public IReadOnlyCollection<Contrato> Contratos => _contratos.ToList();

        public void AddContrato(DateTime dataInicio, double salarioContratual)
            => _contratos.Add(new Contrato(this, dataInicio, salarioContratual));

        public bool ContratoPodeSerEncerrado(Guid idContrato)
            => _contratos.FirstOrDefault(x => x.Id == idContrato)?.ContratoAindaVigente ?? false;

        public void EncerreContrato(Guid idContrato, DateTime date)
        {
            if (ContratoPodeSerEncerrado(idContrato))
                _contratos.FirstOrDefault(x => x.Id == idContrato)?.EncerreContrato(date);
        }
    }
}