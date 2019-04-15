using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCItem.Areas.Admin.Controllers
{
    [RouteArea("admin",AreaPrefix ="yucren")]
    [Route("users/{action}")]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}