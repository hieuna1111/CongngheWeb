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

        public List<Sach> listComicManga2(int top)
        {
            return db.Database.SqlQuery<Sach>("select * from Sach where ID in (select TOP(4) ID from Sach where TenSach like '%Conan%' and MaCD = 40  group by ID Order by MAX(SoLuongTon) DESC)").ToList();
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

        public List<Sach> listSearch(string searchString, int giaMin, int giaMax)
        {
            var res = db.Saches.Where(x => x.TenSach.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            if (giaMin > 0 && giaMax >= giaMin)
            {
                res = res.Where(x => x.GiaBan >= giaMin && x.GiaBan <= giaMax).OrderBy(x => x.GiaBan).ToList();
            }
            else if (giaMin >= 0 && giaMax == 0)
            {
                res = res.Where(x => x.GiaBan >= giaMin).OrderBy(x => x.GiaBan).ToList();
            }
            else if (giaMax >= 0 && giaMin == 0)
            {
                res = res.Where(x => x.GiaBan <= giaMax).OrderBy(x => x.GiaBan).ToList();

            }
            return res;
        }

        public List<Sach> getBookBySearch(string searchString, int giaMin, int giaMax, ref int totalRecord, int pageIndex = 1, int pageSize = 1)
        {
            totalRecord = db.Saches.Where(x => x.TenSach.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList().Count();
            var res = db.Saches.Where(x => x.TenSach.ToLower().Trim().Contains(searchString.ToLower().Trim())).OrderBy(x => x.GiaBan).ToList();
            if (giaMin > 0 && giaMax >= giaMin)
            {
                res = res.Where(x => x.GiaBan >= giaMin && x.GiaBan <= giaMax).OrderBy(x => x.GiaBan).ToList();
            }
            else if (giaMin >= 0 && giaMax == 0)
            {
                res = res.Where(x => x.GiaBan >= giaMin).OrderBy(x => x.GiaBan).ToList();
            }
            else if (giaMax >= 0 && giaMin == 0)
            {
                res = res.Where(x => x.GiaBan <= giaMax).OrderBy(x => x.GiaBan).ToList();
            }

            return res.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}