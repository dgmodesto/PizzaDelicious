using AutoMapper;
using PizzaDelicious.Catalog.Application.ViewModels;
using PizzaDelicious.Catalog.Domain.Interface;
using PizzaDelicious.Catalog.Domain.Models;
using PizzaDelicious.Catalog.Domain.Services;
using PizzaDelicious.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaDelicious.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository, IMapper mapper, IStockService stockService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _stockService = stockService;
        }



        /*Product*/
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll());
        }

        public async Task<IEnumerable<ProductViewModel>> GetByCategory(int code)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetByCategory(code));
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<bool> AddProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Add(product);

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> UpdateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.Commit();
        }

        /*Category*/
        public async Task<IEnumerable<CategoryViewModel>> GetCategory()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.GetCategories());
        }

        public async Task<bool> UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            _productRepository.Update(category);

            return await _productRepository.UnitOfWork.Commit();
        }
        public async Task<bool> AddCategory(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            _productRepository.Add(category);

            return await _productRepository.UnitOfWork.Commit();
        }



        public async Task<ProductViewModel> DebitStock(Guid id, int quantity)
        {
            if (!_stockService.DebitStock(id, quantity).Result)
            {
                throw new DomainException("Falha ao debitar estoque");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<ProductViewModel> ResetStock(Guid id, int quantity)
        {
            if (!_stockService.ResetStock(id, quantity).Result)
            {
                throw new DomainException("Falha ao debitar estoque");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }


        public void Dispose()
        {
            _productRepository?.Dispose();
            _stockService?.Dispose();
        }
    }
}
