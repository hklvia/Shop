using MODEL;
using System.Collections.Generic;

namespace IDAL
{
    public interface IProductCategoryDAL : IBaseDAL<ProductCategory>
    {
        /// <summary>
        /// 获取某一节点下的所有子节点
        /// </summary>
        /// <typeparam name="id"></typeparam>
        /// <param name=""></param>
        /// <returns></returns>
        List<ProductCategory> GetSub(int id);//id为父节点的id
    }
}
