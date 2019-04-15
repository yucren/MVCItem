using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCItem.Models
{
    
    public class CustomActionAttribute : FilterAttribute, IActionFilter
    {
        private Stopwatch timer;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            timer = Stopwatch.StartNew();
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            timer.Stop();
            if (filterContext.Exception == null)
            {
                filterContext.HttpContext.Response.Write($"<div>登陆用时：{timer.Elapsed.TotalSeconds:F6}s</div>");
            }
        }
        

    }
    public class ColorAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //dynamic p = filterContext.HttpContext.Profile;
            //if (string.IsNullOrEmpty(p.Color))
            //{
            //    p.Color = "Blue";
            //}
            //p.Save();

            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            dynamic p = filterContext.HttpContext.Profile;
            if (string.IsNullOrEmpty(p.Color))
            {
                p.Color = "Blue";
            }
            p.Save();
        }
    }
}