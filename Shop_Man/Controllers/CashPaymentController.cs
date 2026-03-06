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
    public class CashPaymentController : Controller
    {


        private ISupplierCategory supplierCategory;
        private ICustomerSubCategory customerSubCategory;
        private ICustomerRepository customerRepository;
        private ISupplierSubCategory supplierSubCategory;
        private ISupplierRepository supplierRepository;
        private IUserService userService;
        private IPaymentMediumRepository paymentMediumRepository;

        private ICashPaymentRepository cashPaymentRepository;
        private ICashReceiveRepository cashrecieveRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private IDayCloseRepository dayCloseEntry;
        public CashPaymentController(ISupplierCategory _supplierCategory, ISupplierSubCategory _supplierSubCategory, ISupplierRepository _supplierRepository, IUserService _userService, IHttpContextAccessor _httpContextAccessor, IPaymentMediumRepository _paymentMediumRepository, ICashPaymentRepository _cashPaymentRepository, ICashReceiveRepository _cashrecieveRepository, ICustomerRepository _customerRepository, ICustomerSubCategory _customerSubCategory, IDayCloseRepository _dayCloseEntry)
        {
            supplierCategory = _supplierCategory;
            supplierSubCategory = _supplierSubCategory;
            supplierRepository = _supplierRepository;
            userService = _userService;
            httpContextAccessor = _httpContextAccessor;
            paymentMediumRepository = _paymentMediumRepository;
            cashPaymentRepository = _cashPaymentRepository;
            cashrecieveRepository = _cashrecieveRepository;
            customerRepository = _customerRepository;
            customerSubCategory = _customerSubCategory;
            dayCloseEntry = _dayCloseEntry;
        }


        public JsonResult GetCashPaymentViewModel()
        {
            CashPaymentViewModel obj = new CashPaymentViewModel();
            obj.Customers = customerRepository.Customers.ToList();
            obj.Suppliers = supplierRepository.Suppliers.ToList();
            obj.CustomerSubCategorys = customerSubCategory.CustomerSubCategorys.ToList();
            obj.SupplierSubCategorys = supplierSubCategory.SupplierSubCategorys.ToList();
            obj.PaymentMedium = paymentMediumRepository.GetCahPaymentMedium;
            obj.CashPayments = cashPaymentRepository.CashPayments.OrderByDescending(a => a.InvoicNoSL).ToList();
            return Json(obj);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CashPaymentEntry()
        {
            return View();

        }



        public JsonResult SaveCashPayment([FromBody] CashPayment cashPayment)
        {
            CashPayment entry;
            decimal _prevCashPaymount = 0;
            int prevSuppID = 0;
            int prevCustID = 0;

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == cashPayment.PaymentDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update Not Possible !!" });
            //}
            if (cashPayment.CashPaymentID == 0)
            {
                entry = new CashPayment();
            }

            else
            {
                entry = cashPaymentRepository.CashPayments.SingleOrDefault(a => a.CashPaymentID == cashPayment.CashPaymentID);

                if ((DateTime.Today - entry.PaymentDate).TotalDays > 5 && !entry.IsAllowEdit)
                {
                    return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
                }



                if (entry.Supplier != null)
                {
                    prevSuppID = entry.Supplier.SupplierId;
                }
                else
                {
                    prevCustID = entry.Customer.CustomerID;
                }
                _prevCashPaymount = entry.Amount;
            }

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });

            }

            entry.User = user;
            Company comp = userService.GetCompanyByUser(1);
            entry.Company = comp;

            PaymentMedium pay = paymentMediumRepository.PaymentMediums.SingleOrDefault(a => a.PaymentMediumID == cashPayment.PaymentMedium.PaymentMediumID && a.Name.ToUpper() == "CASH");

            if (pay == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid payment medium" });
            }
            entry.PaymentMedium = pay;
            Supplier supp = supplierRepository.Suppliers.SingleOrDefault(a => a.SupplierId == cashPayment.Supplier.SupplierId);
            Customer cust = customerRepository.Customers.SingleOrDefault(a => a.CustomerID == cashPayment.Customer.CustomerID);
            if (supp == null && cust==null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Supplier/Customer" });
            }
            if (supp != null && cust != null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Mismatch Supplier/Customer" });
            }
            entry.Supplier = supp;
            entry.Customer = cust;
            entry.Amount = cashPayment.Amount;
            entry.Note = cashPayment.Note;
            entry.PaymentDate = cashPayment.PaymentDate;
            ResultObj obj = cashPaymentRepository.SaveCashPayment(entry, prevCustID, prevSuppID, _prevCashPaymount);
            if (obj.ResultID == 1)
            {
                obj.Obj = cashPaymentRepository.CashPayments.OrderByDescending(a => a.InvoicNoSL).ToList();
            }
            return Json(obj);
        }


        public JsonResult GetCashPayment([FromBody] CashPayment cashPayment)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            CashPayment cashPaymentID = cashPaymentRepository.CashPayments.SingleOrDefault(a => a.CashPaymentID == cashPayment.CashPaymentID);
            obj.Obj = cashPaymentID;
            return Json(obj);
        }


        public JsonResult DeleteCashPayment([FromBody] CashPayment cashPayment)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            Company comm = userService.GetCompanyByUser(user.UserId);
            CashPayment entry = cashPaymentRepository.CashPaymentsAsNoTracking.SingleOrDefault(a => a.CashPaymentID == cashPayment.CashPaymentID);

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == entry.PaymentDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update Not Possible !!" });
            //}
            if ((DateTime.Today - entry.PaymentDate).TotalDays > 5 && !entry.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }
            int prevSuppId = entry.Supplier==null?0: entry.Supplier.SupplierId;
            int prevcustId = entry.Customer == null ? 0 : entry.Customer.CustomerID;
            decimal prevAmount = entry.Amount;

            ResultObj obj = cashPaymentRepository.DeleteCashPayment(cashPayment, prevcustId, prevSuppId, prevAmount);
            if (obj.ResultID == 1)
            {
                obj.Obj = cashPaymentRepository.CashPayments.OrderByDescending(a => a.InvoicNoSL).ToList();

            }
            return Json(obj);
        }


        ///////// Report --------------------------------------
        ///

        public IActionResult Cash_Tran_Reports()
        {
            return View();
        }

        public IActionResult CashTransactionReport(string fromDate, string toDate, string customerIDs, string IsDetails, string type, string recvPayType)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

            }
            else
            {
                Company comp = userService.GetCompanyByUser(user.UserId);
                TempData["CompanyName"] = comp.Name;

                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "Cash Transaction Report ";
            }

            return View();
        }

        public JsonResult GetCashTransactionReport([FromBody] CashTranReportsViewModel model)
        {


            ResultObj obj = new ResultObj();

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage="Session Expired " });  ;
            }

            List<SP_Cash_Transaction_Details> listAll = new List<SP_Cash_Transaction_Details>();
            List<CashPayment> listCashPay = new List<CashPayment>();
            List<CashReceive> listCashRecv = new List<CashReceive>();

            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;
            List<int> custIDS = model.CustomerIds.Split(",").Select(a => int.Parse(a)).ToList();
            string title = "Details Cash Transaction Report, ";
            if (model.IsDetails == "0")
            {

                title = "  Cash/Expenses Transaction Report, ";
                if (model.RecvPayType == "Receive")
                {
                    title = "  Cash Receive Report, ";
                }

                if (model.RecvPayType == "Payment")
                {
                    title = "  Cash Payment Report, ";
                }
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
       
            if (model.RecvPayType == "All")
            {

                if (model.IsDetails == "0")
                {

                    listAll = cashPaymentRepository.SP_Cash_Transaction_Summ(_fromDate, _toDate).ToList();
                    obj.Obj = listAll;
                }


                else
                {

                    listAll = cashPaymentRepository.Cash_Transaction_Details(_fromDate, _toDate).ToList();
                    obj.Obj = listAll;
                }
            }
            else
            {
                if (model.RecvPayType == "Payment")
                {
                    if (model.CustomerIds != "0")
                    {

                        listCashPay= cashPaymentRepository.CashPaymentDetails.Where(a => a.PaymentDate >= _fromDate && a.PaymentDate <= _toDate && custIDS.Contains(a.Supplier.SupplierId)).ToList();
                        obj.Obj = listCashPay;
                    }

                    else
                    {
                        listCashPay = cashPaymentRepository.CashPaymentDetails.Where(a => a.PaymentDate >= _fromDate && a.PaymentDate <= _toDate).ToList();
                        obj.Obj = listCashPay;
                    }
                }
               else if (model.RecvPayType == "Receive")
                {
                    if (model.CustomerIds != "0")
                    {

                        listCashRecv = cashrecieveRepository.CashReceivesDetails.Where(a => a.ReceiveDate >= _fromDate && a.ReceiveDate <= _toDate && custIDS.Contains(a.Customer.CustomerID)).ToList();
                        obj.Obj = listCashRecv;
                    }

                    else
                    {
                        listCashRecv = cashrecieveRepository.CashReceivesDetails.Where(a => a.ReceiveDate >= _fromDate && a.ReceiveDate <= _toDate).ToList();
                        obj.Obj = listCashRecv;
                    }
                }

            }
            obj.ResultNo = title;
         
            return Json(obj);
        }
    }
}
