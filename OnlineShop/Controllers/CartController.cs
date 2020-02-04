using Common;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[OnlineShop.Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return View(list);
        }


        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var cartSession = (List<CartItem>)Session[OnlineShop.Common.CommonConstants.CartSession];
            foreach (var item in cartSession)
            {
                var objCartItem = jsonCart.FirstOrDefault(x => x.Product.ID == item.Product.ID);
                if(objCartItem != null)
                {
                    item.Quantity = objCartItem.Quantity;
                }
            }

            return Json(new { status = true }); 
        }

        public JsonResult DeleteAll()
        {
            var cartSession = (List<CartItem>)Session[OnlineShop.Common.CommonConstants.CartSession];
            cartSession = null;
            Session[OnlineShop.Common.CommonConstants.CartSession] = cartSession;
            return Json(new { status = true });
        }

        public JsonResult DeleteById(long id)
        {
            var cartSession = (List<CartItem>)Session[OnlineShop.Common.CommonConstants.CartSession];
            var objCartSession = cartSession.FirstOrDefault(x => x.Product.ID == id);
            if(objCartSession != null)
            {
                cartSession.Remove(objCartSession);
            }
            return Json(new { status = true });
        }


        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[OnlineShop.Common.CommonConstants.CartSession];
            if(cart !=null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(s=>s.Product.ID == productId))
                {
                    foreach(var item in list)
                    {
                        if(item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //Tạo mới đỗi tượng
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[OnlineShop.Common.CommonConstants.CartSession] = list;
            }
            else
            {
                //Tạo mới đỗi tượng
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);

                Session[OnlineShop.Common.CommonConstants.CartSession] = list;
            }
            return RedirectToAction("Index");


        }


        public ActionResult Payment()
        {
            var cart = Session[OnlineShop.Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName,string mobile,string address,string email)
        {
            var order = new Order
            {
                CreateDate = DateTime.Now,
                ShipName = shipName,
                ShipMobile = mobile,
                ShipEmail = email,
                ShipAddress = address
            };
            var idOrder = new OrderDao().Insert(order);
            var cart = (List<CartItem>)Session[OnlineShop.Common.CommonConstants.CartSession];
            double total = 0;
            var orderDetailDao = new OrderDetailDao();
            foreach (var item in cart)
            {
                var orderDetail = new OrderDetail()
                {
                    OrderID = idOrder,
                    ProductID = item.Product.ID,
                    Price = (double)item.Product.Price,
                    Quantity = item.Quantity
                };
                orderDetailDao.Insert(orderDetail);
                total += Convert.ToDouble(item.Product.Price.GetValueOrDefault(0) * item.Quantity);
            }
            string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

            content = content.Replace("{{CustomerName}}", shipName);
            content = content.Replace("{{Phone}}", mobile);
            content = content.Replace("{{Email}}", email);
            content = content.Replace("{{Address}}", address);
            content = content.Replace("{{Total}}", total.ToString("N0"));

            new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
            return Redirect("/hoan-thanh");
        }

        public ActionResult PaymentSuccess()
        {
            return View();
        }


    }
}