using EventSourcing;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaDelicious.Catalog.Application.Services;
using PizzaDelicious.Catalog.Data;
using PizzaDelicious.Catalog.Data.Repository;
using PizzaDelicious.Catalog.Domain.Events;
using PizzaDelicious.Catalog.Domain.Interface;
using PizzaDelicious.Catalog.Domain.Services;
using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.Data.EventSourcing;
using PizzaDelicious.Core.Messages.CommomMessages.IntegrationEvents;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using PizzaDelicious.Payments.AntiCorruption.Implementations;
using PizzaDelicious.Payments.AntiCorruption.Interfaces;
using PizzaDelicious.Payments.Business.Interfaces;
using PizzaDelicious.Payments.Business.services;
using PizzaDelicious.Payments.Data;
using PizzaDelicious.Payments.Data.Repository;
using PizzaDelicious.Register.Application.Services;
using PizzaDelicious.Register.Data;
using PizzaDelicious.Register.Data.Repository;
using PizzaDelicious.Register.Domain.Interfaces;
using PizzaDelicious.Sale.Application.Commands;
using PizzaDelicious.Sale.Application.Events;
using PizzaDelicious.Sale.Application.Queries;
using PizzaDelicious.Sale.Data;
using PizzaDelicious.Sale.Data.Repository;
using PizzaDelicious.Sale.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelicious.Api.UI.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            /*Mediator*/
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            /*Notifications*/
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            /*Catalog - Products*/
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockService, StockService>();

            services.AddScoped<INotificationHandler<OrderStartedEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<ProductLowStockEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<OrderProcessingCanceledEvent>, ProductEventHandler>();

            /*Register Clients*/
            services.AddScoped<IClientAppService, ClientAppService>();
            services.AddScoped<IClientRepository, ClientRepository>();

            /*Sale*/
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            
            /*Sale Commands*/
            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyVoucherOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelProcessingOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelProcessingOrderResetStockCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<FinishOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<StartOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, OrderCommandHandler>();

            /*Sale Events*/
            services.AddScoped<INotificationHandler<OrderDraftStartedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderStockRejectEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderEventHandler>();
            //services.AddScoped<INotificationHandler<OrderPaymentRefused>, OrderEventHandler>();


            /*Payments*/
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentCreditCardFacade, PaymentCrediCardFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();

            // Event Sourcing
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();


            /*Context*/
            services.AddScoped<RegisterContext>();
            services.AddScoped<CatalogContext>();
            services.AddScoped<SaleContext>();
            services.AddScoped<PaymentContext>();

        }
    }
}
