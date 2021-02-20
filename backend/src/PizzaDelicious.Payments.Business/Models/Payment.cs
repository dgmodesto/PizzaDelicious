using PizzaDelicious.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Payments.Business.Models
{
    public class Payment : Entity, IAggregateRoot
    {
        public Guid Orderid { get; set; }
        public string Status { get; set; }
        public decimal Value { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardCvv { get; set; }

        // EF Relationship
        public Transaction Transaction { get; set; }
    }
}
