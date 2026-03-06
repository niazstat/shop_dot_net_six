using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class DayCloseReportSingle
    {
        public DateTime dDate { get; set; }
        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }
        public decimal Cash_Receive { get; set; } = 0;
        public decimal Bkash_Receive { get; set; } = 0;
        public decimal Online_Receive { get; set; } = 0;
        public decimal Check_Receive { get; set; } = 0;
        public decimal Others_Receive { get; set; } = 0;


        public decimal Cash_Payment { get; set; } = 0;
        public decimal Bkash_Payment { get; set; } = 0;
        public decimal Online_Payment { get; set; } = 0;
        public decimal Check_Payment { get; set; } = 0;
        public decimal Others_Payment { get; set; } = 0;
        public decimal Opening_Balance { get; set; } = 0;
        public decimal AddLess { get {
                return (Cash_Receive + Bkash_Receive + Online_Receive + Check_Receive + Others_Receive) -
(Cash_Payment + Bkash_Payment + Online_Payment + Check_Payment + Others_Payment);
            } }
        public decimal Closing_Balance { get; set; } = 0;
        public Company Company { get; set; }
        public decimal TotalSales { get;  set; } = 0;
        public decimal TotalPurchase { get;  set; } = 0;

    }

   
}
