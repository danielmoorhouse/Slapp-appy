using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlappMain.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.AverageReview = "avrev";
            base.OnActionExecuting(filterContext);
        }
     
      
    }
}