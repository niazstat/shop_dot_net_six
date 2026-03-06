using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class View_Item_Close
    {

        public int ItemClosesID { get; set; }

        public int CompanyProductID { get; set; }


        public DateTime FromDate { get; set; }

        [NotMapped]
        public string FromDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", FromDate); } }


        public DateTime ToDate { get; set; }

        [NotMapped]
        public string ToDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", ToDate); } }


    
        public decimal TotalPurQty { get; set; }


        public decimal TotalPurAmount { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalPurNetPurAmount { get; set; }

        public decimal AvgAmount { get; set; }
        public int ArticleID { get; set; }
        public int SizeID { get; set; }

        public string ArticleName { get; set; }
        public string SizeName { get; set; }

    }
}
