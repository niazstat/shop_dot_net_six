using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_View_DayClose_Details
    {
      
        public DateTime dDate { get; set; }


        public int ChallanNoID { get; set; }
        public string ChallanNo { get; set; }

        public string TransType { get; set; }
        [NotMapped]
        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }


        public string Describtion { get; set; }


        public decimal ReceiveAmount { get; set; }

        public decimal PaymentAmount { get; set; }


      


        public int CompanyID { get; set; }

    

        public int UserId { get; set; }

  
    }
}
