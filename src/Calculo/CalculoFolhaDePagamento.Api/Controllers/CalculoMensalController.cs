using CalculoFolhaDePagamento.Api.ViewModel;
using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Services.Repositories;
using CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using IdentityServer4;
using Infra.CrossCutting.EventBusRabbitMQ;
using Infra.CrossCutting.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalculoFolhaDePagamento.Api.Controllers
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
                                             _feedbackExecucaoCalculoServiceDomain);

            calculo.InicieCalculo(_contratoRepository);
            _ = calculo.CalculeContratos();

            return Ok(calculo.IdExecucao);
        }
    }
}