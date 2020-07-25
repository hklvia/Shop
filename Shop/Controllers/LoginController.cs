﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COMMON;
using System.Web.Mvc;
using IBLL;
using BLL;
using System.Net;
using System.Web.Security;

namespace Shop.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        ILoginBLL BLL = new LoginBLL();
        // GET: Login
        public ActionResult Index()
        {
            //读cookies
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null&&Request.Cookies[FormsAuthentication.FormsCookieName].Value != null && Request.Cookies[FormsAuthentication.FormsCookieName].Value != "")
            {
                //string userID = Server.UrlDecode(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                //var result = BLL.Search(x => x.Name == username);
                var cookieValue = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                var userID = Convert.ToInt32(FormsAuthentication.Decrypt(cookieValue).UserData);
                //根据用户名从数据库中查询用户信息
                var result = BLL.GetOne(userID);

                if (result != null && result.ID == userID)
                {
                    //Session["user"] = result[0];
                    //实现滑动过期时间
                    Response.Cookies[FormsAuthentication.FormsCookieName].Value = cookieValue;
                    Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(1);
                    return Redirect("/Product/List");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string username, string password)
        {
            string salt = ")+_*&^@!&*(";
            password = Md5Helper.Md5(Md5Helper.Md5(salt + password));
            //从数据库查询用户信息，写入到session中
            var result = BLL.Search(x => x.Name == username && x.Password == password);
            if (result.Count == 1)
            {
                FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(
                    1,
                    "user",
                    DateTime.Now,
                    DateTime.Now.AddDays(1),
                    false,
                    result[0].ID.ToString()
                    );
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(Ticket));
                Response.Cookies.Add(cookie);
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(1);
                return Json(new { state = true });
            }
            return Json(new { state = false, msg = "您的用户名或密码输入错误！" });
        }

        public ActionResult SignOut()
        {
            Response.Cookies[FormsAuthentication.FormsCookieName].Value = "";
            return Redirect("/Login/Index");
        }
    }
}