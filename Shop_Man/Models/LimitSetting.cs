using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class LimitSetting
    {

        public int LimitSettingID { get; set; }
        public DateTime dDate { get; set; }
        [NotMapped]
        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }
        public string Particular { get; set; }

        public decimal LimitAmount { get; set; }

        public Customer Customer { get; set; }

        public Supplier Supplier { get; set; }

        public Employee Employee { get; set; }

        public ExpensesHead ExpensesHead { get; set; }

        public DateTime EntryDate { get; set; }

    }
}
