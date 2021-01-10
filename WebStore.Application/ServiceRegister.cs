using WebStore.Application.Cart;
using WebStore.Application.OrdersAdmin;
using WebStore.Application.ProductsAdmin;
using WebStore.Application.StockAdmin;
using WebStore.Application.UsersAdmin;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            @this.AddTransient<AddCustomerInfo>();
            @this.AddTransient<AddToCart>();
            @this.AddTransient<DeleteFromCart>();
            @this.AddTransient<GetCart>();
            @this.AddTransient<GetCustomerInfo>();
            @this.AddTransient<GetCartOrder>();

            @this.AddTransient<CreateProduct>();
            @this.AddTransient<DeleteProduct>();
            @this.AddTransient<GetProduct>();
            @this.AddTransient<GetProducts>();
            @this.AddTransient<UpdateProduct>();

            @this.AddTransient<CreateStock>();
            @this.AddTransient<DeleteStock>();
            @this.AddTransient<GetStock>();
            @this.AddTransient<UpdateStock>();

            @this.AddTransient<GetOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();

            @this.AddTransient<CreateUser>();

            return @this;
        }
    }
}
