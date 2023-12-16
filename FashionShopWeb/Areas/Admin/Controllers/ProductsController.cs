using FashionShopWeb.ApiModels;
using FashionShopWeb.Areas.Admin.ViewModels;
using FashionShopWeb.Models;
using FashionShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;
namespace FashionShopWeb.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Admin/Products/
        FashionShopDataContext db = new FashionShopDataContext();

        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult GetAll(string productSearchType, string productSearchInput, string sortCol, string sortType, int page = 1)
        {
            IEnumerable<SanPhamVM> query = null;
            if (!string.IsNullOrEmpty(productSearchType) && !string.IsNullOrEmpty(productSearchInput))
            {
                query = db.SanPhams
                        .Where(p => p.TenSanPham.Contains(productSearchInput))
                        .Select(sanPham => new SanPhamVM
                        {
                            SanPham = sanPham,
                            Gia = db.func_GiaSanPham(sanPham.IdSanPham)
                        });
            }
            else
            {
                query = from sanPham in db.SanPhams
                        orderby sanPham.IdSanPham descending
                        select new SanPhamVM
                        {
                            SanPham = sanPham,
                            Gia = db.func_GiaSanPham(sanPham.IdSanPham)
                        };
            }



            // Paging
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
                    case "GiaDaGiam":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.Gia);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.Gia);
                        else return HttpNotFound();
                        break;
                    case "GiaGoc":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.SanPham.GiaBan);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.SanPham.GiaBan);
                        else return HttpNotFound();
                        break;
                    case "GiamGia":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.SanPham.GiamGia);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.SanPham.GiamGia);
                        else return HttpNotFound();
                        break;
                    case "SoLuongDanhGia":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.SanPham.SoLuongDanhGia);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.SanPham.SoLuongDanhGia);
                        else return HttpNotFound();
                        break;
                    default:
                        return HttpNotFound();
                }
            }

            return View(query);
        }
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]

        public ActionResult AddPartial()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var lsp in db.LoaiSanPhamChas.ToList())
            {
                items.Add(new SelectListItem
                {
                    Value = lsp.IdLoaiSPCha.ToString(),
                    Text = lsp.TenLoaiSPCha
                });
            }

            ViewBag.SPLoaiCha = items;

            return PartialView();
        }
        [CustomAuthorize("Admin", "Quản lý")]
        [HttpPost]
        public JsonResult Add(SanPham sp)
        {
            var res = new { success = false, message = "Thêm sản phẩm không thành công" };
            SanPham temp = sp;
            if (ModelState.IsValid)
            {
                try
                {
                    SanPham newSp = new SanPham()
                    {
                        TenSanPham = sp.TenSanPham,
                        IdLoaiSP = sp.IdLoaiSP,
                        AnhSP = sp.AnhSP,
                        AnhSPChiTiet1 = sp.AnhSPChiTiet1,
                        AnhSPChiTiet2 = sp.AnhSPChiTiet2,
                        GiaBan = sp.GiaBan,
                        GiamGia = sp.GiamGia,
                        SoLuongDanhGia = sp.SoLuongDanhGia,
                        NoiDungSanPham = sp.NoiDungSanPham,
                        DanhGiaSanPham = sp.DanhGiaSanPham,
                        ThanhToanVanChuyen = sp.ThanhToanVanChuyen,
                        TonTai = true
                    };
                    db.SanPhams.InsertOnSubmit(newSp);
                    db.SubmitChanges();

                    res = new { success = true, message = "Thêm sản phẩm thành công" };
                }
                catch (Exception ex)
                {

                    res = new { success = false, message = "Đã xảy ra lỗi:" + ex.Message };
                }
            }

            return Json(res);
        }
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult EditPartial()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var lsp in db.LoaiSanPhamChas.ToList())
            {
                items.Add(new SelectListItem
                {
                    Value = lsp.IdLoaiSPCha.ToString(),
                    Text = lsp.TenLoaiSPCha
                });
            }

            ViewBag.SPLoaiCha = items;
            return PartialView();
        }
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult GetDetails(long productId)
        {
            long a = productId;
            var query = (from sanPham in db.SanPhams
                         where sanPham.IdSanPham == productId
                         select new SanPhamVM
                         {
                             SanPham = sanPham,
                             Gia = db.func_GiaSanPham(sanPham.IdSanPham)
                         }).FirstOrDefault();
            long idLoaiCha = (from sanPham in db.SanPhams
                              join loaiSanPham in db.LoaiSanPhams on sanPham.IdLoaiSP equals loaiSanPham.IdLoaiSP
                              where sanPham.IdSanPham == productId
                              select loaiSanPham.IdLoaiSPCha).FirstOrDefault().Value;
            ProductVM product = new ProductVM(query);
            if (product != null)
            {
                return Json(new
                {
                    productData = product,
                    idLoaiCha = idLoaiCha
                }, JsonRequestBehavior.AllowGet);

            }

            return HttpNotFound();
        }
        [CustomAuthorize("Admin", "Quản lý")]
        public JsonResult Update(SanPham sp)
        {
            var res = new { success = false, message = "Cập nhật sản phẩm không thành công" };
            SanPham temp = sp;
            if (ModelState.IsValid)
            {
                try
                {
                    var query = db.SanPhams.Where(s => s.IdSanPham == sp.IdSanPham).First();
                    if(query != null)
                    {
                        query.TenSanPham = sp.TenSanPham;
                        query.IdLoaiSP = sp.IdLoaiSP;
                        query.AnhSP = sp.AnhSP;
                        query.AnhSPChiTiet1 = sp.AnhSPChiTiet1;
                        query.AnhSPChiTiet2 = sp.AnhSPChiTiet2;
                        query.GiaBan = sp.GiaBan;
                        query.GiamGia = sp.GiamGia;
                        query.NoiDungSanPham = sp.NoiDungSanPham;
                        query.DanhGiaSanPham = sp.DanhGiaSanPham;
                        query.ThanhToanVanChuyen = sp.ThanhToanVanChuyen;
                        db.SubmitChanges();
                    }

                    res = new { success = true, message = "Thêm sản phẩm thành công" };
                }
                catch (Exception ex)
                {

                    res = new { success = false, message = "Đã xảy ra lỗi:" + ex.Message };
                }
            }

            return Json(res);
        }
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult Search(string productSearchType, string productSearchInput)
        {

            if (!string.IsNullOrEmpty(productSearchInput))
            {
                if (productSearchType == "id")
                {
                    var query = db.SanPhams.Where(p => p.IdLoaiSP.ToString().Contains(productSearchInput));
                }
                else if (productSearchType == "name")
                {
                    var query = db.SanPhams.Where(p => p.TenSanPham.Contains(productSearchInput));
                }
            }
            return View();
        }

        [CustomAuthorize("Admin", "Quản lý")]
        public JsonResult Delete(long idSanPham)
        {
            var res = new { success = false, message = "Xóa sản phẩm không thành công" };

            try
            {
                var productToDelete = db.SanPhams.FirstOrDefault(p => p.IdSanPham == idSanPham);

                if (productToDelete != null)
                {
                    db.SanPhams.DeleteOnSubmit(productToDelete);
                    db.SubmitChanges();

                    res = new { success = true, message = "Xóa sản phẩm thành công" };
                }
                else
                {
                    res = new { success = false, message = "Sản phẩm không tồn tại" };
                }
            }
            catch (Exception ex)
            {
                res = new { success = false, message = "Đã xảy ra lỗi: " + ex.Message };
            }

            return Json(res);
        }

    }
}