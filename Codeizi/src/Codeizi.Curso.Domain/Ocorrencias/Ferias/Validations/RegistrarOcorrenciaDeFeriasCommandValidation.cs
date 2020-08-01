using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Validations
{
    public class RegistrarOcorrenciaDeFeriasCommandValidation : OcorrenciaDeFeriasValidation<OcorrenciaDeFeriasCommand>
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
