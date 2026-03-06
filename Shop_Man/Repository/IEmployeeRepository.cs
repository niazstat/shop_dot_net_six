using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Employees { get; }
        IQueryable<Designation> Designations { get; }
        ResultObj SaveEmployee(Employee employee);
        Employee FindEmployee(int _employeeId);

        ResultObj DeleteEmployee(Employee employee);

        ResultObj SaveDesignation(Designation designation);
        Designation FindDesignation(int _designation);


        ResultObj DeleteDesignation(Designation designation);


        List<SP_Employee_Salary_and_Expenses> SP_Employee_Salary_and_Expenses(DateTime fromDate, DateTime toDate, int empID);
        List<SP_Employee_Salary_and_Expenses> SP_Employee_Expenses(DateTime fromDate, DateTime toDate, int empID);
        SP_Employee_Salary_and_Expenses_SingleEmployee SP_Employee_Salary_and_Expenses_SingleEmployee(int empID);

    }
}
