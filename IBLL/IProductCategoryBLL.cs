using MODEL;
using System.Collections.Generic;

namespace IBLL
{
    public interface IProductCategoryBLL : IBaseBLL<ProductCategory>
    {
        /// <summary>
        /// 获取某一节点下的所有子节点
        /// </summary>
        /// <typeparam name="ProductCategory"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ProductCategory> GetSub(int id);//id为父节点的id
    }
}
