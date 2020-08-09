using QuanLyCafe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCafe.Controllers
{
    public class BaoCaoDoanhSoController : Controller
    {
        QLCAFEEntities db = new QLCAFEEntities();
        // GET: BaoCaoDoanhSo
        public ActionResult Index()
        {
            List<DONDATHANG> listDDH = db.DONDATHANGs.Where(x => x.TRANGTHAI == true).ToList();
            IDictionary<string, int> d = new Dictionary<string, int>();           
            IDictionary<string, string> dvt = new Dictionary<string, string>();
            foreach (var i in listDDH)
            {
                List<CHITIETDATHANG> listctdh = db.CHITIETDATHANGs.Where(x => x.SOHOADONDAT == i.SOHOADONDAT).ToList();
                foreach(var j in listctdh)
                {
                    if (d.ContainsKey(j.SANPHAM.TENSP))
                    {
                        d[j.SANPHAM.TENSP] += j.SOLUONG;
                    }
                    else
                    {
                        d.Add(j.SANPHAM.TENSP, j.SOLUONG);                       
                        dvt.Add(j.SANPHAM.TENSP, j.SANPHAM.DONVITINH.TenDVT);
                    }
                }
            }
            d.OrderBy(x => x.Key);
            ViewBag.TENSP = d.Keys.ToList();
            ViewBag.SL = d;          
            ViewBag.DVT = dvt;
            Session["tuNgayDS"] = "2020-01-01";
            Session["denNgayDS"] = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public ActionResult TimKiem(string tuNgay, string denNgay)
        {

            List<DONDATHANG> listDonDatHang = new List<DONDATHANG>();
            DateTime TN = DateTime.Parse(tuNgay);
            DateTime DN = DateTime.Parse(denNgay);

            foreach (var i in db.DONDATHANGs)
            {
                DateTime ngayGiao = i.NGAYGIAO;
                if (ngayGiao >= TN && ngayGiao <= DN && i.TRANGTHAI == true)
                {
                    listDonDatHang.Add(i);
                }
            }
            IDictionary<string, int> d = new Dictionary<string, int>();
            IDictionary<string, string> dvt = new Dictionary<string, string>();
            foreach (var i in listDonDatHang)
            {
                List<CHITIETDATHANG> listctdh = db.CHITIETDATHANGs.Where(x => x.SOHOADONDAT == i.SOHOADONDAT).ToList();
                foreach (var j in listctdh)
                {
                    if (d.ContainsKey(j.SANPHAM.TENSP))
                    {
                        d[j.SANPHAM.TENSP] += j.SOLUONG;
                    }
                    else
                    {
                        d.Add(j.SANPHAM.TENSP, j.SOLUONG);
                        dvt.Add(j.SANPHAM.TENSP, j.SANPHAM.DONVITINH.TenDVT);
                    }
                }
            }
            d.OrderBy(x => x.Key);
            ViewBag.TENSP = d.Keys.ToList();
            ViewBag.SL = d;
            ViewBag.DVT = dvt;
            Session["tuNgayDS"] = tuNgay.ToString();
            Session["denNgayDS"] = denNgay.ToString();

            return View("Index");
        }
    }
}