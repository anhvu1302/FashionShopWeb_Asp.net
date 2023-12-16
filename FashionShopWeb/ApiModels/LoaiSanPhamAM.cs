using FashionShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShopWeb.ApiModels
{
    public class LoaiSanPhamAM
    {
        public int IdLoaiSP {  get; set; }

        public string TenLoaiSP {  get; set; }

        public Nullable<int> IdLoaiSPCha { get; set; }
        public LoaiSanPhamAM()
        {
            
        }
        public LoaiSanPhamAM(LoaiSanPham lsp)
        {
            IdLoaiSP = lsp.IdLoaiSP;
            TenLoaiSP = lsp.TenLoaiSP;
            IdLoaiSPCha = lsp.IdLoaiSPCha;
        }
    }
}