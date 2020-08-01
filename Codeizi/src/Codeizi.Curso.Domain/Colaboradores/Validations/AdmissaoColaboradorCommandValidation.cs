using Codeizi.Curso.RH.Domain.Colaboradores.Commands;
using Codeizi.Curso.RH.Domain.SharedKernel.Commands;
using FluentValidation;

namespace Codeizi.Curso.RH.Domain.Colaboradores.Validations
{
    public class AdmissaoColaboradorCommandValidation : ColaboradorValidation<AdmissaoColaboradorCommand>
    {
        public AdmissaoColaboradorCommandValidation()
        {
            ValideNome();
            ValideSobrenome();
            ValideDataAdmisaoColaborador();
            ValideSalarioContratual();
            ValideObservacaoContratual();
            ValideDataAdmisaoAntesDataNascimento();
        }
    }
}