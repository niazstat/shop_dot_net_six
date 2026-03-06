using Shop_Man.Models;
using Shop_Man.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface IPagePermissionRepository
    {
        IQueryable<ProjController> ProjControllers { get; }
        IQueryable<PermittedController> PermittedControllers { get; }
        ResultObj SavePagePermission(User user,List<ProjController> permittedControllers);

        ResultObj UpdateEditPermissionBlock(EditPermissionObj model);

        ResultObj UpdateEditPermissionUnBlock(EditPermissionObj model);
    }
}
