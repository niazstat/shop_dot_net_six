using Microsoft.EntityFrameworkCore;
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private OrderManagementDBContext context;
        public EFEmployeeRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Employee> Employees => context.Employees.Include(a=>a.Designation);

        public IQueryable<Designation> Designations => context.Designations;

        public ResultObj DeleteDesignation(Designation designation)
        {
            ResultObj res = new ResultObj();



            Designation dbEntry = context.Designations
                   .FirstOrDefault(p => p.DesignationID == designation.DesignationID);
            if (dbEntry != null)
            {
                context.Designations.Remove(dbEntry);

            }

            try
            {
                context.SaveChanges();
                res.ResultID = 1;
                res.ResultMessage = "Successfully Deleted !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        public ResultObj DeleteEmployee(Employee employee)
        {
            ResultObj res = new ResultObj();



            Employee dbEntry = context.Employees
                   .FirstOrDefault(p => p.EmployeeID == employee.EmployeeID);
            if (dbEntry != null)
            {
                context.Employees.Remove(dbEntry);

            }



            try
            {
                context.SaveChanges();
                res.ResultID = 1;
                res.ResultMessage = "Successfully Deleted !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        public Designation FindDesignation(int _designationId)
        {
            return Designations.SingleOrDefault(a => a.DesignationID == _designationId);
        }

        public Employee FindEmployee(int _employeeId)
        {
            return Employees.SingleOrDefault(a => a.EmployeeID == _employeeId);
        }

        public ResultObj SaveDesignation(Designation designation)
        {
            ResultObj res = new ResultObj();
            if (designation.DesignationID == 0)
            {
                context.Designations.Add(designation);
            }
            else
            {
                context.Entry(designation).State = EntityState.Modified;

            }

            try
            {
                context.SaveChanges();
                res.ResultID = 1;
                res.ResultMessage = "Successfully Added !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        public ResultObj SaveEmployee(Employee employee)
        {
            ResultObj res = new ResultObj();
            if (employee.EmployeeID == 0)
            {
                context.Employees.Add(employee);
            }
            else
            {
                  context.Entry(employee).State = EntityState.Modified;
   
            }

            try
            {
                context.SaveChanges();
                res.ResultID = 1;
                res.ResultMessage = "Successfully Added !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        //public List<SP_Employee_Salary_and_Expenses> SP_Employee_Expenses(DateTime fromDate, DateTime toDate, int empID)
        //{
        //    List<SP_Employee_Salary_and_Expenses> list = new List<SP_Employee_Salary_and_Expenses>();
        //    try
        //    {
        //        list = context.Query<SP_Employee_Salary_and_Expenses>().FromSql(@"EXEC SP_Employee_Expenses {0},{1},{2}", fromDate, toDate, empID).ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return list;
        //}

        //public List<SP_Employee_Salary_and_Expenses> SP_Employee_Salary_and_Expenses(DateTime fromDate, DateTime toDate,int emid)
        //{
        //    List<SP_Employee_Salary_and_Expenses> list = new List<SP_Employee_Salary_and_Expenses>();
        //    try
        //    {
        //        list = context.Query<SP_Employee_Salary_and_Expenses>().FromSql(@"EXEC SP_Employee_Salary_and_Expenses {0},{1},{2}", fromDate, toDate, emid).ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return list;

        //}

        //public SP_Employee_Salary_and_Expenses_SingleEmployee SP_Employee_Salary_and_Expenses_SingleEmployee(int empID)
        //{
        //    SP_Employee_Salary_and_Expenses_SingleEmployee  obj = new SP_Employee_Salary_and_Expenses_SingleEmployee();
        //    try
        //    {
        //        obj = context.Query<SP_Employee_Salary_and_Expenses_SingleEmployee>().FromSql(@"EXEC SP_Employee_Salary_and_Expenses_SingleEmployee {0}", empID).First();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return obj;
        //}
        public List<SP_Employee_Salary_and_Expenses> SP_Employee_Expenses(DateTime fromDate, DateTime toDate, int empID)
        {
            try
            {
                return context.SP_Employee_Salary_and_Expenses
                    .FromSqlInterpolated($"EXEC SP_Employee_Expenses {fromDate}, {toDate}, {empID}")
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                // optionally log ex
                return new List<SP_Employee_Salary_and_Expenses>();
            }
        }

        public List<SP_Employee_Salary_and_Expenses> SP_Employee_Salary_and_Expenses(DateTime fromDate, DateTime toDate, int empID)
        {
            try
            {
                return context.SP_Employee_Salary_and_Expenses
                    .FromSqlInterpolated($"EXEC SP_Employee_Salary_and_Expenses {fromDate}, {toDate}, {empID}")
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                // optionally log ex
                return new List<SP_Employee_Salary_and_Expenses>();
            }
        }

        public SP_Employee_Salary_and_Expenses_SingleEmployee SP_Employee_Salary_and_Expenses_SingleEmployee(int empID)
        {
            try
            {
                return context.SP_Employee_Salary_and_Expenses_SingleEmployee
                    .FromSqlInterpolated($"EXEC SP_Employee_Salary_and_Expenses_SingleEmployee {empID}")
                    .AsNoTracking()
                    .FirstOrDefault() ?? new SP_Employee_Salary_and_Expenses_SingleEmployee();
            }
            catch (Exception ex)
            {
                // optionally log ex
                return new SP_Employee_Salary_and_Expenses_SingleEmployee();
            }
        }

    }
}
