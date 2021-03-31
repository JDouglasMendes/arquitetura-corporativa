using AutoMapper;
using Domain.SharedKernel.IMediatorBus;
using RH.Application.ViewModels;
using RH.Domain.Ocorrencias.Ferias.Commands;
using System.Threading.Tasks;

namespace RH.Application.Ocorrencias.Ferias
{
    public class OcorrenciaDeFeriasAppService : IOcorrenciaDeFeriasAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;

        public OcorrenciaDeFeriasAppService(IMapper mapper, IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
        }

        public async Task RegistrarOcorrenciaDeFeriasCommand(RegistrarOcorrenciaDeFeriasViewModel ocorrenciaDeFerias)
        {
            var ocorrenciaDeFeriasCommand = _mapper.Map<RegistrarOcorrenciaDeFeriasCommand>(ocorrenciaDeFerias);
            await _bus.SendCommand(ocorrenciaDeFeriasCommand);
        }
    }
}