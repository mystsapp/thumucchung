using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using thumucchung.Models;

namespace thumucchung.Controllers
{
    public class HomeController : BaseController
    {
        string allowUpload = ConfigurationManager.AppSettings["AllowExtDisplay"].ToString();
        string allowDisplay = ConfigurationManager.AppSettings["AllowUploadFile"].ToString();
        public string ftpPath= ConfigurationManager.AppSettings["ftpserver"].ToString();
        string ftpaccount = ConfigurationManager.AppSettings["account"].ToString();
        string ftppass = ConfigurationManager.AppSettings["pass"].ToString();
        int baoloi = 0;
        int thanhcong = 0;
        //string FlagUrl= "/Content/image/folder.png";
        string phongban,username,chinhanh = "";

        public ActionResult Index(string path)
        {
            bool upload = false;
            ViewBag.ftp = ftpPath;
            phongban = Session["phongban"].ToString();
            username = Session["username"].ToString().Replace(".", "");           
            chinhanh = Session["chinhanh"].ToString();
            string[] a = Session["username"].ToString().Split('.');
            username = a[0].ToString();
            ViewBag.username = username;
         
            
            if (String.IsNullOrEmpty(path))
            {
                path = ftpPath+chinhanh+"/USERS/"+ username;  
               // path = ftpPath + chinhanh + "/" + phongban;
            }
         
            //else
            //{
            //    path = ftpPath + path;
            //}
            //ViewBag.home = path;// ftpPath+path;// + "Khanh";// phongban;
            ExplorerModel explorerModel = new ExplorerModel();
            path = HttpUtility.UrlDecode(path);
            path = path.Replace("aabbccddee", ":");
            path = path.Replace("asdfghjkl", "/");
            path = path.Replace("aqwsedrf", "+");

            ViewBag.home = path.Replace ("ftp://intranet.appsgt.net:4200/", "");
            upload = bool.Parse(Session["upload"].ToString());
            if (upload)
            {
                string duongdan = ViewBag.home;
                if (duongdan.Contains(chinhanh) && duongdan.Contains(phongban) )
                {
                    upload = true;
                }
                else
                {
                    upload = false;
                }
            }
            ViewBag.upload = upload;
            ViewBag.path = path;
           
            int iIndex = path.IndexOf('/');
            if (iIndex > -1)
            {
                path = path.Substring(iIndex + 1);

                iIndex = path.IndexOf('/');

                if (iIndex == 0)
                {
                    path = path.TrimStart('/');
                }

                path = "ftp://" + path;
            }

            string sFlagUrl = "~/Content/image/folder-close-icon48.png";

            Session["sUncPath"] = path;
            List<DirModel> dirModels = new List<DirModel>();
            List<FileModel> fileModels = new List<FileModel>();

            string[] dirs = GetDirectoryListing(path, ftpaccount, ftppass);
            string sTmp = "";
            foreach (string d in dirs)
            {
                sTmp = d;
                // bo dau / 
                if (sTmp.StartsWith("/"))
                {
                    sTmp = sTmp.TrimStart('/');
                }
                // bo cac ten file
                if (d.IndexOf('.') == -1)
                {
                    // get dir
                    DirModel dd = new DirModel();
                    dd.DirName = sTmp;
                    dd.DirAccessed = DateTime.Now;
                    dd.FullDirName = path + "/" + sTmp;
                    dd.FlagUrl = sFlagUrl;
                    dirModels.Add(dd);
                }
                else
                {
                    // get file extension
                    int indexExt = sTmp.LastIndexOf('.');
                    string sExt = sTmp.Substring(indexExt + 1);
                    long lFileLenght = GetFTPFileSize(path + "/" + sTmp, ftpaccount, ftppass);
                    if (allowDisplay.Contains(sExt.ToLower()))
                    {
                        FileModel ff = new FileModel();
                        ff.FileName = sTmp;
                        
                        ff.FileAccessed = DateTime.Now;
                        ff.FileSizeText = (lFileLenght < 1024) ? lFileLenght.ToString() + " B" : String.Format("{0:0,0}",( lFileLenght / 1024)) + " KB";
                        //ff.FileSizeText = GetSizeString(lFileLenght);
                        ff.FileExt = sExt;
                        ff.ParentFileDir = path;
                        fileModels.Add(ff);
                    }

                }
            }
            explorerModel = new ExplorerModel(dirModels, fileModels);
            return View(explorerModel);

        }
        public ActionResult readFile(string path)
        {
            string strFileName = "";
            //For test
            //Session["userid"] = mUsrTest;
            //Session["pass"] = mPwd;
            path = path.Replace("aabbccddee", ":");
            path = path.Replace("asdfghjkl", "/");
            path = path.Replace("aqwsedrf", "+");
            path = HttpUtility.UrlDecode(path);
            string googledrive = path.Replace("ftp://intranet.appsgt.net:4200/", "http://appsgt.net:1234/document/");
            ViewBag.googledrive = googledrive;
            int iIndex = path.IndexOf('/');

            if (iIndex > -1)
            {
                path = path.Substring(iIndex + 1);

                iIndex = path.IndexOf('/');

                if (iIndex == 0)
                {
                    path = path.TrimStart('/');
                }

                int iLastIndex = path.LastIndexOf('/');
                if (iLastIndex > -1)
                {
                    strFileName = path.Substring(iLastIndex + 1);
                }

                strFileName = HttpUtility.UrlDecode(strFileName);
                //path = "ftp://" + path;
            }
            //return Redirect("https://docs.google.com/gview?url=http://appsgt.net:1234/document/"+path+strFileName+"&embedded=true");
            return Redirect("https://docs.google.com/gview?url=" +googledrive + "&embedded=true");
           // return Redirect("https://docs.google.com/gview?url=http://appsgt.net:1234/document/" + path  + "&embedded=true");
        }
        public ActionResult downFile(string path)
        {
            string strFileName = "";
            //For test
            //Session["userid"] = mUsrTest;
            //Session["pass"] = mPwd;
            path = path.Replace("aabbccddee", ":");
            path = path.Replace("asdfghjkl", "/");
            path = path.Replace("aqwsedrf", "+");
            path = HttpUtility.UrlDecode(path);
            int iIndex = path.IndexOf('/');

            if (iIndex > -1)
            {
                path = path.Substring(iIndex + 1);

                iIndex = path.IndexOf('/');

                if (iIndex == 0)
                {
                    path = path.TrimStart('/');
                }

                int iLastIndex = path.LastIndexOf('/');
                if (iLastIndex > -1)
                {
                    strFileName = path.Substring(iLastIndex + 1);
                }

                
                path = "ftp://" + path;
            }


            //http://stackoverflow.com/questions/1176022/unknown-file-type-mime
            //return base.File(ftpPath, "application/octet-stream");

            //LARGE FILE              
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
                request.KeepAlive = true;
                request.UsePassive = true;
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpaccount, ftppass);
                FtpWebResponse ftpResponse = (FtpWebResponse)request.GetResponse();

