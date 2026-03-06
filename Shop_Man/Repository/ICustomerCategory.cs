using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface ICustomerCategory
    {

        IQueryable<CustomerCategory> CustomerCategorys { get; }
        ResultObj SaveCustomerCategory(CustomerCategory customerCategory);

        CustomerCategory GetCustomerCategory(int id);

        ResultObj DeleteCustomerCategory(CustomerCategory customerCategory);
    }
}
