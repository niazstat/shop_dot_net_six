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
    public class SalesAdjustController : Controller
    {




        private ICustomerRepository customerRepository;
        private IUserService userService;

        private IProductsRepositor productsRepositor;
        private ICompanyProductRepository companyProductRepository;
        private ISalesRepository salesRepository;
        private ISalesAdjust saleAdjustRepo;
        private readonly IHttpContextAccessor httpContextAccessor;
        private IDayCloseRepository dayCloseEntry;
        public SalesAdjustController(ICustomerRepository _customerRepository, IUserService _userService, IHttpContextAccessor _httpContextAccessor, IProductsRepositor _productsRepositor, ISalesRepository _salesRepository, ICompanyProductRepository _companyProductRepository, ISalesAdjust _saleAdjustRepo, IDayCloseRepository _dayCloseEntry)
        {

            customerRepository = _customerRepository;
            userService = _userService;
            httpContextAccessor = _httpContextAccessor;

            productsRepositor = _productsRepositor;
            salesRepository = _salesRepository;
            companyProductRepository = _companyProductRepository;
            saleAdjustRepo = _saleAdjustRepo;
            dayCloseEntry = _dayCloseEntry;
        }


        public IActionResult Index()
        {
            return View();
        }




        public IActionResult SalesAdjustEntry()
        {
            return View();
        }

        public IActionResult SalesAdjustReport()
        {
            return View();
        }



        public IActionResult SalesAdjustReportView(string fromDate, string toDate, string customerIDs, string IsDetails, string type)
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Sales Adjust Report ";


            return View();
        }



        public JsonResult GetSalesAdjustReport([FromBody] SalesReportViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }
            List<SalesAdjust> list = new List<SalesAdjust>();
            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Details Sales Adjust Report, ";
            if (model.IsDetails == "0")
            {
                title = "Summary Sales Adjust Report, ";
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
                    list = saleAdjustRepo.SalesAdjustsFull.Where(a => a.SalesAdjustDate >= _fromDate && a.SalesAdjustDate <= _toDate && custIDS.Contains(a.Customer.CustomerID)).ToList();
                }

                else
                {
                    list = saleAdjustRepo.SalesAdjustsFull.Where(a => a.SalesAdjustDate >= _fromDate && a.SalesAdjustDate <= _toDate && custIDS.Contains(a.Customer.CustomerID)).ToList();
                }
            }

            else
            {
                if (model.IsDetails != "0")
                {
                    list = saleAdjustRepo.SalesAdjustsFull.Where(a => a.SalesAdjustDate >= _fromDate && a.SalesAdjustDate <= _toDate).ToList();
                }
                else
                {
                    list = saleAdjustRepo.SalesAdjustsFull.Where(a => a.SalesAdjustDate >= _fromDate && a.SalesAdjustDate <= _toDate).ToList();
                }
            }
            obj.ResultNo = title;
            obj.Obj = list;
            return Json(obj);
        }


        public JsonResult GetSalesAdjustFromSalesNo([FromBody] SalesHead salesHead)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            ResultObj obj = new ResultObj();
            SalesHead head = salesRepository.SalesHeads.FirstOrDefault(a => a.GeneratedSalesNo == salesHead.GeneratedSalesNo);

            SalesAdjust _adjust = new SalesAdjust();
            if (head != null)
            {
                SalesAdjustDetails _detAdj = saleAdjustRepo.SalesAdjustDetails.Where(a => a.SalesHeadID == head.SalesHeadID).FirstOrDefault();
                if (_detAdj != null)
                {
                    _adjust = saleAdjustRepo.SalesAdjustAsnoTracking.FirstOrDefault(a => a.SalesAdjustID == _detAdj.SalesAdjustID);
                    obj.Obj = _adjust;
                }
            }

            return Json(obj);
        }



        public JsonResult SaveSalesAdjust([FromBody] SalesAdjust salesAdjust)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            //if ((DateTime.Today - salesAdjust.SalesAdjustDate).TotalDays > 5 && !salesAdjust.IsAllowEdit)
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            //}

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == salesReturn.ReturnDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}
            Company comp = userService.GetCompanyByUser(1);
            ResultObj obj = new ResultObj();

            var ids = salesAdjust.SalesAdjustDetailsList
            .Select(x => x.SalesHeadID)
            .ToList();
            //if (saleAdjustRepo.SalesAdjustDetails.AsEnumerable().Any(a => salesAdjust.SalesAdjustDetailsList.Any(b => b.SalesHeadID == a.SalesHeadID)))
            

                if (saleAdjustRepo.SalesAdjustDetails
                        .Where(x => x.SalesHeadID != null)
                        .Any(x => ids.Contains(x.SalesHeadID.Value)))
            {
                // bool fond = true;





                var _ids = salesAdjust.SalesAdjustDetailsList.Select(a => a.SalesHeadID).ToList();

                SalesAdjustDetails _detAdj = saleAdjustRepo.SalesAdjustDetails.Where(a => _ids.Contains(a.SalesHeadID)).FirstOrDefault();
                salesAdjust.SalesAdjustID = _detAdj.SalesAdjustID;

                obj = saleAdjustRepo.UpdateSalesAdjust(salesAdjust);


                foreach (var item in salesAdjust.SalesAdjustDetailsList)
                {

                    // SalesDetails salesDet = salesRepository.SalesDetailsAsNoTracking.SingleOrDefault(a => a.SalesDetailsID == item.SalesDetailsID);
                    // CompanyProduct comProd = companyProductRepository.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == salesDet.CompanyProductID);

                    if (saleAdjustRepo.SalesAdjustDetails.Any(a => a.SalesDetailsID == item.SalesDetailsID))
                    {

                        SalesAdjustDetails _adjDet = saleAdjustRepo.SalesAdjustDetails.SingleOrDefault(a => a.SalesDetailsID == item.SalesDetailsID);
                        _adjDet.SalesAdjustRate = item.SalesAdjustRate;

                        //salesAdjust is ot used here
                        obj = saleAdjustRepo.UpdateSalesAdjustDetails(salesAdjust, _adjDet);
                        if (obj.ResultID == -1)
                        {
                            return Json(obj);
                        }
                    }
                    else
                    {


                        item.Company = comp;
                        item.SalesAdjustID = _detAdj.SalesAdjustID;

                        obj = saleAdjustRepo.InsertSalesAdjustDetails(salesAdjust, item);
                        if (obj.ResultID == -1)
                        {
                            return Json(obj);
                        }
                    }

                }


            }
            else
            {

                salesAdjust.Company = comp;
                salesAdjust.UserId = user.UserId;
                salesAdjust.AutoSalesAdjustNo = 0;


           var _ids = salesAdjust.SalesAdjustDetailsList.Select(a => a.SalesHeadID).ToList();

              //  SalesAdjustDetails _detAdj = saleAdjustRepo.SalesAdjustDetails.Where(a => _ids.Contains(a.SalesHeadID)).First();




                bool isCustmerChecked = false;

                foreach (var item in salesAdjust.SalesAdjustDetailsList)
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

                    item.SalesRate = salesDet.SalesRate;

                    item.UOMID = salesDet.UOM.UOMID;
                    // _totamAmount += item.SalesAmount;
                    // _totamNetReturnAmount += item.ReyurnQtyInPair * (salesDet.SalesRate - salesDet.CommissionRate);

                }


                obj = saleAdjustRepo.SaveSalesAdjust(salesAdjust);
            }
            //obj.Obj = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }
    }
}
