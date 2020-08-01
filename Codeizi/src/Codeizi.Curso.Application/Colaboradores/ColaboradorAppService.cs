using AutoMapper;
using Codeizi.Curso.RH.Application.ViewModels;
using Codeizi.Curso.RH.Domain.Colaboradores.Commands;
using Codeizi.Curso.RH.Domain.SharedKernel.IMediatorBus;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Application.Colaboradores
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