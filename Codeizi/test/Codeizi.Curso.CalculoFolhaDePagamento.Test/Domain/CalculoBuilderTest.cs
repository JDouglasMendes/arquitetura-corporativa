using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class CalculoBuilderTest
    {
        [Fact]
        public void CalculeContratos()
        {
            var quantidadeCalls = 500;
            var mockCalculoRepository = Substitute.For<ICalculoRepository>();
            mockCalculoRepository
                .InsiraValoresCalculados(Arg.Any<ComponentesCalculados>())
                .Returns(Task.CompletedTask);
                
            var mockContratoRepository = Substitute.For<IContratoRepository>();
            mockContratoRepository
                .ObterContratosVigentes(Arg.Any<DateTime>())
                .Returns(CenarioContrato.CrieCenarioValido(quantidadeCalls));

            var builder = new CalculoBuilder(DateTime.Now,
                                             EnumFolhaDePagamento.Mensal,
                                             mockCalculoRepository);

            var task = builder.InicieCalculo(mockContratoRepository)
                                .CalculeContratos();

            if(task.Status != TaskStatus.RanToCompletion)
            {               
                Assert.True(builder.EmAndamento);                
                Assert.True(builder.PercentualExecutado < 100);
            }

            while (task.Status != TaskStatus.RanToCompletion) { }

            Assert.Equal(100, builder.PercentualExecutado);
            Assert.False(builder.EmAndamento);
            Assert.True(builder.IdExecucao != Guid.Empty);
            mockCalculoRepository.Received(quantidadeCalls).InsiraValoresCalculados(Arg.Any<ComponentesCalculados>());
        }

        [Fact]
        public async void CalculeContratosChamandoInicioDeFormaIndevida()
        {
            var quantidadeCalls = 500;
            var mockCalculoRepository = Substitute.For<ICalculoRepository>();
            mockCalculoRepository
                .InsiraValoresCalculados(Arg.Any<ComponentesCalculados>())
                .Returns(Task.CompletedTask);

            var mockContratoRepository = Substitute.For<IContratoRepository>();
            mockContratoRepository
                .ObterContratosVigentes(Arg.Any<DateTime>())
                .Returns(CenarioContrato.CrieCenarioValido(quantidadeCalls));

            var builder = new CalculoBuilder(DateTime.Now,
                                             EnumFolhaDePagamento.Mensal,
                                             mockCalculoRepository);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => builder.CalculeContratos());           
        }
      
    }
}
