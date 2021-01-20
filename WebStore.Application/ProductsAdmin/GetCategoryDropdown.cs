using System.Collections.Generic;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.ProductsAdmin
{
    [Service]
    public class GetCategoryDropdown
    {
        private readonly ICategoryManager _categoryManager;

        public GetCategoryDropdown(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public IEnumerable<Response> Action() =>
            _categoryManager.GetCategoriesForDropDown(x => new Response
                {
                    Value = x.Id,
                    Text = x.CategoryName
                }); 

        public class Response
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }
 
    }
}
