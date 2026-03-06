using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFSupplierCategory : ISupplierCategory
    {

        private OrderManagementDBContext context;
        public EFSupplierCategory(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<SupplierCategory> SupplierCategorys =>context.SupplierCategorys;

        public ResultObj DeleteSupplierCategory(SupplierCategory supplierCategory)
        {

            ResultObj res = new ResultObj();

            SupplierCategory dbEntry = context.SupplierCategorys
                   .FirstOrDefault(p => p.SupplierCategoryID == supplierCategory.SupplierCategoryID);
            if (dbEntry != null)
            {
                context.SupplierCategorys.Remove(dbEntry);

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

        public SupplierCategory GetSupplierCategory(int id)
        {
            return context.SupplierCategorys.FirstOrDefault(x => x.SupplierCategoryID == id);
        }

        public ResultObj SaveSupplierCategory(SupplierCategory supplierCategory)
        {
            ResultObj res = new ResultObj();
            if (supplierCategory.SupplierCategoryID == 0)
            {
                context.SupplierCategorys.Add(supplierCategory);
            }
            else
            {
                SupplierCategory dbEntry = context.SupplierCategorys
                       .FirstOrDefault(p => p.SupplierCategoryID == supplierCategory.SupplierCategoryID);
                if (dbEntry != null)
                {
                    dbEntry.SupplierCategoryName = supplierCategory.SupplierCategoryName;
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
