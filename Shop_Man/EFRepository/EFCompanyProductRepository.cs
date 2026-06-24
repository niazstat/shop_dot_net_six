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
    public class EFCompanyProductRepository : ICompanyProductRepository
    {
        private OrderManagementDBContext context;
        public EFCompanyProductRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<CompanyProduct> CompanyProducts => context.CompanyProducts.Include(a=>a.Supplier).Include(a => a.ProdType).Include(a => a.ProdName).Include(a => a.Article).Include(a => a.Size).Include(a => a.ProdCoCategory).Include(a => a.UOM);



        public ResultObj DeleteCompanyProduct(CompanyProduct companyProduct)
        {
            ResultObj res = new ResultObj();

            CompanyProduct dbEntry = context.CompanyProducts
                   .FirstOrDefault(p => p.CompanyProductID == companyProduct.CompanyProductID);
            if (dbEntry != null)
            {
                context.CompanyProducts.Remove(dbEntry);

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

        public List<CompanyProduct> GetCompanyProductByProName(int prodNameID)
        {
            return CompanyProducts.Where(a => a.ProdName.ProdNameID == prodNameID).ToList();
        }

        public List<CompanyProduct> GetCompanyProductInCompany(int _suppID)
        {
            return CompanyProducts.Where(a=>a.Supplier.SupplierId==_suppID).ToList();
        }

        public List<CompanyProduct> GetCompanyProductInCompanyAndProdDoron(int _suppID, int prodDoronID)
        {

            if (prodDoronID == 0)
            {
                return GetCompanyProductInCompany(_suppID);
            }
            else
            {
                return CompanyProducts.Where(a => a.Supplier.SupplierId == _suppID).ToList();
            }
        }

        //public IQueryable<SP_Current_Stock> GetCurrentStock()
        //{
        //    return context.Query<SP_Current_Stock>().FromSql(@"EXEC SP_Current_Stock");

        //}

        //public IQueryable<SP_Datewise_Stock> GetDatewiseStock(int _compID, DateTime _fromDate, DateTime _todate)
        //{
        //    return context.Query<SP_Datewise_Stock>().FromSql(@"EXEC SP_Datewise_Stock {0},{1},{2}", _compID, _fromDate, _todate);
        //}

        //public IQueryable<SP_Current_Stock_Article_Size> Get_SP_Current_Stock_Article_Size(CompanyProduct model)
        //{
        //    return context.Query<SP_Current_Stock_Article_Size>().FromSql(@"EXEC SP_Current_Stock_Article_Size {0},{1}",model.Article.ArticleID,model.Size.SizeID);

        //}


        public IQueryable<SP_Current_Stock> GetCurrentStock()
        {
            return context.SP_Current_Stock
                .FromSqlInterpolated($"EXEC SP_Current_Stock")
                .AsNoTracking()
                .AsQueryable();
        }

        public IQueryable<SP_Datewise_Stock> GetDatewiseStock(int compID, DateTime fromDate, DateTime toDate)
        {
            return context.SP_Datewise_Stock
                .FromSqlInterpolated($"EXEC SP_Datewise_Stock {compID}, {fromDate}, {toDate}")
                .AsNoTracking()
                .AsQueryable();
        }

        public IQueryable<SP_Current_Stock_Article_Size> Get_SP_Current_Stock_Article_Size(CompanyProduct model)
        {
            return context.SP_Current_Stock_Article_Size
                .FromSqlInterpolated($"EXEC SP_Current_Stock_Article_Size {model.Article.ArticleID}, {model.Size.SizeID}")
                .AsNoTracking()
                .AsQueryable();
        }

        public ResultObj SaveCompanyProduct(CompanyProduct companyProduct)
        {
            ResultObj res = new ResultObj();
            context.AttachRange(companyProduct.Article);
            if (companyProduct.ProdCoCategory.ProdCoCategoryID != 0) { context.AttachRange(companyProduct.ProdCoCategory); }
            else
            {
                companyProduct.ProdCoCategory = null;
            }

            // context.AttachRange(companyProduct.Company);
            context.AttachRange(companyProduct.ProdName);
            context.AttachRange(companyProduct.ProdType);
            context.AttachRange(companyProduct.UOM);
            context.AttachRange(companyProduct.Size);
            context.AttachRange(companyProduct.Supplier);
            if (companyProduct.CompanyProductID == 0)
            {

            
            
                context.CompanyProducts.Add(companyProduct);
            }


            else
            {
                CompanyProduct dbEntry = context.CompanyProducts
                       .FirstOrDefault(p => p.CompanyProductID == companyProduct.CompanyProductID);
                if (dbEntry != null)
                {
                   // dbEntry.BuyComm = companyProduct.BuyComm;
                    dbEntry.Article = companyProduct.Article;
                   // dbEntry.BuyPrice = companyProduct.BuyPrice;
                    dbEntry.Supplier = companyProduct.Supplier;
                    dbEntry.ProdCoCategory = companyProduct.ProdCoCategory==null?null: companyProduct.ProdCoCategory;
                    dbEntry.ProdName = companyProduct.ProdName;
                    dbEntry.ProdType = companyProduct.ProdType;
                    dbEntry.SellComm = companyProduct.SellComm;
                    dbEntry.SellPrice = companyProduct.SellPrice;
                    dbEntry.Size = companyProduct.Size;

                    dbEntry.UOM = companyProduct.UOM;
                  

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
        public List<View_Item_Close> Get_View_Item_Close_List(DateTime toDate)
        {
            return context.View_Item_Close
                .FromSqlInterpolated($"SELECT * FROM [View_Item_Close] WHERE ToDate = {toDate}")
                .AsNoTracking()
                   .AsEnumerable()
                .ToList();
        }

        public List<View_Item_Close> Get_View_Item_Current_Close_List(DateTime toDate)
        {
            return context.View_Item_Close
                .FromSqlInterpolated($"EXEC SP_Process_View_Items_Year_Close {toDate}")
                .AsNoTracking()
                   .AsEnumerable()
                .ToList();
        }

        public List<LastEntryDates> LastEntryDated()
        {
            return context.LastEntryDates
                .FromSqlRaw(@"
            SELECT LastEntryDate 
            FROM (
                SELECT DISTINCT REPLACE(CONVERT(VARCHAR(20), [ToDate], 106), ' ', '-') AS LastEntryDate
                FROM [View_Item_Close]
            ) A
            ORDER BY CAST(LastEntryDate AS DATE) DESC
        ")
                .AsNoTracking()
                .ToList();
        }

        //public List<View_Item_Close> Get_View_Item_Close_List(DateTime _todate)
        //{
        //    return context.Query<View_Item_Close>().FromSql(@" SELECT *  FROM [View_Item_Close]  where ToDate={0}",_todate).ToList();

        //}

        //public List<View_Item_Close> Get_View_Item_Current_Close_List(DateTime _todate)
        //{
        //    return context.Query<View_Item_Close>().FromSql(@" EXEC SP_Process_View_Items_Year_Close {0}", _todate).ToList();

        //}


        //public List<LastEntryDates> LastEntryDated()
        //{
        //    return context.Query<LastEntryDates>().FromSql(@" Select LastEntryDate from (SELECT distinct    replace( Convert(  varchar(20),[ToDate],106),' ','-') AS LastEntryDate  FROM [View_Item_Close]) A order by cast(LastEntryDate  as date)  desc ").ToList();

        //}
        public ResultObj Save_Item_Close_List(int userID, int compID, DateTime toDate)
        {
            var res = new ResultObj();

            try
            {
                var resID = context.SP_Process_Customer_Year_Close
                    .FromSqlInterpolated($"EXEC SP_Process_Items_Year_Close {userID}, {compID}, {DateTime.Today}, {toDate}")
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

        //public ResultObj Save_Item_Close_List(int userID, int compID, DateTime _todate)
        //{

        //        ResultObj res = new ResultObj();


        //        SP_Process_Customer_Year_Close resID = new SP_Process_Customer_Year_Close();

        //        string rrrr = String.Format("EXEC SP_Process_Items_Year_Close {0},{1},{2},{3}", userID, compID, DateTime.Today, _todate);
        //        try
        //        {
        //            resID = context.Query<SP_Process_Customer_Year_Close>().FromSql(@"EXEC SP_Process_Items_Year_Close {0},{1},{2},{3}", userID, compID, DateTime.Today, _todate).Take(1).SingleOrDefault();
        //            res.ResultID = 1;
        //            res.ResultMessage = "Successfully Added /Updated !";
        //        }
        //        catch (Exception ex)
        //        {
        //            res.ResultID = -1;
        //            res.ResultMessage = ex.ToString();
        //        }

        //        return res;

        //}
        public ResultObj Delete_Item_Close_List(int userID, int compID, DateTime toDate)
        {
            var res = new ResultObj();

            try
            {
                var resID = context.SP_Process_Customer_Year_Close
                    .FromSqlInterpolated($"EXEC SP_Delete_Process_Items_Year_Close {userID}, {compID}, {DateTime.Today}, {toDate}")
                    .AsNoTracking()
                       .AsEnumerable()
                    .FirstOrDefault();

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

        //public ResultObj Delete_Item_Close_List(int userID, int compID, DateTime _todate)
        //{
        //    ResultObj res = new ResultObj();


        //    SP_Process_Customer_Year_Close resID = new SP_Process_Customer_Year_Close();

        //    string rrrr = String.Format("EXEC SP_Delete_Process_Items_Year_Close {0},{1},{2},{3}", userID, compID, DateTime.Today, _todate);
        //    try
        //    {
        //        resID = context.Query<SP_Process_Customer_Year_Close>().FromSql(@"EXEC SP_Delete_Process_Items_Year_Close {0},{1},{2},{3}", userID, compID, DateTime.Today, _todate).Take(1).SingleOrDefault();
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
    }
}
