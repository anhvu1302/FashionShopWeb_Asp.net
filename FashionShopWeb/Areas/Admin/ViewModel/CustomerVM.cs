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
    public class CustomerVM
    {
        public KhachHang customer { get; set; }
        public long id;
        public long idUser;
        public string name;
        public DateTime birth;
        public string gender;
        public string address;
        public string phoneNumber;
        public string email;
        public CustomerVM()
        {

        }
        public CustomerVM(CustomerVM cus)
        {
            id = cus.id;
            idUser = cus.idUser;
            name = cus.name;
            birth = cus.birth;
            gender = cus.gender;
            address = cus.address;
            phoneNumber = cus.phoneNumber;
            email = cus.email;
        }
    }
}