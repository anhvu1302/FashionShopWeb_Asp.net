using FashionShopWeb.Models;
using FashionShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace FashionShopWeb.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        FashionShopDataContext db = new FashionShopDataContext();
        //GET: Admin/AdminHome
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]

        public ActionResult DashBoard()
        {
            return View();
        }
    }
}