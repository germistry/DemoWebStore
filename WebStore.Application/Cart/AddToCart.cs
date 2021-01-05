﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Database;
using WebStore.Domain.Models;

namespace WebStore.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;
        private ApplicationDBContext _context;

        public AddToCart(ISession session, ApplicationDBContext context)
        {
            _session = session;
            _context = context;
        }

        public async Task<bool> Action(Request request)
        {
            var stockOnHold = _context.StocksOnHold
                .Where(x => x.SessionId == _session.Id)
                .ToList();

            var stockToHold = _context.Stock
                .Where(x => x.Id == request.StockId)
                .FirstOrDefault();

            if(stockToHold.Qty < request.Qty)
            {
                return false;
            }
            _context.StocksOnHold.Add(new StockOnHold
            {
                StockId = stockToHold.Id,
                SessionId = _session.Id,
                Qty = request.Qty,
                ExpiryDate = DateTime.Now.AddMinutes(30)
            });
            stockToHold.Qty = stockToHold.Qty - request.Qty;

            //TODO Better way to do this would be using a filter. 
            foreach(var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTime.Now.AddMinutes(30);
            }

            await _context.SaveChangesAsync();

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");
            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }
            if(cartList.Any(x => x.StockId == request.StockId))
            {
                cartList.Find(x => x.StockId == request.StockId).Qty += request.Qty;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = request.StockId,
                    Qty = request.Qty
                });
            }
            
            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
