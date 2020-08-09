using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyCafe.Models
{
    public class NhaCungCapModel
    {
        
        public int MANCC { get; set; }
        [Required(ErrorMessage = "Bạn hãy nhập tên nhà cung cấp!")]
        public string TENNCC { get; set; }
        [Required(ErrorMessage = "Bạn hãy nhập địa chỉ!")]
        
        public string DIACHI { get; set; }
        [Required(ErrorMessage = "Bạn hãy nhập số điện thoại!")]
        [RegularExpression(@"^([0-9]+)", ErrorMessage = "Số điện thoại gồm các kí tự số!")]
        public string DIENTHOAI { get; set; }
        [Required(ErrorMessage = "Nhập Email của bạn!")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email Không đúng!")]

        public string EMAIL { get; set; }
    }
}