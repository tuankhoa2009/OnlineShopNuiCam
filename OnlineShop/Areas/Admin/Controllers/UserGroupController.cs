using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserGroupController : Controller
    {
        // GET: Admin/UserGroup
        public ActionResult Index()
        {
            var lstCredentialDao = new CredentialDao().listCredential();
            Dictionary<string,List<Credential>> item = lstCredentialDao.GroupBy(x=>x.UserGroupID).ToDictionary(s =>s.Key,x=>x.ToList());
            ViewBag.ListCredential = item;
            ViewBag.Mod = new CredentialDao().ListCredentialRoles("MOD");
            ViewBag.Admin = new CredentialDao().ListCredentialRoles("ADMIN");
            ViewBag.Member = new CredentialDao().ListCredentialRoles("MEMBER");
            return View();
        }
    }
}