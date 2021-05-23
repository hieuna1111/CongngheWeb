using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            IQueryable<ChuDe> model = db.ChuDes;

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.TenCD.Contains(searchString)); 
            }

            return model.OrderByDescending(s => s.ID).ToPagedList(page, pageSize);
        }

        public int Create(string TenCD)
        {
            object[] parameters = 
            {
                new SqlParameter("@ChuDe", TenCD)
            };
            int result = db.Database.ExecuteSqlCommand("USP_InsertChuDe @ChuDe", parameters);

            return result;
        }

        public int Edit(int ID, string TenCD)
        {
            object[] parameters =
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@newChuDe", TenCD)
            };
            int result = db.Database.ExecuteSqlCommand("USP_UpdateChuDe @ID ,@newChuDe", parameters);
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