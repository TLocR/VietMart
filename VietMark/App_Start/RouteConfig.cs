using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VietMark
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            

            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional, area = "" },
               namespaces: new[] { "VietMark.Controllers" }
            );
            routes.MapRoute(
               name: "About",
               url: "gioi-thieu",
               defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional, area = "" },
               namespaces: new[] { "VietMark.Controllers" }
            );
            routes.MapRoute(
               name: "Blog",
               url: "bai-viet",
               defaults: new { controller = "Home", action = "Blog", id = UrlParameter.Optional, area = "" },
               namespaces: new[] { "VietMark.Controllers" }
            );
            routes.MapRoute(
               name: "List Product",
               url: "san-pham/{metatitle}/{cat_id}",
               defaults: new { controller = "Home", action = "list_product_by_cat_id", id = UrlParameter.Optional, area = "" },
               namespaces: new[] { "VietMark.Controllers" }
            );
            routes.MapRoute(
               name: "Product by id",
               url: "chi-tiet-san-pham/{title}/{maSP}",
               defaults: new { controller = "SanPham", action = "Details", id = UrlParameter.Optional, area = "" },
               namespaces: new[] { "VietMark.Controllers" }
            );
            routes.MapRoute(
               name: "Cart",
               url: "gio-hang",
               defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional, area = "" },
               namespaces: new[] { "VietMark.Controllers" }
            );
            routes.MapRoute(
             name: "Register customer",
             url: "dang-ky/khach-hang",
             defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional, area = "" },
             namespaces: new[] { "VietMark.Controllers" }
            );
            routes.MapRoute(
             name: "Register owner",
             url: "dang-ky/cua-hang",
             defaults: new { controller = "Account", action = "RegisterOwner", id = UrlParameter.Optional, area = "" },
             namespaces: new[] { "VietMark.Controllers" }
            );
            routes.MapRoute(
            name: "Login",
            url: "dang-nhap",
            defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional, area = "" },
            namespaces: new[] { "VietMark.Controllers" }
           );
           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "VietMark.Controllers" }
            );
        }
    }
}
