using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class Designation
    {
        public int DesignationID { get; set; }
        public string Name { get; set; }
        public Company Company { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; }

        public int UpdateUserID { get; set; }
    }
}
