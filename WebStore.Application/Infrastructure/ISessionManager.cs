using System.Collections.Generic;
using WebStore.Domain.Models;

namespace WebStore.Application.Infrastructure
{
    public interface ISessionManager
    {
        void AddCustomerInfo(CustomerInfo customerInfo);
        CustomerInfo GetCustomerInfo();
        
        string GetId();
        void AddCartProduct(int stockId, int qty);
        void DeleteCartProduct(int stockId, int qty);
        List<CartProduct> GetCart();

    }
}
