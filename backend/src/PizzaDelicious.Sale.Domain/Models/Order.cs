using FluentValidation.Results;
using PizzaDelicious.Core.DomainObjects;
using PizzaDelicious.Sale.Domain.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaDelicious.Sale.Domain.Models
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public OrderStatusEnum OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        // EF Relationship 
        public virtual Voucher Voucher {get; private set;}

        protected  Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public Order(Guid clientId, bool voucherUsed, decimal discount, decimal totalValue)
        {
            ClientId = clientId;
            VoucherUsed = voucherUsed;
            Discount = discount;
            TotalValue = totalValue;
            _orderItems = new List<OrderItem>();
        }

        public ValidationResult ApplyVoucher(Voucher voucher)
        {
            var validationResult = voucher.ValidIfApplicable();
            if (!validationResult.IsValid) return validationResult;
            Voucher = voucher;
            VoucherUsed = true;
            CalculateOrderValue();

            return validationResult;
        }

        private void CalculateOrderValue()
        {
            TotalValue = OrderItems.Sum(p => p.CalculateValue());
            CalculateDiscountTotalValue();
        }

        private void CalculateDiscountTotalValue()
        {
            if (!VoucherUsed) return;

            decimal discount = 0;
            var value = TotalValue;

            if(Voucher.TypeVoucherDiscount == TypeVoucherDiscountEnum.Percent)
            {
                if(Voucher.Percent.HasValue)
                {
                    discount = (value * Voucher.Percent.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if(Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }

            TotalValue = value < 0 ? 0 : value;
            Discount = discount;
        }

        public bool OrderItemExisting(OrderItem item)
        {
            return _orderItems.Any(p => p.ProductId == item.ProductId);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            item.AssociateOrder(Id);

            if(OrderItemExisting(item))
            {
                var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);
                existingItem.AddUnits(item.Quantity);
                item = existingItem;

                _orderItems.Remove(existingItem);
            }

            item.CalculateValue();
            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existingItem == null) throw new DomainException("O item não pertence ao pedido");

            _orderItems.Remove(existingItem);
            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void RemoveItem(OrderItem orderItem)
        {
            if (!orderItem.IsValid()) return;

            var existingItem = OrderItems.FirstOrDefault(x => x.ProductId == orderItem.ProductId);

            if (existingItem == null) throw new DomainException("O item não pertence ao pedido");
            _orderItems.Remove(existingItem);

            CalculateOrderValue();
        }

        public void UpdateUnits(OrderItem item, int units)
        {
            item.UpdateUnits(units);
            UpdateItem(item);
        }

        public void BecameDraf()
        {
            OrderStatus = OrderStatusEnum.Draft;
        }

        public void InitiateOrder()
        {
            OrderStatus = OrderStatusEnum.Initiate;
        }

        public void FinishOrder()
        {
            OrderStatus = OrderStatusEnum.PaidOut;
        }

        public void CancelOrder()
        {
            OrderStatus = OrderStatusEnum.Canceled;
        }

        //Classe dentro de outra classe é chamada de Classe aninhada
        public static class OrderFactory
        {
            public static Order NewDraftOrder(Guid clientId)
            {
                var order = new Order
                {
                    ClientId = clientId
                };
                order.BecameDraf();
                return order;
                
            }
        }

  
    }
}
