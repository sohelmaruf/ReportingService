using Ari_ReportProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ari_ReportProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            

            if (User.Identity.IsAuthenticated && Session["password"] == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                

                return RedirectToAction("Login", "Account");
            }

            var _SC = new SC.SCService();
            _SC.Timeout = 5 * 60 * 1000;
            _SC.AuthHeaderValue = new SC.AuthHeader();
            _SC.AuthHeaderValue.UserName = User.Identity.Name;

            

            _SC.AuthHeaderValue.Password = Session["password"].ToString();

            var filters = new SC.SerializableDictionaryOfStringString();
           
            var orderIDs = _SC.Orders_Get(filters);
            var orders = new List<SC.OrderData>();
            if (Session["orders"] != null)
            {
                orders = Session["orders"] as List<SC.OrderData>;
            }
            else
            {
                if (orderIDs.Length > 0)
                {
                    orders = _SC.Orders_GetDatas(orderIDs).ToList();
                    Session["orders"] = orders;

                }
            }
            

            ViewBag.orders = orders.OrderByDescending(o=>o.Order.TimeOfOrder).ToList();
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}