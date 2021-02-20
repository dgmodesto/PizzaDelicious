using PizzaDelicious.Core.Messages.CommomMessages.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Catalog.Domain.Events
{
    public class ProductLowStockEvent : DomainEvent
    {
        public int QuantityStock { get; private set; }

        public ProductLowStockEvent(Guid aggregateId, int quantityStock)
            :base(aggregateId)
        {
            QuantityStock = quantityStock;
        }
    }
}