                //LARGE FILE

                // **************************************************
                Response.Buffer = false;

                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName);

                // **************************************************
                // 8KB

                //---------------------------------------------------
                long lngFileLength = GetFTPFileSize(path, ftpaccount, ftppass);
                Response.AddHeader("Content-Length", lngFileLength.ToString());

                using (Stream responseStream = ftpResponse.GetResponseStream())
                {
                    int intBufferSize = 8 * 1024;

                    // Create buffer for reading [intBufferSize] bytes from file
                    byte[] bytBuffers = new System.Byte[intBufferSize];

                    // Total bytes that should be read
                    long lngDataToRead = lngFileLength;

                    // Read the bytes of file
                    while (lngDataToRead > 0)
                    {
                        // Verify that the client is connected or not?
                        if (Response.IsClientConnected)
                        {
                            // Read the data and put it in the buffer.
                            int intTheBytesThatReallyHasBeenReadFromTheStream =
                                responseStream.Read(buffer: bytBuffers, offset: 0, count: intBufferSize);

                            // Write the data from buffer to the current output stream.
                            Response.OutputStream.Write(buffer: bytBuffers, offset: 0, count: intTheBytesThatReallyHasBeenReadFromTheStream);

                            // Flush (Send) the data to output
                            // (Don't buffer in server's RAM!)
                            Response.Flush();

                            lngDataToRead =
                                lngDataToRead - intTheBytesThatReallyHasBeenReadFromTheStream;
                        }
                        else
                        {
                            // Prevent infinite loop if user disconnected!
                            lngDataToRead = -1;
                        }
                    }

                    if (responseStream != null)
                    {
                        //Close the file.
                        responseStream.Close();
                        responseStream.Dispose();
                    }
                }

