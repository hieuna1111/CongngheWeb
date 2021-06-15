using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.EF;

namespace WebApplication.Models.DAO
{
    public class SachModel
    {
        private dbContext db = null;
        public SachModel()
        {
            db = new dbContext();
        }

        public List<Sach> listNewBook(int top)
        {
            return db.Saches.OrderByDescending(x => x.NgayCapNhat).Take(top).ToList();
        }

        public List<Sach> BestSellingBook(int top)
        {
            return db.Database.SqlQuery<Sach>("select * from Sach where ID in (SELECT TOP("+top+") ProductID FROM OrderDetail GROUP BY ProductID ORDER BY SUM(Quantity) DESC)").ToList();
        }

        public List<Sach> listComicManga(int top)
        {
            return db.Database.SqlQuery<Sach>("select * from Sach where ID in (select TOP(4) ID from Sach where TenSach like '%Doraemon%' and MaCD = 40  group by ID Order by MAX(SoLuongTon) DESC)").ToList();
        }

        public List<Sach> getBookByCategory(int catID, ref int totalRecord, int pageIndex = 1, int pageSize = 1)
        {
            totalRecord = db.Saches.Where(x => x.MaCD == catID && x.Status == true && x.SoLuongTon > 0).ToList().Count();
            return db.Saches.Where(x => x.MaCD == catID && x.Status == true && x.SoLuongTon > 0).OrderBy(x => x.GiaBan).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public Sach getBookDetail(int id)
        {
            return db.Saches.Find(id);
        }

        public List<Sach> RecommendActive(int top, int idChuDe)
        {
            return db.Saches.Where(x => x.MaCD == idChuDe).OrderByDescending(x => x.GiaBan).Take(top).ToList();
        }

        public List<Sach> RecommendItem(int top, int idChuDe)
        {
            return db.Saches.Where(x => x.MaCD == idChuDe).OrderBy(x => x.GiaBan).Take(top).ToList();
        }

    }
}