using FashionShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShopWeb.ViewModels
{
    public class FavoriteProductPartialVM
    {
        FashionShopDataContext db = new FashionShopDataContext();

        public int IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string AnhSP { get; set; }
        public long? Gia { get; set; }
        public FavoriteProductPartialVM(int IdSanPham)
        {
            this.IdSanPham = IdSanPham;
            var product = db.SanPhams.Single(x => x.IdSanPham == IdSanPham);
            this.TenSanPham = product.TenSanPham;
            this.AnhSP = product.AnhSP;
            this.Gia = db.func_GiaSanPham(IdSanPham);
        }
    }
}