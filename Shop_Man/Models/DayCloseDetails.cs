using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class DayCloseDetails
    {
        public int DayCloseDetailsID { get; set; }
        public DateTime dDate { get; set; }
        

        public int ChallanNoID { get; set; }
        public string ChallanNo { get; set; }

        public string TransType { get; set; }
        [NotMapped]
        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }



        public string SalesAndReturnType { get; set; }
        public string Describtion { get; set; }


        public decimal ReceiveAmount { get; set; }

        public decimal PaymentAmount { get; set; }


        public decimal SalesAmount { get; set; }
        public decimal SalesQty { get; set; }


        public decimal PurchaseAmount { get; set; }
        public decimal PurchaseQty { get; set; }



        public decimal SalesReturnAmount { get; set; }
        public decimal SalesReturnQty { get; set; }


        public decimal PurchaseReturnAmount { get; set; }
        public decimal PurchaseReturnQty { get; set; }



        public DateTime EntryDate { get; set; }


        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }


        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int UpdateUserID { get; set; }

        public decimal SalesAdjustAmount { get; set; }
        
    }
}
