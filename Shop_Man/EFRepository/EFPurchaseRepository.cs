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
    public class EFPurchaseRepository : IPurchaseRepository
    {
        private OrderManagementDBContext context;
        public EFPurchaseRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<PurchaseHead> PurchaseHeads => context.PurchaseHeads.Include(a=>a.Supplier).Include(a => a.PurchaseDetailsList);

        public IQueryable<PurchaseHead> PurchaseHeadInDetails => context.PurchaseHeads.Include(a => a.PurchaseDetailsList).ThenInclude(b => b.CompanyProduct).Include(a => a.PurchaseDetailsList).ThenInclude(b => b.ProdName).Include(a => a.PurchaseDetailsList).ThenInclude(b => b.Article).Include(a => a.PurchaseDetailsList).ThenInclude(b => b.Size).Include(a => a.PurchaseDetailsList).ThenInclude(b => b.UOM).Include(a => a.Supplier);
        public IQueryable<PurchaseHead> PurchaseHeadInDetailsAsnotracking => context.PurchaseHeads.Include(a => a.PurchaseDetailsList).ThenInclude(b => b.CompanyProduct).Include(a => a.PurchaseDetailsList).ThenInclude(b => b.ProdName).Include(a => a.PurchaseDetailsList).ThenInclude(b => b.Article).Include(a => a.PurchaseDetailsList).ThenInclude(b => b.Size).Include(a => a.PurchaseDetailsList).ThenInclude(b => b.UOM).Include(a => a.Supplier).AsNoTracking();


        public IQueryable<PurchaseDetails> PurchaseDetails => throw new NotImplementedException();

        public ResultObj DeletePurchaseDetails(PurchaseHead purchaseHead, PurchaseDetails purchaseDetails,int prevSupplierID)
        {
            ResultObj result = new ResultObj();
            // SalesDetails dbEntry = context.SalesDetailss.SingleOrDefault(p => p.SalesDetailsID == salesDetails.SalesDetailsID && p.SalesHeadID == salesDetails.SalesHeadID && p.CompanyProductID == salesDetails.CompanyProductID);


            if (purchaseDetails != null)
            {
                try
                {


                    LogEntryEdit _log = new LogEntryEdit();
                    _log.ChallanID = purchaseHead.PurchaseHeadID;
                    _log.dDate = purchaseHead.PurchaseDate;
                    _log.EditItem = "Purchase Details";
                    _log.EditType = "Delete";
                    _log.IsProcessDone = false;
                    string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
                    _log.Remarks = $"Supp Name:{ _supp_Cust} ";
                    context.LogEntryEdits.Add(_log);


                    Supplier supp = context.Suppliers.SingleOrDefault(a => a.SupplierId == prevSupplierID);
                    supp.CurrentBalance = supp.CurrentBalance + (purchaseDetails.PurchaseQtyInPairAmount - purchaseDetails.CommissionAmount);
                    context.Entry(supp).State = EntityState.Modified;




                    // string cccc = $"Delete From SalesDetailss where SalesDetailsID={salesDetails.SalesDetailsID} and CompanyProductID={salesDetails.CompanyProductID}  and SalesHeadID={salesDetails.SalesHeadID}";
                    PurchaseDetails pd = context.PurchaseDetails.SingleOrDefault(a => a.CompanyProductID == purchaseDetails.CompanyProductID && a.PurchaseDetailsID == purchaseDetails.PurchaseDetailsID);

                    CompanyProduct det = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == purchaseDetails.CompanyProductID);
                    det.CurrentStock = det.CurrentStock - pd.PurchaseQtyInPair;

                    context.Entry(det).State = EntityState.Modified;

                    context.Database.ExecuteSqlRaw($"Delete From PurchaseDetails where PurchaseDetailsID={purchaseDetails.PurchaseDetailsID} and CompanyProductID={purchaseDetails.CompanyProductID}  and PurchaseHeadID={purchaseDetails.PurchaseHeadID}");
                    context.SaveChanges();
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

        public PurchaseDetails FindPurchaseDetails(int _purchaseDetailsID)
        {
            return context.PurchaseDetails.AsNoTracking().SingleOrDefault(a => a.PurchaseDetailsID == _purchaseDetailsID);
        }

        public ResultObj InsertPurchaseDetails(PurchaseHead purchaseHead, PurchaseDetails purchaseDetails, int PrevComProductID, decimal prevQty,decimal _prevAmount, int prevSupplierID)
        {
            ResultObj res = new ResultObj();

            PurchaseHead head = context.PurchaseHeads.SingleOrDefault(a => a.PurchaseHeadID == purchaseDetails.PurchaseHeadID);
            context.AttachRange(purchaseDetails.CompanyProduct);
            context.AttachRange(head);
            context.AttachRange(purchaseDetails.Company);


            Supplier supp = context.Suppliers.SingleOrDefault(a => a.SupplierId == prevSupplierID);
            supp.CurrentBalance = supp.CurrentBalance -_prevAmount + (purchaseDetails.PurchaseQtyInPairAmount-purchaseDetails.CommissionAmount);
            context.Entry(supp).State = EntityState.Modified;
           


            CompanyProduct CurrentComProduct = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == purchaseDetails.CompanyProductID);
            if (purchaseDetails.PurchaseDetailsID == 0)
            {

                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = purchaseHead.PurchaseHeadID;
                _log.dDate = purchaseHead.PurchaseDate;
                _log.EditItem = "Purchase Details";
                _log.EditType = "Insert";
                _log.IsProcessDone = false;
                string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
                _log.Remarks = $"Supp Name:{ _supp_Cust} ";
                context.LogEntryEdits.Add(_log);





                CurrentComProduct.CurrentStock = CurrentComProduct.CurrentStock + purchaseDetails.PurchaseQtyInPair;
                CurrentComProduct.BuyComm = purchaseDetails.CommissionRate;
                CurrentComProduct.BuyPrice = purchaseDetails.PurchaseRate;
                context.Entry(CurrentComProduct).State = EntityState.Modified;

                context.PurchaseDetails.Add(purchaseDetails);
            }
            else
            {

                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = purchaseHead.PurchaseHeadID;
                _log.dDate = purchaseHead.PurchaseDate;
                _log.EditItem = "Purchase Details";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;
                string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
                _log.Remarks = $"Supp Name:{ _supp_Cust} ";
                context.LogEntryEdits.Add(_log);



                if (PrevComProductID == purchaseDetails.CompanyProductID)
                {
                    CurrentComProduct.CurrentStock = CurrentComProduct.CurrentStock + purchaseDetails.PurchaseQtyInPair - prevQty;

                    CurrentComProduct.BuyComm = purchaseDetails.CommissionRate;
                    CurrentComProduct.BuyPrice = purchaseDetails.PurchaseRate;

                    context.Entry(CurrentComProduct).State = EntityState.Modified;
                }
                else
                {
                    CurrentComProduct.CurrentStock = CurrentComProduct.CurrentStock + purchaseDetails.PurchaseQtyInPair;
                    CurrentComProduct.BuyComm = purchaseDetails.CommissionRate;
                    CurrentComProduct.BuyPrice = purchaseDetails.PurchaseRate;



                    CompanyProduct PreviousProd = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == PrevComProductID);

                    PreviousProd.CurrentStock = PreviousProd.CurrentStock - prevQty;
                    context.Entry(CurrentComProduct).State = EntityState.Modified;
                    context.Entry(PreviousProd).State = EntityState.Modified;
                }

                context.Entry(purchaseDetails).State = EntityState.Modified;
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

        public ResultObj SavePurchase(PurchaseHead purchaseHead)
        {
            ResultObj res = new ResultObj();

            //PaymentMedium pay = context.PaymentMediums.SingleOrDefault(a => a.PaymentMediumID == salesHead.PaymentMediumID);
           // salesHead.PaymentMedium = pay;
            //context.Attach(salesHead.PaymentMedium);
            context.Attach(purchaseHead.Company);
           // context.Attach(purchaseHead.Supplier);

            

            var localCustomer = context.Suppliers.SingleOrDefault(a => a.SupplierId == purchaseHead.Supplier.SupplierId);

            if (localCustomer != null)
            {
                purchaseHead.Supplier = localCustomer;   // 👈 existing tracked entity ব্যবহার করুন
            }
            else
            {
                context.Attach(purchaseHead.Supplier);
            }



            //foreach (var item in purchaseHead.PurchaseDetailsList)
            //{

            //    CompanyProduct det = context.CompanyProducts.SingleOrDefault(a => a.CompanyProductID == item.CompanyProductID);
            //    det.CurrentStock = det.CurrentStock + item.PurchaseQtyInPair;
            //    det.BuyComm =item.CommissionRate;
            //    det.BuyPrice = item.PurchaseRate;
            //    context.Entry(det).State = EntityState.Modified;

            //}

            if (purchaseHead.PurchaseHeadID == 0)
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = purchaseHead.PurchaseHeadID;
                _log.dDate = purchaseHead.PurchaseDate;
                _log.EditItem = "Purchase";
                _log.EditType = "New";
                _log.IsProcessDone = false;
                string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
                _log.Remarks = $"Supp Name:{ _supp_Cust} ";
                context.LogEntryEdits.Add(_log);



                //Supplier supp = context.Suppliers.SingleOrDefault(a => a.SupplierId == purchaseHead.Supplier.SupplierId);
                //supp.CurrentBalance = supp.CurrentBalance + purchaseHead.TotalNetAmount ;
                ///context.Entry(supp).State = EntityState.Modified;
                context.PurchaseHeads.Add(purchaseHead).GetDatabaseValues(); ;
            }

            else
            {
                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = purchaseHead.PurchaseHeadID;
                _log.dDate = purchaseHead.PurchaseDate;
                _log.EditItem = "Purchase";
                _log.EditType = "Update";
                _log.IsProcessDone = false;
                string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
                _log.Remarks = $"Supp Name:{ _supp_Cust} ";
                context.LogEntryEdits.Add(_log);
            }

            try
            {
                context.SaveChanges();
                context.Entry(purchaseHead).State = EntityState.Detached;
                //context.Entry(purchaseHead).Reload();
                res.ResultID = 1;
                res.ResultNo = purchaseHead.GeneratedPurchaseHeadNo;
                res.Obj = purchaseHead;
                res.ResultMessage = "Successfully Added /Updated !";

            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;
        }

        public ResultObj UpdatePurchase(PurchaseHead purchaseHead,int prevSuppID)
        {
            ResultObj res = new ResultObj();

        
            context.Attach(purchaseHead.Company);

           context.AttachRange(purchaseHead.Supplier);


            LogEntryEdit _log = new LogEntryEdit();
            _log.ChallanID = purchaseHead.PurchaseHeadID;
            _log.dDate = purchaseHead.PurchaseDate;
            _log.EditItem = "Purchase";
            _log.EditType = "Update";
            _log.IsProcessDone = false;
            string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
            _log.Remarks = $"Supp Name:{ _supp_Cust} ";
            context.LogEntryEdits.Add(_log);


            if (purchaseHead.Supplier.SupplierId != prevSuppID)// when previous suup bot equal to present then update current Balance/ if not it is already updated where item inserted/Updated
            {
                Supplier supp = context.Suppliers.SingleOrDefault(a => a.SupplierId == prevSuppID);

                supp.CurrentBalance = supp.CurrentBalance - purchaseHead.TotalNetAmount;//ReversEntry
                context.Entry(supp).State = EntityState.Modified;

                Supplier newsupp = context.Suppliers.SingleOrDefault(a => a.SupplierId == purchaseHead.Supplier.SupplierId);

                newsupp.CurrentBalance = newsupp.CurrentBalance + purchaseHead.TotalNetAmount;//ReversEntry
                context.Entry(newsupp).State = EntityState.Modified;
            }

            try
            {
                context.SaveChanges();
                context.Entry(purchaseHead).State = EntityState.Detached;
                context.Entry(purchaseHead).Reload();
                res.ResultID = 1;
                res.ResultNo = purchaseHead.GeneratedPurchaseHeadNo;
                res.Obj = purchaseHead;
                res.ResultMessage = "Successfully Added /Updated !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;
        }

        public ResultObj UpdatePurchaseDetails(PurchaseHead purchaseHead,PurchaseDetails purchaseDetails)
        {
            throw new NotImplementedException();
        }
    }
}
