using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class SalesDetails
    {

        public int SalesDetailsID { get; set; }

        public int CompanyProductID { get; set; }

        public CompanyProduct CompanyProduct { get; set; }
        public int SalesHeadID { get; set; }

        public string? ProductName { get; set; }
        public ProdName ProdName { get; set; }


        public string? ArticleName { get; set; }
        public Article Article { get; set; }


        public string? SizeName { get; set; }
        public Size Size { get; set; }


        public string? UOMName { get; set; }

        public UOM UOM { get; set; }


        public decimal CurrentStockQty { get; set; }

       

        public decimal SalesQtyInPair { get; set; }
        public decimal SalesQtyInDozen { get { return SalesQtyInPair / 12; } }

        public decimal CommissionRate { get; set; }

        public decimal CommissionAmount { get { return CommissionRate * SalesQtyInPair; } }


        public decimal SalesRate { get; set; }



        public decimal SalesAmount { get { return SalesRate* SalesQtyInPair; } }


        public decimal BuyRate { get; set; }
        public decimal BuyAmount { get { return BuyRate * SalesQtyInPair; } }
        public decimal BuyCommRate { get; set; }
        public decimal BuyCommAmount { get { return BuyCommRate * SalesQtyInPair; } }
        public decimal ReturnQtyInPair { get; set; }

        public decimal SalesAdjustRate { get; set; }
        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public DateTime EntryDate { get; set; }

        public ICollection<SalesReturnDetails>? SalesReturnDetails { get; set; }

        public ICollection<SalesAdjustDetails>? SalesAdjustDetails { get; set; }

        public DateTime LastUpdateTime { get; set; }


        public bool IsAllowEdit { get; set; }


        public int UpdateUserID { get; set; }
    }
}
