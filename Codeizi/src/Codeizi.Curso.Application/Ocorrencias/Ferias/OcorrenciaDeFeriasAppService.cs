using AutoMapper;
using Codeizi.Curso.RH.Application.ViewModels;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands;
using Codeizi.Curso.RH.Domain.SharedKernel.IMediatorBus;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Application.Ocorrencias.Ferias
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