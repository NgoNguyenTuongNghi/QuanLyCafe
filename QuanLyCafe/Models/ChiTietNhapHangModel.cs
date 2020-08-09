using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyCafe.Models
{
    public class ChiTietNhapHangModel
    {
        public int SOHOADONNHAP { get; set; }
        public int MASP { get; set; }
        public int SOLUONG { get; set; }
        public long GIA { get; set; }

        public string hinhAnh { get; set; }

        public string TenSP { get; set; }
        public string NCC { get; set; }

        public DateTime ngayNhap { get; set; }

        public bool tinhTrang { get; set; }
    }
}