using PizzaDelicious.Core.DomainObjects.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Core.Messages.CommomMessages.IntegrationEvents
{
    public class OrderProcessingCanceledEvent: IntegrationEvent
    {
        public OrderProcessingCanceledEvent(Guid orderId, Guid clientId, ListProdutctOrder productsOrder)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            ProductsOrder = productsOrder;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public ListProdutctOrder  ProductsOrder { get; private set; }

    }
}
