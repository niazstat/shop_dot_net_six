using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class CashPayment
    {

        public int CashPaymentID { get; set; }
        public string? InvoicNo { get; set; }
        public int InvoicNoSL { get; set; }

        [NotMapped]
        public string GeneratedInvoicNo { get { return "CashP/" + InvoicNo; } }

        public DateTime PaymentDate { get; set; }
        [NotMapped]
        public string PaymentDateDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", PaymentDate); } }
        public DateTime EntryDate { get; set; } = DateTime.Now;

        public Supplier? Supplier { get; set; }
        public Customer? Customer { get; set; }
        public PaymentMedium? PaymentMedium { get; set; }
        public BankAccount? BankAccount { get; set; }
        public decimal PreviousDue { get; set; }
        public decimal Amount { get; set; }

        public User User { get; set; }

        public string? Note { get; set; }
        public Company? Company { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;

        public int UpdateUserID { get; set; }

    }
}
