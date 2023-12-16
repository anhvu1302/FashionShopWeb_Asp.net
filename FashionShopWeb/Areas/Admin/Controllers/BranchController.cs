using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using FashionShopWeb.Areas.Admin.ViewModel;
using FashionShopWeb.Models;
using System.Collections.Generic;
using System;

namespace FashionShopWeb.Areas.Admin.Controllers
{
    public class BranchController : Controller
    {
        // GET: Admin/Branch
        FashionShopDataContext db = new FashionShopDataContext();
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult GetAllBranch(string brSearchType, string brSearchInput, string sortCol, string sortType, int page = 1)
        {
            IEnumerable<BranchVM> query = null;
            
            if (!string.IsNullOrEmpty(brSearchType) && !string.IsNullOrEmpty(brSearchInput))
            {
                query = db.ChiNhanhs
                        .Where(b => b.TenChiNhanh.Contains(brSearchInput))
                        .Select(br => new BranchVM
                        {
                            Branch = br
                        });
            }
            else
            {
                query = from br in db.ChiNhanhs
                        orderby br.IdChiNhanh descending
                        select new BranchVM
                        {
                            Branch = br
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
                if(sortCol == "name")
                {
                    if (sortType == "ASC")
                        query = query.OrderBy(x => x.Branch.TenChiNhanh);
                    else if (sortType == "DESC")
                        query = query.OrderByDescending(x => x.Branch.TenChiNhanh);
                    else return HttpNotFound();
                }
                else
                {
                    return HttpNotFound();
                }
            }
                return View(query);
        }
        [CustomAuthorize("Admin", "Quản lý", "Nhân viên")]
        public ActionResult Search(string brSearchType, string brSearchInput)
        {
            if (!string.IsNullOrEmpty(brSearchInput))
            {
                if(brSearchType == "name")
                {
                    var query = db.ChiNhanhs.Where(b => b.TenChiNhanh.ToString().Contains(brSearchInput));
                }
            }
            return View();
        }
    }
}