using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FashionShopWeb.ApiModels;
using FashionShopWeb.Models;

namespace FashionShopWeb.ApiControllers
{
    public class SanPhamController : ApiController
    {
        FashionShopDataContext db = new FashionShopDataContext();
        public List<SanPhamAP> GetList()
        {
            List<SanPhamAP> lst = new List<SanPhamAP>();
            foreach (var item in db.SanPhams.ToList())
            {
                SanPhamAP ap = new SanPhamAP(item);
                lst.Add(ap);
            }
            return lst;
        }
        public SanPhamAP Get(long id)
        {
            SanPhamAP ap = new SanPhamAP(db.SanPhams.Where(x => x.IdSanPham == id).FirstOrDefault());
            return ap;
        }
    }
}
