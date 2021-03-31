using AutoMapper;
using Domain.SharedKernel.IMediatorBus;
using RH.Application.ViewModels;
using RH.Domain.Colaboradores.Commands;
using System.Threading.Tasks;

namespace RH.Application.Colaboradores
{
    public class ColaboradorAppService : IColaboradorAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;

        public ColaboradorAppService(IMapper mapper, IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
        }

        public async Task RealizeAdmissao(ColaboradorAdmissaoViewModel colaboradorAdmissaoViewModel)
        {
            var colaboradorAdmissaoComand = _mapper.Map<AdmissaoColaboradorCommand>(colaboradorAdmissaoViewModel);
            await _bus.SendCommand(colaboradorAdmissaoComand);
        }
    }
}