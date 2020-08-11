using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.Infra.CrossCutting.Configuration;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Codeizi.Curso.SignalrHub.Events
{
    // [ServiceMediatorBus("notificar-usuario")]
    public class NotificationUserServiceBus : IConsumerServiceBus
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ICodeiziConfiguration _codeiziConfiguration;
        public NotificationUserServiceBus(IHubContext<NotificationsHub> hubContext,
            ICodeiziConfiguration codeiziConfiguration)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _codeiziConfiguration = codeiziConfiguration;
        }

        public string RoutingKey => _codeiziConfiguration.NotificarUsuarioRoutingKey;

        public async Task Handle(Publishable publishable)
        {
            var message = publishable.ToObject<Notification>();
            await _hubContext.Clients
                    .Group(message.UserName)
                    .SendAsync("MessageUser", new { message.Message });
        }
    }
}