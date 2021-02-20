using PizzaDelicious.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Application.Events
{
    public class OrderProductRemovedEvent : Event
    {
        public OrderProductRemovedEvent(Guid clientId, Guid orderId, Guid productId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            ProductId = productId;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
    }
}
