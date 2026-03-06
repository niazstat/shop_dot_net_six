using Microsoft.AspNetCore.Mvc;
using Shop_Man.Infrastructure;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Controllers
{
    public class CustomerCategoryController : Controller
    {
        private ICustomerCategory customerCategoryRepo;
        private ICustomerSubCategory customerSubCategoryRepo;
        public CustomerCategoryController(ICustomerCategory _customerCategoryRepo, ICustomerSubCategory _customerSubCategoryRepo)
        {
            customerCategoryRepo = _customerCategoryRepo;
            customerSubCategoryRepo = _customerSubCategoryRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult InsertNewCustomerCategory([FromBody]CustomerCategory customerCategory)
        {
            ResultObj obj = customerCategoryRepo.SaveCustomerCategory(customerCategory);
            if(obj.ResultID != -1)
            {
                obj.Obj = customerCategoryRepo.CustomerCategorys;
            }
            return    Json(obj);
        }

        [Authorize]
        public ActionResult InsertNewSubCustomerCategory([FromBody]CustomerSubCategory customerSubCategory)
        {
            ResultObj obj = customerSubCategoryRepo.SaveCustomerSubCategory(customerSubCategory);
            if (obj.ResultID != -1)
            {
                obj.Obj = customerSubCategoryRepo.CustomerSubCategorys.ToList();
            }
            return Json(obj);
        }

        [Authorize]
        public ActionResult CustomerSubCategorysInCategory([FromBody]CustomerCategory customerCategory)
        {
            var obj = customerSubCategoryRepo.CustomerSubCategorysInCategory(customerCategory.CustomerCategoryID);

            return Json(obj);
        }


        [Authorize]
        public ActionResult DeleteSubCustomerCategory([FromBody] CustomerSubCategory customerSubCategory)
        {
            ResultObj obj = customerSubCategoryRepo.DeleteCustomerSubCategory(customerSubCategory);
            if (obj.ResultID != -1)
            {
                obj.Obj = customerSubCategoryRepo.CustomerSubCategorys.ToList();
            }
            return Json(obj);
        }

        [Authorize]
        public ActionResult DeleteCustomerCategory([FromBody] CustomerCategory customerCategory)
        {
            ResultObj obj = customerCategoryRepo.DeleteCustomerCategory(customerCategory);
            if (obj.ResultID != -1)
            {
                obj.Obj = customerCategoryRepo.CustomerCategorys;
            }
            return Json(obj);
        }


        [Authorize]
        public ActionResult GetCustomerCategory([FromBody] CustomerCategory customerCategory)
        {
            //  var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            CustomerCategory cust = customerCategoryRepo.CustomerCategorys.SingleOrDefault(a=>a.CustomerCategoryID==customerCategory.CustomerCategoryID);

            return Json(cust);
        }


        [Authorize]
        public ActionResult GetSubCustomerCategory([FromBody] CustomerSubCategory customerSubCategory)
        {
            CustomerSubCategory obj = customerSubCategoryRepo.CustomerSubCategorys.SingleOrDefault(a=>a.CustomerSubCategoryID== customerSubCategory.CustomerSubCategoryID);
          
            return Json(obj);
        }

    }
}
