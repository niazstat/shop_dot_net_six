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
    public class EFSalesAdjustRepository : ISalesAdjust
    {
        private OrderManagementDBContext context;
        public EFSalesAdjustRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<SalesAdjust> SalesAdjusts =>context.SalesAdjusts.Include(a=>a.Customer);

        public IQueryable<SalesAdjust> SalesAdjustsFull => context.SalesAdjusts.Include(a => a.SalesAdjustDetailsList).ThenInclude(b => b.CompanyProduct).Include(a => a.SalesAdjustDetailsList).ThenInclude(b => b.ProdName).Include(a => a.SalesAdjustDetailsList).ThenInclude(b => b.Article).Include(a => a.SalesAdjustDetailsList).ThenInclude(b => b.Size).Include(a => a.SalesAdjustDetailsList).ThenInclude(b => b.UOM).Include(a => a.Customer);

        public IQueryable<SalesAdjustDetails> SalesAdjustDetails => context.SalesAdjustDetails.Include(a => a.CompanyProduct);

        public IQueryable<SalesAdjust> SalesAdjustAsnoTracking => context.SalesAdjusts.Include(a => a.Customer).AsNoTracking();

        public IQueryable<SalesAdjustDetails> SalesAdjustDetailsAsNoTracking => context.SalesAdjustDetails.Include(a => a.CompanyProduct).AsNoTracking();

        public ResultObj DeleteSalesAdjustDetails(SalesAdjust salesAdjust, SalesAdjustDetails salesAdjustDetails)
        {
            throw new NotImplementedException();
        }

        public SalesAdjustDetails FindSalesAdjustDetails(int _saesDetailsID)
        {
            throw new NotImplementedException();
        }

        public ResultObj InsertSalesAdjustDetails(SalesAdjust salesAdjust, SalesAdjustDetails salesAdjustDetails)
        {

            ResultObj res = new ResultObj();

           // SalesReturn head = context.SalesReturns.SingleOrDefault(a => a.SalesReturnID == salesReturnDetails.SalesReturnID);
            //context.AttachRange(salesDetails.CompanyProduct);
            //context.AttachRange(head);
            //context.AttachRange(salesDetails.Company);








            SalesDetails details = context.SalesDetailss.SingleOrDefault(a => a.SalesDetailsID == salesAdjustDetails.SalesDetailsID);
            details.SalesAdjustRate = salesAdjustDetails.SalesAdjustRate;

            context.Entry(details).State = EntityState.Modified;
            //model.ReyurnQtyInPair = salesReturnDetails.ReyurnQtyInPair;

            context.SalesAdjustDetails.Add(salesAdjustDetails);

            LogEntryEdit _log = new LogEntryEdit();
            _log.ChallanID = salesAdjust.SalesAdjustID; ;
            _log.dDate = salesAdjust.SalesAdjustDate;
            _log.EditItem = "Sales Sales Adjust Details";
            _log.EditType = "Insert";
            _log.IsProcessDone = false;
            string _supp_Cust = salesAdjust.Customer == null ? "No Cust" : salesAdjust.Customer.CustomerID.ToString();
            _log.Remarks = $"Cust Name:{ _supp_Cust} ";
            context.LogEntryEdits.Add(_log);



            try
            {

                context.SaveChanges();
                res.ResultID = 1;
                res.ResultMessage = "Successfully Added  !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;



        }

        public ResultObj SaveSalesAdjust(SalesAdjust salesAdjust)
        {

            ResultObj res = new ResultObj();


            context.Attach(salesAdjust.Company);
            context.Attach(salesAdjust.Customer);
            // decimal _totNetRetAmount = salesReturn.SalesReturnDetailsList.Sum(a => a.ReyurnQtyInPair * (a.RetRate - a.ReturnCommissionRate));

            //if (salesReturn.Customer != null)
            //{
            //    Customer cust = context.Customer.SingleOrDefault(a => a.CustomerID == salesReturn.Customer.CustomerID);
            //    cust.CurrentBalance = cust.CurrentBalance - _totNetRetAmount;

            //    context.Entry(cust).State = EntityState.Modified;
            //}


            foreach (var item in salesAdjust.SalesAdjustDetailsList)
            {

                //CompanyProduct det = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == item.CompanyProductID);
                //det.CurrentStock = det.CurrentStock + item.ReyurnQtyInPair;

                //context.Entry(det).State = EntityState.Modified;

                SalesDetails details = context.SalesDetailss.SingleOrDefault(a => a.SalesDetailsID == item.SalesDetailsID);
                details.SalesAdjustRate = item.SalesAdjustRate;
                    //details.ReturnQtyInPair + item.ReyurnQtyInPair;

                context.Entry(details).State = EntityState.Modified;

            }

            if (salesAdjust.SalesAdjustID == 0)
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = salesAdjust.SalesAdjustID;
                _log.dDate = salesAdjust.SalesAdjustDate;
                _log.EditItem = "Sales Adjest";
                _log.EditType = "New";
                _log.IsProcessDone = false;
                string _supp_Cust = salesAdjust.Customer == null ? "No Cust" : salesAdjust.Customer.CustomerID.ToString();
                _log.Remarks = $"Cust Name:{ _supp_Cust} ";
                context.LogEntryEdits.Add(_log);




                context.SalesAdjusts.Add(salesAdjust).GetDatabaseValues(); ;
            }

            else
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = salesAdjust.SalesAdjustID;
                _log.dDate = salesAdjust.SalesAdjustDate;
                _log.EditItem = "Sales Adjust";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;
                string _supp_Cust = salesAdjust.Customer == null ? "No Cust" : salesAdjust.Customer.CustomerID.ToString();
                _log.Remarks = $"Cust Name:{ _supp_Cust} ";
                context.LogEntryEdits.Add(_log);

            }
            try
            {
                context.SaveChanges();
                context.Entry(salesAdjust).State = EntityState.Detached;
                context.Entry(salesAdjust).Reload();
                res.ResultID = 1;
                res.ResultNo = salesAdjust.GeneratedSalesAdjustNo;
                res.Obj = salesAdjust;
                res.ResultMessage = "Successfully Added /Updated !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;




        }

        public ResultObj UpdateSalesAdjust(SalesAdjust salesAdjust)
        {
            ResultObj res = new ResultObj();

            SalesAdjust head = context.SalesAdjusts.SingleOrDefault(a => a.SalesAdjustID == salesAdjust.SalesAdjustID);
            head.SalesAdjustDate = salesAdjust.SalesAdjustDate;
            context.Entry(head).State = EntityState.Modified;



            try
            {
                context.SaveChanges();

                res.ResultID = 1;

                res.ResultMessage = "Successfully AUpdated !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;
   

        }

        public ResultObj UpdateSalesAdjustDetails(SalesAdjust salesAdjust, SalesAdjustDetails salesAdjustDetails)
        {
            ResultObj res = new ResultObj();

            SalesDetails details = context.SalesDetailss.SingleOrDefault(a => a.SalesDetailsID == salesAdjustDetails.SalesDetailsID);
            details.SalesAdjustRate = salesAdjustDetails.SalesAdjustRate;


            context.Entry(details).State = EntityState.Modified;
            //model.ReyurnQtyInPair = salesReturnDetails.ReyurnQtyInPair;
            context.Entry(salesAdjustDetails).State = EntityState.Modified;


            LogEntryEdit _log = new LogEntryEdit();
            _log.ChallanID = salesAdjustDetails.SalesAdjustDetailsID;
            _log.dDate = salesAdjust.SalesAdjustDate;
            _log.EditItem = "Sales Adjust  Details";
            _log.EditType = "Update";
            _log.IsProcessDone = false;
            string _supp_Cust = salesAdjust.Customer == null ? "No Cust" : salesAdjust.Customer.CustomerID.ToString();
            _log.Remarks = $"Cust Name:{ _supp_Cust} ";
            context.LogEntryEdits.Add(_log);


            try
            {
                context.SaveChanges();

                res.ResultID = 1;

                res.ResultMessage = "Successfully AUpdated !";
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
