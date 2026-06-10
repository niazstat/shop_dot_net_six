using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Models.ViewModels;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Controllers
{
    public class SalesController : Controller
    {
        private ICustomerCategory customerCategoryRepo;
        private ICustomerSubCategory customerSubCategoryRepo;
        private ICustomerRepository customerRepository;
        private IUserService userService;
        private IPaymentMediumRepository paymentMediumRepository;
        private IProductsRepositor productsRepositor;
        private ICompanyProductRepository companyProductRepository;
        private ISalesRepository salesRepository;
        private IDayCloseRepository dayCloseEntry;

        private readonly IHttpContextAccessor httpContextAccessor;
        public SalesController(ICustomerCategory _customerCategoryRepo, ICustomerSubCategory _customerSubCategoryRepo, ICustomerRepository _customerRepository, IUserService _userService, IHttpContextAccessor _httpContextAccessor, IPaymentMediumRepository _paymentMediumRepository, IProductsRepositor _productsRepositor, ISalesRepository _salesRepository, ICompanyProductRepository _companyProductRepository, IDayCloseRepository _dayCloseEntry)
        {
            customerCategoryRepo = _customerCategoryRepo;
            customerSubCategoryRepo = _customerSubCategoryRepo;
            customerRepository = _customerRepository;
            userService = _userService;
            httpContextAccessor = _httpContextAccessor;
            paymentMediumRepository = _paymentMediumRepository;
            productsRepositor = _productsRepositor;
            salesRepository=_salesRepository;
            companyProductRepository = _companyProductRepository;
            dayCloseEntry=_dayCloseEntry;

    }
        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult SalesEntry()
        {
            return View();
        }

        public JsonResult GetSalesEntryViewModel()
        {
            SalesEntryViewModel obj = new SalesEntryViewModel();
            obj.CustomerCategorys = customerCategoryRepo.CustomerCategorys.ToList();
            obj.CustomerSubCategorys = customerSubCategoryRepo.CustomerSubCategorys.ToList();
            //obj.Customers = customerRepository.Customers.ToList();

            obj.CustomerModels =  customerRepository.CustomersQuerable
      .Select(c => new CustomerModel
      {
          CustomerID = c.CustomerID,
          CustomerNo = c.CustomerNo,
           Name = c.Name,
            Address1= c.Address1,
          ShopName = c.ShopName,
          SubCategoryID = c.CustomerSubCategoryID,
          SUbCategoryName = c.CustomerSubCategory.CustomerSubCategoryName,
          CategoryID = c.CustomerSubCategory.CustomerCategoryID,
          CategoryName = c.CustomerSubCategory.CustomerCategory.CustomerCategoryName
      })
      .ToList();
            obj.PaymentMediums = paymentMediumRepository.PaymentMediums.ToList();
            obj.ProdNames = productsRepositor.ProdNames.ToList();
            return Json(obj);
        }


        public JsonResult SaveSales([FromBody] SalesHead salesHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            if (salesHead.SalesDate > DateTime.Today)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Date!" });
            }

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == salesHead.SalesDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update Not Possible !!" });
            //}
            int custWiseID = 0;
            Company comp= userService.GetCompanyByUser(user.UserId);
            Customer _custMaxLimit = null;
            decimal custPrevBalance = 0;
            if (salesHead.Customer != null)
            {

                if (salesHead.Customer.CustomerID == 0)
                {
                    custWiseID = salesRepository.SalesHeadsDetailsAsnoTracking.Where(a => a.Customer == null).Select(b => (int?)b.CustwiseSalesNo).Max()??0;
                }
                else
                {
                     _custMaxLimit = customerRepository.GetCustomer(salesHead.Customer.CustomerID);
                    custWiseID = salesRepository.SalesHeadsDetailsAsnoTracking.Where(a => a.Customer.CustomerID == salesHead.Customer.CustomerID).Select(b => (int?)b.CustwiseSalesNo).Max() ?? 0;

                    custPrevBalance= customerRepository.GetCustomerPreviousBalance(salesHead.Customer.CustomerID);
               
                }
            }
            else
            {
                custWiseID = salesRepository.SalesHeadsDetailsAsnoTracking.Where(a => a.Customer == null).Select(b => (int?)b.CustwiseSalesNo).Max() ?? 0;


            }



            salesHead.Company = comp;
            salesHead.UserId = user.UserId;
            salesHead.AutoSalesNo = 0;
            salesHead.CustwiseSalesNo = custWiseID+1;
            salesHead.PreviousBalance = custPrevBalance;
            if (salesHead.IsCashSales)
            {
                salesHead.Customer = null;
            }
            decimal _totamAmount = 0;

            decimal _totalCommision = 0;

            foreach (var item in salesHead.SalesDetailsList)
            {
                CompanyProduct comProd = companyProductRepository.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == item.CompanyProductID);

                item.CompanyProduct = comProd;
                item.Article = comProd.Article;
                item.ArticleName = comProd.Article.Name;
                item.CurrentStockQty = comProd.CurrentStock;
                item.ProdName = comProd.ProdName;
                item.ProductName = comProd.ProdName.Name;
                item.Size = comProd.Size;
                item.SizeName = comProd.Size.Name;
                item.UOM = comProd.UOM;
                item.UOMName = comProd.UOM.Name;
                item.Company = comp;
                item.BuyRate = comProd.BuyPrice;
                item.BuyCommRate = comProd.BuyComm;
                _totamAmount += item.SalesAmount;

                _totalCommision += item.CommissionAmount;
            }
            salesHead.TotalAmount = _totamAmount;
            salesHead.TotalCommission = _totalCommision;


            if (salesHead.Customer != null)
            {

                if (salesHead.Customer.CustomerID != 0)
                {
                    if (salesHead.ReceiveAmount < _totamAmount - _totalCommision + salesHead.AddLessAmount)
                    {

                        decimal neatBalance = _totamAmount - _totalCommision - salesHead.ReceiveAmount + custPrevBalance + salesHead.AddLessAmount;

                        if (neatBalance > _custMaxLimit.MaxBalanceLimit)
                        {
                            ResultObj res = new ResultObj { ResultID = -1, ResultMessage = $"Caostomer Maximum Balance Limit Exceeds ! Max Limit {_custMaxLimit.MaxBalanceLimit.ToString()} ,Current Balance : {neatBalance.ToString()}" };
                            return Json(res);
                        }
                    }
                }
            }

         


            ResultObj obj = salesRepository.SaveSales(salesHead);
            //obj.Obj = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }


        public JsonResult UpdateSales([FromBody] SalesHead salesHead)
        {


            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == salesHead.SalesDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update Not Possible !!" });
            //}

            Company comp = userService.GetCompanyByUser(1);

            SalesHead _head = salesRepository.SalesHeadsDetails.SingleOrDefault(a=>a.SalesHeadID== salesHead.SalesHeadID);

            if (_head.SalesDate > DateTime.Today)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Date!" });
            }


            if (( DateTime.Today-_head.SalesDate).TotalDays > 5 && !_head.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }

            decimal prevHeadBalance = _head.TransportCost - _head.ReceiveAmount;
            int prevCusomerID = _head.Customer == null ? 0 : _head.Customer.CustomerID;


            int custWiseID = _head.CustwiseSalesNo;
            decimal custPrevBalance = 0;
            if (prevCusomerID!= salesHead.Customer.CustomerID)
            {
                if (salesHead.Customer.CustomerID == 0)
                {
                    custWiseID = salesRepository.SalesHeadsDetailsAsnoTracking.Where(a => a.Customer == null).Select(b => (int?)b.CustwiseSalesNo).Max() ?? 0;
                }
                else
                {
                    custWiseID = salesRepository.SalesHeadsDetailsAsnoTracking.Where(a => a.Customer.CustomerID == salesHead.Customer.CustomerID).Select(b => (int?)b.CustwiseSalesNo).Max() ?? 0;

                    custPrevBalance = customerRepository.GetCustomerPreviousBalance(salesHead.Customer.CustomerID);

                }
                _head.CustwiseSalesNo = custWiseID+1;
                _head.PreviousBalance = custPrevBalance;
            }

          




            _head.TransportCost = salesHead.TransportCost;
            _head.SalesDate = salesHead.SalesDate;

            _head.PaymentMediumID = salesHead.PaymentMediumID;
            _head.AccNo = salesHead.AccNo;
            _head.CheckNo = salesHead.CheckNo;
            _head.CheckPassDate = salesHead.CheckPassDate;
            _head.Note1 = salesHead.Note1;
            _head.ReceiveAmount = salesHead.ReceiveAmount;

            _head.IsCashSales = salesHead.IsCashSales;
            _head.TotalSackNo = salesHead.TotalSackNo;
            _head.TotalSackNoFee = salesHead.TotalSackNoFee;
            _head.AddLessAmount = salesHead.AddLessAmount;

            _head.LastUpdateTime = DateTime.Now;
            _head.UpdateUserID = user.UserId;
            if (salesHead.IsCashSales)
            {
                _head.Customer = null;
            }
            else
            {
                Customer cust = customerRepository.Customers.SingleOrDefault(a=>a.CustomerID == salesHead.Customer.CustomerID);
                _head.Customer = cust;
            }
            decimal _totamAmount = 0;

            decimal _totalCommision = 0;

            foreach (var item in _head.SalesDetailsList)
            {
               // CompanyProduct comProd = companyProductRepository.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == item.CompanyProductID);

               // item.CompanyProduct = comProd;
               // item.Article = comProd.Article;
               // item.ArticleName = comProd.Article.Name;
               // item.CurrentStockQty = comProd.CurrentStock;
               // item.ProdName = comProd.ProdName;
               // item.ProductName = comProd.ProdName.Name;
               // item.Size = comProd.Size;
               // item.SizeName = comProd.Size.Name;
               // item.UOM = comProd.UOM;
                //item.UOMName = comProd.UOM.Name;
                //item.Company = comp;
              //  item.BuyRate = comProd.BuyPrice;
                _totamAmount += item.SalesAmount;

                _totalCommision += item.CommissionAmount;
            }
            _head.TotalAmount = _totamAmount;
            _head.TotalCommission = _totalCommision;

            ResultObj obj = salesRepository.UpdateSales(_head, prevCusomerID,  prevHeadBalance);
            //obj.Obj = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }


        public JsonResult InsertSalesDetails([FromBody] SalesDetails salesDetails)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if(user == null)
            {
             
                return Json(new ResultObj { ResultID=-1, ResultMessage = "Sesion Expired ,Please login Again" });
            }


            Company comp = userService.GetCompanyByUser(user.UserId);

            SalesHead _head = salesRepository.SalesHeadsDetailsAsnoTracking.SingleOrDefault(a => a.SalesHeadID == salesDetails.SalesHeadID);

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == _head.SalesDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update Not Possible !!" });
            //}

            if ((DateTime.Today - _head.SalesDate).TotalDays > 5 && !_head.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days Over ." });
            }
            int prevCusomerID = _head.Customer == null ? 0 : _head.Customer.CustomerID;


            SalesDetails prebSalesDetails = salesRepository.FindSalesDetails(salesDetails.SalesDetailsID);
            int _prevComProductID = prebSalesDetails == null ? 0: prebSalesDetails.CompanyProductID;

            decimal _prevSalesQty = prebSalesDetails == null ? 0 : prebSalesDetails.SalesQtyInPair;
            decimal _prevAMount = prebSalesDetails == null ? 0 :( prebSalesDetails.SalesAmount-prebSalesDetails.CommissionAmount);


            CompanyProduct comProd = companyProductRepository.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == salesDetails.CompanyProductID);

            salesDetails.Company = comp;
            
            salesDetails.CompanyProduct = comProd;
            salesDetails.Article = comProd.Article;
            salesDetails.ArticleName = comProd.Article.Name;
            salesDetails.CurrentStockQty = comProd.CurrentStock;
            salesDetails.ProdName = comProd.ProdName;
            salesDetails.ProductName = comProd.ProdName.Name;
            salesDetails.Size = comProd.Size;
            salesDetails.SizeName = comProd.Size.Name;
            salesDetails.UOM = comProd.UOM;
            salesDetails.UOMName = comProd.UOM.Name;
            salesDetails.BuyRate = comProd.BuyPrice;
            salesDetails.BuyCommRate = comProd.BuyComm;

            obj = salesRepository.InsertSalesDetails(_head,salesDetails, _prevComProductID,_prevSalesQty, _prevAMount, prevCusomerID);
            obj.Obj = salesRepository.SalesHeadsDetails.SingleOrDefault(a=>a.SalesHeadID== salesDetails.SalesHeadID);
            return Json(obj);
        }
        public JsonResult DeleteSalesDetails([FromBody] SalesDetails salesDetails)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            SalesHead _head = salesRepository.SalesHeadsDetailsAsnoTracking.SingleOrDefault(a => a.SalesHeadID == salesDetails.SalesHeadID);
            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == _head.SalesDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update Not Possible !!" });
            //}

            if ((DateTime.Today - _head.SalesDate).TotalDays > 5 && !_head.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Day Over ." });
            }
            int prevCusomerID = _head.Customer == null ? 0 : _head.Customer.CustomerID;


            ResultObj obj = salesRepository.DeleteSalesDetails(_head,salesDetails, prevCusomerID);
            obj.Obj = salesRepository.SalesHeadsDetails.SingleOrDefault(a => a.SalesHeadID == salesDetails.SalesHeadID);
            return Json(obj);
        }
     


        public JsonResult GetSalesBySalesNo([FromBody] SalesHead salesHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(1);

            SalesHead _head = salesRepository.SalesHeadsDetails.SingleOrDefault(a=>a.GeneratedSalesNo==salesHead.GeneratedSalesNo);

            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }


            ResultObj obj = new ResultObj();
            obj.Obj = _head;
            obj.ResultID = 1;
            return Json(obj);
        }
        public JsonResult GetSalesBySalesNoWithReturn([FromBody] SalesHead salesHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(1);

            SalesHead _head = salesRepository.SalesDetailsWithreturn.SingleOrDefault(a => a.GeneratedSalesNo == salesHead.GeneratedSalesNo);

            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }


            ResultObj obj = new ResultObj();
            obj.Obj = _head;
            obj.ResultID = 1;
            return Json(obj);
        }

        public JsonResult GetSalesBySalesNoNext([FromBody] SalesHead salesHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(user.UserId);

            SalesHead _head = salesRepository.SalesHeads.SingleOrDefault(a => a.GeneratedSalesNo == salesHead.GeneratedSalesNo);


          

            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }


            SalesHead _head2 = salesRepository.SalesHeadsDetails.Where(a => a.SalesHeadID > _head.SalesHeadID).OrderBy(a=>a.SalesHeadID).FirstOrDefault();
           
            if (_head2 == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }

            ResultObj obj = new ResultObj();
            obj.Obj = _head2;
            obj.ResultID = 1;
            return Json(obj);
        }


        public JsonResult GetSalesBySalesNoPrev([FromBody] SalesHead salesHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(user.UserId);

            SalesHead _head = salesRepository.SalesHeads.SingleOrDefault(a => a.GeneratedSalesNo == salesHead.GeneratedSalesNo);




            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }


            SalesHead _head2 = salesRepository.SalesHeadsDetails.Where(a => a.SalesHeadID < _head.SalesHeadID).OrderByDescending(a => a.SalesHeadID).FirstOrDefault();
            if (_head2 == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }

            ResultObj obj = new ResultObj();
            obj.Obj = _head2;
            obj.ResultID = 1;
            return Json(obj);
        }

        /////////Sale Report --------------------------------------
        ///

        public IActionResult Reports()
        {
            return View();
        }
        public JsonResult GetSalesEntryViewModelReport()
        {
            SalesEntryViewModel obj = new SalesEntryViewModel();
         
            obj.Customers = customerRepository.Customers.ToList();
          
            obj.Articles = productsRepositor.Articles.ToList();
            return Json(obj);
        }


        public IActionResult SalesReport(string fromDate, string toDate,string customerIDs,string IsDetails,string type)
        {
            Company comp = userService.GetCompanyByUser(1);


    
            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Sales Report ";


            return View();
        }

        public JsonResult GetSalesReport([FromBody] SalesReportViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }
            List<SalesHead> list = new List<SalesHead>();
             DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Details Sales Report, ";
            if (model.IsDetails == "0")
            {
                 title = "Summary Sales Report, ";
            }


                if (model.Type == "day")
            {
                _toDate = DateTime.Today;
                _fromDate = DateTime.Today;

                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) +" (Today)";
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
               List<int> custIDS = model.CustomerIds.Split(",").Select(a=>int.Parse(a)).ToList();

                if (model.IsDetails != "0")
                {
                    list = salesRepository.SalesHeadsDetails.Where(a => a.SalesDate >= _fromDate && a.SalesDate <= _toDate && custIDS.Contains(a.Customer.CustomerID)).ToList();
                }

                else
                {
                    list = salesRepository.SalesHeads.Where(a => a.SalesDate >= _fromDate && a.SalesDate <= _toDate && custIDS.Contains(a.Customer.CustomerID)).ToList();
                }
            }

            else
            {
                if (model.IsDetails != "0")
                {
                    list = salesRepository.SalesHeadsDetails.Where(a => a.SalesDate >= _fromDate && a.SalesDate <= _toDate).ToList();
                }
                else
                {
                    list = salesRepository.SalesHeads.Where(a => a.SalesDate >= _fromDate && a.SalesDate <= _toDate).ToList();
                }
            }
            obj.ResultNo = title;
            obj.Obj = list;
            return Json(obj);
        }
    }
}
