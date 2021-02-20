using PizzaDelicious.Core.Messages;
using PizzaDelicious.Core.Messages.CommomMessages.DomainEvents;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T events) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        Task PublishDomainEvent<T>(T notification) where T : DomainEvent;
    }
}
