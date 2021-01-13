using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISession _session;
        private const string Cart = "cart";
        private const string CustomerInfo = "customer-info";
        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCustomerInfo(CustomerInfo customerInfo)
        {
            var stringObject = JsonConvert.SerializeObject(customerInfo);
            _session.SetString(CustomerInfo, stringObject);
        }
        public CustomerInfo GetCustomerInfo()
        {
            var stringObject = _session.GetString(CustomerInfo);

            if (string.IsNullOrEmpty(stringObject))
                return null;

            var customerInfo = JsonConvert.DeserializeObject<CustomerInfo>(stringObject);
            return customerInfo;
        }

        public void AddCartProduct(CartProduct cartProduct)
        {

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString(Cart);
            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }
            if (cartList.Any(x => x.StockId == cartProduct.StockId))
            {
                cartList.Find(x => x.StockId == cartProduct.StockId).Qty += cartProduct.Qty;
            }
            else
            {
                cartList.Add(cartProduct);
            }

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString(Cart, stringObject);
        }
        public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector)
        {
            var stringObject = _session.GetString(Cart);

            if (string.IsNullOrEmpty(stringObject))
                return new List<TResult>();

            var cartList = JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(stringObject);
            return cartList.Select(selector);
        }
        public void DeleteCartProduct(int stockId, int qty)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString(Cart);

            if (string.IsNullOrEmpty(stringObject)) return;
            
            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            if (!cartList.Any(x => x.StockId == stockId)) return;
            
            var product = cartList.First(x => x.StockId == stockId);
            product.Qty -= qty;

            if (product.Qty <= 0)
                cartList.Remove(product);

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString(Cart, stringObject);
        }
        public string GetId() => _session.Id;

        public void ClearCart()
        {
            _session.Remove(Cart);
        }
    }
}
