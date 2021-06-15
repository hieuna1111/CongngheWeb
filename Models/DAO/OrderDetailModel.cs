using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.EF;

namespace WebApplication.Models.DAO
{
    public class OrderDetailModel
    {
        private dbContext db = null;
        public OrderDetailModel()
        {
            db = new dbContext();
        }

        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int NumbericSold(int id)
        {
            int SoLuong = 0;
            var sluong = db.OrderDetails.Where(e => e.ProductID == id).ToList();
            if (sluong != null)
            {
                foreach (var item in sluong)
                {
                    SoLuong = SoLuong + (int)item.Quantity;
                }
            }
            return SoLuong;
        }
    }
}