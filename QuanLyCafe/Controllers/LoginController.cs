using QuanLyCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCafe.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoginUser(NguoiDungModel model)
        {
            string result = "";
            QLCAFEEntities db = new QLCAFEEntities();
            if(model.PASSWORD!=null && model.MAND != null)
            {
                NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.SingleOrDefault(x => x.MAND == model.MAND && x.PASSWORD == model.PASSWORD);
                
                if (nGUOIDUNG != null)
                {
                    Session["TenNV"] = nGUOIDUNG.TENNV;
                    Session["MaND"] = nGUOIDUNG.MAND;
                

                    if(nGUOIDUNG.MAQUYEN == 1)
                    {
                        result = "admin";
                    }
                    else if(nGUOIDUNG.MAQUYEN == 2)
                    {
                        result = "NhanVien";
                    }
                }
                else
                {
                    result = "fail";
                }
            }
            
           
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index");
        }
    }
}