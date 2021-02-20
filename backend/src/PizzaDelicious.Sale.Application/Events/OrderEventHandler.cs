using MediatR;
using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.Messages.CommomMessages.IntegrationEvents;
using PizzaDelicious.Sale.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaDelicious.Sale.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<OrderDraftStartedEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<OrderStockRejectEvent>
    //INotificationHandler<OrderPaymentRealizedEvent>,
    //INotificationHandler<OrderPaymentRefusedEvent>,

    {
        private readonly IMediatorHandler _mediatorHandler;

        public OrderEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(OrderDraftStartedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(OrderStockRejectEvent notification, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CancelProcessingOrderCommand(notification.OrderId, notification.ClientId));
        }
    }
}
