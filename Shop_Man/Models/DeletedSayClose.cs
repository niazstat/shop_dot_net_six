using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class DeletedSayClose
    {

        public int DeletedSayCloseID { get; set; }

        public DateTime DataCloseDate { get; set; }
        [NotMapped]
        public string DataCloseDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", DataCloseDate); } }


        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime EntryDate { get; set; }


        [NotMapped]
        public string EntryDateFormated { get { return String.Format("{0:dd/MM/yyyy h:mm tt}", EntryDate); } }

    }
}
