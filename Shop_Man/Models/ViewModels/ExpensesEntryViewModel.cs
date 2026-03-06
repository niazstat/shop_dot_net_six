using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class ExpensesEntryViewModel
    {
        public ExpensesEntryViewModel()
        {
            Expenses = new List<Expense>();
            ExpensesHeads = new List<ExpensesHead>();
            Employees = new List<Employee>();
        }

        public List<Expense> Expenses { get; set; }
        public List<ExpensesHead> ExpensesHeads { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
