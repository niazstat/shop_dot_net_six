using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class CashPaymentViewModel
    {
        public CashPaymentViewModel()
        {
            Suppliers = new List<Supplier>();
    
            CashPayments = new List<CashPayment>();
            Customers = new List<Customer>();
            CustomerSubCategorys = new List<CustomerSubCategory>();
            SupplierSubCategorys = new List<SupplierSubCategory>();

        }
        public PaymentMedium PaymentMedium { get; set; }
        public List<Supplier> Suppliers { get; set; }

        public List<Customer> Customers { get; set; }

        public List<CustomerSubCategory> CustomerSubCategorys { get; set; }
        public List<SupplierSubCategory> SupplierSubCategorys { get; set; }
        public List<CashPayment> CashPayments { get; set; }
    }
}
