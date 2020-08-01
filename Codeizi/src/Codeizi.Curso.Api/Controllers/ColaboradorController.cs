using Codeizi.Curso.RH.Application.Colaboradores;
using Codeizi.Curso.RH.Application.ViewModels;
using Codeizi.Curso.RH.Domain.SharedKernel.Notifications;
using IdentityServer4;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class ColaboradorController : ApiController
    {
        private readonly IColaboradorAppService colaboradorAppService;

        public ColaboradorController(INotificationHandler<DomainNotification> notifications,
                                     IColaboradorAppService colaboradorAppService)
            : base(notifications)
        {
            this.colaboradorAppService = colaboradorAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ColaboradorAdmissaoViewModel colaboradorAdmissaoViewModel)
        {
            await colaboradorAppService.RealizeAdmissao(colaboradorAdmissaoViewModel);
            return Response(colaboradorAdmissaoViewModel);
        }
    }
}