using PizzaDelicious.Sale.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Sale.Application.Queries
{
    public interface IOrderQueries
    {
        Task<ShopCarViewModel> GetCarClient(Guid clientId);
        Task<IEnumerable<OrderViewModel>> GetOrdersClient(Guid clientId);
    }
}
