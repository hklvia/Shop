//                   _oo0oo_
//                  o8888888o
//                  88" . "88
//                  (| -_- |)
//                  0\  =  /0
//                ___/`---'\___
//              .' \\|     |// '.
//             / \\|||  :  |||// \
//            / _||||| -:- |||||- \
//           |   | \\\  - /// |   |
//           | \_|  ''\---/''  |_/ |
//           \  .-\__  '-'  ___/-. /
//         ___'. .'  /--.--\  `. .'___
//      ."" '<  `.___\_<|>_/___.' >' "".
//     | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//     \  \ `_.   \_ __\ /__ _/   .-` /  /
// =====`-.____`.___ \_____/___.-`___.-'=====
//                   `=---='

// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

//       佛祖保佑       永不宕机     永无BUG
using BLL;
using IBLL;
using MODEL;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductController : BaseController<Product, IProductBLL>
    {
        //public override IProductBLL Bll { get { return new ProductBLL(); } }

        public override IProductBLL Bll
        {
            get
            {
                return new ProductBLL();
            }
        }

        [HttpPost]
        public ActionResult Add(ProductVModel vModel)
        {
            //Product product = vModel.Product;
            //List<ProductSku> skuList = vModel.Skus;
            //List<ProductAttr> attrList = vModel.Attrs;
            vModel.Product.CreateTime = DateTime.Now;
            var result = Bll.Add(vModel.Product, vModel.Skus, vModel.Attrs);
            return Json(new { state = result > 0, msg = result > 0 ? "提交成功！" : "提交失败！" });
        }

        [HttpPost]
        public ActionResult GetAll(int draw, int pageSize, int pageIndex)
        {
            //var name = this.CurrentUser.Name;

            int count;
            var list = Bll.Search(pageSize, pageIndex, false, x => x.ID, x => true, out count);
            var result = new
            {
                draw,
                data = list,
                recordsTotal = count,
                recordsFiltered = count
            };
            return Json(result);
        }

        [HttpPost]
        public ActionResult Update(ProductVModel vModel)
        {
            vModel.Product.UpdateTime = DateTime.Now;
            var result = Bll.Update(vModel.Product, vModel.Skus, vModel.Attrs);
            return Json(new { state = result > 0, msg = result > 0 ? "修改成功！" : "修改失败！" });
        }

        [HttpGet]
        public ActionResult GetOne(int id)
        {
            List<ProductSku> skus;
            List<ProductAttr> attrs;
            var product = Bll.GetOne(id, out skus, out attrs);
            var result = new
            {
                product,
                attrs,
                skus
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}