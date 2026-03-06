using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class ExpensesReportsViewModel
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }


        public string ExpensesHeadIDs { get; set; }


        public string Type { get; set; }

        public string IsDetails { get; set; }

        public string RecvPayType { get; set; }
    }
}
