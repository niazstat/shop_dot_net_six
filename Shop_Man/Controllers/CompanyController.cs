using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Controllers
{
    public class CompanyController : Controller
    {


        private IUserService userService;
     
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyController(IUserService _userService, IHttpContextAccessor httpContextAccessor)
        {
            userService = _userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CompanyInfo()
        {
            return View();
        }

        public JsonResult GetCompanyInfo()
        {
            Company entry;

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });

            }
          
            entry = userService.GetCompanyByUser(user.UserId);

            return Json(new ResultObj {  ResultID=1, Obj= entry });
        }

        public JsonResult UpdateCompanyInfo([FromBody] Company model)
        {
            Company entry;

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });

            }
            if (model.CompanyID == 0)
            {
                return  Json( new ResultObj { ResultID = -1, ResultMessage = "Invalid Company ID " });
            }

            else
            {
               // entry = userService.GetCompanyByUser(user.UserId);
                //userService.GetCompanyById(model.CompanyID);

            }


         
            //entry.Name = model.Name;
            //entry.Address = model.Address;
            //entry.Shopname = model.Shopname;
            //entry.ShopAddress = model.ShopAddress;

            //entry.MobileNo1 = model.MobileNo1;
            //entry.MobileNo2 = model.MobileNo2;

           
            ResultObj obj = userService.UpdateCompany(model);
          
            return Json(obj);
        }
    }
}
