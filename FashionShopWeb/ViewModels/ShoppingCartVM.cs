﻿using FashionShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShopWeb.ViewModels
{
    public class ShoppingCartVM
    {
        FashionShopDataContext db = new FashionShopDataContext();

        public long IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string AnhSP { get; set; }
        public long? Gia { get; set; }
        public int SoLuong { get; set; }
        public long? ThanhTien()
        {
            return Gia * SoLuong;
        }
        public ShoppingCartVM(long IdSanPham)
        {
            this.IdSanPham = IdSanPham;
            var product = db.SanPhams.Single(x=>x.IdSanPham == IdSanPham);
            this.TenSanPham = product.TenSanPham;
            this.AnhSP = product.AnhSP;
            this.Gia = db.func_GiaSanPham(IdSanPham);
            this.SoLuong = 1;
        }
    }
}