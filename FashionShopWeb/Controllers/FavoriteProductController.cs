using FashionShopWeb.Models;
using FashionShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShopWeb.Controllers
{
    public class FavoriteProductController : Controller
    {
        FashionShopDataContext db = new FashionShopDataContext();
        string sessionFavoriteName = "SessionFavoriteProduct";
        public ActionResult FavoriteProductPartial()
        {
            List<FavoriteProductPartialVM> favoritesList = GetFavoritesList();
            ViewBag.Total = Total();
            return PartialView(favoritesList);
        }
        public ActionResult TotalPartial()
        {
            ViewBag.Total = Total();
            return PartialView();
        }
        public List<FavoriteProductPartialVM> GetFavoritesList()
        {
            List<FavoriteProductPartialVM> favoritesList = Session[sessionFavoriteName] as List<FavoriteProductPartialVM>;
            if (favoritesList == null)
            {
                favoritesList = new List<FavoriteProductPartialVM>();
                Session[sessionFavoriteName] = favoritesList;
            }
            return favoritesList;
        }
        private int Total()
        {
            int total = 0;
            List<FavoriteProductPartialVM> favoritesList = GetFavoritesList();
            if (favoritesList != null)
            {
                total = favoritesList.Count;
            }
            return total;
        }

        [HttpPost]
        public JsonResult Add(int idSanPham)
        {
            List<FavoriteProductPartialVM> favoritesList = GetFavoritesList();

            if (favoritesList == null)
            {
                favoritesList = new List<FavoriteProductPartialVM>();
            }

            if (favoritesList.Any(x => x.IdSanPham == idSanPham))
            {
                return Json(new
                {
                    status = false,
                    message = "Bạn đã thích sản phẩm này!"
                });
            }
            else
            {
                FavoriteProductPartialVM newItem = new FavoriteProductPartialVM(idSanPham);
                favoritesList.Add(newItem);
            }

            Session[sessionFavoriteName] = favoritesList;

            return Json(new
            {
                status = true,
                message = "Đã thêm vào danh sách sản phẩm yêu thích!"
            });
        }


        [HttpPost]
        public JsonResult Delete(int idSanPham)
        {
            List<FavoriteProductPartialVM> favoritesList = GetFavoritesList();
            favoritesList.RemoveAll(x => x.IdSanPham == idSanPham);
            Session[sessionFavoriteName] = favoritesList;
            return Json(new
            {
                status = true
            });
        }
    }
}