namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<DemoApplication.Models.DAL.DemoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DemoApplication.Models.DAL.DemoDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.vat.AddOrUpdate(x => x.Id,
       new Models.Vat() { Id = 1, VatPercent = 13 });
            context.Login.AddOrUpdate(x => x.Id, new Models.Login()
            {
                Id = 1,
                Email = "dbugtest2016@gmail.com",
                Password = Crypto.Hash("nepalnepal")
            });
        }
    }
}
