using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
   public interface ICustomerSubCategory
    {
        IQueryable<CustomerSubCategory> CustomerSubCategorys { get; }
        ResultObj SaveCustomerSubCategory(CustomerSubCategory customerSubCategory);

        CustomerSubCategory GetCustomerSubCategory(int id);

        List<CustomerSubCategory> CustomerSubCategorysInCategory(int _categoryID);

        ResultObj DeleteCustomerSubCategory(CustomerSubCategory customerSubCategory);
    }
}
