using IDAL;
using MODEL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DAL
{
    public class ProductCategoryDAL : BaseDAL<ProductCategory>, IProductCategoryDAL
    {
        //获取某一节点下的所有子节点
        public List<ProductCategory> GetSub(int id)
        {
            return entities.ProductCategory.Where(x => x.PID == id).ToList();
        }

        public override void Update(ProductCategory model)
        {
            DbEntityEntry entityEntry = entities.Entry(model);
            entityEntry.State = EntityState.Modified;
            entityEntry.Property("CreateTime").IsModified = false;
        }
    }
}
