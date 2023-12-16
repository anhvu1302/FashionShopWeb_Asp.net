using FashionShopWeb.ApiModels;
using FashionShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FashionShopWeb.ApiControllers
{
    public class LoaiSanPhamController : ApiController
    {
        FashionShopDataContext db = new FashionShopDataContext();
        [HttpGet]
        public List<LoaiSanPhamAM> GetList()
        {
            List<LoaiSanPhamAM> lst = new List<LoaiSanPhamAM>();
            foreach (var item in db.LoaiSanPhams.ToList())
            {
                LoaiSanPhamAM lsp = new LoaiSanPhamAM(item);
                lst.Add(lsp);
            }
            return lst;
        }
        [HttpGet]
        public LoaiSanPhamAM GetDetails(long id)
        {
            LoaiSanPhamAM lsp = new LoaiSanPhamAM(db.LoaiSanPhams.Where(x => x.IdLoaiSP == id).FirstOrDefault());
            return lsp;
        }
        [HttpPost]
        public bool Add(string TenLoaiSP, int IdLoaiSPCha)
        {
            string tem1 = TenLoaiSP;
            int tem2 = IdLoaiSPCha;
            try
            {
                LoaiSanPham lsp = new LoaiSanPham()
                {
                    TenLoaiSP = TenLoaiSP,
                    IdLoaiSPCha = IdLoaiSPCha
                };
                db.LoaiSanPhams.InsertOnSubmit(lsp);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut]
        public bool Update(int IdLoaiSP, string TenLoaiSP, int IdLoaiSPCha)
        {
            var lsp = db.LoaiSanPhams.FirstOrDefault(x => x.IdLoaiSP.Equals(IdLoaiSP));

            if (lsp == null)
                return false;

            try
            {
                lsp.TenLoaiSP = TenLoaiSP;
                lsp.IdLoaiSPCha = IdLoaiSPCha;

                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpDelete]
        public bool Delete(long id)
        {
            var lsp = db.LoaiSanPhams.FirstOrDefault(x => x.IdLoaiSP.Equals(id));
            if (lsp == null)
                return false;

            try
            {
                db.LoaiSanPhams.DeleteOnSubmit(lsp);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
