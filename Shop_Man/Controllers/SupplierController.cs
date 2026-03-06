using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Man.Infrastructure;
using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using Shop_Man.Models.ViewModels;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Controllers
{
    public class SupplierController : Controller
    {

        private ISupplierCategory supplierCategoryRepo;
        private ISupplierSubCategory supplierSubCategoryRepo;
        private ISupplierRepository supplierRepository;
        private IUserService userService;
     
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SupplierController(ISupplierCategory _supplierCategoryRepo, ISupplierSubCategory _supplierSubCategoryRepo, ISupplierRepository _supplierRepository, IUserService _userService, IHttpContextAccessor httpContextAccessor)
        {
            supplierCategoryRepo = _supplierCategoryRepo;
            supplierSubCategoryRepo = _supplierSubCategoryRepo;

            supplierRepository = _supplierRepository;
            userService = _userService;
            _httpContextAccessor = httpContextAccessor;
     
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SupplierLock()
        {
            return View();
        }
        public IActionResult SupplierEntry()
        {
            return View();
        }

        public JsonResult AddSuppllier([FromBody] Supplier supplier)
        {
            ResultObj obj =new ResultObj();

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];

             if(!userService.IsControllerAndActionPermitted("Supplier", "SupplierEntry",user))
            {
                obj.ResultID = -1;
                obj.ResultMessage = "Un Auothorized User !";

                return Json(obj);
            }



            supplier.Company = userService.GetCompanyByUser(1);
            supplier.UserId = user.UserId;
            supplier.IsLocked = true;
             obj = supplierRepository.SaveSupplier(supplier);
            obj.Obj = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }


        public JsonResult DeleteSupplier([FromBody] Supplier supplier)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];


            if (!userService.IsControllerAndActionPermitted("Supplier", "SupplierEntry", user))
            {
                obj.ResultID = -1;
                obj.ResultMessage = "Un Auothorized User !";

                return Json(obj);
            }
            supplier.Company = userService.GetCompanyByUser(1);
            supplier.UserId = user.UserId;
              obj = supplierRepository.DeleteSupplier(supplier);
            obj.Obj = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }

        public JsonResult GetSupplier([FromBody] Supplier supplier)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (!userService.IsControllerAndActionPermitted("Supplier", "SupplierEntry", user))
            {
                obj.ResultID = -1;
                obj.ResultMessage = "Un Auothorized User !";

                return Json(obj);
            }
  
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            Supplier supp = supplierRepository.GetSupplier(supplier.SupplierId);

            return Json(supp);
        }

        public JsonResult GetSuppliers()
        {

          

            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (!userService.IsControllerAndActionPermitted("Supplier", "SupplierEntry", user))
            {
                obj.ResultID = -1;
                obj.ResultMessage = "Un Auothorized User !";

                return Json(obj);
            }

            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            List<  Supplier> supps = supplierRepository.Suppliers.ToList();

            return Json(supps);
        }
        [Authorize]
        public JsonResult SupplierEntryViewModel()
        {

            SupplierEntryViewModel model = new SupplierEntryViewModel();

            model.Suppliers = supplierRepository.Suppliers.ToList();
            model.SupplierCategorys = supplierCategoryRepo.SupplierCategorys.ToList();
            model.SupplierSubCategorys = supplierSubCategoryRepo.SupplierSubCategorys.ToList();
      
            return Json(model);
        }


        [Authorize]
        public ActionResult InsertNewSupplierCategory([FromBody] SupplierCategory supplierCategory)
        {
            ResultObj obj = supplierCategoryRepo.SaveSupplierCategory(supplierCategory);
            if (obj.ResultID != -1)
            {
                obj.Obj = supplierCategoryRepo.SupplierCategorys;
            }
            return Json(obj);
        }

        [Authorize]
        public ActionResult InsertNewSubSupplierCategory([FromBody] SupplierSubCategory supplierSubCategory)
        {
            ResultObj obj = supplierSubCategoryRepo.SaveSupplierSubCategory(supplierSubCategory);
            if (obj.ResultID != -1)
            {
                obj.Obj = supplierSubCategoryRepo.SupplierSubCategorys.ToList();
            }
            return Json(obj);
        }

        [Authorize]
        public ActionResult SupplierSubCategorysInCategory([FromBody] SupplierCategory supplierCategory)
        {
            var obj = supplierSubCategoryRepo.SupplierSubCategorysInCategory(supplierCategory.SupplierCategoryID);

            return Json(obj);
        }


        [Authorize]
        public ActionResult DeleteSubSupplierrCategory([FromBody] SupplierSubCategory supplierSubCategory)
        {
            ResultObj obj = supplierSubCategoryRepo.DeleteSupplierSubCategory(supplierSubCategory);
            if (obj.ResultID != -1)
            {
                obj.Obj = supplierSubCategoryRepo.SupplierSubCategorys.ToList();
            }
            return Json(obj);
        }

        [Authorize]
        public ActionResult DeleteSupplierCategory([FromBody] SupplierCategory supplierCategory)
        {
            ResultObj obj = supplierCategoryRepo.DeleteSupplierCategory(supplierCategory);
            if (obj.ResultID != -1)
            {
                obj.Obj = supplierCategoryRepo.SupplierCategorys;
            }
            return Json(obj);
        }


        [Authorize]
        public ActionResult GetSupplierCategory([FromBody] SupplierCategory supplierCategory)
        {
            //  var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            SupplierCategory cust = supplierCategoryRepo.SupplierCategorys.SingleOrDefault(a => a.SupplierCategoryID == supplierCategory.SupplierCategoryID);

            return Json(cust);
        }


        [Authorize]
        public ActionResult GetSubSupplierCategory([FromBody] SupplierSubCategory supplierSubCategory)
        {
            SupplierSubCategory obj = supplierSubCategoryRepo.SupplierSubCategorys.SingleOrDefault(a => a.SupplierSubCategoryID == supplierSubCategory.SupplierSubCategoryID);

            return Json(obj);
        }

        public JsonResult GetSupplierCurrentBalalce([FromBody] Supplier supplier)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;

            decimal suppBal = supplierRepository.GetSupplierCurrentBalance(supplier.SupplierId);
            supplier.CurrentBalance = suppBal;
            return Json(supplier);
        }

        public IActionResult SupplierReport()
        {
            return View();
        }


        public IActionResult SupplierrBalance(string fromDate, string toDate, string type, string customerIDs)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
            if (int.Parse(customerIDs) == 0)
            {
                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "Sales Report ";
                TempData["CustName"] = "";
                TempData["ShopName"] = "";
                TempData["Address"] = "";
            }
            else
            {
                Supplier cust = supplierRepository.GetSupplier(int.Parse(customerIDs));

                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "Sales Report ";
                TempData["CustName"] = cust.Name;
            
                TempData["Address"] = cust.Address1;
            }
            return View();
        }


        public JsonResult GetSupplierBalanceReport([FromBody] SalesReportViewModel model)
        {

            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }
            List<SP_SuppierBalancceDetails> list = new List<SP_SuppierBalancceDetails>();
           List<SP_SupplierBalance_Datewise> list2 = new List<SP_SupplierBalance_Datewise>();
            List<SP_SupplierBalance_ALL> list3 = new List<SP_SupplierBalance_ALL>();
            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Details Supplier Balance Report, ";



            if (model.Type == "day")
            {
                _toDate = DateTime.Today;
                _fromDate = DateTime.Today;

                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Today)";
            }
            else if (model.Type == "week")
            {
                _toDate = DateTime.Today;
                _fromDate = _fromDate.AddDays(-7);
                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Week)";
            }
            else if (model.Type == "month")
            {
                _toDate = DateTime.Today;
                _fromDate = _fromDate.AddMonths(-1);
                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Month)"; ;
            }
            else if (model.Type == "year")
            {
                _toDate = DateTime.Today;
                _fromDate = _fromDate.AddYears(-1);
                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Year)"; ;

            }
            else if (model.Type == "all")
            {

                title += " "; ;

            }
            else
            {

                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Datewise)"; ;

            }
            //List<string> custIDS;
            //            List<Order> AllOrderInBills = repositoryOrder.OrdersWithReceiveMain.Where(a => orderIDsInBills.Contains(a.OrderID)).ToList();


            if (int.Parse(model.CustomerIds) == 0)
            {
                list3 = supplierRepository.SupplierBalanceALL();
                title += " Current Supplier Balance  ";
                obj.Obj = list3;
            }
            else
            {
                if (model.Type == "all")
                {
                    list = supplierRepository.SupplierBalanceDetailsList(int.Parse(model.CustomerIds));
                    obj.Obj = list;
                }
                else
                {
                    list2 = supplierRepository.DatewiseSuppliuerBalanceList(int.Parse(model.CustomerIds), _fromDate, _toDate);
                    obj.Obj = list2;
                }
            }
            obj.ResultNo = title;

            return Json(obj);
        }
    }
}
