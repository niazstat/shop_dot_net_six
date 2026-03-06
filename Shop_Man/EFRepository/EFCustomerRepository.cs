
using Microsoft.EntityFrameworkCore;
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private OrderManagementDBContext context;
        public EFCustomerRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }


        public IQueryable<YearCloseCustomerSumm> YearCloseCustomerSummList => context.YearCloseCustomerSumms;
        

        public IQueryable<YearCloseCustomerDetails> YearCloseCustomerDetailsList=> context.YearCloseCustomerDetailses;
        

        public List<Customer> Customers => context.Customer.Include(a=>a.CustomerSubCategory).Include(a=>a.District)?.ToList();

        public List<SP_CustomerBalance_ALL> CustomerBalanceALL()
        {
            return context.SP_CustomerBalance_ALL
                .FromSqlInterpolated($"EXEC SP_CustomerBalance_ALL")
                .AsNoTracking()
        .AsEnumerable()
                .OrderBy(a => a.CustomerID)
                .ToList();
        }

        public List<SP_CustomerBalance_Details> CustomerBalanceDetailsList(int custID)
        {
            return context.SP_CustomerBalance_Details
                .FromSqlInterpolated($"EXEC SP_CustomerBalance_Details {custID}")
                .AsNoTracking()
                 .AsEnumerable()
                .OrderBy(a => a.dDate)
                .ThenBy(b => b.ID)
                .ToList();
        }

        public List<SP_CustomersWithLastClosingYear> CustomersWithLastClosingYearList()
        {
            return context.SP_CustomersWithLastClosingYear
                .FromSqlInterpolated($"EXEC SP_CustomersWithLastClosingYear")
                .AsNoTracking()
                .ToList();
        }

        public List<SP_CustomerBalance_Datewise> DatewiseCustomerBalanceList(int custID, DateTime fromDate, DateTime toDate)
        {
            return context.SP_CustomerBalance_Datewise
                .FromSqlInterpolated($"EXEC SP_CustomerBalance_Datewise {custID}, {fromDate}, {toDate}")
                .AsNoTracking()
        .AsEnumerable()
                .OrderBy(a => a.dDate)
                .ThenBy(b => b.ID)
                .ToList();
        }

        //public List<SP_CustomerBalance_ALL> CustomerBalanceALL()
        //{
        //    return context.Query<SP_CustomerBalance_ALL>().FromSql(@"EXEC SP_CustomerBalance_ALL").OrderBy(a => a.CustomerID).ToList();

        //}

        //public List<SP_CustomerBalance_Details> CustomerBalanceDetailsList(int custID)
        //{
        //    return context.Query<SP_CustomerBalance_Details>().FromSql(@"EXEC SP_CustomerBalance_Details {0}", custID).OrderBy(a => a.dDate).ThenBy(b => b.ID).ToList();

        //}

        //public List<SP_CustomersWithLastClosingYear> CustomersWithLastClosingYearList()
        //{
        //    return context.Query<SP_CustomersWithLastClosingYear>().FromSql(@"EXEC SP_CustomersWithLastClosingYear").ToList();

        //}

        //public List<SP_CustomerBalance_Datewise> DatewiseCustomerBalanceList(int custID, DateTime formDate, DateTime toDate)
        //{
        //   return context.Query<SP_CustomerBalance_Datewise>().FromSql(@"EXEC SP_CustomerBalance_Datewise {0},{1},{2}", custID, formDate,toDate).OrderBy(a=>a.dDate).ThenBy(b=>b.ID).ToList();

        //}

        public ResultObj DeleteCustomer(Customer customer)
        {
            ResultObj res = new ResultObj();



            Customer dbEntry = context.Customer
                   .FirstOrDefault(p => p.CustomerID == customer.CustomerID);
            if (dbEntry.IsLocked)
            {
                res.ResultID = -1;
                res.ResultMessage = "Edite Blocked ,Please Unlock Block!";
                return res;
            }
            if (dbEntry != null)
            {
                context.Customer.Remove(dbEntry);

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

        public Customer GetCustomer(int id)
        {
            return Customers.SingleOrDefault(a=>a.CustomerID==id);
        }

        //public decimal GetCustomerPreviousBalance(int id)
        //{
        //    CustomerPrevBalancce_SP obj= context.Query<CustomerPrevBalancce_SP>().FromSql(@"EXEC CustomerPrevBalancce_SP {0}", id).First();

        //    return obj.PrevBalance;


        //}
        public decimal GetCustomerPreviousBalance(int id)
        {
            var obj = context.CustomerPrevBalancce_SP
                .FromSqlInterpolated($"EXEC CustomerPrevBalancce_SP {id}")
                .AsNoTracking()
                 .ToList()   // 👈 Important
                .FirstOrDefault();

            return obj?.PrevBalance ?? 0; // return 0 if no record found
        }

        public ResultObj LockUnlockCustomer(Customer customer)
        {
            ResultObj res = new ResultObj();


            Customer dbEntry = context.Customer
                   .FirstOrDefault(p => p.CustomerID == customer.CustomerID);
            if (dbEntry != null)
            {
                dbEntry.IsLocked = !dbEntry.IsLocked;
                dbEntry.LastUpdateTime = DateTime.Now;
                dbEntry.UpdateUserID = customer.UserId;
            }
            else
            {
                res.ResultID = -1;
                res.ResultMessage = "Invalid Customer";
                return res;
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

        public ResultObj SaveCustomer(Customer customer)
        {
            ResultObj res = new ResultObj();
            if (customer.CustomerID == 0)
            {


                

                context.Customer.Add(customer);


            }
            else
            {

                Customer dbEntry = context.Customer
                       .FirstOrDefault(p => p.CustomerID == customer.CustomerID);
                if (dbEntry.IsLocked)
                {
                    res.ResultID = -1;
                    res.ResultMessage = "Edite Blocked ,Please Unlock Block!";
                    return res;
                }
                if (dbEntry != null)
                {
                    dbEntry.Name = customer.Name;
                    dbEntry.ShopName = customer.ShopName;
                    dbEntry.CustomerSubCategoryID = customer.CustomerSubCategoryID;
                    dbEntry.DistrictID = customer.DistrictID;
                    dbEntry.Address1 = customer.Address1;
                    dbEntry.MobileNo = customer.MobileNo;
                    dbEntry.MobileNo2 = customer.MobileNo2;
                    dbEntry.OpeningBalance = customer.OpeningBalance;
                    dbEntry.OpeningCommission = customer.OpeningCommission;
                    dbEntry.OpeningQty = customer.OpeningQty;
                    dbEntry.LastUpdateTime = DateTime.Now;
                    dbEntry.UpdateUserID = customer.UserId;
                    dbEntry.IsBlocked = customer.IsBlocked;
                    dbEntry.StartingYear = customer.StartingYear;
                    dbEntry.IsBlocked = customer.IsBlocked;

                }
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
        public ResultObj Get_SP_Entry_Customer_Year_Close(int userID, int compID, int custID, int yearName, DateTime yearCloseDate)
        {
            var res = new ResultObj();

            try
            {
                var obj = context.SP_Entry_Customer_Year_Close
                    .FromSqlInterpolated($"EXEC SP_Entry_Customer_Year_Close {userID}, {compID}, {custID}, {yearName}, {yearCloseDate}")
                    .AsNoTracking()
                      .AsEnumerable()
                    .FirstOrDefault();

                res.Obj = obj;
                res.ResultID = 1;
                res.ResultMessage = obj?.Note ?? "No record found";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        //public ResultObj Get_SP_Entry_Customer_Year_Close(int _userID,int _comID,int custID, int YearName,DateTime yearcloseDate)
        //{

        //    ResultObj res = new ResultObj();
        //    SP_Entry_Customer_Year_Close obj = new SP_Entry_Customer_Year_Close();

        //    try
        //    {
        //        obj = context.Query<SP_Entry_Customer_Year_Close>().FromSql(@"EXEC SP_Entry_Customer_Year_Close {0},{1},{2},{3},{4}", _userID, _comID, custID, YearName,yearcloseDate).Take(1).SingleOrDefault();

        //        res.Obj = obj;
        //        res.ResultID = 1;
        //        res.ResultMessage = obj.Note;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ResultID = -1;
        //        res.ResultMessage = ex.ToString();
        //    }
        //    return res;


        //}

        public ResultObj UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public ResultObj UpdateCustomerLimit(Customer customer)
        {
            ResultObj res = new ResultObj();
         

                Customer dbEntry = context.Customer
                       .FirstOrDefault(p => p.CustomerID == customer.CustomerID);
                if (dbEntry != null)
                {
                dbEntry.MaxBalanceLimit = customer.MaxBalanceLimit;
                dbEntry.LastUpdateTime = DateTime.Now;
                dbEntry.UpdateUserID = customer.UserId;
            }
            else
            {
                res.ResultID = -1;
                res.ResultMessage ="Invalid Customer";
                return res;
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


        //@userID int,@compID int,@customerID int,@yearName int,@AddLess decimal,@closingNote varchar(1000),@processType int )
        //public ResultObj SaveCustomerYearClose(int userID, int compID, string note, int custID, int yearName, string yearCloseDate)
        //{
        //    ResultObj res = new ResultObj();


        //    SP_Process_Customer_Year_Close resID = new SP_Process_Customer_Year_Close();

        //    string rrrr = String.Format("EXEC SP_Process_Customer_Year_Close {0},{1},{2},{3},{4},{5},{6},{7}", userID, compID, custID, yearName, 0.0, note, 1, yearCloseDate);
        //    try
        //    {
        //        resID = context.Query<SP_Process_Customer_Year_Close>().FromSql(@"EXEC SP_Process_Customer_Year_Close {0},{1},{2},{3},{4},{5},{6},{7}", userID, compID, custID,yearName,0.0, note,1, yearCloseDate).Take(1).SingleOrDefault();
        //        res.ResultID = 1;
        //        res.ResultMessage = "Successfully Added /Updated !";
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ResultID = -1;
        //        res.ResultMessage = ex.ToString();
        //    }

        //    return res;
        //}

        public ResultObj SaveCustomerYearClose(int userID, int compID, string note, int custID, int yearName, string yearCloseDate)
        {
            var res = new ResultObj();

            try
            {
                var resID = context.SP_Process_Customer_Year_Close
                    .FromSqlInterpolated($"EXEC SP_Process_Customer_Year_Close {userID}, {compID}, {custID}, {yearName}, {0.0}, {note}, {1}, {yearCloseDate}")
                    .AsNoTracking()
                      .AsEnumerable()
                    .FirstOrDefault();

                res.ResultID = 1;
                res.ResultMessage = "Successfully Added / Updated!";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        public List<View_Customer_And_YearClosing> Get_Customer_And_YearClosing(int yearName)
        {
            return context.View_Customer_And_YearClosing
                .FromSqlInterpolated($"SELECT * FROM View_Customer_And_YearClosing WHERE YearName = {yearName}")
                .AsNoTracking()
                  .AsEnumerable()
                .ToList();
        }

        public List<SP_Customer_Close_SizeDetails> Get_SP_Customer_Close_SizeDetails(int yearName, int custID)
        {
            return context.SP_Customer_Close_SizeDetails
                .FromSqlInterpolated($"EXEC SP_Customer_Close_SizeDetails {yearName}, {custID}")
                .AsNoTracking()
                 .AsEnumerable()
                .ToList();
        }

        public List<SP_Customer_Close_SizeDetails> Get_SP_Customer_Close_SizeDetails_Unprocessed(int yearName, int custID, string ddate)
        {
            return context.SP_Customer_Close_SizeDetails
                .FromSqlInterpolated($"EXEC SP_Customer_Close_SizeDetails_Unprocessed {yearName}, {custID}, {ddate}")
                .AsNoTracking()
                 .AsEnumerable()
                .ToList();
        }

        public List<SP_View_Customer_Year_Close> Get_SP_View_Customer_Year_Close(int customerID, int yearName, string yearcloseDate)
        {
            return context.SP_View_Customer_Year_Close
                .FromSqlInterpolated($"EXEC SP_View_Customer_Year_Close 0, 0, {customerID}, {yearName}, {yearcloseDate}")
                .AsNoTracking()
                 .AsEnumerable()
                .ToList();
        }

        public List<SP_View_ALL_Customer_Year_Close> Get_SP_View_ALL_Customer_Year_Close(string yearcloseDate)
        {
            return context.SP_View_ALL_Customer_Year_Close
                .FromSqlInterpolated($"EXEC SP_View_ALL_Customer_Year_Close {yearcloseDate}")
                .AsNoTracking()
                 .AsEnumerable()
                .ToList();
        }

        //public List<View_Customer_And_YearClosing> Get_Customer_And_YearClosing(int yearName)
        //{
        //    ResultObj res = new ResultObj();
        //    List<View_Customer_And_YearClosing> obj = new List<View_Customer_And_YearClosing>();


        //        obj = context.Query<View_Customer_And_YearClosing>().FromSql(@" Select * from View_Customer_And_YearClosing where YearName={0}",yearName).ToList();

        //    return obj;
        //}

        //public List<SP_Customer_Close_SizeDetails> Get_SP_Customer_Close_SizeDetails(int yearName, int custID)
        //{
        //    return context.Query<SP_Customer_Close_SizeDetails>().FromSql(@"EXEC SP_Customer_Close_SizeDetails {0},{1}", yearName, custID).ToList();


        //}


        //public List<SP_Customer_Close_SizeDetails> Get_SP_Customer_Close_SizeDetails_Unprocessed(int yearName, int custID,string ddate )
        //{

        //    return context.Query<SP_Customer_Close_SizeDetails>().FromSql(@"EXEC SP_Customer_Close_SizeDetails_Unprocessed {0},{1},{2}", yearName, custID, ddate).ToList();

        //}
        //public List<SP_View_Customer_Year_Close> Get_SP_View_Customer_Year_Close(int customerID, int yearName, string yearcloseDate)
        //{
        //    return context.Query<SP_View_Customer_Year_Close>().FromSql(@"EXEC SP_View_Customer_Year_Close 0,0,{0},{1},{2}", customerID, yearName, yearcloseDate).ToList();
        //}

        //public List<SP_View_ALL_Customer_Year_Close> Get_SP_View_ALL_Customer_Year_Close(  string yearcloseDate)
        //{
        //    return context.Query<SP_View_ALL_Customer_Year_Close>().FromSql(@"EXEC SP_View_ALL_Customer_Year_Close {0}", yearcloseDate).ToList();
        //}
    }
}
