using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class EmployeeEntryViewModel
    {
        public EmployeeEntryViewModel()
        {
            Employees = new List<Employee>();
            Designations = new List<Designation>();
        }

        public List<Employee> Employees { get; set; }
        public List<Designation> Designations { get; set; }
    }
}
