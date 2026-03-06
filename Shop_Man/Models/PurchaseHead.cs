using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class PurchaseHead
    {
        public int PurchaseHeadID { get; set; }
        public int SuppwisePurNo { get; set; }
        public string? PurchaseHeadNo { get; set; }

        public int AutoPurchaseHeadNo { get; set; }
        public string? GeneratedPurchaseHeadNo { get; set; }
        [NotMapped]
        public string GeneratedPurchaseHeadNo2 { get { return "PUR/" + GeneratedPurchaseHeadNo; } }

        public bool IsCashPurchase { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime PurchaseDate { get; set; }
        [NotMapped]
        public string PurchaseDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", PurchaseDate); } }
        public decimal TotalAmount { get; set; }

        public decimal ReceiveAmount { get; set; }
        public decimal TotalCommission { get; set; }

        public decimal TransportCost { get; set; }
     

        public decimal TotalNetAmount { get; set; }



 
      
      
        public string? Note1 { get; set; }

        public string? Note2 { get; set; }


        public ICollection<PurchaseDetails> PurchaseDetailsList { get; set; }

        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime EntryDate { get; set; }


        public string? SuppChallanNo { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int UpdateUserID { get; set; }
    }
}
