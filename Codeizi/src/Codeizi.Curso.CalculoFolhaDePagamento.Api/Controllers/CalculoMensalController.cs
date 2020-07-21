using Codeizi.Curso.CalculoFolhaDePagamento.Api.ViewModel;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using Microsoft.AspNetCore.Mvc;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoMensalController : ControllerBase
    {
        private readonly ICalculoRepository _calculoRepository;
        private readonly IContratoRepository _contratoRepository;
        private readonly IFeedbackExecucaoCalculoServiceDomain _feedbackExecucaoCalculoServiceDomain;

        public CalculoMensalController(ICalculoRepository calculoRepository,
                                       IContratoRepository contratoRepository,
                                       IFeedbackExecucaoCalculoServiceDomain feedbackExecucaoCalculoServiceDomain)
        {
            _calculoRepository = calculoRepository;
            _contratoRepository = contratoRepository;
            _feedbackExecucaoCalculoServiceDomain = feedbackExecucaoCalculoServiceDomain;
        }

        [HttpPost]
        public IActionResult Post(CalculoViewModel calculoViewModel)
        {
            var calculo = new CalculoBuilder(calculoViewModel.Referencia,
                                             EnumFolhaDePagamento.Mensal,
                                             _calculoRepository,
                                             _feedbackExecucaoCalculoServiceDomain);

            calculo.InicieCalculo(_contratoRepository);
            _ = calculo.CalculeContratos();

            return Ok(calculo.IdExecucao);
        }
    }
}