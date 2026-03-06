using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_Current_Stock_Article_Size
    {



        public string SalesInvNo { get; set; }

        public DateTime dDate { get; set; }
        [NotMapped]
        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }


        public string PurInvNo { get; set; }
        public string RetInvNo { get; set; }
        public string ProName { get; set; }
        public string ArticleName { get; set; }
        public string sizeName { get; set; }
        public int  CompanyProductID { get; set; }
        public decimal SalesQty { get; set; }
        public decimal SalesRate { get; set; }

        public decimal CommissionRate { get; set; }
        public decimal NetSalesRate { get; set; }
        public decimal NetSalesAmount { get; set; }
        public decimal PurchaseQtyInPair { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal PurCommissionRate { get; set; }

        public decimal NetPurchaseRate { get; set; }
        public decimal NetPurAmount { get; set; }
        public decimal ReyurnQtyInPair { get; set; }
        public decimal NetReturnRate { get; set; }
        public decimal NetReturnAmount { get; set; }



    }
}
