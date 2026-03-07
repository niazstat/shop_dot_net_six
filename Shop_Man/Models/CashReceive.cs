using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class CashReceive
    {

        public int CashReceiveID { get; set; }

        public string? InvoicNo { get; set; }
        public int InvoicNoSL { get; set; }

        [NotMapped]
        public string GeneratedInvoicNo { get { return "CashR/" + InvoicNo; } }

        public DateTime ReceiveDate { get; set; }
        [NotMapped]
        public string ReceiveDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", ReceiveDate); } }
        public DateTime EntryDate { get; set; } = DateTime.Now;

        public BankAccount? BankAccount { get; set; }
        public Customer? Customer { get; set; }
        public PaymentMedium? PaymentMedium { get; set; }

        public decimal PreviousDue { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get;  set; } = DateTime.Now;

        public int UpdateUserID { get; set; }
    }
}
