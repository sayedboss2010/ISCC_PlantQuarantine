using PlantQuar.WEB.API;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Imges.Controllers
{
    public class Downlood_ImgeController : Controller
    {
        // GET: Imges/Downlood_Imge
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Get_Downlood_Imge(string path1)
        {
            try
            {
                var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();
                //Encrypt.Decrypt y = new Decrypt();
                //var xx = path1.Replace(" ", "+");
                //var DomainPath = "10.10.91.15";//prodction
                //var DomainPath = "10.10.91.11";//test
               //var DomainPath = "10.7.7.242";//local
                //path1 = y.Decryption(xx);
                if (path1[0].ToString() != "/" && path1[0].ToString() != @"\" && path1[0].ToString() != @"\\")
                {
                    path1 = @"\" + path1;
                }                                
                        int port = Request.Url.Port;
                String SharedIp = DomainPath;
              
                
                path1 = @"\\" + SharedIp + path1;
                path1=path1.Replace('/', '\\');
                var absolutePath = HttpContext.Server.MapPath(path1);
                if (!System.IO.File.Exists(absolutePath))
                {               
                    string[] FileArr = path1.Split('.');
                    string extention = FileArr[FileArr.Length - 1];
                    byte[] fileBytes = System.IO.File.ReadAllBytes(path1);

                    if (extention == "pdf" || extention == "PDF")
                    {
                        return File(fileBytes, "application/pdf");
                    }
                    else
                    {
                        string fileName = Path.GetFileName(path1);
                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                    }
                }
                else
                {
                    Response.Redirect("~/Imges/Downlood_Imge/Index");
                    return null;
                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Get_Downlood_Imge");
                // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                //DataClasses1DataContext data = new DataClasses1DataContext();
                string StoredName = "PersonProfile/GetReport";
                string ErrorName = ex.Message;
                //data.Error_saving_App(ErrorName, StoredName, DateTime.Now, 1);
                Response.Redirect("~//Imges/Downlood_Imge/Index");
                return null;
            }
        }       
    }
}