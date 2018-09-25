using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlappMain.Models.ViewModels
{
    public class AppReviewViewModel
    {
        public Review Review { get; set; }

        public App App { get; set;  }

        public Companies Companies { get; set; }
    }
}