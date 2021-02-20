using MediatR;
using PizzaDelicious.Core.Messages;
using PizzaDelicious.Core.Messages.CommomMessages.DomainEvents;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task PublishDomainEvent<T>(T domainEvent) where T : DomainEvent
        {
            await _mediator.Publish(domainEvent);
        }

        public async  Task PublishEvent<T>(T events) where T : Event
        {
            await _mediator.Publish(events);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
