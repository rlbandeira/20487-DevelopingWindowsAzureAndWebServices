using System.Web;
using System.Web.Mvc;

namespace AzureSocialLoginB2C
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
