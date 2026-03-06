using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class SupplierSubCategory
    {
        public int SupplierSubCategoryID { get; set; }

        public string SupplierSubCategoryName { get; set; }


        public int SupplierCategoryID { get; set; }
        public SupplierCategory SupplierCategory { get; set; }
    }
}
