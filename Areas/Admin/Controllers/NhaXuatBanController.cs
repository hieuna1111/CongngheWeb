using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Areas.Admin.Models;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Controllers
{
    public class NhaXuatBanController : Controller
    {
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 6)
        {
            var nxb = new NhaXuatBanModel();
            var model = nxb.listAllPaging(page, pageSize, searchString);
            ViewBag.searchString = searchString;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = new NhaXuatBanModel();
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
        public ActionResult Create(NhaXuatBan nxb)
        {
            var model = new NhaXuatBanModel();
            try
            {
                var result = model.Create(nxb.TenNXB, nxb.DiaChi, nxb.DienThoai);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới nhà xuất bản không thành công.");
                }
                return View(nxb);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new NhaXuatBanModel();
            var list = model.listAll();
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        [HttpPost]
        public ActionResult Edit(int id, NhaXuatBan nxb)
        {
            var model = new NhaXuatBanModel();
            try
            {
                var result = model.Edit(id, nxb.TenNXB, nxb.DiaChi, nxb.DienThoai);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật nhà xuất bản không thành công.");
                }
                return View(nxb);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = new NhaXuatBanModel();
            var list = model.listAll();
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        [HttpPost]
        public ActionResult Delete(int id, NhaXuatBan nxb)
        {
            try
            {
                var model = new NhaXuatBanModel();
                var result = model.Delete(id);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Xóa nhà xuất bản không thành công.");
                }
                return View(nxb);
            }
            catch
            {
                return View();
            }
        }
    }
}
