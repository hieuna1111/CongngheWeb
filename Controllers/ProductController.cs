using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models.DAO;

namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(int id = 1, int page = 1, int pageSize = 4)
        {
            var res = new ChuDeModel().CategoryDetail(id);
            int totalRecord = 0;
            ViewBag.BookByCategory = new SachModel().getBookByCategory(id, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;
            //do cái total page này lấy số nguyên nên nó vậy
            float sl1 = totalRecord % pageSize;
            if (sl1 > 0)
            {
                totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize))+1;
            }  
            else
            {
                totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            }    
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(res);
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory() // 
        {
            ChuDeModel model = new ChuDeModel();
            var res = model.listAll();
            return PartialView(res);
        }

        public ActionResult Detail(int id)
        {
            var res = new SachModel().getBookDetail(id);
            ViewBag.RecommendItemActive = new SachModel().RecommendActive(3, res.MaCD.GetValueOrDefault());
            ViewBag.RecommendItem = new SachModel().RecommendItem(3, res.MaCD.GetValueOrDefault());
            ViewBag.NumbericSold = new OrderDetailModel().NumbericSold(id);
            return View(res);
        }

        [HttpGet]
        public ActionResult VanHoc()
        {
            return View();
        }
    }
}