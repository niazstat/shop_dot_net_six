using System;

namespace Shop_Man.Models
{
    public class ExpensesHead
    {
        public int ExpensesHeadID { get; set; }
 
        public string Name { get; set; }

        public bool IsEmployeeExpenseHead { get; set; }
        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime LastUpdateTime { get; set; }
        public bool IsAllowEdit { get; set; }
        public decimal MaximumLimit { get; internal set; }

          public int UpdateUserID { get; set; }
    }
}
