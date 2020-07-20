using Codeizi.Curso.RH.Domain.Colaboradores.EventHandlers;
using Codeizi.Curso.RH.Domain.Colaboradores.Events;
using System;
using System.Threading;
using Xunit;

namespace Codeizi.Curso.Domain.Test.Colaboradores
{
    public class ColaboradorEventHandlerTest
    {
        [Fact]
        public async void HandleAdimitido()
        {
            var colaboradorAdmitido = new ColaboradorAdmitidoEvent(Guid.NewGuid(),
                                                                   "Codeizi",
                                                                   "Treinamento",
                                                                   DateTime.Now,
                                                                   1000,
                                                                   "Observacao Contratual");

            var eventHandler = new ColaboradorEventHandler();
            await eventHandler.Handle(colaboradorAdmitido, CancellationToken.None);
            Assert.True(true);
        }
    }
}