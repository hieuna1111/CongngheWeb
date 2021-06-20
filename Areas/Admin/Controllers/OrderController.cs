using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Areas.Admin.Models;

namespace WebApplication.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 5)
        {
            var result = Session["Login_Successfull"];
            if (result == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            OrderModel order = new OrderModel();
            var list = order.listAll(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }

        public ActionResult Confirm(int id)
        {
            OrderModel order = new OrderModel();
            order.ConfirmOrder(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id, int page = 1, int pageSize = 5)
        {
            var result = Session["Login_Successfull"];
            if (result == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            OrderDetailModel order = new OrderDetailModel();
            var res = order.OrderDetailsCustomer(id, page, pageSize);
            return View(res);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = Session["Login_Successfull"];
            if (result == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            OrderModel order = new OrderModel();
            order.DeleteOrder(id);
            return RedirectToAction("Index");
        }
    }
}
