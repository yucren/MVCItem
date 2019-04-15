using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCItem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            var users = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
            return View(users);
        }
    }
}