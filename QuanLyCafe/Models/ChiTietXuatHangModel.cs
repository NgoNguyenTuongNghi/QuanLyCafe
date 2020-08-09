using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyCafe.Models
{
    public class ChiTietXuatHangModel
    {
        public int SOHOADONDAT { get; set; }
        public int MASP { get; set; }
        public int SOLUONG { get; set; }

        public string TENSP { get; set; }
        public string DonViTinh { get; set; }
        public long GIA { get; set; }
    }
}