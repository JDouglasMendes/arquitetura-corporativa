﻿using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.RH.Domain.Colaboradores.EventHandlers;
using Codeizi.Curso.RH.Domain.Colaboradores.Events;
using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Threading;
using Xunit;

namespace Codeizi.Curso.Domain.Test.Colaboradores
{
    public class ColaboradorEventHandlerTest
    {
        [Fact]
        public async void HandleAdmitido()
        {
            var mock = Substitute.For<IRabbitMQBus>();
            var colaboradorAdmitido = new NovoColaboradorParaCalculoEvent(Guid.NewGuid(),
                                                                   Guid.NewGuid(),
                                                                   DateTime.Now,
                                                                   null,
                                                                   1000);

            var p = FactoryPublishable.Get(colaboradorAdmitido.AggregateId,
                                                            "add-contrato",
                                                            colaboradorAdmitido);
            var json = JsonConvert.SerializeObject(p);
            var p2 = JsonConvert.DeserializeObject<Publishable>(json);
            var c = p2.ToObject<NovoColaboradorParaCalculoEvent>();

            Assert.Equal(colaboradorAdmitido.AggregateId, c.AggregateId);
            var mockStore = Substitute.For<IEventStore>();

            var eventHandler = new ColaboradorEventHandler(mock, mockStore);
            await eventHandler.Handle(colaboradorAdmitido, CancellationToken.None);
            Assert.True(true);
        }
    }
}