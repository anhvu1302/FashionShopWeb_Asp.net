using FashionShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace FashionShopWeb.Controllers
{
    public class ChatBotController : Controller
    {
        FashionShopDataContext db = new FashionShopDataContext();
        private readonly ShoppingCartController _shoppingCartController;

        // GET: BotChat
        public ActionResult ChatBotPartial()
        {
            return PartialView();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult VoiceProcessing(string transcript)
        {
            string d = transcript;
            if (transcript.ToLower().Contains("thêm"))
            {
                int index = transcript.ToLower().IndexOf("thêm");
                string productName = transcript.Substring(index + "thêm".Length).Trim();
                long idProduct = GetIdProductByName(productName);
                return Json(new { success = true, message = "Thêm sản phẩm thành công",typeAction = "Them", idProduct = idProduct });
            }
            if (transcript.ToLower().Contains("xóa giỏ hàng"))
            {
                return Json(new { success = true, message = "Xóa sản phẩm thành công", typeAction = "XoaGioHang" });
            } else if (transcript.ToLower().Contains("xóa"))
            {
                int index = transcript.ToLower().IndexOf("xóa");
                string productName = transcript.Substring(index + "xóa".Length).Trim();
                long idProduct = GetIdProductByName(productName);
                return Json(new { success = true, message = "Xóa sản phẩm thành công", typeAction = "Xoa", idProduct = idProduct });
            }
            if (transcript.ToLower().Contains("thanh toán"))
            {
                return Json(new { success = true, message = "Thanh toán", typeAction = "ThanhToan" });
            }




            return Json(new { success = true, message = "Vui lòng đọc đúng cú pháp" });
        }

        public long GetIdProductByName(string productName)
        {
            var query = from sanPham in db.SanPhams
                         where sanPham.TenSanPham.Contains("áo sơ mi nam Oxford")
                         select sanPham.IdSanPham;

           return query.FirstOrDefault();
        }
    }
}