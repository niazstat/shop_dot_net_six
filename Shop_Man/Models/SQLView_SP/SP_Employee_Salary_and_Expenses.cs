using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_Employee_Salary_and_Expenses
    {

        public int ID { get; set; }
        public  int  EmployeeID { get; set; }

        public string InvNo { get; set; }
        public string HeadName { get; set; }

        public string EmployeeName { get; set; }

        public DateTime dDate { get; set; }
        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }
        public decimal AddAmount { get; set; }

        public decimal DeductAmount { get; set; }

        public decimal NetAddAmount { get { return AddAmount - DeductAmount; } }

        public decimal PayAmount { get; set; }
        public decimal ReceiveAmpunt { get; set; }

        public decimal NetPayAmount { get { return PayAmount - ReceiveAmpunt; } }


        public decimal BalanceAmount { get { return NetAddAmount - NetPayAmount; } }
        public string Remarks { get; set; }

    }
}
