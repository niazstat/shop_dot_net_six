using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class ItemsClosing
    {
        public int ItemsClosingID { get; set; }

        public DateTime FromDate { get; set; }

        [NotMapped]
        public string FromDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", FromDate); } }


        public DateTime ToDate { get; set; }

        [NotMapped]
        public string ToDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", ToDate); } }


        public CompanyProduct CompanyProduct { get; set; }


        public decimal PurQty { get; set; }


        public decimal NetPurAmount { get; set; }

        public decimal AvgPurAmount { get; set; }


        public DateTime EntryDate { get; set; }
    }
}
