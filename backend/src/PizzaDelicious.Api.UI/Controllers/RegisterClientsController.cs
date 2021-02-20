using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using PizzaDelicious.Register.Application.Services;
using PizzaDelicious.Register.Application.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelicious.Api.UI.Controllers
{
    [Route("api/register/clients")]
    public class RegisterClientsController : ControllerBase
    {

        private readonly IClientAppService _clientAppService;

        public RegisterClientsController(IClientAppService clientAppService, INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
            :base(notifications, mediatorHandler)
        {
            _clientAppService = clientAppService;
        }

        /*Products*/
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_clientAppService.GetAll().Result.ToList());
        }

        [HttpGet]
        [Route(":id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(_clientAppService.GetById(id).Result);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddProducts([FromBody] ClientViewModel clientViewModel)
        {
            return Ok(_clientAppService.AddClient(clientViewModel));
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateProducts([FromBody] ClientViewModel clientViewModel)
        {
            return Ok(_clientAppService.UpdateClient(clientViewModel));
        }

    }
}
