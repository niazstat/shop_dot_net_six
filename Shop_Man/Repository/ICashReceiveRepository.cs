using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface ICashReceiveRepository
    {

        IQueryable<CashReceive> CashReceives { get; }
        IQueryable<CashReceive> CashReceivesAsNotracking { get; }
        IQueryable<CashReceive> CashReceivesDetails { get; }
  
        ResultObj SaveCashReceive(CashReceive cashReceive, int prevCustID, decimal prevAmount);
        CashReceive FindCashReceive(int _cashReceiveID);
        CashReceive FindCashReceiveByInvoice(string _invoiceNo);
        ResultObj UpdateCashReceive(CashReceive cashReceive);
        ResultObj DeleteCashReceive(CashReceive cashReceive,int prevcustId,decimal prevAmount);
    }
}
