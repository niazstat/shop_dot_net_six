using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string? CustomerNo { get; set; }
        public string? Name { get; set; }

        public string? ShopName { get; set; }
        public string? MobileNo { get; set; }
        public string? Address1 { get; set; }
        public string? MobileNo2 { get; set; }

        public string? Disrict { get; set; }
        public string? Address2 { get; set; }
        public DateTime EntryDatetime { get; internal set; }

        public int Isdeleted { get; set; }
        public User? EntryBy { get; set; }
        public int UserId { get; set; }
        public decimal OpeningBalance { get; set; }

        public decimal CurrentBalance { get; set; }


        public int CompanyID { get; set; }
        public Company Company { get; set; }




        public CustomerSubCategory? CustomerSubCategory { get; set; }

        public int CustomerSubCategoryID { get; set; }

        public string? CustomerSubCategoryName { get; set; }

        public decimal OpeningCommission{ get; set; }
        public decimal OpeningQty { get; set; }


        public int DistrictID { get; set; }
       public District? District { get; set; }

        public decimal MaxBalanceLimit { get; set; }
        public bool IsAllowEdit { get; set; }
        public DateTime? LastUpdateTime { get; set; } = DateTime.Now; 

        public int UpdateUserID { get; set; }

        public bool IsLocked { get; set; }

        public int StartingYear { get; set; }
        public int IsBlocked { get; set; } //Blocked for Transaction 

        [NotMapped]
        public string IsLockedText { get { return IsLocked ? "Locked" : "Unlocked"; } }
    }
}
