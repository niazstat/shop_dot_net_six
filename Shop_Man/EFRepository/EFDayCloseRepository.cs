using Microsoft.EntityFrameworkCore;
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using Shop_Man.Models.ViewModels;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFDayCloseRepository : IDayCloseRepository
    {

        private OrderManagementDBContext context;
        public EFDayCloseRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<DayClose> DayCloses => context.DayCloses;

        public IQueryable<DayCloseDetails> DayCloseDetails => throw new NotImplementedException();

        public IQueryable<DeletedSayClose> DeletedSayClose => context.DeletedSayCloses;

        public IQueryable<YearClose> YearCloses => context.YearCloses;

        public IQueryable<YearItemStockClose> YearItemStockCloses => context.YearItemStockCloses;

        //public List<SP_View_DayClose_Details> DayCloseDetailsList(DayCloseViewModel model)
        //{

        //    string ss = String.Format("EXEC [SP_View_DayClose_Details] {0},{1},{2},{3},{4}", model.UserID, model.CompID, model.dDate, model.TransType, model.ViewType);
        //    List<SP_View_DayClose_Details> obj = new List<SP_View_DayClose_Details>();
        //    obj = context.Query<SP_View_DayClose_Details>().FromSql(@"EXEC [SP_View_DayClose_Details] {0},{1},{2},{3},{4}", model.UserID, model.CompID, model.dDate, model.TransType, model.ViewType).ToList();
        //    return obj;
        //}
        public List<SP_View_DayClose_Details> DayCloseDetailsList(DayCloseViewModel model)
        {
            return context.SP_View_DayClose_Details
                .FromSqlInterpolated(
                    $"EXEC [SP_View_DayClose_Details] {model.UserID}, {model.CompID}, {model.dDate}, {model.TransType}, {model.ViewType}"
                )
                .AsNoTracking()
                .AsEnumerable()
                .ToList();
        }

        //public List<SP_View_DayClose_SALES_PUR_Details> DayCloseDetailsSalesPurchaseList(DayCloseViewModel model)
        //{
        //    string ss = String.Format("EXEC [SP_View_DayClose_SALES_PUR_Details] {0},{1},{2},{3},{4}", model.UserID, model.CompID, model.dDate, model.TransType, model.ViewType);
        //    List<SP_View_DayClose_SALES_PUR_Details> obj = new List<SP_View_DayClose_SALES_PUR_Details>();
        //    obj = context.Query<SP_View_DayClose_SALES_PUR_Details>().FromSql(@"EXEC [SP_View_DayClose_SALES_PUR_Details] {0},{1},{2},{3},{4}", model.UserID, model.CompID, model.dDate, model.TransType, model.ViewType).ToList();
        //    return obj;
        //}


        public List<SP_View_DayClose_SALES_PUR_Details> DayCloseDetailsSalesPurchaseList(DayCloseViewModel model)
        {
            return context.SP_View_DayClose_SALES_PUR_Details
                .FromSqlInterpolated(
                    $"EXEC [SP_View_DayClose_SALES_PUR_Details] {model.UserID}, {model.CompID}, {model.dDate}, {model.TransType}, {model.ViewType}"
                )
                .AsNoTracking()
                .AsEnumerable()
                .ToList();
        }



        //public ResultObj DeleteDayClose(int userID, int compID, DateTime dDate)
        //{
        //    {
        //        ResultObj res = new ResultObj();


        //        SP_Process_Delete_DayClose resID = new SP_Process_Delete_DayClose();
        //        try
        //        {
        //            resID = context.Query<SP_Process_Delete_DayClose>().FromSql(@"EXEC SP_Process_Delete_DayClose {0},{1},{2}", dDate, userID, compID).Take(1).SingleOrDefault();
        //            res.ResultID = 1;
        //            res.ResultMessage = "Successfully Deleted  !";
        //        }
        //        catch (Exception ex)
        //        {
        //            res.ResultID = -1;
        //            res.ResultMessage = ex.ToString();
        //        }

        //        return res;
        //    }
        //}

        public ResultObj DeleteDayClose(int userID, int compID, DateTime dDate)
        {
            var res = new ResultObj();

            try
            {
                var resID = context.SP_Process_Delete_DayClose
                    .FromSqlInterpolated(
                        $"EXEC SP_Process_Delete_DayClose {dDate}, {userID}, {compID}"
                    )
                    .AsNoTracking()
                    .AsEnumerable()
                    .FirstOrDefault(); // take single record safely

                res.ResultID = 1;
                res.ResultMessage = "Successfully Deleted!";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        public ResultObj DeleteYearClose(int userID, int compID, int YearName)
        {
            throw new NotImplementedException();
        }

        //public ResultObj GetDataForEntry(DateTime dDate, int compID)
        //{
        //    ResultObj res = new ResultObj();
        //    SP_Entry_DayClose obj = new SP_Entry_DayClose();

        //    try
        //    {
        //        obj= context.Query<SP_Entry_DayClose>().FromSql(@"EXEC [SP_Entry_DayClose] {0},{1}", compID, dDate).Take(1).SingleOrDefault(); 
        //        res.Obj = obj;
        //        res.ResultID = 1;
        //        res.ResultMessage = obj.Note;
        //    }
        //    catch (Exception  ex) {
        //        res.ResultID = -1;
        //        res.ResultMessage =ex.ToString();
        //    }
        //        return res;

        //}

        public ResultObj GetDataForEntry(DateTime dDate, int compID)
        {
            var res = new ResultObj();

            try
            {
                var obj = context.SP_Entry_DayClose
                    .FromSqlInterpolated(
                        $"EXEC [SP_Entry_DayClose] {compID}, {dDate}"
                    )
                    .AsNoTracking()
                    .AsEnumerable()
                    .FirstOrDefault(); // safer than Take(1).SingleOrDefault()

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


        //public ResultObj SaveDayClose(int userID, int compID, decimal addLess, string note, DateTime dDate, int processType)
        //{
        //    ResultObj res = new ResultObj();


        //    SP_Process_DayClose resID = new SP_Process_DayClose();
        //    try
        //    {
        //        resID = context.Query<SP_Process_DayClose>().FromSql(@"EXEC SP_Process_DayClose {0},{1},{2},{3},{4},{5}", userID, compID, addLess, note, dDate, processType).Take(1).SingleOrDefault();
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
        public ResultObj SaveDayClose(int userID, int compID, decimal addLess, string note, DateTime dDate, int processType)
        {
            var res = new ResultObj();

            try
            {
                var resID = context.SP_Process_DayClose
                    .FromSqlInterpolated(
                        $"EXEC SP_Process_DayClose {userID}, {compID}, {addLess}, {note}, {dDate}, {processType}"
                    )
                    .AsNoTracking()
                    .AsEnumerable()
                    .FirstOrDefault(); // take single record safely

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

        // ---#################   Year Close-------------###############
        //public ResultObj GetDataForEntryYearCLose(int yearName, int compID)
        //{
        //    ResultObj res = new ResultObj();
        //    SP_Entry_Year_Close obj = new SP_Entry_Year_Close();

        //    try
        //    {
        //        obj = context.Query<SP_Entry_Year_Close>().FromSql(@"EXEC [SP_Entry_Year_Close] {0},{1}", compID, yearName).Take(1).SingleOrDefault();
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

   

        //public ResultObj SaveYearClose(int userID, int compID, decimal addLess, string note, int yearName, int processType)
        //{
        //    ResultObj res = new ResultObj();


        //    SP_Process_YearClose resID = new SP_Process_YearClose();
        //    try
        //    {
        //        resID = context.Query<SP_Process_YearClose>().FromSql(@"EXEC SP_Process_YearClose {0},{1},{2},{3},{4},{5}", userID, compID,yearName ,addLess, note, processType).Take(1).SingleOrDefault();
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

        public ResultObj SaveYearClose(int userID, int compID, decimal addLess, string note, int yearName, int processType)
        {
            var res = new ResultObj();

            try
            {
                var resID = context.SP_Process_YearClose
                    .FromSqlInterpolated(
                        $"EXEC SP_Process_YearClose {userID}, {compID}, {yearName}, {addLess}, {note}, {processType}"
                    )
                    .AsNoTracking()
                    .AsEnumerable()
                    .FirstOrDefault(); // safely take first or null

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

        //public List<SP_View_YearClose_Details> YearCloseDetailsList(int comId, int YearName, string TransType)
        //{
        //    string ss = String.Format("EXEC [SP_View_YearClose_Details] {0},{1},{2}", comId, YearName, TransType);
        //    List<SP_View_YearClose_Details> obj = new List<SP_View_YearClose_Details>();
        //    obj = context.Query<SP_View_YearClose_Details>().FromSql(@"EXEC [SP_View_YearClose_Details] {0},{1},{2}", comId, YearName, TransType).ToList();
        //    return obj;
        //}
        public List<SP_View_YearClose_Details> YearCloseDetailsList(int comId, int yearName, string transType)
        {
            return context.SP_View_YearClose_Details
                .FromSqlInterpolated(
                    $"EXEC [SP_View_YearClose_Details] {comId}, {yearName}, {transType}"
                )
                .AsNoTracking() // read-only
                .AsEnumerable()
                .ToList();
        }

        //public List<SP_View_DayClose_Yearwise_Actual> Get_SP_View_DayClose_Yearwise_Actual(int comId, DateTime fromDate, DateTime toDate)
        //{
        //    List<SP_View_DayClose_Yearwise_Actual> obj = new List<SP_View_DayClose_Yearwise_Actual>();
        //    obj = context.Query<SP_View_DayClose_Yearwise_Actual>().FromSql(@"EXEC [SP_View_DayClose_Yearwise_Actual] {0},{1}", fromDate, toDate).ToList();
        //    return obj;
        //}
        public List<SP_View_DayClose_Yearwise_Actual> Get_SP_View_DayClose_Yearwise_Actual(int comId, DateTime fromDate, DateTime toDate)
        {
            return context.SP_View_DayClose_Yearwise_Actual
                .FromSqlInterpolated(
                    $"EXEC [SP_View_DayClose_Yearwise_Actual] {fromDate}, {toDate}"
                )
                .AsNoTracking()
                .AsEnumerable()
                .ToList();
        }

        //public List<SP_View_DayClose_Daywise_Actual> Get_SP_View_DayClose_Daywise_Actual(int comId, DateTime fromDate, DateTime toDate)
        //{
        //    List<SP_View_DayClose_Daywise_Actual> obj = new List<SP_View_DayClose_Daywise_Actual>();
        //    obj = context.Query<SP_View_DayClose_Daywise_Actual>().FromSql(@"EXEC [SP_View_DayClose_Daywise_Actual] {0},{1}", fromDate, toDate).ToList();
        //    return obj;
        //}
        public List<SP_View_DayClose_Daywise_Actual> Get_SP_View_DayClose_Daywise_Actual(int comId, DateTime fromDate, DateTime toDate)
        {
            return context.SP_View_DayClose_Daywise_Actual
                .FromSqlInterpolated(
                    $"EXEC [SP_View_DayClose_Daywise_Actual] {fromDate}, {toDate}"
                )
                .AsNoTracking()
                .AsEnumerable()
                .ToList();
        }
        public List<SP_View_DayClose_Yearwise_Actual> SP_View_DayClose_Yearwise_Dayclose(int comId, DateTime fromDate, DateTime toDate)
        {
            return context.SP_View_DayClose_Yearwise_Actual
                .FromSqlInterpolated($"EXEC [SP_View_DayClose_Yearwise_Dayclose] {fromDate}, {toDate}")
                .AsNoTracking()
                .AsEnumerable()
                .ToList();
        }

        public List<SP_Yearly_Profit_Loss> SP_Yearly_Profit_Loss(int yearName)
        {
            return context.SP_Yearly_Profit_Loss
                .FromSqlInterpolated($"EXEC [SP_Yearly_Profit_Loss] {yearName}")
                .AsNoTracking()
                .AsEnumerable()
                .ToList();
        }

        public List<SP_Yearly_Profit_Loss> SP_DateWise_Profit_Loss(DateTime fromDate, DateTime toDate)
        {
            return context.SP_Yearly_Profit_Loss
                .FromSqlInterpolated($"EXEC [SP_DateWise_Profit_Loss] {fromDate}, {toDate}")
                .AsNoTracking()
                .AsEnumerable()
                .ToList();
        }

        public ResultObj GetDataForEntryYearCLose(int yearName, int compID)
        {
            var res = new ResultObj();

            try
            {
                var obj = context.SP_Entry_Year_Close
                    .FromSqlInterpolated(
                        $"EXEC [SP_Entry_Year_Close] {compID}, {yearName}"
                    )
                    .AsNoTracking()
                    .AsEnumerable()
                    .FirstOrDefault(); // safely take first or null

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

        //public List<SP_View_DayClose_Yearwise_Actual> SP_View_DayClose_Yearwise_Dayclose(int comId, DateTime fromDate, DateTime toDate)
        //{
        //    List<SP_View_DayClose_Yearwise_Actual> obj = new List<SP_View_DayClose_Yearwise_Actual>();
        //    obj = context.Query<SP_View_DayClose_Yearwise_Actual>().FromSql(@"EXEC [SP_View_DayClose_Yearwise_Dayclose] {0},{1}", fromDate, toDate).ToList();
        //    return obj;
        //}

        //public List<SP_Yearly_Profit_Loss> SP_Yearly_Profit_Loss(int yearName)
        //{
        //    List<SP_Yearly_Profit_Loss> obj = new List<SP_Yearly_Profit_Loss>();
        //    obj = context.Query<SP_Yearly_Profit_Loss>().FromSql(@"EXEC [SP_Yearly_Profit_Loss] {0}", yearName).ToList();
        //    return obj;
        //}

        //public List<SP_Yearly_Profit_Loss> SP_DateWise_Profit_Loss(DateTime fromDate, DateTime toDate)
        //{
        //    List<SP_Yearly_Profit_Loss> obj = new List<SP_Yearly_Profit_Loss>();
        //    obj = context.Query<SP_Yearly_Profit_Loss>().FromSql(@"EXEC [SP_DateWise_Profit_Loss] {0},{1}", fromDate, toDate).ToList();
        //    return obj;
        //}
    }
}
