using AutoMapper;
using Codeizi.Curso.RH.Application.ViewModels;
using Codeizi.Curso.RH.Domain.Colaboradores.Commands;
using Codeizi.Curso.RH.Domain.SharedKernel.IMediatorBus;

namespace Codeizi.Curso.RH.Application.Colaboradores
{
    public class ColaboradorAppService : IColaboradorAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler bus;

        public ColaboradorAppService(IMapper mapper, IMediatorHandler bus)
        {
            _mapper = mapper;
            this.bus = bus;
        }

        public void RealizeAdmissao(ColaboradorAdmissaoViewModel colaboradorAdmissaoViewModel)
        {
            var colaboradorAdmissaoComand = _mapper.Map<AdmissaoColaboradorCommand>(colaboradorAdmissaoViewModel);
            bus.SendCommand(colaboradorAdmissaoComand);
        }
    }
}