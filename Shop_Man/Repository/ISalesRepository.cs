using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface ISalesRepository
    {
        IQueryable<SalesHead> SalesHeads { get; }
        IQueryable<SalesHead> SalesHeadsDetails { get; }
        IQueryable<SalesHead> SalesHeadsDetailsAsnoTracking { get; }
        IQueryable<SalesDetails> SalesDetails { get; }
        IQueryable<SalesDetails> SalesDetailsAsNoTracking { get; }
        IQueryable<SalesHead> SalesDetailsWithreturn { get; }

        SalesHead SalesDetailsWithreturnForEdit(string salesHeadNo, int returnHeadID);

        ResultObj  SaveSales(SalesHead salesHead);
        SalesDetails FindSalesDetails(int _salesDetailsID);
        ResultObj UpdateSales(SalesHead salesHead, int prevCustID, decimal prevHeadBalance);
        ResultObj UpdateSalesDetails(SalesDetails salesDetails);
        ResultObj InsertSalesDetails(SalesHead salesHead,SalesDetails salesDetails, int PrevComProductID, decimal prevQty,decimal _prevAMount, int prevCusomerID);
        ResultObj DeleteSalesDetails(SalesHead salesHead, SalesDetails salesDetails, int prevCusomerID);
    }
}
