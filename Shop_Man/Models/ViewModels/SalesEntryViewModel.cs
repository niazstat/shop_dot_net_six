using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class SalesEntryViewModel
    {

        public SalesEntryViewModel()
        {
            Customers = new List<Customer>();
            CustomerCategorys = new List<CustomerCategory>();
            CustomerSubCategorys = new List<CustomerSubCategory>();
            ProdNames = new List<ProdName>();
            Articles = new List<Article>();

        }
        public List<Customer> Customers { get; set; }

        public List<CustomerCategory> CustomerCategorys { get; set; }
        public List<CustomerSubCategory> CustomerSubCategorys { get; set; }

        public List<PaymentMedium> PaymentMediums { get; set; }
        public List<ProdName> ProdNames { get; set; }
        public List<Article> Articles { get; set; }
    }
}
