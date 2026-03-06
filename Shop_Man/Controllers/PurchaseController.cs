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
    public class PurchaseController : Controller
    {

        private ISupplierCategory supplierCategoryRepo;
        private ISupplierSubCategory supplierSubCategoryRepo;
        private ISupplierRepository supplierRepository;
        private IUserService userService;
      
        private IProductsRepositor productsRepositor;
        private ICompanyProductRepository companyProductRepository;
        private IPurchaseRepository purchaseRepository;
        private IDayCloseRepository dayCloseEntry;

        private readonly IHttpContextAccessor httpContextAccessor;
        public PurchaseController(ISupplierCategory _supplierCategoryRepo, ISupplierSubCategory _supplierSubCategoryRepo, ISupplierRepository _supplierRepository, IUserService _userService, IHttpContextAccessor _httpContextAccessor,  IProductsRepositor _productsRepositor, IPurchaseRepository _purchaseRepository, ICompanyProductRepository _companyProductRepository,         IDayCloseRepository _dayCloseEntry)
        {
            supplierCategoryRepo = _supplierCategoryRepo;
            supplierSubCategoryRepo = _supplierSubCategoryRepo;
            supplierRepository = _supplierRepository;
            userService = _userService;
            httpContextAccessor = _httpContextAccessor;
    
            productsRepositor = _productsRepositor;
            purchaseRepository = _purchaseRepository;
            companyProductRepository = _companyProductRepository;
            dayCloseEntry = _dayCloseEntry;

        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PurchaseEntry()
        {
            return View();
        }

        public JsonResult GetPurchaseEntryViewModel()
        {
            PurchaseEntryViewModel obj = new PurchaseEntryViewModel();
            obj.SupplierCategorys = supplierCategoryRepo.SupplierCategorys.ToList();
            obj.SupplierSubCategorys = supplierSubCategoryRepo.SupplierSubCategorys.ToList();
            obj.Suppliers = supplierRepository.Suppliers.ToList();
      
            obj.ProdNames = productsRepositor.ProdNames.ToList();
            return Json(obj);
        }


        public JsonResult SavePurchase([FromBody] PurchaseHead purchaseHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == purchaseHead.PurchaseDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}
            Company comp = userService.GetCompanyByUser(1);

            purchaseHead.Company = comp;
            purchaseHead.UserId = user.UserId;
            purchaseHead.AutoPurchaseHeadNo = 0;
        
            decimal _totamAmount = 0;

            decimal _totalCommision = 0;

            foreach (var item in purchaseHead.PurchaseDetailsList)
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
              
                _totamAmount += item.PurchaseQtyInPairAmount;

                _totalCommision += item.CommissionAmount;
            }
            purchaseHead.TotalAmount = _totamAmount;
            purchaseHead.TotalCommission = _totalCommision;
            purchaseHead.TotalNetAmount = _totamAmount - _totalCommision;

            ResultObj obj = purchaseRepository.SavePurchase(purchaseHead);
            //obj.Obj = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }


        public JsonResult UpdatePurchase([FromBody] PurchaseHead purchaseHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

           // SalesHead _head = salesRepository.SalesHeadsDetails.SingleOrDefault(a => a.SalesHeadID == salesHead.SalesHeadID);

        
    
            Company comp = userService.GetCompanyByUser(1);

            PurchaseHead _head = purchaseRepository.PurchaseHeads.SingleOrDefault(a => a.PurchaseHeadID == purchaseHead.PurchaseHeadID);

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == _head.PurchaseDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}

            if ((DateTime.Today - _head.PurchaseDate).TotalDays > 5 && !_head.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }

            int prevSuppID =  _head.Supplier.SupplierId;


            _head.TransportCost = 0;
            _head.PurchaseDate = purchaseHead.PurchaseDate;


            _head.Note1 = purchaseHead.Note1;
            _head.SuppChallanNo = purchaseHead.SuppChallanNo;
            Supplier supp = supplierRepository.Suppliers.SingleOrDefault(a => a.SupplierId == purchaseHead.Supplier.SupplierId);
            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == purchaseHead.PurchaseDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}
            _head.Supplier = supp;

            decimal _totamAmount = 0;

            decimal _totalCommision = 0;

            foreach (var item in _head.PurchaseDetailsList)
            {

                _totamAmount += item.PurchaseQtyInPairAmount;

                _totalCommision += item.CommissionAmount;
            }
            _head.TotalAmount = _totamAmount;
            _head.TotalCommission = _totalCommision;
            _head.TotalNetAmount = _totamAmount - _totalCommision;
            ResultObj obj = purchaseRepository.UpdatePurchase(_head, prevSuppID);
            //obj.Obj = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }



        public JsonResult GetPurchaseBySalesNo([FromBody] PurchaseHead purchaseHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(1);

            PurchaseHead _head = purchaseRepository.PurchaseHeadInDetails.SingleOrDefault(a => a.GeneratedPurchaseHeadNo == purchaseHead.GeneratedPurchaseHeadNo);

            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }


            ResultObj obj = new ResultObj();
            obj.ResultID = 1;
            obj.Obj = _head;
            return Json(obj);
        }

        public JsonResult DeletePurchaseDetails([FromBody] PurchaseDetails purchaseDetails)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];

            PurchaseHead _head = purchaseRepository.PurchaseHeads.SingleOrDefault(a => a.PurchaseHeadID == purchaseDetails.PurchaseHeadID);
            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == _head.PurchaseDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}
            if ((DateTime.Today - _head.PurchaseDate).TotalDays > 5 && !_head.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }

            int prevSupplierID =  _head.Supplier.SupplierId;
            ResultObj obj = purchaseRepository.DeletePurchaseDetails(_head,purchaseDetails, prevSupplierID);
            obj.Obj = purchaseRepository.PurchaseHeadInDetails.SingleOrDefault(a => a.PurchaseHeadID == purchaseDetails.PurchaseHeadID);
            return Json(obj);
        }


        public JsonResult InsertPurchaseDetails([FromBody] PurchaseDetails purchaseDetails)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }


            Company comp = userService.GetCompanyByUser(1);

            PurchaseHead _head = purchaseRepository.PurchaseHeadInDetailsAsnotracking.SingleOrDefault(a => a.PurchaseHeadID == purchaseDetails.PurchaseHeadID);

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == _head.PurchaseDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}

            if ((DateTime.Today - _head.PurchaseDate).TotalDays > 5 && !_head.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }
            int prevSupplierID =_head.Supplier.SupplierId;


            PurchaseDetails prevSalesDetails = purchaseRepository.FindPurchaseDetails(purchaseDetails.PurchaseDetailsID);
            int _prevComProductID = prevSalesDetails == null ? 0 : prevSalesDetails.CompanyProductID;

            decimal _prevPurchaseQty = prevSalesDetails == null ? 0 : prevSalesDetails.PurchaseQtyInPair;
            decimal _prevAmount = prevSalesDetails == null ? 0 : (prevSalesDetails.PurchaseQtyInPairAmount - prevSalesDetails.CommissionAmount);


            CompanyProduct comProd = companyProductRepository.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == purchaseDetails.CompanyProductID);

            purchaseDetails.Company = comp;

            purchaseDetails.CompanyProduct = comProd;
            purchaseDetails.Article = comProd.Article;
            purchaseDetails.ArticleName = comProd.Article.Name;
            purchaseDetails.CurrentStockQty = comProd.CurrentStock;
            purchaseDetails.ProdName = comProd.ProdName;
            purchaseDetails.ProductName = comProd.ProdName.Name;
            purchaseDetails.Size = comProd.Size;
            purchaseDetails.SizeName = comProd.Size.Name;
            purchaseDetails.UOM = comProd.UOM;
            purchaseDetails.UOMName = comProd.UOM.Name;


            obj = purchaseRepository.InsertPurchaseDetails(_head,purchaseDetails, _prevComProductID, _prevPurchaseQty, _prevAmount,prevSupplierID);
            obj.Obj = purchaseRepository.PurchaseHeadInDetails.SingleOrDefault(a => a.PurchaseHeadID == purchaseDetails.PurchaseHeadID);
            return Json(obj);
        }

        public JsonResult GetPurchaseByNoNext([FromBody] PurchaseHead purchaseHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(user.UserId);

            PurchaseHead _head = purchaseRepository.PurchaseHeads.SingleOrDefault(a => a.GeneratedPurchaseHeadNo == purchaseHead.GeneratedPurchaseHeadNo);




            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }


           // SalesHead _head2 = salesRepository.SalesHeadsDetails.Where(a => a.SalesHeadID > _head.SalesHeadID).OrderBy(a => a.SalesHeadID).FirstOrDefault();
            PurchaseHead _head2 = purchaseRepository.PurchaseHeadInDetails.Where(a => a.PurchaseHeadID > _head.PurchaseHeadID).OrderBy(a => a.PurchaseHeadID).FirstOrDefault();



            if (_head2 == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }

            ResultObj obj = new ResultObj();
            obj.Obj = _head2;
            obj.ResultID = 1;
            return Json(obj);
        }



        public JsonResult GetPurchaseByNoPrev([FromBody] PurchaseHead purchaseHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(user.UserId);

            //SalesHead _head = salesRepository.SalesHeads.SingleOrDefault(a => a.GeneratedSalesNo == salesHead.GeneratedSalesNo);
            PurchaseHead _head = purchaseRepository.PurchaseHeads.SingleOrDefault(a => a.GeneratedPurchaseHeadNo == purchaseHead.GeneratedPurchaseHeadNo);




            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }


           // SalesHead _head2 = salesRepository.SalesHeadsDetails.Where(a => a.SalesHeadID < _head.SalesHeadID).OrderByDescending(a => a.SalesHeadID).FirstOrDefault();
            PurchaseHead _head2 = purchaseRepository.PurchaseHeadInDetails.Where(a => a.PurchaseHeadID < _head.PurchaseHeadID).OrderByDescending(a => a.PurchaseHeadID).FirstOrDefault();



            if (_head2 == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }

            ResultObj obj = new ResultObj();
            obj.Obj = _head2;
            obj.ResultID = 1;
            return Json(obj);
        }



        ///  Reports  
        public IActionResult PurchaseReport(string fromDate, string toDate, string customerIDs, string IsDetails, string type)
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Sales Report ";


            return View();
        }



        public IActionResult Reports()
        {
            return View();
        }

        public JsonResult GetPurchaseEntryViewModelReport()
        {
            PurchaseEntryViewModel obj = new PurchaseEntryViewModel();
       
            
            obj.Suppliers = supplierRepository.Suppliers.ToList();

            obj.Articles = productsRepositor.Articles.ToList();
            return Json(obj);
        }


        public JsonResult GetPurchaseReport([FromBody] SalesReportViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }
            List<PurchaseHead> list = new List<PurchaseHead>();
            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Details Purchase Report, ";
            if (model.IsDetails == "0")
            {
                title = "Summary Purchase Report, ";
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
                    list = purchaseRepository.PurchaseHeadInDetails.Where(a => a.PurchaseDate >= _fromDate && a.PurchaseDate <= _toDate && custIDS.Contains(a.Supplier.SupplierId)).ToList();
                }

                else
                {
                    list = purchaseRepository.PurchaseHeads.Where(a => a.PurchaseDate >= _fromDate && a.PurchaseDate <= _toDate && custIDS.Contains(a.Supplier.SupplierId)).ToList();
                }
            }

            else
            {
                if (model.IsDetails != "0")
                {
                    list = purchaseRepository.PurchaseHeadInDetails.Where(a => a.PurchaseDate >= _fromDate && a.PurchaseDate <= _toDate).ToList();
                }
                else
                {
                    list = purchaseRepository.PurchaseHeads.Where(a => a.PurchaseDate >= _fromDate && a.PurchaseDate <= _toDate).ToList();
                }
            }
            obj.ResultNo = title;
            obj.Obj = list;
            return Json(obj);
        }

    }
}
