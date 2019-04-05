using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using thumucchung.Models;
using thumucchung.Models.EF;
namespace thumucchung.Controllers
{
    public class DriverController : Controller
    {
        // GET: Driver
        public ActionResult Index()
        {
            try
            {
                Login l = new Login();
                l.username = Request.QueryString["a"].ToString();
                l.password = Request.QueryString["c"].ToString();
                l.phongban = Request.QueryString["b"].ToString();
                Service s = new Service();
                var result = s.login(l.username, l.password, l.phongban);
                if (result == 0)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    user u = s.getUserInfoByName(l.username, l.phongban);
                    if (u != null)
                    {
                        Session["username"] = u.username;
                        Session["fullName"] = u.fullName.ToUpper();
                        Session["phongban"] = u.phongban;
                        Session["role"] = u.role;
                        Session["adminkl"] = u.adminkl;
                        Session["adminkd"] = u.adminkl;
                        Session["chinhanh"] = u.chinhanh;
                        Session["upload"] = u.upload;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }                
            }
            catch
            {
                return RedirectToAction("Index", "login");
            }
           
           
        }
    }
}