using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using thumucchung.Models;
using thumucchung.Models.EF;
namespace thumucchung.Controllers
{
    public class AccountController : BaseController
    {
       // Service s = null;
        
        // GET: Account
        public ActionResult Index(string searchstring,int page=1,int pageSize=10)
        {
            try
            {
                var entity = new Service().listUser(searchstring, page, pageSize);
                ViewBag.searchstring = searchstring;
                return View(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        [HttpGet]
        public ActionResult themUser()
        {
            dschinhanh("");
            Trangthai("True");
            truycap("True");
            upload("False");
            doimk();
            phongban();
            quyen();
            khachdoan();
            khachle();
            return View();
        }
        [HttpPost]
        public ActionResult themuser(user entity)
        {
           
               entity.fullName = entity.fullName.ToUpper();
               entity.dirPathName = String.IsNullOrEmpty(entity.dirPathName) ? "" : entity.dirPathName;
               string result = new Service().insertUser(entity);
               if (!String.IsNullOrEmpty(result))
               {
                    SetAlert("Thêm nhân viên thành công.", "success");
                    return RedirectToAction("Index","Account");
               }
               else
               {
                   SetAlert("Thêm nhân viên không thành công.", "error");
               }
          
            return RedirectToAction("Index", "Account");
        }
        [HttpGet]
        public ActionResult capnhatUser(string username)
        {
            var u = new Service().getUserByName(username);
            dschinhanh(u.chinhanh);
            Trangthai(u.trangthai.ToString());
            truycap();
           
            upload(u.upload.ToString());
            doimk();
            phongban();
            quyen();
            khachle(u.adminkl.ToString());
            khachdoan(u.adminkd.ToString());
            return View(u);
        }
        [HttpPost]
        public ActionResult capnhatUser(user entity)
        {
            bool result = new Service().updateUser(entity);
            if (result)
            {
                SetAlert("Cập nhật nhân viên thành công.", "success");
            }
            else
            {
                SetAlert("Cập nhật nhân viên không thành công.", "error");
            }
            return RedirectToAction("Index", "Account");
        }
        [HttpPost]
        public JsonResult changeStatus(string username)
        {
            var result = new Service().changeStatus(username);
            return Json(new { status = result });
        }
        [HttpPost]
        public JsonResult deleteUser (string username)
        {
            bool status = true;
            string message = String.Empty;
            try
            {
                var result = new Service().delUser(username);
                status = result;
            }
            catch(Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return Json(new
            {
                status = status,
                message = message
            });
        }
        public bool checkUser(string username,string phongban)
        {
            try
            {
                return new Service().checkUsername(username, phongban);
            }
            catch
            {
                return false;   
            
            }
        }
        public void dschinhanh(string cn="")
        {
            Service s = new Service();          
            ViewBag.chinhanh = new SelectList(s.dsChinhanh(), "chinhanh", "chinhanh", cn);           
        }
        public void Trangthai(string select= "True")
        {
            List<SelectListItem> trangthai = new List<SelectListItem>();
            trangthai.Add(new SelectListItem { Text = "Kích hoạt", Value = "True" });
            trangthai.Add(new SelectListItem { Text = "Khóa", Value = "False" });

            ViewBag.trangthai = new SelectList(trangthai, "Value", "Text", select);
        }
        public void truycap(string select = "True")
        {
            List<SelectListItem> truycap = new List<SelectListItem>();
            truycap.Add(new SelectListItem { Text = "Được phép", Value = "True" });
            truycap.Add(new SelectListItem { Text = "Không được phép", Value = "False" });

            ViewBag.truycap = new SelectList(truycap, "Value", "Text", select);
        }
        public void upload(string select = "False")
        {
            List<SelectListItem> upload = new List<SelectListItem>();
            upload.Add(new SelectListItem { Text = "Không được phép", Value = "False" });
            upload.Add(new SelectListItem { Text = "Được phép", Value = "True" });
           
            ViewBag.upload = new SelectList(upload, "Value", "Text", select);
        }
        public void doimk(string select="True")
        {
            List<SelectListItem> doimk = new List<SelectListItem>();
            doimk.Add(new SelectListItem { Text = "Có", Value = "True" });
            doimk.Add(new SelectListItem { Text = "Không", Value = "False" });

            ViewBag.doimk = new SelectList(doimk, "Value", "Text",select);
        }
        public void khachdoan(string select = "True")
        {
            List<SelectListItem> adminkd = new List<SelectListItem>();
            adminkd.Add(new SelectListItem { Text = "Phải", Value = "True" });
            adminkd.Add(new SelectListItem { Text = "Không", Value = "False" });

            ViewBag.adminkd = new SelectList(adminkd, "Value", "Text", select);
        }
        public void khachle(string select = "True")
        {
            List<SelectListItem> adminkl = new List<SelectListItem>();
            adminkl.Add(new SelectListItem { Text = "Phải", Value = "True" });
            adminkl.Add(new SelectListItem { Text = "Không", Value = "False" });

            ViewBag.adminkl = new SelectList(adminkl, "Value", "Text", select);
        }
        public void phongban()
        {
            try
            {              
                ViewBag.phongban = new Service().dsPhong().Select(x => new SelectListItem { Text = x.tenphongban, Value = x.phongban }).ToList();
            }
            catch (Exception ex)
            { throw ex; }
        }
        public void quyen(string select="")
        {
            try
            {
                Service s = new Service();
                ViewBag.quyen=new  SelectList(s.dsPhanquyen(),"role","role",select);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}