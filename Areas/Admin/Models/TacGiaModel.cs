using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication.Models._EF;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Models
{
    public class TacGiaModel
    {
        private dbContext db = null;
        public TacGiaModel()
        {
            db = new dbContext();
        }

        public List<TacGia> listAll()
        {
            return db.Database.SqlQuery<TacGia>("SELECT * FROM dbo.TacGia").ToList();
        }

        public IEnumerable<TacGia> listAllPaging(int page, int pageSize, string searchString)
        {
            var res = db.TacGias.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                res = res.Where(x => x.HoTenTG.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            return res.OrderByDescending(s => s.ID).ToPagedList(page, pageSize);
        }

        public int Create(string HoTenTG, string DiaChi, string Sdt, string TieuSu)
        {
            string MetaTitle = new MetaLink().nameToMeta(HoTenTG);
            object[] parameters =
            {
                new SqlParameter("@HoTenTG", HoTenTG),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@DienThoai", Sdt),
                new SqlParameter("@TieuSu", TieuSu),
                new SqlParameter("@MetaTitle", MetaTitle)
            };
            int result = db.Database.ExecuteSqlCommand("USP_InsertTacGia @HoTenTG, @DiaChi, @DienThoai, @TieuSu, @MetaTitle", parameters);
            return result;
        }

        public int Edit(int ID, string HoTenTG, string DiaChi, string Sdt, string TieuSu)
        {
            string MetaTitle = new MetaLink().nameToMeta(HoTenTG);
            object[] parameters =
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@HoTenTG", HoTenTG),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@DienThoai", Sdt),
                new SqlParameter("@TieuSu", TieuSu),
                new SqlParameter("@MetaTitle", MetaTitle)
            };
            int result = db.Database.ExecuteSqlCommand("USP_UpdateTacGia @ID , @HoTenTG, @DiaChi, @DienThoai, @TieuSu, @MetaTitle", parameters);
            return result;
        }

        public int Delete(int ID)
        {
            object[] parameters =
            {
                new SqlParameter("@ID", ID)
            };
            int result = db.Database.ExecuteSqlCommand("USP_DeleteTacGia @ID", parameters);
            return result;
        }
    }
}