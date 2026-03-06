using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_View_ALL_Customer_Year_Close
    {


        public int CustomerID { get; set; }

        public string Name { get; set; }

        public string ShopName { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal BuyAmount { get; set; }

        public decimal ReturnAmount { get; set; }

        public decimal AddLess { get; set; }

        public decimal AdjustAmont { get; set; }
        public decimal ShortAmount { get; set; }

        public decimal RejectAmount { get; set; }
        public decimal BuyReturn { get; set; }

        public string Remarks { get; set; }


        public decimal SalesAdjustAmount { get; set; }
        [NotMapped]
        public decimal NetProfit { get { return SalesAmount - (BuyAmount - BuyReturn) - ReturnAmount + AddLess - AdjustAmont - ShortAmount - RejectAmount - SalesAdjustAmount; } }

        public int IsBlocked { get; set; } //Blocked for Transaction 
    }
}
