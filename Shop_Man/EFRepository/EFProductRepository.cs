
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFProductRepository : IProductsRepositor
    {

        private OrderManagementDBContext context;
        public EFProductRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Size> Sizes => context.Size;

        public IQueryable<Color> Colors => context.Color;

        public IQueryable<Product> Products => context.Products;

        public IQueryable<ProdName> ProdNames => context.ProdNames;

        public IQueryable<Article> Articles => context.Articles;

        public IQueryable<ProdCoCategory> ProdCoCategorys => context.ProdCoCategorys;

        public IQueryable<UOM> UOMs => context.UOMs;

        public IQueryable<ProdType> ProdTypes => context.ProdTypes;

        public ResultObj DeleteArticle(Article article)
        {
            ResultObj res = new ResultObj();

            Article dbEntry = context.Articles
                   .FirstOrDefault(p => p.ArticleID == article.ArticleID);
            if (dbEntry != null)
            {
                context.Articles.Remove(dbEntry);
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

        public ResultObj DeleteProdCoCategory(ProdCoCategory prodCoCategory)
        {
            ResultObj res = new ResultObj();
            ProdCoCategory dbEntry = context.ProdCoCategorys
                   .FirstOrDefault(p => p.ProdCoCategoryID == prodCoCategory.ProdCoCategoryID);
            if (dbEntry != null)
            {
                context.ProdCoCategorys.Remove(dbEntry);
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

        public ResultObj DeleteProdName(ProdName prodName)
        {
            ResultObj res = new ResultObj();
            ProdName dbEntry = context.ProdNames
                   .FirstOrDefault(p => p.ProdNameID == prodName.ProdNameID);
            if (dbEntry != null)
            {
                context.ProdNames.Remove(dbEntry);
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

        public ResultObj DeleteProdType(ProdType prodType)
        {

            ResultObj res = new ResultObj();
            ProdType  dbEntry = context.ProdTypes
                   .FirstOrDefault(p => p.ProdTypeID == prodType.ProdTypeID);
            if (dbEntry != null)
            {
                context.ProdTypes.Remove(dbEntry);
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

        public ResultObj DeleteSize(Size size)
        {
            ResultObj res = new ResultObj();
            Size dbEntry = context.Size
                   .FirstOrDefault(p => p.SizeID == size.SizeID);
            if (dbEntry != null)
            {
                context.Size.Remove(dbEntry);
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

        public ResultObj SaveArticle(Article article)
        {
            ResultObj res = new ResultObj();
            if (article.ArticleID == 0)
            {
                context.Articles.Add(article);
            }

            else
            {
                Article dbEntry = context.Articles
                       .FirstOrDefault(p => p.ArticleID == article.ArticleID);
                if (dbEntry != null)
                {
                    dbEntry.Name = article.Name;
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

        public ResultObj SaveColor(Color color)
        {
            ResultObj res = new ResultObj();
            if (color.ColorID == 0)
            {
                context.Color.Add(color);
            }
            context.SaveChanges();

            return res;
        }

        public ResultObj SaveProdCoCategory(ProdCoCategory prodCoCategory)
        {
            ResultObj res = new ResultObj();
            if (prodCoCategory.ProdCoCategoryID == 0)
            {
                context.ProdCoCategorys.Add(prodCoCategory);
            }

            else
            {

                ProdCoCategory dbEntry = context.ProdCoCategorys
                       .FirstOrDefault(p => p.ProdCoCategoryID == prodCoCategory.ProdCoCategoryID);
                if (dbEntry != null)
                {

                    dbEntry.Name = prodCoCategory.Name;
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

        public ResultObj SaveProdName(ProdName prodName)
        {

            ResultObj res = new ResultObj();
            if (prodName.ProdNameID == 0)
            {
                context.ProdNames.Add(prodName);
            }

            else
            {

                ProdName dbEntry = context.ProdNames
                       .FirstOrDefault(p => p.ProdNameID == prodName.ProdNameID);
                if (dbEntry != null)
                {

                    dbEntry.Name = prodName.Name;
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


        //Ponner Doron
        public ResultObj SaveProdType(ProdType prodType)
        {
            ResultObj res = new ResultObj();
            if (prodType.ProdTypeID == 0)
            {
                context.ProdTypes.Add(prodType);
            }

            else
            {

                ProdType dbEntry = context.ProdTypes
                       .FirstOrDefault(p => p.ProdTypeID == prodType.ProdTypeID);
                if (dbEntry != null)
                {

                    dbEntry.Name = prodType.Name;
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

        public ResultObj SaveProduct(Product product)
        {
            ResultObj res = new ResultObj();
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();

            return res;
        }

        public ResultObj SaveSize(Size size)
        {
            ResultObj res = new ResultObj();
            if (size.SizeID == 0)
            {
                context.Size.Add(size);
            }

            else
            {

                Size dbEntry = context.Size
                       .FirstOrDefault(p => p.SizeID == size.SizeID);
                if (dbEntry != null)
                {

                    dbEntry.Name = size.Name;
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

        public ResultObj SaveUOM(UOM uOM)
        {

            ResultObj res = new ResultObj();
            if (uOM.UOMID == 0)
            {
                context.UOMs.Add(uOM);
            }

            else
            {

                UOM dbEntry = context.UOMs
                       .FirstOrDefault(p => p.UOMID == uOM.UOMID);
                if (dbEntry != null)
                {

                    dbEntry.Name = uOM.Name;
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
    }
}
