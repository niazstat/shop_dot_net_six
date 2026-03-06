using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class BankEntryViewModel
    {
        public BankEntryViewModel()
        {
            Banks = new List<Bank>();
        }

        public List<Bank> Banks { get; set; }
    }
}
