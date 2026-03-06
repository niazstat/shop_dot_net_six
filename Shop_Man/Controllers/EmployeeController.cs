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
    public class EmployeeController : Controller
    {

        private IEmployeeRepository employeeRepository;
      
        private IUserService userService;
     
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISalarySheetRepository salarySheetRepo;
        public EmployeeController(IEmployeeRepository _employeeRepository, IUserService _userService, IHttpContextAccessor _httpContextAccessor, ISalarySheetRepository _salarySheetRepo)
        {
            employeeRepository = _employeeRepository;
              userService = _userService;
            httpContextAccessor = _httpContextAccessor;
            salarySheetRepo = _salarySheetRepo;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewEmployee()
        {
            return View();
        }

        public IActionResult EmployeeExpensesLimitEntry()
        {
            return View();
        }


        public JsonResult GetEmployeeEntryViewModel()
        {
            EmployeeEntryViewModel obj = new EmployeeEntryViewModel();
            obj.Designations = employeeRepository.Designations.ToList();
            obj.Employees = employeeRepository.Employees.ToList();
            return Json(obj);
        }
        public JsonResult GetAllEmployees()
        {
         
            return Json(employeeRepository.Employees.ToList());
        }

        public JsonResult SaveEmployee([FromBody] Employee employee)
        {
            Employee entry;
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            if (employee.EmployeeID == 0)
            {
                entry = new Employee();
            }

            else
            {
                entry = employeeRepository.Employees.SingleOrDefault(a => a.EmployeeID == employee.EmployeeID);
            }

            Designation designation = employeeRepository.Designations.SingleOrDefault(a => a.DesignationID == employee.Designation.DesignationID);



            if (designation == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Designation" });
            }


            Company comp = userService.GetCompanyByUser(user.UserId);
            entry.Company = comp;
           entry.User = user;
            entry.BloodGroup = employee.BloodGroup;
            entry.CurrentAddress = employee.CurrentAddress;
            entry.DateOfBirth = employee.DateOfBirth;
            entry.Designation = designation;
            entry.District = employee.District;

            entry.Division = employee.Division;

            entry.FatherName = employee.FatherName;
            entry.IsLedgerClose = employee.IsLedgerClose;
            entry.JoiningDate = employee.JoiningDate;
            entry.MaleFemale = employee.MaleFemale;
            entry.Mobile1 = employee.Mobile1;
            entry.MotherName = employee.MotherName;
            entry.Name = employee.Name;
            entry.Village = employee.Village;
            entry.NID = employee.NID;
            entry.PermanentAddress = employee.PermanentAddress;
            entry.Religion = employee.Religion;
            entry.Salary = employee.Salary;
            entry.Thana = employee.Thana;
            entry.HouseNo = employee.HouseNo;
            entry.RoadNo = employee.RoadNo;
            ResultObj obj = employeeRepository.SaveEmployee(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = employeeRepository.Employees.OrderByDescending(a => a.EmployeeID).ToList();
            }
            return Json(obj);
        }

        public JsonResult UpdateEmployeeExpnsesLimit([FromBody] Employee employee)
        {
            Employee entry;
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            if (employee.EmployeeID == 0)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Employee" });
            }

            else
            {
                entry = employeeRepository.Employees.SingleOrDefault(a => a.EmployeeID == employee.EmployeeID);
            }

           


            if (entry == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Employee" });
            }


          
            entry.MaxSalaryLimit = employee.MaxSalaryLimit;
            entry.LastUpdateTime = DateTime.Now;
            entry.UpdateUserID = user.UserId;
            ResultObj obj = employeeRepository.SaveEmployee(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = employeeRepository.Employees.OrderByDescending(a => a.EmployeeID).ToList();
            }
            return Json(obj);
        }

        public JsonResult DeleteEmployee([FromBody] Employee employee)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            ResultObj obj = employeeRepository.DeleteEmployee(employee);
            obj.Obj = employeeRepository.Employees.ToList();
            return Json(obj);
        }

        public JsonResult GetEmployee([FromBody] Employee employee)
        {
            ResultObj obj = new ResultObj();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];

          
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            Employee emp = employeeRepository.FindEmployee(employee.EmployeeID);
            obj.ResultID = 1;
            obj.Obj = emp;
            obj.ResultMessage = "";
            return Json(obj);
        }

        public JsonResult GetEmployees()
        {

            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            List<Employee> emps = employeeRepository.Employees.ToList();

            return Json(emps);
        }

        public JsonResult SaveDesignation([FromBody] Designation designation)
        {
            Designation entry;
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            if (designation.DesignationID == 0)
            {
                entry = new Designation();
            }

            else
            {
                entry = employeeRepository.Designations.SingleOrDefault(a => a.DesignationID == designation.DesignationID);
            }

            Company comp = userService.GetCompanyByUser(1);
            entry.Company = comp;
            // entry.User = user;
            entry.Name = designation.Name;
     
            ResultObj obj = employeeRepository.SaveDesignation(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = employeeRepository.Designations.OrderByDescending(a => a.DesignationID).ToList();
            }
            return Json(obj);
        }


        public JsonResult DeleteDesignation([FromBody] Designation designation)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            ResultObj obj = employeeRepository.DeleteDesignation(designation);
            obj.Obj = employeeRepository.Designations.ToList();
            return Json(obj);
        }

        public JsonResult GetDesignation([FromBody] Designation designation)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            Designation desig = employeeRepository.FindDesignation(designation.DesignationID);
            return Json(desig);
        }



        public IActionResult SalaryEntry()
        {
            return View();
        }




        public JsonResult SalaryEntryViewModel()
        {
            SalaryEntryViewModel model = new SalaryEntryViewModel();
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            ResultObj obj = new ResultObj();

            model.Employees = employeeRepository.Employees.ToList();
            model.SalarySheetHeads = salarySheetRepo.SalarySheetHeads.ToList();
            obj.Obj = model;
            return Json(obj);
        }
        public JsonResult LoadSalaryByYearAndName([FromBody] SalarySheet salarySheet)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
        
            ResultObj obj = new ResultObj();

           // obj.Obj = salarySheetRepo.SalarySheets.Where(a => a.YearName == salarySheet.PayDate.Year && a.MonthName == salarySheet.PayDate.ToString("MMM")).ToList();
            obj.Obj = salarySheetRepo.SalarySheets.ToList();
            return Json(obj);
        }

        public JsonResult SaveSalary([FromBody] SalarySheet salarySheet)
        {
            SalarySheet entry;
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            if (salarySheet.SalarySheetID == 0)
            {
                entry = new SalarySheet();
            }

            else
            {
                entry = salarySheetRepo.SalarySheets.SingleOrDefault(a => a.SalarySheetID == salarySheet.SalarySheetID);
            }

            Employee emp = employeeRepository.Employees.SingleOrDefault(a => a.EmployeeID == salarySheet.EmployeeID);



            if (emp == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Employee" });
            }

            SalarySheetHead _head = salarySheetRepo.SalarySheetHeads.SingleOrDefault(a => a.SalarySheetHeadID == salarySheet.SalarySheetHeadID);
            if (_head == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Head Name" });
            }
            //if (salarySheet.SalarySheetID == 0 && salarySheetRepo.SalarySheets.Any(a => a.YearName == salarySheet.YearName && a.MonthName == salarySheet.MonthName && a.EmployeeID == salarySheet.EmployeeID))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Duplicate Entry" });
            //}
            //if (salarySheet.SalarySheetID != 0 && salarySheetRepo.SalarySheets.Any(a => a.YearName == salarySheet.YearName && a.MonthName == salarySheet.MonthName && a.EmployeeID == salarySheet.EmployeeID && a.SalarySheetID != salarySheet.SalarySheetID))
            //{
            //    return Json(new ResultObj { ResultID = -1, ResultMessage = "Duplicate Entry" });
            //}

            Company comp = userService.GetCompanyByUser(user.UserId);
            entry.Company = comp;
            entry.User = user;
            entry.Employee = emp;
            entry.AddAmount = salarySheet.AddAmount;
            entry.SalarySheetHead = _head;
            entry.Amount = salarySheet.Amount;
            entry.DeductAmount = salarySheet.DeductAmount;
            entry.MonthName = salarySheet.MonthName;

            entry.PayDate = salarySheet.PayDate;
                //DateTime.Parse("01-" + salarySheet.MonthName + "-" + salarySheet.YearName.ToString()).AddMonths(1).AddDays(-1);
            entry.YearName = salarySheet.PayDate.Year;
            entry.MonthName = salarySheet.PayDate.ToString("MMM");
            entry.Remarks = salarySheet.Remarks;


            ResultObj obj = salarySheetRepo.SaveSalarySheet(entry);
            if (obj.ResultID == 1)
            {
               // obj.Obj = salarySheetRepo.SalarySheets.Where(a => a.YearName == salarySheet.PayDate.Year && a.MonthName == salarySheet.PayDate.ToString("MMM")).ToList();
                obj.Obj = salarySheetRepo.SalarySheets.ToList();
            }
            return Json(obj);
        }


        public JsonResult DeleteSalarySheet([FromBody] SalarySheet salarySheet)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            if (salarySheet.SalarySheetID == 0)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Invalid Salary Entry" });
            }
            Company comm = userService.GetCompanyByUser(user.UserId);
            ResultObj obj = salarySheetRepo.DeleteSalarySheet(salarySheet);
            if (obj.ResultID == 1)
            {
                //  obj.Obj = salarySheetRepo.SalarySheets.Where(a => a.YearName == salarySheet.PayDate.Year && a.MonthName == salarySheet.PayDate.ToString("MMM")).ToList();

             obj.Obj = salarySheetRepo.SalarySheets.ToList();

            }
            return Json(obj);
        }


        public JsonResult SaveSalarySheetHead([FromBody] SalarySheetHead salarySheetHead)
        {
            SalarySheetHead entry;
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            if (salarySheetHead.SalarySheetHeadID == 0)
            {
                entry = new SalarySheetHead();
            }

            else
            {
                entry = salarySheetRepo.SalarySheetHeads.SingleOrDefault(a => a.SalarySheetHeadID == salarySheetHead.SalarySheetHeadID);
            }

            Company comp = userService.GetCompanyByUser(1);
            entry.Company = comp;
            entry.User = user;
            entry.Name = salarySheetHead.Name;

            ResultObj obj = salarySheetRepo.SaveSalarySheetHead(entry);
            if (obj.ResultID == 1)
            {
                obj.Obj = salarySheetRepo.SalarySheetHeads.OrderByDescending(a => a.SalarySheetHeadID).ToList();
            }
            return Json(obj);
        }


        public JsonResult DeleteSalarySheetHead([FromBody] SalarySheetHead salarySheetHead)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            ResultObj obj = salarySheetRepo.DeleteSalarySheetHead(salarySheetHead);
            obj.Obj = salarySheetRepo.SalarySheetHeads.ToList();
            return Json(obj);
        }

        public JsonResult GetSalarySheetHead([FromBody] SalarySheetHead salarySheetHead)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            SalarySheetHead desig = salarySheetRepo.FindSalarySheetHead(salarySheetHead.SalarySheetHeadID);
            return Json(desig);
        }


        public IActionResult EmplyeeSalaryAndCostReport()
        {
            return View();
        }



        public IActionResult EmplyeeSalaryAndCostReportView(string fromDate, string toDate, string customerIDs, string IsDetails, string type, string recvPayType)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Employee VS Expense Report ";


            return View();
        }



        public JsonResult GetEmplyeeSalaryAndCostReport([FromBody] ExpensesReportsViewModel model)
        {


            ResultObj obj = new ResultObj();
            List<SP_Employee_Salary_and_Expenses> list = new List<SP_Employee_Salary_and_Expenses>();
            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Details Employee and Expenses Report, ";
            if (model.IsDetails == "0")
            {
                title = "Summary  Employee and Expense Report, ";
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
         
               // List<int> custIDS = model.ExpensesHeadIDs.Split(",").Select(a => int.Parse(a)).ToList();

                if (model.IsDetails != "0")
                {
                   
                    list = employeeRepository.SP_Employee_Salary_and_Expenses(_fromDate, _toDate, int.Parse( model.ExpensesHeadIDs));
                }

                else
                {

                    list = employeeRepository.SP_Employee_Salary_and_Expenses(_fromDate, _toDate, int.Parse(model.ExpensesHeadIDs));
                }
            

       
            obj.ResultNo = title;
            obj.Obj = list;
            return Json(obj);
        }

        public IActionResult EmployeeSalaryWithdrawView(string fromDate, string toDate, string customerIDs, string IsDetails, string type, string recvPayType)
        {
            var user = (User)httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            string _filter = "";



            ResultObj obj = new ResultObj();
            List<SP_Employee_Salary_and_Expenses> list = new List<SP_Employee_Salary_and_Expenses>();
            DateTime _fromDate =  DateTime.Parse( fromDate);
            DateTime _toDate = DateTime.Parse(toDate);

            string title = "Salary Withdraw Report, ";
          


            if (type == "day")
            {
                _toDate = DateTime.Today;
                _fromDate = DateTime.Today;

                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Today)";
            }
            else if (type == "week")
            {
                _toDate = DateTime.Today;
                _fromDate = _fromDate.AddDays(-7);
                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Week)";
            }
            else if (type == "month")
            {
                _toDate = DateTime.Today;
                _fromDate = _fromDate.AddMonths(-1);
                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Month)"; ;
            }
            else if (type == "year")
            {
                _toDate = DateTime.Today;
                _fromDate = _fromDate.AddYears(-1);
                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Year)"; ;

            }

            else
            {

                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Datewise)"; ;

            }

            TempData["Title"] = title;
            //List<string> custIDS;
            //            List<Order> AllOrderInBills = repositoryOrder.OrdersWithReceiveMain.Where(a => orderIDsInBills.Contains(a.OrderID)).ToList();

            // List<int> custIDS = model.ExpensesHeadIDs.Split(",").Select(a => int.Parse(a)).ToList();
            

            list = employeeRepository.SP_Employee_Expenses(_fromDate, _toDate, int.Parse(customerIDs));
            
            if (int.Parse(customerIDs) == 0)
            {
                _filter = "All Employees";
            }
            else
            {
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        _filter = "Employee Name : " +list[0].EmployeeName;
                    }
                }
            }

            TempData["Filter"] = _filter;
            return View(list);
        }
    }
}
