using System;
using System.Collections.Generic;
using WebStore.Domain.Models;

namespace WebStore.Domain.Infrastructure
{
    public interface ISessionManager
    {
        void AddCustomerInfo(CustomerInfo customerInfo);
        CustomerInfo GetCustomerInfo();
        
        string GetId();
        void AddCartProduct(CartProduct cartProduct);
        void DeleteCartProduct(int stockId, int qty);
        IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector);
        void ClearCart();

    }
}
