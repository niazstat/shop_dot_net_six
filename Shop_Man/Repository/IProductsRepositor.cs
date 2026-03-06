
using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface IProductsRepositor
    {
        IQueryable<Color> Colors { get; }
        ResultObj SaveColor(Color color);


        IQueryable<Product> Products { get; }
        ResultObj SaveProduct(Product product);


        ResultObj SaveProdName(ProdName prodName);
        IQueryable<ProdName> ProdNames { get; }
        ResultObj DeleteProdName(ProdName prodName);



        ResultObj SaveArticle(Article article);
        ResultObj DeleteArticle(Article article);
        IQueryable<Article> Articles { get; }

        ResultObj SaveProdCoCategory(ProdCoCategory prodCoCategory);
        IQueryable<ProdCoCategory> ProdCoCategorys { get; }
        ResultObj DeleteProdCoCategory(ProdCoCategory prodCoCategory);


        ResultObj SaveSize(Size size);
        IQueryable<Size> Sizes { get; }
        ResultObj DeleteSize(Size size);

        ResultObj SaveProdType(ProdType prodType);
        IQueryable<ProdType> ProdTypes { get; }
        ResultObj DeleteProdType(ProdType prodType);


        ResultObj SaveUOM(UOM uOM);
        IQueryable<UOM> UOMs { get; }
    }
}
