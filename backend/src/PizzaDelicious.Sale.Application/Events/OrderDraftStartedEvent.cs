using PizzaDelicious.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Application.Events
{
    public class OrderDraftStartedEvent : Event
    {
        public OrderDraftStartedEvent(Guid clientId, Guid orderId)
        {
            ClientId = clientId;
            OrderId = orderId;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
    }
}
