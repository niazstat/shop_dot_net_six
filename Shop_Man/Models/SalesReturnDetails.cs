using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class SalesReturnDetails
    {

        public int SalesReturnDetailsID { get; set; }

        public int CompanyProductID { get; set; }

        public CompanyProduct CompanyProduct { get; set; }
        public int SalesReturnID { get; set; }

        public string? ProductName { get; set; }
        public ProdName ProdName { get; set; }


        public string ArticleName { get; set; }
        public Article Article { get; set; }


        public string? SizeName { get; set; }
        public Size Size { get; set; }


        public string? UOMName { get; set; }
        public int UOMID { get; set; }
        public UOM UOM { get; set; }


        public decimal CurrentStockQty { get; set; }



        public decimal ReyurnQtyInPair { get; set; }
        public decimal ReyurnQtyInDozen { get { return ReyurnQtyInPair / 12; } }

        public decimal ReturnCommissionRate { get; set; }

        public decimal ReturnCommissionAmount { get { return ReturnCommissionRate * ReyurnQtyInPair; } }


        public decimal RetRate { get; set; }



        public decimal RetAmount { get { return RetRate * ReyurnQtyInPair; } }






        public decimal SalesCommissionRate { get; set; }

        public decimal SalesCommissionAmount { get { return SalesCommissionRate * ReyurnQtyInPair; } }


        public decimal SalesRate { get; set; }



        public decimal SalesAmount { get { return SalesRate * ReyurnQtyInPair; } }

        /// </summary>



        public decimal BuyRate { get; set; }
        public decimal BuyAmount { get { return BuyRate * ReyurnQtyInPair; } }
        public decimal BuyCommRate { get; set; }
        public decimal BuyCommAmount { get { return BuyCommRate * ReyurnQtyInPair; } }

        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public DateTime EntryDate { get; set; }

        public SalesDetails SalesDetails { get; set; }
        public int SalesDetailsID { get; set; }

        public int SalesHeadID { get; set; }
    }
}
