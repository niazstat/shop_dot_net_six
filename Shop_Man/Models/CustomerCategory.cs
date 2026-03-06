using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class CustomerCategory
    {
        public int CustomerCategoryID { get; set; }

        public string CustomerCategoryName { get; set; }

        public IEnumerable<CustomerSubCategory> CustomerSubCategorys { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int UpdateUserID { get; set; }
    }
}
