using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.BusModel;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.RH.Domain.SharedKernel.IMediatorBus;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain
{
    [ServiceMediatorBus("add-contrato")]
    public class NovoContratoServicoBus
    {
        private readonly IContratoRepository contratoRepository;

        public NovoContratoServicoBus(IContratoRepository contratoRepository)
            => this.contratoRepository = contratoRepository;

        public Contrato ConvertTo(ContratoBusModel x)
        {
            var contrato = new Contrato(x.IdColaborador,
                             x.IdContrato,
                             new Vigencia(x.DataInicio),
                             new ValorComponenteCalculo(x.SalarioContratual));

            if (x.DataFim.HasValue)
                contrato.FinalizeContrato(x.DataFim.Value);

            return contrato;
        }

        public Task Handle(string message)
        {
            var contrato = JsonConvert.DeserializeObject<ContratoBusModel>(message);
            contratoRepository.InsiraNovoContrato(contrato, ConvertTo);
            return Task.CompletedTask;
        }
    }
}