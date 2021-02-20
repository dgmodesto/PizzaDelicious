using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using PizzaDelicious.Sale.Application.Queries;

namespace PizzaDelicious.Api.UI.Controllers
{
    [Route("api/sale/orders")]
    public class SaleOrdersController : ControllerBase
    {
        private readonly IOrderQueries _orderQueries;

        public SaleOrdersController(
            IOrderQueries orderQueries,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler
            ) : base(notifications, mediatorHandler)
        {
            _orderQueries = orderQueries;
        }

        [HttpGet]
        [Route("order-by-client")]
        public async Task<IActionResult> GetOrderByClient(Guid clientId)
        {
            return Ok(await _orderQueries.GetOrdersClient(clientId));
        }

        [HttpGet]
        [Route("shopping-cart-by-client")]
        public async Task<IActionResult> GetCartClient(Guid clientId)
        {
            return Ok(await _orderQueries.GetCarClient(clientId));
        }
    }
}
