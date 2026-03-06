using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class DayCloseController : Controller
    {
        private IUserService userService;

        private IPurchaseRepository purchaseRepository;
        private ISalesRepository salsRepository;

        private ICashPaymentRepository cashPaymentRepository;

        private ICashReceiveRepository cashReceiveRepository;
        private IChequeTransactionRepository chequeTransactionRepository;
        private IExpensesRepository expensesRepository;
        private IDayCloseRepository dayCloseRepository;
        private ISalesRepository salesRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public DayCloseController(IDayCloseRepository _dayCloseRepository, IExpensesRepository _expensesRepository, ICashReceiveRepository _cashReceiveRepository,
            ICashPaymentRepository _cashPaymentRepository, ISalesRepository _salsRepository, IUserService _userService, IHttpContextAccessor _httpContextAccessor, IPurchaseRepository _purchaseRepository, ISalesRepository _salesRepository, IChequeTransactionRepository _chequeTransactionRepository)
        {
            userService = _userService;
            httpContextAccessor = _httpContextAccessor;
            purchaseRepository = _purchaseRepository;
            expensesRepository = _expensesRepository;
            cashReceiveRepository = _cashReceiveRepository;
            cashPaymentRepository = _cashPaymentRepository;
            salsRepository = _salsRepository;
            dayCloseRepository = _dayCloseRepository;
            salesRepository = _salesRepository;
            chequeTransactionRepository = _chequeTransactionRepository;
        }



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ProfitLoss()
        {
            return View();
        }

        public IActionResult DaycloseReport()
        {
            return View();
        }


        public IActionResult YearlyReport(string fromDate)
        {


            Company comp = userService.GetCompanyByUser(1);

            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Yearly Report";


            return View();
        }

        public JsonResult GetYearlyProfitLoss([FromBody] SalesReportViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }


            if (!userService.IsControllerAndActionPermitted("DayClose", "ProfitLoss", user))
            {
                obj.ResultID = -1;
                obj.ResultMessage = "UnAuothorized User !";

                return Json(obj);
            }

            List<SP_Yearly_Profit_Loss> list = new List<SP_Yearly_Profit_Loss>();
           

            string title = "Yearly Report  : "+model.FromDate.Year;
                
            if(model.Type == "Date")
            {
                title = "From " + String.Format("{0:dd-MMM-yyyy}", model.FromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", model.ToDate) + " ";
                list = dayCloseRepository.SP_DateWise_Profit_Loss(model.FromDate,model.ToDate);
            }
            else
            {
                list = dayCloseRepository.SP_Yearly_Profit_Loss(model.FromDate.Year);
            }
            
               
            obj.ResultNo = title;
            obj.Obj = list;
            return Json(obj);
        }




        public IActionResult DaycloseReportView(string fromDate, string toDate, string type, string type2)
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Day Close Report";


            return View();
        }


        public JsonResult GetDaycloseReport([FromBody] SalesReportViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }
            List<DayClose> list = new List<DayClose>();
            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Day Close Report : ";
           


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


            if (model.Type == "year")
            {
                title = "Yearly Report "; 


                if (model.Type2 == "Actual")
                {
                    title = "Yearly Report (from Entry)";

                    List <SP_View_DayClose_Yearwise_Actual> list2 = dayCloseRepository.Get_SP_View_DayClose_Yearwise_Actual(1, _fromDate, _toDate);

                    obj.ResultNo = title;
                    obj.Obj = list2;
                    return Json(obj);
                }
                else
                {
                    title = "Yearly Report (from Dayclose)";
                    List<SP_View_DayClose_Yearwise_Actual> list2 = dayCloseRepository.SP_View_DayClose_Yearwise_Dayclose(1, _fromDate, _toDate);

                    obj.ResultNo = title;
                    obj.Obj = list2;
                    return Json(obj);
                }

            }

            else
            {

                if (model.Type2 == "Actual")
                {
                    title += " (from Entry)";
                    List<SP_View_DayClose_Daywise_Actual> list2 = dayCloseRepository.Get_SP_View_DayClose_Daywise_Actual(1, _fromDate, _toDate);
                    obj.ResultNo = title;
                    obj.Obj = list2;
                    return Json(obj);

                }
                else
                {
                    title += " (from Dayclose)";
                    list = dayCloseRepository.DayCloses.Where(a => a.dDate >= _fromDate && a.dDate <= _toDate).ToList();
                }
            }
            
            obj.ResultNo = title;
            obj.Obj = list;
            return Json(obj);
        }

        public IActionResult DeleteDayClose()
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
            return View();
        }
        public JsonResult MakeDeleteDayClose([FromBody] DayClose model)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(user.UserId);
            ResultObj obj = dayCloseRepository.DeleteDayClose(user.UserId, comp.CompanyID, model.dDate);
            if (obj.ResultID == 1)
            {
                obj.Obj = dayCloseRepository.DeletedSayClose.OrderByDescending(a => a.DataCloseDate).ToList();
            }
            return Json(obj);

        }


        public JsonResult GetDelatedDayClose([FromBody] DayClose model)
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            ResultObj obj = new ResultObj();
            obj.Obj = dayCloseRepository.DeletedSayClose.OrderByDescending(a => a.DataCloseDate).ToList();

            return Json(obj);

        }

        public IActionResult DayCloseEntry()
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;

            return View();
        }

        public JsonResult SaveDayClose([FromBody] DayClose model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }


            return Json(obj);
        }

        //List<DayClose> _obj = dayCloseRepository.DayCloses.OrderByDescending(a => a.SlNo).ToList();

        //not using
        public JsonResult GetDayClose([FromBody] DayClose model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            DayClose dayClose = new DayClose();

            if (dayCloseRepository.DayCloses.Any(a => a.dDate == model.dDate))
            {
                dayClose = dayCloseRepository.DayCloses.SingleOrDefault(a => a.dDate == model.dDate);
            }
            else
            {
                DayClose prevDaCose = dayCloseRepository.DayCloses.SingleOrDefault(a => a.dDate == model.dDate.AddDays(-1));


                dayClose.PrevBalance = prevDaCose == null ? 0 : prevDaCose.Balance;

                dayClose.Bkashpayment = 0;
                dayClose.BkashReceive = 0;
                dayClose.CashPayment = cashPaymentRepository.CashPayments.Where(a => a.PaymentDate == model.dDate).Sum(b => b.Amount);

                dayClose.CashReceive = cashReceiveRepository.CashReceives.Where(a => a.ReceiveDate == model.dDate).Sum(b => b.Amount);
                dayClose.TotalPurchase = purchaseRepository.PurchaseHeads.Where(a => a.PurchaseDate == model.dDate).Sum(b => b.PurchaseDetailsList.Sum(c => c.PurchaseQtyInPairAmount));
                dayClose.TotalSales = salsRepository.SalesHeads.Where(a => a.SalesDate == model.dDate).Sum(b => b.SalesDetailsList.Sum(c => c.SalesAmount));
                dayClose.Balance = dayClose.PrevBalance + dayClose.BkashReceive + dayClose.CashReceive - dayClose.CashPayment - dayClose.Bkashpayment;
                dayClose.Note = "";

            }

            obj.Obj = dayClose;
            return Json(obj);
        }

        public JsonResult DayCloseDetailsList([FromBody] DayCloseViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            List<SP_View_DayClose_Details> _list = dayCloseRepository.DayCloseDetailsList(model);


            if (_list == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            if (model.ViewType == 1)
            {
                ResultObj obj2 = dayCloseRepository.GetDataForEntry(model.dDate, model.ViewType);
                obj.Obj2 = obj2.Obj;
            }

            else
            {
                obj.Obj2 = dayCloseRepository.DayCloses.Where(a => a.dDate == model.dDate).SingleOrDefault();
            }
            obj.Obj = _list;

            return Json(obj);
        }



        public JsonResult DayCloseDetailsSalesPurchaseList([FromBody] DayCloseViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            List<SP_View_DayClose_SALES_PUR_Details> _list = dayCloseRepository.DayCloseDetailsSalesPurchaseList(model);


            if (_list == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            if (model.ViewType == 1)
            {
                ResultObj obj2 = dayCloseRepository.GetDataForEntry(model.dDate, model.ViewType);
                obj.Obj2 = obj2.Obj;
            }

            else
            {
                obj.Obj2 = dayCloseRepository.DayCloses.Where(a => a.dDate == model.dDate).SingleOrDefault();
            }
            obj.Obj = _list;

            return Json(obj);
        }

        public JsonResult GetDayCloseNew([FromBody] DayClose model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            obj = dayCloseRepository.GetDataForEntry(model.dDate, 1);
            if (obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            return Json(obj);
        }

        public JsonResult GetLastDayClose()
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            DateTime maxDate = dayCloseRepository.DayCloses.Max(a => a.dDate);

            DayClose _obj = dayCloseRepository.DayCloses.Where(a => a.dDate == maxDate).SingleOrDefault();
            obj.Obj = _obj;
            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            return Json(obj);
        }

        public JsonResult GetExistingDayClose([FromBody] DayClose model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }



            DayClose _obj = dayCloseRepository.DayCloses.Where(a => a.dDate == model.dDate).SingleOrDefault();

            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            obj.Obj = _obj;

            return Json(obj);
        }

        public JsonResult GetExistingDayCloseList([FromBody] DayClose model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }



            List<DayClose> _obj = dayCloseRepository.DayCloses.OrderByDescending(a => a.SlNo).ToList();

            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            obj.Obj = _obj;

            return Json(obj);
        }
        //public JsonResult GetNextDayClose([FromBody] DayClose model)
        //{
        //    ResultObj obj = new ResultObj();
        //    var user = (User)httpContextAccessor.HttpContext.Items["User"];
        //    if (user == null)
        //    {
        //        return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
        //    }

        //    DateTime maxDate = model.dDate.AddDays(1);

        //    DayClose _obj = dayCloseRepository.DayCloses.Where(a => a.dDate == maxDate).SingleOrDefault();
        //    if (_obj == null)
        //    {

        //        obj.ResultMessage = "No Data Found";
        //    }
        //    obj.Obj = _obj;

        //    return Json(obj);
        //}

        public JsonResult SaveClose([FromBody] DayClose model)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company com = userService.GetCompanyByUser(user.UserId);
            //DayClose dayClose = new DayClose();

            //if (dayCloseRepository.DayCloses.Any(a => a.dDate == model.dDate))
            //{
            //    dayClose = dayCloseRepository.DayCloses.SingleOrDefault(a => a.dDate == model.dDate);
            //}
            //else
            //{
            //    dayClose = new DayClose();

            //}
            //dayClose.Company = userService.GetCompanyByUser(user.UserId);
            //dayClose.Note = model.Note;
            //dayClose.Balance = model.Balance;
            //dayClose.Bkashpayment = model.Bkashpayment;
            //dayClose.BkashReceive = model.BkashReceive;
            //dayClose.CashPayment = model.CashPayment;
            //dayClose.CashReceive = model.CashReceive;
            //dayClose.dDate = model.dDate;
            //dayClose.OthersPayment = model.OthersPayment;
            //dayClose.OthersReceive = model.OthersReceive;
            //dayClose.PrevBalance = model.PrevBalance;
            //dayClose.TotalPurchase = model.TotalPurchase;
            //dayClose.TotalSales = model.TotalSales;
            //dayClose.UserId = user.UserId;

            ResultObj obj = dayCloseRepository.SaveDayClose(user.UserId, com.CompanyID, model.AddLess, model.Note, model.dDate, 1);
            if (obj.ResultID == 1)
            {
                obj.Obj = dayCloseRepository.DayCloses.OrderByDescending(a => a.SlNo).ToList();
            }
            return Json(obj);

        }

        public JsonResult DayCloseReportSingleView([FromBody] DayClose model)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            DayCloseReportSingle _obj = new DayCloseReportSingle();
            _obj.dDate = model.dDate;
            _obj.Company = userService.GetCompanyByUser(user.UserId);
            _obj.Cash_Payment = cashPaymentRepository.CashPayments.Where(a => a.PaymentDate == model.dDate).Sum(b => b.Amount);
            _obj.Cash_Receive = cashReceiveRepository.CashReceives.Where(a => a.ReceiveDate == model.dDate).Sum(b => b.Amount);

            _obj.Cash_Receive += salesRepository.SalesHeadsDetails.Where(a => a.SalesDate == model.dDate && a.PaymentMedium.Name.ToUpper() == "CASH").Sum(b => b.ReceiveAmount);
            _obj.Bkash_Receive = salesRepository.SalesHeadsDetails.Where(a => a.SalesDate == model.dDate && a.PaymentMedium.Name.ToUpper() == "BKASH").Sum(b => b.ReceiveAmount);
            _obj.Cash_Payment += expensesRepository.Expenses.Where(a => a.TranDate == model.dDate && a.Type.ToUpper() == "PAYMENT").Sum(b => b.Amount);
            _obj.Cash_Payment -= expensesRepository.Expenses.Where(a => a.TranDate == model.dDate && a.Type.ToUpper() == "RECEIVE").Sum(b => b.Amount);

            _obj.TotalPurchase = purchaseRepository.PurchaseHeads.Where(a => a.PurchaseDate == model.dDate).Sum(b => b.PurchaseDetailsList.Sum(c => c.PurchaseQtyInPairAmount));
            _obj.TotalSales = salsRepository.SalesHeads.Where(a => a.SalesDate == model.dDate).Sum(b => b.SalesDetailsList.Sum(c => c.SalesAmount));

            _obj.Check_Payment = chequeTransactionRepository.ChequeTransactionDetails.Where(a => a.TranDate == model.dDate && a.Type.ToUpper() == "RECEIVE" && a.IsChequePassed == true).Sum(b => b.Amount);
            _obj.Check_Receive = chequeTransactionRepository.ChequeTransactionDetails.Where(a => a.TranDate == model.dDate && a.Type.ToUpper() == "PAYMENT" && a.IsChequePassed == true).Sum(b => b.Amount);



            ResultObj obj = new ResultObj();
            obj.Obj = _obj;
            return Json(obj);
        }

        ///-------------  Yearly Close
        ///

        public IActionResult YearCloseEntry()
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
            return View();
        }

        ///----------------GetDataForEntryYearCLose
        ///



        public JsonResult GetYearCloseNew([FromBody] YearClose model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            obj = dayCloseRepository.GetDataForEntryYearCLose(model.YearName, 1);
            if (obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            return Json(obj);
        }


        public JsonResult GetExistingYearClose([FromBody] YearClose model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            YearClose _obj = dayCloseRepository.YearCloses.Where(a => a.YearName == model.YearName).SingleOrDefault();

            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            obj.Obj = _obj;

            return Json(obj);
        }


        public JsonResult GetExistingYearCloseList()
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }



            List<YearClose> _obj = dayCloseRepository.YearCloses.OrderByDescending(a => a.SlNo).ToList();

            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            obj.Obj = _obj;

            return Json(obj);
        }


        public JsonResult SaveYearClose([FromBody] YearClose model)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company com = userService.GetCompanyByUser(user.UserId);


            ResultObj obj = dayCloseRepository.SaveYearClose(user.UserId, com.CompanyID, model.AddLess, model.Note, model.YearName, 1);
            if (obj.ResultID == 1)
            {
                obj.Obj = dayCloseRepository.YearCloses.OrderByDescending(a => a.SlNo).ToList();
            }
            return Json(obj);

        }


        public JsonResult GetLastYearClose()
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            int maxDate = dayCloseRepository.YearCloses.Max(a => a.YearName);

            YearClose _obj = dayCloseRepository.YearCloses.Where(a => a.YearName == maxDate).SingleOrDefault();
            obj.Obj = _obj;
            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            return Json(obj);
        }

        public JsonResult YearCloseDetailsList([FromBody] DayCloseViewModel model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            List<SP_View_YearClose_Details> _list = dayCloseRepository.YearCloseDetailsList(1,model.YearName,model.TransType);


            if (_list == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            //if (model.ViewType == 1)
            //{
            //    ResultObj obj2 = dayCloseRepository.GetDataForEntryYearCLose(model.dDate, model.ViewType);
            //    obj.Obj2 = obj2.Obj;
            //}

            //else
            //{
            //    obj.Obj2 = dayCloseRepository.DayCloses.Where(a => a.dDate == model.dDate).SingleOrDefault();
            //}
            obj.Obj = _list;

            return Json(obj);
        }

    }
}
