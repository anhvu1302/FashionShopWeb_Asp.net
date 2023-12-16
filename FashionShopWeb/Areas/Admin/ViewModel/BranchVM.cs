using FashionShopWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FashionShopWeb.Areas.Admin.ViewModel
{
    public class BranchVM
    {
        public ChiNhanh Branch { get; set; }
        private string name { get; set; }
        private string address { get; set; }
        public BranchVM() { }
        public BranchVM(ChiNhanh br)
        {
            name = br.TenChiNhanh;
            address = br.DiaChi;
        }
    }
}