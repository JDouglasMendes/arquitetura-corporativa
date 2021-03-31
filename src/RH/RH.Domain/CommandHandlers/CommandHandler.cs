using Domain.SharedKernel.Commands;
using Domain.SharedKernel.IMediatorBus;
using Domain.SharedKernel.Notifications;
using MediatR;
using RH.Domain.Contracts.Repository;
using System.Diagnostics.CodeAnalysis;

namespace RH.Domain.CommandHandlers
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        protected IMediatorHandler Bus { get; }
        private readonly DomainNotificationHandler _notifications;

        protected CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            Bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        [ExcludeFromCodeCoverage]
        public bool Commit()
        {
            if (_notifications.HasNotifications())
            {
                return false;
            }
            try
            {
                if (_uow.Commit())
                {
                    return true;
                }
                Bus.RaiseEvent(new DomainNotification("Commit", "Não foi possível salvar os dados"));
                return false;
            }

            catch (System.Exception ex)
            {
                Bus.RaiseEvent(new DomainNotification("Commit", $"Não foi possível salvar os dados MESSAGE:{ex.Message}"));
                return false;
            }

        }
    }
}