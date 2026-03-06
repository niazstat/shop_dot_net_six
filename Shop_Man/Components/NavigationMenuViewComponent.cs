using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Components
{
    public class NavigationMenuViewComponent:ViewComponent
    {
        private IPagePermissionRepository repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NavigationMenuViewComponent(IPagePermissionRepository _repository, IHttpContextAccessor httpContextAccessor)
        {
            repository = _repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            List<PermittedController> obj;

            if (user != null)
            {
                IQueryable<PermittedController> list = repository.PermittedControllers.Where(a => a.UserId == user.UserId && a.PermittedProjActions.All(s => s.UserId == user.UserId));


                obj = list.ToList();

            }
            else
            {
                obj = new List<PermittedController>();
            }
            return View(obj);
        }
    }
}
