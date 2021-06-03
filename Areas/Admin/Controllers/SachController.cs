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
    public class SachController : Controller
    {
        public ActionResult Index(string searchField = "", string searchString = "", int page = 1, int pageSize = 6, int priceMin = 0, int priceMax = 0)
        {
            var result = Session["Login_Successfull"];
            if (result == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }    

            List<SearchField> list = new List<SearchField>()
            {
                new SearchField() {Name = "TenSach", Value = "Tên sách"},
                new SearchField() {Name = "TenCD", Value = "Tên chủ đề"},
                new SearchField() {Name = "TenNXB", Value = "Tên nhà xuất bản"},
                new SearchField() {Name = "GiaTang", Value = "Giá tăng dần"},
                new SearchField() {Name = "GiaGiam", Value = "Giá giảm dần"}
            };
            ViewBag.searchField = new SelectList(list, "Name", "Value");
        
            var sach = new SachModel();

            var model = sach.listAllPaging(page, pageSize, searchString, searchField, priceMin, priceMax);

            ViewBag.searchString = searchString;
            ViewBag._searchField = searchField;

            if (priceMin == 0 && priceMax == 0)
            {
                ViewBag.priceMin = null;
                ViewBag.priceMax = null;
            }
            else
            {
                ViewBag.priceMin = priceMin;
                ViewBag.priceMax = priceMax;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var result = Session["Login_Successfull"];
            if (result == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            var sach = new SachModel();
            var model = sach.ListAll(id);
            var res = model.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }


        public void SetViewBag(int? selectedID = null)
        {
            var model = new SachModel();
            ViewBag.HoTenTG = new SelectList(model.GetList_HoTenTG_IntoDropDownList(), "HoTenTG", "HoTenTG", selectedID);
            ViewBag.TenCD = new SelectList(model.GetList_TenCD_IntoDropDownList(), "TenCD", "TenCD", selectedID);
            ViewBag.TenNXB = new SelectList(model.GetList_TenNXB_IntoDropDownList(), "TenNXB", "TenNXB", selectedID); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Login_Successfull"] == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            SetViewBag();
            return View();
        }
         
        [HttpPost]
        public ActionResult Create(_Sach collection, HttpPostedFileBase uploadhinh, HttpPostedFileBase uploadhinh2)
        {
            var model = new SachModel();
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext db = new dbContext();
                    if (uploadhinh != null && uploadhinh.ContentLength > 0)
                    {
                        int id = int.Parse(db.Saches.ToList().Last().ID.ToString());
                        id += 1;
                        string _FileName = "";
                        int index = uploadhinh.FileName.IndexOf('.');
                        _FileName = "sach" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Upload/sach"), _FileName);
                        uploadhinh.SaveAs(_path);
                        collection.AnhBia = _FileName;
                        db.SaveChanges();
                    }
                    if (uploadhinh2 != null && uploadhinh2.ContentLength > 0)
                    {
                        int id = int.Parse(db.Saches.ToList().Last().ID.ToString());
                        id += 1;
                        string _FileName = "";
                        int index = uploadhinh2.FileName.IndexOf('.');
                        _FileName = "sach" + (id+1).ToString() + "." + uploadhinh2.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Upload/sach"), _FileName);
                        uploadhinh2.SaveAs(_path);
                        collection.BiaSau = _FileName;
                        db.SaveChanges();
                    }
                    int result = model.Create(collection.TenSach, collection.GiaBan, collection.AnhBia, collection.BiaSau, collection.SoLuongTon, collection.HoTenTG,collection.TenCD, collection.TenNXB, collection.MoTa, collection.Detail);

                    if (result > 0)
                        return RedirectToAction("Index");
                    else
                        ModelState.AddModelError("", "Thêm mới sách không thành công.");
                }
                SetViewBag();
                return View(collection);
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
            var model = new SachModel();
            var list = model.ListAll(id);
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            SetViewBag(id);
            return View(res);
        }

        [HttpPost]
        public ActionResult Edit(int id, _Sach collection, HttpPostedFileBase uploadhinh)
        {
            try
            {
                var model = new SachModel();
                if (ModelState.IsValid)
                {
                    dbContext db = new dbContext();
                    if (uploadhinh != null && uploadhinh.ContentLength > 0)
                    {
                        int IDSach = id;
                        string _FileName = "";
                        int index = uploadhinh.FileName.IndexOf('.');
                        _FileName = "sach" + IDSach.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Upload/sach"), _FileName);
                        uploadhinh.SaveAs(_path);
                        collection.AnhBia = _FileName;
                        db.SaveChanges();
                    }
                    else { collection.AnhBia = model.GetPathImageByID(collection.ID); }

                    int result = model.Edit(id, collection.TenSach, collection.GiaBan, collection.AnhBia, collection.SoLuongTon, collection.TenCD, collection.TenNXB);

                    if (result > 0)
                        return RedirectToAction("Index");
                    else
                        ModelState.AddModelError("", "Cập nhật sách không thành công.");
                }
                SetViewBag(id);
                return View(collection);
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
            var model = new SachModel();
            var list = model.ListAll(id);
            var res = list.Where(x => x.ID == id).FirstOrDefault();
            return View(res);
        }

        [HttpPost]
        public ActionResult Delete(int id, _Sach collection)
        {
            try
            {
                var model = new SachModel();
                int result = model.Delete(id);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Xóa sách không thành công.");
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
