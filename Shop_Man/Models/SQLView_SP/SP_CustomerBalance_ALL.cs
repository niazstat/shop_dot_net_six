using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_CustomerBalance_ALL
    {
        public int CustomerID { get; set; }
        public int CustomerNo { get; set; }
        public string CustomerSubCategoryName { get; set; }
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


        public decimal SalesAdjustAmount { get; set; }
        public int IsBlocked { get; set; } //Blocked for Transaction 
    }
}
