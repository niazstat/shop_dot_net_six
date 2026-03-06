using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class ProductImage
    {

        public int ProductImageID { get; set; }

        [Column(TypeName = "image")]
        public byte[] ProductImageData { get; set; }
    }
}
