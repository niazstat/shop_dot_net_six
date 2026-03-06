using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int EmployeeCode { get; set; }
        public string? GeneratedCode { get; set; }

        public string? Name { get; set; }

        public string? FatherName { get; set; }

        public string? MotherName { get; set; }
        public string? PermanentAddress { get; set; }
        public string? CurrentAddress { get; set; }

        public Designation? Designation { get; set; }

        public DateTime JoiningDate { get; set; }
        [NotMapped]
        public string JoiningDateFormated { get { return String.Format("{0:dd-MMM-yyyy}", JoiningDate); } }


        public DateTime DateOfBirth { get; set; }
        [NotMapped]
        public string DateOfBirthFormated { get { return String.Format("{0:dd-MMM-yyyy}", DateOfBirth); } }

        public string? Mobile1 { get; set; }

        public string? Mobile2 { get; set; }

        public decimal? Salary { get; set; }

        public string? Religion { get; set; }

        public string? BloodGroup { get; set; }

        public string? NID { get; set; }

        public string? Village { get; set; }

        public string? Thana { get; set; }
        public string?  District { get; set; }
        public string? Division { get; set; }

        public string? MaleFemale { get; set; }
        public string? RoadNo { get; set; }
        public string? HouseNo { get; set; }
        public bool IsLedgerClose { get; set; }

        public DateTime EntryDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; }

        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public DateTime LastUpdateTime { get; set; }= DateTime.Now;


        public bool IsAllowEdit { get; set; }
        public decimal MaxSalaryLimit { get;  set; }

        public int UpdateUserID { get; set; }
    }
}
