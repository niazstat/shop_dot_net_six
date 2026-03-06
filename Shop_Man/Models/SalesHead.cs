using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class SalesHead
    {
        public int SalesHeadID { get; set; }
        public string? SalesNo { get; set; }

        public int AutoSalesNo { get; set; }
        public int CustwiseSalesNo { get; set; }
        public string? GeneratedSalesNo { get; set; }
        [NotMapped]
        public string GeneratedSalesNo2 { get { return "SAL/" + GeneratedSalesNo; } }

        public bool IsCashSales { get; set; }
        public Customer? Customer { get; set; }
        public DateTime SalesDate { get; set; }
        [NotMapped]
        public string SalesDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", SalesDate); } }
        public decimal TotalAmount { get; set; }

        public decimal ReceiveAmount { get; set; }
        public decimal TotalCommission { get; set; }

        public decimal TransportCost { get; set; }
        public decimal PackingCost { get; set; }
        public decimal TotalSackNo { get; set; }
        public decimal TotalSackNoFee { get; set; }
        public decimal TotalNetAmount { get; set; }
        public decimal AddLessAmount { get; set; }

        public decimal PreviousBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public int PaymentMediumID { get; set; }

        public PaymentMedium? PaymentMedium { get; set; }
        public string? AccNo { get; set; }
        public string? CheckNo { get; set; }
        public DateTime CheckPassDate { get; set; }
        [NotMapped]
        public string checkPassDateFormat { get { return String.Format("{0:dd-MMM-yyyy}", CheckPassDate); } }
        public string? Note1 { get; set; }

        public string? Note2 { get; set; }


        public ICollection<SalesDetails> SalesDetailsList { get; set; }

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
