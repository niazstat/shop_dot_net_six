using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Shop_Man.Infrastructure;
using Shop_Man.Models;
using Shop_Man.Models.ViewModels;
using Shop_Man.Repository;

namespace Shop_Man.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        private IUserService repoUser;
        private readonly IConfiguration _config;
        public int PageSize = 4;

        public HomeController(IStoreRepository repo, IConfiguration config, IUserService _repoUser)
        {
            repository = repo;
            _config = config;
            repoUser = _repoUser;
        }



    

   
 //       public ViewResult Index(string category,int productPage = 1)
 //       => View(new ProductsListViewModel
 //       {
 //           Products = repository.Products
 //.OrderBy(p => p.ProductID)
 //.Skip((productPage - 1) * PageSize)
 //.Take(PageSize),
 //           PagingInfo = new PagingInfo
 //           {
 //               CurrentPage = productPage,
 //               ItemsPerPage = PageSize,
            
 //               TotalItems = category == null ?
 //repository.Products.Count() :
 //repository.Products.Where(e =>
 //e.Category == category).Count()
 //           }
 //       });

        public ViewResult LogIn()
        {

            SetJWTCookie("");


            return View();
        }


        [Authorize]
        public ViewResult Index()
        {
            return View();
        }
        public ViewResult Logout()
        {

            SetJWTCookie("");


            return View();
        }

        [HttpPost]

        public IActionResult Login(string UserName, string password)
        {
            IActionResult response = Unauthorized();


            User user = repoUser.AuthenticateUser(new  AuthenticateRequest {  Username = UserName, Password = password });
            if (user == null)
            {
                TempData["LogInfo"] = "Invalid User ID or Password !";
                return View();
            }


            if (user != null)
            {
                //Get Company ID;

                var dsd = repoUser.Authenticate(user);

                SetJWTCookie(dsd.Token);
                return RedirectToAction("Index", "Home");
            }
            return response;
        }
        private void SetJWTCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(3),
            };

      
     
            Response.Cookies.Append("jwtCookie", token, cookieOptions);
        }

       // [Authorize]
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {

            var jwt = Request.Cookies["jwtCookie"];
            var users = repoUser.GetAll();
            return View();
        }
    }
}
