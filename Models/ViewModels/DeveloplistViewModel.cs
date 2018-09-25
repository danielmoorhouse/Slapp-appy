using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlappMain.Models.ViewModels
{
 
    public class DeveloplistViewModel
    {
        //the develop model represented here
        public Develop DevelopCredit;

        //the linked company
        public Companies Company;


        //the linked app
        public App app;

        public IList<Review> Reviews;


    }
}