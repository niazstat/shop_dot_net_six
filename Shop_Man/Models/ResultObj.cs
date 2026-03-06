using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class ResultObj
    {
        public int ResultID { get; set; }
        public string ResultMessage { get; set; }
        public string ResultNo { get; set; }
        public Object Obj { get; set; }
        public Object Obj2 { get; set; }
    }
}
