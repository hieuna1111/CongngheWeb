using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.EF;

namespace WebApplication.Models.DAO
{
    public class SlideModel
    {
        private dbContext db = null;
        public SlideModel()
        {
            db = new dbContext();
        }

        public List<Slide> listAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}