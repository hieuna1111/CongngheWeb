using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Areas.Admin.Models;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoginAction()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public ActionResult LoginAction(_User us)
        {
            dbContext db = new dbContext();
            var userInfo = db.Users.Where(e => e.Username.ToLower() == us.userName.ToLower()
                                          && e.Password == us.password).FirstOrDefault();
            if (userInfo != null)
            {
                Session["Login_Successfull"] = us.userName;
                return RedirectToAction("Index", "Management");
            }
            else
            {
                ViewBag.Login_Unsuccessful = "Thông tin tài khoản mật khẩu không chính xác.";
                return View("~/Views/Home/Index.cshtml");
            }
        }
    }
}