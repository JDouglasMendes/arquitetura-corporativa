using Domain.SharedKernel.Notifications;
using IdentityServer4;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RH.Application.Colaboradores;
using RH.Application.ViewModels;
using System.Threading.Tasks;

namespace RH.Service.Api.Controllers
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