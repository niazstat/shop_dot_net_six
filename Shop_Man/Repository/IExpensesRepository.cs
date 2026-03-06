using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface IExpensesRepository
    {


        IQueryable<Expense> Expenses { get; }
        IQueryable<ExpensesHead> ExpensesHeads { get; }
        ResultObj SaveExpense(Expense expense);
        Expense FindExpense(int _ExpenseID);

        ResultObj DeleteExpense(Expense expense);

        ResultObj SaveExpensesHead(ExpensesHead expensesHead);
        ExpensesHead FindExpensesHead(int _expensesHeadID);
        List<SP_Expenses_Datewise> SP_Expenses_Datewise(DateTime fromDate, DateTime toDate,string ids);

        ResultObj DeleteExpensesHead(ExpensesHead expensesHead);
    }
}
