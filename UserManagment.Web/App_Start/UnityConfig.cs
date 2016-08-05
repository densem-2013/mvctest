using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using UserManagment.Infrastructure.Repositories;
using UserManagment.Web.Models;

namespace UserManagment.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<DbContext, AppContext>();
            container.RegisterType<IUserRepository, UserRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}