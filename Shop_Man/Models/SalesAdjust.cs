using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class SalesAdjust
    {
        public int SalesAdjustID { get; set; }


        public ICollection<SalesAdjustDetails>? SalesAdjustDetailsList { get; set; }


        public string? SalesAdjustNo { get; set; }


 


        public int AutoSalesAdjustNo { get; set; }
        public int CustwiseSalesAdjustNo { get; set; }
        public string? GeneratedSalesAdjustNo { get; set; }
        [NotMapped]
        public string GeneratedSalesAdjustNo2 { get { return "SADJ/" + GeneratedSalesAdjustNo; } }


        public Customer Customer { get; set; }
        public DateTime SalesAdjustDate { get; set; }
        [NotMapped]
        public string SalesAdjustFormated { get { return String.Format("{0:dd-MMM-yyyy}", SalesAdjustDate); } }

        public string? Note1 { get; set; }
        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime EntryDate { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int UpdateUserID { get; set; }



    }
}
