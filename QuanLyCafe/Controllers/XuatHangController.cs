using PagedList;
using QuanLyCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace QuanLyCafe.Controllers
{
    public class XuatHangController : Controller
    {
        QLCAFEEntities db = new QLCAFEEntities();
        
         // GET: XuatHang
        public ActionResult Index(int? page)
        {
            
            List<SANPHAM> listSP = db.SANPHAMs.Where(x=>x.HANGTON >0).ToList();

            List<SanPhamModel> listSPModel = new List<SanPhamModel>();
            Session["Select"] = 0;
            Session["TimKiem"] = "";
            
            foreach (var i in listSP)
            {
                SanPhamModel sp = new SanPhamModel();
                sp.MASP = i.MASP;
                sp.TENSP = i.TENSP;
                sp.MANCC = i.MANCC;
                sp.MALH = i.MALH;
                sp.HinhAnh = i.HinhAnh;
                sp.GIA = i.GIA;
                sp.MaDVT = i.MaDVT;
                sp.HANGTON = i.HANGTON;
                sp.NhaCungCap = i.NHACUNGCAP.TENNCC;
                sp.LoaiHang = i.LOAIHANG.TENLH;
                sp.DonViTinh = i.DONVITINH.TenDVT;
                listSPModel.Add(sp);
            }
            listSPModel.Reverse();      
            
            if (page == null) page = 1;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(listSPModel.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Index1(int select, string timKiem, int? page)
        {
            List<SANPHAM> listSP = db.SANPHAMs.Where(x => x.TENSP.Contains(timKiem) && x.HANGTON > 0).ToList();

            List<SanPhamModel> listSPModel = new List<SanPhamModel>();
            foreach (var i in listSP)
            {
                SanPhamModel sp = new SanPhamModel();
                sp.MASP = i.MASP;
                sp.TENSP = i.TENSP;
                sp.MANCC = i.MANCC;
                sp.MALH = i.MALH;
                sp.HinhAnh = i.HinhAnh;
                sp.GIA = i.GIA;
                sp.MaDVT = i.MaDVT;
                sp.HANGTON = i.HANGTON;
                sp.NhaCungCap = i.NHACUNGCAP.TENNCC;
                sp.LoaiHang = i.LOAIHANG.TENLH;
                sp.DonViTinh = i.DONVITINH.TenDVT;
                listSPModel.Add(sp);
            }
            Session["Select"] = 0;
            Session["TimKiem"] = timKiem;
            switch (select)
            {
                case 1:
                    {
                        Session["Select"] = 1;
                        listSPModel = listSPModel.OrderBy(x => x.TENSP).ToList();
                        break;
                    }
                case 2:
                    {
                        listSPModel = listSPModel.OrderBy(x => x.TENSP).ToList();
                        Session["Select"] = 2;
                        listSPModel.Reverse();
                        break;
                    }
                case 3:
                    {
                        listSPModel = listSPModel.OrderBy(x => x.GIA).ThenBy(x => x.TENSP).ToList();
                        Session["Select"] = 3;
                        break;
                    }
                case 4:
                    {
                        listSPModel = listSPModel.OrderBy(x => x.GIA).ThenBy(x => x.TENSP).ToList();
                        Session["Select"] = 4;
                        listSPModel.Reverse();
                        break;
                    }
                default:
                    {
                        Session["Select"] = 0;                       
                        break;
                    }
            }
            if (page == null) page = 1;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View("Index", listSPModel.ToPagedList(pageNumber, pageSize));
        }
        

        public ActionResult GioHang(string IDs)
        {
            List<KHACHHANG> listKH = db.KHACHHANGs.ToList();
            ViewBag.listKH = new SelectList(listKH,"MAKH", "TENKH");

            TempData["list"] = getList(IDs);
            Session["IDs"] = IDs;
            return View();
        }
        [HttpGet]
        public JsonResult KhachHang(int? ID)
        {
            if (ID != null)
            {                
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(x => x.MAKH == ID);
                List<String> list = new List<string>();
                list.Add(kh.DIACHI);
                list.Add(kh.DIENTHOAI);
                list.Add(kh.EMAIL);
                return Json(list,JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);           
        }
        [HttpPost]
        public JsonResult HuySanPham(int ID)
        {
            string IDs = Session["IDs"].ToString();

            IDs = IDs.Replace(ID + "-", "");
            Session["IDs"] = IDs;

            return Json("",JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReLoadGioHang()
        {
            List<KHACHHANG> listKH = db.KHACHHANGs.ToList();
            ViewBag.listKH = new SelectList(listKH, "MAKH", "TENKH");

            string IDs = Session["IDs"].ToString();
            TempData["list"] = getList(IDs);
            return View("GioHang");
        }
        [HttpPost]
        public JsonResult ThanhToan(int maKH,long thanhtien, SanPhamModel[] itemlist)
        {
            DONDATHANG ddh = new DONDATHANG();
            ddh.MAKH = maKH;
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ddh.NGAYMUA = Convert.ToDateTime(dateTime);
            ddh.TONGTIEN = thanhtien;
            ddh.NGAYGIAO = Convert.ToDateTime(dateTime);
            ddh.TRANGTHAI = false;
            db.DONDATHANGs.Add(ddh);
            db.SaveChanges();
            Session["IDs"] = "";
            int soHoaDonDatNew = db.DONDATHANGs.OrderByDescending(x => x.SOHOADONDAT).ToList()[0].SOHOADONDAT;

            foreach (SanPhamModel i in itemlist)
            {
                CHITIETDATHANG ctdh = new CHITIETDATHANG();
                ctdh.MASP = i.MASP;
                ctdh.SOHOADONDAT = soHoaDonDatNew;
                ctdh.SOLUONG = i.soLuongBan;
                ctdh.GIA = i.GIA;
                db.CHITIETDATHANGs.Add(ctdh);
                db.SaveChanges();
            }
            return Json("Ok");
        }
        private List<SanPhamModel> getList(string IDs)
        {
            List<SanPhamModel> list = new List<SanPhamModel>();
            if (IDs != "")
            {
                string[] chuoiID = IDs.Split('-');
                
                foreach (string s in chuoiID)
                {

                    if (s != "")
                    {
                        int id = Int32.Parse(s);
                        SANPHAM sp = db.SANPHAMs.SingleOrDefault(x => x.MASP == id);
                        SanPhamModel model = new SanPhamModel();
                        model.MASP = sp.MASP;
                        model.TENSP = sp.TENSP;
                        model.GIA = sp.GIA;
                        model.HinhAnh = sp.HinhAnh;
                        model.DonViTinh = sp.DONVITINH.TenDVT;
                        model.HANGTON = sp.HANGTON;
                        list.Add(model);
                    }
                }
            }
            
            
            return list;
            
        }

    }
}