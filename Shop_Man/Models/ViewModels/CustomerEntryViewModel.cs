using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class CustomerEntryViewModel
    {
        public CustomerEntryViewModel()
        {
            Customers = new List<Customer>();
            CustomerCategorys = new List<CustomerCategory>();
            CustomerSubCategorys = new List<CustomerSubCategory>();
            DistrictS = new List<District>();
        }
        public List<Customer> Customers { get; set; }

        public List<CustomerCategory> CustomerCategorys { get; set; }
        public List<CustomerSubCategory> CustomerSubCategorys { get; set; }
        public List<District> DistrictS { get; set; }
    }
}
