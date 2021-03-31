using CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace CalculoFolhaDePagamento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class FeedbackCalculoController : ControllerBase
    {
        private readonly IFeedbackExecucaoCalculoServiceDomain feedbackExecucaoCalculoServiceDomain;

        public FeedbackCalculoController(IFeedbackExecucaoCalculoServiceDomain feedbackExecucaoCalculoServiceDomain)
            => this.feedbackExecucaoCalculoServiceDomain = feedbackExecucaoCalculoServiceDomain;

        [HttpGet("{idCalculo}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Get(string idCalculo)
        {
            if (Guid.TryParse(idCalculo, out var result))
            {
                var percentual = feedbackExecucaoCalculoServiceDomain.PercentualExecucao(result);
                if (percentual > 0)
                    return Ok(percentual);
            }
            return BadRequest(new { Message = $"Não existe cálculo com esse id: {idCalculo}" });
        }
    }
}