using MODEL;
using System.Collections.Generic;

namespace Shop.Models
{
    public class ProductCategoryUpdateVModel
    {
        public ProductCategory Category { get; set; }
        public List<ProductCategory> Categories { get; set; }
    }
}