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


    public class ExpensesController : Controller
    {

        private IExpensesRepository expensesRepository;
        private IEmployeeRepository employeeRepository;

        private IUserService userService;

        private readonly IHttpContextAccessor httpContextAccessor;
        private IDayCloseRepository dayCloseEntry;
        public ExpensesController(IExpensesRepository _expensesRepository, IUserService _userService, IHttpContextAccessor _httpContextAccessor, IEmployeeRepository _employeeRepository, IDayCloseRepository _dayCloseEntry)
        {
            expensesRepository = _expensesRepository;
            userService = _userService;
            httpContextAccessor = _httpContextAccessor;

            employeeRepository = _employeeRepository;
            dayCloseEntry = _dayCloseEntry;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExpensesEntry()
        {
            return View();
        }

       
        public JsonResult ExpensesEntryViewModel()
        {
            ExpensesEntryViewModel obj = new ExpensesEntryViewModel();
            obj.ExpensesHeads = expensesRepository.ExpensesHeads.ToList();
            obj.Expenses = expensesRepository.Expenses.Where(a=>a.TranDate.Year==DateTime.Today.Year && a.TranDate.Month == DateTime.Today.Month).ToList();
            obj.Employees = employeeRepository.Employees.ToList();
            return Json(obj);
        }


        public JsonResult LoadCurrentMonthExpensesData([FromBody] Expense expense)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            ResultObj obj = new ResultObj();
            obj.Obj = expensesRepository.Expenses.Where(a => a.TranDate.Year == expense.TranDate.Year && a.TranDate.Month == expense.TranDate.Month).ToList();
            return Json(obj);
        }

        public JsonResult SaveExpenses([FromBody] Expense expense)
        {
            Expense entry;
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            // incase of Edit


            decimal prevAmount = 0;


            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == expense.TranDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update Not Possible !!" });
            //}
            if (expense.ExpenseID == 0)
            {
                entry = new Expense();
            }

            else
            {



                entry = expensesRepository.FindExpense( expense.ExpenseID);

                prevAmount = entry.Amount;
                if ((DateTime.Today - entry.TranDate).TotalDays > 5 && !entry.IsAllowEdit)
                {
                    return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
                }
            }
            ExpensesHead head;
            Employee employee=new Employee();
            if (expense.IsEmployeeExpenses)
            {
                head = expensesRepository.ExpensesHeads.FirstOrDefault(a => a.IsEmployeeExpenseHead == true);
                employee = employeeRepository.Employees.SingleOrDefault(a=>a.EmployeeID==expense.Employee.EmployeeID);

                decimal _totalExpensesInMointh = employeeRepository.SP_Employee_Salary_and_Expenses_SingleEmployee( expense.Employee.EmployeeID ).BalanceAmount;
                if (_totalExpensesInMointh- prevAmount + expense.Amount > employee.MaxSalaryLimit)
                {
                    return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Expenses Limit" });
                }
            
            }
            else
            {
                head = expensesRepository.ExpensesHeads.SingleOrDefault(a => a.ExpensesHeadID == expense.ExpensesHead.ExpensesHeadID);
            }


            if (head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Expenses Head Name" });
            }
            if (expense.IsEmployeeExpenses && employee == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Employee" });
            }

            Company comp = userService.GetCompanyByUser(1);
            entry.Employee = expense.IsEmployeeExpenses==true? employee:null;
            
            entry.Company = comp;
            entry.User = user;
            entry.Amount = expense.Amount;
            entry.ExpensesHead = head;
            entry.Note =  expense.Note;
            entry.TranDate = expense.TranDate;
            entry.Type = expense.Type;
            entry.IsEmployeeExpenses = expense.IsEmployeeExpenses;
            ResultObj obj = expensesRepository.SaveExpense(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = expensesRepository.Expenses.Where(a => a.TranDate.Year == expense.TranDate.Year).OrderByDescending(a => a.ExpenseID).ToList();
            }
            return Json(obj);
        }


        public JsonResult DeleteExpenses([FromBody] Expense expense)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];

            Expense dbEntry = expensesRepository.Expenses
             .FirstOrDefault(p => p.ExpenseID == expense.ExpenseID);

            //if (dayCloseEntry.DayCloses.Any(a => a.dDate == dbEntry.TranDate))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Day Closes, Entry/Update/Delete Not Possible !!" });
            //}

            if ((DateTime.Today - expense.TranDate).TotalDays > 5 && !expense.IsAllowEdit)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Maximum Edit Days 5 Days  Over ." });
            }
            ResultObj obj = expensesRepository.DeleteExpense(dbEntry);
            obj.Obj = obj.Obj = expensesRepository.Expenses.Where(a => a.TranDate.Year == expense.TranDate.Year ).OrderByDescending(a => a.ExpenseID).ToList(); ;
            return Json(obj);
        }


        public JsonResult GetExpenses([FromBody] Expense expense)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];


            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            Expense emp = expensesRepository.FindExpense(expense.ExpenseID);
            obj.ResultID = 1;
            obj.Obj = emp;
            obj.ResultMessage = "";
            return Json(obj);
        }



        public JsonResult SaveExpensesHead([FromBody] ExpensesHead expensesHead)
        {
            ExpensesHead entry;
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            if (expensesHead.ExpensesHeadID == 0)
            {
                entry = new ExpensesHead();
            }

            else
            {
                entry = expensesRepository.ExpensesHeads.SingleOrDefault(a => a.ExpensesHeadID == expensesHead.ExpensesHeadID);
            }

            Company comp = userService.GetCompanyByUser(1);
            entry.Company = comp;
            entry.User = user;
            entry.Name = expensesHead.Name;

            ResultObj obj = expensesRepository.SaveExpensesHead(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = expensesRepository.ExpensesHeads.OrderByDescending(a => a.ExpensesHeadID).ToList();
            }
            return Json(obj);
        }


        public JsonResult DeleteExpensesHead([FromBody] ExpensesHead expensesHead)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            ResultObj obj = expensesRepository.DeleteExpensesHead(expensesHead);
            obj.Obj = expensesRepository.ExpensesHeads.ToList();
            return Json(obj);
        }

        public JsonResult GetExpensesHead([FromBody] ExpensesHead expensesHead)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ExpensesHead desig = expensesRepository.FindExpensesHead(expensesHead.ExpensesHeadID);
            return Json(desig);
        }


        public IActionResult Reports()
        {
            return View();
        }


        public JsonResult ExpensesEntryViewModelReports()
        {
            ExpensesEntryViewModel obj = new ExpensesEntryViewModel();
            obj.ExpensesHeads = expensesRepository.ExpensesHeads.ToList();
         
            return Json(obj);
        }


        public IActionResult ExpensesReport(string fromDate, string toDate, string customerIDs, string IsDetails, string type,string recvPayType)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Expenses Report ";


            return View();
        }


        public JsonResult GetExpensesReport([FromBody] ExpensesReportsViewModel model)
        {


            ResultObj obj = new ResultObj();
            List<SP_Expenses_Datewise> list = new List<SP_Expenses_Datewise>();
            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Details Expenses Report, ";
            if (model.IsDetails == "0")
            {
                title = "Summary Expense Report, ";
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
            if (model.ExpensesHeadIDs != "0")
            {
                List<int> custIDS = model.ExpensesHeadIDs.Split(",").Select(a => int.Parse(a)).ToList();

                if (model.IsDetails != "0")
                {
                    // list = expensesRepository.Expenses.Where(a => a.TranDate >= _fromDate && a.TranDate <= _toDate && custIDS.Contains(a.ExpensesHead.ExpensesHeadID)).ToList();

                    list = expensesRepository.SP_Expenses_Datewise(_fromDate,_toDate, model.ExpensesHeadIDs);
                }

                else
                {
                    // list = expensesRepository.Expenses.Where(a => a.TranDate >= _fromDate && a.TranDate <= _toDate && custIDS.Contains(a.ExpensesHead.ExpensesHeadID)).ToList();
                    list = expensesRepository.SP_Expenses_Datewise(_fromDate, _toDate, model.ExpensesHeadIDs);
                }
            }

            else
            {
                if (model.IsDetails != "0")
                {
                    //  list = list = expensesRepository.Expenses.Where(a => a.TranDate >= _fromDate && a.TranDate <= _toDate ).ToList();
                    list = expensesRepository.SP_Expenses_Datewise(_fromDate, _toDate, model.ExpensesHeadIDs);
                }
                else
                {
                    // list = expensesRepository.Expenses.Where(a => a.TranDate >= _fromDate && a.TranDate <= _toDate ).ToList();
                    list=expensesRepository.SP_Expenses_Datewise(_fromDate, _toDate, model.ExpensesHeadIDs);
                }
            }
            obj.ResultNo = title;
            obj.Obj = list;
            return Json(obj);
        }
    }
}
