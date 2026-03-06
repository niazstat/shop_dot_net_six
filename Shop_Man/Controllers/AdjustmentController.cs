using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Man.Models;
using Shop_Man.Models.ViewModels;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Controllers
{
    public class AdjustmentController : Controller
    {

        private ISupplierCategory supplierCategory;
        private ICustomerSubCategory customerSubCategory;
        private ICustomerRepository customerRepository;
        private ISupplierSubCategory supplierSubCategory;
        private ISupplierRepository supplierRepository;
        private IUserService userService;
 

        private IAdjustmentRepository adjustmentRepository;
 
        private readonly IHttpContextAccessor httpContextAccessor;
        public AdjustmentController(ISupplierCategory _supplierCategory, ISupplierSubCategory _supplierSubCategory, ISupplierRepository _supplierRepository, IUserService _userService, IHttpContextAccessor _httpContextAccessor, IAdjustmentRepository _adjustmentRepository, ICustomerRepository _customerRepository, ICustomerSubCategory _customerSubCategory)
        {
            supplierCategory = _supplierCategory;
            supplierSubCategory = _supplierSubCategory;
            supplierRepository = _supplierRepository;
            userService = _userService;
            httpContextAccessor = _httpContextAccessor;

            adjustmentRepository = _adjustmentRepository;
            customerRepository = _customerRepository;
            customerSubCategory = _customerSubCategory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdjustEntry()
        {
            return View();
        }


        public JsonResult GetAdjustmentEntryViewModel()
        {
            AdjustmentEntryViewModel obj = new AdjustmentEntryViewModel();
            obj.Customers = customerRepository.Customers.ToList();
            obj.Suppliers = supplierRepository.Suppliers.ToList();
            obj.CustomerSubCategorys = customerSubCategory.CustomerSubCategorys.ToList();
            obj.SupplierSubCategorys = supplierSubCategory.SupplierSubCategorys.ToList();
          
            obj.Adjustments = adjustmentRepository.Adjustments.OrderByDescending(a => a.AdjustmentID).ToList();
            return Json(obj);
        }

        public JsonResult SaveAdjustment([FromBody] Adjustment adjustment)
        {
            Adjustment entry;
         
            if (adjustment.AdjustmentID == 0)
            {
                entry = new Adjustment();
            }

            else
            {



                entry = adjustmentRepository.Adjustments.SingleOrDefault(a => a.AdjustmentID == adjustment.AdjustmentID);


                if ((DateTime.Today - entry.PaymentDate).TotalDays > 5 && !entry.IsAllowEdit)
                {
                    return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
                }
            }

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });

            }

            entry.User = user;
            Company comp = userService.GetCompanyByUser(1);
            entry.Company = comp;

           
          
            Supplier supp = supplierRepository.Suppliers.SingleOrDefault(a => a.SupplierId == adjustment.Supplier.SupplierId);
            Customer cust = customerRepository.Customers.SingleOrDefault(a => a.CustomerID == adjustment.Customer.CustomerID);
            if (supp == null && cust == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Supplier/Customer" });
            }
            if (supp != null && cust != null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Mismatch Supplier/Customer" });
            }
            entry.Supplier = supp;
            entry.Customer = cust;
            entry.Amount = adjustment.Amount;
            entry.ClosingShortAmount = adjustment.ClosingShortAmount;
            entry.RejectGoodsAmount = adjustment.RejectGoodsAmount;
            entry.Note = adjustment.Note;
            entry.PaymentDate = adjustment.PaymentDate;
            ResultObj obj = adjustmentRepository.SaveAdjustment(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = adjustmentRepository.Adjustments.OrderByDescending(a => a.AdjustmentID).ToList();
            }
            return Json(obj);
        }

        public JsonResult GetAdjustment([FromBody] Adjustment adjustment)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            Adjustment model = adjustmentRepository.Adjustments.SingleOrDefault(a => a.AdjustmentID == adjustment.AdjustmentID);
            obj.Obj = model;
            return Json(obj);
        }

        public JsonResult DeleteAdjustment([FromBody] Adjustment adjustment)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            Company comm = userService.GetCompanyByUser(user.UserId);
            Adjustment entry = adjustmentRepository.AdjustmentsAsNoTracking.SingleOrDefault(a => a.AdjustmentID == adjustment.AdjustmentID);


            if ((DateTime.Today - entry.PaymentDate).TotalDays > 5 && !entry.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }

            ResultObj obj = adjustmentRepository.DeleteAdjustment(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = adjustmentRepository.Adjustments.OrderByDescending(a => a.AdjustmentID).ToList();

            }
            return Json(obj);
        }

        public IActionResult Reports()
        {
            return View();
        }


        public IActionResult AdjustReport(string fromDate, string toDate, string customerIDs, string IsDetails, string type,string _custType)
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Adjust Report ";


            return View();
        }

        public JsonResult GetAdjustReport([FromBody] SalesReportViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }
      
            List<Adjustment> list = new List<Adjustment>();
            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Adjust  Report, ";


            if (model.Type.ToUpper() == "ALL")
            {
                title += "Adjust  Report, ";
            }



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

            else
            {

                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Datewise)"; ;

            }
            //List<string> custIDS;
            //            List<Order> AllOrderInBills = repositoryOrder.OrdersWithReceiveMain.Where(a => orderIDsInBills.Contains(a.OrderID)).ToList();

            if (model.ArticleNames.ToUpper() == "ALL")
            {
                list = adjustmentRepository.Adjustments.Where(a => a.PaymentDate >= _fromDate && a.PaymentDate <= _toDate ).ToList();
            }
            else if (model.ArticleNames.ToUpper() == "CUST")
            {

                List<int> custIDS = model.CustomerIds.Split(",").Select(a => int.Parse(a)).ToList();

                if (model.IsDetails != "0")
                {
                    list = adjustmentRepository.Adjustments.Where(a => a.PaymentDate >= _fromDate && a.PaymentDate <= _toDate && custIDS.Contains(a.Customer.CustomerID)).ToList();
                }

                else
                {
                    list = adjustmentRepository.Adjustments.Where(a => a.PaymentDate >= _fromDate && a.PaymentDate <= _toDate && a.Supplier==null).ToList();
                }



            }
            else if (model.ArticleNames.ToUpper() == "SUPP")
            {

                List<int> custIDS = model.CustomerIds.Split(",").Select(a => int.Parse(a)).ToList();

                if (model.IsDetails != "0")
                {
                    list = adjustmentRepository.Adjustments.Where(a => a.PaymentDate >= _fromDate && a.PaymentDate <= _toDate && custIDS.Contains(a.Supplier.SupplierId)).ToList();
                }

                else
                {
                    list = adjustmentRepository.Adjustments.Where(a => a.PaymentDate >= _fromDate && a.PaymentDate <= _toDate && (a.Customer == null)).ToList();
                }



            }


            obj.ResultNo = title;
            obj.Obj = list;
            return Json(obj);
        }
    }
}
