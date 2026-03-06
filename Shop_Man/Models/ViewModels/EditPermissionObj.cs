using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class EditPermissionObj
    {

        public string Type { get; set; }
        public bool IsSearchByDate { get; set; }
        public int ChallanID { get; set; }
        public string ChallanNo { get; set; }
         public DateTime dDate { get; set; }
        [NotMapped]
        public string dDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", dDate); } }

        public bool IsAllowEdit { get; set; }
    }
}
