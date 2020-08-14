using BLL;
using IBLL;
using MODEL;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductAttrKeyController : Controller
    {
        IProductAttrKeyBLL attrKeyBLL = new ProductAttrKeyBLL();
        IProductAttrValueBLL attrValueBLL = new ProductAttrValueBLL();

        // GET: ProductSpe 
        public ActionResult List(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ProductAttrKeyVModel productAttrKeyVModel)
        {
            ProductAttrKey productAttrKey = new ProductAttrKey()
            {
                AttrName = productAttrKeyVModel.AttrName,
                OrderNum = productAttrKeyVModel.OrderNum,
                EnterType = productAttrKeyVModel.EnterType,
                IsSku = 0,
                ProductCategoryID = productAttrKeyVModel.ProductCategoryID
            };
            attrKeyBLL.Add(productAttrKey);

            if (productAttrKeyVModel.AttrValues != null)
            {
                foreach (var item in productAttrKeyVModel.AttrValues)
                {
                    ProductAttrValue productAttrValue = new ProductAttrValue()
                    {
                        AttrValue = item,
                        ProductAttrKeyID = productAttrKey.ID
                    };
                    attrValueBLL.Add(productAttrValue);
                }
            }

            return Json(new { state = true, msg = "添加成功！" });
        }

        public ActionResult GetByCategoryID(int draw, int categoryId)
        {
            var list = attrKeyBLL.GetByCategoryID(categoryId).Where(x => x.IsSku == 0).ToList();
            //list = list.Where(x => x.IsSku == 0).ToList();
            //var result = new {draw = draw, data = list};
            return Json(new { draw, data = list });
        }

        [HttpGet]
        public ActionResult GetOne(int id)
        {
            var attrKey = attrKeyBLL.GetOne(id);
            var attrValue = attrValueBLL.GetAllByAttrKeyID(id);
            var data = new { attrKey = attrKey, attrValue = attrValue };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(ProductAttrKeyVModel productAttrKeyVModel)
        {
            ProductAttrKey productAttrKey = new ProductAttrKey()
            {
                ID = productAttrKeyVModel.ID,
                AttrName = productAttrKeyVModel.AttrName,
                OrderNum = productAttrKeyVModel.OrderNum,
                EnterType = productAttrKeyVModel.EnterType,
                IsSku = 0,
                ProductCategoryID = productAttrKeyVModel.ProductCategoryID
            };

            List<ProductAttrValue> attrValues = new List<ProductAttrValue>();
            if (productAttrKeyVModel.AttrValues != null)
            {
                foreach (var item in productAttrKeyVModel.AttrValues)
                {
                    ProductAttrValue productAttrValue = new ProductAttrValue()
                    {
                        AttrValue = item,
                        ProductAttrKeyID = productAttrKey.ID
                    };
                    attrValues.Add(productAttrValue);
                }
            }

            var result = attrKeyBLL.Update(productAttrKey, attrValues);
            return Json(new { state = result > 0, msg = result > 0 ? "修改成功！" : "修改失败！" });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = attrKeyBLL.Delete(id);
            return Json(new { state = result > 0 ? true : false, msg = result > 0 ? "删除成功！" : "删除失败！" });
        }

        [HttpGet]
        public ActionResult GetWithAttrValuesByCategoryID(int categoryID)
        {
            var list = attrKeyBLL.Search(x => x.ProductCategoryID == categoryID && x.IsSku == 0);
            List<ProductAttrKeyVModel> vList = new List<ProductAttrKeyVModel>();
            foreach (var item in list)
            {
                var vModel = new ProductAttrKeyVModel();
                vModel.ID = item.ID;
                vModel.AttrName = item.AttrName;
                vModel.EnterType = item.EnterType;
                vModel.AttrValues = new List<string>();
                var attrvalues = attrValueBLL.Search(x => x.ProductAttrKeyID == item.ID);
                foreach (var valueItem in attrvalues)
                {
                    vModel.AttrValues.Add(valueItem.AttrValue);
                }
                vList.Add(vModel);
            }
            return Json(vList, JsonRequestBehavior.AllowGet);
        }
    }
}