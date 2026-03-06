using Microsoft.EntityFrameworkCore;
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFSalarySheetRepository : ISalarySheetRepository
    {

        private OrderManagementDBContext context;
        public EFSalarySheetRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<SalarySheet> SalarySheets => context.SalarySheets.Include(a=>a.Employee).Include(a => a.SalarySheetHead);

        public IQueryable<SalarySheet> SalarySheetAsNotracking => context.SalarySheets.Include(a => a.Employee).Include(a => a.SalarySheetHead).AsNoTracking();

        public IQueryable<SalarySheetHead> SalarySheetHeads => context.SalarySheetHeads;

        public ResultObj DeleteSalarySheet(SalarySheet salarySheet)
        {

            ResultObj res = new ResultObj();

            SalarySheet dbEntry = context.SalarySheets
                   .FirstOrDefault(p => p.SalarySheetID == salarySheet.SalarySheetID);
            if (dbEntry != null)
            {
                context.SalarySheets.Remove(dbEntry);

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

        public ResultObj DeleteSalarySheetHead(SalarySheetHead salarySheetHead)
        {
            ResultObj res = new ResultObj();
            SalarySheetHead dbEntry = context.SalarySheetHeads
                   .FirstOrDefault(p => p.SalarySheetHeadID == salarySheetHead.SalarySheetHeadID);
            if (dbEntry != null)
            {
                context.SalarySheetHeads.Remove(dbEntry);

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

        public SalarySheetHead FindSalarySheetHead(int _salarySheetHeadID)
        {
            return SalarySheetHeads.SingleOrDefault(a => a.SalarySheetHeadID == _salarySheetHeadID);
        }

        public List<SalarySheet> FindSalarySheetsByEmployeeID(int employeeID)
        {
            return context.SalarySheets.Include(a => a.Employee).Where(b => b.EmployeeID == employeeID).ToList();
        }

        public ResultObj SaveSalarySheet(SalarySheet salarySheet)
        {
            ResultObj res = new ResultObj();
            // context.Attach(cashReceive.PaymentMedium);
            // context.Attach(cashReceive.Customer);

            if (salarySheet.SalarySheetID == 0)
            {

                context.SalarySheets.Add(salarySheet);

            }

            else
            {
           

                context.Entry(salarySheet).State = EntityState.Modified;
           
            }



            try
            {
                context.SaveChanges();
                context.Entry(salarySheet).State = EntityState.Detached;


                context.Entry(salarySheet).Reload();
            
                res.Obj = salarySheet;

                res.ResultID = 1;


                res.ResultMessage = "Successfully Added /Updated !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;
        }

        public ResultObj SaveSalarySheetHead(SalarySheetHead salarySheetHead)
        {
            ResultObj res = new ResultObj();
            if (salarySheetHead.SalarySheetHeadID == 0)
            {
                context.SalarySheetHeads.Add(salarySheetHead);
            }
            else
            {
                context.Entry(salarySheetHead).State = EntityState.Modified;

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
    }
}
