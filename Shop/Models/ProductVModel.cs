using MODEL;
using System.Collections.Generic;

namespace Shop.Models
{
    public class ProductVModel
    {
        public Product Product { get; set; }
        public List<ProductSku> Skus { get; set; }
        public List<ProductAttr> Attrs { get; set; }
        //public List<ProductSkuImg> ProductSkuImg { get; set; }
    }
}