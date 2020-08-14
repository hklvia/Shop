using IDAL;
using MODEL;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ProductAttrValueDAL : BaseDAL<ProductAttrValue>, IProductAttrValueDAL
    {
        public List<ProductAttrValue> GetAllByAttrKeyID(int attrKeyID)
        {
            return entities.ProductAttrValue.Where(x => x.ProductAttrKeyID == attrKeyID).ToList();
        }

    }
}
