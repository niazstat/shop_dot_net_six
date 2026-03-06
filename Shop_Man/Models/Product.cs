using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class Product
    {
        public long ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        public string Category
        {
            get; set;
        }


        public Color Color { get; set; }
        public int ColorID { get; set; }


        public Size Size { get; set; }
        public int SizeID { get; set; }


        public ProductImage ProductImage { get; set; }
        public int ProductImageID { get; set; }


        public int CompanyID { get; set; }
        public Company Company { get; set; }


        public decimal OpeningStock { get; set; }

        public decimal CurrentStock { get; set; }
    }
}
