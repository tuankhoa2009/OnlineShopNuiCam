using Model.Dao;
using Model.EF;
using OnlineShop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int? page)
        {
            var dao = new ProductDao();
            var lstProduct = dao.listProductAll(searchString);
            int pageSize = 5;
            int pageNumber = page ?? 1;
            ViewBag.searchString = searchString;
            return View(lstProduct.ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = new ProductDao().ViewDetail(id);
            var productModel = new ProductModel()
            {
                ID = product.ID,
                Name = product.Name,
                Code = product.Code,
                Price = product.Price,
                Description = product.Description,
                CreatedDate = product.CreatedDate,
                CategoryID = product.CategoryID,
                Image = product.Image,
                MetaTitle = product.MetaTitle,
                Quantity = product.Quantity
            };
            return View(productModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductModel model)
        {
            if(ModelState.IsValid)
            {
               
                // Lấy ra tên của hình
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName); // Lấy ra đuôi hình ảnh (.png,.jpg ...)
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                model.Image = "/assets/client/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("/assets/client/images/"), fileName); //Xử lý thêm anhrvaof thư mục
                model.ImageFile.SaveAs(fileName);

                var dao = new ProductDao();

                var product = new Product()
                {
                    ID = model.ID,
                    Name = model.Name,
                    Code = model.Code,
                    Price = model.Price,
                    Description = model.Description,
                    CreatedDate = model.CreatedDate,
                    CategoryID = model.CategoryID,
                    Image = model.Image,
                    MetaTitle = model.MetaTitle,
                    Quantity = model.Quantity
                };

                var flagUpdate = dao.Update(product);
                if (flagUpdate)
                {
                    return RedirectToAction("Index", "Product");
                }
            }         
            return View(model);
        }

        public JsonResult ChangeStatusProduct(long id)
        {
            var result = new ProductDao().ChangeStatusProduct(id);

            return Json(new { status = result });
        }

        public ActionResult Delete(long id)
        {
            var dao = new ProductDao().Delete(id);
            if(dao != 0)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
              
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            if(ModelState.IsValid)
            {
                // Lấy ra tên của hình
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName); // Lấy ra đuôi hình ảnh (.png,.jpg ...)
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                model.Image = "/assets/client/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("/assets/client/images/"), fileName); //Xử lý thêm anhrvaof thư mục
                model.ImageFile.SaveAs(fileName);
                var product = new Product()
                {
                    Name = model.Name,
                    Code = model.Code,
                    Price = model.Price,
                    Description = model.Description,
                    CreatedDate = model.CreatedDate,
                    CategoryID = model.CategoryID,
                    Image = model.Image,
                    MetaTitle = model.MetaTitle,
                    Quantity = model.Quantity
                };
                var insertProductID = new ProductDao().InsertProduct(product);
                if(insertProductID > 0)
                {
                    ModelState.Clear();
                    model = new ProductModel();
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                }
            }
           
            return View(model);
        }
    }
}