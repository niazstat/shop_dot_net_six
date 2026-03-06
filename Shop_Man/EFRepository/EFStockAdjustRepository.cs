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
    public class EFStockAdjustRepository : IStockAdjustRepository
    {
        private OrderManagementDBContext context;
        public EFStockAdjustRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }


        public IQueryable<StockAdjustHead> StockAdjustHead => context.StockAdjustHead.Include(a => a.StockAdjustDetailsList);

        public IQueryable<StockAdjustHead> StockAdjustHeadFull => context.StockAdjustHead.Include(a => a.StockAdjustDetailsList).ThenInclude(b => b.CompanyProduct).Include(a => a.StockAdjustDetailsList).ThenInclude(b => b.ProdName).Include(a => a.StockAdjustDetailsList).ThenInclude(b => b.Article).Include(a => a.StockAdjustDetailsList).ThenInclude(b => b.Size).Include(a => a.StockAdjustDetailsList).ThenInclude(b => b.UOM).Include(a => a.User);

        public IQueryable<StockAdjustDetails> StockAdjustDetails => context.StockAdjustDetails.Include(a => a.CompanyProduct);

        public IQueryable<StockAdjustHead> StockAdjustHeadAsnoTracking => context.StockAdjustHead.Include(a => a.StockAdjustDetailsList).ThenInclude(b => b.CompanyProduct).Include(a => a.StockAdjustDetailsList).ThenInclude(b => b.ProdName).Include(a => a.StockAdjustDetailsList).ThenInclude(b => b.Article).Include(a => a.StockAdjustDetailsList).ThenInclude(b => b.Size).Include(a => a.StockAdjustDetailsList).ThenInclude(b => b.UOM).AsNoTracking();

        public IQueryable<StockAdjustDetails> StockAdjustDetailsAsNoTracking => context.StockAdjustDetails.Include(a => a.CompanyProduct);

        public ResultObj DeleteStockAdjustDetails(StockAdjustHead stockAdjustHead, StockAdjustDetails stockAdjustDetails)
        {

            ResultObj result = new ResultObj();
            // SalesDetails dbEntry = context.SalesDetailss.SingleOrDefault(p => p.SalesDetailsID == salesDetails.SalesDetailsID && p.SalesHeadID == salesDetails.SalesHeadID && p.CompanyProductID == salesDetails.CompanyProductID);


            if (stockAdjustDetails != null)
            {
                try
                {

                    // string cccc = $"Delete From SalesDetailss where SalesDetailsID={salesDetails.SalesDetailsID} and CompanyProductID={salesDetails.CompanyProductID}  and SalesHeadID={salesDetails.SalesHeadID}";
                    StockAdjustDetails sd = context.StockAdjustDetails.SingleOrDefault(a => a.CompanyProductID == stockAdjustDetails.CompanyProductID && a.StockAdjustDetailsID == stockAdjustDetails.StockAdjustDetailsID);

                    context.Database.ExecuteSqlRaw($"Delete From StockAdjustDetails where StockAdjustDetailsID={stockAdjustDetails.StockAdjustDetailsID} and CompanyProductID={stockAdjustDetails.CompanyProductID}  and StockAdjustHeadID={stockAdjustDetails.StockAdjustHeadID}");

                    LogEntryEdit _log = new LogEntryEdit();
                    _log.ChallanID = stockAdjustHead.StockAdjustHeadID;
                    _log.dDate = stockAdjustHead.AdjustDate;
                    _log.EditItem = "Stock Adjust Details";
                    _log.EditType = "Delete";
                    _log.IsProcessDone = false;

                    context.LogEntryEdits.Add(_log);

                    context.SaveChanges();
                    context.Entry(sd).State = EntityState.Detached;
                    result.ResultID = 1;
                    result.ResultMessage = "Items Deleted";
                }
                catch (Exception ex)
                {
                    result.ResultID = -1;
                    result.ResultMessage = ex.ToString();
                }
            }


            return result;



        }

        public StockAdjustDetails FindStockAdjustDetails(int _id)
        {

            return context.StockAdjustDetails.AsNoTracking().SingleOrDefault(a => a.StockAdjustDetailsID == _id);


        }

        public ResultObj InsertStockAdjustDetails(StockAdjustHead stockAdjustHead, StockAdjustDetails stockAdjustDetails)
        {

            ResultObj res = new ResultObj();

            StockAdjustHead head = context.StockAdjustHead.SingleOrDefault(a => a.StockAdjustHeadID == stockAdjustDetails.StockAdjustHeadID);
            context.AttachRange(stockAdjustDetails.CompanyProduct);
            context.AttachRange(head);
            context.AttachRange(stockAdjustDetails.Company);




            CompanyProduct CurrentComProduct = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == stockAdjustDetails.CompanyProductID);
            if (stockAdjustDetails.StockAdjustDetailsID == 0)
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = stockAdjustHead.StockAdjustHeadID;
                _log.dDate = stockAdjustHead.AdjustDate;
                _log.EditItem = "Stock Adjust  Details";
                _log.EditType = "New";
                _log.IsProcessDone = false;

                context.LogEntryEdits.Add(_log);





                context.StockAdjustDetails.Add(stockAdjustDetails);
            }
            else
            {

                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = stockAdjustHead.StockAdjustHeadID;
                _log.dDate = stockAdjustHead.AdjustDate;
                _log.EditItem = "Stock Adjust Details";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;

                context.LogEntryEdits.Add(_log);







                context.Entry(stockAdjustDetails).State = EntityState.Modified;
            }


            try
            {

                context.SaveChanges();
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

        public ResultObj SaveStockAdjustHead(StockAdjustHead stockAdjustHead)
        {

            LogEntryEdit _log = new LogEntryEdit();
            ResultObj res = new ResultObj();



            context.Attach(stockAdjustHead.Company);




            //foreach (var item in stockAdjustHead.StockAdjustDetailsList)
            //{

            //    CompanyProduct det = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == item.CompanyProductID);
            //    det.CurrentStock = det.CurrentStock - item.SalesQtyInPair;

            //    context.Entry(det).State = EntityState.Modified;

            //}



            if (stockAdjustHead.StockAdjustHeadID == 0)
            {

                _log = new LogEntryEdit();
                _log.ChallanID = stockAdjustHead.StockAdjustHeadID;
                _log.dDate = stockAdjustHead.AdjustDate;
                _log.EditItem = "Stock Adjust";
                _log.EditType = "New";
                _log.IsProcessDone = false;

                context.LogEntryEdits.Add(_log);


                context.StockAdjustHead.Add(stockAdjustHead).GetDatabaseValues(); ;
            }

            else
            {
                _log = new LogEntryEdit();
                _log.ChallanID = stockAdjustHead.StockAdjustHeadID;
                _log.dDate = stockAdjustHead.AdjustDate;
                _log.EditItem = "Stock Adjust";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;

                context.LogEntryEdits.Add(_log);
            }
            try
            {
                context.SaveChanges();
                context.Entry(stockAdjustHead).State = EntityState.Detached;
                context.Entry(stockAdjustHead).Reload();
                res.ResultID = 1;
                res.ResultNo = stockAdjustHead.GeneratedStockAdjustNo;
                res.Obj = stockAdjustHead;
                res.ResultMessage = "Successfully Added /Updated !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;




        }

        public ResultObj UpdateStockAdjustDetails(StockAdjustHead stockAdjustHead, StockAdjustDetails stockAdjustDetails, decimal prevQty)
        {
            throw new NotImplementedException();
        }

        public ResultObj UpdateStockAdjustHead(StockAdjustHead stockAdjustHead)
        {

            ResultObj res = new ResultObj();

            LogEntryEdit _log = new LogEntryEdit();
            _log.ChallanID = stockAdjustHead.StockAdjustHeadID;
            _log.dDate = stockAdjustHead.AdjustDate;
            _log.EditItem = "Stock Adjust";
            _log.EditType = "Edit";
            _log.IsProcessDone = false;
            context.LogEntryEdits.Add(_log);


            try
            {
                context.SaveChanges();
                context.Entry(stockAdjustHead).State = EntityState.Detached;
                context.Entry(stockAdjustHead).Reload();
                res.ResultID = 1;
                res.ResultNo = stockAdjustHead.GeneratedStockAdjustNo;
                res.Obj = stockAdjustHead;
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
