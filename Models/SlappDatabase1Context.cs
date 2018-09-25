using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

public class SlappDatabase1Context : DbContext
{
    // You can add custom code to this file. Changes will not be overwritten.
    // 
    // If you want Entity Framework to drop and regenerate your database
    // automatically whenever you change your model schema, please use data migrations.
    // For more information refer to the documentation:
    // http://msdn.microsoft.com/en-us/data/jj591621.aspx

    public SlappDatabase1Context() : base("name=SlappDatabase1Context")
    { }

   
    public DbSet<SlappMain.Models.App> App { get; set; }

    public DbSet<SlappMain.Models.Companies> Companies { get; set; }

    public DbSet<SlappMain.Models.Develop> Dev { get; set; }

    public DbSet<SlappMain.Models.Cat> Cat { get; set; }

    public DbSet<SlappMain.Models.AppCat> AppCats { get; set; }

    public DbSet<SlappMain.Models.Pegi> Pegis { get; set; }

    public DbSet<SlappMain.Models.AppPegi> AppPegis { get; set; }

    public DbSet<SlappMain.Models.Platform> Platforms { get; set; }
    public DbSet<SlappMain.Models.AppPlats> AppPlats { get; set; }

    public DbSet<SlappMain.Models.Review> Review { get; set; }

   
}


