using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Man.Infrastructure;
using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using Shop_Man.Models.ViewModels;
using Shop_Man.Repository;

namespace Shop_Man.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerCategory customerCategoryRepo;
        private ICustomerSubCategory customerSubCategoryRepo;
        private ICustomerRepository customerRepository;
        private IUserService userService;
        private IDistrictRepository districtRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomerController(ICustomerCategory _customerCategoryRepo, ICustomerSubCategory _customerSubCategoryRepo, ICustomerRepository _customerRepository, IUserService _userService, IHttpContextAccessor httpContextAccessor, IDistrictRepository _districtRepository)
        {
            customerCategoryRepo = _customerCategoryRepo;
            customerSubCategoryRepo = _customerSubCategoryRepo;
            customerRepository = _customerRepository;
            userService = _userService;
            _httpContextAccessor = httpContextAccessor;
            districtRepository = _districtRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CustomerEntry()
        {
            return View();
        }
        public IActionResult CustomerLock()
        {
            return View();
        }

        public IActionResult CustomerBalanceLimitEntry()
        {
            return View();
        }

        public JsonResult AddCustomer([FromBody] Customer customer)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            customer.Company = userService.GetCompanyByUser(1);
            customer.UserId = user.UserId;
            customer.IsLocked = true;
            ResultObj obj = customerRepository.SaveCustomer(customer);
            obj.Obj = customerRepository.Customers.ToList();
            return Json(obj);
        }
        public JsonResult UpdateCustomerLimit([FromBody] Customer customer)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            customer.Company = userService.GetCompanyByUser(1);
            customer.UserId = user.UserId;
            ResultObj obj = customerRepository.UpdateCustomerLimit(customer);
            obj.Obj = customerRepository.Customers.ToList();
            return Json(obj);
        }

        public JsonResult LockUnlockCustomer([FromBody] Customer customer)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            customer.Company = userService.GetCompanyByUser(1);
            customer.UserId = user.UserId;
            ResultObj obj = customerRepository.LockUnlockCustomer(customer);
            obj.Obj = customerRepository.Customers.ToList();
            return Json(obj);
        }

        public JsonResult DeleteCustomer([FromBody] Customer customer)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            customer.Company = userService.GetCompanyByUser(1);
            customer.UserId = user.UserId;
            ResultObj obj = customerRepository.DeleteCustomer(customer);
            obj.Obj = customerRepository.Customers.ToList();
            return Json(obj);
        }

        public JsonResult GetCustomer([FromBody] Customer customer)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            Customer cust = customerRepository.GetCustomer(customer.CustomerID);

            return Json(cust);
        }
        //   custPrevBalance= customerRepository.GetCustomerPreviousBalance(salesHead.Customer.CustomerID);
        public JsonResult GetCustomerCurrentBalalce([FromBody] Customer customer)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;

            decimal currBal = customerRepository.GetCustomerPreviousBalance(customer.CustomerID);
            customer.CurrentBalance = currBal;
            return Json(customer);
        }
        public JsonResult GetCustomers()
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            List<Customer> custs = customerRepository.Customers.ToList();

            return Json(custs);
        }


        [Authorize]
        public JsonResult CustomerEntryViewModel()
        {

            CustomerEntryViewModel model = new CustomerEntryViewModel();

            model.Customers = customerRepository.Customers.ToList();
            model.CustomerCategorys = customerCategoryRepo.CustomerCategorys.ToList();
            model.CustomerSubCategorys = customerSubCategoryRepo.CustomerSubCategorys.ToList();
            model.DistrictS = districtRepository.District.ToList();
            return Json(model);
        }


        [Authorize]
        public JsonResult CustomerSubcategory()
        {

            CustomerEntryViewModel model = new CustomerEntryViewModel();

    
            model.CustomerSubCategorys = customerSubCategoryRepo.CustomerSubCategorys.ToList();

            return Json(model);
        }


        // customer Balance

        public IActionResult CustomerReport()
        {
            List<int> _obj = customerRepository.YearCloseCustomerSummList.Select(a => a.YearName).Distinct().OrderBy(x=>x).ToList();
            return View(_obj);
        }

        public IActionResult CustomerBalance(string fromDate, string toDate, string type, string customerIDs)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
            if (int.Parse(customerIDs) == 0)
            {
                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "Sales Report ";
                TempData["CustName"] = "";
                TempData["ShopName"] = "";
                TempData["Address"] = "";
            }
            else
            {
                Customer cust = customerRepository.GetCustomer(int.Parse(customerIDs));

                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "Sales Report ";
                TempData["CustName"] = cust.Name;
                TempData["ShopName"] = cust.ShopName;
                TempData["Address"] = cust.Address1;
            }
            return View();
        }
        public JsonResult GetPartyBalanceReport([FromBody] SalesReportViewModel model)
        {

            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Session Expired " }); ;
            }
            List<SP_CustomerBalance_Details> list = new List<SP_CustomerBalance_Details>();
            List<SP_CustomerBalance_Datewise> list2 = new List<SP_CustomerBalance_Datewise>();
            List<SP_CustomerBalance_ALL> list3 = new List<SP_CustomerBalance_ALL>();
            DateTime _fromDate = model.FromDate;
            DateTime _toDate = model.ToDate;

            string title = "Details Party Balance Report, ";
          


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
            else if (model.Type == "all")
            {
                
                title += " "; ;

            }
            else
            {

                title += "From " + String.Format("{0:dd-MMM-yyyy}", _fromDate) + " To " + String.Format("{0:dd-MMM-yyyy}", _toDate) + " (Datewise)"; ;

            }
            //List<string> custIDS;
            //            List<Order> AllOrderInBills = repositoryOrder.OrdersWithReceiveMain.Where(a => orderIDsInBills.Contains(a.OrderID)).ToList();


            if (int.Parse(model.CustomerIds) == 0){
                list3 = customerRepository.CustomerBalanceALL();
                title += " Current Customer Balance  "; 
                obj.Obj = list3;
            }
            else {
                if (model.Type == "all")
                {
                    list = customerRepository.CustomerBalanceDetailsList(int.Parse(model.CustomerIds));
                    obj.Obj = list;
                }
                else
                {
                    list2 = customerRepository.DatewiseCustomerBalanceList(int.Parse(model.CustomerIds), _fromDate, _toDate);
                    obj.Obj = list2;
                }
            }
            obj.ResultNo = title;
           
            return Json(obj);
        }
