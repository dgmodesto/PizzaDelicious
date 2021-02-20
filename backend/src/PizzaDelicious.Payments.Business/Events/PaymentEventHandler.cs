using MediatR;
using PizzaDelicious.Core.DomainObjects.DTO;
using PizzaDelicious.Core.Messages.CommomMessages.IntegrationEvents;
using PizzaDelicious.Payments.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaDelicious.Payments.Business.Events
{
    public class PaymentEventHandler : INotificationHandler<OrderStockConfirmedEvent>
    {

        private readonly IPaymentService _paymentService;

        public PaymentEventHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(OrderStockConfirmedEvent notification, CancellationToken cancellationToken)
        {
            var paymentOrder = new PaymentOrder
            {
                OrderId = notification.OrderId,
                ClientId = notification.ClientId,
                Total = notification.Total,
                CardName = notification.CardName,
                CardNumber = notification.CardNumber,
                CardExpiration = notification.CardExpiration,
                CardCvv = notification.CardCvv
            };

            await _paymentService.RealizePaymentOrder(paymentOrder);
        }
    }
}
