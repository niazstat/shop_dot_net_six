using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class ProjController
    {
        public int ProjControllerID { get; set; }
        public string ControllerName { get; set; }
        public string ControllerAction { get; set; }
        public string ControllerDes { get; set; }
        public string ControllerIcon { get; set; }
        public List<ProjAction> ProjActions{ get; set; }
    }
}
