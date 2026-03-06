using Microsoft.Data.SqlClient;
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
    public class EFCashPaymentRepository : ICashPaymentRepository
    {
        private OrderManagementDBContext context;
        public EFCashPaymentRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<CashPayment> CashPayments => context.CashPayments.Include(a => a.Supplier).Include(a => a.Customer).Include(b => b.PaymentMedium);
        public IQueryable<CashPayment> CashPaymentsAsNoTracking => context.CashPayments.Include(a => a.Supplier).Include(a=>a.Customer).Include(b => b.PaymentMedium).AsNoTracking();

        public IQueryable<CashPayment> CashPaymentDetails => context.CashPayments.Include(a => a.Supplier).Include(b => b.PaymentMedium);


        //public IQueryable<SP_Cash_Transaction_Details> Cash_Transaction_Details(DateTime fromdate, DateTime toDate)
        //{
        //return context.Query<SP_Cash_Transaction_Details>().FromSql(@"EXEC SP_Cash_Transaction_Details {0},{1},{2}", fromdate, toDate,"");

        //}

        public IQueryable<SP_Cash_Transaction_Details> Cash_Transaction_Details(DateTime fromdate, DateTime toDate)
        {
            return context.CashTransactionDetails
                .FromSqlRaw(
                    "EXEC SP_Cash_Transaction_Details @FromDate, @ToDate, @Param",
                    new SqlParameter("@FromDate", fromdate),
                    new SqlParameter("@ToDate", toDate),
                    new SqlParameter("@Param", "")
                )
                .AsNoTracking();
        }

        //public IQueryable<SP_Cash_Transaction_Details> SP_Cash_Transaction_Summ(DateTime fromdate, DateTime toDate)
        //{
        //    return context.Query<SP_Cash_Transaction_Details>().FromSql(@"EXEC SP_Cash_Transaction_Summ {0},{1},{2}", fromdate, toDate, "");

        //}
        public IQueryable<SP_Cash_Transaction_Details> SP_Cash_Transaction_Summ(DateTime fromdate, DateTime toDate)
        {
            return context.CashTransactionDetails
                .FromSqlInterpolated(
                    $"EXEC SP_Cash_Transaction_Summ {fromdate}, {toDate}, {""}"
                )
                .AsNoTracking();
        }

        public ResultObj DeleteCashPayment(CashPayment cashPayment, int prevCustID, int prevSuppID, decimal prevAmount)
        {
            ResultObj res = new ResultObj();

            LogEntryEdit _log = new LogEntryEdit();
            if (cashPayment != null)
            {
                _log.ChallanID = cashPayment.CashPaymentID;
                _log.dDate = cashPayment.PaymentDate;
                _log.EditItem = "CashPayment";
                _log.EditType = "Delete";
                _log.IsProcessDone =false;
                string _supp_Cust = cashPayment.Customer == null ? cashPayment.Supplier.Name : cashPayment.Supplier.Name;
                _log.Remarks = $"Amount:{cashPayment.Amount} Cust?Supp Name:{ _supp_Cust}";

                context.LogEntryEdits.Add(_log);
                if (prevSuppID != 0)
                {
                    Supplier newSupp = context.Suppliers.SingleOrDefault(a => a.SupplierId == prevSuppID);
                    newSupp.CurrentBalance = newSupp.CurrentBalance + prevAmount;//
                    context.Entry(newSupp).State = EntityState.Modified;
                    context.CashPayments.Remove(cashPayment);
                }
                if (prevCustID != 0)
                {

                    Customer newCust = context.Customer.SingleOrDefault(a => a.CustomerID == prevCustID);
                    newCust.CurrentBalance = newCust.CurrentBalance + cashPayment.Amount;//
                    context.Entry(newCust).State = EntityState.Modified;
                    context.CashPayments.Remove(cashPayment);
                    //context.CashPayments.Add(cashPayment).GetDatabaseValues();
                }

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

        public CashPayment FindCashPayment(int _cashPaymentID)
        {
            throw new NotImplementedException();
        }

        public CashPayment FindCCashPaymentByInvoice(string _invoiceNo)
        {
            throw new NotImplementedException();
        }

        public ResultObj SaveCashPayment(CashPayment cashPayment,int prevCustID , int _prevSuppId,decimal prevAmount)
        {
            ResultObj res = new ResultObj();
          
            if (cashPayment.CashPaymentID == 0)
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = cashPayment.CashPaymentID;
                _log.dDate = cashPayment.PaymentDate;
                _log.EditItem = "CashPayment";
                _log.EditType = "New";
                _log.IsProcessDone = false;
                string _supp_Cust = cashPayment.Supplier == null ? cashPayment.Customer.Name : cashPayment.Supplier.Name;
                _log.Remarks = $"Prev Amount:{prevAmount}Amount:{cashPayment.Amount} Cust?Supp Name:{ _supp_Cust}";
                context.LogEntryEdits.Add(_log);
                context.CashPayments.Add(cashPayment).GetDatabaseValues();
              
            }

            else
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = cashPayment.CashPaymentID;
                _log.dDate = cashPayment.PaymentDate;
                _log.EditItem = "CashPayment";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;
                int _supp_Cust = prevCustID == 0 ? _prevSuppId: prevCustID;
                _log.Remarks = $"Prev Amount:{prevAmount}Amount:{cashPayment.Amount} Cust?Supp Name:{ _supp_Cust}";
                context.LogEntryEdits.Add(_log);
               
                context.Entry(cashPayment).State = EntityState.Modified;
             
            }

            try
            {
                context.SaveChanges();
                context.Entry(cashPayment).State = EntityState.Detached;


                context.Entry(cashPayment).Reload();
                res.ResultNo = cashPayment.GeneratedInvoicNo;
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

        public ResultObj UpdateCashPayment(CashPayment cashPayment)
        {
            throw new NotImplementedException();
        }
    }
}
