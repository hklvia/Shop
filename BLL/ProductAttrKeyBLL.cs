using DAL;
using IBLL;
using IDAL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VMODEL;

namespace BLL
{
    public class ProductAttrKeyBLL : BaseBLL<ProductAttrKey, ProductAttrKeyDAL>, IProductAttrKeyBLL
    {
        public List<ProductAttrKey> GetByCategoryID(int categoryId)
        {
            return dal.GetByCategoryID(categoryId);
        }
        IProductAttrValueDAL dalAttrValue = new ProductAttrValueDAL();

        public List<ProductAttrKeyVModel> GetByCategoryID(int categoryID, bool isSku)
        {
            Expression<Func<ProductAttrKey, bool>> expression = x => x.ProductCategoryID == categoryID;
            if (isSku)
            {
                expression = x => x.ProductCategoryID == categoryID && x.IsSku == 1;
            }
            var list = dal.Search(expression);
            List<ProductAttrKeyVModel> vList = new List<ProductAttrKeyVModel>();
            foreach (var item in list)
            {
                var vModel = new ProductAttrKeyVModel();
                vModel.ID = item.ID;
                vModel.AttrName = item.AttrName;
                vModel.EnterType = item.EnterType;
                vModel.IsImg = item.IsImg;
                vModel.AttrValues = new List<string>();
                var attrvalues = dalAttrValue.Search(x => x.ProductAttrKeyID == item.ID);
                foreach (var valueItem in attrvalues)
                {
                    vModel.AttrValues.Add(valueItem.AttrValue);
                }
                vList.Add(vModel);
            }
            return vList;
        }

        public int Update(ProductAttrKey attrKey, List<ProductAttrValue> attrValues)
        {
            dal.Update(attrKey);
            IProductAttrValueDAL attrValueDal = new ProductAttrValueDAL();

            var attrValueList = attrValueDal.GetAllByAttrKeyID(attrKey.ID);
            foreach (var item in attrValueList)
            {
                attrValueDal.Delete(item);
            }

            foreach (var item in attrValues)
            {
                attrValueDal.Add(item);
            }
            return SaveChange();
        }

        public override int Delete(int id)
        {
            //1.对attrKey的删除
            dal.Delete(id);
            //2.对attrValue的删除
            IProductAttrValueDAL attrValueDal = new ProductAttrValueDAL();
            var attrValueList = attrValueDal.GetAllByAttrKeyID(id);
            if (attrValueList.Count > 0)  //下拉选择时选择属性值
            {
                foreach (var item in attrValueList)
                {
                    attrValueDal.Delete(item);
                }
            }
            return SaveChange();
        }

    }
}
