using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Database.Interfaces
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ApplicationDBContext _context;

        public CategoryManager(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<int> CreateCategory(Category category)
        {
            _context.Category.Add(category);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteCategory(int id)
        {
            var category = _context.Category.FirstOrDefault(x => x.Id == id);
            _context.Category.Remove(category);
            return _context.SaveChangesAsync();
        }

        public Task<int> UpdateCategory(Category category)
        {
            _context.Category.Update(category);

            return _context.SaveChangesAsync();
        }

        public TResult GetCategoryById<TResult>(int id, Func<Category, TResult> selector)
        {
            return _context.Category
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();
        }

        public TResult GetCategoryByName<TResult>(string categoryName, Func<Category, TResult> selector)
        {
            return _context.Category
                .Include(x => x.Products).AsEnumerable()
                .Where(x => x.CategoryName == categoryName)
                .Select(selector)
                .FirstOrDefault();
        }

        public IEnumerable<TResult> GetCategoriesWithProducts<TResult>(Func<Category, TResult> selector)
        {
            return _context.Category
                .Include(x => x.Products).AsEnumerable()
                .Select(selector)
                .ToList();
        }
    }
}
