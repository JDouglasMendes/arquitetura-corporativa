using Domain.SharedKernel.IMediatorBus;
using Domain.SharedKernel.Notifications;
using Infra.CrossCutting.Bus;
using Infra.CrossCutting.Configuration;
using Infra.CrossCutting.Identity;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RH.Application.AutoMapper;
using RH.Domain.Colaboradores.CommandHandlers;
using RH.Domain.Colaboradores.Commands;
using RH.Domain.Colaboradores.Contracts;
using RH.Domain.Colaboradores.EventHandlers;
using RH.Domain.Colaboradores.Events;
using RH.Domain.Ocorrencias.Ferias.CommandHandlers;
using RH.Domain.Ocorrencias.Ferias.Commands;
using RH.Domain.Ocorrencias.Ferias.Events;
using RH.Domain.Ocorrencias.Ferias.EventsHandlers;
using RH.Domain.Ocorrencias.Ferias.Validations;
using RH.Infra.Data.Context;
using RH.Infra.Data.DAO.Contracts;
using RH.Infra.Data.Repository;
using System;
using System.Linq;
using System.Reflection;

namespace RH.Infra.CrossCutting.IoC
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
            services.AddScoped<CodeiziContext>();

            services.AddScoped<IUser, UserContext>();

            services.AddScoped<RegistrarOcorrenciaDeFeriasCommandValidation>();
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
            services.AddScoped<INotificationHandler<NovoColaboradorParaCalculoEvent>, ColaboradorEventHandler>();
            services.AddScoped<INotificationHandler<ColaboradorEventSource>, ColaboradorEventHandler>();
            services.AddScoped<INotificationHandler<ContratoQueryEvent>, ColaboradorEventHandler>();
            services.AddScoped<INotificationHandler<AgendamentoDeFeriasQueryEvent>, FeriasEventHandler>();

            services.AddScoped<IRequestHandler<AdmissaoColaboradorCommand, bool>, ColaboradorCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarOcorrenciaDeFeriasCommand, bool>, FeriasCommandHandler>();
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