using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext) //Hàm kiểm tra Nếu không có session thì trở về trang đăng nhập
        {

            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if(session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }

            base.OnActionExecuted(filterContext);
        }

        protected void SetAlert(string message,string type)
        {
            TempData["AlertMessage"] = message; 
            if(type =="success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if(type =="warning")
            {

                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {

                TempData["AlertType"] = "alert-error";
            }
        }

    }
}