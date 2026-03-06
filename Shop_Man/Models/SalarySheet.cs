using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class SalarySheet
    {

        public int SalarySheetID { get; set; }

        public int AutoSalarySheetNo { get; set; }
        public string GeneratedSalarySheetNo { get; set; }
        [NotMapped]
        public string GeneratedSalarySheetNo2 { get { return "SLR/" + GeneratedSalarySheetNo; } }


        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public int YearName { get; set; }
        public string MonthName { get; set; }
        public DateTime PayDate { get; set; }
       
     
        [NotMapped]
        public string PayDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", PayDate); } }

        public decimal Amount { get; set; }

        public decimal AddAmount { get; set; }

        public decimal DeductAmount { get; set; }


        [NotMapped]
        public decimal NetPayAmount { get { return Amount + AddAmount - DeductAmount; } }

        public int SalarySheetHeadID { get; set; }
        public SalarySheetHead SalarySheetHead { get; set; }
        public string Remarks { get; set; }

        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public bool IsAllowEdit { get; set; }
   

        public int UpdateUserID { get; set; }
    }
}
