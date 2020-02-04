using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using OnlineShop.Areas.Admin.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int? page)
        {
            var dao = new UserDao();
            var lstUser = dao.listAllUser(searchString);
            int pageSize = 1;
            int pageNumber = page ?? 1;
            ViewBag.searchString = searchString;
            return View(lstUser.ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Edit(int id)
        {
            var User = new UserDao().SingleUserID(id);
            return View(User);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if(!string.IsNullOrEmpty(user.Password))
                {
                    var encryptorPassword = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptorPassword;
                }           
                bool tempFlag = dao.Update(user); //Isert user xuông db
                if (tempFlag)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Update user thất bại");
                }

            }

            return View(user);
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            var dao = new UserDao().Delete(id);
            if(dao)
            {
                return RedirectToAction("Index","User");
            }

            return View();
        }



        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(UserModel usermodel)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptorPassword = Encryptor.MD5Hash(usermodel.Password);
                var user = new User()
                {
                    UserName = usermodel.UserName,
                    Password = encryptorPassword,
                    Name = usermodel.Name,
                    Address = usermodel.Address,
                    Email = usermodel.Email,
                    Phone = usermodel.Phone,
                    Status = usermodel.Status
                };
                long id = dao.Insert(user); //Isert user xuông db
                if(id >0)
                {
                    SetAlert("Thêm user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user thất bại");
                }

            }

            return View(usermodel);
        }
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);

            return Json(new { status=result});
        }


        public ActionResult LogOutAdmin()
        {
            var userSession = new UserLogin();
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/Admin/Login");
        }

    }
}