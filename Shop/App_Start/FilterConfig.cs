using Shop.Models;
using System.Web.Mvc;

namespace Shop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoginCheckAttribute());
        }
    }
}
