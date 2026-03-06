using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_CustomersWithLastClosingYear
    {

      public int   CustomerID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address1 { get; set; }
        public decimal OpeningBalance { get; set; }
        public string ShopName { get; set; }

        public decimal MaxBalanceLimit { get; set; }

        public int LastClosingYearName { get; set; }

        public DateTime YearCloseDate { get; set; }

        public string YearCloseDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", YearCloseDate); } }

    }
}
