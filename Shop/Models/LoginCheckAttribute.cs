using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Shop.Models
{
    public class LoginCheckAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 判断验证是否通过
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Request.Cookies[FormsAuthentication.FormsCookieName] != null && httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value != null && httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value != "";
        }

        /// <summary>
        /// 验证未通过
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Login/Index");
        }
    }
}