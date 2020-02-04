using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;


namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().listAllData();      
            return PartialView(model);
        }

        public ActionResult ProductAll(int? page)
        {
            string sreachString = null;
            int pageSize = 3;
            int pageNumber = page ?? 1;
            var model = new ProductDao().listProductAll(sreachString);

            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public JsonResult ListName(string keySearch)
        {

            var res = new ProductDao().ListName(keySearch);

            return Json(new
            {
                data = res,
                status = true
            },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Category (long id,int? page)
        {
            int pageSize = 1;
            int pageNumber = page ?? 1;
            var categoryProduct = new ProductCategoryDao().ViewDetail(id);
            var lstCategoryProduct = new ProductDao().listProductCategoryById(categoryProduct.ID);
            if(lstCategoryProduct != null)
            {
                ViewBag.Category = categoryProduct;
                return View(lstCategoryProduct.ToPagedList(pageNumber,pageSize));
            }
            TempData["StatusCategory"] = "Không tìm thấy danh mục sản phẩm";
            return View();
        }

        public ActionResult Sreach(string keyWord , int? page)
        {
            int pageSize = 1;
            int pageNumber = page ?? 1;
            var lstCategoryProduct = new ProductDao().listProductCategoryBySearch(keyWord);
            ViewBag.KeyWord = keyWord;
            if (lstCategoryProduct != null)
            {
                return View(lstCategoryProduct.ToPagedList(pageNumber, pageSize));
            }
            TempData["StatusCategory"] = "Không tìm thấy danh mục sản phẩm";
            return View();
        }

        public ActionResult DetailProduct(long id)
        {
            var objProduct = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(id);
            ViewBag.RelatedProducts = new ProductDao().listRelatedProduct(objProduct.ID);
            if (ViewBag.Category == null)
            {
                return View();
            }
            if (objProduct !=null)
            {
                return View(objProduct);
            }
            return View();
        }


    }
}