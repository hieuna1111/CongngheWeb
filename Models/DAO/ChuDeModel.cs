using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.EF;

namespace WebApplication.Models.DAO
{
    public class ChuDeModel
    {
        private dbContext db = null;
        public ChuDeModel()
        {
            db = new dbContext();
        }

        public List<ChuDe> listAll()
        {
            return db.ChuDes.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public ChuDe CategoryDetail(int id)
        {
            return db.ChuDes.Find(id);
        }
    }
}