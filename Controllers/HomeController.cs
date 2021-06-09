using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models.DAO;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideModel().listAll();
            ViewBag.ListNewBook = new SachModel().listNewBook(8);
            ViewBag.BestSellingBook = new SachModel().BestSellingBook(8);
            ViewBag.ListRecommend = new SachModel().listRecommend(3);
            return View();
        }

        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            MenuModel model = new MenuModel();
            var res = model.listByGroupId(2);
            return PartialView(res);
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            MenuModel model = new MenuModel();
            var res = model.listByGroupId(1);
            return PartialView(res);
        }
    }
}