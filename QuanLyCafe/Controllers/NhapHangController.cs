using QuanLyCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.WebPages;
using Microsoft.SqlServer.Server;

namespace QuanLyCafe.Controllers
{
    public class NhapHangController : Controller
    {
        QLCAFEEntities db = new QLCAFEEntities();
        // GET: NhapHang
        public ActionResult Index(int? page)
        {
            List<CHITIETNHAPHANG> listNhapHang = db.CHITIETNHAPHANGs.ToList();
            List<ChiTietNhapHangModel> listModel = new List<ChiTietNhapHangModel>();
            foreach(var i in listNhapHang)
            {
                ChiTietNhapHangModel model = new ChiTietNhapHangModel();
                model.SOHOADONNHAP = i.SOHOADONNHAP;
                model.MASP = i.MASP;
                model.hinhAnh = i.SANPHAM.HinhAnh;
                model.TenSP = i.SANPHAM.TENSP;
                model.GIA = i.GIA;
                model.SOLUONG = i.SOLUONG;
                model.ngayNhap = i.DONNHAPHANG.NGAYNHAPHANG;
                model.tinhTrang = i.DONNHAPHANG.TINHTRANGDONNHAP;
                model.NCC = i.SANPHAM.NHACUNGCAP.TENNCC;
                listModel.Add(model);
            }
            
            listModel.Reverse();
            if (page == null) page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(listModel.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult TimKiem(string tuNgay,string denNgay,int? page)
        {
            List<CHITIETNHAPHANG> listNhapHang = new List<CHITIETNHAPHANG>();
            DateTime TN = DateTime.Parse(tuNgay);
            DateTime DN = DateTime.Parse(denNgay);
            
            foreach (var i in db.CHITIETNHAPHANGs)
            {
                DateTime ngayNhapHang = i.DONNHAPHANG.NGAYNHAPHANG;
                if(ngayNhapHang >= TN && ngayNhapHang <= DN)
                {
                    listNhapHang.Add(i);
                }
            }

            List<ChiTietNhapHangModel> listModel = new List<ChiTietNhapHangModel>();
            foreach (var i in listNhapHang)
            {
                ChiTietNhapHangModel model = new ChiTietNhapHangModel();
                model.SOHOADONNHAP = i.SOHOADONNHAP;
                model.MASP = i.MASP;
                model.hinhAnh = i.SANPHAM.HinhAnh;
                model.TenSP = i.SANPHAM.TENSP;
                model.GIA = i.GIA;
                model.SOLUONG = i.SOLUONG;
                model.ngayNhap = i.DONNHAPHANG.NGAYNHAPHANG;
                model.tinhTrang = i.DONNHAPHANG.TINHTRANGDONNHAP;
                model.NCC = i.SANPHAM.NHACUNGCAP.TENNCC;
                listModel.Add(model);
            }
            Session["tuNgayN"] = tuNgay;
            Session["denNgayN"] = denNgay;
            listModel.Reverse();
            if (page == null) page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View("Index", listModel.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public JsonResult  tinhTrangThayDoi(int soHoaDon,int  maSP)
        {
            CHITIETNHAPHANG ctnh = db.CHITIETNHAPHANGs.SingleOrDefault(x => x.MASP == maSP && x.SOHOADONNHAP == soHoaDon);
            ctnh.DONNHAPHANG.TINHTRANGDONNHAP = true;
            ctnh.SANPHAM.HANGTON += ctnh.SOLUONG;
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult ThemMoiDonNhap(int? page)
        {
            List<SANPHAM> listSp = db.SANPHAMs.Where(x => x.HANGTON <= 1000 && x.TRANGTHAI==false).ToList();
            List<SanPhamModel> listModel = new List<SanPhamModel>();
            foreach(var i in listSp)
            {
                SanPhamModel sp = new SanPhamModel();
                sp.MASP = i.MASP;
                sp.TENSP = i.TENSP;
                sp.NhaCungCap = i.NHACUNGCAP.TENNCC;
                sp.DonViTinh = i.DONVITINH.TenDVT;
                sp.HANGTON = i.HANGTON;
                sp.HinhAnh = i.HinhAnh;
                sp.GIA = i.GIA;
                listModel.Add(sp);
            }
            if (page == null) page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(listModel.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Nhap(int id)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(x => x.MASP == id);
            ChiTietNhapHangModel model = new ChiTietNhapHangModel();
            model.MASP = id;
            model.TenSP = sp.TENSP;
            model.tinhTrang = false;
            model.SOLUONG = 50;
            model.NCC = sp.NHACUNGCAP.TENNCC;
            return PartialView("Nhap", model);
        }
        [HttpPost]
        public JsonResult XacNhan(ChiTietNhapHangModel model)
        {
            
            DONNHAPHANG dnh = new DONNHAPHANG();
            CHITIETNHAPHANG ctnh = new CHITIETNHAPHANG();
            if (ModelState.IsValid)
            {
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dnh.NGAYNHAPHANG = Convert.ToDateTime(dateTime);
                dnh.TINHTRANGDONNHAP = model.tinhTrang;
                db.DONNHAPHANGs.Add(dnh);
                db.SaveChanges();
                DONNHAPHANG tam = db.DONNHAPHANGs.OrderByDescending(x=>x.SOHOADONNHAP).ToList()[0];
                ctnh.SOHOADONNHAP = tam.SOHOADONNHAP;
                ctnh.MASP = model.MASP;
                ctnh.GIA = model.GIA;
                ctnh.SOLUONG = model.SOLUONG;
                db.CHITIETNHAPHANGs.Add(ctnh);
                db.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}