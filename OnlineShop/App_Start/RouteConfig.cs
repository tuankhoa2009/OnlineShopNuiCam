using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Danh muc sản phẩm",
                url: "san-pham/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );


            routes.MapRoute(
                name: "Giới thiệu",
                url: "gioi-thieu",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
              name: "Tin tức",
              url: "tin-tuc",
              defaults: new { controller = "Home", action = "Content", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
          );



            routes.MapRoute(
                name: "Danh sách sản phẩm",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Product", action = "ProductAll", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );



            routes.MapRoute(
               name: "Chi tiet sản phẩm",
               url: "chi-tiet/{metatitle}-{id}",
               defaults: new { controller = "Product", action = "DetailProduct", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
           );


            routes.MapRoute(
               name: "Add to cart",
               url: "them-gio-hang",
               defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
           );

            routes.MapRoute(
              name: "Cart",
              url: "gio-hang",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
          );


            routes.MapRoute(
          name: "Payment",
          url: "thanh-toan",
          defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
          namespaces: new[] { "OnlineShop.Controllers" }
             );


            routes.MapRoute(
          name: "PaymentSucess",
          url: "hoan-thanh",
          defaults: new { controller = "Cart", action = "PaymentSuccess", id = UrlParameter.Optional },
          namespaces: new[] { "OnlineShop.Controllers" }
             );



            routes.MapRoute(
          name: "Contact",
          url: "lien-he",
          defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
          namespaces: new[] { "OnlineShop.Controllers" }
             );

            routes.MapRoute(
         name: "Register",
         url: "dang-ky",
         defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
         namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
           name: "Login",
           url: "dang-nhap",
           defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
           namespaces: new[] { "OnlineShop.Controllers" }
               );

            routes.MapRoute(
          name: "Search",
          url: "tim-kiem",
          defaults: new { controller = "Product", action = "Sreach", id = UrlParameter.Optional },
          namespaces: new[] { "OnlineShop.Controllers" }
              );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
        }
    }
}
