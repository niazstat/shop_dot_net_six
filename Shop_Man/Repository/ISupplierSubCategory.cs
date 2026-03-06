using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
   public interface ISupplierSubCategory
    {
        IQueryable<SupplierSubCategory> SupplierSubCategorys { get; }
        ResultObj SaveSupplierSubCategory(SupplierSubCategory supplierSubCategory);

        SupplierSubCategory GetSupplierSubCategory(int id);

        List<SupplierSubCategory> SupplierSubCategorysInCategory(int _categoryID);

        ResultObj DeleteSupplierSubCategory(SupplierSubCategory supplierSubCategory);
    }
}
