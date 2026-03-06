using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class Adjustment
    {
        public int AdjustmentID { get; set; }
        public string InvoicNo { get; set; }
        public int InvoicNoSL { get; set; }

        [NotMapped]
        public string GeneratedInvoicNo { get { return "Adj/" + InvoicNo; } }

        public DateTime PaymentDate { get; set; }
        [NotMapped]
        public string PaymentDateDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", PaymentDate); } }
        public DateTime EntryDate { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public Supplier Supplier { get; set; }
        public Customer Customer { get; set; }
      
  
        public decimal PreviousDue { get; set; }
        public decimal Amount { get; set; }

        public decimal ClosingShortAmount { get; set; }
        public decimal RejectGoodsAmount { get; set; }
        public User User { get; set; }

        public string Note { get; set; }
        public Company Company { get; set; }


        public bool IsAllowEdit { get; set; }

        public decimal MaxAdjLimit { get; set; }
        public int UpdateUserID { get; set; }
    }
}
