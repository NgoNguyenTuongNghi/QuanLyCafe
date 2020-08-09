using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace QuanLyCafe.Models
{
    public class LoaiHangModel
    {
       
        public int MALH { get; set; }
        [Required(ErrorMessage = "Bạn hãy nhập tên loại hàng!")]
        public string TENLH { get; set; }
    }
}