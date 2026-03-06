using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface IChequeTransactionRepository
    {

        IQueryable<ChequeTransaction> ChequeTransactions { get; }
        IQueryable<ChequeTransaction> ChequeTransactionDetails { get; }
        IQueryable<BankAccount> BankAccounts { get; }



        IQueryable<Bank> Banks { get; }
        ResultObj SaveBank(Bank bank);
        ResultObj DeleteBank(Bank bank);


      
        ResultObj SaveBankAccount(BankAccount bankAccount);
        ResultObj DeleteBankAccount(BankAccount bankAccount);
       



        ResultObj SaveChequeTransaction(ChequeTransaction chequeTransaction,int prevCustomerID, int prevSupplierID, decimal previousAmount);
        SalesDetails FindChequeTransaction(int id);
        SalesDetails FindChequeTransactionByInvoice(string _invoiceNo);
        ResultObj UpdateChequeTransaction(ChequeTransaction chequeTransaction);
        ResultObj DeleteChequeTransaction(ChequeTransaction obj,int prevCustID,int prevSuppID,decimal prevAmount);
    }
}
