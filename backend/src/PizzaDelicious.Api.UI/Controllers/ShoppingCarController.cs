using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaDelicious.Catalog.Application.Services;
using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using PizzaDelicious.Sale.Application.Commands;
using PizzaDelicious.Sale.Application.Queries;
using PizzaDelicious.Sale.Application.Queries.ViewModels;

namespace PizzaDelicious.Api.UI.Controllers
{
    [Route("api/shopping/car-client")]
    public class ShoppingCarController : ControllerBase
    {

        private readonly IProductAppService _productAppService;
        private readonly IOrderQueries _orderQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public ShoppingCarController(
            INotificationHandler<DomainNotification> notifications,
            IProductAppService productAppService, 
            IOrderQueries orderQueries, 
            IMediatorHandler mediatorHandler)
            :base(notifications, mediatorHandler)
        {
            _productAppService = productAppService;
            _orderQueries = orderQueries;
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet]
        [Route("my-car")]
        public async Task<IActionResult> GetCartClient(Guid clientId)
        {
            return Ok(await _orderQueries.GetCarClient(clientId));
        }

        [HttpPost]
        [Route("my-car")]
        public async Task<IActionResult> AddItem(Guid clientId, Guid productId, int quantity)
        {
            var product = await _productAppService.GetById(productId);
            if (product == null) return BadRequest("Produto não existe no catálogo");

            if(product.QuantityStock < quantity)
            {
                return BadRequest("produto com estoque insuficiente");
            }

            var command = new AddOrderItemCommand(clientId, product.Id, product.Name, quantity, product.Value);
            await _mediatorHandler.SendCommand(command);

            if(ValidOperation())
            {
                return Ok("Item adicionado ao carrinho");
            }
    
            return BadRequest(GetMessageErro());
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoveItem(Guid clientId, Guid productId)
        {
            var product = await _productAppService.GetById(productId);
            if (product == null) return BadRequest("Produto não existe no catálogo");

            var command = new RemoveOrderItemCommand(clientId, productId);
            await _mediatorHandler.SendCommand(command);

            if (ValidOperation())
            {
                return Ok(await _orderQueries.GetCarClient(clientId)); 
            }

            return BadRequest(GetMessageErro());
        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem(Guid clientId, Guid productId, int quantity)
        {
            var product = await _productAppService.GetById(productId);
            if (product == null) return BadRequest("Produto não existe no catálogo");

            var command = new UpdateOrderItemCommand(clientId, productId, quantity);
            await _mediatorHandler.SendCommand(command);

            if (ValidOperation())
            {
                return Ok(await _orderQueries.GetCarClient(clientId));
            }

            return BadRequest(GetMessageErro());
        }


        [HttpPost]
        [Route("apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(Guid clientId,  string voucherCode)
        {
            var command = new ApplyVoucherOrderCommand(clientId, voucherCode);
            await _mediatorHandler.SendCommand(command);

            if(ValidOperation())
            {
                return Ok(await _orderQueries.GetCarClient(clientId));
            }

            return BadRequest(GetMessageErro());
        }

        [HttpGet]
        [Route("buy-summary")]
        public async Task<IActionResult> BuySummary(Guid clientId)
        {
            return Ok(await _orderQueries.GetCarClient(clientId));
        }

        [HttpPost]
        [Route("start-order")]
        public async Task<IActionResult> StartOrder(Guid clientId, ShopCarViewModel model)
        {
            var car = await _orderQueries.GetCarClient(clientId);

            var command = new StartOrderCommand(car.OrderId, clientId, car.TotalValue, 
                model.Payment.CardName, model.Payment.CardNumber, model.Payment.CardExpiration, model.Payment.CardCvv);

            await _mediatorHandler.SendCommand(command);

            if(ValidOperation())
            {

                return Ok(await _orderQueries.GetCarClient(clientId));
            }

            return BadRequest(GetMessageErro());
        }

    }
}
