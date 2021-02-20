using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Core.Messages.CommomMessages.DomainEvents
{
    public class DomainEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
