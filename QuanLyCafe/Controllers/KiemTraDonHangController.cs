using PagedList;
using QuanLyCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCafe.Controllers
{
    public class KiemTraDonHangController : Controller
    {
        QuanLyCafe.Models.QLCAFEEntities db = new Models.QLCAFEEntities();
        // GET: KiemTraDonHang
        public ActionResult Index(int? page)
        {
            List<DONDATHANG> listDDH = db.DONDATHANGs.ToList();
            List<DonDatHangmodel> listModel = new List<DonDatHangmodel>();

            foreach(var i in listDDH)
            {
                DonDatHangmodel model = new DonDatHangmodel();
                model.SOHOADONDAT = i.SOHOADONDAT;
                model.tenKH = i.KHACHHANG.TENKH;
                model.TONGTIEN = i.TONGTIEN;
                model.NGAYMUA = i.NGAYMUA;
                model.TRANGTHAI = i.TRANGTHAI;
                model.NGAYGIAO = i.NGAYGIAO;
                listModel.Add(model);
            }
            listModel.Reverse();
            if (page == null) page = 1;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(listModel.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public JsonResult tinhTrangThayDoi(int soHoaDonDat)
        {
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(x => x.SOHOADONDAT == soHoaDonDat);
            ddh.TRANGTHAI = true;
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ddh.NGAYGIAO = Convert.ToDateTime(dateTime);

            List<CHITIETDATHANG> listChiTietDatHang = db.CHITIETDATHANGs.Where(x => x.SOHOADONDAT == soHoaDonDat).ToList();
            foreach(var i in listChiTietDatHang)
            {
                SANPHAM sp = db.SANPHAMs.SingleOrDefault(x => x.MASP == i.MASP);
                sp.HANGTON = sp.HANGTON - i.SOLUONG;
            }
            db.SaveChanges();

            return Json("");
        }
        [HttpPost]
        public JsonResult HuyDon(int soHoaDonDat)
        {
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(x => x.SOHOADONDAT == soHoaDonDat);
            

            List<CHITIETDATHANG> listChiTietDatHang = db.CHITIETDATHANGs.Where(x => x.SOHOADONDAT == soHoaDonDat).ToList();
            foreach (var i in listChiTietDatHang)
            {
                db.CHITIETDATHANGs.Remove(i);
            }
            db.DONDATHANGs.Remove(ddh);
            db.SaveChanges();


            return Json("");
        }
        public ActionResult XemChiTiet(int soHoaDonDat)
        {
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(x => x.SOHOADONDAT == soHoaDonDat);
            DonDatHangmodel model = new DonDatHangmodel();
            model.tenKH = ddh.KHACHHANG.TENKH;
            model.DIACHI = ddh.KHACHHANG.DIACHI;
            model.DIENTHOAI = ddh.KHACHHANG.DIENTHOAI;
            model.EMAIL = ddh.KHACHHANG.EMAIL;
            model.TONGTIEN = ddh.TONGTIEN;
            model.NGAYMUA = ddh.NGAYMUA;
            model.TRANGTHAI = ddh.TRANGTHAI;
            model.NGAYGIAO = ddh.NGAYGIAO;

            List<CHITIETDATHANG> cHITIETDATHANGs = db.CHITIETDATHANGs.Where(x => x.SOHOADONDAT == soHoaDonDat).ToList();
            List<ChiTietXuatHangModel> listctxh = new List<ChiTietXuatHangModel>();
            foreach(var i in cHITIETDATHANGs)
            {
                ChiTietXuatHangModel ctxh = new ChiTietXuatHangModel();
                ctxh.DonViTinh = i.SANPHAM.DONVITINH.TenDVT;
                ctxh.TENSP = i.SANPHAM.TENSP;
                ctxh.SOLUONG = i.SOLUONG;
                ctxh.GIA = i.GIA;
                listctxh.Add(ctxh);
            }
            TempData["list"] = listctxh;
            return View(model);
        }
    }

    
}