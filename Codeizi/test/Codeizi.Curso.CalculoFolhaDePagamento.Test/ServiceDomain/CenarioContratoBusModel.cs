using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.BusModel;
using Newtonsoft.Json;
using System;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.ServiceDomain
{
    public static class CenarioContratoBusModel
    {
        public static ContratoBusModel CrieContrato
            => new ContratoBusModel
            {
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddYears(1),
                IdColaborador = Guid.NewGuid(),
                IdContrato = Guid.NewGuid(),
                SalarioContratual = 1000f,                
            };


        public static string ToJson(this object objeto)
         => JsonConvert.SerializeObject(objeto);    
    }
}