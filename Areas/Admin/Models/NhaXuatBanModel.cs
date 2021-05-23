using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
            IQueryable<NhaXuatBan> model = db.NhaXuatBans;

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.TenNXB.Contains(searchString));
            }

            return model.OrderByDescending(s => s.ID).ToPagedList(page, pageSize);
        }

        public int Create(string TenNXB, string DiaChi, string sdt)
        {
            object[] parameters =
            {
                new SqlParameter("@TenNXB", TenNXB),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@SDT", sdt)
            };
            int result = db.Database.ExecuteSqlCommand("USP_InsertNhaXuatBan @TenNXB, @DiaChi, @SDT", parameters);

            return result;
        }

        public int Edit(int ID, string TenNXB, string DiaChi, string sdt)
        {
            object[] parameters =
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@TenNXB", TenNXB),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@SDT", sdt)
            };
            int result = db.Database.ExecuteSqlCommand("USP_UpdateNhaXuatBan @ID , @TenNXB, @DiaChi, @SDT", parameters);
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