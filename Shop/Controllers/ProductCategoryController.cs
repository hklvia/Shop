using BLL;
using IBLL;
using MODEL;
using Newtonsoft.Json;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductCategoryController : Controller
    {
        IProductCategoryBLL bll = new ProductCategoryBLL();

        //删除图片文件
        //public void DeleteImage(string imgName)
        //{
        //    var sArray = imgName.Split('/');
        //    //string path = Server.MapPath("/ShopImg/");
        //    //if (System.IO.File.Exists($"{path + imgName}"))
        //    string path = Server.MapPath("/Upload/Product/CategoryImg/");
        //    var a = Server.MapPath("/Upload/Product/CategoryImg/") + imgName.Split('/').Last().ToString();
        //    if (System.IO.File.Exists(path + sArray.Last().ToString()))
        //    {
        //        try
        //        {
        //            System.IO.File.Delete(imgName);
        //        }
        //        catch (IOException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //    }
        //}

        [HttpGet]
        public ActionResult Add()
        {
            List<ProductCategory> categories = bll.GetAll();
            ProductCategoryUpdateVModel vModel = new ProductCategoryUpdateVModel() { Categories = categories };
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Add(ProductCategory category)
        {
            if (!ModelState.IsValid)// 对模型的验证是否通过
            {
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        return Json(new { state = false, msg = error.ErrorMessage });
                    }
                }
            }

            var result = bll.Add(category);
            return Json(new { state = result > 0 ? true : false, msg = result > 0 ? "添加成功！" : "添加失败！" });
        }

        public ActionResult List()
        {
            return View();
        }

        //分类下拉列表数据
        public ActionResult GetAll1()
        {
            //递归生成json数据
            //1、获取所有一级节点（分类）
            var rootList = bll.GetSub(0);//(男装、女装)
            List<dynamic> list = new List<dynamic>();

            dynamic firstNode = new ExpandoObject();//ExpandoObject：动态类型，可以动态为其添加属性
            firstNode.text = "无上级分类";    //text：属性名称
            firstNode.tags = new List<int>();     //tags：额外的信息到每个节点
            firstNode.tags.Add(0);      //存放了分类ID
            list.Add(firstNode);

            foreach (var category in rootList)
            {
                dynamic treeObj = new ExpandoObject();
                treeObj.text = category.Name;
                //treeObj.nodeId = category.ID;
                treeObj.tags = new List<int>();
                treeObj.tags.Add(category.ID);

                GetSub1(treeObj);
                list.Add(treeObj);
            }
            //构造返回json对象 {"draw":,"data":}
            return Json(JsonConvert.SerializeObject(list));
        }

        //递归获取父节点的所有子节点（层层钻取获取子节点）
        private void GetSub1(dynamic treeObj)//( 1 男装 2 男士上衣 3 男士T恤) 
        {
            int pid = (int)treeObj.tags[0];
            var subList = bll.Search(x => x.PID == pid);
            subList = subList.Where(x => x.Path.Split(',').Length != 3).ToList();
            //判断子节点下是否还包含子节点
            if (subList.Count == 0)//递归终止条件
            {
                return;
            }
            treeObj.nodes = new List<dynamic>();//初始化子节点集合
            foreach (var category in subList)
            {
                dynamic treeObj1 = new ExpandoObject();
                treeObj.nodeId = category.ID;
                treeObj1.text = category.Name;
                treeObj1.tags = new List<int>();
                treeObj1.tags.Add(category.ID);
                GetSub1(treeObj1);//开始进行递归
                treeObj.nodes.Add(treeObj1);
            }
        }

        [HttpPost]
        public ActionResult GetAll(int draw)
        {
            //递归生成json数据
            //1.获取所有一级节点
            var rootList = bll.GetSub(0);
            List<ProductCategoryVModel> list = new List<ProductCategoryVModel>();
            foreach (var category in rootList)
            {
                ProductCategoryVModel vModel = new ProductCategoryVModel();
                vModel.ID = category.ID;
                vModel.Name = category.Name;
                vModel.OrderNum = category.OrderNum;
                vModel.Img = category.Img;
                vModel.PID = category.PID;
                vModel.Path = category.Path;
                vModel.KeyWords = category.KeyWords;
                //初始化子节点集合
                vModel.children = new List<ProductCategoryVModel>();
                GetSub(vModel);
                list.Add(vModel);
            }

            var result = new { draw = draw, data = list };
            return Json(result);
        }
        //获取父节点下的所有子节点
        private void GetSub(ProductCategoryVModel parentNode)
        {
            var subList = bll.GetSub(parentNode.ID);
            //判断子节点下是否还包含子节点
            if (subList.Count() == 0) //相当于递归终止条件
            {
                return;
            }
            foreach (var category in subList)
            {
                ProductCategoryVModel vModel = new ProductCategoryVModel();
                vModel.ID = category.ID;
                vModel.Name = category.Name;
                vModel.OrderNum = category.OrderNum;
                vModel.Img = category.Img;
                vModel.PID = category.PID;
                vModel.Path = category.Path;
                vModel.KeyWords = category.KeyWords;
                //初始化子节点集合
                vModel.children = new List<ProductCategoryVModel>();
                GetSub(vModel); //开始进行递归
                parentNode.children.Add(vModel);
            }
        }

        [HttpPost]
        public ActionResult Delete(ProductCategory productCategory)
        {
            var result = bll.Delete(productCategory.ID);
            //DeleteImage(productCategory.Img);
            return Json(new { state = result > 0 ? true : false, msg = result > 0 ? "删除成功！" : "删除失败！" });
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var category = bll.GetOne(id);
            List<ProductCategory> categories = bll.GetAll();
            ProductCategoryUpdateVModel vModel = new ProductCategoryUpdateVModel() { Category = category, Categories = categories };
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Update(ProductCategory category)
        {
            if (!ModelState.IsValid)// 对模型的验证是否通过
            {
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        return Json(new { state = false, msg = error.ErrorMessage });
                    }
                }
            }


            category.UpdateTime = DateTime.Now;
            var result = bll.Update(category);
            //DeleteImage(productCategory.Img);
            return Json(new { state = result > 0 ? true : false, msg = result > 0 ? "修改成功！" : "修改失败！" });
        }

        [HttpGet]
        public ActionResult GetByPID(int pid)
        {
            var list = bll.GetSub(pid);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}