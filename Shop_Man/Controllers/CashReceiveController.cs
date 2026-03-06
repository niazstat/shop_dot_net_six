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
    public class CashReceiveController : Controller
    {


        private ICustomerCategory customerCategoryRepo;
        private ICustomerSubCategory customerSubCategoryRepo;
        private ICustomerRepository customerRepository;
        private IUserService userService;
        private IPaymentMediumRepository paymentMediumRepository;
      
     
        private ICashReceiveRepository cashReceiveRepository;

        private readonly IHttpContextAccessor httpContextAccessor;
        public CashReceiveController(ICustomerCategory _customerCategoryRepo, ICustomerSubCategory _customerSubCategoryRepo, ICustomerRepository _customerRepository, IUserService _userService, IHttpContextAccessor _httpContextAccessor, IPaymentMediumRepository _paymentMediumRepository, ICashReceiveRepository _cashReceiveRepository)
        {
            customerCategoryRepo = _customerCategoryRepo;
            customerSubCategoryRepo = _customerSubCategoryRepo;
            customerRepository = _customerRepository;
            userService = _userService;
            httpContextAccessor = _httpContextAccessor;
            paymentMediumRepository = _paymentMediumRepository;
            cashReceiveRepository = _cashReceiveRepository;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CashReceiveEntry()
        {

            return View();

        }


        public JsonResult GetCashReceiveViewModel()
        {
            CashReceiveViewModel obj = new CashReceiveViewModel();
            obj.CustomerCategorys = customerCategoryRepo.CustomerCategorys.ToList();
            obj.CustomerSubCategorys = customerSubCategoryRepo.CustomerSubCategorys.ToList();
            obj.Customers = customerRepository.Customers.ToList();
            obj.PaymentMedium = paymentMediumRepository.GetCahPaymentMedium;
            DateTime last7Days = DateTime.Now.AddDays(-7);
            obj.CashReceives = cashReceiveRepository.CashReceivesDetails
                    .Where(a => a.ReceiveDate >= last7Days).OrderByDescending(a=>a.InvoicNoSL).ToList();
            return Json(obj);
        }


        public JsonResult SaveCashReceive([FromBody] CashReceive cashReceive)
        {
            CashReceive entry;
            decimal _prevCashRecvAmount = 0;
            int prevCusomerID = 0;
            if (cashReceive.CashReceiveID == 0)
            {
                entry = new CashReceive();
            }

            else
            {
                entry = cashReceiveRepository.CashReceives.SingleOrDefault(a=>a.CashReceiveID==cashReceive.CashReceiveID);
                 prevCusomerID = entry.Customer.CustomerID;
                _prevCashRecvAmount = entry.Amount;

                if ((DateTime.Today - entry.ReceiveDate).TotalDays > 5 && !entry.IsAllowEdit)
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

            PaymentMedium pay = paymentMediumRepository.PaymentMediums.SingleOrDefault(a => a.PaymentMediumID == cashReceive.PaymentMedium.PaymentMediumID && a.Name.ToUpper() == "CASH");

            if (pay == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid payment medium" });
            }
            entry.PaymentMedium = pay;
            Customer cust = customerRepository.Customers.SingleOrDefault(a => a.CustomerID == cashReceive.Customer.CustomerID);

            if (cust == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Customer" });
            }
            entry.Customer = cust;
            entry.Amount = cashReceive.Amount;
            entry.Note = cashReceive.Note;
            entry.ReceiveDate = cashReceive.ReceiveDate;
            if (entry.CashReceiveID == 0)
            {
                entry.EntryDate = DateTime.Now;
            }

            entry.LastUpdateTime = DateTime.Now;
            ResultObj obj = cashReceiveRepository.SaveCashReceive(entry, prevCusomerID,_prevCashRecvAmount);
            if (obj.ResultID == 1)
            {
                DateTime last7Days = DateTime.Now.AddDays(-7);
                obj.Obj= cashReceiveRepository.CashReceivesDetails.Where(a => a.ReceiveDate >= last7Days).OrderByDescending(a => a.InvoicNoSL).ToList();
            }
            return Json(obj);
        }

        public JsonResult GetCashReceive([FromBody] CashReceive cashReceive)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
           
            CashReceive sucashReceive = cashReceiveRepository.CashReceives.SingleOrDefault(a=>a.CashReceiveID==cashReceive.CashReceiveID);
           
            obj.Obj = sucashReceive;
            return Json(obj);
        }

        public JsonResult DeleteCashReceive([FromBody] CashReceive cashReceive)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }


            Company comm = userService.GetCompanyByUser(user.UserId);

            CashReceive entry = cashReceiveRepository.CashReceivesAsNotracking.SingleOrDefault(a=>a.CashReceiveID==cashReceive.CashReceiveID);

            if ((DateTime.Today - entry.ReceiveDate).TotalDays > 5 && !entry.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }

            int prevcustId = entry.Customer.CustomerID;
            decimal prevAmount = entry.Amount;
            ResultObj obj = cashReceiveRepository.DeleteCashReceive(cashReceive, prevcustId, prevAmount);
            if (obj.ResultID == 1)
            {
                obj.Obj = cashReceiveRepository.CashReceivesDetails.OrderByDescending(a => a.InvoicNoSL).ToList();
            }
            return Json(obj);
        }

    }
}
