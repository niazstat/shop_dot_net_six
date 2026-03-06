using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Man.Infrastructure;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Controllers
{
    public class DistrictController : Controller
    {
        private IUserService userService;
        private IDistrictRepository districtRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DistrictController( IUserService _userService, IHttpContextAccessor httpContextAccessor, IDistrictRepository _districtRepository)
        {
            userService = _userService;
            _httpContextAccessor = httpContextAccessor;
            districtRepository = _districtRepository;
        }

        [Authorize]
        public ActionResult SaveDistrict([FromBody] District district)
        {
            ResultObj obj = districtRepository.SaveDistrict(district);
            if (obj.ResultID != -1)
            {
                obj.Obj = districtRepository.District;
            }
            return Json(obj);
        }

        public JsonResult GetDistrict([FromBody] District district)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            District dist = districtRepository.District.FirstOrDefault(a=>a.DistrictID==district.DistrictID);

            return Json(dist);
        }

        public JsonResult DeleteDistrict([FromBody] District district)
        {

            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            //customer.Company = userService.GetCompanyByUser(user.UserId);
            //customer.UserId = user.UserId;
            ResultObj obj = districtRepository.DeleteDistrict(district);
            if (obj.ResultID != -1)
            {
                obj.Obj = districtRepository.District;
            }
            return Json(obj);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
