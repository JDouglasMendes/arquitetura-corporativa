using Codeizi.Curso.Application.AutoMapper;
using Codeizi.Curso.Domain.Colaboradores.CommandHandlers;
using Codeizi.Curso.Domain.Colaboradores.Commands;
using Codeizi.Curso.Domain.Colaboradores.Contracts;
using Codeizi.Curso.Domain.SharedKernel.Events;
using Codeizi.Curso.Domain.SharedKernel.IMediatorBus;
using Codeizi.Curso.Domain.SharedKernel.Notifications;
using Codeizi.Curso.infra.Data.EventSource.Context;
using Codeizi.Curso.infra.Data.EventSource.EventSource;
using Codeizi.Curso.Infra.CrossCutting.Configuration;
using Codeizi.Curso.Infra.Data.Context;
using Codeizi.Curso.Infra.Data.DAO.Contracts;
using Codeizi.Curso.Infra.Data.Repository;
using Codeizi.Infra.CrossCutting.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Codeizi.Curso.Infra.CrossCutting.IoC
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
            services.AddScoped<IEventStoreRepository, EventStoreRavenDBRepository>();
            services.AddScoped<IEventStore, RavenDBEventStore>();
            services.AddScoped<DocumentStoreHolder>();
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
                {
                    services.AddScoped(contrato, implementation);
                }
            });
        }

    }
}
