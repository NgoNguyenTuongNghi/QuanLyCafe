using QuanLyCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCafe.Controllers
{
    public class BaoCaoDoanhThuController : Controller
    {
        QLCAFEEntities db = new QLCAFEEntities();
        // GET: BaoCaoDoanhThu
        public ActionResult Index()
        {
            List<DONDATHANG> list = db.DONDATHANGs.Where(x => x.TRANGTHAI == true).ToList();            
            long s = 0, s1 = 0;
            IDictionary<string, long> d = new Dictionary<string, long>();
            IDictionary<string, long> g = new Dictionary<string, long>();
            foreach (var i in list)
            {
                DonDatHangmodel model = new DonDatHangmodel();
                string ngay = i.NGAYGIAO.ToString().Split(' ')[0];
                if (d.ContainsKey(ngay) && g.ContainsKey(ngay))
                {
                    d[ngay] += i.TONGTIEN;
                    if (i.TONGTIEN > 5000000)
                    {
                        g[ngay] += i.TONGTIEN / 10;
                    }
                    else
                    {
                        g[ngay] += 0;
                    }

                }
                else
                {
                    d.Add(ngay, i.TONGTIEN);
                    if (i.TONGTIEN > 5000000)
                    {
                        g.Add(ngay, i.TONGTIEN / 10);
                    }
                    else
                    {
                        g.Add(ngay, 0);
                    }
                }
                s += i.TONGTIEN;
                if (i.TONGTIEN > 5000000)
                {
                    s1 += i.TONGTIEN / 10;
                }
                else
                {
                    s1 += 0;
                }
            }
            d.Reverse();
            g.Reverse();
            ViewBag.Ngay = d.Keys.ToList();
            ViewBag.tongTien = d;
            ViewBag.giamTien = g;
            ViewBag.Tong = s;
            ViewBag.Giam = s1;
            ViewBag.ThanhToan = s - s1;
            Session["tuNgayDT"] = "2020-07-01";
            Session["denNgayDT"] = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public ActionResult TimKiem(string tuNgay, string denNgay)
        {
            
            //List<DONDATHANG> listDonDatHang = new List<DONDATHANG>();
            DateTime TN = DateTime.Parse(tuNgay);
            DateTime DN = DateTime.Parse(denNgay);

            //foreach (var i in db.DONDATHANGs)
            //{
            //    DateTime ngayGiao = i.NGAYGIAO;
            //    if (ngayGiao >= TN && ngayGiao <= DN && i.TRANGTHAI==true)
            //    {
            //        listDonDatHang.Add(i);
            //    }
            //}
            List<DONDATHANG> listDonDatHang = db.DONDATHANGs.Where(x => x.TRANGTHAI == true && x.NGAYGIAO >= TN && x.NGAYGIAO <= DN).ToList();
            long s = 0, s1 = 0;
            IDictionary<string, long> d = new Dictionary<string, long>();
            IDictionary<string, long> g = new Dictionary<string, long>();
            foreach (var i in listDonDatHang)
            {
                DonDatHangmodel model = new DonDatHangmodel();
                string ngay = i.NGAYGIAO.ToString().Split(' ')[0];
                if (d.ContainsKey(ngay) && g.ContainsKey(ngay))
                {
                    d[ngay] += i.TONGTIEN;
                    if (i.TONGTIEN > 5000000)
                    {
                        g[ngay] += i.TONGTIEN / 10;
                    }
                    else
                    {
                        g[ngay] += 0;
                    }

                }
                else
                {
                    d.Add(ngay, i.TONGTIEN);
                    if (i.TONGTIEN > 5000000)
                    {
                        g.Add(ngay, i.TONGTIEN / 10);
                    }
                    else
                    {
                        g.Add(ngay, 0);
                    }
                }
                s += i.TONGTIEN;
                if (i.TONGTIEN > 5000000)
                {
                    s1 += i.TONGTIEN / 10;
                }
                else
                {
                    s1 += 0;
                }

            }
            d.Reverse();
            g.Reverse();
            ViewBag.Ngay = d.Keys.ToList();
            ViewBag.tongTien = d;
            ViewBag.giamTien = g;
            ViewBag.Tong = s;
            ViewBag.Giam = s1;
            ViewBag.ThanhToan = s - s1;
            Session["tuNgayDT"] = tuNgay.ToString();
            Session["denNgayDT"] = denNgay.ToString();
                       
            return View("Index");
        }
    }
}