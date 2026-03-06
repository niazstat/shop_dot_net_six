using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Man.EFRepository;
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
    public class StockAdjustController : Controller
    {



        private IUserService userService;

        private IProductsRepositor productsRepositor;
        private ICompanyProductRepository companyProductRepository;

        private IStockAdjustRepository stockAdjustRepository;

        private readonly IHttpContextAccessor httpContextAccessor;
        public StockAdjustController(IUserService _userService, IProductsRepositor _productsRepositor, ICompanyProductRepository _companyProductRepository, IStockAdjustRepository _stockAdjustRepository, IHttpContextAccessor _httpContextAccessor)
        {
            userService = _userService;
            productsRepositor = _productsRepositor;

            companyProductRepository = _companyProductRepository;
            stockAdjustRepository = _stockAdjustRepository;

            httpContextAccessor = _httpContextAccessor;
        }





        public IActionResult Index()
        {
            return View();
        }


        public IActionResult StockAdjustEntry()
        {
            return View();
        }
        public IActionResult StockAdjustReport()
        {
            return View();
        }


        public IActionResult StockAdjustReportView(string fromDate, string toDate, string type)
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Stock Adjust Report ";


            return View();
        }

        public JsonResult GetStockAdjustReport([FromBody] SalesReportViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }
            List<StockAdjustHead> list = new List<StockAdjustHead>();
            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Stock Adjust Report : ";



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




            list = stockAdjustRepository.StockAdjustHeadFull.Where(a => a.AdjustDate >= _fromDate && a.AdjustDate <= _toDate).ToList();


            obj.ResultNo = title;
            obj.Obj = list;
            return Json(obj);
        }



        public JsonResult GetSalesEntryViewModel()
        {
            SalesEntryViewModel obj = new SalesEntryViewModel();



            obj.ProdNames = productsRepositor.ProdNames.ToList();
            return Json(obj);
        }





        public JsonResult SaveStockAdjust([FromBody] StockAdjustHead salesHead)
        {
            ResultObj obj = new ResultObj();

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            if (salesHead.AdjustDate > DateTime.Today)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Date!" });
            }

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == salesHead.SalesDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update Not Possible !!" });
            //}

            salesHead.EntryDate = DateTime.Now;
            salesHead.LastUpdateTime = DateTime.Now;
            int custWiseID = 0;
            Company comp = userService.GetCompanyByUser(user.UserId);


            salesHead.Company = comp;
            salesHead.UserId = user.UserId;

            List<View_Item_Close> Get_View_Item_Current_Close_List = new List<View_Item_Close>();

            try
            {


                Get_View_Item_Current_Close_List = companyProductRepository.Get_View_Item_Current_Close_List(salesHead.AdjustDate);

            }

            catch (Exception ex)
            {
                obj.ResultID = -1;
                return Json(obj);

            }


            foreach (var item in salesHead.StockAdjustDetailsList)
            {
                CompanyProduct comProd = companyProductRepository.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == item.CompanyProductID);

                View_Item_Close view_Item_Close = Get_View_Item_Current_Close_List.SingleOrDefault(a => a.CompanyProductID == item.CompanyProductID);


                if (view_Item_Close != null)
                {
                    item.AdjustRate = view_Item_Close.AvgAmount;
                }


                item.CompanyProduct = comProd;
                item.Article = comProd.Article;
                item.ArticleName = comProd.Article.Name;

                item.ProdName = comProd.ProdName;
                item.ProductName = comProd.ProdName.Name;
                item.Size = comProd.Size;
                item.SizeName = comProd.Size.Name;
                item.UOM = comProd.UOM;
                item.UOMName = comProd.UOM.Name;
                item.Company = comp;
                item.EntryDate = DateTime.Now;


            }

            obj = stockAdjustRepository.SaveStockAdjustHead(salesHead);
            //obj.Obj = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }


        public JsonResult UpdateStockAdjust([FromBody] StockAdjustHead salesHead)
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

            StockAdjustHead _head = stockAdjustRepository.StockAdjustHeadFull.SingleOrDefault(a => a.StockAdjustHeadID == salesHead.StockAdjustHeadID);

            if (_head.AdjustDate > DateTime.Today)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Date!" });
            }


            //if ((DateTime.Today - _head.AdjustDate).TotalDays > 5 && !_head.IsAllowEdit)
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            //}





            _head.Note1 = salesHead.Note1;
            _head.AdjustDate = salesHead.AdjustDate;

            _head.LastUpdateTime = DateTime.Now;
            _head.UpdateUserID = user.UserId;

            decimal _totamAmount = 0;

            decimal _totalCommision = 0;

            foreach (var item in _head.StockAdjustDetailsList)
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
                // _totamAmount += item.SalesAmount;

                // _totalCommision += item.CommissionAmount;
            }


            ResultObj obj = stockAdjustRepository.UpdateStockAdjustHead(_head);
            //obj.Obj = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }


        public JsonResult InsertStockadjustDetails([FromBody] StockAdjustDetails salesDetails)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }


            Company comp = userService.GetCompanyByUser(user.UserId);

            StockAdjustHead _head = stockAdjustRepository.StockAdjustHeadAsnoTracking.SingleOrDefault(a => a.StockAdjustHeadID == salesDetails.StockAdjustHeadID);

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == _head.SalesDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update Not Possible !!" });
            //}

            if ((DateTime.Today - _head.AdjustDate).TotalDays > 5 && !_head.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days Over ." });
            }



            List<View_Item_Close> Get_View_Item_Current_Close_List = new List<View_Item_Close>();

            try
            {


                Get_View_Item_Current_Close_List = companyProductRepository.Get_View_Item_Current_Close_List(_head.AdjustDate);

            }

            catch (Exception ex)
            {
                obj.ResultID = -1;
                return Json(obj);

            }




            CompanyProduct comProd = companyProductRepository.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == salesDetails.CompanyProductID);


            View_Item_Close view_Item_Close = Get_View_Item_Current_Close_List.SingleOrDefault(a => a.CompanyProductID == salesDetails.CompanyProductID);


            if (view_Item_Close != null)
            {
                salesDetails.AdjustRate = view_Item_Close.AvgAmount;
            }


            salesDetails.Company = comp;

            salesDetails.CompanyProduct = comProd;
            salesDetails.Article = comProd.Article;
            salesDetails.ArticleName = comProd.Article.Name;

            salesDetails.ProdName = comProd.ProdName;
            salesDetails.ProductName = comProd.ProdName.Name;
            salesDetails.Size = comProd.Size;
            salesDetails.SizeName = comProd.Size.Name;
            salesDetails.UOM = comProd.UOM;
            salesDetails.UOMName = comProd.UOM.Name;
            salesDetails.EntryDate = DateTime.Now;


            obj = stockAdjustRepository.InsertStockAdjustDetails(_head, salesDetails);
            obj.Obj = stockAdjustRepository.StockAdjustHeadFull.SingleOrDefault(a => a.StockAdjustHeadID == salesDetails.StockAdjustHeadID);
            return Json(obj);
        }



        public JsonResult DeleteStockAdjustDetails([FromBody] StockAdjustDetails salesDetails)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            StockAdjustHead _head = stockAdjustRepository.StockAdjustHeadAsnoTracking.SingleOrDefault(a => a.StockAdjustHeadID == salesDetails.StockAdjustHeadID);
            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == _head.SalesDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update Not Possible !!" });
            //}

            if ((DateTime.Today - _head.AdjustDate).TotalDays > 5 && !_head.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Day Over ." });
            }



            ResultObj obj = stockAdjustRepository.DeleteStockAdjustDetails(_head, salesDetails);
            obj.Obj = stockAdjustRepository.StockAdjustHeadFull.SingleOrDefault(a => a.StockAdjustHeadID == salesDetails.StockAdjustHeadID);
            return Json(obj);
        }




        public JsonResult GetStockAdjustByAdjustNo([FromBody] StockAdjustHead salesHead)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(1);

            StockAdjustHead _head = stockAdjustRepository.StockAdjustHeadFull.SingleOrDefault(a => a.GeneratedStockAdjustNo == salesHead.GeneratedStockAdjustNo);

            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "No Data Found" });
            }


            ResultObj obj = new ResultObj();
            obj.Obj = _head;
            obj.ResultID = 1;
            return Json(obj);
        }
    }
}
