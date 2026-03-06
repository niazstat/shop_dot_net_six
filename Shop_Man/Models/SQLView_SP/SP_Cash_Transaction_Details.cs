using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_Cash_Transaction_Details
    {
        public int ID { get; set; }
        public string Type{ get; set; }
    public DateTime Ddate { get; set; }
        public string FormatedDdate { get { return String.Format("{0:dd-MMM-yyyy}", Ddate); } }


        public string InvoicNo { get; set; }

        public int CustomerID { get; set; }

        public string CustName { get; set; }

        public decimal RecvAmount { get; set; }

        public int SupplierId { get; set; }

        public string SuppName { get; set; }
        public decimal PayAmount { get; set; }
        public string Note { get; set; }

        public decimal Expenses { get; set; }
    }
}
