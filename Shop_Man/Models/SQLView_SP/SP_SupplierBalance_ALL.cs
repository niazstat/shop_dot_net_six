using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_SupplierBalance_ALL
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
    
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
