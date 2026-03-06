using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class SupplierCategory
    {
        public int SupplierCategoryID { get; set; }

        public string SupplierCategoryName { get; set; }

        public IEnumerable<SupplierCategory> SupplierCategorys { get; set; }
    }
}
