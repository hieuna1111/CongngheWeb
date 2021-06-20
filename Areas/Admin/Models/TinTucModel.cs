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
    public class TinTucModel
    {
        private dbContext db = null;
        public TinTucModel()
        {
            db = new dbContext();
        }

        public List<TinTuc> ListAll(int id)
        {
            //var list = db.Database.SqlQuery<_Sach>("SELECT * FROM Sach ORDER BY ID DESC").ToList();
            var list = db.Database.SqlQuery<TinTuc>("USP_SelectDetailTinTuc @ID = " + id + "").ToList();
            return list;
        }

        public string GetPathImageByID(int id)
        {
            var res = db.TinTucs.Where(x => x.ID == id).FirstOrDefault();
            return res.Image;
        }

        public IEnumerable<TinTuc> listAllPaging(int page, int pageSize, string searchString)
        {
            var res = db.TinTucs.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                res = res.Where(x => x.Name.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }

            return res.OrderByDescending(s => s.ID).ToPagedList(page, pageSize);
        }

        public int Create(string name, string image, string description)
        {
            string MetaTitle = new MetaLink().nameToMeta(name);
            var date = DateTime.Now;
            object[] parameters =
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Image", image),
                new SqlParameter("@Detail", description),
                new SqlParameter("@CreatedDate", date),
                new SqlParameter("@MetaTitle", MetaTitle)
            };
            int result = db.Database.ExecuteSqlCommand("USP_InsertTinTuc @Name, @Image, @Detail, @CreatedDate, @MetaTitle", parameters);
            return result;
        }

        public int Edit(int ID, string name, string image, string description)
        {
            string MetaTitle = new MetaLink().nameToMeta(name);
            var date = DateTime.Now;
            object[] parameters =
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@Name", name),
                new SqlParameter("@Image", image),
                new SqlParameter("@Detail", description),
                new SqlParameter("@CreatedDate", date),
                new SqlParameter("@MetaTitle", MetaTitle)
            };
            int result = db.Database.ExecuteSqlCommand("USP_UpdateTinTuc @ID, @Name, @Image,  @Detail, @CreatedDate, @MetaTitle", parameters);

            return result;
        }

        public int Delete(int id)
        {
            object[] parameters =
            {
                new SqlParameter("@ID", id)
            };
            int result = db.Database.ExecuteSqlCommand("USP_DeleteTinTuc @ID", parameters);
            return result;
        }
    }
}