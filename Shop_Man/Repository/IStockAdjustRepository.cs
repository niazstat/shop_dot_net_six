using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface IStockAdjustRepository
    {

        IQueryable<StockAdjustHead> StockAdjustHead { get; }
        IQueryable<StockAdjustHead> StockAdjustHeadFull { get; }

        IQueryable<StockAdjustDetails> StockAdjustDetails { get; }
        IQueryable<StockAdjustHead> StockAdjustHeadAsnoTracking { get; }

        IQueryable<StockAdjustDetails> StockAdjustDetailsAsNoTracking { get; }
        ResultObj SaveStockAdjustHead(StockAdjustHead stockAdjustHead);
        StockAdjustDetails FindStockAdjustDetails(int _id);
        ResultObj UpdateStockAdjustHead(StockAdjustHead stockAdjustHead);
        ResultObj UpdateStockAdjustDetails(StockAdjustHead stockAdjustHead, StockAdjustDetails stockAdjustDetails, decimal prevQty);

        ResultObj DeleteStockAdjustDetails(StockAdjustHead stockAdjustHead, StockAdjustDetails stockAdjustDetails);

        ResultObj InsertStockAdjustDetails(StockAdjustHead stockAdjustHead, StockAdjustDetails stockAdjustDetails);


    }
}
