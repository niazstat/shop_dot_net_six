using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class CompanyProductEntryViewModel
    {
        
        public CompanyProductEntryViewModel()
        {
            ProdNames = new List<ProdName>();
            Articles = new List<Article>();
            ProdCoCategorys = new List<ProdCoCategory>();
            Sizes = new List<Size>();
            ProdTypes = new List<ProdType>();
            UOMs = new List<UOM>();
            Suppliers = new List<Supplier>();

        }

        public List<ProdName> ProdNames { get; set; }
        public List<Article> Articles { get; set; }
        public List<ProdCoCategory> ProdCoCategorys { get; set; }
        public List<Size> Sizes { get; set; }
        public List<ProdType> ProdTypes { get; set; }
        public List<UOM> UOMs { get; set; }
        public List<Supplier> Suppliers { get; set; }

    }
}
