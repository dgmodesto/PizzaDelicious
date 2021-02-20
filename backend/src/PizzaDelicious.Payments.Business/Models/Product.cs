using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Payments.Business.Models
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}
