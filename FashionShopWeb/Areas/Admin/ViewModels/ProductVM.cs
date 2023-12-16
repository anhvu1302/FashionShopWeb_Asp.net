using FashionShopWeb.Models;
using FashionShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShopWeb.Areas.Admin.ViewModels
{
    public class ProductVM
    {
        public long IdSanPham;

        public string TenSanPham;

        public int IdLoaiSP;

        public string AnhSP;

        public string AnhSPChiTiet1;

        public string AnhSPChiTiet2;

        public Nullable<long> GiaBan;

        public Nullable<int> GiamGia;

        public Nullable<int> SoLuongDanhGia;

        public string NoiDungSanPham;

        public string DanhGiaSanPham;

        public string ThanhToanVanChuyen;

        public bool TonTai;

        public long? Gia;
        public ProductVM()
        {

        }
        public ProductVM(SanPhamVM sp)
        {
            IdSanPham = sp.SanPham.IdSanPham;
            TenSanPham = sp.SanPham.TenSanPham;
            IdLoaiSP = sp.SanPham.IdLoaiSP;
            AnhSP = sp.SanPham.AnhSP;
            AnhSPChiTiet1 = sp.SanPham.AnhSPChiTiet1;
            AnhSPChiTiet2 = sp.SanPham.AnhSPChiTiet2;
            GiaBan = sp.SanPham.GiaBan;
            GiamGia = sp.SanPham.GiamGia;
            SoLuongDanhGia = sp.SanPham.SoLuongDanhGia;
            NoiDungSanPham = sp.SanPham.NoiDungSanPham;
            DanhGiaSanPham = sp.SanPham.DanhGiaSanPham;
            ThanhToanVanChuyen = sp.SanPham.ThanhToanVanChuyen;
            TonTai = sp.SanPham.TonTai;
            Gia = sp.Gia;
        }
    }
}