using BotDetect.Web.Mvc;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserID = user.ID;
                    userSession.UserName = user.UserName;
                    Session.Add(CommonConstants.USER_SESSION, userSession); //Lưu Session của user đăng nhập
                    return Redirect("/");

                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "User name hoặc password không đúng");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Bạn không có quyền truy cập");
                }

            }
            return View(model);

        }

        public ActionResult LogOut()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng!")] //Validatetion của captcha
        public ActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var userDao = new UserDao();
                if (userDao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (userDao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User()
                    {
                        Name = model.Name,
                        Password = Encryptor.MD5Hash(model.Password),
                        Phone = model.Phone,
                        Email = model.Email,
                        Address = model.Address,
                        CreatedDate = DateTime.Now,
                        Status = true
                    };
                    if(!string.IsNullOrEmpty(model.ProvinceID)) // THêm tỉnh
                    {
                        user.ProvinceID = int.Parse(model.ProvinceID);
                    }

                    if (!string.IsNullOrEmpty(model.DistrictID)) // THêm quận huyện
                    {
                        user.DistrictID = int.Parse(model.DistrictID);
                    }
                    var result = userDao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("CaptchaCode", "Mã xác nhận không đúng!");
            }
            return View(model);
        }

        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));
            // Đọc từ file xml để lấy dữ liệu các tỉnh
            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel province = null;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);
            }
            return Json(new
            {
                data = list,
                status = true
            });
        }

        public JsonResult LoadDistrict(int provinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));
            // Đọc từ file xml để lấy dữ liệu các tỉnh
            var xElement = xmlDoc.Element("Root").Elements("Item").SingleOrDefault(x => x.Attribute("type").Value == "province"
            && int.Parse(x.Attribute("id").Value)==provinceID);
            var idProvinceID = xElement.Attribute("id").Value; //Lấy ID của province
            var list = new List<DistrictModel>();
            DistrictModel district = null;
            foreach (var item in xElement.Elements("Item").Where(x=>x.Attribute("type").Value=="district"))
            {
                district = new DistrictModel();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = int.Parse(idProvinceID);
                list.Add(district);
            }
            return Json(new
            {
                data = list,
                status = true
            });
        }


    }
}