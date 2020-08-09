using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyCafe.Models
{
   
    public class SanPhamModel
    {
        
        public int MASP { get; set; }
        [Required(ErrorMessage ="Bạn hãy nhập tên Sản phẩm!")]
        public string TENSP { get; set; }
        public int HANGTON { get; set; }
        
        public string HinhAnh { get; set; }
        [Required(ErrorMessage = "Bạn hãy nhập giá!")]
        [RegularExpression(@"^([1-9][0-9]+[0]{2})", ErrorMessage = "Giá chỉ chứa các chữ số và lớn hơn 1000VNĐ!")]
       
        public long GIA { get; set; }
        [Required(ErrorMessage = "Bạn hãy lựa chọn loại hàng!")]
        public int MALH { get; set; }
        [Required(ErrorMessage = "Bạn hãy lựa chọn nhà cung cấp!")]
        public int MANCC { get; set; }
        [Required(ErrorMessage = "Bạn hãy lựa chọn đơn vị tính!")]
        public int MaDVT { get; set; }

        public string LoaiHang { get; set; }
        public string DonViTinh { get; set; }
        public string NhaCungCap { get; set; }
       
        public HttpPostedFileWrapper ImageFile { get; set; }

        public int soLuongBan { get; set; }
    }
}