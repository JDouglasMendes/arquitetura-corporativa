using Codeizi.Curso.Application.Colaboradores;
using Codeizi.Curso.Application.ViewModels;
using Codeizi.Curso.Domain.SharedKernel.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codeizi.Curso.Api.Controllers
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
        public IActionResult Post(ColaboradorAdmissaoViewModel colaboradorAdmissaoViewModel)
        {
            _logger.LogInformation($"Tentativa de admissção do colaborador {colaboradorAdmissaoViewModel.Nome}");
            colaboradorAppService.RealizeAdmissao(colaboradorAdmissaoViewModel);
            return Response(colaboradorAdmissaoViewModel);
        }
    }
}