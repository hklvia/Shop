using MODEL;
using System.Collections.Generic;

namespace IDAL
{
    public interface IProductAttrKeyDAL : IBaseDAL<ProductAttrKey>
    {
        List<ProductAttrKey> GetByCategoryID(int categoryId);
    }
}