using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Domain.Models
{
    public class Product
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enable { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Image { get; set; }
        public int QuantityStock { get; set; }
        public int Heigh { get; set; }
        public int Depth { get; set; }
        public int width { get; set; }
    }
}
