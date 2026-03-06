using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
   public interface ISalarySheetRepository
    {
        IQueryable<SalarySheet> SalarySheets { get; }
        IQueryable<SalarySheet> SalarySheetAsNotracking { get; }
        ResultObj SaveSalarySheet(SalarySheet salarySheet);
        ResultObj DeleteSalarySheet(SalarySheet salarySheet);

        List<SalarySheet> FindSalarySheetsByEmployeeID(int employeeID);

        IQueryable<SalarySheetHead> SalarySheetHeads { get; }
        ResultObj SaveSalarySheetHead(SalarySheetHead salarySheetHead);
        SalarySheetHead FindSalarySheetHead(int _salarySheetHeadID);
 

        ResultObj DeleteSalarySheetHead(SalarySheetHead salarySheetHead);

    }
}
