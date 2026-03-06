using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
 

namespace Shop_Man.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
           // OrderLine or=new OrderLine {  MM_TO_SFT= MM}
            return View();
        }

        public IActionResult NewOrder()
        {
            // OrderLine or=new OrderLine {  MM_TO_SFT= MM}
            return View();
        }
    }
}
