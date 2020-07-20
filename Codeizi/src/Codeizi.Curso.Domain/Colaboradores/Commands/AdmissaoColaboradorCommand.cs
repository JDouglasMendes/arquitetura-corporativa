using Codeizi.Curso.RH.Domain.Colaboradores.Validations;
using System;

namespace Codeizi.Curso.RH.Domain.Colaboradores.Commands
{
    public class AdmissaoColaboradorCommand : ColaboradorCommand
    {
        public AdmissaoColaboradorCommand(string nome, string sobrenome, DateTime dataDeAdmissao, double salarioContratual, DateTime dataNascimento)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataDeAdmissao = dataDeAdmissao;
            SalarioContratual = salarioContratual;
            DataNascimento = dataNascimento;
        }

        public override bool IsValid()
        {
            ValidationResult = new AdmissaoColaboradorCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}