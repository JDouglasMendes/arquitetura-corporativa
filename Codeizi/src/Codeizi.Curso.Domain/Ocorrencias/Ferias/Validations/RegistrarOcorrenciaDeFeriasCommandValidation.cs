using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;

namespace Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Validations
{
    public class RegistrarOcorrenciaDeFeriasCommandValidation : OcorrenciaDeFeriasValidation<RegistrarOcorrenciaDeFeriasCommand>
    {
        public RegistrarOcorrenciaDeFeriasCommandValidation(IOcorrenciaDeDeriasRepository ocorrenciaDeDeriasRepository)
            : base(ocorrenciaDeDeriasRepository)
        {
            ValideContratoInfomado();
            ValidePeriodoAquisitivoInformado();
            ValideDataDeInicioInformado();
            ValideDiasDeFeriasInformado();
            ValideMaximoDeFerias();
            ValideDiasDeAbono();
            ValideSaldoFeriasPeriodoAquisitivo();
            ValideFeriasAposUmAnoDeContrato();
        }
    }
}