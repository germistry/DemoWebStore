using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.CategoriesAdmin
{
    [Service]
    public class DeleteCategory
    {
        private readonly ICategoryManager _categoryManager;

        public DeleteCategory(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        public Task<int> Action(int id)
        {
            return _categoryManager.DeleteCategory(id);
        }
    }
}
