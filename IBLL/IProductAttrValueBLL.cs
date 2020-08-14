using MODEL;
using System.Collections.Generic;

namespace IBLL
{
    public interface IProductAttrValueBLL : IBaseBLL<ProductAttrValue>
    {
        List<ProductAttrValue> GetAllByAttrKeyID(int attrKeyID);
    }
}
