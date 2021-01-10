using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebStore.Application.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISession _session;
        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCustomerInfo(CustomerInfo customerInfo)
        {
            var stringObject = JsonConvert.SerializeObject(customerInfo);
            _session.SetString("customer-info", stringObject);
        }
        public CustomerInfo GetCustomerInfo()
        {
            var stringObject = _session.GetString("customer-info");

            if (string.IsNullOrEmpty(stringObject))
                return null;

            var customerInfo = JsonConvert.DeserializeObject<CustomerInfo>(stringObject);
            return customerInfo;
        }

        public void AddCartProduct(int stockId, int qty)
        {

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");
            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }
            if (cartList.Any(x => x.StockId == stockId))
            {
                cartList.Find(x => x.StockId == stockId).Qty += qty;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = stockId,
                    Qty = qty
                });
            }

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);
        }
        public List<CartProduct> GetCart()
        {
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))
                return null;

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            return cartList;
        }
        public void DeleteCartProduct(int stockId, int qty)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject)) return;
            
            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            if (!cartList.Any(x => x.StockId == stockId)) return;
            
            var product = cartList.First(x => x.StockId == stockId);
            product.Qty -= qty;

            if (product.Qty <= 0)
                cartList.Remove(product);

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);
        }
        public string GetId() => _session.Id;
    }
}
