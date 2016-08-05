using System.Web.Mvc;

namespace UserManagment.Web.Areas.UserControlArea
{
    public class UserControlAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "UserControlArea"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "UserControl_default",
                "UserControlArea/{controller}/{action}/{id}",
                new {controller = "User", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}