using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class StockAdjustDetails
    {


        public int StockAdjustDetailsID { get; set; }
        public int StockAdjustHeadID { get; set; }
        public int CompanyProductID { get; set; }

        public CompanyProduct CompanyProduct { get; set; }
     

        public string ProductName { get; set; }
        public ProdName ProdName { get; set; }


        public string ArticleName { get; set; }
        public Article Article { get; set; }


        public string SizeName { get; set; }
        public Size Size { get; set; }


        public string UOMName { get; set; }
        public UOM UOM { get; set; }






        public decimal AdjustQtyInPair { get; set; }
        public decimal AdjustQtyInDozen { get { return AdjustQtyInPair / 12; } }

   


        public decimal AdjustRate { get; set; }



        public decimal AdjustAmount { get { return AdjustRate * AdjustQtyInPair; } }



        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public DateTime EntryDate { get; set; }



    }
}
