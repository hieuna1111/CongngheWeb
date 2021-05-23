using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Areas.Admin.Models;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Controllers
{
    public class ChuDeController : Controller
    {
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 6)
        {
            var chude = new ChuDeModel();
            var model = chude.listAllPaging(page, pageSize, searchString);
            ViewBag.searchString = searchString;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = new ChuDeModel();
            var list = model.listAll();
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ChuDe cd)
        {
            var model = new ChuDeModel();
            try
            {
                var result = model.Create(cd.TenCD);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới chủ đề không thành công.");
                }
                return View(cd);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new ChuDeModel();
            var list = model.listAll();
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        [HttpPost]
        public ActionResult Edit(int id, ChuDe cd)
        {
            var model = new ChuDeModel();
            try
            {
                var result = model.Edit(id, cd.TenCD);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật chủ đề không thành công.");
                }
                return View(cd);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var model = new ChuDeModel();
            var list = model.listAll();
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        [HttpPost]
        public ActionResult Delete(int id, ChuDe cd)
        {
            try
            {
                var model = new ChuDeModel();
                var result = model.Delete(id);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Xóa chủ đề không thành công.");
                }
                return View(cd);
            }
            catch
            {
                return View();
            }
        }
    }
}
