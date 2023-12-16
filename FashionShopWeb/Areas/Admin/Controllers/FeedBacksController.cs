using FashionShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace FashionShopWeb.Areas.Admin.Controllers
{
    public class FeedBacksController : Controller
    {
        // GET: Admin/FeedBacks
        FashionShopDataContext db = new FashionShopDataContext();
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult AllFeedBacks()
        {
            return View(db.PhanHois.ToList());
        }
    }
}