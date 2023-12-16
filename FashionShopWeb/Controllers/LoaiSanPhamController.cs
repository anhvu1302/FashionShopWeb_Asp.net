using FashionShopWeb.Models;
using FashionShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace FashionShopWeb.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        FashionShopDataContext db = new FashionShopDataContext();
        // GET: LoaiSanPham
        public ActionResult LoaiSanPhamPartial()
        {
            var lspChaList = db.LoaiSanPhamChas.ToList();


            foreach (var item in lspChaList)
            {
                ViewData[item.IdLoaiSPCha.ToString()] = GetLoaiSpByIdLoaiSpCha(item.IdLoaiSPCha);
            }
            return PartialView(lspChaList);
        }
        public List<LoaiSanPham> GetLoaiSpByIdLoaiSpCha(int idLoaiCha)
        {
            var query = from l in db.LoaiSanPhams
                        where l.IdLoaiSPCha == idLoaiCha
                        select l;
            return query.ToList();
        }
    }
}