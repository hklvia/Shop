using DAL;
using IBLL;
using MODEL;
using System.Collections.Generic;

namespace BLL
{
    public class ProductAttrValueBLL : BaseBLL<ProductAttrValue, ProductAttrValueDAL>, IProductAttrValueBLL
    {
        public List<ProductAttrValue> GetAllByAttrKeyID(int attrKeyID)
        {
            return dal.GetAllByAttrKeyID(attrKeyID);
        }
    }
}
