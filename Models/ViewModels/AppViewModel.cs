using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlappMain.Models.ViewModels
{
    public class AppViewModel
    {
        //the app record
        public App App;
        //related review records 
        public IList<Review> Reviews;
        //related company records linked via develop
        public IList<Companies> Companies;
        //related platform records linked via appplat
        public IList<Platform> Platforms;

        public IList<Pegi> Pegi;
       
    }
}