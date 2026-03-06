using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class StockAdjustHead
    {

        public int StockAdjustHeadID { get; set; }


 

        public int AutoStockAdjust { get; set; }

        public string GeneratedStockAdjustNo { get; set; }
        [NotMapped]
        public string GeneratedStockAdjustNo2 { get { return "STCK/" + GeneratedStockAdjustNo; } }


        public DateTime AdjustDate { get; set; }
        [NotMapped]
        public string AdjustDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", AdjustDate); } }

        public string Note1 { get; set; }

        public ICollection<StockAdjustDetails> StockAdjustDetailsList { get; set; }

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
