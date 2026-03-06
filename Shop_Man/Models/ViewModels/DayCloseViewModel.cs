using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class DayCloseViewModel
    {

        public int UserID { get; set; }

        public int CompID { get; set; }
        public int YearName { get; set; }
        public string TransType { get; set; }
        public DateTime dDate { get; set; }
        public int ViewType { get; set; }
    }
}
