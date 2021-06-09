using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // liên hệ
            routes.MapRoute(
                name: "Contact",
                url: "lien-he/{action}/{id}",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                new[] { "WebApplication.Controllers" }
            );

            // sản phẩm
            routes.MapRoute(
                name: "Product",
                url: "san-pham/{action}/{id}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                new[] { "WebApplication.Controllers" }
            );

            // tin tức
            routes.MapRoute(
                name: "News",
                url: "tin-tuc/{action}/{id}",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
                new[] { "WebApplication.Controllers" }
            );

            // giới thiệu
            routes.MapRoute(
                name: "About",
                url: "gioi-thieu/{action}/{id}",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                new[] { "WebApplication.Controllers" }
            );

            // chi tiết một sản phẩm nào đó
            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                new[] { "WebApplication.Controllers" }
            );

            //xem tat ca san pham cua danh muc
            routes.MapRoute(
                name: "Product Category",
                url: "chu-de/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "WebApplication.Controllers" }
            );

            // xem giỏ hàng
            routes.MapRoute(
                name: "View Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                new[] { "WebApplication.Controllers" }
            );

            // thêm giỏ hàng
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                new[] { "WebApplication.Controllers" }
            );

            // thanh toán
            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                new[] { "WebApplication.Controllers" }
            );

            // thanh toán thành công
            routes.MapRoute(
                name: "Success Payment",
                url: "thanh-toan-thanh-cong",
                defaults: new { controller = "Cart", action = "SuccessPayment", id = UrlParameter.Optional },
                new[] { "WebApplication.Controllers" }
            );

            // thanh toán không thành công
            routes.MapRoute(
                name: "Unsuccess Payment",
                url: "loi-thanh-toan",
                defaults: new { controller = "Cart", action = "UnSuccessPayment", id = UrlParameter.Optional },
                new[] { "WebApplication.Controllers" }
            );

            //default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebApplication.Controllers" }
            );
        }
    }
}
