using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class PurchaseDetails
    {

        public int PurchaseDetailsID { get; set; }

        public int CompanyProductID { get; set; }

        public CompanyProduct CompanyProduct { get; set; }
        public int PurchaseHeadID { get; set; }

        public string? ProductName { get; set; }
        public ProdName ProdName { get; set; }


        public string? ArticleName { get; set; }
        public Article Article { get; set; }


        public string? SizeName { get; set; }
        public Size Size { get; set; }


        public string? UOMName { get; set; }
        public UOM UOM { get; set; }


        public decimal CurrentStockQty { get; set; }



        public decimal PurchaseQtyInPair { get; set; }
        public decimal PurchaseQtyInDozen { get { return PurchaseQtyInPair / 12; } }

        public decimal CommissionRate { get; set; }

        public decimal CommissionAmount { get { return CommissionRate * PurchaseQtyInPair; } }


        public decimal PurchaseRate { get; set; }



        public decimal PurchaseQtyInPairAmount { get { return PurchaseRate * PurchaseQtyInPair; } }


  
     

        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public DateTime EntryDate { get; set; } = DateTime.Now;

        public DateTime LastUpdateTime { get; set; }=DateTime.Now;


        public bool IsAllowEdit { get; set; }
     

        public int UpdateUserID { get; set; }
    }
}
