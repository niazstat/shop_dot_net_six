using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class AdjustmentEntryViewModel
    {
        public AdjustmentEntryViewModel()
        {
            Suppliers = new List<Supplier>();

            Adjustments = new List<Adjustment>();
            Customers = new List<Customer>();
            CustomerSubCategorys = new List<CustomerSubCategory>();
            SupplierSubCategorys = new List<SupplierSubCategory>();

        }

        public List<Supplier> Suppliers { get; set; }

        public List<Customer> Customers { get; set; }

        public List<CustomerSubCategory> CustomerSubCategorys { get; set; }
        public List<SupplierSubCategory> SupplierSubCategorys { get; set; }
        public List<Adjustment> Adjustments { get; set; }
    }
}
