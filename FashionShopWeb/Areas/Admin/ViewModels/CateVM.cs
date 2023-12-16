using FashionShopWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FashionShopWeb.Areas.Admin.ViewModels
{
    public class CateVM
    {
        public LoaiSanPham LoaiSanPham { get; set; }
        public LoaiSanPhamCha LoaiSanPhamCha { get; set; }
        public CateVM() { }
    }
}