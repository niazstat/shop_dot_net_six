using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class CompanyProduct
    {
        public int CompanyProductID { get; set; }
        public Supplier Supplier { get; set; }
        public ProdName? ProdName { get; set; }
        public Article? Article { get; set; }
        public ProdCoCategory? ProdCoCategory { get; set; }
        public Size Size { get; set; }
        public ProdType? ProdType { get; set; }

        public UOM? UOM { get; set; }
        public decimal BuyPrice { get; set; }

        public decimal BuyComm { get; set; }


        public decimal SellPrice { get; set; }

        public decimal SellComm { get; set; }



        public int CompanyID { get; set; }
        public Company Company { get; set; }


        public decimal OpeningStock { get; set; }

        public decimal CurrentStock { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int UpdateUserID { get; set; }

    }
}
