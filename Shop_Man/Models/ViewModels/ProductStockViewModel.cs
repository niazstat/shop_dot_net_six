using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.ViewModels
{
    public class ProductStockViewModel
    {


        public ProductStockViewModel()
        {

            ProdNames = new List<ProdName>();
            Articles = new List<Article>();
            Sizes = new List<Size>();

        }
 


        public List<ProdName> ProdNames { get; set; }
        public List<Article> Articles { get; set; }
        public List<Size> Sizes { get; set; }
    }
}
