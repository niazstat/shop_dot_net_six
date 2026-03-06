using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class BankAccountEntryViewModel
    {
        public BankAccountEntryViewModel()
        {
            Banks = new List<Bank>();
            BankAccounts = new List<BankAccount>();
        }

        public List<Bank> Banks { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
    }
}
