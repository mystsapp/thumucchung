using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using thumucchung.Models;
using thumucchung.Models.EF;
using PagedList;
using System.Web.Mvc;

namespace thumucchung
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        private qlthumucDbContext db = null;
        public Service()
        {
            db = new qlthumucDbContext();
        }
       public int login(string username,string password,string phongban)
       {
            MaHoaSHA1 sha1 = new MaHoaSHA1();
          
            var result = db.Database.SqlQuery<user>("spLogin @username, @password, @phongban",
            new SqlParameter[]
            {
                         new SqlParameter("@username", username),
                         new SqlParameter ("@password",sha1.EncodeSHA1(password)),
                         new SqlParameter("@phongban",phongban)

            }).FirstOrDefault();
            if (result ==null)
            {
                return  0;
            }
            else
            {
                return 1;
            }
            
       }
        public int doimatkhau(string username,string password,string newpassword, string confirmpassword, string phongban)
        {
            MaHoaSHA1 sha1 = new MaHoaSHA1();
            if (String.IsNullOrEmpty(password))
            {
                return -1;
            }
            if(Session["oldpassword"].ToString()!=sha1.EncodeSHA1(password))
            {
                return -2;
            }
            if(String.IsNullOrEmpty(newpassword))
            {
                return -3;
            }
            if (String.IsNullOrEmpty(confirmpassword))
            {
                return -4;
            }
            if (newpassword != confirmpassword)
            {
                return -5;
            }
            else
            {
                var result = db.Database.ExecuteSqlCommand("spDoimatkhau @username, @password, @phongban",
                                    new SqlParameter[]
                                    {
                                    new SqlParameter("@username", username),
                                    new SqlParameter ("@password",sha1.EncodeSHA1(newpassword)),
                                    new SqlParameter("@phongban",phongban)
                                    });
                if (result == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        [WebMethod]
        public  user getUserInfoByName(string username,string phongban)
        {
            try
            {
                //return db.users.Where(x => x.username == username).FirstOrDefault();
                var u = db.users.SqlQuery("spGetUserInfoByUsername @username, @phongban",
                    new SqlParameter[]
                    {
                         new SqlParameter("@username", username),
                         new SqlParameter("@phongban", phongban)

                    }).FirstOrDefault();

                return u;
                //return db.users.Where(x=>x.u)
            }
            catch (SqlException ex)
            {
                throw ex;
               // return null;
            }
        }
        [WebMethod]
        public user getUserByName(string username)
        {
            try
            {
                return db.users.Where(x => x.username == username).FirstOrDefault();
                //var u = db.users.SqlQuery("spGetUserInfoByUsername @username, @phongban",
                //    new SqlParameter[]
                //    {
                //         new SqlParameter("@username", username),
                //         new SqlParameter("@phongban", phongban)

                //    }).FirstOrDefault();

                //return u;
                //return db.users.Where(x=>x.u)
            }
            catch (SqlException ex)
            {
                throw ex;
                // return null;
            }
        }
        [WebMethod]
        public IEnumerable<user> dsUser()
        {
            try
            {
                var result = db.Database.SqlQuery<user>("spDanhsachUser");
                return result;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public string insertUser(user entity)
        {
            try
            {
                MaHoaSHA1 sha1 = new MaHoaSHA1();
                entity.password = !string.IsNullOrEmpty(entity.password) ? sha1.EncodeSHA1(entity.password) : sha1.EncodeSHA1("123");
                db.Entry(entity).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return entity.username;
            }
            catch (Exception ex)
            {
                throw ex;
                //return string.Empty ;   
            }
        }
        [WebMethod]
        public bool updateUser(user entity)
        {
            try
            {
                var u = db.users.Where(x => x.username == entity.username).FirstOrDefault();
                MaHoaSHA1 sha1 = new MaHoaSHA1();
                if (!String.IsNullOrEmpty(entity.password))
                {
                    u.password = sha1.EncodeSHA1(entity.password);
                }               
                u.fullName = entity.fullName.ToUpper();
                u.chinhanh = entity.chinhanh;
                u.dirPathName = entity.dirPathName;
                u.doimk = entity.doimk;
                u.phongban = entity.phongban;
                u.trangthai = entity.trangthai;
                u.upload = entity.upload;
                u.show = entity.show;
                u.adminkd = entity.adminkd;
                u.adminkl = entity.adminkl;
                u.role = entity.role;
                u.email = entity.email;
                db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
               // return false;
            }
        }
        [WebMethod]
        public bool changeStatus(string username)
        {
            var u = db.users.Find(username);
            u.trangthai = !u.trangthai;
            db.SaveChanges();
            return u.trangthai;
        }
        [WebMethod]
        public bool delUser(string username)
        {
            try
            {
                var result = db.users.Where(x => x.username == username).FirstOrDefault();
                db.Entry(result).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [WebMethod]
        public bool checkUsername(string username,string phongban)
        {
            try
            {
                var u = db.users.SqlQuery("spGetUserInfoByUsername @username, @phongban",
                     new SqlParameter[]
                     {
                         new SqlParameter("@username", username),
                         new SqlParameter("@phongban", phongban)

                     }).FirstOrDefault();

                if (u == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        [WebMethod]
        public IEnumerable<user> listUser(string searchstring, int page, int pageSize)
        {
            try
            {
                string search = String.IsNullOrEmpty(searchstring) ? "" : searchstring;
                IQueryable<user> model = db.users;
                return model.Where(x => x.username.Contains(search) || x.fullName.Contains(search) || x.phongban.Contains(search) ||  x.chinhanh.Contains(search)).OrderBy(x => x.chinhanh).ToPagedList(page, pageSize);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public IEnumerable<dmchinhanh> dsChinhanh()
        {
            try
            {
                List<dmchinhanh> model = db.Database.SqlQuery<dmchinhanh>("spDsChinhanh").ToList();
                return model;               
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public IEnumerable<dmphong> dsPhong()
        {
            try
            {
                var result = db.dmphongs.ToList();
                return result;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public IEnumerable<phanquyen> dsPhanquyen()
        {
            try
            {
                var result = db.phanquyens.ToList();
                return result;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
