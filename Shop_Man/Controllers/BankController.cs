using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Controllers
{
    public class BankController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BaknEntry()
        {
            return View();
        }
        public IActionResult BankAccountEntry()
        {
            return View();
        }
    }
}
