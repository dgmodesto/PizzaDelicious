using PizzaDelicious.Sale.Application.Queries.ViewModels;
using PizzaDelicious.Sale.Domain.Interfaces;
using PizzaDelicious.Sale.Domain.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Sale.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ShopCarViewModel> GetCarClient(Guid clientId)
        {
            var order = await _orderRepository.GetOrderDraftByClientId(clientId);
            if (order == null) return null;

            var cart = new ShopCarViewModel
            {
                ClientId = order.ClientId,
                TotalValue = order.TotalValue,
                OrderId = order.Id,
                ValueDiscount = order.Discount,
                SubTotal = order.Discount - order.TotalValue
            };

            if(order.VoucherId != null)
            {
                cart.VoucherCode = order.Voucher.Code;
            }

            foreach(var item in order.OrderItems)
            {
                cart.Items.Add(new ShopCarItemViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitValue = item.UnitValue,
                    TotalValue = item.UnitValue
                });
            }

            return cart;
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersClient(Guid clientId)
        {
            var orders = await _orderRepository.GetListByClientId(clientId);

            orders = orders.Where(x => x.OrderStatus == OrderStatusEnum.PaidOut || x.OrderStatus == OrderStatusEnum.Canceled)
                .OrderByDescending(x => x.Code);

            if (!orders.Any()) return null;

            var ordersView = new List<OrderViewModel>();

            foreach(var order in orders)
            {
                ordersView.Add(new OrderViewModel
                {
                    Id = order.Id,
                    TotalValue = order.TotalValue,
                    OrderStatus = (int)order.OrderStatus,
                    Code = order.Code,
                    RegisterDate = order.RegisterDate
                });
            }

            return ordersView;

        }
    }
}
