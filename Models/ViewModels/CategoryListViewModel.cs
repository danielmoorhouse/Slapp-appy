using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlappMain.Models.ViewModels
{
    public class CategoryListViewModel
    {
        //the app/category model represented here
        public AppCat AppCatLink;
        //linked category to access data

        public Cat Cat;



        //linked App to access data

        public App App;
              
    }
}