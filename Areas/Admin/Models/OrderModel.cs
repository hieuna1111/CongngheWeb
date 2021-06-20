using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.EF;

namespace WebApplication.Areas.Admin.Models
{
    public class OrderModel
    {
        private dbContext db = null;
        public OrderModel()
        {
            db = new dbContext();
        }

        public IEnumerable<Order> listAll(string searchString, int page, int pageSize)
        {
            var res = db.Orders.OrderByDescending(x => x.CreatedDate).ThenBy(x => x.Status == false).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                res = res.Where(x => x.ShipName.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            return res.ToPagedList(page, pageSize);
        }

        public int Insert(Order o)
        {
            o.Status = false;
            db.Orders.Add(o);
            db.SaveChanges();
            return o.ID;
        }

        public void ConfirmOrder(int id)
        {
            var res = db.Orders.Find(id);
            res.Status = true;
            db.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var res = db.Orders.Find(id);
            db.Orders.Remove(res);
            db.SaveChanges();
        }
    }
}