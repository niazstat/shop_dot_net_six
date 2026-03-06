using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class DayClose
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

        public decimal CashReceive { get; set; }
        public decimal BkashReceive { get; set; }
        public decimal ChequeReceive { get; set; }
        public decimal OthersReceive { get; set; }

        public decimal CashPayment { get; set; }
        public decimal Bkashpayment { get; set; }
        public decimal ChequePayment { get; set; }
        public decimal OthersPayment { get; set; }



        public decimal TotalSalesReturnAmount { get; set; }

        public decimal TotalPurchaseReturnAmount { get; set; }


        public decimal TotalSalesReturnQty { get; set; }

        public decimal TotalPurchaseReturnQty { get; set; }



        public string Note { get; set; }

        public decimal Balance { get; set; }

        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime EntryDate { get; set; }

        public decimal TotalReceive { get { return BkashReceive + CashReceive + ChequeReceive + OthersReceive; } }

        public decimal TotalPayment { get { return Bkashpayment+ CashPayment+ChequePayment+OthersPayment ; } }


        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int UpdateUserID { get; set; }

        public decimal TotalSalesAdjustAmount { get; set; }

    }
}
