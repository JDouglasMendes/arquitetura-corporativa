using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codeizi.Curso.CalculoFolhaDePagamento.Api.ViewModel;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoMensalController : ControllerBase
    {
        private readonly ICalculoRepository _calculoRepository;
        private readonly IContratoRepository _contratoRepository;
        public CalculoMensalController(ICalculoRepository calculoRepository,
                                       IContratoRepository contratoRepository)
        {
            _calculoRepository = calculoRepository;
            _contratoRepository = contratoRepository;
        }

        [HttpPost]
        public IActionResult Post(CalculoViewModel calculoViewModel)
        {
            var calculo = new CalculoBuilder(calculoViewModel.Referencia,
                                             EnumFolhaDePagamento.Mensal,
                                             _calculoRepository,
                                             null);

            calculo.InicieCalculo(_contratoRepository);
            _ = calculo.CalculeContratos();

            return Ok(calculo.IdExecucao);
        }
    }
}
