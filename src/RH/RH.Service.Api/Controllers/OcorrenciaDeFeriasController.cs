using Domain.SharedKernel.Notifications;
using IdentityServer4;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RH.Application.Ocorrencias.Ferias;
using RH.Application.ViewModels;
using System.Threading.Tasks;

namespace RH.Service.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class OcorrenciaDeFeriasController : ApiController
    {
        private readonly IOcorrenciaDeFeriasAppService _ocorrenciaDeFeriasAppService;

        public OcorrenciaDeFeriasController(INotificationHandler<DomainNotification> notifications,
                                            IOcorrenciaDeFeriasAppService ocorrenciaDeFeriasAppService)
            : base(notifications)
        {
            _ocorrenciaDeFeriasAppService = ocorrenciaDeFeriasAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RegistrarOcorrenciaDeFeriasViewModel registrarOcorrenciaDeFeriasViewModel)
        {
            await _ocorrenciaDeFeriasAppService.RegistrarOcorrenciaDeFeriasCommand(registrarOcorrenciaDeFeriasViewModel);
            return Response();
        }
    }
}