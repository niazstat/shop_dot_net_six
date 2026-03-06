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
    public class EFSalesRepository : ISalesRepository
    {

        private OrderManagementDBContext context;
        public EFSalesRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<SalesHead> SalesHeads => context.SalesHeads.Include(a=>a.SalesDetailsList).Include(a=>a.PaymentMedium).Include(a=>a.Customer);
        public IQueryable<SalesHead> SalesHeadsDetails => context.SalesHeads.Include(a => a.SalesDetailsList).ThenInclude(b => b.CompanyProduct).Include(a => a.SalesDetailsList).ThenInclude(b => b.ProdName).Include(a => a.SalesDetailsList).ThenInclude(b => b.Article).Include(a => a.SalesDetailsList).ThenInclude(b => b.Size).Include(a => a.SalesDetailsList).ThenInclude(b => b.UOM).Include(a => a.PaymentMedium).Include(a => a.Customer).Include(a=>a.User);
        public IQueryable<SalesHead> SalesHeadsDetailsAsnoTracking => context.SalesHeads.Include(a => a.SalesDetailsList).ThenInclude(b => b.CompanyProduct).Include(a => a.SalesDetailsList).ThenInclude(b => b.ProdName).Include(a => a.SalesDetailsList).ThenInclude(b => b.Article).Include(a => a.SalesDetailsList).ThenInclude(b => b.Size).Include(a => a.SalesDetailsList).ThenInclude(b => b.UOM).Include(a => a.PaymentMedium).Include(a => a.Customer).AsNoTracking();

        //

        public IQueryable<SalesDetails> SalesDetails => context.SalesDetailss.Include(a=>a.CompanyProduct);
        public IQueryable<SalesDetails> SalesDetailsAsNoTracking => context.SalesDetailss.Include(a => a.CompanyProduct).Include(b=>b.UOM).AsNoTracking();

        public IQueryable<SalesHead> SalesDetailsWithreturn => context.SalesHeads.Include(a => a.SalesDetailsList).ThenInclude(b => b.CompanyProduct).Include(a => a.SalesDetailsList).ThenInclude(b => b.ProdName).Include(a => a.SalesDetailsList).ThenInclude(b => b.Article).Include(a => a.SalesDetailsList).ThenInclude(b => b.Size).Include(a => a.SalesDetailsList).ThenInclude(b => b.UOM).Include(a => a.PaymentMedium).Include(a => a.Customer).Include(a => a.SalesDetailsList).ThenInclude(a=>a.SalesReturnDetails);


        public SalesHead SalesDetailsWithreturnForEdit(string salesHeadNo, int returnHeadID)
        {
            // return context.SalesHeads.Include(a => a.SalesDetailsList).ThenInclude(b => b.CompanyProduct).Include(a => a.SalesDetailsList).ThenInclude(b => b.ProdName).Include(a => a.SalesDetailsList).ThenInclude(b => b.Article).Include(a => a.SalesDetailsList).ThenInclude(b => b.Size).Include(a => a.SalesDetailsList).ThenInclude(b => b.UOM).Include(a => a.PaymentMedium).Include(a => a.Customer).Include(a => a.SalesDetailsList).ThenInclude(a => a.SalesReturnDetails)
            // .AsNoTracking().AsQueryable()

            //  .SingleOrDefault(a => a.GeneratedSalesNo2 == salesHeadNo);

                    return context.SalesHeads
            .AsNoTracking()
            .Include(a => a.SalesDetailsList)
                .ThenInclude(b => b.CompanyProduct)
            .Include(a => a.SalesDetailsList)
                .ThenInclude(b => b.ProdName)
            .Include(a => a.SalesDetailsList)
                .ThenInclude(b => b.Article)
            .Include(a => a.SalesDetailsList)
                .ThenInclude(b => b.Size)
            .Include(a => a.SalesDetailsList)
                .ThenInclude(b => b.UOM)
            .Include(a => a.SalesDetailsList)
                .ThenInclude(a => a.SalesReturnDetails)
            .Include(a => a.PaymentMedium)
            .Include(a => a.Customer)
            .SingleOrDefault(a => a.GeneratedSalesNo2 == salesHeadNo);



            //if (salesHeadEntity != null)
            //{
            //    salesHeadEntity.SalesDetailsList = result.SalesDetails
            //        .Select(sd => sd.SalesDetail)
            //        .ToList();

            //    foreach (var salesDetail in salesHeadEntity.SalesDetailsList)
            //    {
            //        salesDetail.SalesReturnDetails = sd.FilteredSalesReturnDetails.ToList();
            //    }
            //}



        }

        public ResultObj SaveSales(SalesHead salesHead)
        {
            LogEntryEdit _log = new LogEntryEdit();
            ResultObj res = new ResultObj();

            PaymentMedium pay = context.PaymentMediums.SingleOrDefault(a => a.PaymentMediumID == salesHead.PaymentMediumID);
            salesHead.PaymentMedium = pay;
            context.Attach(salesHead.PaymentMedium);
            context.Attach(salesHead.Company);

            var localCustomer = context.Customer
    .Local
    .FirstOrDefault(c => c.CustomerID == salesHead.Customer.CustomerID);

            if (localCustomer != null)
            {
                salesHead.Customer = localCustomer;   // 👈 existing tracked entity ব্যবহার করুন
            }
            else
            {
                context.Attach(salesHead.Customer);
            }

            if (!salesHead.IsCashSales)
            {
                //Customer cust = context.Customer.SingleOrDefault(a=>a.CustomerID==salesHead.Customer.CustomerID);
                //cust.CurrentBalance = cust.CurrentBalance+ salesHead.TotalAmount+ salesHead.TransportCost - salesHead.TotalCommission - salesHead.ReceiveAmount;
                ////context.AttachRange(cust);
                //context.Entry(cust).State = EntityState.Modified;
            }


            foreach (var item in salesHead.SalesDetailsList)
            {

                //CompanyProduct det = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == item.CompanyProductID);
                //det.CurrentStock = det.CurrentStock - item.SalesQtyInPair;

                //context.Entry(det).State = EntityState.Modified;

            }
          

          
            if (salesHead.SalesHeadID == 0)
            {

                 _log = new LogEntryEdit();
                _log.ChallanID = salesHead.SalesHeadID;
                _log.dDate = salesHead.SalesDate;
                _log.EditItem = "Sales";
                _log.EditType = "New";
                _log.IsProcessDone = false;
                string _supp_Cust = salesHead.Customer == null ? "No Cust" : salesHead.Customer.Name;
                _log.Remarks = $"Cust Name:{ _supp_Cust}";
                context.LogEntryEdits.Add(_log);
         

                context.SalesHeads.Add(salesHead).GetDatabaseValues(); ;
            }

            else
            {
                _log = new LogEntryEdit();
                _log.ChallanID = salesHead.SalesHeadID;
                _log.dDate = salesHead.SalesDate;
                _log.EditItem = "Sales";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;
                string _supp_Cust = salesHead.Customer == null ? "No Cust" : salesHead.Customer.CustomerID.ToString();
                _log.Remarks = $"Cust Name:{ _supp_Cust}";
                context.LogEntryEdits.Add(_log);
            }
            try
            {
                context.SaveChanges();
                context.Entry(salesHead).State = EntityState.Detached;
               // context.Entry(salesHead).Reload();
                res.ResultID = 1;
                res.ResultNo = salesHead.GeneratedSalesNo;
                res.Obj = salesHead;
                res.ResultMessage = "Successfully Added /Updated !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;
        }

        public ResultObj UpdateSales(SalesHead salesHead,int prevCustID, decimal prevHeadBalance)
        {
            ResultObj res = new ResultObj();

            PaymentMedium pay = context.PaymentMediums.SingleOrDefault(a => a.PaymentMediumID == salesHead.PaymentMediumID);
            salesHead.PaymentMedium = pay;
            context.Attach(salesHead.PaymentMedium);
            context.Attach(salesHead.Company);


            LogEntryEdit _log = new LogEntryEdit();
            _log.ChallanID = salesHead.SalesHeadID;
            _log.dDate = salesHead.SalesDate;
            _log.EditItem = "Sales";
            _log.EditType = "Edit";
            _log.IsProcessDone = false;
            string _supp_Cust = salesHead.Customer == null ? "No Cust" : salesHead.Customer.CustomerID.ToString();
            _log.Remarks = $"Cust Name:{ _supp_Cust} previous Cust ID :{prevCustID}";
            context.LogEntryEdits.Add(_log);

            if (prevCustID != 0)
            {
                Customer prevcust = context.Customer.SingleOrDefault(a => a.CustomerID ==prevCustID);

                prevcust.CurrentBalance = prevcust.CurrentBalance - salesHead.TotalAmount  + salesHead.TotalCommission -  prevHeadBalance;//ReversEntry
                context.Entry(prevcust).State = EntityState.Modified;
            }



            if (!salesHead.IsCashSales)
            {
                Customer cust = context.Customer.SingleOrDefault(a => a.CustomerID == salesHead.Customer.CustomerID);

                cust.CurrentBalance = cust.CurrentBalance + salesHead.TotalAmount + salesHead.TransportCost - salesHead.TotalCommission - salesHead.ReceiveAmount;
                //context.AttachRange(cust);
                context.Entry(cust).State = EntityState.Modified;


              //  context.AttachRange(salesHead.Customer);
            }

            try
            {
                context.SaveChanges();
                context.Entry(salesHead).State = EntityState.Detached;
                context.Entry(salesHead).Reload();
                res.ResultID = 1;
                res.ResultNo = salesHead.GeneratedSalesNo;
                res.Obj = salesHead;
                res.ResultMessage = "Successfully Added /Updated !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;
        }



        public ResultObj InsertSalesDetails(SalesHead salesHead, SalesDetails salesDetails, int PrevComProductID, decimal prevQty,decimal _prevAMount, int prevCusomerID)
        {
            ResultObj res = new ResultObj();

           SalesHead head = context.SalesHeads.SingleOrDefault(a=>a.SalesHeadID==salesDetails.SalesHeadID);
            context.AttachRange(salesDetails.CompanyProduct);
           context.AttachRange(head);
            context.AttachRange(salesDetails.Company);

            if (prevCusomerID != 0)
            {
                Customer prevcust = context.Customer.SingleOrDefault(a => a.CustomerID == prevCusomerID);

                prevcust.CurrentBalance = prevcust.CurrentBalance + (salesDetails.SalesQtyInPair * (salesDetails.SalesRate - salesDetails.CommissionRate))+ _prevAMount; 

                context.Entry(prevcust).State = EntityState.Modified;
            }



            CompanyProduct CurrentComProduct = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == salesDetails.CompanyProductID);
            if (salesDetails.SalesDetailsID == 0)
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = salesHead.SalesHeadID;
                _log.dDate = salesHead.SalesDate;
                _log.EditItem = "Sales Details";
                _log.EditType = "New";
                _log.IsProcessDone = false;
                string _supp_Cust = salesHead.Customer == null ? "No Cust" : salesHead.Customer.CustomerID.ToString();
                _log.Remarks = $"Cust Name:{ _supp_Cust} ";
                context.LogEntryEdits.Add(_log);

                CurrentComProduct.CurrentStock = CurrentComProduct.CurrentStock - salesDetails.SalesQtyInPair;

                context.Entry(CurrentComProduct).State = EntityState.Modified;

                context.SalesDetailss.Add(salesDetails);
            }
            else
            {

                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = salesHead.SalesHeadID;
                _log.dDate = salesHead.SalesDate;
                _log.EditItem = "Sales Details";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;
                string _supp_Cust = salesHead.Customer == null ? "No Cust" : salesHead.Customer.CustomerID.ToString();
                _log.Remarks = $"Cust Name:{ _supp_Cust} ";
                context.LogEntryEdits.Add(_log);

                if (PrevComProductID==salesDetails.CompanyProductID)
                {
                    CurrentComProduct.CurrentStock = CurrentComProduct.CurrentStock - salesDetails.SalesQtyInPair+ prevQty;

                    context.Entry(CurrentComProduct).State = EntityState.Modified;
                }
                else
                {
                    CurrentComProduct.CurrentStock = CurrentComProduct.CurrentStock - salesDetails.SalesQtyInPair ;

                    CompanyProduct PreviousProd = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == PrevComProductID);

                    PreviousProd.CurrentStock = PreviousProd.CurrentStock + prevQty;
                    context.Entry(CurrentComProduct).State = EntityState.Modified;
                    context.Entry(PreviousProd).State = EntityState.Modified;
                }

                context.Entry(salesDetails).State = EntityState.Modified;
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

        public ResultObj UpdateSalesDetails(SalesDetails salesDetails)
        {
            throw new NotImplementedException();
        }

        public ResultObj DeleteSalesDetails(SalesHead salesHead, SalesDetails salesDetails, int prevCusomerID)
        {
            ResultObj result = new ResultObj();
            // SalesDetails dbEntry = context.SalesDetailss.SingleOrDefault(p => p.SalesDetailsID == salesDetails.SalesDetailsID && p.SalesHeadID == salesDetails.SalesHeadID && p.CompanyProductID == salesDetails.CompanyProductID);


            if (salesDetails != null)
            {
                try
                {

                    // string cccc = $"Delete From SalesDetailss where SalesDetailsID={salesDetails.SalesDetailsID} and CompanyProductID={salesDetails.CompanyProductID}  and SalesHeadID={salesDetails.SalesHeadID}";
                    SalesDetails sd = context.SalesDetailss.SingleOrDefault(a => a.CompanyProductID == salesDetails.CompanyProductID && a.SalesDetailsID == salesDetails.SalesDetailsID);

                    if (prevCusomerID != 0)
                    {
                        Customer prevcust = context.Customer.SingleOrDefault(a => a.CustomerID == prevCusomerID);

                        prevcust.CurrentBalance = prevcust.CurrentBalance - (sd.SalesQtyInPair * (sd.SalesRate - sd.CommissionRate));

                        context.Entry(prevcust).State = EntityState.Modified;
                    }

                    CompanyProduct det = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == salesDetails.CompanyProductID);
                    det.CurrentStock = det.CurrentStock + sd.SalesQtyInPair;

                    context.Entry(det).State = EntityState.Modified;

                    context.Database.ExecuteSqlRaw($"Delete From SalesDetailss where SalesDetailsID={salesDetails.SalesDetailsID} and CompanyProductID={salesDetails.CompanyProductID}  and SalesHeadID={salesDetails.SalesHeadID}");

                    LogEntryEdit _log = new LogEntryEdit();
                    _log.ChallanID = salesHead.SalesHeadID;
                    _log.dDate = salesHead.SalesDate;
                    _log.EditItem = "Sales Details";
                    _log.EditType = "Delete";
                    _log.IsProcessDone = false;
                    string _supp_Cust = salesHead.Customer == null ? "No Cust" : salesHead.Customer.CustomerID.ToString();
                    _log.Remarks = $"Cust Name:{ _supp_Cust} ";
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

        public SalesDetails FindSalesDetails(int _salesDetailsID)
        {
           return context.SalesDetailss.AsNoTracking().SingleOrDefault(a=>a.SalesDetailsID== _salesDetailsID);
        }

     
    }
}
