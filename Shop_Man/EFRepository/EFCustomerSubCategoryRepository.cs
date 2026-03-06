using Microsoft.EntityFrameworkCore;
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFCustomerSubCategoryRepository : ICustomerSubCategory
    {

        private OrderManagementDBContext context;
        public EFCustomerSubCategoryRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<CustomerSubCategory> CustomerSubCategorys => context.CustomerSubCategorys.Include(a=>a.CustomerCategory);

        public List<CustomerSubCategory> CustomerSubCategorysInCategory(int _categoryID)
        {
            return CustomerSubCategorys.Where(a => a.CustomerCategoryID == _categoryID).ToList();
        }

        public ResultObj DeleteCustomerSubCategory(CustomerSubCategory customerSubCategory)
        {

            ResultObj res = new ResultObj();



            CustomerSubCategory dbEntry = context.CustomerSubCategorys
                   .FirstOrDefault(p => p.CustomerSubCategoryID == customerSubCategory.CustomerSubCategoryID);
            if (dbEntry != null)
            {
                context.CustomerSubCategorys.Remove(dbEntry);

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

        public CustomerSubCategory GetCustomerSubCategory(int id)
        {
            return context.CustomerSubCategorys.FirstOrDefault(x => x.CustomerSubCategoryID == id);
        }

        public ResultObj SaveCustomerSubCategory(CustomerSubCategory customerSubCategory)
        {
            ResultObj res = new ResultObj();
            if (customerSubCategory.CustomerSubCategoryID == 0)
            {
                context.CustomerSubCategorys.Add(customerSubCategory);
            }

            else
            {
                CustomerSubCategory dbEntry = context.CustomerSubCategorys
                       .FirstOrDefault(p => p.CustomerSubCategoryID == customerSubCategory.CustomerSubCategoryID);
                if (dbEntry != null)
                {
                    dbEntry.CustomerSubCategoryName = customerSubCategory.CustomerSubCategoryName;
                    dbEntry.CustomerCategoryID = customerSubCategory.CustomerCategoryID;

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
