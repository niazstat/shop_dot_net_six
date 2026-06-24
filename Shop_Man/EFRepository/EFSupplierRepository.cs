using Microsoft.EntityFrameworkCore;
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFSupplierRepository : ISupplierRepository
    {
        private OrderManagementDBContext context;
        public EFSupplierRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }


        public IQueryable<Supplier> Suppliers => context.Suppliers.Include(a => a.SupplierSubCategory);

        //public List<SP_SupplierBalance_Datewise> DatewiseSuppliuerBalanceList(int suppId, DateTime formDate, DateTime toDate)
        //{
        //    return context.Query<SP_SupplierBalance_Datewise>().FromSql(@"EXEC SP_SupplierBalance_Datewise {0},{1},{2}", suppId, formDate, toDate).OrderBy(a => a.dDate).ThenBy(b => b.ID).ToList();

        //}
   

        public List<SP_SupplierBalance_Datewise> DatewiseSuppliuerBalanceList(int suppId, DateTime formDate, DateTime toDate)
        {
            return context.SP_SupplierBalance_Datewise
            .FromSqlInterpolated($"EXEC SP_SupplierBalance_Datewise {suppId}, {formDate}, {toDate}")
            .AsNoTracking()
                .AsEnumerable()
            .OrderBy(a => a.dDate)
            .ThenBy(b => b.ID)
            .ToList();
        }

        public ResultObj DeleteSupplier(Supplier supplier)
        {
            ResultObj res = new ResultObj();



            Supplier dbEntry = context.Suppliers
                   .FirstOrDefault(p => p.SupplierId == supplier.SupplierId);

            if (dbEntry.IsLocked)
            {
                res.ResultID = -1;
                res.ResultMessage = "Edite Blocked ,Please Unlock Block!";
            }
            if (dbEntry != null)
            {
                context.Suppliers.Remove(dbEntry);

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

        public Supplier GetSupplier(int id)
        {
            return Suppliers.SingleOrDefault(a => a.SupplierId == id);
        }

        //public decimal GetSupplierCurrentBalance(int id)
        //{
        //    SP_SuppierPrevBalancce obj = context.Query<SP_SuppierPrevBalancce>().FromSql(@"EXEC SP_SuppierPrevBalancce {0}", id).First();

        //    return obj.PrevBalance;

        //}
        public decimal GetSupplierCurrentBalance(int id)
        {
            var obj = context.SP_SuppierPrevBalancce
                .FromSqlInterpolated($"EXEC SP_SuppierPrevBalancce {id}")
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();

            return obj?.PrevBalance ?? 0; // return 0 if no record found
        }

        public ResultObj SaveSupplier(Supplier supplier)
        {
            ResultObj res = new ResultObj();
            if (supplier.SupplierId == 0)
            {
                context.Suppliers.Add(supplier);


            }
            else
            {

                Supplier dbEntry = context.Suppliers
                       .FirstOrDefault(p => p.SupplierId == supplier.SupplierId);
                if (dbEntry.IsLocked)
                {
                    res.ResultID = -1;
                    res.ResultMessage = "Edite Blocked ,Please Unlock Block!";
                }
                if (dbEntry != null)
                {
                    dbEntry.Name = supplier.Name;
              
                    dbEntry.SupplierSubCategoryID = supplier.SupplierSubCategoryID;
                    dbEntry.ContactPerson = supplier.ContactPerson;
                    dbEntry.Address1 = supplier.Address1;
                    dbEntry.MobileNo1 = supplier.MobileNo1;
                    dbEntry.MobileNo2 = supplier.MobileNo2;
                    dbEntry.OpeningBalance = supplier.OpeningBalance;
                    dbEntry.CurrentBalance = dbEntry.CurrentBalance+ supplier.OpeningBalance;
                    dbEntry.LastEditedDate = DateTime.Now;
                    dbEntry.IsEdited = 1;
                }
            }


            try
            {
                context.SaveChanges();
                res.ResultID = 1;
                res.ResultMessage = "Successfully Added /Updated !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        //public List<SP_SupplierBalance_ALL> SupplierBalanceALL()
        //{
        //    return context.Query<SP_SupplierBalance_ALL>().FromSql(@"EXEC SP_SupplierBalance_ALL").OrderBy(a => a.SupplierId).ToList();

        //}

        //public List<SP_SuppierBalancceDetails> SupplierBalanceDetailsList(int suppID)
        //{
        //    return context.Query<SP_SuppierBalancceDetails>().FromSql(@"EXEC SP_SuppierBalancceDetails {0}", suppID).OrderBy(a => a.dDate).ThenBy(b => b.ID).ToList();

        //}
        public List<SP_SupplierBalance_ALL> SupplierBalanceALL()
        {
            return context.SP_SupplierBalance_ALL
                .FromSqlInterpolated($"EXEC SP_SupplierBalance_ALL")
                .AsNoTracking()
                  .AsEnumerable()
                .OrderBy(a => a.SupplierId)
                .ToList();
        }

        public List<SP_SuppierBalancceDetails> SupplierBalanceDetailsList(int suppID)
        {
            return context.SP_SuppierBalancceDetails
                .FromSqlInterpolated($"EXEC SP_SuppierBalancceDetails {suppID}")
                .AsNoTracking()
                  .AsEnumerable()
                .OrderBy(a => a.dDate)
                .ThenBy(b => b.ID)
                .ToList();
        }

        public ResultObj UpdateSupplier(Supplier supplier)
        {
            throw new NotImplementedException();
        }
    }
}
