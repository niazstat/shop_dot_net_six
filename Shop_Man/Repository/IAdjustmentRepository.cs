using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface IAdjustmentRepository
    {

        IQueryable<Adjustment> Adjustments { get; }
   
        IQueryable<Adjustment> AdjustmentsAsNoTracking { get; }
        ResultObj SaveAdjustment(Adjustment adjustment);
        Adjustment FindAdjustment(int _adjustmentID);
        Adjustment FindAdjustmentByInvoice(string _invoiceNo);
   
        ResultObj DeleteAdjustment(Adjustment adjustment);



    }
}
