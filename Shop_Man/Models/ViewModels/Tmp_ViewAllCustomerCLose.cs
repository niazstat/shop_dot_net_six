using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class Tmp_ViewAllCustomerCLose
    {
        public int Id { get; set; }

        public int CustomerID { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal BuyAmount { get; set; }

        public decimal ReturnAmount { get; set; }

        public decimal AddLess { get; set; }

        public decimal AdjustAmont { get; set; }
        public decimal ShortAmount { get; set; }

        public decimal RejectAmount { get; set; }
        public decimal BuyReturn { get; set; }

        public string Remarks { get; set; }


        public DateTime EntryDate { get; set; }
    
    }
}
