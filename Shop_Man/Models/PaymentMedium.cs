using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class PaymentMedium
    {
       public int PaymentMediumID { get; set; }
        public string? Name { get; set; }

        public string? MediumType { get; set; }
        public int CompanyID { get; set; }

        public Company Company { get; set; }
    }
}
