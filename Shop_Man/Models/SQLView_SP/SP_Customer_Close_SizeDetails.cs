using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_Customer_Close_SizeDetails
    {
        public int ArticleID { get; set; }
        public string ArticleName { get; set; }
        public int SizeID { get; set; }
        public string SizeName { get; set; }
        public decimal SalesQtyInPair { get; set; }
    }
}
