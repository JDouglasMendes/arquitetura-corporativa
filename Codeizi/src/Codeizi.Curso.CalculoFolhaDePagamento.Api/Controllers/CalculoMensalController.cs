using Codeizi.Curso.CalculoFolhaDePagamento.Api.ViewModel;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.Infra.CrossCutting.Identity;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class CalculoMensalController : ControllerBase
    {
        private readonly ICalculoRepository _calculoRepository;
        private readonly IContratoRepository _contratoRepository;
        private readonly IFeedbackExecucaoCalculoServiceDomain _feedbackExecucaoCalculoServiceDomain;
        private readonly IUser _user;
        private readonly IRabbitMQBus _rabbitMQBus;
        public CalculoMensalController(ICalculoRepository calculoRepository,
                                       IContratoRepository contratoRepository,
                                       IFeedbackExecucaoCalculoServiceDomain feedbackExecucaoCalculoServiceDomain,
                                       IRabbitMQBus rabbitMQBus,
                                       IUser user)
        {
            _calculoRepository = calculoRepository;
            _contratoRepository = contratoRepository;
            _feedbackExecucaoCalculoServiceDomain = feedbackExecucaoCalculoServiceDomain;
            _user = user;
            _rabbitMQBus = rabbitMQBus;
        }

        [HttpPost]
        public IActionResult Post(CalculoViewModel calculoViewModel)
        {
            var calculo = new CalculoBuilder(calculoViewModel.Referencia,
                                             EnumFolhaDePagamento.Mensal,
                                             _calculoRepository,
                                             _feedbackExecucaoCalculoServiceDomain,
                                             _rabbitMQBus,
                                             _user);

            calculo.InicieCalculo(_contratoRepository);
            _ = calculo.CalculeContratos();

            return Ok(calculo.IdExecucao);
        }
    }
}