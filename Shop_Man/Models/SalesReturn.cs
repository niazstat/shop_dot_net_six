using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class SalesReturn
    {
        public int SalesReturnID { get; set; }


        public ICollection<SalesReturnDetails>? SalesReturnDetailsList { get; set; }


        public string? ReturnNo { get; set; }

        public int AutoReturnNo { get; set; }
        public int CustwiseReturnNo { get; set; }
        public string? GeneratedReturnNo { get; set; }
        [NotMapped]
        public string GeneratedReturnNo2 { get { return "RET/" + GeneratedReturnNo; } }

      
        public Customer Customer { get; set; }
        public DateTime ReturnDate { get; set; }
        [NotMapped]
        public string ReturnDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", ReturnDate); } }
 
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
