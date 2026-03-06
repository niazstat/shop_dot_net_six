using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class Expense
    {
        public int ExpenseID { get; set; }




        public DateTime TranDate { get; set; }
        [NotMapped]
        public string TranDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", TranDate); } }

        public int AutoSLNo { get; set; }
        public string? GeneratedAutoSLNo { get; set; }
        [NotMapped]
        public string GeneratedAutoSLNo2 { get { return "Exp/" + GeneratedAutoSLNo; } }


        public int ExpensesHeadID { get; set; }
        public ExpensesHead ExpensesHead { get; set; }
        public string? Note { get; set; }

        public string? Type { get; set; } //Receive or Payment
        public decimal Amount { get; set; }

        public bool IsEmployeeExpenses { get; set; }
        public Employee? Employee { get; set; }


        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime EntryDate { get; set; } = DateTime.Now;

        public DateTime LastUpdateTime { get; set; } = DateTime.Now;


        public bool IsAllowEdit { get; set; }

    
        public int UpdateUserID { get; set; }

    }
}
