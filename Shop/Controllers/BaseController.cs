using BLL;
using IBLL;
using MODEL;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Shop.Controllers
{
    public class BaseController<T, B> : Controller
        where T : BaseModel, new()
        where B : IBaseBLL<T>
    {
        public virtual B Bll { get; set; }
        ILoginBLL LoginBLL = new LoginBLL();

        public Admin CurrentUser
        {
            get
            {
                //获取cookie值
                var cookieValue = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                var userID = Convert.ToInt32(FormsAuthentication.Decrypt(cookieValue).UserData);
                //根据用户名从数据库中查询用户信息
                return LoginBLL.GetOne(userID);
            }
        }

        [HttpGet]
        public virtual ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            var result = Bll.Delete(id);
            return Json(new { state = result > 0 ? true : false, msg = result > 0 ? "删除成功！" : "删除失败！" });
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            return View();
        }
    }
}