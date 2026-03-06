using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface ICashPaymentRepository
    {


        IQueryable<CashPayment> CashPayments { get; }
        IQueryable<CashPayment> CashPaymentDetails { get; }
        IQueryable<CashPayment> CashPaymentsAsNoTracking { get; }
        ResultObj SaveCashPayment(CashPayment cashPayment, int _prevCustID, int _prevSuppId, decimal prevAmount);
        CashPayment FindCashPayment(int _cashPaymentID);
        CashPayment FindCCashPaymentByInvoice(string _invoiceNo);
        ResultObj UpdateCashPayment(CashPayment cashPayment);
        ResultObj DeleteCashPayment(CashPayment cashPayment, int prevCustID, int prevSuppID, decimal prevAmount);

         IQueryable<SP_Cash_Transaction_Details> Cash_Transaction_Details(DateTime fromdate,DateTime toDate);
        IQueryable<SP_Cash_Transaction_Details> SP_Cash_Transaction_Summ(DateTime fromdate, DateTime toDate);

    }
}
