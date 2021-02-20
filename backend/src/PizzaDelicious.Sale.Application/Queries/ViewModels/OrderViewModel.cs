using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Application.Queries.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public decimal TotalValue { get; set;}
        public DateTime RegisterDate { get; set; }
        public int OrderStatus { get; set; }
    }
}
