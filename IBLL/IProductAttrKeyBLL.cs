using MODEL;
using System.Collections.Generic;
using VMODEL;

namespace IBLL
{
    public interface IProductAttrKeyBLL : IBaseBLL<ProductAttrKey>
    {
        List<ProductAttrKey> GetByCategoryID(int categoryId);
        List<ProductAttrKeyVModel> GetByCategoryID(int categoryId, bool isSku);
        int Update(ProductAttrKey attrKey, List<ProductAttrValue> attrValues);
    }
}
