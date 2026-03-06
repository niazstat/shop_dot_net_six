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
    public class EFCashReceiveRepository : ICashReceiveRepository
    {


        private OrderManagementDBContext context;
        public EFCashReceiveRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<CashReceive> CashReceives => context.CashReceive.Include(a => a.Customer).Include(b => b.PaymentMedium);
        public IQueryable<CashReceive> CashReceivesAsNotracking => context.CashReceive.Include(a => a.Customer).Include(b => b.PaymentMedium).AsNoTracking();

        public IQueryable<CashReceive> CashReceivesDetails =>context.CashReceive.Include(a=>a.Customer).Include(b=>b.PaymentMedium);

        public ResultObj DeleteCashReceive(CashReceive cashReceive, int prevCustID, decimal prevAmount)
        {
            ResultObj res = new ResultObj();


            LogEntryEdit _log = new LogEntryEdit();
            _log.ChallanID = cashReceive.CashReceiveID;
            _log.dDate = cashReceive.ReceiveDate;
            _log.EditItem = "Cash Receive";
            _log.EditType = "Delete";
            _log.IsProcessDone = false;
            //string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
            _log.Remarks = $"PrevCust ID:{ prevCustID} ,Prev Amount :{prevAmount}";
            context.LogEntryEdits.Add(_log);



            Customer newCust = context.Customer.SingleOrDefault(a => a.CustomerID == prevCustID);

            newCust.CurrentBalance = newCust.CurrentBalance + prevAmount;//ReversEntry
            context.Entry(newCust).State = EntityState.Modified;

            context.CashReceive.Remove(cashReceive);


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

        public CashReceive FindCashReceive(int _cashReceiveID)
        {
            throw new NotImplementedException();
        }

        public CashReceive FindCashReceiveByInvoice(string _invoiceNo)
        {
            throw new NotImplementedException();
        }

        public ResultObj SaveCashReceive(CashReceive cashReceive,int _prevCustId,decimal prevAmount)
        {
            ResultObj res = new ResultObj();
           // context.Attach(cashReceive.PaymentMedium);
           // context.Attach(cashReceive.Customer);

            if (cashReceive.CashReceiveID == 0)
            {

                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = cashReceive.CashReceiveID;
                _log.dDate = cashReceive.ReceiveDate;
                _log.EditItem = "Cash Receive";
                _log.EditType = "New";
                _log.IsProcessDone = false;
                //string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
                _log.Remarks = $"PrevCust ID:{ _prevCustId} ,Prev Amount :{prevAmount}";
                context.LogEntryEdits.Add(_log);

                Customer newCust = context.Customer.SingleOrDefault(a => a.CustomerID == cashReceive.Customer.CustomerID);
                newCust.CurrentBalance = newCust.CurrentBalance - cashReceive.Amount ;//
                context.Entry(newCust).State = EntityState.Modified;
                context.CashReceive.Add(cashReceive).GetDatabaseValues(); 
            }
           
            else
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = cashReceive.CashReceiveID;
                _log.dDate = cashReceive.ReceiveDate;
                _log.EditItem = "Cash Receive";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;
                //string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
                _log.Remarks = $"PrevCust ID:{ _prevCustId} ,Prev Amount :{prevAmount}";
                context.LogEntryEdits.Add(_log);


                if (cashReceive.Customer.CustomerID == _prevCustId)
                {
                    Customer prevcust = context.Customer.SingleOrDefault(a => a.CustomerID == _prevCustId);
                    prevcust.CurrentBalance = prevcust.CurrentBalance - cashReceive.Amount + prevAmount;//ReversEntry
                    context.Entry(prevcust).State = EntityState.Modified;
                }

                else
                {
                    Customer prevcust = context.Customer.SingleOrDefault(a => a.CustomerID == _prevCustId);
                    prevcust.CurrentBalance = prevcust.CurrentBalance  + prevAmount;//ReversEntry
                    context.Entry(prevcust).State = EntityState.Modified;
                    Customer newCust = context.Customer.SingleOrDefault(a => a.CustomerID == cashReceive.Customer.CustomerID);

                    newCust.CurrentBalance = newCust.CurrentBalance - cashReceive.Amount ;//
                    context.Entry(newCust).State = EntityState.Modified;
                }


                context.Entry(cashReceive).State = EntityState.Modified;
                // context.CashReceive.Add(cashReceive);
                //CashReceive dbEntry = context.CashReceive
                //       .FirstOrDefault(p => p.CashReceiveID == cashReceive.CashReceiveID);
                //if (dbEntry != null)
                //{
                //    //context.AttachRange(cashReceive.Customer);
                //    dbEntry.Amount = cashReceive.Amount;
                //    dbEntry.Note = cashReceive.Note;
                //    dbEntry.ReceiveDate = cashReceive.ReceiveDate;
                //    dbEntry.Customer = cashReceive.Customer;
                //}
            }



            try
            {
                context.SaveChanges();
                context.Entry(cashReceive).State = EntityState.Detached;

              
                    context.Entry(cashReceive).Reload();
                    res.ResultNo = cashReceive.GeneratedInvoicNo;
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

        public ResultObj UpdateCashReceive(CashReceive cashReceive)
        {
            throw new NotImplementedException();
        }
    }
}
