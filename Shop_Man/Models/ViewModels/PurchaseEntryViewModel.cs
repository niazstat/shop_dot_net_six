using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class PurchaseEntryViewModel
    {

        public PurchaseEntryViewModel()
        {
            Suppliers = new List<Supplier>();
            SupplierCategorys = new List<SupplierCategory>();
            SupplierSubCategorys = new List<SupplierSubCategory>();
            ProdNames = new List<ProdName>();
            Articles = new List<Article>();


        }
        public List<Supplier> Suppliers { get; set; }

        public List<SupplierCategory> SupplierCategorys { get; set; }
        public List<SupplierSubCategory> SupplierSubCategorys { get; set; }

     
        public List<ProdName> ProdNames { get; set; }

        public List<Article> Articles { get; set; }
    }
}
