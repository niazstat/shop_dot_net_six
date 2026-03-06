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
    public class SalesReturnController : Controller
    {

        private ICustomerCategory customerCategoryRepo;
        private ICustomerSubCategory customerSubCategoryRepo;
        private ICustomerRepository customerRepository;
        private IUserService userService;
        private IPaymentMediumRepository paymentMediumRepository;
        private IProductsRepositor productsRepositor;
        private ICompanyProductRepository companyProductRepository;
        private ISalesRepository salesRepository;
        private ISalesReturnRepository salesReturnRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private IDayCloseRepository dayCloseEntry;
        public SalesReturnController(ICustomerCategory _customerCategoryRepo, ICustomerSubCategory _customerSubCategoryRepo, ICustomerRepository _customerRepository, IUserService _userService, IHttpContextAccessor _httpContextAccessor, IPaymentMediumRepository _paymentMediumRepository, IProductsRepositor _productsRepositor, ISalesRepository _salesRepository, ICompanyProductRepository _companyProductRepository, ISalesReturnRepository _salesReturnRepository, IDayCloseRepository _dayCloseEntry)
        {
            customerCategoryRepo = _customerCategoryRepo;
            customerSubCategoryRepo = _customerSubCategoryRepo;
            customerRepository = _customerRepository;
            userService = _userService;
            httpContextAccessor = _httpContextAccessor;
            paymentMediumRepository = _paymentMediumRepository;
            productsRepositor = _productsRepositor;
            salesRepository = _salesRepository;
            companyProductRepository = _companyProductRepository;
            salesReturnRepository = _salesReturnRepository;
            dayCloseEntry = _dayCloseEntry;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SalesReturnEntry()
        {
            return View();
        }

        public IActionResult SalesReturnReport()
        {
            return View();
        }


        public JsonResult SaveSalesReturn([FromBody] SalesReturn salesReturn)
         {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            if ((DateTime.Today - salesReturn.ReturnDate).TotalDays > 5 && !salesReturn.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == salesReturn.ReturnDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}
            Company comp = userService.GetCompanyByUser(1);




            salesReturn.Company = comp;
            salesReturn.UserId = user.UserId;
            salesReturn.AutoReturnNo = 0;

         

            bool isCustmerChecked = false;

            foreach (var item in salesReturn.SalesReturnDetailsList)
            {

                SalesDetails salesDet = salesRepository.SalesDetailsAsNoTracking.SingleOrDefault(a => a.SalesDetailsID == item.SalesDetailsID);
                CompanyProduct comProd = companyProductRepository.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == salesDet.CompanyProductID);
                if (!isCustmerChecked)
                {

                }

                item.CompanyProduct = comProd;
                item.Article = comProd.Article;
                item.Size = comProd.Size;
                item.ProdName = comProd.ProdName;
                item.CompanyProductID = salesDet.CompanyProductID;
                item.Company = comp;
               // item.RetRate = salesDet.SalesRate;
               // item.ReturnCommissionRate = salesDet.CommissionRate;
                item.SalesRate = salesDet.SalesRate;
                item.SalesCommissionRate = salesDet.CommissionRate;
               // item.UOM = salesDet.UOM;
                item.UOMID = salesDet.UOM.UOMID;
                item.UOM = null;
                // _totamAmount += item.SalesAmount;
                // _totamNetReturnAmount += item.ReyurnQtyInPair * (salesDet.SalesRate - salesDet.CommissionRate);

            }


            ResultObj obj = salesReturnRepository.SaveSalesReturn(salesReturn);
            //obj.Obj = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }
        public JsonResult DeleteSalesReturnLine([FromBody] SalesReturnDetails salesReturnDet)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            SalesReturnDetails entry = salesReturnRepository.SalesReturnDetailsAsNoTracking.SingleOrDefault(a => a.SalesReturnDetailsID == salesReturnDet.SalesReturnDetailsID && salesReturnDet.SalesDetailsID == salesReturnDet.SalesDetailsID);



            if (entry == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Data " });
            }

            SalesReturn retMAin = salesReturnRepository.SalesReturns.SingleOrDefault(a => a.SalesReturnID == entry.SalesReturnID);

            if ((DateTime.Today - retMAin.ReturnDate).TotalDays > 5 && !retMAin.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == retMAin.ReturnDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}
            ResultObj obj = salesReturnRepository.DeleteSalesReturnDetails(retMAin,entry);
        
            return Json(obj);
        }

        public JsonResult UpdateSalesReturnDetails([FromBody] SalesReturnDetails salesReturnDet)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            SalesReturnDetails entry = salesReturnRepository.SalesReturnDetailsAsNoTracking.SingleOrDefault(a => a.SalesReturnDetailsID == salesReturnDet.SalesReturnDetailsID && salesReturnDet.SalesDetailsID == salesReturnDet.SalesDetailsID);
            SalesReturn retMAin = salesReturnRepository.SalesReturns.SingleOrDefault(a => a.SalesReturnID == entry.SalesReturnID);

            if ((DateTime.Today - retMAin.ReturnDate).TotalDays > 5 && !retMAin.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == retMAin.ReturnDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}

            decimal _prevQty = entry.ReyurnQtyInPair;
            if (entry == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Data " });
            }

            entry.RetRate = salesReturnDet.RetRate;
            entry.ReturnCommissionRate = salesReturnDet.ReturnCommissionRate;

            entry.ReyurnQtyInPair = salesReturnDet.ReyurnQtyInPair;
            ResultObj obj = salesReturnRepository.UpdateSalesReturnDetails(retMAin,entry, _prevQty);

            return Json(obj);
        }

        public JsonResult InsertSalesReturnDetails([FromBody] SalesReturnDetails salesReturnDet)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            SalesReturnDetails entry = salesReturnRepository.SalesReturnDetailsAsNoTracking.SingleOrDefault(a => a.SalesReturnDetailsID == salesReturnDet.SalesReturnDetailsID && salesReturnDet.SalesDetailsID == salesReturnDet.SalesDetailsID);
           
            if (entry == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid/Mismatch Data " });
            }
            SalesReturn retMAin = salesReturnRepository.SalesReturns.SingleOrDefault(a => a.SalesReturnID == salesReturnDet.SalesReturnID);

            if ((DateTime.Today - retMAin.ReturnDate).TotalDays > 5 && !retMAin.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == retMAin.ReturnDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}
            Company comp = userService.GetCompanyByUser(1);

            SalesDetails salesDet = salesRepository.SalesDetailsAsNoTracking.SingleOrDefault(a => a.SalesDetailsID == salesReturnDet.SalesDetailsID);
            CompanyProduct comProd = companyProductRepository.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == salesDet.CompanyProductID);


            salesReturnDet.CompanyProduct = comProd;
            salesReturnDet.Article = comProd.Article;
            salesReturnDet.Size = comProd.Size;
            salesReturnDet.ProdName = comProd.ProdName;
            salesReturnDet.CompanyProductID = salesDet.CompanyProductID;
            salesReturnDet.Company = comp;
           // salesReturnDet.RetRate = salesDet.SalesRate;
           // salesReturnDet.ReturnCommissionRate = salesDet.CommissionRate;
            salesReturnDet.SalesRate = salesDet.SalesRate;
            salesReturnDet.SalesCommissionRate = salesDet.CommissionRate;


            ResultObj obj = salesReturnRepository.InsertSalesReturnDetails(retMAin,salesReturnDet);

            return Json(obj);
        }


        public JsonResult GetSalesRetirnBySalesNo([FromBody] SalesReturn salesHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(1);

            SalesReturn _head = salesReturnRepository.SalesReturnFull.SingleOrDefault(a => a.GeneratedReturnNo == salesHead.GeneratedReturnNo);

            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }


            ResultObj obj = new ResultObj();
            obj.Obj = _head;
            obj.ResultID = 1;
            return Json(obj);
        }


        public JsonResult GetSalesReturnBySalesNoEdit([FromBody] SalesReturn salesHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(1);

            //SalesDetailsWithreturnForEdit

            SalesReturn _head2 = salesReturnRepository.SalesReturnAsnoTracking.SingleOrDefault(a => a.GeneratedReturnNo == salesHead.GeneratedReturnNo);

            SalesHead _head = salesRepository.SalesDetailsWithreturnForEdit(_head2.ReturnNo.Substring(4), _head2.SalesReturnID);


            //SalesHead _head = salesRepository.SalesDetailsWithreturn.SingleOrDefault(a=>a.GeneratedSalesNo2==_head2.ReturnNo);

            SalesHead _head3 = _head;
            foreach (SalesDetails item in _head.SalesDetailsList)
            {
                item.SalesReturnDetails = item.SalesReturnDetails.Where(a => a.SalesReturnID == _head2.SalesReturnID).ToList();
            }


            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }


            ResultObj obj = new ResultObj();
            obj.Obj = _head3;
            obj.ResultID = 1;
            return Json(obj);
        }



        public IActionResult SalesReturnReportView(string fromDate, string toDate, string customerIDs, string IsDetails, string type)
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Sales Report ";


            return View();
        }

        public JsonResult GetSalesReturnReport([FromBody] SalesReportViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }
            List<SalesReturn> list = new List<SalesReturn>();
            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Details Sales Return Report, ";
            if (model.IsDetails == "0")
            {
                title = "Summary Sales Return Report, ";
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
            if (model.CustomerIds != "0")
            {
                List<int> custIDS = model.CustomerIds.Split(",").Select(a => int.Parse(a)).ToList();

                if (model.IsDetails != "0")
                {
                    list = salesReturnRepository.SalesReturnFull.Where(a => a.ReturnDate >= _fromDate && a.ReturnDate <= _toDate && custIDS.Contains(a.Customer.CustomerID)).ToList();
                }

                else
                {
                    list = salesReturnRepository.SalesReturnFull.Where(a => a.ReturnDate >= _fromDate && a.ReturnDate <= _toDate && custIDS.Contains(a.Customer.CustomerID)).ToList();
                }
            }

            else
            {
                if (model.IsDetails != "0")
                {
                    list = salesReturnRepository.SalesReturnFull.Where(a => a.ReturnDate >= _fromDate && a.ReturnDate <= _toDate).ToList();
                }
                else
                {
                    list = salesReturnRepository.SalesReturnFull.Where(a => a.ReturnDate >= _fromDate && a.ReturnDate <= _toDate).ToList();
                }
            }
            obj.ResultNo = title;
            obj.Obj = list;
            return Json(obj);
        }
    }
}
