using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaDelicious.Catalog.Application.Services;
using PizzaDelicious.Catalog.Application.ViewModels;
using PizzaDelicious.Catalog.Domain.Services;
using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelicious.Api.UI.Controllers
{
    [Route("api/catalog/products")]
    public class CatalogProductsController : ControllerBase
    {

        private readonly IProductAppService _productAppService;
        private readonly IStockService _stockService;
        public CatalogProductsController(IProductAppService productAppService, IStockService stockService, INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
            :base(notifications, mediatorHandler)
        {
            _productAppService = productAppService;
            _stockService = stockService;
        }

        /*Products*/
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_productAppService.GetAll().Result.ToList());
        }

        [HttpGet]
        [Route(":id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(_productAppService.GetById(id).Result);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddProducts([FromBody] ProductViewModel productView)
        {
            return Ok(_productAppService.AddProduct(productView).Result);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateProducts([FromBody] ProductViewModel productView)
        {
            return Ok(_productAppService.UpdateProduct(productView).Result);
        }

        /*Categories*/

        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(_productAppService.GetCategory().Result.ToList());
        }


        [HttpPost]
        [Route("categories")]
        public async Task<IActionResult> AddCategories([FromBody] CategoryViewModel categoryViewModel)
        {
            return Ok(_productAppService.AddCategory(categoryViewModel).Result);
        }

        [HttpPut]
        [Route("categories")]
        public async Task<IActionResult> UpdateCategories([FromBody] CategoryViewModel categoryViewModel)
        {
            return Ok(_productAppService.UpdateCategory(categoryViewModel));
        }

        /*Stock*/

        [HttpPost]
        [Route("debit-stock")]
        public async Task<IActionResult> DebitStock(Guid id, int quantity)
        {
            return Ok(_stockService.DebitStock(id, quantity).Result);
        }


        [HttpPost]
        [Route("reset-stock")]
        public async Task<IActionResult> ResetStock(Guid id, int quantity)
        {
            return Ok(_stockService.ResetStock(id, quantity).Result);
        }
    }
}
