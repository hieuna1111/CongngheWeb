using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.EF;

namespace WebApplication.Models.DAO
{
    public class TinTucModel
    {
        private dbContext db = null;
        public TinTucModel()
        {
            db = new dbContext();
        }

        public List<TinTuc> listAll()
        {
            return db.TinTucs.Where(x => x.Status == true).OrderByDescending(x=>x.ID).ToList();
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

        public TinTuc getNewsDetail(int id)
        {
            return db.TinTucs.Find(id);
        }
    }
}