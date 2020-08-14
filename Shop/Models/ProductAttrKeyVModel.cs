using MODEL;
using System.Collections.Generic;

namespace Shop.Models
{
    public class ProductAttrKeyVModel : ProductAttrKey
    {
        public List<string> AttrValues { get; set; }
    }
}