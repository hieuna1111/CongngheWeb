using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models._EF;
using WebApplication.Models.DAO;
using System.Web.Script.Serialization;
using WebApplication.Models.EF;

namespace WebApplication.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return View(list);
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.sach.ID == item.sach.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;

            return Json(new { 
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.sach.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult AddItem(int productID, int quantity)
        {
            var product = new SachModel().getBookDetail(productID);

            var cart = Session[CartSession];

            if (cart != null)
            {
                var list = (List<CartItem>)cart;

                if (list.Exists(x=>x.sach.ID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.sach.ID == productID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //Tạo mới đối tượng
                    var item = new CartItem();
                    item.sach = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }

                //Gán vào Session
                Session[CartSession] = list;
            }
            else
            {
                //Tạo mới đối tượng
                var item = new CartItem();
                item.sach = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);

                //Gán vào Session
                Session[CartSession] = list;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMobile = mobile;
            order.ShipName = shipName;

            if (order.ShipAddress != "" && order.ShipMobile != "" && order.ShipName != "")
            {
                var id = new OrderModel().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailModel = new OrderDetailModel();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.sach.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.sach.GiaBan;
                    orderDetail.Quantity = item.Quantity;
                    detailModel.Insert(orderDetail);
                }
            }
            else
            {
                return Redirect("/loi-thanh-toan");
            }
            Session[CartSession] = null;
            return Redirect("/thanh-toan-thanh-cong");
        }

        public ActionResult SuccessPayment()
        {
            return View();
        }

        public ActionResult UnSuccessPayment()
        {
            return View();
        }
    }
}