using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Areas.Admin.Models;
using WebApplication.Models._EF;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Controllers
{
    public class TinTucController : Controller
    {
        // GET: Admin/TinTuc
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 6)
        {
            var tintuc = new TinTucModel();
            var model = tintuc.listAllPaging(page, pageSize, searchString);
            ViewBag.searchString = searchString;
            return View(model);
        }

        // GET: Admin/TinTuc/Details/5
        public ActionResult Details(int id)
        {
            var sach = new TinTucModel();
            var model = sach.ListAll(id);
            var res = model.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        // GET: Admin/TinTuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TinTuc/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TinTuc tintuc, HttpPostedFileBase uploadhinh)
        {
            var model = new TinTucModel();
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext db = new dbContext();
                    if (uploadhinh != null && uploadhinh.ContentLength > 0)
                    {
                        string MetaTitle = new MetaLink().nameToMeta(tintuc.Name);
                        int id = int.Parse(db.TinTucs.ToList().Last().ID.ToString());
                        id += 1;
                        string _FileName = "";
                        int index = uploadhinh.FileName.IndexOf('.');
                        _FileName = MetaTitle + "1" + "." + uploadhinh.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Upload/tintuc"), _FileName);
                        uploadhinh.SaveAs(_path);
                        tintuc.Image = "~/Upload/tintuc/" + _FileName;
                        db.SaveChanges();
                    }
                    int result = model.Create(tintuc.Name, tintuc.Image, tintuc.Detail);
                    if (result > 0)
                        return RedirectToAction("Index");
                    else
                        ModelState.AddModelError("", "Thêm mới tin tức không thành công.");
                }
                return View(tintuc);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/TinTuc/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new TinTucModel();
            var list = model.ListAll(id);
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        // POST: Admin/TinTuc/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, TinTuc tintuc, HttpPostedFileBase uploadhinh)
        {
            var model = new TinTucModel();

            dbContext db = new dbContext();
            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {
                string MetaTitle = new MetaLink().nameToMeta(tintuc.Name);
                string _FileName = "";
                int index = uploadhinh.FileName.IndexOf('.');
                _FileName = MetaTitle + "1" + "." + uploadhinh.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/Upload/tintuc"), _FileName);
                uploadhinh.SaveAs(_path);
                tintuc.Image= "~/Upload/tintuc/" + _FileName;
                db.SaveChanges();
            }
            else { tintuc.Image = model.GetPathImageByID(tintuc.ID); }

            if (ModelState.IsValid)
            {
                int result = model.Edit(id, tintuc.Name, tintuc.Image, tintuc.Detail);
                if (result > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Cập nhật tin tức không thành công.");
            }
            return View(tintuc);
        }

        // GET: Admin/TinTuc/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new TinTucModel();
            var list = model.ListAll(id);
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        // POST: Admin/TinTuc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var model = new TinTucModel();
                int result = model.Delete(id);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Xóa tin tức không thành công.");
                    return View("Index");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
