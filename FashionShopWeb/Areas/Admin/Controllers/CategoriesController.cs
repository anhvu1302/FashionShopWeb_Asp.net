using FashionShopWeb.Areas.Admin.ViewModels;
using FashionShopWeb.Models;
using FashionShopWeb.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;

namespace FashionShopWeb.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        FashionShopDataContext db = new FashionShopDataContext();

        // GET: Categories
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult GetAllCate(string cateSearchType, string cateSearchInput, string sortCol, string sortType, int page = 1)
        {
            IEnumerable<CategoriesVM> query = null;
            if(!string.IsNullOrEmpty(cateSearchType) && !string.IsNullOrEmpty(cateSearchInput))
            {
                query = db.LoaiSanPhams
                        .Where(c => c.TenLoaiSP.Contains(cateSearchInput))
                        .Select(lsp => new CategoriesVM
                        {
                            Category = lsp,
                           
                        });
            }
            else
            {
                query = from lsp in db.LoaiSanPhams
                        orderby lsp.IdLoaiSP descending
                        select new CategoriesVM
                        {
                            Category = lsp,
                        };
            }
            int NoOfRecordPerPage = 12;
            int NoOfPages = (int)Math.Ceiling((double)query.Count() / NoOfRecordPerPage);
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.STT = (page - 1) * NoOfRecordPerPage + 1;
            ViewBag.NoOfPages = NoOfPages;
            query = query.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage);
            if (!string.IsNullOrEmpty(sortCol) && !string.IsNullOrEmpty(sortType))
            {
                switch (sortCol)
                {
                    case "nameCategory":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.TenLoaiSP);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.TenLoaiSP);
                        else return HttpNotFound();
                        break;
                    case "nameParentCategory":
                        if (sortType == "ASC")
                            query = query.OrderBy(x => x.TenLoaiSPCha);
                        else if (sortType == "DESC")
                            query = query.OrderByDescending(x => x.TenLoaiSPCha);
                        else return HttpNotFound();
                        break;
                    default:
                        return HttpNotFound();
                }
            }
            return View(query);
        }
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult Search(string cateSearchType, string cateSearchInput)
        {
            if (!string.IsNullOrEmpty(cateSearchInput))
            {
                if(cateSearchType == "id")
                {
                    var query = db.LoaiSanPhams.Where(c => c.IdLoaiSP.ToString().Contains(cateSearchInput));
                }else if(cateSearchType == "name")
                {
                    var query = db.LoaiSanPhams.Where(c => c.TenLoaiSP.Contains(cateSearchInput));
                }
            }
            return View();
        }
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult GetLoaiSanPham(int parentId)
        {
            // Retrieve child types based on the selected parent type (parentId)
            var childTypes = db.LoaiSanPhams.Where(pt => pt.IdLoaiSPCha == parentId).ToList();

            var childItems = childTypes.Select(pt => new SelectListItem
            {
                Value = pt.IdLoaiSP.ToString(),
                Text = pt.TenLoaiSP.ToString()
            });

            return Json(childItems, JsonRequestBehavior.AllowGet);
        }
        
    }
}