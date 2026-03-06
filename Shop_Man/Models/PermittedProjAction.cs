using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class PermittedProjAction
    {

        public int PermittedProjActionID { get; set; }
        public int ProjActionID { get; set; }
        public ProjAction ProjAction { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
