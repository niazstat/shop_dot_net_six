using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_Entry_DayClose
    {
        public int DayCloseID { get; set; }

        public int SlNo { get; set; }

        public DateTime dDate { get; set; }
        [NotMapped]
        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }
 
        public decimal PrevBalance { get; set; }
        public decimal AddLess { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalPurchase { get; set; }
        public decimal TotalSalesQty { get; set; }
        public decimal TotalPurchaseQty { get; set; }

        public decimal TotalPurchaseReturnAmount { get; set; }
        public decimal TotalPurchaseReturnQty { get; set; }
        public decimal TotalSalesReturnAmount { get; set; }
        public decimal TotalSalesReturnQty { get; set; }
 

        public decimal CashReceive { get; set; }
        public decimal BkashReceive { get; set; }
        public decimal ChequeReceive { get; set; }
        public decimal OthersReceive { get; set; }
        public decimal CashPayment { get; set; }
        public decimal Bkashpayment { get; set; }
        public decimal ChequePayment { get; set; }
        public decimal OthersPayment { get; set; }
        public decimal Balance { get; set; }
        public string Note { get; set; }

        public int CompanyID { get; set; }

        public decimal TotalReceive { get { return BkashReceive + CashReceive + ChequeReceive + OthersReceive; } }

        public decimal TotalPayment { get { return Bkashpayment + CashPayment + ChequePayment + OthersPayment; } }
    }
}
