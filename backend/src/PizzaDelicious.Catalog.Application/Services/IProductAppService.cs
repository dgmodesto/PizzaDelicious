
using PizzaDelicious.Catalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        /*Product*/
        Task<ProductViewModel> GetById(Guid id);
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<IEnumerable<ProductViewModel>> GetByCategory(int code);
        Task<bool> AddProduct(ProductViewModel productViewModel);
        Task<bool> UpdateProduct(ProductViewModel productViewModel);

        /*Category*/
        Task<bool> AddCategory(CategoryViewModel productViewModel);
        Task<bool> UpdateCategory(CategoryViewModel productViewModel);
        Task<IEnumerable<CategoryViewModel>> GetCategory();

        /*Stock*/
        Task<ProductViewModel> DebitStock(Guid id, int quantity);
        Task<ProductViewModel> ResetStock(Guid id, int quantity);
    }
}
