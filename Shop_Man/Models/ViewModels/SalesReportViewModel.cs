using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class SalesReportViewModel
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }


        public string CustomerIds { get; set; }

        public string ArticleNames { get; set; }

        public string IsDetails { get; set; }

        public string Type { get; set; }

        public string Type2 { get; set; }


    }
}
