using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models._EF;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Models
{
    public class ChuDeModel
    {
        private dbContext db = null;
        public ChuDeModel()
        {
            db = new dbContext();
        }

        public List<ChuDe>listAll()
        {
            return db.Database.SqlQuery<ChuDe>("Select * from ChuDe").ToList();
        }

        public IEnumerable<ChuDe>listAllPaging(int page, int pageSize, string searchString)
        {
            var res = db.ChuDes.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                res = res.Where(x => x.TenCD.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }

            return res.OrderByDescending(s => s.ID).ToPagedList(page, pageSize);
        }

        public int Create(string TenCD)
        {
            string MetaTitle = new MetaLink().nameToMeta(TenCD);
            object[] parameters = 
            {
                new SqlParameter("@ChuDe", TenCD),
                new SqlParameter("@MetaTitle", MetaTitle)
            };
            int result = db.Database.ExecuteSqlCommand("USP_InsertChuDe @ChuDe, @MetaTitle", parameters);

            return result;
        }

        public int Edit(int ID, string TenCD)
        {
            string MetaTitle = new MetaLink().nameToMeta(TenCD);
            object[] parameters =
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@newChuDe", TenCD),
                new SqlParameter("@MetaTitle", MetaTitle)
            };
            int result = db.Database.ExecuteSqlCommand("USP_UpdateChuDe @ID ,@newChuDe, @MetaTitle", parameters);
            return result;
        }

        public int Delete(int id)
        {
            object[] parameters =
            {
                new SqlParameter("@ID", id)
            };
            int result = db.Database.ExecuteSqlCommand("USP_DeleteChuDe @ID", parameters);
            return result;
        }
    }
}