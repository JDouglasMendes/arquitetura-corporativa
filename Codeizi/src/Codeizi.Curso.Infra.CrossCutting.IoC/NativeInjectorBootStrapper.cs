using Codeizi.Curso.Infra.CrossCutting.Configuration;
using Codeizi.Curso.RH.Application.AutoMapper;
using Codeizi.Curso.RH.Domain.Colaboradores.CommandHandlers;
using Codeizi.Curso.RH.Domain.Colaboradores.Commands;
using Codeizi.Curso.RH.Domain.Colaboradores.Contracts;
using Codeizi.Curso.RH.Domain.Colaboradores.EventHandlers;
using Codeizi.Curso.RH.Domain.Colaboradores.Events;
using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using Codeizi.Curso.RH.Domain.SharedKernel.IMediatorBus;
using Codeizi.Curso.RH.Domain.SharedKernel.Notifications;
using Codeizi.Curso.RH.infra.Data.EventSource.Context;
using Codeizi.Curso.RH.infra.Data.EventSource.EventSource;
using Codeizi.Curso.RH.infra.Data.EventSource.EventStore;
using Codeizi.Curso.RH.Infra.CrossCutting.Bus;
using Codeizi.Curso.RH.Infra.Data.Context;
using Codeizi.Curso.RH.Infra.Data.DAO.Contracts;
using Codeizi.Curso.RH.Infra.Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Codeizi.Curso.RH.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            AdicioneCamadaApplication(services);
            AdicioneEventosDeDominio(services);
            AdicioneComandosDeDominio(services);
            AdicioneCamadaDeDados(services);

            services.AddScoped<ICodeiziConfiguration, CodeiziConfiguration>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, EventStoreMongoDB>();
            services.AddScoped<DatabaseEventSource>();
            services.AddScoped<CodeiziContext>();
        }

        private static void AdicioneCamadaApplication(IServiceCollection services)
        {
            AddReferences(typeof(DomainToViewModelMappingProfile),
                                        typeof(DomainToViewModelMappingProfile),
                                        services);
        }

        private static void AdicioneEventosDeDominio(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }

        private static void AdicioneComandosDeDominio(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<ColaboradorAdmitidoEvent>, ColaboradorEventHandler>();
            services.AddScoped<INotificationHandler<ColaboradorAdmitidoEventSource>, ColaboradorEventHandler>();

            services.AddScoped<IRequestHandler<AdmissaoColaboradorCommand, bool>, ColaboradorCommandHandler>();
        }

        private static void AdicioneCamadaDeDados(IServiceCollection services)
        {
            AddReferences(typeof(IColaboradorDAO), typeof(IColaboradorDAO), services);
            AddReferences(typeof(IColaboradorRepository), typeof(ColaboradorRepository), services);
        }

        private static void AddReferences(Type assemblyContrato, Type assemblyImplementacao, IServiceCollection services)
        {
            var contracts = Assembly.GetAssembly(assemblyContrato)
                        .GetTypes().Where(x => x.IsInterface && !x.IsGenericType).ToList();

            contracts.ForEach(contrato =>
            {
                var implementation = Assembly.GetAssembly(assemblyImplementacao)
                .GetTypes().FirstOrDefault(x => x.IsClass && !x.IsAbstract &&
                                  x.GetInterface(contrato.Name, true) != null);

                if (implementation != null)
                    services.AddScoped(contrato, implementation);
            });
        }
    }
}