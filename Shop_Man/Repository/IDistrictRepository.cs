using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface IDistrictRepository
    {
        IQueryable<District> District { get; }
        ResultObj SaveDistrict(District district);
        ResultObj DeleteDistrict(District district);
    }
}
