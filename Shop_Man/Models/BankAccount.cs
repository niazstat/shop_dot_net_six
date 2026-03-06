using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class BankAccount
    {
        public int BankAccountID { get; set; }

        public DateTime Startdate { get; set; }
        [NotMapped]
        public string StartdateFormatted { get { return String.Format("{0:dd-MMM-yyyy}", Startdate); } }
        public string? AccountHolderName { get; set; }
        public string? AccountName { get; set; }

        public string? Address { get; set; }
        public Bank? Bank { get; set; }
        public string? BankName { get; set; }
        public string? AccountTypeName { get; set; }

        public string? AccountNo { get; set; }
        public decimal StartingBalance { get; set; }

        public decimal CurrentBalance { get; set; }
        public Company Company { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get;  set; }= DateTime.Now;

        public int? UpdateUserID { get; set; }
    }
}
