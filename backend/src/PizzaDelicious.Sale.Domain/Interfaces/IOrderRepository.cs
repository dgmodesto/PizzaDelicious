using PizzaDelicious.Core.Data;
using PizzaDelicious.Sale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Sale.Domain.Interfaces
{
    public interface IOrderRepository: IRepository<Order>
    {
        //Order
        Task<Order> GetById(Guid id);
        Task<IEnumerable<Order>> GetListByClientId(Guid clientId);
        Task<Order> GetOrderDraftByClientId(Guid clientId);
        void Add(Order order);
        void Update(Order order);

        //Order Item
        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);
        void AddItem(OrderItem orderItem);
        void UpdateItem(OrderItem orderItem);
        void RemoveItem(OrderItem orderItem);

        //Voucher
        Task<Voucher> GetVoucherByCode(string code);
    }
}
