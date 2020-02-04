using System.Web.Mvc;

namespace OnlineShop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );


            context.MapRoute(
            name: "Logout Admin",
            url: "logoutAdmin",
            defaults: new { area = "Admin", controller = "User", action = "LogOutAdmin", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop.Areas.Admin.Controllers" }
            );



        }
    }
}