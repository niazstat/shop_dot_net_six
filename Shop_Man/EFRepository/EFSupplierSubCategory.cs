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
    public class EFSupplierSubCategory : ISupplierSubCategory
    {

        private OrderManagementDBContext context;
        public EFSupplierSubCategory(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<SupplierSubCategory> SupplierSubCategorys => context.SupplierSubCategorys.Include(a => a.SupplierCategory);

        public ResultObj DeleteSupplierSubCategory(SupplierSubCategory supplierSubCategory)
        {
            ResultObj res = new ResultObj();

            SupplierSubCategory dbEntry = context.SupplierSubCategorys
                   .FirstOrDefault(p => p.SupplierSubCategoryID == supplierSubCategory.SupplierSubCategoryID);
            if (dbEntry != null)
            {
                context.SupplierSubCategorys.Remove(dbEntry);

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

        public SupplierSubCategory GetSupplierSubCategory(int id)
        {
            return SupplierSubCategorys.FirstOrDefault(a => a.SupplierSubCategoryID == id);
        }

        public ResultObj SaveSupplierSubCategory(SupplierSubCategory supplierSubCategory)
        {
            ResultObj res = new ResultObj();
            if (supplierSubCategory.SupplierSubCategoryID == 0)
            {
                context.SupplierSubCategorys.Add(supplierSubCategory);
            }

            else
            {
                SupplierSubCategory dbEntry = context.SupplierSubCategorys
                       .FirstOrDefault(p => p.SupplierSubCategoryID == supplierSubCategory.SupplierSubCategoryID);
                if (dbEntry != null)
                {
                    dbEntry.SupplierSubCategoryName = supplierSubCategory.SupplierSubCategoryName;
                    dbEntry.SupplierCategoryID = supplierSubCategory.SupplierCategoryID;

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

        public List<SupplierSubCategory> SupplierSubCategorysInCategory(int _categoryID)
        {
            return SupplierSubCategorys.Where(a => a.SupplierCategoryID == _categoryID).ToList();
        }
    }
}
