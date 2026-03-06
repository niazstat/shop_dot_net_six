using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface ISupplierCategory
    {
        IQueryable<SupplierCategory> SupplierCategorys { get; }
        ResultObj SaveSupplierCategory(SupplierCategory supplierCategory);

        SupplierCategory GetSupplierCategory(int id);

        ResultObj DeleteSupplierCategory(SupplierCategory supplierCategory);
    
    }
}
