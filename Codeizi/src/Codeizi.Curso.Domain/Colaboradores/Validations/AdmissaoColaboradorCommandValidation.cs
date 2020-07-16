using Codeizi.Curso.Domain.Colaboradores.Commands;

namespace Codeizi.Curso.Domain.Colaboradores.Validations
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