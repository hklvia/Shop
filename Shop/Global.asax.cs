using System.Web.Mvc;
using System.Web.Routing;

namespace Shop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //程序第一次启动时会执行该方法
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
