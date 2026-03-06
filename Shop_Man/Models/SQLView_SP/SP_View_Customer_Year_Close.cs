using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_View_Customer_Year_Close
    {
        public string Type { get; set; }

        public DateTime YearCloseDate { get; set; }

        public string YearCloseDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", YearCloseDate); } }
        public string AutoNo { get; set; }
        public string Note { get; set; }
        public DateTime dDate { get; set; }

        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }
        public decimal OpeningBalance { get; set; }

        public decimal SalesAmount { get; set; }

        public decimal PurchaseAmount { get; set; }
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

        public decimal RetPurchaseAmount { get; set; }


        public decimal SalesAdjustAmount { get; set; }


    }
}
