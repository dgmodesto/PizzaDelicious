using PizzaDelicious.Core.DomainObjects.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Catalog.Domain.Services
{
    public interface IStockService : IDisposable
    {
        Task<bool> DebitStock(Guid productId, int quantity);
        Task<bool> DebitListProductOrder(ListProdutctOrder list);
        Task<bool> ResetStock(Guid productId, int quantity);
        Task<bool> ResetListProductOrder(ListProdutctOrder list);
    }
}
