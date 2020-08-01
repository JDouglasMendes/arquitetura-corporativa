using Codeizi.Curso.RH.Application.Colaboradores;
using Codeizi.Curso.RH.Application.ViewModels;
using Codeizi.Curso.RH.Domain.SharedKernel.Notifications;
using IdentityServer4;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codeizi.Curso.RH.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ApiController
    {
        private readonly IColaboradorAppService colaboradorAppService;
        private readonly ILogger<ColaboradorController> _logger;

        public ColaboradorController(INotificationHandler<DomainNotification> notifications,
                                     IColaboradorAppService colaboradorAppService,
                                     ILogger<ColaboradorController> logger)
            : base(notifications)
        {
            this.colaboradorAppService = colaboradorAppService;
            _logger = logger;
        }

        [HttpPost]
        [Route("realize-admissao")]
        [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
        public IActionResult Post(ColaboradorAdmissaoViewModel colaboradorAdmissaoViewModel)
        {
            _logger.LogInformation($"Tentativa de admissção do colaborador {colaboradorAdmissaoViewModel.Nome}");
            colaboradorAppService.RealizeAdmissao(colaboradorAdmissaoViewModel);
            return Response(colaboradorAdmissaoViewModel);
        }
    }
}