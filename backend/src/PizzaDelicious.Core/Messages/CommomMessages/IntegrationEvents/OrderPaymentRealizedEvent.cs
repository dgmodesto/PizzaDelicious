using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Core.Messages.CommomMessages.IntegrationEvents
{
    public class OrderPaymentRealizedEvent : IntegrationEvent
    {
        public OrderPaymentRealizedEvent(Guid orderId, Guid clientId, Guid paymentId, Guid transactionId, decimal total)
        {
            OrderId = orderId;
            ClientId = clientId;
            PaymentId = paymentId;
            TransactionId = transactionId;
            Total = total;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid PaymentId { get; private set; }
        public Guid TransactionId { get; private set; }
        public decimal Total { get; private set; }
    }
}
