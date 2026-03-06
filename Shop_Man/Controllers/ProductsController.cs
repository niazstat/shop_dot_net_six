using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SizeEntry()
        {
            return View();
        }

        public IActionResult ColorEntry()
        {
            return View();
        }

        public IActionResult ProductEntry()
        {
            return View();
        }
    }
}
