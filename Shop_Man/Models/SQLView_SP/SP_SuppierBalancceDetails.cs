using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_SuppierBalancceDetails
    {

        public int ID { get; set; }

        public string Type { get; set; }

        public string AutoNo { get; set; }
        public string Note { get; set; }
        public DateTime dDate { get; set; }

        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }

        public decimal OpeningBalance { get; set; }

        public decimal PurchaseAmount { get; set; }
        public decimal CashPaymentAmount { get; set; }
        public decimal CheckRecev { get; set; }
        public decimal CheckPayment { get; set; }
        public decimal AdjustAmount { get; set; }
        public decimal ClosingShortAmount { get; set; }
        public decimal RejectGoodsAmount { get; set; }
    }
}
