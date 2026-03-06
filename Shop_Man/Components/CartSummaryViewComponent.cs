using Microsoft.AspNetCore.Mvc;
using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Components
{
    public class CartSummaryViewComponent:ViewComponent
    {
        private Cart cart;

        public CartSummaryViewComponent(Cart _cart)
        {
            cart = _cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
