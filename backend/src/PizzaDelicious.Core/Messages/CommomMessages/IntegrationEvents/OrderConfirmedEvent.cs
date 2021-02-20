using PizzaDelicious.Core.DomainObjects.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Core.Messages.CommomMessages.IntegrationEvents
{
    public class OrderConfirmedEvent : IntegrationEvent
    {
        public OrderConfirmedEvent(Guid orderId, Guid clientId, decimal total, ListProdutctOrder listProductOrder, string cardName, string cardNumber, string cardExpiration, string cardCvv)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
            ListProductOrder = listProductOrder;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }
        public ListProdutctOrder ListProductOrder { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiration { get; private set; }
        public string CardCvv { get; private set; }
    }
}
