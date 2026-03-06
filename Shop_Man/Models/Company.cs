using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class Company
    {

        [BindNever]
        public int CompanyID { get; set; }

        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Proprietor { get; set; }
        public string? ShortDescription { get; set; }
        public string? Address2 { get; set; }

        public string? Shopname { get; set; }
        public string? ShopAddress { get; set; }


        public string? MobileNo1 { get; set; }

        public string? MobileNo2 { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int? UpdateUserID { get; set; }

    }
}
