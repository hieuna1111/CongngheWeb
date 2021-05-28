using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Areas.Admin.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Admin/Management
        public ActionResult Index()
        {
            var result = Session["Login_Successfull"];
            if (result == null)
            {
                Session["TextNull"] = "Đăng nhập trước khi vào trang chủ.";
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                return View();
            }
        }


    }
}