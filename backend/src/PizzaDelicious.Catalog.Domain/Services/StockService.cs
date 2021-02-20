using PizzaDelicious.Catalog.Domain.Events;
using PizzaDelicious.Catalog.Domain.Interface;
using PizzaDelicious.Catalog.Domain.Models;
using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.DomainObjects.DTO;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Catalog.Domain.Services
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public StockService(IProductRepository productRepository, IMediatorHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> DebitListProductOrder(ListProdutctOrder list)
        {
            foreach(var item in list.Itens)
            {
                if (!await DebitStock(item.Id, item.Quantity)) return false;

            }
            return await _productRepository.UnitOfWork.Commit();

        }

        public async Task<bool> DebitStock(Guid productId, int quantity)
        {
            if (!await DebitItemStock(productId, quantity)) return false;

            return await _productRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DebitItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;

            if(!product.HasStock(quantity))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Estoque", $"Produto - {product.Name} sem estoque"));
            }

            product.DebitStock(quantity);

            VerifyStockAndSendEventLowStock(product);

            _productRepository.Update(product);
            return true;
        }

        private async void VerifyStockAndSendEventLowStock(Product product)
        {
            // TODO: 10 pode ser parametrizável em arquivo de configuração
            int valueMinSotck = 10;
            if (product.QuantityStock < valueMinSotck)
            {
                await _mediatorHandler.PublishDomainEvent(new ProductLowStockEvent(product.Id, product.QuantityStock));
            }
        }

        public async Task<bool> ResetListProductOrder(ListProdutctOrder list)
        {
            foreach(var item in list.Itens)
            {
                await ResetItemStock(item.Id, item.Quantity);
            }
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ResetStock(Guid productId, int quantity)
        {
            if (!await ResetItemStock(productId, quantity)) return false;
            return await _productRepository.UnitOfWork.Commit();
        }

        private async Task<bool> ResetItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;

            product.ResetStock(quantity);
            _productRepository.Update(product);
            return true;
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }

    }
}
