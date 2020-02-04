using Model.Dao;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Slide = new SlideDao().listAllData();
            var productList = new ProductDao();
            ViewBag.listNewProducts = productList.listNewProduct(4);
            ViewBag.listFutureProducts = productList.listFutureProduct(4);
            return View();
        }

        [ChildActionOnly]
       // [OutputCache(Duration =3600 * 24)] // Sau 1 ngày khi web sẽ ko gọi server để load lai mainmenu
        public ActionResult MainMenu()
        {
            var res = new MenuDao().ListByGroupId(1.ToString());
            return PartialView(res); 
        }

        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var res = new MenuDao().ListByGroupId(2.ToString());
            return PartialView(res);
        }

        public ActionResult Footer()
        {
            var res = new FooterDao().GetFooter();
            return PartialView(res);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        // GET: Home
        public ActionResult About()
        {
            
            return View();
        }
        public ActionResult Content()
        {

            return View();
        }

    }
}