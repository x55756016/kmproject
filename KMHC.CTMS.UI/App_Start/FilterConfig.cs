using System.Web;
using System.Web.Mvc;
using KMHC.CTMS.UI.Attribute;

namespace KMHC.CTMS.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionAttribute());
            //filters.Add(new AuthorizedAttribute());
        }
    }
}