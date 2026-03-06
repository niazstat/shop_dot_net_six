using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class CustomerSubCategory
    {

        public int CustomerSubCategoryID { get; set; }

        public string CustomerSubCategoryName { get; set; }


        public int CustomerCategoryID { get; set; }
        public CustomerCategory CustomerCategory { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int UpdateUserID { get; set; }
    }
}
