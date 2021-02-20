using Microsoft.EntityFrameworkCore;
using PizzaDelicious.Catalog.Domain.Interface;
using PizzaDelicious.Catalog.Domain.Models;
using PizzaDelicious.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Catalog.Data.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;


        public async Task<IEnumerable<Product>> GetAll()
        {
            var result = await _context.Set<Product>().AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetByCategory(int code)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(c => c.Category)
                .Where(c => c.Category.Code == code)
                .ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            var result = await _context.Products.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var result = await _context.Set<Category>().ToListAsync<Category>();
                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
