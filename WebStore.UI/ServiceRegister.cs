using System.Linq;
using System.Reflection;
using WebStore.Application;
using WebStore.Database.Interfaces;
using WebStore.Domain.Infrastructure;
using WebStore.UI.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;
            
            var services = definedTypes
                .Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach (var service in services)
            {
                @this.AddTransient(service);
            }
            
            @this.AddTransient<IStockManager, StockManager>();
            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddTransient<IProductManager, ProductManager>();
            @this.AddScoped<ISessionManager, SessionManager>();

            return @this;
        }
    }
}
