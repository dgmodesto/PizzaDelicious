using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Payments.Business.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public List<Product> Products {get; set;}
    }
}
