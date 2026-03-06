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
    public class EFChequeTransactionRepository : IChequeTransactionRepository
    {
        private OrderManagementDBContext context;
        public EFChequeTransactionRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<ChequeTransaction> ChequeTransactions => context.ChequeTransactions;

        public IQueryable<ChequeTransaction> ChequeTransactionDetails => context.ChequeTransactions.Include(a=>a.Customer).Include(a=>a.Supplier).Include(a=>a.PaymentMedium).Include(a=>a.BankAccount);

        public IQueryable<Bank> Banks => context.Banks;

        public IQueryable<BankAccount> BankAccounts => context.BankAccounts.Include(a=>a.Bank);

        public ResultObj DeleteBank(Bank bank)
        {

            ResultObj res = new ResultObj();

            Bank dbEntry = context.Banks
                   .FirstOrDefault(p => p.BankID == bank.BankID);
            if (dbEntry != null)
            {
                context.Banks.Remove(dbEntry);

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



        public ResultObj DeleteChequeTransaction(ChequeTransaction obj, int prevCustomerID, int prevSupplierID, decimal previousAmount)
        {

            ResultObj res = new ResultObj();

            ChequeTransaction dbEntry = context.ChequeTransactions
                   .FirstOrDefault(p => p.ChequeTransactionID == obj.ChequeTransactionID);
            if (dbEntry != null)
            {
                if (previousAmount > 0)
                {
                    if (prevSupplierID > 0)
                    {
                        Supplier prevSupp = context.Suppliers.SingleOrDefault(a => a.SupplierId == prevSupplierID);
                        prevSupp.CurrentBalance = prevSupp.CurrentBalance + previousAmount;//
                        context.Entry(prevSupp).State = EntityState.Modified;
                    }
                    else if (prevCustomerID > 0)
                    {
                        Customer prevCust = context.Customer.SingleOrDefault(a => a.CustomerID == prevCustomerID);
                        prevCust.CurrentBalance = prevCust.CurrentBalance + previousAmount;//
                        context.Entry(prevCust).State = EntityState.Modified;
                    }

                }


                context.ChequeTransactions.Remove(dbEntry);

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
  

        public SalesDetails FindChequeTransaction(int id)
        {
            throw new NotImplementedException();
        }

        public SalesDetails FindChequeTransactionByInvoice(string _invoiceNo)
        {
            throw new NotImplementedException();
        }

        public ResultObj SaveBank(Bank bank)
        {

            ResultObj res = new ResultObj();
            if (bank.BankID == 0)
            {
                context.Banks.Add(bank).GetDatabaseValues();
            }

            else
            {
                context.Entry(bank).State = EntityState.Modified;
             
            }
            try
            {
                context.SaveChanges();
               
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

        public ResultObj SaveBankAccount(BankAccount bankAccount)
        {
            ResultObj res = new ResultObj();


            // context.Attach(cashReceive.PaymentMedium);
            // context.Attach(cashReceive.Customer);

            if (bankAccount.BankAccountID == 0)
            {

                context.BankAccounts.Add(bankAccount).GetDatabaseValues();
            }

            else
            {
                context.Entry(bankAccount).State = EntityState.Modified;

            }

            try
            {
                context.SaveChanges();
                context.Entry(bankAccount).State = EntityState.Detached;


                //context.Entry(chequeTransaction).Reload();
               // res.ResultNo = chequeTransaction.GeneratedInvoicNo;
                // res.Obj = cashReceive;

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
        public ResultObj DeleteBankAccount(BankAccount bankAccount)
        {
            ResultObj res = new ResultObj();

            BankAccount dbEntry = context.BankAccounts
                   .FirstOrDefault(p => p.BankAccountID == bankAccount.BankAccountID);
            if (dbEntry != null)
            {
                context.BankAccounts.Remove(dbEntry);

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
        public ResultObj SaveChequeTransaction(ChequeTransaction chequeTransaction, int prevCustomerID, int prevSupplierID, decimal previousAmount)
        {
            ResultObj res = new ResultObj();


            // context.Attach(cashReceive.PaymentMedium);
            // context.Attach(cashReceive.Customer);

            if (chequeTransaction.IsChequePassed)
            {
                //decimal _amount = chequeTransaction.Amount;
                //if (chequeTransaction.Supplier != null)
                //{
                  
                //    if (chequeTransaction.Type.ToUpper() == "RECEIVE")
                //    {
                //        _amount = -1 * _amount;
                //    }

                //    Supplier newSupp = context.Suppliers.SingleOrDefault(a => a.SupplierId == chequeTransaction.Supplier.SupplierId);
                //    newSupp.CurrentBalance = newSupp.CurrentBalance - _amount;//
                //    context.Entry(newSupp).State = EntityState.Modified;
                //}
                //else if (chequeTransaction.Customer != null)
                //{
                //    if (chequeTransaction.Type.ToUpper() == "PAYMENT")
                //    {
                //        _amount = -1 * _amount;
                //    }
                //    Customer newCust = context.Customer.SingleOrDefault(a => a.CustomerID == chequeTransaction.Customer.CustomerID);
                //    newCust.CurrentBalance = newCust.CurrentBalance - _amount;//
                //    context.Entry(newCust).State = EntityState.Modified;
                //}
            }




            if (chequeTransaction.ChequeTransactionID == 0)
            {
           
                context.ChequeTransactions.Add(chequeTransaction).GetDatabaseValues();
            }

            else
            {

                if (previousAmount > 0)
                {
                    if (prevSupplierID >0)
                    {
                        Supplier prevSupp = context.Suppliers.SingleOrDefault(a => a.SupplierId == prevSupplierID);
                        prevSupp.CurrentBalance = prevSupp.CurrentBalance + previousAmount;//
                        context.Entry(prevSupp).State = EntityState.Modified;
                    }
                    else if (prevCustomerID >0)
                    {
                        Customer prevCust = context.Customer.SingleOrDefault(a => a.CustomerID == prevCustomerID);
                        prevCust.CurrentBalance = prevCust.CurrentBalance + previousAmount;//
                        context.Entry(prevCust).State = EntityState.Modified;
                    }

                }


                context.Entry(chequeTransaction).State = EntityState.Modified;
             
            }

            try
            {
                context.SaveChanges();
                context.Entry(chequeTransaction).State = EntityState.Detached;


                context.Entry(chequeTransaction).Reload();
                res.ResultNo = chequeTransaction.GeneratedInvoicNo;
                // res.Obj = cashReceive;

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

        public ResultObj UpdateChequeTransaction(ChequeTransaction chequeTransaction)
        {
            throw new NotImplementedException();
        }
    }
}
