using Codeizi.Curso.RH.Application.Ocorrencias.Ferias;
using Codeizi.Curso.RH.Application.ViewModels;
using Codeizi.Curso.RH.Domain.SharedKernel.Notifications;
using IdentityServer4;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Api.Controllers
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