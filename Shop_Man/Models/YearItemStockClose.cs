using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class YearItemStockClose
    {
        public int YearItemStockCloseID { get; set; }

        public int SlNo { get; set; }

        public int YearName { get; set; }

        public decimal PrevStock { get; set; }

        public CompanyProduct CompanyProduct { get; set; }
       public int CompanyProductID { get; set; }
        public decimal OpBalance { get; set; }

        public decimal PurchaseQty { get; set; }

        public decimal PurchaseAmount { get; set; }

        public decimal SalesQty { get; set; }

        public decimal SalesAmount { get; set; }


        public decimal ReturnQty { get; set; }

        public decimal AdjustQty { get; set; }

        public decimal StockAdjustAmount { get; set; }

        public decimal ReturnAmount { get; set; }

        public decimal ClosingBalance { get; set; }


        public DateTime EntryDate { get; set; }


        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }


        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int UpdateUserID { get; set; }
    }
}
