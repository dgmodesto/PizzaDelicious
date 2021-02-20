using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelicious.Api.UI.Controllers
{
    public abstract class ControllerBase: Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;
        protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        protected ControllerBase(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected bool ValidOperation()
        {
            return !_notifications.HasNotification();
        }

        protected IEnumerable<string> GetMessageErro()
        {
            return _notifications.GetNotifications().Select(x => x.Value).ToList();
        }

        protected void NotificateError(string code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(code, message));
        }


    }
}
