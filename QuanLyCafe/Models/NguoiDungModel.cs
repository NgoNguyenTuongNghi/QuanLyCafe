using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyCafe.Models
{
    public class NguoiDungModel
    {
        
        public string MAND { get; set; }

        [Required(ErrorMessage = "Nhập Mật khẩu mới của bạn!")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Mật khẩu ít nhất 8 kí tự gồm chữ và số!")]
        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "Nhập lại Mật khẩu của bạn!")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Mật khẩu ít nhất 8 kí tự gồm chữ và số!")]
        public string REPASSWORD { get; set; }

        [Required(ErrorMessage ="Nhập Tên của bạn!")]
        

        public string TENNV { get; set; }

        [Required(ErrorMessage = "Nhập Email của bạn!")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email Không đúng!")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Nhập Đia chỉ của bạn!")]
        public string DIACHI { get; set; }
        public short MAQUYEN { get; set; }
        public string QUYEN { get; set; }

        [Required(ErrorMessage = "Nhập lại Mật khẩu mới của bạn!")]
        [RegularExpression(@"^([a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9]+)", ErrorMessage = "Mật khẩu ít nhất 3 kí tự!")]
        public string PASSWỎDOLD { get; set; }
    }
}