using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebStore.Domain.Models;

namespace WebStore.Domain.Infrastructure
{
    public interface ICategoryManager
    {
        Task<int> CreateCategory(Category category);
        Task<int> DeleteCategory(int id);
        Task<int> UpdateCategory(Category category);
        IEnumerable<TResult> GetCategoriesForDropDown<TResult>(Expression<Func<Category, TResult>> selector);
        TResult GetCategoryById<TResult>(int id, Func<Category, TResult> selector);
        TResult GetCategoryByName<TResult>(string categoryName, Func<Category, TResult> selector);
        IEnumerable<TResult> GetCategoriesWithProducts<TResult>(Func<Category, TResult> selector);
    }
}
