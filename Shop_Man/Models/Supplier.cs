using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string? Name { get; set; }

        public string? Address1 { get; set; }

        public string? Addres2 { get; set; }


        public string? ContactPerson { get; set; }

        public string? MobileNo1 { get; set; }

        public string? MobileNo2 { get; set; }

        public int Isdeleted { get; set; }

        public DateTime EntryDate { get; set; }

        public int IsEdited { get; set; }

        public DateTime LastEditedDate { get; set; }

        public User? EntryBy { get; set; }
        public int? UserId { get; set; }

        public decimal OpeningBalance { get; set; }

        public decimal CurrentBalance { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }





        public int SupplierSubCategoryID { get; set; }
        public SupplierSubCategory? SupplierSubCategory { get; set; }

        public bool IsLocked { get; set; }

    }
}
