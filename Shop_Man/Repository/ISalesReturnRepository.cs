using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public interface ISalesReturnRepository
    {
        IQueryable<SalesReturn> SalesReturns { get; }
        IQueryable<SalesReturn> SalesReturnFull { get; }

        IQueryable<SalesReturnDetails> SalesReturnDetails { get; }
        IQueryable<SalesReturn> SalesReturnAsnoTracking { get; }
       
        IQueryable<SalesReturnDetails> SalesReturnDetailsAsNoTracking { get; }
        ResultObj SaveSalesReturn(SalesReturn salesReturn);
        SalesReturnDetails FindSalesReturnDetailss(int _salesDetailsID);
        ResultObj UpdateSalesReturn(SalesReturn salesReturn, int prevCustID, decimal prevHeadBalance);
        ResultObj UpdateSalesReturnDetails(SalesReturn salesReturn, SalesReturnDetails salesReturnDetails, decimal prevRetQty);
       
        ResultObj DeleteSalesReturnDetails(SalesReturn salesReturn, SalesReturnDetails salesReturnDetails);

        ResultObj InsertSalesReturnDetails(SalesReturn salesReturn, SalesReturnDetails salesReturnDetails);
    }
}
