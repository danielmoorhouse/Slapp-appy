using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlappMain.Models
{
    public class AppCatContext : SlappDatabase1Context
    {
        public AppCat appCats;
        public App Apps;
        public Cat Cats;
    }
}