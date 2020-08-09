using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyCafe.Models
{
    public class DonViTinhModel
    {
        public int MaDVT { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập Đơn vị tính!")]
        public string TenDVT { get; set; }
    }
}