using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using thumucchung.Models;
using thumucchung.Models.EF;
namespace thumucchung.Controllers
{
    public class LoginController : Controller
    {
       
        // GET: Login
        public ActionResult Index()
        {

            dsphongban();
            return View();
        }
        [HttpPost]
        public ActionResult login(string username, string password, string phongban)
        {
            if (ModelState.IsValid)
            {
                Service sv = new Service();
                var result = sv.login(username, password, phongban);
                if (result == 0)
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
                else
                {
                    user u = sv.getUserInfoByName(username,phongban);
                    if (u != null)
                    {
                        Session["username"] = u.username;
                        Session["fullName"] = u.fullName.ToUpper();
                        Session["oldpassword"] = u.password;
                        Session["phongban"] = u.phongban;
                        Session["role"] = u.role;
                        Session["adminkl"]=u.adminkl;
                        Session["adminkd"] = u.adminkd;
                        Session["upload"] = u.upload;
                        Session["show"] = u.show;
                        Session["chinhanh"] = u.chinhanh;
                        Session["thongbao"] = "";
                        bool doi = u.doimk;
                        if (doi)
                        {
                            Session["thongbao"] = "Đề nghị anh chị đổi mật khẩu theo qui định.";
                        }
                        DateTime ngaydoimk = u.ngaydoimk;
                        int kq = (DateTime.Now.Month - ngaydoimk.Month) + 12 * (DateTime.Now.Year - ngaydoimk.Year);
                        if (kq > 1)
                        {
                            Session["thongbao"] = "Đề nghị anh chị đổi mật khẩu theo qui định.";
                        }
                        
                        return RedirectToAction("Index", "Home");
                        
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                    }
                }
               
            }
            dsphongban();
            return View("Index");          
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("/");
        }
        public void dsphongban()
        {
            try
            {
                Service s = new Service();
                ViewBag.phongban = new SelectList(s.dsPhong(), "phongban", "tenphongban");
            }
            catch (Exception ex)
            { throw ex; }
        }

        [HttpGet]
        public ActionResult changepass()
        {
            var entity = new changepassModel();
            Service s = new Service();
            var dao = new Service().getUserInfoByName(Session["username"].ToString(),Session["phongban"].ToString());// new UserDao().getUserByName(Session["username"].ToString());
            entity.username = dao.username;
            entity.password = dao.password;
            return View("changepass", entity);
        }
        [HttpPost]
        public ActionResult changepass(changepassModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = new Service().doimatkhau(entity.username, entity.password, entity.newpassword, entity.confirmpassword, Session["phongban"].ToString());
                if (result == -1)
                {
                    ModelState.AddModelError("", "Vui lòng nhập mật khẩu cũ.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu cũ không đúng.");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Vui lòng nhập mật khẩu mới.");
                }
                else if (result == -4)
                {
                    ModelState.AddModelError("", "Vui lòng nhập lại mật khẩu mới.");
                }
                else if (result == -5)
                {
                    ModelState.AddModelError("", "Mật khẩu nhập lại không đúng.");
                }
                else if (result == 1)
                {
                    Session["thongbao"] = "";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("changepass");
        }

    }
}