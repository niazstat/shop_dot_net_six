using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class SupplierEntryViewModel
    {

        public SupplierEntryViewModel()
        {
            Suppliers = new List<Supplier>();
            SupplierCategorys = new List<SupplierCategory>();
            SupplierSubCategorys = new List<SupplierSubCategory>();
        }
        public List<Supplier> Suppliers { get; set; }

        public List<SupplierCategory> SupplierCategorys { get; set; }
        public List<SupplierSubCategory> SupplierSubCategorys { get; set; }

    }
}
