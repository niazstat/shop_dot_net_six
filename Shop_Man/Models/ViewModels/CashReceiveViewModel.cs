using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class CashReceiveViewModel
    {

        public CashReceiveViewModel()
        {
            Customers = new List<Customer>();
            CustomerCategorys = new List<CustomerCategory>();
            CustomerSubCategorys = new List<CustomerSubCategory>();
            CashReceives = new List<CashReceive>();


        }
        public PaymentMedium PaymentMedium { get; set; }
        public List<Customer> Customers { get; set; }

        public List<CustomerCategory> CustomerCategorys { get; set; }
        public List<CustomerSubCategory> CustomerSubCategorys { get; set; }

        public List<CashReceive> CashReceives { get; set; }
    }
}
