using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using Shop_Man.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
   public interface IDayCloseRepository
    {
        IQueryable<DayClose> DayCloses { get; }
        IQueryable<DayCloseDetails> DayCloseDetails { get; }

        IQueryable<DeletedSayClose> DeletedSayClose { get; }
        ResultObj GetDataForEntry(DateTime  dDate,int compID);

        List<SP_View_DayClose_Details> DayCloseDetailsList(DayCloseViewModel model);

        List<SP_View_DayClose_SALES_PUR_Details> DayCloseDetailsSalesPurchaseList(DayCloseViewModel model);
        ResultObj SaveDayClose(int userID , int compID , decimal addLess , string note , DateTime dDate , int processType);

        ResultObj DeleteDayClose(int userID, int compID,  DateTime dDate);


        //  --- Year close


        ResultObj GetDataForEntryYearCLose(int yearName, int compID);
        ResultObj SaveYearClose(int userID, int compID, decimal addLess, string note, int yearName, int processType);

        IQueryable<YearClose> YearCloses { get; }

        ResultObj DeleteYearClose(int userID, int compID, int YearName);


        List<SP_View_YearClose_Details> YearCloseDetailsList(int comId,int YearName,string TransType);
        IQueryable<YearItemStockClose> YearItemStockCloses { get; }


        List<SP_View_DayClose_Yearwise_Actual> Get_SP_View_DayClose_Yearwise_Actual(int comId, DateTime fromDate, DateTime toDate);
        List<SP_View_DayClose_Daywise_Actual> Get_SP_View_DayClose_Daywise_Actual(int comId, DateTime fromDate, DateTime toDate);


        List<SP_View_DayClose_Yearwise_Actual> SP_View_DayClose_Yearwise_Dayclose(int comId, DateTime fromDate, DateTime toDate);

        List<SP_Yearly_Profit_Loss> SP_Yearly_Profit_Loss(int yearName);
        List<SP_Yearly_Profit_Loss> SP_DateWise_Profit_Loss(DateTime fromDate, DateTime toDate);

    }
}
