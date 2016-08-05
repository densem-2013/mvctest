using System;
using System.Data.Entity.Migrations;
using UserManagment.Web.Models;

namespace UserManagment.Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppContext>
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin", "") + @"App_Data\Employeers.xml";
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AppContext context)
        {
            AppDbInitializer.GetUsersFromXml(context,_path);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
