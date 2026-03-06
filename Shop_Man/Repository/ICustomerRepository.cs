
using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> Customers { get; }
        ResultObj SaveCustomer(Customer customer);
        ResultObj UpdateCustomerLimit(Customer customer);
        ResultObj LockUnlockCustomer(Customer customer);
        ResultObj UpdateCustomer(Customer customer);
        ResultObj DeleteCustomer(Customer customer);

        decimal GetCustomerPreviousBalance(int id);
        Customer GetCustomer(int id);

        List<SP_CustomerBalance_Datewise> DatewiseCustomerBalanceList(int custID, DateTime formDate, DateTime toDate);
        List<SP_CustomerBalance_Details>  CustomerBalanceDetailsList(int custID);
        List<SP_CustomerBalance_ALL> CustomerBalanceALL();

        /// <summary>
        /// / Year Close 
        /// </summary>
        /// <returns></returns>
        List<SP_CustomersWithLastClosingYear> CustomersWithLastClosingYearList();

        ResultObj SaveCustomerYearClose(int userID, int compID, string note, int custID, int yearName,string yearCloseDate);
        ResultObj Get_SP_Entry_Customer_Year_Close(int _userID, int _comID, int custID, int YearName,DateTime yearCloseDate);

        IQueryable<YearCloseCustomerSumm> YearCloseCustomerSummList { get; }

        IQueryable<YearCloseCustomerDetails> YearCloseCustomerDetailsList { get; }

        List<View_Customer_And_YearClosing> Get_Customer_And_YearClosing(int yearName);


        List<SP_Customer_Close_SizeDetails> Get_SP_Customer_Close_SizeDetails(int yearName,int custID);


        List<SP_Customer_Close_SizeDetails> Get_SP_Customer_Close_SizeDetails_Unprocessed(int yearName, int custID, string ddate);
        List<SP_View_Customer_Year_Close> Get_SP_View_Customer_Year_Close( int customerID,  int yearName, string yearcloseDate);
        List<SP_View_ALL_Customer_Year_Close> Get_SP_View_ALL_Customer_Year_Close(string yearcloseDate);

    }
}
