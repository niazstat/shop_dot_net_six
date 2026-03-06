using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class LogEntryEdit
    {

        public  int LogEntryEditID { get; set; }

        public string EditItem { get; set; }

        public string EditType { get; set; }
        public int ChallanID { get; set; }
        public int ChallanDetailsID { get; set; }
        public DateTime dDate { get; set; }

        public DateTime EntryDate { get; set; }

        public string Remarks { get; set; }

        public bool  IsProcessDone { get; set; }
        public int UserId { get; set; }
    }
}
