using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyCafe.Models
{
    public class DonDatHangmodel
    {
        public int SOHOADONDAT { get; set; }
        public int MAKH { get; set; }
        public System.DateTime NGAYMUA { get; set; }
        public System.DateTime NGAYGIAO { get; set; }
        public bool TRANGTHAI { get; set; }
        public long TONGTIEN { get; set; }

        public string tenKH { get; set; }
        public string DIACHI { get; set; }
        public string DIENTHOAI { get; set; }
        public string EMAIL { get; set; }
    }
}