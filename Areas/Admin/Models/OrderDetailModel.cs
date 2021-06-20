using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Models
{
    public class OrderDetailModel
    {
        private dbContext db = null;
        public OrderDetailModel()
        {
            db = new dbContext();
        }

        public IEnumerable<OrderDetail> OrderDetailsCustomer(int id, int page, int pageSize)
        {
            var res = db.OrderDetails.Where(x => x.OrderID == id).ToList();
            return res.ToPagedList(page, pageSize);
        }
    }
}