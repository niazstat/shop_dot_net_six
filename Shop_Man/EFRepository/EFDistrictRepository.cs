using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFDistrictRepository : IDistrictRepository
    {

        private OrderManagementDBContext context;
        public EFDistrictRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<District> District => context.Districts;

        public ResultObj SaveDistrict(District district)
        {
            ResultObj res = new ResultObj();
            if (district.DistrictID == 0)
            {
                context.Districts.Add(district);
            }

            else
            {
                District dbEntry = context.Districts
                   .FirstOrDefault(p => p.DistrictID == district.DistrictID);
                if (dbEntry != null)
                {
                    dbEntry.Name = district.Name;
              
                }
            }
            try
            {
                context.SaveChanges();
                res.ResultID = 1;
                res.ResultMessage = "Successfully Added !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        public ResultObj DeleteDistrict(District district)
        {
            ResultObj res = new ResultObj();



            District dbEntry = context.Districts
                   .FirstOrDefault(p => p.DistrictID == district.DistrictID);
            if (dbEntry != null)
            {
                context.Districts.Remove(dbEntry);

            }



            try
            {
                context.SaveChanges();
                res.ResultID = 1;
                res.ResultMessage = "Successfully Deleted !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }
    }
}
