using Codeizi.Curso.RH.Domain.Colaboradores;
using Codeizi.Curso.RH.Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;

namespace Codeizi.Infra.Data.Test.Cenarios
{
    public class ColaboradorBuilder
    {
        private Colaborador colaborador;

        public ColaboradorBuilder CrieColaboradorSucesso()
        {
            colaborador = new Colaborador(Guid.NewGuid(), NomePessoa.Crie("Codeizi", "Treinamento"), DateTime.Now.AddYears(-20));
            colaborador.AddContrato(DateTime.Now, 1000);
            return this;
        }

        public ColaboradorBuilder CrieColaboradorSucesso(Guid chave)
        {
            colaborador = new Colaborador(chave, NomePessoa.Crie("Codeizi", "Treinamento"), DateTime.Now.AddYears(-20));
            CrieContratos(colaborador).ForEach(c => colaborador.AddContrato(c.DataInicio, c.SalarioContratual));
            return this;
        }

        private List<Contrato> CrieContratos(Colaborador colaborador)
        {
            return new List<Contrato>
            {
                new Contrato(colaborador, DateTime.Now, 1000),
            };
        }

        public Colaborador Get
        {
            get
            {
                if (colaborador == null)
                {
                    throw new ArgumentException("Objeto não inicializado");
                }

                return colaborador;
            }
        }

        public static IEnumerable<Contrato> CrieContratos(Colaborador colaborador, int quantidade)
        {
            var contratos = new List<Contrato>();
            while (quantidade > 0)
            {
                contratos.Add(new Contrato(colaborador, DateTime.Now, 100));
                quantidade--;
            }
            return contratos;
        }
    }
}