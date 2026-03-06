using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_Datewise_Stock
    {
        public int CompanyProductID { get; set; }
        public int ArticleID { get; set; }
        public int SizeID { get; set; }
        public string SuppName { get; set; }
        public string ProdName { get; set; }
        public string Article { get; set; }
        public string ProdCoCategory { get; set; }
        public string Size { get; set; }
        public string ProdType { get; set; }


        public decimal StoctQty { get; set; }

        public decimal OpQty { get; set; }
        public decimal PurchaseQty { get; set; }
        public decimal SalesQty { get; set; }

        public decimal ReturnQty { get; set; }
        public decimal ReturnPrice { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }

        public decimal PurchaseAmount { get; set; }

        public decimal NetSalesAmount { get; set; }
    }
}
