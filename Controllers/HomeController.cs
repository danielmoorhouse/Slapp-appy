using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SlappMain.Models;
using SlappMain.Models.ViewModels;

namespace SlappMain.Controllers
{
    public class HomeController : BaseController
    {
        private SlappDatabase1Context db = new SlappDatabase1Context();


        // GET: Applications
        public ActionResult Index()
        {
           
            var topPicks = db.App.Where(a => a.AppId == a.AppId).Take(8);


            return View(topPicks);
        }
    }
    }


    
