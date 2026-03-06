using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_Expenses_Datewise
    {

        public string GeneratedAutoSLNo { get; set; }
        public string Type { get; set; }
        public DateTime dDate { get; set; }
        public string FormatedDdate { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }


        public string Name { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
    
    }
}
