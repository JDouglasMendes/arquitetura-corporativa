using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Codeizi.Curso.SignalrHub.Events
{
    [ServiceMediatorBus("notificar-usuario")]
    public class NotificationUserServiceBus
    {
        private readonly IHubContext<NotificationsHub> _hubContext;

        public NotificationUserServiceBus(IHubContext<NotificationsHub> hubContext)
            => _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        public async Task Handle(Publishable publishable)
        {
            var message = publishable.ToObject<Notification>();
            await _hubContext.Clients
                    .Group(message.UserName)
                    .SendAsync("MessageUser", new { message.Message });
        }
    }
}