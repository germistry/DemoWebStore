using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Database.Interfaces
{
    public class ProductManager : IProductManager
    {
        private readonly ApplicationDBContext _context;

        public ProductManager(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<int> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(product);
            return _context.SaveChangesAsync();
        }
        public Task<int> UpdateProduct(Product product)
        {
            _context.Products.Update(product);

            return _context.SaveChangesAsync();
        }

        public TResult GetProductById<TResult>(int id, Func<Product, TResult> selector)
        {
            return _context.Products
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();
        }

        public TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector)
        {
            return _context.Products
                .Include(x => x.Stock).AsEnumerable()
                .Where(x => x.Name == name)
                .Select(selector)
                .FirstOrDefault();
        }
        public IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector)
        {
            return _context.Products
                .Include(x => x.Stock).AsEnumerable()
                .Select(selector)
                .ToList();
        }

        
    }
}
