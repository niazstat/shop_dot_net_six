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
    public class CompanyProductController : Controller
    {

        private ICustomerCategory customerCategoryRepo;
        private ICustomerSubCategory customerSubCategoryRepo;
        private ISupplierRepository supplierRepository;
        private IUserService userService;
        private IProductsRepositor productRepository;
        private IDayCloseRepository repoDailyClose;
        private ICompanyProductRepository companyProductRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyProductController(ICustomerCategory _customerCategoryRepo, ICustomerSubCategory _customerSubCategoryRepo, ISupplierRepository _supplierRepository, IUserService _userService, IHttpContextAccessor httpContextAccessor, ICompanyProductRepository _companyProductRepository, IProductsRepositor _productRepository, IDayCloseRepository _repoDailyClose)
        {
            customerCategoryRepo = _customerCategoryRepo;
            customerSubCategoryRepo = _customerSubCategoryRepo;
            supplierRepository = _supplierRepository;
            userService = _userService;
            _httpContextAccessor = httpContextAccessor;
            companyProductRepository = _companyProductRepository;
            productRepository = _productRepository;
            repoDailyClose = _repoDailyClose;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CompanyProductEntry()
        {
            return View();
        }

        public JsonResult GetCompanyProductEntryViewModel()
        {

            CompanyProductEntryViewModel obj = new CompanyProductEntryViewModel();
            obj.Articles = productRepository.Articles.ToList();
            obj.ProdCoCategorys = productRepository.ProdCoCategorys.ToList();
            obj.ProdNames = productRepository.ProdNames.ToList();
            obj.ProdTypes = productRepository.ProdTypes.ToList();
            obj.Sizes = productRepository.Sizes.ToList();
            obj.UOMs = productRepository.UOMs.ToList();
            obj.Suppliers = supplierRepository.Suppliers.ToList();
            return Json(obj);
        }


        public JsonResult GetProName([FromBody] ProdName prodName)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ProdName obj = productRepository.ProdNames.SingleOrDefault(a => a.ProdNameID == prodName.ProdNameID);
            return Json(obj);
        }



        public JsonResult SaveProName([FromBody] ProdName prodName)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];

            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            bool isDuplicateArticle = productRepository.ProdNames.Any(a => a.Name.Trim() == prodName.Name.Trim() && a.ProdNameID != prodName.ProdNameID);

            if (isDuplicateArticle)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Duplicate  name!" });
            }



            ResultObj obj = productRepository.SaveProdName(prodName);
            obj.Obj = productRepository.ProdNames.ToList();
            return Json(obj);
        }
        public JsonResult DeleteProName([FromBody] ProdName prodName)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ResultObj obj = productRepository.DeleteProdName(prodName);
            obj.Obj = productRepository.ProdNames.ToList();
            return Json(obj);
        }

        public JsonResult SaveArticle([FromBody] Article article)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;

            bool isDuplicateArticle = productRepository.Articles.Any(a => a.Name.Trim() == article.Name.Trim() && a.ArticleID != article.ArticleID);

            if (isDuplicateArticle)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Duplicate Article name!" });
            }

            ResultObj obj = productRepository.SaveArticle(article);
            obj.Obj = productRepository.Articles.ToList();
            return Json(obj);
        }
        public JsonResult DeleteArticle([FromBody] Article article)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ResultObj obj = productRepository.DeleteArticle(article);
            obj.Obj = productRepository.Articles.ToList();
            return Json(obj);
        }

        public JsonResult DeleteCoCategory([FromBody] ProdCoCategory prodCoCategory)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ResultObj obj = productRepository.DeleteProdCoCategory(prodCoCategory);
            obj.Obj = productRepository.ProdCoCategorys.ToList();
            return Json(obj);
        }

        public JsonResult SaveCoCategory([FromBody] ProdCoCategory prodCoCategory)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];


            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            bool isDuplicateArticle = productRepository.ProdCoCategorys.Any(a => a.Name.Trim() == prodCoCategory.Name.Trim() && a.ProdCoCategoryID != prodCoCategory.ProdCoCategoryID);

            if (isDuplicateArticle)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Duplicate  name!" });
            }

            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ResultObj obj = productRepository.SaveProdCoCategory(prodCoCategory);
            obj.Obj = productRepository.ProdCoCategorys.ToList();
            return Json(obj);
        }


        public JsonResult DeleteSize([FromBody] Size size)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ResultObj obj = productRepository.DeleteSize(size);
            obj.Obj = productRepository.Sizes.ToList();
            return Json(obj);
        }
        public JsonResult SaveSize([FromBody] Size size)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;

            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            bool isDuplicateArticle = productRepository.Sizes.Any(a => a.Name.Trim() == size.Name.Trim() && a.SizeID != size.SizeID);

            if (isDuplicateArticle)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Duplicate Size name!" });
            }
            ResultObj obj = productRepository.SaveSize(size);
            obj.Obj = productRepository.Sizes.ToList();
            return Json(obj);
        }


        public JsonResult DeleteProdType([FromBody] ProdType prodType)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ResultObj obj = productRepository.DeleteProdType(prodType);
            obj.Obj = productRepository.ProdTypes.ToList();
            return Json(obj);
        }


        public JsonResult SaveProdType([FromBody] ProdType prodType)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];


            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            bool isDuplicateArticle = productRepository.ProdTypes.Any(a => a.Name.Trim() == prodType.Name.Trim() && a.ProdTypeID != prodType.ProdTypeID);

            if (isDuplicateArticle)
            {
                return Json(new ResultObj { ResultID = -1, ResultMessage = "Duplicate  name!" });
            }

            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ResultObj obj = productRepository.SaveProdType(prodType);
            obj.Obj = productRepository.ProdTypes.ToList();
            return Json(obj);
        }


        public JsonResult GetArcticle([FromBody] Article article)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            Article obj = productRepository.Articles.SingleOrDefault(a => a.ArticleID == article.ArticleID);
            return Json(obj);
        }
        public JsonResult GetCoCategory([FromBody] ProdCoCategory prodCoCategory)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];




            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ProdCoCategory obj = productRepository.ProdCoCategorys.SingleOrDefault(a => a.ProdCoCategoryID == prodCoCategory.ProdCoCategoryID);
            return Json(obj);
        }

        public JsonResult GetSize([FromBody] Size size)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            Size obj = productRepository.Sizes.SingleOrDefault(a => a.SizeID == size.SizeID);
            return Json(obj);
        }

        public JsonResult GetProdType([FromBody] ProdType prodType)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ProdType obj = productRepository.ProdTypes.SingleOrDefault(a => a.ProdTypeID == prodType.ProdTypeID);
            return Json(obj);
        }


        public JsonResult SaveCompanyProduct([FromBody] CompanyProduct companyProduct)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comm = userService.GetCompanyByUser(user.UserId);
            companyProduct.CompanyID = comm.CompanyID;
            //customer.UserId = user.UserId;
            ResultObj obj = companyProductRepository.SaveCompanyProduct(companyProduct);
            obj.Obj = productRepository.Articles.ToList();
            return Json(obj);
        }

        public JsonResult DeleteCompanyProduct([FromBody] CompanyProduct companyProduct)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            Company comm = userService.GetCompanyByUser(user.UserId);
            companyProduct.CompanyID = comm.CompanyID;
            //customer.UserId = user.UserId;
            ResultObj obj = companyProductRepository.DeleteCompanyProduct(companyProduct);
            obj.Obj = productRepository.Articles.ToList();
            return Json(obj);
        }
        public JsonResult GetCompanyProductInCompany([FromBody] Supplier supplier)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];


            List<CompanyProduct> obj = companyProductRepository.GetCompanyProductInCompany(supplier.SupplierId);

            return Json(obj);
        }

        public JsonResult GetCompanyProductInCompanyAndProdDoron([FromBody] Supplier supplier, ProdType prodType)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];


            List<CompanyProduct> obj = companyProductRepository.GetCompanyProductInCompanyAndProdDoron(supplier.SupplierId, prodType.ProdTypeID);

            return Json(obj);
        }


        public JsonResult GetCompanyProduct([FromBody] CompanyProduct companyProduct)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];


            CompanyProduct obj = companyProductRepository.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == companyProduct.CompanyProductID);

            return Json(obj);
        }



        public JsonResult GetCompanyProductByProdName([FromBody] ProdName prodName)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];


            List<CompanyProduct> obj = companyProductRepository.GetCompanyProductByProName(prodName.ProdNameID);

            return Json(obj);
        }

        public IActionResult ProductStock()
        {
            return View();
        }

        public JsonResult GetProductStockViewModel()
        {
            ProductStockViewModel obj = new ProductStockViewModel();
            obj.Sizes = productRepository.Sizes.ToList();
            obj.Articles = productRepository.Articles.ToList();
            obj.ProdNames = productRepository.ProdNames.ToList();
            return Json(obj);
        }

        public IActionResult GetProductStockWithValueViewModel()
        {
            return View();
        }
        public IActionResult ProductCurrentStock()
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Current Stock Report ";
            return View();
        }

        public IActionResult ProductCurrentStockValue()
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Current Stock Report ";
            return View();
        }

        public IActionResult ProductCurrentStockArticleSize(string _articleID, string _sizeID)
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Current Stock Report Article and Size";
            return View();
        }

        public JsonResult GetProductCurrentStockArticleSize([FromBody] CompanyProduct model)
        {

            ResultObj _obj = new ResultObj();

            CompanyProduct comProd = companyProductRepository.CompanyProducts.FirstOrDefault(a => a.Article.ArticleID == model.Article.ArticleID && a.Size.SizeID == model.Size.SizeID);

            int _year = repoDailyClose.YearItemStockCloses.Max(a=>a.YearName);

            YearItemStockClose obj2 = repoDailyClose.YearItemStockCloses.FirstOrDefault(a=>a.CompanyProductID== comProd.CompanyProductID && a.YearName== _year);
            _obj.Obj = companyProductRepository.Get_SP_Current_Stock_Article_Size(model).ToList();
            _obj.Obj2 = obj2==null?new YearItemStockClose(): obj2;
            return Json(_obj);
        }



        public JsonResult GetProductCurrentStock()
        {
            ResultObj _obj = new ResultObj();
            _obj.Obj = companyProductRepository.GetCurrentStock().ToList();
            return Json(_obj);
        }


        public IActionResult ProductDatewiseStock(string fromDate, string toDate)
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Current Stock Report ";
            return View();
        }

        public JsonResult GetProductDatewiseStock([FromBody] CompanyProductReportViewModel model)
        {
            ResultObj _obj = new ResultObj();
            Company comp = userService.GetCompanyByUser(1);
            _obj.Obj = companyProductRepository.GetDatewiseStock(comp.CompanyID, model.FromDate,model.ToDate).ToList();
            return Json(_obj);
        }


        public IActionResult PurchseItemClose()
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Current Stock Report ";
            return View();
        }

        public IActionResult DeletePurchseItemClose()
        {
            Company comp = userService.GetCompanyByUser(1);



            TempData["CompanyName"] = comp.Name;

            TempData["CompanyAddress"] = comp.Address;
            TempData["Title"] = "Current Stock Report ";
            return View();
        }


        public JsonResult GetLastEntryDated()
        {

            Company comp = userService.GetCompanyByUser(1);
        
            return Json(companyProductRepository.LastEntryDated());
        }



        public JsonResult Get_View_Item_Close_List([FromBody] CompanyProductReportViewModel model)
        {
            ResultObj _obj = new ResultObj();
            Company comp = userService.GetCompanyByUser(1);
            _obj.Obj = companyProductRepository.Get_View_Item_Close_List( model.ToDate);
            return Json(_obj);
        }

        public JsonResult Get_View_Item_Current_Close_List([FromBody] CompanyProductReportViewModel model)
        {
            ResultObj _obj = new ResultObj();
            Company comp = userService.GetCompanyByUser(1);
            _obj.Obj = companyProductRepository.Get_View_Item_Current_Close_List(model.ToDate);
            return Json(_obj);
        }

        public JsonResult Save_Item_Close_List([FromBody] CompanyProductReportViewModel model)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];


            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(user.UserId);

            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ResultObj obj = companyProductRepository.Save_Item_Close_List(user.UserId, comp.CompanyID,model.ToDate);
            if (obj.ResultID == 1)
            {
                obj.Obj = companyProductRepository.Get_View_Item_Close_List(model.ToDate);
            }


            return Json(obj);
        }


        public JsonResult Delete_Item_Close_List([FromBody] CompanyProductReportViewModel model)
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];


            if (user == null)
            {

                return Json(new ResultObj { ResultID = -1, ResultMessage = "Sesion Expired ,Please login Again" });
            }
            Company comp = userService.GetCompanyByUser(user.UserId);

            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ResultObj obj = companyProductRepository.Delete_Item_Close_List(user.UserId, comp.CompanyID, model.ToDate);
            if (obj.ResultID == 1)
            {
                obj.Obj = companyProductRepository.Get_View_Item_Close_List(model.ToDate);
            }


            return Json(obj);
        }

    }
}
