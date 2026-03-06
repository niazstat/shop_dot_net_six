using Microsoft.EntityFrameworkCore;
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Models.ViewModels;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFPagePermissionRepository : IPagePermissionRepository
    {
        private OrderManagementDBContext context;
        public EFPagePermissionRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<ProjController> ProjControllers => context.ProjControllers.Include(a => a.ProjActions);

        public IQueryable<PermittedController> PermittedControllers => context.PermittedControllers.Include(a => a.ProjController).Include(b => b.PermittedProjActions).ThenInclude(c => c.ProjAction);

        public List<PermittedController> PermittedControllers2 => context.PermittedControllers.Include(a => a.ProjController).Include(b => b.PermittedProjActions).ThenInclude(c => c.ProjAction).ToList();


        public ResultObj SavePagePermission(User user, List<ProjController> permittedControllers)
        {
            throw new NotImplementedException();
        }

        public ResultObj UpdateEditPermissionBlock(EditPermissionObj model)
        {
            ResultObj result = new ResultObj();

            if (model.Type == "SALES")
            {
                context.Database.ExecuteSqlRaw($" Update SalesHeads set IsAllowEdit=0 where SalesHeadID={model.ChallanID}");

            }
            else if (model.Type == "PURCHASE")
            {
                context.Database.ExecuteSqlRaw($" Update PurchaseHeads set IsAllowEdit=0 where PurchaseHeadID={model.ChallanID}");


            }
            else if (model.Type == "EXPENSES")
            {

                context.Database.ExecuteSqlRaw($" Update Expenses set IsAllowEdit=0 where ExpenseID={model.ChallanID}");

            }
            else if (model.Type == "CASHPAYMENT")
            {

                context.Database.ExecuteSqlRaw($" Update CashPayments set IsAllowEdit=0 where CashPaymentID={model.ChallanID}");
            }
            else if (model.Type == "CASHRECEIVE")
            {

                context.Database.ExecuteSqlRaw($" Update CashReceive set IsAllowEdit=0 where CashReceiveID={model.ChallanID}");

            }
            else if (model.Type == "ADJUST")
            {
                context.Database.ExecuteSqlRaw($" Update Adjustments set IsAllowEdit=0 where AdjustmentID={model.ChallanID}");

            }
            else if (model.Type == "SALESRETURN")
            {
                context.Database.ExecuteSqlRaw($" Update SalesReturns set IsAllowEdit=0 where SalesReturnID={model.ChallanID}");

            }

            else
            {
                return new ResultObj { ResultID = -1, ResultMessage = "Incvalid Type " };
            }

            try
            {
                context.SaveChanges();

                result.ResultID = 1;
                result.ResultMessage = "Items Updated";
            }
            catch (Exception ex)
            {
                result.ResultID = -1;
                result.ResultMessage = ex.ToString();
            }

            return result;
        }

        public ResultObj UpdateEditPermissionUnBlock(EditPermissionObj model)
        {
            ResultObj result = new ResultObj();

            if (model.Type == "SALES")
            {
                context.Database.ExecuteSqlRaw($" Update SalesHeads set IsAllowEdit=1 where SalesHeadID={model.ChallanID}");

            }
            else if (model.Type == "PURCHASE")
            {
                context.Database.ExecuteSqlRaw($" Update PurchaseHeads set IsAllowEdit=1 where PurchaseHeadID={model.ChallanID}");


            }
            else if (model.Type == "EXPENSES")
            {

                context.Database.ExecuteSqlRaw($" Update Expenses set IsAllowEdit=1 where ExpenseID={model.ChallanID}");

            }
            else if (model.Type == "CASHPAYMENT")
            {

                context.Database.ExecuteSqlRaw($" Update CashPayments set IsAllowEdit=1 where CashPaymentID={model.ChallanID}");
            }
            else if (model.Type == "CASHRECEIVE")
            {

                context.Database.ExecuteSqlRaw($" Update CashReceive set IsAllowEdit=1 where CashReceiveID={model.ChallanID}");

            }
            else if (model.Type == "ADJUST")
            {
                context.Database.ExecuteSqlRaw($" Update Adjustments set IsAllowEdit=1 where AdjustmentID={model.ChallanID}");

            }
            else if (model.Type == "SALESRETURN")
            {
                context.Database.ExecuteSqlRaw($" Update SalesReturns set IsAllowEdit=1 where SalesReturnID={model.ChallanID}");

            }
            else
            {
                return new ResultObj { ResultID = -1, ResultMessage = "Incvalid Type " };
            }

            try
            {
                context.SaveChanges();
            
                result.ResultID = 1;
                result.ResultMessage = "Items Updated";
            }
            catch (Exception ex)
            {
                result.ResultID = -1;
                result.ResultMessage = ex.ToString();
            }

            return result;

        }
    }
}
