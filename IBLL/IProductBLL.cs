using MODEL;
using System.Collections.Generic;

namespace IBLL
{
    public interface IProductBLL : IBaseBLL<Product>
    {
        int Add(Product product, List<ProductSku> skuList, List<ProductAttr> attrList);
        Product GetOne(int id, out List<ProductSku> skus, out List<ProductAttr> attrs);
        int Update(Product product, List<ProductSku> skuList, List<ProductAttr> attrList);
    }
}