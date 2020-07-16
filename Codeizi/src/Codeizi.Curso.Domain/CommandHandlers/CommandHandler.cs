using Codeizi.Curso.Domain.Contracts.Repository;
using Codeizi.Curso.Domain.SharedKernel.Commands;
using Codeizi.Curso.Domain.SharedKernel.IMediatorBus;
using Codeizi.Curso.Domain.SharedKernel.Notifications;
using MediatR;

namespace Codeizi.Curso.Domain.CommandHandlers
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
#pragma warning disable CA1031 // Do not catch general exception types
            catch (System.Exception)
            {
                Bus.RaiseEvent(new DomainNotification("Commit", "Não foi possível salvar os dados"));
                return false;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}