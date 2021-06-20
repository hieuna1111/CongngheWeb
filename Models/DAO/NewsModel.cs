using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.EF;

namespace WebApplication.Models.DAO
{
    public class NewsModel
    {
        private dbContext db = null;
        public NewsModel()
        {
            db = new dbContext();
        }


    }
}