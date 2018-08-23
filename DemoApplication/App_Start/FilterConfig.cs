using DemoApplication.Models.DAL;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApplication
{
    public class FilterConfig
    {
        DemoDbContext _db = new DemoDbContext();
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
       
    }
}
