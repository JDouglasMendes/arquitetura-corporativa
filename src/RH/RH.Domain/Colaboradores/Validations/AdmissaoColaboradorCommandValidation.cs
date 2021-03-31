using RH.Domain.Colaboradores.Commands;

namespace RH.Domain.Colaboradores.Validations
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