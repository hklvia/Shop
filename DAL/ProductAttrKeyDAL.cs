using IDAL;
using MODEL;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ProductAttrKeyDAL : BaseDAL<ProductAttrKey>, IProductAttrKeyDAL
    {
        public List<ProductAttrKey> GetByCategoryID(int categoryId)
        {
            return entities.ProductAttrKey.Where(x => x.ProductCategoryID == categoryId).ToList();
        }
    }
}
