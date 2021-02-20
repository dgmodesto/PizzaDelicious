using PizzaDelicious.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Domain.Models
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }

        //EF Relationship
        public virtual Order Order { get; set; }

        protected OrderItem(){}

        public OrderItem(Guid productId, string productName, int quantity, decimal unitValue)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitValue = unitValue;
        }

        public void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public decimal CalculateValue()
        {
            return Quantity * UnitValue;
        }

        public void AddUnits(int units)
        {
            Quantity += units;
        }

        public void UpdateUnits(int units)
        {
            Quantity = units;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
