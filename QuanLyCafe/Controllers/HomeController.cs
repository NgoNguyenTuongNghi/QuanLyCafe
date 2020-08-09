using PagedList;
using QuanLyCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCafe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            List<SANPHAM> listSP = db.SANPHAMs.Where(x => x.TRANGTHAI == false).ToList();

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
            return View(listSPModel.ToPagedList(pageNumber,pageSize));
        }
        
        public ActionResult Index1(int select,string timKiem, int? page)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            List<SANPHAM> listSP = db.SANPHAMs.Where(x=>x.TENSP.Contains(timKiem) && x.TRANGTHAI==false).ToList();

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
                        listSPModel = listSPModel.OrderBy(x => x.GIA).ThenBy(x=>x.TENSP).ToList();
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
                        listSPModel.Reverse();
                        break;
                    }
            }
            if (page == null) page = 1;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View("Index", listSPModel.ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult CapNhatThongTin(string ID)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.SingleOrDefault(x => x.MAND == ID);

            NguoiDungModel model = new NguoiDungModel();

            if (nGUOIDUNG != null)
            {
                model.MAND = nGUOIDUNG.MAND;
                model.TENNV = nGUOIDUNG.TENNV;
                model.DIACHI = nGUOIDUNG.DIACHI;
                model.EMAIL = nGUOIDUNG.EMAIL;
                model.MAQUYEN = nGUOIDUNG.MAQUYEN;
                model.QUYEN = nGUOIDUNG.QUYEN.QUYEN1;
                model.PASSWORD = nGUOIDUNG.PASSWORD;
            }
           

            return PartialView("CapNhatThongTin",model);
        }
        public ActionResult DoiMatKhau(string ID)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.SingleOrDefault(x => x.MAND == ID);

            NguoiDungModel model = new NguoiDungModel();

            if (nGUOIDUNG != null)
            {
                model.MAND = nGUOIDUNG.MAND;
                model.TENNV = nGUOIDUNG.TENNV;
                model.DIACHI = nGUOIDUNG.DIACHI;
                model.EMAIL = nGUOIDUNG.EMAIL;
                model.MAQUYEN = nGUOIDUNG.MAQUYEN;
            }


            return PartialView("DoiMatKhau", model);
        }
        [HttpPost]
        public JsonResult CapNhatMatKhau(NguoiDungModel model)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            bool result = false;
            

            if(model.PASSWORD == model.REPASSWORD)
            {
                
                NGUOIDUNG nv = db.NGUOIDUNGs.SingleOrDefault(x => x.MAND == model.MAND && x.PASSWORD == model.PASSWỎDOLD);
                if (nv != null)
                {
                    result = true;
                    nv.PASSWORD = model.PASSWORD;
                    db.SaveChanges();
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        
        public JsonResult CapNhatThongTin(NguoiDungModel model)
        {
            QLCAFEEntities db = new QLCAFEEntities();

            if (model.DIACHI!=null && model.EMAIL!=null && model.TENNV!=null)
            {   
                NGUOIDUNG nv = db.NGUOIDUNGs.SingleOrDefault(x => x.MAND == model.MAND);
                nv.TENNV = model.TENNV;
                nv.DIACHI = model.DIACHI;
                nv.EMAIL = model.EMAIL;
                nv.MAQUYEN = model.MAQUYEN;
                nv.PASSWORD = model.PASSWORD;
                db.SaveChanges();
                Session["TenNV"] = nv.TENNV;
            }
           
            
            return Json("",JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEditSanPham(int ID)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            SanPhamModel model = new SanPhamModel();
            List<NHACUNGCAP> listNCC = db.NHACUNGCAPs.ToList();
            ViewBag.listNCC = new SelectList(listNCC, "MANCC", "TENNCC");

            List<DONVITINH> listDVT = db.DONVITINHs.ToList();
            ViewBag.listDVT = new SelectList(listDVT, "MaDVT", "TenDVT");

            List<LOAIHANG> listLH = db.LOAIHANGs.ToList();
            ViewBag.listLH = new SelectList(listLH, "MALH", "TENLH");
            if (ID > 0)
            {
                SANPHAM sp = db.SANPHAMs.SingleOrDefault(x => x.MASP == ID);
                model.MASP = ID;
                model.TENSP = sp.TENSP;
                model.MANCC = sp.MANCC;
                model.MALH = sp.MALH;
                model.GIA = sp.GIA;
                model.MaDVT = sp.MaDVT;
                model.HANGTON = sp.HANGTON;
                model.HinhAnh = sp.HinhAnh;
                return PartialView("EditSanPham", model);
            }
            else
            {
                return PartialView("AddSanPham", model);
            }

            
        }
        [HttpPost]
        public JsonResult AddEditSanPham(SanPhamModel model)
        {
            
            QLCAFEEntities db = new QLCAFEEntities();
            bool result = false;
            var file = model.ImageFile;
            if (ModelState.IsValid)
            {
                if (model.MASP == 0)
                {
                    SANPHAM sANPHAM = db.SANPHAMs.SingleOrDefault(x => x.TENSP == model.TENSP);

                    if (sANPHAM == null && file != null)
                    {
                        SANPHAM sp = new SANPHAM();
                        sp.TENSP = model.TENSP;
                        sp.MANCC = model.MANCC;
                        sp.MaDVT = model.MaDVT;
                        sp.GIA = model.GIA;
                        sp.HANGTON = model.HANGTON;
                        sp.HinhAnh = "/Image/" + file.FileName;
                        sp.MALH = model.MALH;
                        sp.TRANGTHAI = false;
                        db.SANPHAMs.Add(sp);
                        db.SaveChanges();

                        file.SaveAs(Server.MapPath("/Image/" + file.FileName));
                        result = true;
                    }
                }                          
            }
            if (model.MASP != 0)
            {               
                SANPHAM sp = db.SANPHAMs.SingleOrDefault(x => x.MASP == model.MASP);
                sp.TENSP = model.TENSP;
                sp.MANCC = model.MANCC;
                sp.MaDVT = model.MaDVT;
                sp.GIA = model.GIA;
                sp.HANGTON = model.HANGTON;
                if (file!=null)
                {
                    sp.HinhAnh = "/Image/" + file.FileName;
                    file.SaveAs(Server.MapPath("/Image/" + file.FileName));
                }
                else sp.HinhAnh = model.HinhAnh;
                sp.MALH = model.MALH;
                db.SaveChanges();
                result = true;
            }
            return Json(result,JsonRequestBehavior.AllowGet);

        }


        public ActionResult ThemLH()
        {            
            LoaiHangModel model = new LoaiHangModel();         
            return PartialView("ThemLH", model);
        }
        public ActionResult ThemNCC()
        {
            NhaCungCapModel model = new NhaCungCapModel();
            return PartialView("ThemNCC", model);
        }

        public ActionResult ThemDVT()
        {
            DonViTinhModel model = new DonViTinhModel();
            return PartialView("ThemDVT", model);
        }

        public PartialViewResult SuaLH()
        {
            QLCAFEEntities db = new QLCAFEEntities();
            List<LOAIHANG> listLoaiHang = db.LOAIHANGs.ToList();
            List<LoaiHangModel> listLHModel = new List<LoaiHangModel>();
            foreach (var i in listLoaiHang)
            {
                LoaiHangModel lh = new LoaiHangModel();
                lh.MALH = i.MALH;
                lh.TENLH = i.TENLH;
                listLHModel.Add(lh);
            }
           
            return PartialView("SuaLH", listLHModel);
        }
        public ActionResult SuaLHEx(int ID)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            LoaiHangModel model = new LoaiHangModel();

            LOAIHANG lh = db.LOAIHANGs.SingleOrDefault(x => x.MALH == ID);
            model.MALH = lh.MALH;
            model.TENLH = lh.TENLH;
            return PartialView("SuaLHEx", model);
        }
        [HttpPost]
        public JsonResult CapNhatLHEx(LoaiHangModel model)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            if (ModelState.IsValid)
            {
                LOAIHANG lh = db.LOAIHANGs.SingleOrDefault(x => x.MALH == model.MALH);
                lh.TENLH = model.TENLH;
                db.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SuaDVT()
        {
            QLCAFEEntities db = new QLCAFEEntities();
            List<DONVITINH> listDVT = db.DONVITINHs.ToList();
            List<DonViTinhModel> listDVTModel = new List<DonViTinhModel>();
            foreach (var i in listDVT)
            {
                DonViTinhModel dvt = new DonViTinhModel();
                dvt.MaDVT = i.MaDVT;
                dvt.TenDVT = i.TenDVT;
                listDVTModel.Add(dvt);
            }

            return PartialView("SuaDVT", listDVTModel);
        }
        public ActionResult SuaDVTEx(int ID)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            DonViTinhModel model = new DonViTinhModel();

            DONVITINH dvt = db.DONVITINHs.SingleOrDefault(x => x.MaDVT == ID);
            model.MaDVT = dvt.MaDVT;
            model.TenDVT = dvt.TenDVT;
            return PartialView("SuaDVTEx", model);
        }
        [HttpPost]
        public JsonResult CapNhatDVTEx(DonViTinhModel model)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            if (ModelState.IsValid)
            {
                DONVITINH dvt = db.DONVITINHs.SingleOrDefault(x => x.MaDVT == model.MaDVT);
                dvt.TenDVT = model.TenDVT;
                db.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SuaNCC()
        {
            QLCAFEEntities db = new QLCAFEEntities();
            List<NHACUNGCAP> listNCC = db.NHACUNGCAPs.ToList();
            List<NhaCungCapModel> listNCCModel = new List<NhaCungCapModel>();
            foreach (var i in listNCC)
            {
                NhaCungCapModel ncc = new NhaCungCapModel();
                ncc.MANCC = i.MANCC;
                ncc.TENNCC = i.TENNCC;
                listNCCModel.Add(ncc);
            }

            return PartialView("SuaNCC", listNCCModel);
        }
        public ActionResult SuaNCCEx(int ID)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            NhaCungCapModel model = new NhaCungCapModel();

            NHACUNGCAP ncc = db.NHACUNGCAPs.SingleOrDefault(x => x.MANCC == ID);
            model.MANCC = ncc.MANCC;
            model.TENNCC = ncc.TENNCC;
            model.DIACHI = ncc.DIACHI;
            model.DIENTHOAI = ncc.DIENTHOAI;
            model.EMAIL = ncc.EMAIL;
            return PartialView("SuaNCCEx", model);
        }
        [HttpPost]
        public JsonResult CapNhatNCCEx(NhaCungCapModel model)
        {
            QLCAFEEntities db = new QLCAFEEntities();
            if (ModelState.IsValid)
            {
                NHACUNGCAP ncc = db.NHACUNGCAPs.SingleOrDefault(x => x.MANCC == model.MANCC);
                ncc.TENNCC = model.TENNCC;
                ncc.DIACHI = model.DIACHI;
                ncc.EMAIL = model.EMAIL;
                ncc.DIENTHOAI = model.DIENTHOAI;
                db.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostThemDVT(DonViTinhModel model)
        {
            bool result = false;
            QLCAFEEntities db = new QLCAFEEntities();
            DONVITINH dONVITINH = new DONVITINH();

            if(model.TenDVT != null)
            {
                DONVITINH dvt = db.DONVITINHs.SingleOrDefault(x => x.TenDVT == (model.TenDVT));
                if(dvt == null)
                {
                    
                    result = true;
                    dONVITINH.TenDVT = model.TenDVT;
                    db.DONVITINHs.Add(dONVITINH);
                    db.SaveChanges();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult PostThemNCC(NhaCungCapModel model)
        {
            bool result = false;
            QLCAFEEntities db = new QLCAFEEntities();
            NHACUNGCAP nHACUNGCAP = new NHACUNGCAP();

            if (ModelState.IsValid)
            {
                NHACUNGCAP ncc = db.NHACUNGCAPs.SingleOrDefault(x => x.MANCC == (model.MANCC));
                if (ncc == null)
                {
                    
                    result = true;
                    nHACUNGCAP.DIACHI = model.DIACHI;
                    nHACUNGCAP.DIENTHOAI = model.DIENTHOAI;
                    nHACUNGCAP.EMAIL = model.EMAIL;
                    nHACUNGCAP.TENNCC = model.TENNCC;                   
                    db.NHACUNGCAPs.Add(nHACUNGCAP);
                    db.SaveChanges();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult PostThemLH(LoaiHangModel model)
        {
            bool result = false;
            QLCAFEEntities db = new QLCAFEEntities();
            LOAIHANG lOAIHANG = new LOAIHANG();

            if (ModelState.IsValid)
            {
                LOAIHANG lh = db.LOAIHANGs.SingleOrDefault(x => x.MALH == (model.MALH));
                if (lh == null)
                {
                    result = true;                   
                    lOAIHANG.TENLH = model.TENLH;
                    db.LOAIHANGs.Add(lOAIHANG);
                    db.SaveChanges();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult XoaSanPham(int ID)
        {
           
            QLCAFEEntities db = new QLCAFEEntities();
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(x => x.MASP == ID);
            sp.TRANGTHAI = true;
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index2()
        {
            return View();
        }

    }
}