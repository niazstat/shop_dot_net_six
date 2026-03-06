using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class YearCloseDetails
    {

        public int YearCloseDetailsID { get; set; }

        public int SlNo { get; set; }

        public int YearName { get; set; }
        public string TranType { get; set; }

        public Customer Customer { get; set; }
        public Supplier Supplier { get; set; }
        public Employee Employee { get; set; }


        public  decimal OpeningBalance { get; set; }



        public decimal ReceiveAmount { get; set; }

        public decimal PaymentAmount { get; set; }


        public decimal SalesAmount { get; set; }
        public decimal SalesQty { get; set; }


        public decimal PurchaseAmount { get; set; }
        public decimal PurchaseQty { get; set; }



        public decimal SalesReturnAmount { get; set; }
        public decimal SalesReturnQty { get; set; }


        public decimal ClosingBalance { get; set; }


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
