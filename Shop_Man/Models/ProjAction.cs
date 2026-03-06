using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class ProjAction
    {
        public int ProjActionID { get; set; }

        public string ActionController { get; set; }
        public string ActionName { get; set; }

        public string ActionDes { get; set; }

        public int ProjControllerID { get; set; }
    }
}
