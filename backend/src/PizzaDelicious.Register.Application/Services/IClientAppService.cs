using PizzaDelicious.Register.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Register.Application.Services
{
    public interface IClientAppService
    {
        /*Client*/
        Task<ClientViewModel> GetById(Guid id);
        Task<IEnumerable<ClientViewModel>> GetAll();
        Task<bool> AddClient(ClientViewModel clientViewModel);
        Task<bool> UpdateClient(ClientViewModel clientViewModel);

    }
}
