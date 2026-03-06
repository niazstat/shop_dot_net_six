using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class SalaryEntryViewModel
    {

        public SalaryEntryViewModel()
        {
            Employees = new List<Employee>();
            SalarySheetHeads = new List<SalarySheetHead>();

        }
        public List<Employee> Employees { get; set; }

        public List<SalarySheetHead> SalarySheetHeads { get; set; }
    }
}
