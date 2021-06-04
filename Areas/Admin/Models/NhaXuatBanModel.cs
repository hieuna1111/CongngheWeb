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
    public class NhaXuatBanModel
    {
        private dbContext db = null;
        public NhaXuatBanModel()
        {
            db = new dbContext();
        }

        public List<NhaXuatBan>listAll()
        {
            return db.Database.SqlQuery<NhaXuatBan>("SELECT * FROM dbo.NhaXuatBan").ToList();
        }

        public IEnumerable<NhaXuatBan> listAllPaging(int page, int pageSize, string searchString)
        {
            var res = db.NhaXuatBans.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                res = res.Where(x => x.TenNXB.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            return res.OrderByDescending(s => s.ID).ToPagedList(page, pageSize);
        }

        public int Create(string TenNXB, string DiaChi, string sdt)
        {
            string MetaTitle = new MetaLink().nameToMeta(TenNXB);
            object[] parameters =
            {
                new SqlParameter("@TenNXB", TenNXB),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@SDT", sdt),
                new SqlParameter("@MetaTitle", MetaTitle)
            };
            int result = db.Database.ExecuteSqlCommand("USP_InsertNhaXuatBan @TenNXB, @DiaChi, @SDT, @MetaTitle", parameters);
            return result;
        }

        public int Edit(int ID, string TenNXB, string DiaChi, string sdt)
        {
            string MetaTitle = new MetaLink().nameToMeta(TenNXB);
            object[] parameters =
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@TenNXB", TenNXB),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@SDT", sdt),
                new SqlParameter("@MetaTitle", MetaTitle)
            };
            int result = db.Database.ExecuteSqlCommand("USP_UpdateNhaXuatBan @ID , @TenNXB, @DiaChi, @SDT, @MetaTitle", parameters);
            return result;
        }

        public int Delete(int id)
        {
            object[] parameters =
            {
                new SqlParameter("@ID", id)
            };
            int result = db.Database.ExecuteSqlCommand("USP_DeleteNhaXuatBan @ID", parameters);
            return result;
        }
    }
}