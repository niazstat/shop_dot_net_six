using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class ChequeTrasactionViewModel
    {


        public ChequeTrasactionViewModel()
        {
            
            Types = new List<string>();

            ReceiverPayer = new List<string>();
            BankAccounts = new List<BankAccount>();
            ChequeTransactions = new List<ChequeTransaction>();
            Customers = new List<Customer>();
            Suppliers = new List<Supplier>();

        }
        public PaymentMedium PaymentMedium { get; set; }

        public List<string> ReceiverPayer { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
        public List<string> Types { get; set; }
        public List<ChequeTransaction> ChequeTransactions { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Supplier> Suppliers { get; set; }
    }
}
