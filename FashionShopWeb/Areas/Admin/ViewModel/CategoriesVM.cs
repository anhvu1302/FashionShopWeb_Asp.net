using FashionShopWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShopWeb.Areas.Admin.ViewModels
{
    public class CategoriesVM
    {
        public LoaiSanPham Category { get; set; }
        public int IdLoaiSP { get; set; }
        [StringLength(255, ErrorMessage ="Không được vượt quá 255 ký tự")]
        [Display(Name = "Tên loại sản phẩm")]
        public string TenLoaiSP { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên loại sản phẩm.")]
        [Display(Name = "Loại sản phẩm cha")]
        public int? IdLoaiSPCha { get; set; }
        [Display(Name = "Tên loại sản phẩm cha")]
        public string TenLoaiSPCha { get; set; }
        public List<SelectListItem> LoaiSanPhamChaList { get; set; }
        public CategoriesVM()
        {

        }
        public CategoriesVM(LoaiSanPham lsp)
        {
            IdLoaiSP = lsp.IdLoaiSP;
            TenLoaiSP = lsp.TenLoaiSP;
            IdLoaiSPCha = lsp.IdLoaiSPCha;
        }
    }
}