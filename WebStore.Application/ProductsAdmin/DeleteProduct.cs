using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private readonly IProductManager _productManager;

        public DeleteProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public Task<int> Action(int id)
        {
            return _productManager.DeleteProduct(id);
        }
    }
}
