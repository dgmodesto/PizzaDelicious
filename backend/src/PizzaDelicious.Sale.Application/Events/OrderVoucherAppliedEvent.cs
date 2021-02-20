using PizzaDelicious.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Application.Events
{
    public class OrderVoucherAppliedEvent: Event
    {
        public OrderVoucherAppliedEvent(Guid clientId, Guid orderId, Guid voucherId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            VoucherId = voucherId;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid VoucherId { get; private set; }
    }
}
