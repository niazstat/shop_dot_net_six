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
    public class EFAdjustmentRepository : IAdjustmentRepository
    {
        private OrderManagementDBContext context;
        public EFAdjustmentRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Adjustment> Adjustments => context.Adjustments.Include(a=>a.Customer).Include(b=>b.Supplier);

        public IQueryable<Adjustment> AdjustmentsAsNoTracking => context.Adjustments.Include(a => a.Customer).Include(b => b.Supplier).AsNoTracking();

        public ResultObj DeleteAdjustment(Adjustment adjustment)
        {
            ResultObj res = new ResultObj();


            if (adjustment != null)
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = adjustment.AdjustmentID;
                _log.dDate = adjustment.PaymentDate;
                _log.EditItem = "Adjustment";
                _log.EditType = "Delete";
                _log.IsProcessDone = false;
                string _supp_Cust = adjustment.Customer == null ? adjustment.Supplier.Name : adjustment.Customer.Name;
                _log.Remarks = $"Prev Amount:{adjustment.Amount}Cust/Supp Name:{ _supp_Cust}";
                context.LogEntryEdits.Add(_log);
                context.Adjustments.Remove(adjustment);
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

        public Adjustment FindAdjustment(int _adjustmentID)
        {
            throw new NotImplementedException();
        }

        public Adjustment FindAdjustmentByInvoice(string _invoiceNo)
        {
            throw new NotImplementedException();
        }

        public ResultObj SaveAdjustment(Adjustment adjustment)
        {
            ResultObj res = new ResultObj();

            if (adjustment.AdjustmentID == 0)
            {


                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = adjustment.AdjustmentID;
                _log.dDate = adjustment.PaymentDate;
                _log.EditItem = "Adjustment";
                _log.EditType = "New";
                _log.IsProcessDone = false;
                string _supp_Cust = adjustment.Customer == null ? adjustment.Supplier.Name : adjustment.Customer.Name;
                _log.Remarks = $"Prev Amount:{adjustment.Amount}Cust/Supp Name:{ _supp_Cust}";
                context.LogEntryEdits.Add(_log);



                context.Adjustments.Add(adjustment).GetDatabaseValues();

            }

            else
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = adjustment.AdjustmentID;
                _log.dDate = adjustment.PaymentDate;
                _log.EditItem = "Adjustment";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;
                string _supp_Cust = adjustment.Customer == null ? adjustment.Supplier.Name : adjustment.Customer.Name;
                _log.Remarks = $"Prev Amount:{adjustment.Amount}Cust/Supp Name:{ _supp_Cust}";
                context.LogEntryEdits.Add(_log);
                context.Entry(adjustment).State = EntityState.Modified;

            }

            try
            {
                context.SaveChanges();
                context.Entry(adjustment).State = EntityState.Detached;


                context.Entry(adjustment).Reload();
                res.ResultNo = adjustment.GeneratedInvoicNo;
                // res.Obj = cashReceive;

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
    }
}
