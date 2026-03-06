using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
   public interface ISupplierRepository
    {
        IQueryable<Supplier> Suppliers { get; }
        ResultObj SaveSupplier(Supplier supplier);
        ResultObj UpdateSupplier(Supplier supplier);
        ResultObj DeleteSupplier(Supplier supplier);

        Supplier GetSupplier(int id);

        decimal GetSupplierCurrentBalance(int id);
        List<SP_SuppierBalancceDetails> SupplierBalanceDetailsList(int suppID);
        List<SP_SupplierBalance_Datewise> DatewiseSuppliuerBalanceList(int suppId, DateTime formDate, DateTime toDate);
        List<SP_SupplierBalance_ALL> SupplierBalanceALL();
    }
}
