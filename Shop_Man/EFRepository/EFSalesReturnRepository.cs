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
    public class EFSalesReturnRepository : ISalesReturnRepository
    {


        private OrderManagementDBContext context;
        public EFSalesReturnRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<SalesReturn> SalesReturns => context.SalesReturns.Include(a=>a.Customer);

        public IQueryable<SalesReturn> SalesReturnFull => context.SalesReturns.Include(a => a.SalesReturnDetailsList).ThenInclude(b => b.CompanyProduct).Include(a => a.SalesReturnDetailsList).ThenInclude(b => b.ProdName).Include(a => a.SalesReturnDetailsList).ThenInclude(b => b.Article).Include(a => a.SalesReturnDetailsList).ThenInclude(b => b.Size).Include(a => a.SalesReturnDetailsList).ThenInclude(b => b.UOM).Include(a => a.Customer);

        public IQueryable<SalesReturnDetails> SalesReturnDetails => context.SalesReturnDetails.Include(a => a.CompanyProduct);

        public IQueryable<SalesReturn> SalesReturnAsnoTracking => context.SalesReturns.Include(a => a.Customer).AsNoTracking();

        public IQueryable<SalesReturnDetails> SalesReturnDetailsAsNoTracking => context.SalesReturnDetails.Include(a => a.CompanyProduct).AsNoTracking();

        public ResultObj DeleteSalesReturnDetails(SalesReturn salesReturn, SalesReturnDetails salesReturnDetails)
        {
            ResultObj res = new ResultObj();


            LogEntryEdit _log = new LogEntryEdit();
            _log.ChallanID = salesReturn.SalesReturnID;
            _log.dDate = salesReturn.ReturnDate;
            _log.EditItem = "Sales Return Details";
            _log.EditType = "Delete";
            _log.IsProcessDone = false;
            string _supp_Cust = salesReturn.Customer == null ? "No Cust" : salesReturn.Customer.CustomerID.ToString();
            _log.Remarks = $"Cust Name:{ _supp_Cust} ";
            context.LogEntryEdits.Add(_log);



            CompanyProduct det = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == salesReturnDetails.CompanyProductID);
            det.CurrentStock = det.CurrentStock - salesReturnDetails.ReyurnQtyInPair;

            context.Entry(det).State = EntityState.Modified;

            SalesDetails details = context.SalesDetailss.SingleOrDefault(a => a.SalesDetailsID == salesReturnDetails.SalesDetailsID);
            details.ReturnQtyInPair = details.ReturnQtyInPair - salesReturnDetails.ReyurnQtyInPair;

            context.Entry(details).State = EntityState.Modified;





            context.SalesReturnDetails.Remove(salesReturnDetails);


            try
            {
                context.SaveChanges();

                res.ResultID = 1;
                res.ResultMessage = "Successfully Deleted!";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;
        }

        public SalesReturnDetails FindSalesReturnDetailss(int _salesDetailsID)
        {
            throw new NotImplementedException();
        }

        public ResultObj InsertSalesReturnDetails(SalesReturn salesReturn, SalesReturnDetails salesReturnDetails)
        {

            ResultObj res = new ResultObj();

            SalesReturn head = context.SalesReturns.SingleOrDefault(a => a.SalesReturnID == salesReturnDetails.SalesReturnID);
            //context.AttachRange(salesDetails.CompanyProduct);
            //context.AttachRange(head);
            //context.AttachRange(salesDetails.Company);

        




            CompanyProduct det = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == salesReturnDetails.CompanyProductID);
            det.CurrentStock = det.CurrentStock + salesReturnDetails.ReyurnQtyInPair;

            context.Entry(det).State = EntityState.Modified;

            SalesDetails details = context.SalesDetailss.SingleOrDefault(a => a.SalesDetailsID == salesReturnDetails.SalesDetailsID);
            details.ReturnQtyInPair = details.ReturnQtyInPair  + salesReturnDetails.ReyurnQtyInPair;


            context.Entry(details).State = EntityState.Modified;
            //model.ReyurnQtyInPair = salesReturnDetails.ReyurnQtyInPair;
            
            context.SalesReturnDetails.Add(salesReturnDetails);

            LogEntryEdit _log = new LogEntryEdit();
            _log.ChallanID = salesReturn.SalesReturnID;
            _log.dDate = salesReturn.ReturnDate;
            _log.EditItem = "Sales Return Details";
            _log.EditType = "Insert";
            _log.IsProcessDone = false;
            string _supp_Cust = salesReturn.Customer == null ? "No Cust" : salesReturn.Customer.CustomerID.ToString();
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

        public ResultObj SaveSalesReturn(SalesReturn salesReturn)
        {
            ResultObj res = new ResultObj();

  
            context.Attach(salesReturn.Company);
            decimal _totNetRetAmount = salesReturn.SalesReturnDetailsList.Sum(a => a.ReyurnQtyInPair * (a.RetRate - a.ReturnCommissionRate));

            if (salesReturn.Customer !=null)
            {
                //Customer cust = context.Customer.SingleOrDefault(a => a.CustomerID == salesReturn.Customer.CustomerID);
                //cust.CurrentBalance = cust.CurrentBalance - _totNetRetAmount;

                //context.Entry(cust).State = EntityState.Modified;
            }

            var localCustomer = context.Customer
    .Local
    .FirstOrDefault(c => c.CustomerID == salesReturn.Customer.CustomerID);

            if (localCustomer != null)
            {
                salesReturn.Customer = localCustomer;   // 👈 existing tracked entity 
            }
            else
            {
                context.Attach(salesReturn.Customer);
            }

            foreach (var item in salesReturn.SalesReturnDetailsList)
            {
                //context.Attach(item.UOM);
                //CompanyProduct det = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == item.CompanyProductID);
                //det.CurrentStock = det.CurrentStock + item.ReyurnQtyInPair;

                //context.Entry(det).State = EntityState.Modified;

                SalesDetails details = context.SalesDetailss.SingleOrDefault(a => a.SalesDetailsID == item.SalesDetailsID);
                details.ReturnQtyInPair = details.ReturnQtyInPair + item.ReyurnQtyInPair;

                context.Entry(details).State = EntityState.Modified;

            }

            if (salesReturn.SalesReturnID == 0)
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = salesReturn.SalesReturnID;
                _log.dDate = salesReturn.ReturnDate;
                _log.EditItem = "Sales Return";
                _log.EditType = "New";
                _log.IsProcessDone = false;
                string _supp_Cust = salesReturn.Customer == null ? "No Cust" : salesReturn.Customer.CustomerID.ToString();
                _log.Remarks = $"Cust Name:{ _supp_Cust} ";
                context.LogEntryEdits.Add(_log);




                context.SalesReturns.Add(salesReturn).GetDatabaseValues(); ;
            }

            else
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = salesReturn.SalesReturnID;
                _log.dDate = salesReturn.ReturnDate;
                _log.EditItem = "Sales Return";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;
                string _supp_Cust = salesReturn.Customer == null ? "No Cust" : salesReturn.Customer.CustomerID.ToString();
                _log.Remarks = $"Cust Name:{ _supp_Cust} ";
                context.LogEntryEdits.Add(_log);

            }
            try
            {
                context.SaveChanges();
                context.Entry(salesReturn).State = EntityState.Detached;
                //context.Entry(salesReturn).Reload();
                res.ResultID = 1;
                res.ResultNo = salesReturn.GeneratedReturnNo;
                res.Obj = salesReturn;
                res.ResultMessage = "Successfully Added /Updated !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;


        }

        public ResultObj UpdateSalesReturn(SalesReturn salesReturn, int prevCustID, decimal prevHeadBalance)
        {
            throw new NotImplementedException();
        }

        public ResultObj UpdateSalesReturnDetails(SalesReturn salesReturn, SalesReturnDetails salesReturnDetails,decimal prevRetQty)
        {
            ResultObj res = new ResultObj();


            //SalesReturnDetails model = context.SalesReturnDetails.SingleOrDefault(a => a.SalesReturnDetailsID == salesReturnDetails.SalesReturnDetailsID);


            CompanyProduct det = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == salesReturnDetails.CompanyProductID);
            det.CurrentStock = det.CurrentStock - prevRetQty+ salesReturnDetails.ReyurnQtyInPair;

            context.Entry(det).State = EntityState.Modified;

            SalesDetails details = context.SalesDetailss.SingleOrDefault(a => a.SalesDetailsID == salesReturnDetails.SalesDetailsID);
            details.ReturnQtyInPair = details.ReturnQtyInPair - prevRetQty + salesReturnDetails.ReyurnQtyInPair;


            context.Entry(details).State = EntityState.Modified;
            //model.ReyurnQtyInPair = salesReturnDetails.ReyurnQtyInPair;
            context.Entry(salesReturnDetails).State = EntityState.Modified;


            LogEntryEdit _log = new LogEntryEdit();
            _log.ChallanID = salesReturn.SalesReturnID;
            _log.dDate = salesReturn.ReturnDate;
            _log.EditItem = "Sales Return Details";
            _log.EditType = "Update";
            _log.IsProcessDone = false;
            string _supp_Cust = salesReturn.Customer == null ? "No Cust" : salesReturn.Customer.CustomerID.ToString();
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
