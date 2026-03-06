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
    public class ChequeTransactionController : Controller
    {
        private IUserService userService;
        private IPaymentMediumRepository paymentMediumRepository;

        private ICustomerRepository customerRepo;
        private ISupplierRepository supplierRepo;
        private IChequeTransactionRepository chequeTransactionRepository;

        private readonly IHttpContextAccessor httpContextAccessor;
        private IDayCloseRepository dayCloseEntry;
        public ChequeTransactionController(IUserService _userService, IHttpContextAccessor _httpContextAccessor, IChequeTransactionRepository _chequeTransactionRepository, IPaymentMediumRepository _paymentMediumRepository, ICustomerRepository _customerRepo, ISupplierRepository _supplierRepo, IDayCloseRepository _dayCloseEntry)
        {

            userService = _userService;
            httpContextAccessor = _httpContextAccessor;
            chequeTransactionRepository = _chequeTransactionRepository;
            paymentMediumRepository = _paymentMediumRepository;
            customerRepo = _customerRepo;
            supplierRepo = _supplierRepo;
            dayCloseEntry = _dayCloseEntry;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChequeTransactionEntry()
        {
            return View();
        }

        public JsonResult GetBaknEntrytViewModel()
        {
            BankEntryViewModel obj = new BankEntryViewModel();
            obj.Banks = chequeTransactionRepository.Banks.ToList();

            return Json(obj);
        }
        public IActionResult BankEntry()
        {
            return View();
        }


        public JsonResult GetBankAccountEntryViewModel()
        {
            BankAccountEntryViewModel obj = new BankAccountEntryViewModel();
            obj.Banks = chequeTransactionRepository.Banks.ToList();
            obj.BankAccounts= chequeTransactionRepository.BankAccounts.ToList();
            return Json(obj);
        }
        public IActionResult BankAccountEntry()
        {
            return View();
        }
        public JsonResult SaveBankAccount([FromBody] BankAccount bankAccount)
        {
            BankAccount entry;
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }


            if (bankAccount.BankAccountID == 0)
            {
                entry = new BankAccount();
            }

            else
            {
                entry = chequeTransactionRepository.BankAccounts.SingleOrDefault(a => a.BankAccountID == bankAccount.BankAccountID);
            }

            Bank bank = chequeTransactionRepository.Banks.SingleOrDefault(a=>a.BankID==bankAccount.Bank.BankID);


        
            if (bank == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Bank Name" });
            }


            Company comp = userService.GetCompanyByUser(1);
            entry.Company = comp;

            entry.AccountHolderName = bankAccount.AccountHolderName;
            entry.AccountName = bankAccount.AccountName;
            entry.AccountNo = bankAccount.AccountNo;
            entry.AccountTypeName = bankAccount.AccountTypeName;
            entry.Address = bankAccount.Address;
            entry.Bank = bank;
            entry.StartingBalance = bankAccount.StartingBalance;

            entry.Startdate = bankAccount.Startdate;
            entry.UpdateUserID = user.UserId;
            ResultObj obj = chequeTransactionRepository.SaveBankAccount(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = chequeTransactionRepository.BankAccounts.OrderByDescending(a => a.BankAccountID).ToList();
            }
            return Json(obj);
        }



        public JsonResult GetBankAccount([FromBody] BankAccount bankAccount)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            BankAccount _entry = chequeTransactionRepository.BankAccounts.SingleOrDefault(a => a.BankAccountID == bankAccount.BankAccountID);
            obj.Obj = _entry;
            return Json(obj);
        }
        public JsonResult DeleteBankAccount([FromBody] BankAccount bankAccount)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            BankAccount _entry = chequeTransactionRepository.BankAccounts.SingleOrDefault(a => a.BankAccountID == bankAccount.BankAccountID);
             obj = chequeTransactionRepository.DeleteBankAccount(_entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = chequeTransactionRepository.BankAccounts.OrderByDescending(a => a.BankAccountID).ToList();
            }
            return Json(obj);
        }

        public JsonResult SaveBank([FromBody] Bank bank)
        {
            Bank entry;
            if (bank.BankID == 0)
            {
                entry = new Bank();
            }

            else
            {
                entry = chequeTransactionRepository.Banks.SingleOrDefault(a => a.BankID == bank.BankID);
            }


            var user = (User)httpContextAccessor.HttpContext.Items["User"];

            Company comp = userService.GetCompanyByUser(1);
            entry.Name = bank.Name;
            entry.ShortName = bank.ShortName;
            entry.Company = comp;
            entry.UpdateUserID = user.UserId;
       
            ResultObj obj = chequeTransactionRepository.SaveBank(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = chequeTransactionRepository.Banks.OrderByDescending(a => a.BankID).ToList();
            }
            return Json(obj);
        }


        public JsonResult DeleteBank([FromBody] Bank bank)
        {
            Bank
                entry = chequeTransactionRepository.Banks.SingleOrDefault(a => a.BankID == bank.BankID);


            var user = (User)httpContextAccessor.HttpContext.Items["User"];


            ResultObj obj = chequeTransactionRepository.DeleteBank(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = chequeTransactionRepository.Banks.OrderByDescending(a => a.BankID).ToList();
            }
            return Json(obj);
        }



        public JsonResult GetBank([FromBody] Bank bank)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            Bank _bank = chequeTransactionRepository.Banks.SingleOrDefault(a => a.BankID == bank.BankID);
            obj.Obj = _bank;
            return Json(obj);
        }


        public JsonResult GetChequeTrasactionViewModel()
        {
            ChequeTrasactionViewModel obj = new ChequeTrasactionViewModel();
            obj.PaymentMedium = paymentMediumRepository.GetChequePaymentMedium;
            obj.ReceiverPayer = new List<string> { "Customer", "Supplier" };
            obj.Types = new List<string> { "Receive", "Payment" };
            obj.BankAccounts = chequeTransactionRepository.BankAccounts.ToList();
            obj.ChequeTransactions = chequeTransactionRepository.ChequeTransactionDetails.ToList();
            obj.Customers = customerRepo.Customers.ToList();
            obj.Suppliers = supplierRepo.Suppliers.ToList();
            return Json(obj);
        }



        public JsonResult SaveChequeTransaction([FromBody] ChequeTransaction chequeTransaction)
        {
            ChequeTransaction entry;
            var user = (User)httpContextAccessor.HttpContext.Items["User"];

            decimal prevAmount = 0;
            int prevCustID = 0;
            int prevSuppID = 0;


            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            if (dayCloseEntry.DayCloses.Any(a => a.dDate == chequeTransaction.TranDate))
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            }

            if (chequeTransaction.ChequeTransactionID == 0)
            {
                entry = new ChequeTransaction();
            }

            else
            {
                entry = chequeTransactionRepository.ChequeTransactionDetails.SingleOrDefault(a => a.ChequeTransactionID == chequeTransaction.ChequeTransactionID);

                if (entry.IsChequePassed)
                {
                    prevAmount = entry.Amount;

                    if (entry.Customer != null)
                    {
                        if (entry.Type.ToUpper() == "PAYMENT")
                        {
                            prevAmount = -1 * prevAmount;
                        }


                        prevCustID = entry.Customer.CustomerID;
                    }
                    else
                    {
                        if (entry.Type.ToUpper() == "RECEIVE")
                        {
                            prevAmount = -1 * prevAmount;
                        }
                        prevSuppID = entry.Supplier.SupplierId;
                    }
                   

                }
            }

            Customer cust = customerRepo.Customers.SingleOrDefault(a => a.CustomerID == chequeTransaction.Customer.CustomerID);
            Supplier sup = supplierRepo.Suppliers.SingleOrDefault(a => a.SupplierId == chequeTransaction.Supplier.SupplierId);
            BankAccount acc = chequeTransactionRepository.BankAccounts.SingleOrDefault(a => a.BankAccountID == chequeTransaction.BankAccount.BankAccountID);
            PaymentMedium paym = paymentMediumRepository.PaymentMediums.SingleOrDefault(a => a.PaymentMediumID == chequeTransaction.PaymentMedium.PaymentMediumID);



            Company comp = userService.GetCompanyByUser(user.UserId);

            if (cust == null && sup == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Customer or Supplier !!" });
            }
            if (paym == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Payment Medium !!" });
            }

            if (acc == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Bank Account!!" });
            }


            if (cust != null && sup != null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Miss Match Customer/Supplier" });
            }

            if (!(chequeTransaction.Type == "Receive" || chequeTransaction.Type == "Payment"))
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Receive/Payment Type !!" });
            }


            entry.Amount = chequeTransaction.Amount;
            entry.BankAccount = acc;
            entry.TranDate=chequeTransaction.TranDate;
            entry.ChequePassDate = chequeTransaction.ChequePassDate;
            entry.ChequeTTNo = chequeTransaction.ChequeTTNo;
            entry.Company = comp;
            entry.Customer = cust;
            entry.Supplier = sup;
            entry.IsChequePassed = chequeTransaction.IsChequePassed;
            entry.LedgerName = chequeTransaction.LedgerName;
            entry.Note = chequeTransaction.Note;
            entry.PaymentMedium = paym;
            entry.Type = chequeTransaction.Type;
            entry.User = user;
            //entry.ShortName = bank.ShortName;
            entry.Company = comp;
            ResultObj obj = chequeTransactionRepository.SaveChequeTransaction(entry, prevCustID,  prevSuppID,  prevAmount);
            if (obj.ResultID == 1)
            {
                obj.Obj = chequeTransactionRepository.ChequeTransactionDetails.OrderByDescending(a => a.ChequeTransactionID).ToList();
            }
            return Json(obj);
        }


        public JsonResult GetChequeTransaction([FromBody] ChequeTransaction chequeTransaction)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
         
            ChequeTransaction _obj = chequeTransactionRepository.ChequeTransactionDetails.SingleOrDefault(a => a.ChequeTransactionID == chequeTransaction.ChequeTransactionID);
            obj.Obj = _obj;
            return Json(obj);
        }

        public JsonResult DeleteChequeTransaction([FromBody] ChequeTransaction chequeTransaction)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            decimal prevAmount = 0;
            int prevCustID = 0;
            int prevSuppID = 0;


            ChequeTransaction
                entry = chequeTransactionRepository.ChequeTransactionDetails.SingleOrDefault(a => a.ChequeTransactionID == chequeTransaction.ChequeTransactionID);

            if (dayCloseEntry.DayCloses.Any(a => a.dDate == entry.TranDate))
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            }
            if (entry.IsChequePassed)
            {
                prevAmount = entry.Amount;

                if (entry.Customer != null)
                {
                    if (entry.Type.ToUpper() == "PAYMENT")
                    {
                        prevAmount = -1 * prevAmount;
                    }


                    prevCustID = entry.Customer.CustomerID;
                }
                else
                {
                    if (entry.Type.ToUpper() == "RECEIVE")
                    {
                        prevAmount = -1 * prevAmount;
                    }
                    prevSuppID = entry.Supplier.SupplierId;
                }


            }


            ResultObj obj = chequeTransactionRepository.DeleteChequeTransaction(entry, prevCustID, prevSuppID, prevAmount);
            if (obj.ResultID == 1)
            {
                obj.Obj = chequeTransactionRepository.ChequeTransactionDetails.OrderByDescending(a => a.ChequeTransactionID).ToList();
            }
            return Json(obj);
        }

        // ----- Reports ---------

        public IActionResult ChequeTranReports()
        {
            return View();
        }

        public IActionResult ChequeTranReportView(string fromDate, string toDate, string customerIDs, string IsDetails, string type, string recvPayType, string customerOrSupplier)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                TempData["CompanyName"] = "";

                TempData["CompanyAddress"] = "";
                TempData["Title"] = "Cash Transaction Report ";
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

        public JsonResult GetChequeTransactionReport([FromBody] ChequeTransactionReportViewModel model)
        {


            ResultObj obj = new ResultObj();

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }


            List<ChequeTransaction> listAll = new List<ChequeTransaction>();


            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;
            List<int> custIDS = model.CustomerIds.Split(",").Select(a => int.Parse(a)).ToList();
            string title = "Details Cheque Transaction Report, ";
            if (model.IsDetails == "0")
            {

                title = "  Cheque Transaction Report, ";
                if (model.RecvPayType == "Receive")
                {
                    title = "  Cheque Receive Report, ";
                }

               else if (model.RecvPayType == "Payment")
                {
                    title = "  Cheque Payment Report, ";
                }



            }

            if (model.CustomerOrSupplier == "All")
            {
                title += " (All)";
            }
            else 
            {
                title += " ("+ model.CustomerOrSupplier + ")";
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

            ///----- listAll = chequeTransactionRepository.ChequeTransactionDetails.Where(a => a.TranDate >= _fromDate && a.TranDate <= _toDate && custIDS.Contains(a.Supplier.SupplierId)).ToList();


            if (model.RecvPayType == "All")
            {

                if (model.CustomerOrSupplier == "All")
                {

                    listAll = chequeTransactionRepository.ChequeTransactionDetails.Where(a => a.TranDate >= _fromDate && a.TranDate <= _toDate).ToList();
                }
                else if (model.CustomerOrSupplier == "Customer")
                {

                    listAll = chequeTransactionRepository.ChequeTransactionDetails.Where(a =>( a.TranDate >= _fromDate && a.TranDate <= _toDate) && a.Customer !=null).ToList();
                }
                else if (model.CustomerOrSupplier == "Supplier")
                {

                    listAll = chequeTransactionRepository.ChequeTransactionDetails.Where(a => (a.TranDate >= _fromDate && a.TranDate <= _toDate) && a.Supplier != null).ToList();
                }

                obj.Obj = listAll;
            }

            else if (model.RecvPayType == "Payment")
            {
                if (model.CustomerOrSupplier == "All")
                {
                    listAll = chequeTransactionRepository.ChequeTransactionDetails.Where(a => (a.TranDate >= _fromDate && a.TranDate <= _toDate) && a.Type.ToUpper() == "PAYMENT").ToList();
                }

                else if (model.CustomerOrSupplier == "Customer")
                {

                    listAll = chequeTransactionRepository.ChequeTransactionDetails.Where(a => (a.TranDate >= _fromDate && a.TranDate <= _toDate)  && a.Type.ToUpper() == "PAYMENT" && a.Customer != null).ToList();
                }
                else if (model.CustomerOrSupplier == "Supplier")
                {

                    listAll = chequeTransactionRepository.ChequeTransactionDetails.Where(a => (a.TranDate >= _fromDate && a.TranDate <= _toDate) && a.Type.ToUpper() == "PAYMENT" && a.Supplier != null).ToList();
                }


                obj.Obj = listAll;

            }
            else if (model.RecvPayType == "Receive")
            {
                if (model.CustomerOrSupplier == "All")
                {

                    listAll = chequeTransactionRepository.ChequeTransactionDetails.Where(a => (a.TranDate >= _fromDate && a.TranDate <= _toDate) && a.Type.ToUpper() == "RECEIVE").ToList();
                }
                else if (model.CustomerOrSupplier == "Customer")
                {

                    listAll = chequeTransactionRepository.ChequeTransactionDetails.Where(a => (a.TranDate >= _fromDate && a.TranDate <= _toDate) && a.Type.ToUpper() == "RECEIVE" && a.Customer != null).ToList();
                }
                else if (model.CustomerOrSupplier == "Supplier")
                {

                    listAll = chequeTransactionRepository.ChequeTransactionDetails.Where(a => (a.TranDate >= _fromDate && a.TranDate <= _toDate) && a.Type.ToUpper() == "RECEIVE" && a.Supplier != null).ToList();
                }



                obj.Obj = listAll;
            }


            obj.ResultNo = title;

            return Json(obj);

        }

    }
}
