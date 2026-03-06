using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Man.Models;
using Shop_Man.Models.ViewModels;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Controllers
{
    public class UserPermissionController : Controller
    {


        private IUserService userService;
        private IPagePermissionRepository pagerpermission;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IEmployeeRepository empRepository;


        private ISalesRepository salesRepository;

        private IPurchaseRepository purchaseRepository;

        private IExpensesRepository expensesRepository;

        private ICashPaymentRepository cashPayRepository;

        private ICashReceiveRepository cashReceiveRepository;

        private IAdjustmentRepository adjustRepository;

        private ISalesReturnRepository salesReturnRepository;

        public UserPermissionController(IUserService _userService, IPagePermissionRepository _pagerpermission, IHttpContextAccessor httpContextAccessor, IEmployeeRepository _empRepository, ISalesRepository _salesRepository, IPurchaseRepository _purchaseRepository, IExpensesRepository _expensesRepository, ICashPaymentRepository _caspPayRepository, ICashReceiveRepository _caspReceiveRepository, IAdjustmentRepository _adjustRepository, ISalesReturnRepository _salesReturnRepository)
        {

            userService = _userService;
            _httpContextAccessor = httpContextAccessor;
            pagerpermission = _pagerpermission;
            empRepository = _empRepository;
            salesRepository = _salesRepository;
            purchaseRepository = _purchaseRepository;
            expensesRepository = _expensesRepository;
            cashPayRepository = _caspPayRepository;
            cashReceiveRepository = _caspReceiveRepository;
            adjustRepository = _adjustRepository;
            salesReturnRepository = _salesReturnRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewUser()
        {
            return View();
        }

        public IActionResult EditPermission()
        {
            return View();
        }
        public JsonResult ADDPermission([FromBody] List<PermittedController> controllers)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];

            if (user == null)
            {
                return Json(new ResultObj
                {
                    ResultID = -1,
                    ResultMessage = "Session Expired, Please login Again"
                });
            }

            if (controllers == null || controllers.Count == 0)
            {
                return Json(new ResultObj
                {
                    ResultID = -1,
                    ResultMessage = "No Permission Data"
                });
            }

            int userId = controllers[0].UserId;

            ResultObj obj = userService.ADDPermission(controllers, userId);

            return Json(obj);
        }
        //public JsonResult ADDPermission([FromBody] List<PermittedController> controllers)
        //{
        //    var user = (User)_httpContextAccessor.HttpContext.Items["User"];
        //    if (user == null)
        //    {

        //        return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
        //    }


        //    User user2 = new User();
        //    int userId = 0;
        //    if (controllers != null)
        //    {
        //        if (controllers.Count > 0)
        //        {
        //            userId = controllers[0].UserId;
        //        }
        //    }
        //   // var _user = userService.Users.AsNoTracking().FirstOrDefault(a => a.UserId == user2.UserId);


        //    //foreach (var item in controllers)
        //    //{
        //    //    item.User = null;
        //    //    item.UserId = userId;

        //    //    //if (item.ProjController != null)
        //    //    //{
        //    //    //    context.Attach(item.ProjController);
        //    //    //}
        //    //}

        //    //supplier.Company = userService.GetCompanyByUser(1);
        //    //supplier.UserId = user.UserId;
        //    ResultObj obj = userService.ADDPermission(controllers, userId);
        //    // obj.Obj = userService.ADDPermission(controllers);
        //    return Json(obj);
        //}

        public JsonResult AddUser([FromBody] User model)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }



            Employee emp = empRepository.FindEmployee(model.EmpId);
            if (emp != null)
            {
                model.FullName = emp.Name;
            }
            //supplier.Company = userService.GetCompanyByUser(1);

            Company com = userService.GetCompanyByUser(user.UserId);
            UserToCompany userToCom = new UserToCompany();
            userToCom.Id = 0;
            userToCom.Company = com;
            userToCom.User = model;


            //supplier.UserId = user.UserId;
            ResultObj obj = userService.AddUser(model, userToCom);
            if (obj.ResultID == 1)
            {
                obj.Obj = userService.GetAll();
            }

            return Json(obj);
        }

        public JsonResult GetAllUser()
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            //supplier.Company = userService.GetCompanyByUser(1);
            //supplier.UserId = user.UserId;

            // obj.Obj = userService.ADDPermission(controllers);
            return Json(userService.GetAll().ToList());
        }

        public JsonResult GetAllPages()
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //supplier.Company = userService.GetCompanyByUser(1);
            //supplier.UserId = user.UserId;

            // obj.Obj = userService.ADDPermission(controllers);
            return Json(pagerpermission.ProjControllers.ToList());
        }

        public JsonResult GetAuser([FromBody] User model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            //supplier.Company = userService.GetCompanyByUser(1);
            //supplier.UserId = user.UserId;
            obj.ResultID = 1;
            obj.Obj = userService.GetById(model.UserId);
            return Json(obj);
        }

        public JsonResult GetUserPermittedPages([FromBody] User model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            List<PermittedController> finalPermittedList = new List<PermittedController>();

            //supplier.Company = userService.GetCompanyByUser(1);
            //supplier.UserId = user.UserId;
            List<PermittedController> rawObjList = pagerpermission.PermittedControllers.Where(a => a.UserId == model.UserId).ToList();

            foreach (PermittedController item in rawObjList)
            {
                PermittedController _obj = new PermittedController { PermittedControllerID = item.PermittedControllerID, ProjController = item.ProjController, User = item.User, UserId = item.UserId, PermittedProjActionsList = new List<PermittedProjAction>() };

                if (item.PermittedProjActions != null)
                {
                    foreach (PermittedProjAction item2 in item.PermittedProjActions)
                    {
                        if (item2.UserId == model.UserId)
                        {
                            _obj.PermittedProjActionsList.Add(item2);
                        }
                        finalPermittedList.Add(_obj);

                    }
                }
            }



            obj.Obj = new { AllPages = pagerpermission.ProjControllers.ToList(), PermittedPages = finalPermittedList };
            return Json(obj);
        }


        public JsonResult GetEditDateForPerimission([FromBody] EditPermissionObj model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            List<EditPermissionObj> list = new List<EditPermissionObj>();

            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            if (model.Type == "SALES")
            {
                List<SalesHead> _data = new List<SalesHead>();
                if (model.IsSearchByDate)
                {
                     _data = salesRepository.SalesHeads.Where(a => a.SalesDate == model.dDate).ToList();                
                }
                else
                {
                     _data = salesRepository.SalesHeads.Where(a => a.GeneratedSalesNo == model.ChallanNo).ToList();
                }

                foreach (var item in _data)
                {
                    list.Add(new EditPermissionObj { ChallanID = item.SalesHeadID, ChallanNo = item.GeneratedSalesNo, dDate = item.SalesDate, Type = model.Type, IsAllowEdit = item.IsAllowEdit });
                }
            }
            else if (model.Type == "PURCHASE")
            {

                List<PurchaseHead> _data = new List<PurchaseHead>();
                if (model.IsSearchByDate)
                {
                    _data = purchaseRepository.PurchaseHeads.Where(a => a.PurchaseDate == model.dDate).ToList();
                }
                else
                {
                    _data = purchaseRepository.PurchaseHeads.Where(a => a.GeneratedPurchaseHeadNo == model.ChallanNo).ToList();
                }

                foreach (var item in _data)
                {
                    list.Add(new EditPermissionObj { ChallanID = item.PurchaseHeadID, ChallanNo = item.GeneratedPurchaseHeadNo, dDate = item.PurchaseDate, Type = model.Type, IsAllowEdit = item.IsAllowEdit });
                }

            }
            else if (model.Type == "EXPENSES")
            {

                List<Expense> _data = new List<Expense>();
                if (model.IsSearchByDate)
                {
                    _data = expensesRepository.Expenses.Where(a => a.TranDate == model.dDate).ToList();
                }
                else
                {
                    _data = expensesRepository.Expenses.Where(a => a.GeneratedAutoSLNo == model.ChallanNo).ToList();
                }

                foreach (var item in _data)
                {
                    list.Add(new EditPermissionObj { ChallanID = item.ExpenseID, ChallanNo = item.GeneratedAutoSLNo, dDate = item.TranDate, Type = model.Type, IsAllowEdit = item.IsAllowEdit });
                }


            }
            else if (model.Type == "CASHPAYMENT")
            {
                List<CashPayment> _data = new List<CashPayment>();
                if (model.IsSearchByDate)
                {
                    _data = cashPayRepository.CashPayments.Where(a => a.PaymentDate == model.dDate).ToList();
                }
                else
                {
                    _data = cashPayRepository.CashPayments.Where(a => a.InvoicNo == model.ChallanNo).ToList();
                }

                foreach (var item in _data)
                {
                    list.Add(new EditPermissionObj { ChallanID = item.CashPaymentID, ChallanNo = item.InvoicNo, dDate = item.PaymentDate, Type = model.Type, IsAllowEdit = item.IsAllowEdit });
                }

            }
            else if (model.Type == "CASHRECEIVE")
            {

                List<CashReceive> _data = new List<CashReceive>();
                if (model.IsSearchByDate)
                {
                    _data = cashReceiveRepository.CashReceives.Where(a => a.ReceiveDate == model.dDate).ToList();
                }
                else
                {
                    _data = cashReceiveRepository.CashReceives.Where(a => a.InvoicNo == model.ChallanNo).ToList();
                }

                foreach (var item in _data)
                {
                    list.Add(new EditPermissionObj { ChallanID = item.CashReceiveID, ChallanNo = item.InvoicNo, dDate = item.ReceiveDate, Type = model.Type, IsAllowEdit = item.IsAllowEdit });
                }

            }
            else if (model.Type == "ADJUST")
            {
                List<Adjustment> _data = new List<Adjustment>();
                if (model.IsSearchByDate)
                {
                    _data = adjustRepository.Adjustments.Where(a => a.PaymentDate == model.dDate).ToList();
                }
                else
                {
                    _data = adjustRepository.Adjustments.Where(a => a.InvoicNo == model.ChallanNo).ToList();
                }

                foreach (var item in _data)
                {
                    list.Add(new EditPermissionObj { ChallanID = item.AdjustmentID, ChallanNo = item.InvoicNo, dDate = item.PaymentDate, Type = model.Type, IsAllowEdit = item.IsAllowEdit });
                }

            }

            else if (model.Type == "SALESRETURN")
            {
                List<SalesReturn> _data = new List<SalesReturn>();
                if (model.IsSearchByDate)
                {
                    _data = salesReturnRepository.SalesReturns.Where(a => a.ReturnDate == model.dDate).ToList();
                }
                else
                {
                    _data = salesReturnRepository.SalesReturns.Where(a => a.GeneratedReturnNo == model.ChallanNo).ToList();
                }

                foreach (var item in _data)
                {
                    list.Add(new EditPermissionObj { ChallanID = item.SalesReturnID, ChallanNo = item.GeneratedReturnNo, dDate = item.ReturnDate, Type = model.Type, IsAllowEdit = item.IsAllowEdit });
                }

            }

            else
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Type" });
            }
            obj.ResultID = 1;
            obj.Obj = list;
            return Json(obj);
        }


        public JsonResult UpdateEditPermissionBlock([FromBody] EditPermissionObj model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
           

            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            obj = pagerpermission.UpdateEditPermissionBlock(model);

            return Json(obj);

        }


        public JsonResult UpdateEditPermissionUnBlock([FromBody] EditPermissionObj model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];


            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            obj = pagerpermission.UpdateEditPermissionUnBlock(model);

            return Json(obj);

        }

    }



}
