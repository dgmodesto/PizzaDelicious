using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Application.Queries.ViewModels
{
    public class ShopCarViewModel
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal  SubTotal { get; set; }
        public decimal TotalValue { get; set; }
        public decimal ValueDiscount { get; set; }
        public string VoucherCode { get; set; }

        public List<ShopCarItemViewModel> Items { get; set; } = new List<ShopCarItemViewModel>();
        public ShopCarPaymentViewModel Payment { get; set; }
    }
}
