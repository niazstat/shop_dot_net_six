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
    public class EFExpensesRepository : IExpensesRepository
    {
        private OrderManagementDBContext context;
        public EFExpensesRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Expense> Expenses => context.Expenses.Include(a=>a.ExpensesHead).Include(a => a.Employee);

        public IQueryable<ExpensesHead> ExpensesHeads => context.ExpensesHeads;

        public ResultObj DeleteExpense(Expense dbEntry)
        {
            ResultObj res = new ResultObj();
      
            if (dbEntry != null)
            {


                LogEntryEdit _log = new LogEntryEdit();
                _log.ChallanID = dbEntry.ExpenseID;
                _log.dDate = dbEntry.TranDate;
                _log.EditItem = "Expenses";
                _log.EditType = "Delete";
                _log.IsProcessDone = false;
                //string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
                _log.Remarks = $"Expenses head ID :{ dbEntry.ExpensesHeadID} ";
                context.LogEntryEdits.Add(_log);

                context.Expenses.Remove(dbEntry);

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

        public ResultObj DeleteExpensesHead(ExpensesHead expensesHead)
        {
            ResultObj res = new ResultObj();
            ExpensesHead dbEntry = context.ExpensesHeads
                   .FirstOrDefault(p => p.ExpensesHeadID == expensesHead.ExpensesHeadID);
            if (dbEntry != null)
            {
                context.ExpensesHeads.Remove(dbEntry);

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

        public Expense FindExpense(int _ExpenseID)
        {
            return Expenses.Include(a=>a.Employee).SingleOrDefault(a => a.ExpenseID == _ExpenseID);
        }

        public ExpensesHead FindExpensesHead(int _expensesHeadID)
        {
            return ExpensesHeads.SingleOrDefault(a => a.ExpensesHeadID == _expensesHeadID);
        }

        public ResultObj SaveExpense(Expense expense)
        {
            ResultObj res = new ResultObj();
            LogEntryEdit _log = new LogEntryEdit();
            if (expense.ExpenseID == 0)
            {
                
                _log.ChallanID = expense.ExpenseID;
                _log.dDate = expense.TranDate;
                _log.EditItem = "Expenses";
                _log.EditType = "New";
                _log.IsProcessDone = false;
                //string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
                _log.Remarks = $"Expenses head ID :{ expense.ExpensesHeadID} ";
                context.LogEntryEdits.Add(_log);


                context.Expenses.Add(expense);
            }
            else
            {
                _log.ChallanID = expense.ExpenseID;
                _log.dDate = expense.TranDate;
                _log.EditItem = "Expenses";
                _log.EditType = "Edit";
                _log.IsProcessDone = false;
                //string _supp_Cust = purchaseHead.Supplier == null ? "No Supp" : purchaseHead.Supplier.SupplierId.ToString();
                _log.Remarks = $"Expenses head ID :{ expense.ExpensesHeadID} ";
                context.LogEntryEdits.Add(_log);



                context.Entry(expense).State = EntityState.Modified;
            }
            try
            {
                context.SaveChanges();
                res.ResultID = 1;

                context.Entry(expense).State = EntityState.Detached;
                context.Entry(expense).Reload();
              
                res.ResultNo = expense.GeneratedAutoSLNo;
                res.ResultMessage = "Successfully Added !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        public ResultObj SaveExpensesHead(ExpensesHead expensesHead)
        {
            ResultObj res = new ResultObj();
            if (expensesHead.ExpensesHeadID == 0)
            {
                context.ExpensesHeads.Add(expensesHead);
            }
            else
            {
                context.Entry(expensesHead).State = EntityState.Modified;

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
        public List<SP_Expenses_Datewise> SP_Expenses_Datewise(DateTime fromDate, DateTime toDate, string ids)
        {
            try
            {
                return context.SP_Expenses_Datewise
                    .FromSqlInterpolated($"EXEC SP_Expenses_Datewise {fromDate}, {toDate}, {ids}")
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                // optionally log ex
                return new List<SP_Expenses_Datewise>();
            }
        }

        //public List<SP_Expenses_Datewise> SP_Expenses_Datewise(DateTime fromDate, DateTime toDate,string ids)
        //{
        //    List<SP_Expenses_Datewise> list = new List<SP_Expenses_Datewise>();
        //    try
        //    {
        //        list = context.Query<SP_Expenses_Datewise>().FromSql(@"EXEC SP_Expenses_Datewise {0},{1},{2}", fromDate, toDate,ids).ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return list;

        //}
    }
}
