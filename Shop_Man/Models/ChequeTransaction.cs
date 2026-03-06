using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class ChequeTransaction
    {
        public int ChequeTransactionID { get; set; }

        public string InvoicNo { get; set; }
        public int InvoicNoSL { get; set; }
        [NotMapped]
        public string GeneratedInvoicNo { get { return "Chq/" + InvoicNo; } }

        public DateTime TranDate { get; set; }

        [NotMapped]
        public string TranDateFormatted { get { return String.Format("{0:dd-MMM-yyyy}", TranDate); } }
        public string? Type { get; set; } //Recive / Payment

        public PaymentMedium? PaymentMedium { get; set; }

        public Supplier? Supplier { get; set; }

        public Customer? Customer { get; set; }
        public string? LedgerName { get; set; }

        public decimal Amount { get; set; }

        public BankAccount? BankAccount { get; set; }

        public string? Note { get; set; }

        public string? ChequeTTNo { get; set; }

        public User User { get; set; }

        public bool IsChequePassed { get; set; }


        public DateTime ChequePassDate { get; set; }

        [NotMapped]
        public string ChequePassDateFormatted { get { return String.Format("{0:dd-MMM-yyyy}", ChequePassDate); } }

        public DateTime EntryDate { get; set; } = DateTime.Now;

   
        public Company Company { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; } = DateTime.Now;

        public int UpdateUserID { get; set; }

    }
}
