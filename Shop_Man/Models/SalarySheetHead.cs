using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class SalarySheetHead
    {
        public int SalarySheetHeadID { get; set; }

        public string Name { get; set; }

        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime LastUpdateTime { get; set; }

    }
}
