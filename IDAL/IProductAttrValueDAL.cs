using MODEL;
using System.Collections.Generic;

namespace IDAL
{
    public interface IProductAttrValueDAL : IBaseDAL<ProductAttrValue>
    {
        List<ProductAttrValue> GetAllByAttrKeyID(int attrKeyID);
    }
}
