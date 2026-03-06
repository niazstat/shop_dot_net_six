using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_Entry_Customer_Year_Close
    {
        public int CustomerID { get; set; }
        public int YearName { get; set; }

        public DateTime YearCloseDate { get; set; }

        public string YearCloseDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", YearCloseDate); } }
        public string Note { get; set; }
        public string Name { get; set; }
        public string ShopName { get; set; }
        public decimal OpeningBalance { get; set; }


        public decimal SalesAmount { get; set; }
        public decimal TotalSackNoFee { get; set; }
        public decimal TransportCost { get; set; }
        public decimal PackingCost { get; set; }
        public decimal AddLessAmount { get; set; }
        public decimal ReceiveAmount { get; set; }
        public decimal CashReceiveAmount { get; set; }
        public decimal CheckRecev { get; set; }
        public decimal CheckPayment { get; set; }

        public decimal CashPayment { get; set; }
        public decimal AdjustAmount { get; set; }
        public decimal ReturnAmount { get; set; }
        public decimal ClosingShortAmount { get; set; }

        public decimal RejectGoodsAmount { get; set; }
        public decimal ClosingAmount { get; set; }

        public decimal SalesAdjustAmount { get; set; }

    }
}
