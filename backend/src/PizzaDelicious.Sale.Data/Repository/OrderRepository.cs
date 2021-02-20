using Microsoft.EntityFrameworkCore;
using PizzaDelicious.Core.Data;
using PizzaDelicious.Sale.Domain.Interfaces;
using PizzaDelicious.Sale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Sale.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SaleContext _context;

        public OrderRepository(SaleContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        /*Order*/
        public void Add(Order order)
        {
            _context.Add(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetListByClientId(Guid clientId)
        {
            try
            {
                
                var orders = await _context.Set<Order>().AsNoTracking()
               //.Include(p => p.OrderItems).Where(c => c.OrderItems.Select(x => x.OrderId == c.Id).FirstOrDefault())
               //.Where(x => x.ClientId == clientId)
               .ToListAsync();


                if (orders.Count == 0) return null;

                return orders;

            }
            catch (Exception ex)
            {

                return null;
            }

        }
        
        public async Task<Order> GetOrderDraftByClientId(Guid clientId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.ClientId == clientId && x.OrderStatus == Domain.Models.Enuns.OrderStatusEnum.Draft);
            if (order == null) return null;

            await _context.Entry(order)
                .Collection(x => x.OrderItems).LoadAsync();

            if (order.VoucherId != null)
            {
                await _context.Entry(order)
                    .Reference(x => x.Voucher).LoadAsync();
            }

            return order;
        }


        /*Order Items*/
        public async Task<OrderItem> GetItemById(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(x => x.ProductId == productId && x.OrderId == orderId);
        }
        public void AddItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
        }

        public void RemoveItem(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
        }
   
        public void UpdateItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
        }

        /*Voucher*/
        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(x => x.Code == code);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
