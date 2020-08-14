using MODEL;
using System.Collections.Generic;

namespace VMODEL
{
    public class ProductVModel
    {
        public Product Product { get; set; }
        //public List<ProductAttrKeyVModel> Attrs { get; set; }
        public List<ProductAttr> Attrs { get; set; }
        public List<ProductSku> ProductSkus { get; set; }
    }
}
