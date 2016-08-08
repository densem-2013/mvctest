using System.Data.Entity;
using UserManagment.Core.DAL;

namespace UserManagment.Web.Models
{
    public class AppContext : DbContext
    {
        public AppContext()
            : base("name=AppContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        static AppContext()
        {
            Database.SetInitializer(new AppDbInitializer());
        }
        public DbSet<User> Users { get; set; } 
    }
}