//---Customer Closing---------------------

        public IActionResult CustomerYearClosing()
        {
            return View();
        }


        public IActionResult CustomerYearClosingViewCurrent()
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);

            TempData["CompanyName"] = comp.Name;
            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "List of Customers  ";

            TempData["ShopName"] = "";
            TempData["Address"] = "";


            return View();
        
        }
        public JsonResult CustomersWithLastClosingYearList()
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;

            List< SP_CustomersWithLastClosingYear> result = customerRepository.CustomersWithLastClosingYearList();
          
            return Json(result);
        }


        public JsonResult GetCustomerYearCloseNew([FromBody] YearCloseCustomerSumm model)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com= userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            model.YearName = model.YearCloseDate.Year;
          ResultObj result = customerRepository.Get_SP_Entry_Customer_Year_Close(user.UserId,com.CompanyID, model.CustomerID, model.YearName,model.YearCloseDate);

            return Json(result);
        }

        public JsonResult GetLastCustomerYearClose([FromBody] YearCloseCustomerSumm model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            int? maxDate = customerRepository.YearCloseCustomerSummList.Where(a=>a.CustomerID==model.CustomerID).Max(a =>(int ?) a.YearName);

            YearCloseCustomerSumm _obj = customerRepository.YearCloseCustomerSummList.Where(a => a.YearName == maxDate && a.CustomerID==model.CustomerID).SingleOrDefault();
            obj.Obj = _obj;
            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            return Json(obj);
        }



        public JsonResult SaveCustomerYearClose([FromBody] YearCloseCustomerSumm model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            model.YearName = model.YearCloseDate.Year;

            ResultObj _obj = customerRepository.SaveCustomerYearClose(user.UserId, com.CompanyID, "A ", model.CustomerID, model.YearName, model.YearCloseDateFormated);

            if (_obj.ResultID == 1)
            {

               _obj.Obj2 = customerRepository.CustomersWithLastClosingYearList();
            }
           

            return Json(_obj);
        }


        public JsonResult GetExistingCustomerYearClose([FromBody] YearCloseCustomerSumm model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }



            YearCloseCustomerSumm _obj = customerRepository.YearCloseCustomerSummList.Where(a => a.YearName == model.YearName && a.CustomerID==model.CustomerID).SingleOrDefault();

            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            obj.Obj = _obj;

            return Json(obj);
        }



        public JsonResult GetExistingCustomerYearCloseList([FromBody] YearCloseCustomerSumm model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }



            List<YearCloseCustomerSumm> _obj = customerRepository.YearCloseCustomerSummList.Where(a => a.CustomerID == model.CustomerID).OrderBy(a => a.YearCloseDate).ToList();

            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            obj.Obj = _obj;

            return Json(obj);
        }
      // 

        public IActionResult CustomerClosingList(string customerID)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
            if (int.Parse(customerID) == 0)
            {
                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "Sales Report ";
                TempData["CustName"] = "";
                TempData["ShopName"] = "";
                TempData["Address"] = "";
            }
            else
            {
                Customer cust = customerRepository.GetCustomer(int.Parse(customerID));

                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "Sales Report ";
                TempData["CustName"] = cust.Name;
                TempData["ShopName"] = cust.ShopName;
                TempData["Address"] = cust.Address1;
            }
            return View();
        }


        public IActionResult CustomerClosingInAYear(string yearName)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
           
                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "List of Customers  Closing  in " + yearName;
          
                TempData["ShopName"] = "";
                TempData["Address"] = "";
            
         
            return View();
        }



        public IActionResult CustomerClosingInAYearAllCustomer(string yearName)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);

            TempData["CompanyName"] = comp.Name;
            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "All Customer Year Closing  in :" + yearName;

            TempData["ShopName"] = "";
            TempData["Address"] = "";


            return View();
        }


        public JsonResult GetExistingCustomerYearCloseListInAYear([FromBody] YearCloseCustomerSumm model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }



            List<View_Customer_And_YearClosing> _obj = customerRepository.Get_Customer_And_YearClosing(model.YearName);

            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            obj.Obj = _obj;

            return Json(obj);
        }
        public IActionResult CustomerClosingDetails(string customerID, string yearName)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
            if (int.Parse(customerID) == 0)
            {
                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "Customer Year Closing Details in " + yearName;
                TempData["CustName"] = "";
                TempData["ShopName"] = "";
                TempData["Address"] = "";
                TempData["yearName"] = yearName;
            }
            else
            {
                Customer cust = customerRepository.GetCustomer(int.Parse(customerID));

                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
              
                TempData["Title"] = "Customer Year Closing Details in " + yearName;
                
                TempData["CustName"] = cust.Name;
                TempData["ShopName"] = cust.ShopName;
                TempData["Address"] = cust.Address1;
                TempData["yearName"] = yearName;
            }
            return View();
        }

        public IActionResult CustomerClosingViewCurrentDate(string customerID, string yearName, string _type , string dDate )
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
            if (int.Parse(customerID) == 0)
            {
                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "Customer Year Closing Details in " + yearName;
                TempData["CustName"] = "";
                TempData["ShopName"] = "";
                TempData["Address"] = "";
                TempData["yearName"] = yearName;
            }
            else
            {
                Customer cust = customerRepository.GetCustomer(int.Parse(customerID));

                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                
                TempData["Title"] = "Customer Year Closing Details up to " + dDate;
                
              
                TempData["CustName"] = cust.Name;
                TempData["ShopName"] = cust.ShopName;
                TempData["Address"] = cust.Address1;
                TempData["yearName"] = yearName;
            }
            return View();
        }


        public IActionResult View_ALL_Customer_Year_Close( string dDate)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comp = userService.GetCompanyByUser(user.UserId);
          
                TempData["CompanyName"] = comp.Name;
                TempData["CompanyAddress"] = comp.Address;
                TempData["Title"] = "All Customer Year Closing  in " + dDate;
                TempData["CustName"] = "";
                TempData["ShopName"] = "";
                TempData["Address"] = "";
                TempData["yearName"] = dDate;
            
           
            return View();
        }



        public JsonResult GetExistingCustomerYearCloseDetails([FromBody] View_Cust_Close model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }


            List<YearCloseCustomerDetails> _obj = customerRepository.YearCloseCustomerDetailsList.Where(a => a.Customer.CustomerID == model.CustomerID && a.YearCloseDate.Year == model.YearName).OrderBy(a => a.dDate).ToList();


            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            obj.Obj = _obj;

            return Json(obj);
        }



        public JsonResult GetCustomerYearCloseDetailsCurrentDate([FromBody] View_Cust_Close model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            List<SP_View_Customer_Year_Close> _obj = customerRepository.Get_SP_View_Customer_Year_Close(model.CustomerID, model.YearName, model.dDate);


            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }

            obj.Obj = _obj;

            return Json(obj);
        }


        public JsonResult Get_SP_View_ALL_Customer_Year_Close([FromBody] View_Cust_Close model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            List<SP_View_ALL_Customer_Year_Close> _obj = customerRepository.Get_SP_View_ALL_Customer_Year_Close(model.dDate);


            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }

            obj.Obj = _obj;

            return Json(obj);
        }


        public JsonResult Get_SP_View_ALL_Customer_Year_Close_Previous([FromBody] View_Cust_Close model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

        

            
                List<View_Customer_And_YearClosing> _obj = customerRepository.Get_Customer_And_YearClosing(model.YearName);
                obj.Obj = _obj;

            

            if (obj.Obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }



            return Json(obj);
        }



        public JsonResult Get_SP_Customer_Close_SizeDetails([FromBody] View_Cust_Close model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            List<SP_Customer_Close_SizeDetails> _obj = new List<SP_Customer_Close_SizeDetails>();

         
            _obj = customerRepository.Get_SP_Customer_Close_SizeDetails(model.YearName, model.CustomerID);
            
            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            obj.Obj = _obj;

            return Json(obj);
        }




        public JsonResult Get_SP_Customer_Close_SizeDetails_Unprocessed([FromBody] View_Cust_Close model)
        {
            ResultObj obj = new ResultObj();
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company com = userService.GetCompanyByUser(user.UserId);
            if (user == null)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }

            List<SP_Customer_Close_SizeDetails> _obj = new List<SP_Customer_Close_SizeDetails>();

           
            _obj = customerRepository.Get_SP_Customer_Close_SizeDetails_Unprocessed(model.YearName, model.CustomerID, model.dDate);

            
          
            if (_obj == null)
            {

                obj.ResultMessage = "No Data Found";
            }
            obj.Obj = _obj;

            return Json(obj);
        }


    }


    public class View_Cust_Close
    {
        public int CustomerID { get; set; }

        public int YearName { get; set; }
        public string Type { get; set; }

        public string dDate { get; set; }

    }
}
