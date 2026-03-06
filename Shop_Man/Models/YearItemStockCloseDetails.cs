using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class YearItemStockCloseDetails
    {

        public int YearItemStockCloseDetailsID { get; set; }


        public int YearName { get; set; }

        public DateTime dDate { get; set; }

        [NotMapped]
        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }
        public string TranType { get; set; }
         
        public CompanyProduct CompanyProduct { get; set; }

        public string ArticleName { get; set; }

        public string SizeName { get; set; }

        public string PurInv { get; set; }

        public decimal  PurQty{ get; set; }

        public decimal NetPurRate { get; set; }

        public decimal NetPurAmount { get; set; }



        public string SalesInv { get; set; }

        public decimal SalesQty { get; set; }

        public decimal NetSalesRate { get; set; }

        public decimal NetSalesAmount { get; set; }


        public string RetInvNo { get; set; }

        public decimal RetQtyQty { get; set; }

        public decimal NetRetRate { get; set; }

        public decimal NetRetAmount { get; set; }


        public DateTime EntryDate { get; set; }


        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }


        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int UpdateUserID { get; set; }

    }
}
