using DAL;
using IBLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ProductCategoryBLL : BaseBLL<ProductCategory, ProductCategoryDAL>, IProductCategoryBLL
    {
        public override int Add(ProductCategory model)
        {
            var tran = this.BeginTran();
            int result = 0;
            string path = "";
            try
            {
                model.CreateTime = DateTime.Now;
                dal.Add(model);
                result += SaveChange();
                if (model.PID == 0)
                {
                    path = model.ID.ToString();
                }
                else
                {
                    var parent = this.Search(x => x.ID == model.PID).First();
                    path = parent.Path + "," + model.ID;
                }
                model.Path = path;
                this.Update(model);
                result += this.SaveChange();
                tran.Commit();
            }
            catch (Exception)
            {
                result = 0;
                tran.Rollback();
            }
            return result;
        }

        public List<ProductCategory> GetSub(int id)
        {
            return dal.GetSub(id);
        }
        public override int Update(ProductCategory model)
        {
            string path = "";
            if (model.PID == 0)
            {
                path = model.ID.ToString();
            }
            else
            {
                var parent = this.Search(x => x.ID == model.PID).First();
                path = parent.Path + "," + model.ID;
            }
            model.Path = path;

            dal.Update(model);
            return SaveChange();
        }

        public override int Delete(int id)
        {
            dal.Delete(id);
            var subCategory = dal.Search(x => x.PID == id);
            if (subCategory.Count > 0)  //下拉选择时选择属性值
            {
                foreach (var item in subCategory)
                {
                    dal.Delete(item);
                }
            }
            return SaveChange();
        }
    }
}
