using FashionShopWeb.Models;
using FashionShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace FashionShopWeb.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Admin/Orders
        FashionShopDataContext db = new FashionShopDataContext();
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult GetAll(string sortCol, string sortType, int page = 1)
        {
            IEnumerable<DonHangVM> query = null;

            query = from donHang in db.DonHangs
                    orderby donHang.IdDonHang descending
                    select new DonHangVM
                    {
                        DonHang = donHang,
                        TongTien = db.func_TongTienDonHang(donHang.IdDonHang),
                        ListChiTietDonHang = db.ChiTietDonHangs.Where(ct => ct.IdDonHang == donHang.IdDonHang).ToList(),
                    };




            // Paging
            int NoOfRecordPerPage = 6;
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
                    case "IdKhachHang":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.DonHang.IdKhachHang);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.DonHang.IdKhachHang);
                        else return HttpNotFound();
                        break;
                    case "TongTien":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.TongTien);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.TongTien);
                        else return HttpNotFound();
                        break;
                    case "ThoiGianDat":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.DonHang.ThoiGianDatHang);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.DonHang.ThoiGianDatHang);
                        else return HttpNotFound();
                        break;
                    case "TrangThai":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.DonHang.TrangThaiDonHang);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.DonHang.TrangThaiDonHang);
                        else return HttpNotFound();
                        break;
                    default:
                        return HttpNotFound();
                }
            }

            return View(query);
        }
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult ConfirmOrder(long orderId)
        {
            var order = db.DonHangs.Where(x => x.IdDonHang == orderId).FirstOrDefault();

            if (order != null)
            {
                order.TrangThaiDonHang = "Đã xác nhận";
                db.SubmitChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Order not found" });
        }

    }
}