using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface ICompanyProductRepository
    {

        IQueryable<CompanyProduct> CompanyProducts { get; }
        ResultObj SaveCompanyProduct(CompanyProduct companyProduct);

        List<CompanyProduct> GetCompanyProductInCompany(int _suppID);
        List<CompanyProduct> GetCompanyProductInCompanyAndProdDoron(int _suppID,int prodDoronID);

        ResultObj DeleteCompanyProduct(CompanyProduct companyProduct);

        List<CompanyProduct> GetCompanyProductByProName(int prodNameID);
        IQueryable<SP_Current_Stock> GetCurrentStock();
        IQueryable<SP_Datewise_Stock> GetDatewiseStock(int _compID,DateTime _fromDate,DateTime _todate);

        IQueryable<SP_Current_Stock_Article_Size> Get_SP_Current_Stock_Article_Size(CompanyProduct model);

        List<LastEntryDates> LastEntryDated();

        List<View_Item_Close> Get_View_Item_Close_List( DateTime _todate);

        List<View_Item_Close> Get_View_Item_Current_Close_List(DateTime _todate);
        ResultObj Save_Item_Close_List(int userID, int compID, DateTime _todate);

        ResultObj Delete_Item_Close_List(int userID, int compID, DateTime _todate);
    }
}
