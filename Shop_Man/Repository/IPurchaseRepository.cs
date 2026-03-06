using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
   public interface IPurchaseRepository
    {
        IQueryable<PurchaseHead> PurchaseHeads { get; }
        IQueryable<PurchaseHead> PurchaseHeadInDetails { get; }

        IQueryable<PurchaseHead> PurchaseHeadInDetailsAsnotracking { get; }
        IQueryable<PurchaseDetails> PurchaseDetails { get; }
        ResultObj SavePurchase(PurchaseHead purchaseHead);
        PurchaseDetails FindPurchaseDetails(int _purchaseDetailsID);
        ResultObj UpdatePurchase(PurchaseHead purchaseHead,int prevSuppID);
        ResultObj UpdatePurchaseDetails(PurchaseHead purchaseHead,PurchaseDetails purchaseDetails);
        ResultObj InsertPurchaseDetails(PurchaseHead purchaseHead,PurchaseDetails purchaseDetails, int PrevComProductID, decimal prevQty,decimal _prevAmount, int prevSupplierID);
        ResultObj DeletePurchaseDetails(PurchaseHead purchaseHead,PurchaseDetails purchaseDetails, int prevSupplierID);
    }
}
