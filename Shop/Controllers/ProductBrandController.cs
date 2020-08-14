using BLL;
using IBLL;
using MODEL;
using System;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductBrandController : BaseController<ProductBrand, IProductBrandBLL>
    {
        public override IProductBrandBLL Bll
        {
            get
            {
                return new ProductBrandBLL();
            }
        }
        //IProductBLL bll = new ProductBLL();
        [HttpPost]
        public ActionResult Add(ProductBrand productBrand)
        {
            productBrand.CreateTime = DateTime.Now;
            var result = Bll.Add(productBrand);
            return Json(new { state = result > 0, msg = result > 0 ? "提交成功！" : "提交失败！" });
        }

        [HttpPost]
        public ActionResult GetAll(int draw, int pageSize, int pageIndex)
        {
            int count;
            var list = Bll.Search(pageSize, pageIndex, false, x => x.ID, x => true, out count);
            //var count = Bll.GetCount(x => true);
            var result = new
            {
                draw = draw,
                data = list,
                recordsTotal = count,
                recordsFiltered = count
            };
            return Json(result);
        }

        public ActionResult GetAllByProductAdd()
        {
            //var result = Bll.GetAll();
            return Json(Bll.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(ProductBrand productBrand)
        {
            productBrand.UpdateTime = DateTime.Now;
            var result = Bll.Update(productBrand);
            return Json(new { state = result > 0, msg = result > 0 ? "修改成功！" : "修改失败！" });
        }

        [HttpGet]
        public ActionResult GetOne(int id)
        {
            var productBrand = Bll.GetOne(id);
            var result = new
            {
                productBrand = productBrand
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}