using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
   public interface ISalesAdjust
    {

        IQueryable<SalesAdjust> SalesAdjusts { get; }
        IQueryable<SalesAdjust> SalesAdjustsFull { get; }


        IQueryable<SalesAdjustDetails> SalesAdjustDetails { get; }
        IQueryable<SalesAdjust> SalesAdjustAsnoTracking { get; }


        IQueryable<SalesAdjustDetails> SalesAdjustDetailsAsNoTracking { get; }
        ResultObj SaveSalesAdjust(SalesAdjust salesAdjust);
        SalesAdjustDetails FindSalesAdjustDetails(int _saesDetailsID);

        ResultObj UpdateSalesAdjust(SalesAdjust salesAdjust);
        ResultObj UpdateSalesAdjustDetails(SalesAdjust salesAdjust, SalesAdjustDetails salesAdjustDetails);

        ResultObj DeleteSalesAdjustDetails(SalesAdjust salesAdjust, SalesAdjustDetails salesAdjustDetails);

        ResultObj InsertSalesAdjustDetails(SalesAdjust salesAdjust, SalesAdjustDetails salesAdjustDetails);


    }
}
