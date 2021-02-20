using MediatR;
using PizzaDelicious.Catalog.Domain.Interface;
using PizzaDelicious.Catalog.Domain.Services;
using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.Messages.CommomMessages.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaDelicious.Catalog.Domain.Events
{
    public class ProductEventHandler :
        INotificationHandler<ProductLowStockEvent>,
        INotificationHandler<OrderStartedEvent>,
        INotificationHandler<OrderProcessingCanceledEvent>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IStockService _stockService;

        public ProductEventHandler(IProductRepository productRepository, IMediatorHandler mediatorHandler, IStockService stockService)
        {
            _productRepository = productRepository;
            _mediatorHandler = mediatorHandler;
            _stockService = stockService;
        }

        public async Task Handle(OrderStartedEvent notification, CancellationToken cancellationToken)
        {
            var result = await _stockService.DebitListProductOrder(notification.ListProductOrder);
            if(result)
            {
                await _mediatorHandler.PublishEvent(new OrderConfirmedEvent(notification.OrderId, notification.ClientId, notification.Total, notification.ListProductOrder, notification.CardName, notification.CardNumber, notification.CardExpiration, notification.CardCvv));
            } else
            {
                await _mediatorHandler.PublishEvent(new OrderStockRejectEvent(notification.OrderId, notification.ClientId));
            }
        }

        public async Task Handle(ProductLowStockEvent notification, CancellationToken cancellationToken)
        {
            var produto = await _productRepository.GetById(notification.AggregateId);

            // Enviar um email notificando que o stock deste produto está abaixo do desejado
            // Podemos jogar essa informação numa fila, pra outro sistema de monitoria receber essa informação e mostrar em algum dashboard
        }

        public async Task Handle(OrderProcessingCanceledEvent notification, CancellationToken cancellationToken)
        {
            await _stockService.ResetListProductOrder(notification.ProductsOrder);
        }
    }
}
