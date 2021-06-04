using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Areas.Admin.Models;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Controllers
{
    public class TacGiaController : Controller
    {
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 6)
        {
            if (Session["Login_Successfull"] == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            var tg = new TacGiaModel();
            var model = tg.listAllPaging(page, pageSize, searchString);
            ViewBag.searchString = searchString;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (Session["Login_Successfull"] == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            var model = new TacGiaModel();
            var list = model.listAll();
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Login_Successfull"] == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(TacGia tg)
        {
            var model = new TacGiaModel();
            try
            {
                var result = model.Create(tg.HoTenTG, tg.DiaChi, tg.DienThoai, tg.TieuSu);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới tác giả không thành công.");
                }
                return View(tg);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Login_Successfull"] == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            var model = new TacGiaModel();
            var list = model.listAll();
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        [HttpPost]
        public ActionResult Edit(int id, TacGia tg)
        {
            var model = new TacGiaModel();
            try
            {
                var result = model.Edit(id, tg.HoTenTG, tg.DiaChi, tg.DienThoai, tg.TieuSu);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật tác giả không thành công.");
                }
                return View(tg);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Login_Successfull"] == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            var model = new TacGiaModel();
            var list = model.listAll();
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        [HttpPost]
        public ActionResult Delete(int id, TacGia tg)
        {
            try
            {
                var model = new TacGiaModel();
                var result = model.Delete(id);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Xóa nhà tác giả không thành công.");
                }
                return View(tg);
            }
            catch
            {
                return View();
            }
        }
    }
}
