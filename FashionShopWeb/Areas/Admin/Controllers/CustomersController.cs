using FashionShopWeb.Areas.Admin.ViewModels;
using FashionShopWeb.Models;
using FashionShopWeb.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using FashionShopWeb.Areas.Admin.ViewModel;

namespace FashionShopWeb.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Admin/Customers
        FashionShopDataContext db = new FashionShopDataContext();
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult GetAllCustomers(string customerSearchType, string customerSearchInput, string sortCol, string sortType, int page = 1)
        {
            IEnumerable<CustomerVM> query = null;
            if (!string.IsNullOrEmpty(customerSearchType) && !string.IsNullOrEmpty(customerSearchInput))
            {
                query = db.KhachHangs
                        .Where(c => c.TenKhachHang.Contains(customerSearchInput))
                        .Select(cus => new CustomerVM
                        {
                            customer = cus
                        });
            }
            else
            {
                query = from cus in db.KhachHangs
                        orderby cus.IdKhachHang descending
                        select new CustomerVM
                        {
                            customer = cus

                        };
            }
            int NoOfRecordPerPage = 12;
            int NoOfPages = (int)Math.Ceiling((double)query.Count() / NoOfRecordPerPage);
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.STT = (page - 1) * NoOfRecordPerPage + 1;
            ViewBag.NoOfPages = NoOfPages;
            query = query.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage);
            if (!string.IsNullOrEmpty(sortCol) && !string.IsNullOrEmpty(sortType))
            {
                switch (sortCol)
                {
                    case "CustomerName":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.name);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.name);
                        else return HttpNotFound();
                        break;
                    case "Birth":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.birth);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.birth);
                        else return HttpNotFound();
                        break;
                    case "Gender":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.gender);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.gender);
                        else return HttpNotFound();
                        break;
                    default:
                        return HttpNotFound();
                }
            }
                return View(query);
        }
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult Search(string customerSearchType, string customerSearchInput)
        {
            if (!string.IsNullOrEmpty(customerSearchInput))
            {
                switch (customerSearchType)
                {
                    case "id":
                        var query = db.KhachHangs.Where(p => p.IdKhachHang.ToString().Contains(customerSearchInput));
                        break;
                    case "name":
                        query = db.KhachHangs.Where(p => p.TenKhachHang.Contains(customerSearchInput));
                        break;
                    case "phoneNumber":
                        query = db.KhachHangs.Where(p => p.SoDienThoai.Contains(customerSearchInput));
                        break;
                    case "birth":
                        query = db.KhachHangs.Where(p => p.NgaySinh.ToString().Contains(customerSearchInput));
                        break;
                    case "email":
                        query = db.KhachHangs.Where(p => p.Email.Contains(customerSearchInput));
                        break;
                }
            }
            return View();
        }
    }
}