using MediatR;
using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.DomainObjects.DTO;
using PizzaDelicious.Core.Extensions;
using PizzaDelicious.Core.Messages;
using PizzaDelicious.Core.Messages.CommomMessages.DomainEvents;
using PizzaDelicious.Core.Messages.CommomMessages.IntegrationEvents;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using PizzaDelicious.Sale.Application.Events;
using PizzaDelicious.Sale.Domain.Interfaces;
using PizzaDelicious.Sale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaDelicious.Sale.Application.Commands
{
    public class OrderCommandHandler :
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<UpdateOrderItemCommand, bool>,
        IRequestHandler<RemoveOrderItemCommand, bool>,
        IRequestHandler<ApplyVoucherOrderCommand, bool>,
        IRequestHandler<StartOrderCommand, bool>,
        IRequestHandler<FinishOrderCommand, bool>,
        IRequestHandler<CancelProcessingOrderResetStockCommand, bool>,
        IRequestHandler<CancelProcessingOrderCommand, bool>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }


        public async Task<bool> Handle(StartOrderCommand request, CancellationToken cancellationToken)
        {
            if (!ValidCommand(request)) return false;

            var order = await _orderRepository.GetOrderDraftByClientId(request.ClientId);
            order.InitiateOrder();

            var listItem = new List<Item>();
            order.OrderItems.ForEach(i => listItem.Add(new Item { Id = i.ProductId, Quantity = i.Quantity }));
            var listProductOrder = new ListProdutctOrder { OrderId = order.Id, Itens = listItem };

            order.AddEvent(new OrderStartedEvent(order.Id, request.ClientId, order.TotalValue, listProductOrder, request.CardName, request.CardNumber, request.CardExpiration, request.CardCvv));

            _orderRepository.Update(order);
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            if (!ValidCommand(request)) return false;

            var order = await _orderRepository.GetOrderDraftByClientId(request.ClientId);
            var orderItem = new OrderItem(request.ProductId, request.Name, request.Quantity, request.UnitValue);

            if (order == null)
            {
                order = Order.OrderFactory.NewDraftOrder(request.ClientId);
                order.AddItem(orderItem);

                _orderRepository.Add(order);
                order.AddEvent(new OrderDraftStartedEvent(request.ClientId, request.ProductId));
            }
            else
            {
                var orderItemExisting = order.OrderItemExisting(orderItem);
                order.AddItem(orderItem);

                if (orderItemExisting)
                {
                    _orderRepository.UpdateItem(order.OrderItems.FirstOrDefault(x => x.ProductId == orderItem.ProductId));
                }
                else
                {
                    _orderRepository.AddItem(orderItem);
                }
            }

            order.AddEvent(new OrderItemAddedEvent(order.ClientId, order.Id, request.ProductId, request.Name, request.UnitValue, request.Quantity));
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            if (!ValidCommand(request)) return false;

            var order = await _orderRepository.GetOrderDraftByClientId(request.ClientId);
            
            if(order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Pedido não encontrado"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, request.ProductId);

            if(!order.OrderItemExisting(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Item do pedido não encontrado"));
                return false;
            }

            order.UpdateUnits(orderItem, request.Quantity);
            order.AddEvent(new OrderProductAddedEvent(request.ClientId, order.Id, request.ProductId, request.Quantity));

            _orderRepository.UpdateItem(orderItem);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
        {
            if (ValidCommand(request)) return false;

            var order = await _orderRepository.GetOrderDraftByClientId(request.Clientid);
            if(order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Item do pedido não encontrado"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, request.ProductId);

            if (orderItem == null && !order.OrderItemExisting(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Item do pedido não encontrado"));
                return false;
            }

            order.RemoveItem(orderItem);
            order.AddEvent(new OrderProductRemovedEvent(request.Clientid, order.Id, request.ProductId));

            _orderRepository.RemoveItem(orderItem);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();



        }

        public async Task<bool> Handle(ApplyVoucherOrderCommand request, CancellationToken cancellationToken)
        {
            if (!ValidCommand(request)) return false;

            var order = await _orderRepository.GetOrderDraftByClientId(request.ClientId);

            if(order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Pedido não encontrado"));
                return false;
            }

            var voucher = await _orderRepository.GetVoucherByCode(request.VoucherCode);

            if(voucher == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Voucher não encontrado"));
                return false;
            }

            var voucherApplicationValidation = order.ApplyVoucher(voucher);
            if(!voucherApplicationValidation.IsValid)
            {
                foreach(var error in voucherApplicationValidation.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }
                return false;
            }

            order.AddEvent(new OrderVoucherAppliedEvent(request.ClientId, order.Id, voucher.Id));
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async  Task<bool> Handle(FinishOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.OrderId);

            if(order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Pedido não encontrado"));
                return false;
            }

            order.FinishOrder();

            order.AddEvent(new OrderFinishedEvent(request.OrderId));
            return await _orderRepository.UnitOfWork.Commit();
            
        }

        public async Task<bool> Handle(CancelProcessingOrderResetStockCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.OrderId);

            if(order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Pedido não encontrado"));
                return false;
            }

            var listItem = new List<Item>();
            order.OrderItems.ForEach(i => listItem.Add(new Item { Id = i.ProductId, Quantity = i.Quantity }));
            var listProductOrder = new ListProdutctOrder { OrderId = order.Id, Itens = listItem };

            order.AddEvent(new OrderProcessingCanceledEvent(order.Id, request.ClientId, listProductOrder));
            order.BecameDraf();

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelProcessingOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.OrderId);

            if(order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Pedido não encontrado"));
                return false;
            }

            order.BecameDraf();
            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool ValidCommand(Command message)
        {
            if (message.IsValid()) return true;

            // Se houver alguma erro de validação dos comandos será publicado uma notificação pra cada erro.
            foreach(var erro in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, erro.ErrorMessage));
            }
            return false;
        }
    }
}
