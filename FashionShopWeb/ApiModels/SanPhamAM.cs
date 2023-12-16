using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionShopWeb.Models;

namespace FashionShopWeb.ApiModels
{
    public class SanPhamAP
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
        public SanPhamAP()
        {
            
        }
        public SanPhamAP(SanPham sp)
        {
            IdSanPham = sp.IdSanPham;
            TenSanPham = sp.TenSanPham;
            IdLoaiSP = sp.IdLoaiSP;
            AnhSP = sp.AnhSP;
            AnhSPChiTiet1 = sp.AnhSPChiTiet1;
            AnhSPChiTiet2 = sp.AnhSPChiTiet2;
            GiaBan = sp.GiaBan;
            GiamGia = sp.GiamGia;
            SoLuongDanhGia = sp.SoLuongDanhGia;
            NoiDungSanPham = sp.NoiDungSanPham;
            DanhGiaSanPham = sp.DanhGiaSanPham;
            ThanhToanVanChuyen = sp.ThanhToanVanChuyen;
            TonTai = sp.TonTai;
        }    
    }
}