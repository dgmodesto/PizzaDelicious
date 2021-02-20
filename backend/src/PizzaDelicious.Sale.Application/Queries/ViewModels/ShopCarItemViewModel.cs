using System;

namespace PizzaDelicious.Sale.Application.Queries.ViewModels
{
    public class ShopCarItemViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue { get; set; }
    }
}