using PlantQuar.WEB.API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Admin.Controllers
{
    public class TestImageController : Controller
    {
        // GET: Admin/TestImage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult save(HttpPostedFileBase file, string txtIp)
        {
            try
            {
                //var DomainPath = ConfigurationManager.AppSettings["applicationUrl"].ToString();
                //var html = richTxt.Replace("src=\"", "src=\"" + DomainPath);
                string fname;
                fname = Path.GetFileName(file.FileName);
                NetworkShare.DisconnectFromShare(@"\\" + txtIp , true); //Disconnect in case we are currently connected with our credentials;
                NetworkShare.ConnectToShare(@"\\" + txtIp , "administrator", "asd@123"); //Connect with the new credentials
                if (!Directory.Exists(@"\\" + txtIp ))
                    Directory.CreateDirectory(@"\\" + txtIp);

                file.SaveAs(Path.Combine(@"\\\\" + txtIp+"\\" + fname));

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string mess = ex.Message;

                throw;
            }
        }

        public ActionResult GetReport(string path1)
        {
            try
            {

                Session["Path_Server"] = path1;
                return Redirect("~/ASP/DisplayImge.aspx");

            }
            catch (Exception ex)
            {

                return null;

            }

        }



        public string Upload_File_Path_NetworkShare(HttpPostedFileBase fileupload, string txtupload)
        {
            try
            {

                var DomainPath = "10.10.91.15"; //ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();
                //var html = richTxt.Replace("src=\"", "src=\"" + DomainPath);
                string fname;
                fname = Path.GetFileName(fileupload.FileName);
                NetworkShare.DisconnectFromShare(@"\\" + DomainPath + "\\plant", true); //Disconnect in case we are currently connected with our credentials;
                NetworkShare.ConnectToShare(@"\\" + DomainPath + "\\plant", "administrator", "asd@123"); //Connect with the new credentials
                string AllPath = "";
                AllPath = @"\\" + DomainPath + "\\plant\\Testupload";
                if (!Directory.Exists(AllPath))
                {
                    Directory.CreateDirectory(AllPath);
                }
                fileupload.SaveAs(Path.Combine(AllPath+"\\"+ fname));
              
               
               
              
                
                
                
             
                return AllPath;
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
                return ex.Message;
            }
        }
    }
}