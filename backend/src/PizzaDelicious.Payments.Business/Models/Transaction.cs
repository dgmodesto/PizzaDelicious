using PizzaDelicious.Core.DomainObjects;
using PizzaDelicious.Payments.Business.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Payments.Business.Models
{
    public class Transaction : Entity
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set;}
        public decimal Total { get; set; }
        public StatusTransactionEnum StatusTransaction { get; set; }

        //EF Relationship
        public Payment Payment { get; set; }
    }
}