                //END LARGER FILE



            }
            catch (WebException we)
            {
                FtpWebResponse response = (FtpWebResponse)we.Response;
                return Content("Lỗi: " + response.StatusDescription);
            }
            catch (Exception ex)
            {
                return Content("Lỗi không đọc được file: " + ex.Message + " !");
            }
            finally
            {
                //Response.Close();
                //nen dung
                this.HttpContext.ApplicationInstance.CompleteRequest();
            }
            //END LARGER FILE                                  


            List<DirModel> dirModels = new List<DirModel>();
            List<FileModel> fileModels = new List<FileModel>();

            ExplorerModel explorerModel = new ExplorerModel(dirModels, fileModels);
            return View(explorerModel);

        }
        [HttpGet]
        public ActionResult uploadFile()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult uploadFile(HttpPostedFileBase[] uploadfile)
        {
            try
            {
                thanhcong = 0;
                baoloi = 0;
                bool result = true;
                string uploadpath = Session["sUncPath"].ToString();
                uploadpath = HttpUtility.UrlDecode(uploadpath);

                foreach (HttpPostedFileBase item in uploadfile)
                {
                    if (item != null)
                    {
                        result = ImportDetail(item, uploadpath);
                        if (result)
                            thanhcong++;
                        else
                            baoloi++;
                    }
                }

                SetAlert("Có " + thanhcong + " file upload thành công, và " + baoloi + " file không thành công.", "success");


                return RedirectToAction("Index", "Home", new { path = HttpUtility.UrlDecode(uploadpath) });

            }
            catch
            {
                ViewBag.Error = "Có " + baoloi + " upload không thành công";
                return View();
            }
        }
        [HttpGet]
        public ActionResult createFolder()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult createFolder(string foldername)
        {
            try
            {
                string uploadpath = Session["sUncPath"].ToString();
                uploadpath = HttpUtility.UrlDecode(uploadpath);
                FtpWebRequest requestDir = (FtpWebRequest)WebRequest.Create(new Uri(uploadpath +"/"+ foldername));

                requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestDir.Credentials = new NetworkCredential(ftpaccount, ftppass);
                requestDir.UsePassive = true;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
                SetAlert("Tạo thư mục " + foldername + " thành công.", "success");
                return RedirectToAction("Index", "Home", new { path = HttpUtility.UrlDecode(uploadpath) });
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                SetAlert("Tạo thư mục lỗi: " + ex , "error");
                return View("Index");

            }
        }
        private long GetFTPFileSize(string fullftpfilepath, string user, string pwd)
        {
            long size = 0;

            try
            {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(fullftpfilepath));
                request.Proxy = null;
                request.Credentials = new NetworkCredential(user, pwd);
                request.Method = WebRequestMethods.Ftp.GetFileSize;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                size = response.ContentLength;
                response.Close();
            }
            catch //(Exception ex)
            {
                //throw ex;
            }


            return size;
        }
        private string[] GetDirectoryListing(string ftpUrl, string user, string pwd)
        {
            FtpWebRequest directoryListRequest = (FtpWebRequest)WebRequest.Create(ftpUrl);
            directoryListRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            directoryListRequest.Credentials = new NetworkCredential(user, pwd);

            //ftp://192.168.3.43/phanmem
            //lay ra phan phanmem
            string sRes = "";
            int iIndex = ftpUrl.IndexOf('/');

            if (iIndex > -1)
            {
                sRes = ftpUrl.Substring(iIndex + 1);
                iIndex = sRes.IndexOf('/');

                if (iIndex == 0)
                {//192.168.3.43/phanmem
                    sRes = sRes.TrimStart('/');

                    iIndex = sRes.LastIndexOf('/');

                    if (iIndex > -1)
                    {//phanmem
                        sRes = sRes.Substring(iIndex + 1);
                    }
                }
            }

            try
            {
                using (FtpWebResponse directoryListResponse = (FtpWebResponse)directoryListRequest.GetResponse())
                {
                    using (StreamReader directoryListResponseReader = new StreamReader(directoryListResponse.GetResponseStream()))
                    {
                        string responseString = directoryListResponseReader.ReadToEnd();

                        //remove //phanmem
                        if (sRes != "")
                        {
                            if (responseString.Contains(sRes))
                            {
                                responseString = responseString.Replace(sRes, "");
                            }
                        }

                        string[] results = responseString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        return results;
                    }
                }
            }
            catch
            {
                string[] results = new string[1];
                results[0] = "";
                return results;
            }

        }
        private string GetSizeString(long length)
        {
            long B = 0, KB = 1024, MB = KB * 1024, GB = MB * 1024, TB = GB * 1024;
            double size = length;
            string suffix = nameof(B);

            if (length >= TB)
            {
                size = Math.Round((double)length / TB, 2);
                suffix = nameof(TB);
            }
            else if (length >= GB)
            {
                size = Math.Round((double)length / GB, 2);
                suffix = nameof(GB);
            }
            else if (length >= MB)
            {
                size = Math.Round((double)length / MB, 2);
                suffix = nameof(MB);
            }
            else if (length >= KB)
            {
                size = Math.Round((double)length / KB, 2);
                suffix = nameof(KB);
            }

            return $"{size} {suffix}";
        }

        public bool ImportDetail(HttpPostedFileBase uploadfile,string uploadpath)
        {
            //string allowUpload = ConfigurationManager.AppSettings["AllowUploadFile"].ToString();
            
            string ext = Path.GetExtension(uploadfile.FileName);
            if (!allowUpload.Contains(ext.ToLower()))
            {
                return false;
            }
            else
            {
               
                string fileName = Path.GetFileName(uploadfile.FileName);
                int BUFFER_SIZE = 8 * 1024;
                var sourceStream = uploadfile.InputStream;
                byte[] buffer = new byte[BUFFER_SIZE];
                int bytesRead = sourceStream.Read(buffer, 0, BUFFER_SIZE);
                try
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadpath + "/" + uploadfile.FileName);
                    request.Method = WebRequestMethods.Ftp.UploadFile;

                    // This example assumes the FTP site uses anonymous logon.
                    request.Credentials = new NetworkCredential(ftpaccount, ftppass);
                    request.ContentLength = sourceStream.Length;
                    request.UsePassive = true;
                    request.UseBinary = true;

                    request.EnableSsl = false;
                    request.KeepAlive = false;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        do
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                            bytesRead = sourceStream.Read(buffer, 0, BUFFER_SIZE);
                        } while (bytesRead > 0);

                        //  requestStream.Write(fileBytes, 0, fileBytes.Length);
                        sourceStream.Close();
                        requestStream.Close();

                        return true;
                    }
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                    //return false;
                }
               
            }
        }
      
       


    }
   
}