using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class Bank
    {
      
        public int BankID { get; set; }

        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public Company Company { get; set; }

        public bool IsAllowEdit { get; set; }
        public DateTime LastUpdateTime { get; internal set; } = DateTime.Now;

        public int? UpdateUserID { get; set; }
    }
}
