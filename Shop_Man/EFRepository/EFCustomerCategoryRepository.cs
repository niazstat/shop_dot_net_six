using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFCustomerCategoryRepository : ICustomerCategory
    {
        private OrderManagementDBContext context;
        public EFCustomerCategoryRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<CustomerCategory> CustomerCategorys => context.CustomerCategorys;

        public ResultObj DeleteCustomerCategory(CustomerCategory customerCategory)
        {

            ResultObj res = new ResultObj();

            CustomerCategory dbEntry = context.CustomerCategorys
                   .FirstOrDefault(p => p.CustomerCategoryID == customerCategory.CustomerCategoryID);
            if (dbEntry != null)
            {
                context.CustomerCategorys.Remove(dbEntry);

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

        public CustomerCategory GetCustomerCategory(int id)
        {
           return context.CustomerCategorys.FirstOrDefault(x => x.CustomerCategoryID == id);
        }

        public ResultObj SaveCustomerCategory(CustomerCategory customerCategory)
        {
            ResultObj res = new ResultObj();
            if (customerCategory.CustomerCategoryID == 0)
            {
                context.CustomerCategorys.Add(customerCategory);
            }
            else
            {
                CustomerCategory dbEntry = context.CustomerCategorys
                       .FirstOrDefault(p => p.CustomerCategoryID == customerCategory.CustomerCategoryID);
                if (dbEntry != null)
                {
                    dbEntry.CustomerCategoryName = customerCategory.CustomerCategoryName;
               

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
    }
}
