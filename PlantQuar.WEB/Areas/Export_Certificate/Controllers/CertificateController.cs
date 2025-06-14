using PlantQuar.DTO.DTO.Export_Certificate;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_Certificate.Controllers
{
    public class CertificateController : Controller
    {
        // GET: Export_Certificate/Certificate

        string apiName = "Certificate_API";
        public CertificateDTO Context;
        public ActionResult Index(long certificateId, bool isPrint)
        {
            Context = new CertificateDTO();
            //change isprinted to true


            var res = APIHandeling.getData(apiName + "?certificateId=" + certificateId);
            var model = res.Content.ReadAsAsync<CertificateDTO>().Result;//object
            ViewBag.certificateId = certificateId;
            ViewBag.isprint = isPrint;
            ViewBag.FullName = Session["FullName"];

            ViewBag.Outlet_Name = Session["Outlet_Name"];
            ViewBag.EmpId = Session["EmpId"];
            try
            {
                var path = "";

                if (model.data_CertificatesFiles.FirstOrDefault().FilePath!=null|| model.data_CertificatesFiles.FirstOrDefault().FilePath != "")
                {
                     path = model.data_CertificatesFiles.Select(a => a.FilePath).FirstOrDefault();
                }
          
                var countFiles = model.data_CertificatesFiles.Select(a => a.FilePath).Count();
                if (path != null)
                {
                    string[] FileDir = path.Split('/');

                    var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();

                    String SharedIp = DomainPath;

                    String All_path = @"\\" + SharedIp + "\\" + path;
                    All_path = All_path.Replace('/', '\\');


                    var absolutePath = HttpContext.Server.MapPath(All_path);
                    if (!System.IO.File.Exists(absolutePath))
                    {
                        string[] FileArr = All_path.Split('.');
                        var FileArr_All_path = All_path.Split();
                        string extention = FileArr[FileArr.Length - 1];
                        byte[] fileBytes = System.IO.File.ReadAllBytes(All_path);

                        if (extention == "pdf" || extention == "PDF")
                        {
                            return File(fileBytes, "application/pdf");
                        }
                        else
                        {

                            string directoryName = Path.GetDirectoryName(All_path);
                            string fileName = Path.GetFileName(All_path);
                            string destinationFilePath = HttpContext.Server.MapPath(@"~\Upload\CertificateAttachment");


                            try
                            {
                                foreach (String filename in Directory.GetFiles(directoryName, "*.*", SearchOption.AllDirectories))
                                {
                                    //your code here   
                                    System.IO.File.Copy(filename, filename.Replace(directoryName, destinationFilePath), true);
                                }

                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }

                            // System.IO.File.Copy(newPath, newPath.Replace(sourceFilePath, destinationFilePath), true);
                            //Now Create all of the directories
                            //  \\10.7.7.242\plant\Certificates\2023\1\PlantFiles279\Image_273.jpg
                            //foreach (string dirPath in Directory.GetDirectories(sourceFilePath, "*", SearchOption.AllDirectories))
                            // foreach (string dirPath in Directory.GetDirectories(@"\\10.7.7.242\plant\Certificates\2023\1\PlantFiles279\Image_273.jpg", "*", SearchOption.AllDirectories))
                            //{
                            //Directory.CreateDirectory(dirPath.Replace(sourceFilePath, destinationFilePath));
                            //}
                            //Create array of links of images
                            //

                            //Copy all the files & Replaces any files with the same name
                            //foreach (string newPath in Directory.GetFiles(sourceFilePath, "*.*", SearchOption.AllDirectories))
                            //{
                            //    System.IO.File.Copy(newPath, newPath.Replace(sourceFilePath, destinationFilePath), true);
                            //}
                            // return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                        }
                    }


                }

            }
            catch (Exception ex)
            {

            }

            // string sourceFilePath = @"D:\Certifacte1";
            //string destinationFilePath = @"D:\plant\23-10-2023\Upload\CertificateFile.txt";
            //  string destinationFilePath = @"D:\plant\23-10-2023\PlantQuar.WEB\Upload\CertificateAttachment";


            //System.IO.File.Copy(sourceFilePath, destinationFilePath);



            ////Now Create all of the directories
            //foreach (string dirPath in Directory.GetDirectories(sourceFilePath, "*", SearchOption.AllDirectories))
            //{
            //    Directory.CreateDirectory(dirPath.Replace(sourceFilePath, destinationFilePath));
            //}
            ////Create array of links of images
            ////

            ////Copy all the files & Replaces any files with the same name
            //foreach (string newPath in Directory.GetFiles(sourceFilePath, "*.*", SearchOption.AllDirectories))
            //{
            //    System.IO.File.Copy(newPath, newPath.Replace(sourceFilePath, destinationFilePath), true);
            //}
            //// Get a list of all the image files in the Images directory.
            ////var path = destinationFilePath;
            //string[] imageFiles = Directory.GetFiles(destinationFilePath);

            //// Add the list of image files to the ViewBag.
            //ViewBag.ImagePaths = imageFiles;


            //System.IO.File.Copy("D:/plant", "D:/noura old");
            Context = model;
            #region c
            /* var Renderer = new IronPdf.HtmlToPdf();
             #region m
             string s = @"<html>
 <style>
     table,tr,td{
         border:1px solid;
     }
 </style>
 <body><div id='cer' dir='rtl' style='width: 800px;'>
         <table dir = 'rtl' style = 'width:750px;'>

                <tbody><tr>


                    <td colspan = '2' rowspan = '2'>

                           <div style = 'display: inline-block;padding: 15px;'><img style = 'width: 155px;left:15px; position: relative;' src = '/Content/NewDesign/images/logo-capq.png'></div>

                                  <div style = 'display: inline-block;padding: 15px 68px 15px 0px;position: relative;top: 26px'>

                                       <p> جمهوريه مصر العربيه</p>

                                          <p> وزارة الزراعة واستصلاح الاراضي </p>

                                             <p> الادارة المركزية للحجر الزراعي </p>

                                            </div>

                                        </td>

                                        <td colspan = '2'>

                                             <div style = 'padding: 15px;'>

                                                  <h3> شهادة صحة نباتية</h3>

                                                     <p>
                                                         رقم: 0628652
                                                     </p>

                                                 </div>

                                             </td>


                                         </tr>

                                         <tr>

                                             <td colspan = '2'>

                                                  <div style = 'padding: 15px;'>

                                                       <label> ١ -الي المنطقة الوطنية لوقاية النباتات في: </label>

                                                       <textarea id = 'Orag_Plant' style = 'resize: none; border: none;'></textarea>

                                                      </div>


                                                  </td>

                                              </tr>

                                              <tr>

                                                  <td colspan = '2'>

                                                       <div style = 'padding: 15px;'>

                                                            <label style = 'display:block'> ٢ -اسم المصدر وعنوانه : </label>

                                                               <textarea id = 'Exporter_NamePrint' style = 'resize: none; border: none;'></textarea>

                                                                  <br>

                                                                  <textarea id = 'Exporter_AddressPrint' style = 'resize: none; border: none;'></textarea>

                                                                 </div>

                                                             </td>

                                                             <td colspan = '2'>

                                                                  <div style = 'padding: 15px;'>

                                                                       <label style = 'display:block'> ٣ -اسم المرسل اليه وعنوانه حسب بياناته: </label>

                                                                         <textarea id = 'Reciever_NamePrint' style = 'resize: none; border: none;'></textarea>

                                                                            <br>

                                                                            <textarea id = 'ImportCompany_AddressPrint' style = 'resize: none; border: none;'></textarea>

                                                                           </div>

                                                                       </td>

                                                                   </tr>


                                                                   <tr>

                                                                       <td colspan = '4'>

                                                                            <div style = 'padding:15px;text-align: center;'>

                                                                                 <label> وصف الشحنة </label>

                                                                                </div>

                                                                            </td>

                                                                        </tr>

                                                                        <tr>

                                                                            <td>

                                                                                <div style = 'padding: 15px;'>

                                                                                     <label> ٤ -وسيله النقل حسب البيانات: </label>

                                                                                      <textarea id = 'Transport_Mean_NamePrint' style = 'resize: none; border: none;'></textarea>

                                                                                     </div>

                                                                                 </td>

                                                                                 <td>

                                                                                     <div style = 'padding: 15px;'>

                                                                                          <label> ٥ -نقطة الدخول حسب البيانات:</label>

                                                                                            <textarea id = 'port_arrive_NamePrint' style = 'resize: none; border: none;'></textarea>

                                                                                           </div>

                                                                                       </td>

                                                                                       <td>

                                                                                           <div style = 'padding: 15px;'>

                                                                                                <label> ٦ -مكان المنشأ: </label>

                                                                                                 <textarea style = 'resize: none; border: none;'> مصر </textarea>

                                                                                              </div>

                                                                                          </td>

                                                                                          <td>

                                                                                              <div style = 'padding: 15px;'>

                                                                                                   <label> ٧ -العلامات المميزه: </label>

                                                                                                    <textarea id = 'Marks' style = 'resize: none; border: none;'></textarea>

                                                                                                   </div>

                                                                                               </td>

                                                                                           </tr>

                                                                                           <tr>

                                                                                               <td>

                                                                                                   <div style = 'padding: 15px;'>

                                                                                                        <label> ٨ -عدد الطرود و وصفها: </label>

                                                                                                         <textarea id = 'PackageCountPrint' style = 'resize: none; border: none;'></textarea>

                                                                                                        </div>

                                                                                                    </td>

                                                                                                    <td colspan = '2'>

                                                                                                         <div style = 'padding: 15px;'>

                                                                                                              <label style = 'display:block'> ٩ -اسم المنتج وكميتة حسب بيناته:</label>

                                                                                                                  <textarea id = 'categoriesPrint' style = 'resize: none; border: none;'>                    </textarea>

                                                                                                                 </div>

                                                                                                             </td>

                                                                                                             <td>

                                                                                                                 <div style = 'padding: 15px;'>

                                                                                                                      <label> ١٠ -الاسماء العلميه للنباتات : </label>

                                                                                                                        <textarea id = 'ScientificNamePrint' style = 'resize: none; border: none;'>                    </textarea>

                                                                                                                       </div>

                                                                                                                   </td>


                                                                                                               </tr>

                                                                                                               <tr>


                                                                                                                   <td colspan = '4'>

                                                                                                                        <div style = 'padding: 15px;'>

                                                                                                                             <p> نشهد بأن النباتات او المنتجات النباتية أو البنودالأخري الخاضعه للوائح الصحة النباتية الموصوفة ضمنا قد فحصت و/ او اختبرت طبقا للاجراءات المعتمدة الملائمة ووجدت خالية من أفات الحجر الزراعي التى حددها الطرف المتعاقد المستورد وفقا لاشترطات الصحة النباتية لدى الطرف المتعاقد المستورد بما في ذلك الاشترطات الخاصه بالافات غير الحجرية الخاضعه للوائح </p>

                                                                                                                             </div>

                                                                                                                         </td>

                                                                                                                     </tr>

                                                                                                                     <tr>


                                                                                                                         <td colspan = '4'>

                                                                                                                              <div style = 'padding: 15px;text-align:center'>


                                                                                                                                   <label> المعالجة للتطهير من التلوث و/ أو الاصابه </label>

                                                                                                                                   </div>

                                                                                                                               </td>

                                                                                                                           </tr>

                                                                                                                           <tr>

                                                                                                                               <td>

                                                                                                                                   <div style = 'padding: 15px;'>

                                                                                                                                        <label> ١١ -التاريخ : </label>

                                                                                                                                          <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                       </div>

                                                                                                                                   </td>

                                                                                                                                   <td>

                                                                                                                                       <div style = 'padding: 15px;'>

                                                                                                                                            <label> ١٢ -المعالجة : </label>

                                                                                                                                              <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                           </div>

                                                                                                                                       </td>

                                                                                                                                       <td>

                                                                                                                                           <div style = 'padding: 15px;'>

                                                                                                                                                <label> ١٣ -الكيماويات(الماده الفعاله) : </label>

                                                                                                                                                  <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                               </div>

                                                                                                                                           </td>

                                                                                                                                           <td>

                                                                                                                                               <div style = 'padding: 15px;'>

                                                                                                                                                    <label> ١٤ -التركيز : </label>

                                                                                                                                                      <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                                   </div>

                                                                                                                                               </td>

                                                                                                                                           </tr>

                                                                                                                                           <tr>

                                                                                                                                               <td colspan = '2'>

                                                                                                                                                    <div style = 'padding: 15px;'>

                                                                                                                                                         <label style = 'display:block'> ١٥ -مدة التعرض ودرجة الحراره: </label>

                                                                                                                                                            <textarea style = 'resize: none; border: none;'> </textarea>

                                                                                                                                                         </div>

                                                                                                                                                     </td>

                                                                                                                                                     <td colspan = '2'>

                                                                                                                                                          <div style = 'padding: 15px;'>

                                                                                                                                                               <label style = 'display:block'> ١٦ -معلومات اخري: </label>

                                                                                                                                                                  <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                                               </div>

                                                                                                                                                           </td>

                                                                                                                                                       </tr>

                                                                                                                                                       <tr>


                                                                                                                                                           <td colspan = '4'>

                                                                                                                                                                <div style = 'padding:15px;text-align:center'>

                                                                                                                                                                     <label> اقرار اضافي </label>

                                                                                                                                                                    </div>

                                                                                                                                                                </td>

                                                                                                                                                            </tr>

                                                                                                                                                            <tr>

                                                                                                                                                                <td colspan = '1'>

                                                                                                                                                                     <div style = 'padding: 15px;'>

                                                                                                                                                                          <label> ١٧ -تاريخ الفحص: </label>

                                                                                                                                                                           <textarea id = 'Check_DatePrint' style = 'resize: none; border: none;'></textarea>

                                                                                                                                                                          </div>

                                                                                                                                                                      </td>

                                                                                                                                                                      <td colspan = '2'>

                                                                                                                                                                           <div style = 'padding: 15px;'>

                                                                                                                                                                                <label style = 'display:block'> ١٨ -تاريخ الاصدار: </label>

                                                                                                                                                                                   <textarea style = 'resize: none; border: none;'> 23 / 09 / 2019 </textarea>

                                                                                                                                                                                </div>

                                                                                                                                                                            </td>

                                                                                                                                                                            <td colspan = '1'>

                                                                                                                                                                                 <div style = 'padding: 15px;'>

                                                                                                                                                                                      <label> ١٩ -مكان الاصدار: </label>

                                                                                                                                                                                       <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                                                                    </div>

                                                                                                                                                                                </td>


                                                                                                                                                                            </tr>

                                                                                                                                                                            <tr>

                                                                                                                                                                                <td>

                                                                                                                                                                                    <div style = 'padding: 15px;'>

                                                                                                                                                                                         <label> ٢٠ -اسماء الفاحصين: </label>

                                                                                                                                                                                          <textarea style = 'resize: none; border: none;'>

                                                                                                                                                                                           </textarea>

                                                                                                                                                                                       </div>

                                                                                                                                                                                   </td>

                                                                                                                                                                                   <td colspan = '2'>

                                                                                                                                                                                        <div style = 'padding: 15px;'>

                                                                                                                                                                                             <label> ٢١ -اسم المسئول المفوض  : </label>

                                                                                                                                                                                               <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                                                                                <br>

                                                                                                                                                                                                <label> ٢٢ -الرقم التعريفي: </label>

                                                                                                                                                                                               <input style = 'border: none;'>

                                                                                                                                                                                                <br>

                                                                                                                                                                                                <label> ٢٣ -ميناء الشحن: </label>

                                                                                                                                                                                               <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                                                                            </div>

                                                                                                                                                                                        </td>

                                                                                                                                                                                        <td>

                                                                                                                                                                                            <div style = 'padding:15px'>

                                                                                                                                                                                                 <label> ٢٤  -ختم المنظومه: </label>

                                                                                                                                                                                                <br>

                                                                                                                                                                                                <label> ٢٥ -التوقيع   : </label>

                                                                                                                                                                                              </div>

                                                                                                                                                                                          </td>


                                                                                                                                                                                      </tr>

                                                                                                                                                                                  </tbody></table>


                                                                                                                                                                              </div></body></html> ";
             #endregion
             var PDF = Renderer.RenderHtmlAsPdf(s);
             var OutputPath = "D:\\work\\tfs_5-2019\\PlantQuar.Web\\Upload\\Certificat\\maiiiiii";
             PDF.SaveAs(OutputPath);*/
            #endregion
            return View(model);
        }


        public ActionResult IndexNew(long certificateId, bool isPrint)
        {
            Context = new CertificateDTO();
            //change isprinted to true


            var res = APIHandeling.getData(apiName + "?certificateId=" + certificateId);
            var model = res.Content.ReadAsAsync<CertificateDTO>().Result;//object
            ViewBag.certificateId = certificateId;
            ViewBag.isprint = isPrint;

            //noura
            try
            {


                var path = model.data_CertificatesFiles.Select(a => a.FilePath).FirstOrDefault();
                var countFiles = model.data_CertificatesFiles.Select(a => a.FilePath).Count();
                if (path != null)
                {
                    string[] FileDir = path.Split('/');

                    var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();

                    String SharedIp = DomainPath;

                    String All_path = @"\\" + SharedIp + "\\" + path;
                    All_path = All_path.Replace('/', '\\');


                    var absolutePath = HttpContext.Server.MapPath(All_path);
                    if (!System.IO.File.Exists(absolutePath))
                    {
                        string[] FileArr = All_path.Split('.');
                        var FileArr_All_path = All_path.Split();
                        string extention = FileArr[FileArr.Length - 1];
                        byte[] fileBytes = System.IO.File.ReadAllBytes(All_path);

                        if (extention == "pdf" || extention == "PDF")
                        {
                            return File(fileBytes, "application/pdf");
                        }
                        else
                        {

                            string directoryName = Path.GetDirectoryName(All_path);
                            string fileName = Path.GetFileName(All_path);
                            string destinationFilePath = HttpContext.Server.MapPath(@"~\Upload\CertificateAttachment");


                            try
                            {
                                foreach (String filename in Directory.GetFiles(directoryName, "*.*", SearchOption.AllDirectories))
                                {
                                    //your code here   
                                    System.IO.File.Copy(filename, filename.Replace(directoryName, destinationFilePath), true);
                                }

                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }

                            // System.IO.File.Copy(newPath, newPath.Replace(sourceFilePath, destinationFilePath), true);
                            //Now Create all of the directories
                            //  \\10.7.7.242\plant\Certificates\2023\1\PlantFiles279\Image_273.jpg
                            //foreach (string dirPath in Directory.GetDirectories(sourceFilePath, "*", SearchOption.AllDirectories))
                            // foreach (string dirPath in Directory.GetDirectories(@"\\10.7.7.242\plant\Certificates\2023\1\PlantFiles279\Image_273.jpg", "*", SearchOption.AllDirectories))
                            //{
                            //Directory.CreateDirectory(dirPath.Replace(sourceFilePath, destinationFilePath));
                            //}
                            //Create array of links of images
                            //

                            //Copy all the files & Replaces any files with the same name
                            //foreach (string newPath in Directory.GetFiles(sourceFilePath, "*.*", SearchOption.AllDirectories))
                            //{
                            //    System.IO.File.Copy(newPath, newPath.Replace(sourceFilePath, destinationFilePath), true);
                            //}
                            // return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                        }
                    }


                }

            }
            catch (Exception ex)
            {

            }

            // string sourceFilePath = @"D:\Certifacte1";
            //string destinationFilePath = @"D:\plant\23-10-2023\Upload\CertificateFile.txt";
            //  string destinationFilePath = @"D:\plant\23-10-2023\PlantQuar.WEB\Upload\CertificateAttachment";


            //System.IO.File.Copy(sourceFilePath, destinationFilePath);



            ////Now Create all of the directories
            //foreach (string dirPath in Directory.GetDirectories(sourceFilePath, "*", SearchOption.AllDirectories))
            //{
            //    Directory.CreateDirectory(dirPath.Replace(sourceFilePath, destinationFilePath));
            //}
            ////Create array of links of images
            ////

            ////Copy all the files & Replaces any files with the same name
            //foreach (string newPath in Directory.GetFiles(sourceFilePath, "*.*", SearchOption.AllDirectories))
            //{
            //    System.IO.File.Copy(newPath, newPath.Replace(sourceFilePath, destinationFilePath), true);
            //}
            //// Get a list of all the image files in the Images directory.
            ////var path = destinationFilePath;
            //string[] imageFiles = Directory.GetFiles(destinationFilePath);

            //// Add the list of image files to the ViewBag.
            //ViewBag.ImagePaths = imageFiles;


            //System.IO.File.Copy("D:/plant", "D:/noura old");
            Context = model;
            #region c
            /* var Renderer = new IronPdf.HtmlToPdf();
             #region m
             string s = @"<html>
 <style>
     table,tr,td{
         border:1px solid;
     }
 </style>
 <body><div id='cer' dir='rtl' style='width: 800px;'>
         <table dir = 'rtl' style = 'width:750px;'>

                <tbody><tr>


                    <td colspan = '2' rowspan = '2'>

                           <div style = 'display: inline-block;padding: 15px;'><img style = 'width: 155px;left:15px; position: relative;' src = '/Content/NewDesign/images/logo-capq.png'></div>

                                  <div style = 'display: inline-block;padding: 15px 68px 15px 0px;position: relative;top: 26px'>

                                       <p> جمهوريه مصر العربيه</p>

                                          <p> وزارة الزراعة واستصلاح الاراضي </p>

                                             <p> الادارة المركزية للحجر الزراعي </p>

                                            </div>

                                        </td>

                                        <td colspan = '2'>

                                             <div style = 'padding: 15px;'>

                                                  <h3> شهادة صحة نباتية</h3>

                                                     <p>
                                                         رقم: 0628652
                                                     </p>

                                                 </div>

                                             </td>


                                         </tr>

                                         <tr>

                                             <td colspan = '2'>

                                                  <div style = 'padding: 15px;'>

                                                       <label> ١ -الي المنطقة الوطنية لوقاية النباتات في: </label>

                                                       <textarea id = 'Orag_Plant' style = 'resize: none; border: none;'></textarea>

                                                      </div>


                                                  </td>

                                              </tr>

                                              <tr>

                                                  <td colspan = '2'>

                                                       <div style = 'padding: 15px;'>

                                                            <label style = 'display:block'> ٢ -اسم المصدر وعنوانه : </label>

                                                               <textarea id = 'Exporter_NamePrint' style = 'resize: none; border: none;'></textarea>

                                                                  <br>

                                                                  <textarea id = 'Exporter_AddressPrint' style = 'resize: none; border: none;'></textarea>

                                                                 </div>

                                                             </td>

                                                             <td colspan = '2'>

                                                                  <div style = 'padding: 15px;'>

                                                                       <label style = 'display:block'> ٣ -اسم المرسل اليه وعنوانه حسب بياناته: </label>

                                                                         <textarea id = 'Reciever_NamePrint' style = 'resize: none; border: none;'></textarea>

                                                                            <br>

                                                                            <textarea id = 'ImportCompany_AddressPrint' style = 'resize: none; border: none;'></textarea>

                                                                           </div>

                                                                       </td>

                                                                   </tr>


                                                                   <tr>

                                                                       <td colspan = '4'>

                                                                            <div style = 'padding:15px;text-align: center;'>

                                                                                 <label> وصف الشحنة </label>

                                                                                </div>

                                                                            </td>

                                                                        </tr>

                                                                        <tr>

                                                                            <td>

                                                                                <div style = 'padding: 15px;'>

                                                                                     <label> ٤ -وسيله النقل حسب البيانات: </label>

                                                                                      <textarea id = 'Transport_Mean_NamePrint' style = 'resize: none; border: none;'></textarea>

                                                                                     </div>

                                                                                 </td>

                                                                                 <td>

                                                                                     <div style = 'padding: 15px;'>

                                                                                          <label> ٥ -نقطة الدخول حسب البيانات:</label>

                                                                                            <textarea id = 'port_arrive_NamePrint' style = 'resize: none; border: none;'></textarea>

                                                                                           </div>

                                                                                       </td>

                                                                                       <td>

                                                                                           <div style = 'padding: 15px;'>

                                                                                                <label> ٦ -مكان المنشأ: </label>

                                                                                                 <textarea style = 'resize: none; border: none;'> مصر </textarea>

                                                                                              </div>

                                                                                          </td>

                                                                                          <td>

                                                                                              <div style = 'padding: 15px;'>

                                                                                                   <label> ٧ -العلامات المميزه: </label>

                                                                                                    <textarea id = 'Marks' style = 'resize: none; border: none;'></textarea>

                                                                                                   </div>

                                                                                               </td>

                                                                                           </tr>

                                                                                           <tr>

                                                                                               <td>

                                                                                                   <div style = 'padding: 15px;'>

                                                                                                        <label> ٨ -عدد الطرود و وصفها: </label>

                                                                                                         <textarea id = 'PackageCountPrint' style = 'resize: none; border: none;'></textarea>

                                                                                                        </div>

                                                                                                    </td>

                                                                                                    <td colspan = '2'>

                                                                                                         <div style = 'padding: 15px;'>

                                                                                                              <label style = 'display:block'> ٩ -اسم المنتج وكميتة حسب بيناته:</label>

                                                                                                                  <textarea id = 'categoriesPrint' style = 'resize: none; border: none;'>                    </textarea>

                                                                                                                 </div>

                                                                                                             </td>

                                                                                                             <td>

                                                                                                                 <div style = 'padding: 15px;'>

                                                                                                                      <label> ١٠ -الاسماء العلميه للنباتات : </label>

                                                                                                                        <textarea id = 'ScientificNamePrint' style = 'resize: none; border: none;'>                    </textarea>

                                                                                                                       </div>

                                                                                                                   </td>


                                                                                                               </tr>

                                                                                                               <tr>


                                                                                                                   <td colspan = '4'>

                                                                                                                        <div style = 'padding: 15px;'>

                                                                                                                             <p> نشهد بأن النباتات او المنتجات النباتية أو البنودالأخري الخاضعه للوائح الصحة النباتية الموصوفة ضمنا قد فحصت و/ او اختبرت طبقا للاجراءات المعتمدة الملائمة ووجدت خالية من أفات الحجر الزراعي التى حددها الطرف المتعاقد المستورد وفقا لاشترطات الصحة النباتية لدى الطرف المتعاقد المستورد بما في ذلك الاشترطات الخاصه بالافات غير الحجرية الخاضعه للوائح </p>

                                                                                                                             </div>

                                                                                                                         </td>

                                                                                                                     </tr>

                                                                                                                     <tr>


                                                                                                                         <td colspan = '4'>

                                                                                                                              <div style = 'padding: 15px;text-align:center'>


                                                                                                                                   <label> المعالجة للتطهير من التلوث و/ أو الاصابه </label>

                                                                                                                                   </div>

                                                                                                                               </td>

                                                                                                                           </tr>

                                                                                                                           <tr>

                                                                                                                               <td>

                                                                                                                                   <div style = 'padding: 15px;'>

                                                                                                                                        <label> ١١ -التاريخ : </label>

                                                                                                                                          <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                       </div>

                                                                                                                                   </td>

                                                                                                                                   <td>

                                                                                                                                       <div style = 'padding: 15px;'>

                                                                                                                                            <label> ١٢ -المعالجة : </label>

                                                                                                                                              <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                           </div>

                                                                                                                                       </td>

                                                                                                                                       <td>

                                                                                                                                           <div style = 'padding: 15px;'>

                                                                                                                                                <label> ١٣ -الكيماويات(الماده الفعاله) : </label>

                                                                                                                                                  <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                               </div>

                                                                                                                                           </td>

                                                                                                                                           <td>

                                                                                                                                               <div style = 'padding: 15px;'>

                                                                                                                                                    <label> ١٤ -التركيز : </label>

                                                                                                                                                      <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                                   </div>

                                                                                                                                               </td>

                                                                                                                                           </tr>

                                                                                                                                           <tr>

                                                                                                                                               <td colspan = '2'>

                                                                                                                                                    <div style = 'padding: 15px;'>

                                                                                                                                                         <label style = 'display:block'> ١٥ -مدة التعرض ودرجة الحراره: </label>

                                                                                                                                                            <textarea style = 'resize: none; border: none;'> </textarea>

                                                                                                                                                         </div>

                                                                                                                                                     </td>

                                                                                                                                                     <td colspan = '2'>

                                                                                                                                                          <div style = 'padding: 15px;'>

                                                                                                                                                               <label style = 'display:block'> ١٦ -معلومات اخري: </label>

                                                                                                                                                                  <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                                               </div>

                                                                                                                                                           </td>

                                                                                                                                                       </tr>

                                                                                                                                                       <tr>


                                                                                                                                                           <td colspan = '4'>

                                                                                                                                                                <div style = 'padding:15px;text-align:center'>

                                                                                                                                                                     <label> اقرار اضافي </label>

                                                                                                                                                                    </div>

                                                                                                                                                                </td>

                                                                                                                                                            </tr>

                                                                                                                                                            <tr>

                                                                                                                                                                <td colspan = '1'>

                                                                                                                                                                     <div style = 'padding: 15px;'>

                                                                                                                                                                          <label> ١٧ -تاريخ الفحص: </label>

                                                                                                                                                                           <textarea id = 'Check_DatePrint' style = 'resize: none; border: none;'></textarea>

                                                                                                                                                                          </div>

                                                                                                                                                                      </td>

                                                                                                                                                                      <td colspan = '2'>

                                                                                                                                                                           <div style = 'padding: 15px;'>

                                                                                                                                                                                <label style = 'display:block'> ١٨ -تاريخ الاصدار: </label>

                                                                                                                                                                                   <textarea style = 'resize: none; border: none;'> 23 / 09 / 2019 </textarea>

                                                                                                                                                                                </div>

                                                                                                                                                                            </td>

                                                                                                                                                                            <td colspan = '1'>

                                                                                                                                                                                 <div style = 'padding: 15px;'>

                                                                                                                                                                                      <label> ١٩ -مكان الاصدار: </label>

                                                                                                                                                                                       <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                                                                    </div>

                                                                                                                                                                                </td>


                                                                                                                                                                            </tr>

                                                                                                                                                                            <tr>

                                                                                                                                                                                <td>

                                                                                                                                                                                    <div style = 'padding: 15px;'>

                                                                                                                                                                                         <label> ٢٠ -اسماء الفاحصين: </label>

                                                                                                                                                                                          <textarea style = 'resize: none; border: none;'>

                                                                                                                                                                                           </textarea>

                                                                                                                                                                                       </div>

                                                                                                                                                                                   </td>

                                                                                                                                                                                   <td colspan = '2'>

                                                                                                                                                                                        <div style = 'padding: 15px;'>

                                                                                                                                                                                             <label> ٢١ -اسم المسئول المفوض  : </label>

                                                                                                                                                                                               <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                                                                                <br>

                                                                                                                                                                                                <label> ٢٢ -الرقم التعريفي: </label>

                                                                                                                                                                                               <input style = 'border: none;'>

                                                                                                                                                                                                <br>

                                                                                                                                                                                                <label> ٢٣ -ميناء الشحن: </label>

                                                                                                                                                                                               <textarea style = 'resize: none; border: none;'></textarea>

                                                                                                                                                                                            </div>

                                                                                                                                                                                        </td>

                                                                                                                                                                                        <td>

                                                                                                                                                                                            <div style = 'padding:15px'>

                                                                                                                                                                                                 <label> ٢٤  -ختم المنظومه: </label>

                                                                                                                                                                                                <br>

                                                                                                                                                                                                <label> ٢٥ -التوقيع   : </label>

                                                                                                                                                                                              </div>

                                                                                                                                                                                          </td>


                                                                                                                                                                                      </tr>

                                                                                                                                                                                  </tbody></table>


                                                                                                                                                                              </div></body></html> ";
             #endregion
             var PDF = Renderer.RenderHtmlAsPdf(s);
             var OutputPath = "D:\\work\\tfs_5-2019\\PlantQuar.Web\\Upload\\Certificat\\maiiiiii";
             PDF.SaveAs(OutputPath);*/
            #endregion
            return View(model);
        }

        public ActionResult Index1(long certificateId, bool isPrint)
        {
            Context = new CertificateDTO();
            //change isprinted to true


            var res = APIHandeling.getData(apiName + "?certificateId=" + certificateId);
            var model = res.Content.ReadAsAsync<CertificateDTO>().Result;//object
            ViewBag.certificateId = certificateId;
            ViewBag.isprint = isPrint;
            ViewBag.FullName = Session["FullName"];

            ViewBag.Outlet_Name = Session["Outlet_Name"] ;
            ViewBag.FullName_En =Session["FullName_En"] ;
            ViewBag.Outlet_Name_En = Session["Outlet_Name_En"] ;
            ViewBag.EmpId = Session["EmpId"];



            try
            {


                var path = model.data_CertificatesFiles.Select(a => a.FilePath).FirstOrDefault();
                var countFiles = model.data_CertificatesFiles.Select(a => a.FilePath).Count();
                if (path != null)
                {
                    string[] FileDir = path.Split('/');

                    var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();

                    String SharedIp = DomainPath;

                    String All_path = @"\\" + SharedIp + "\\" + path;
                    All_path = All_path.Replace('/', '\\');


                    var absolutePath = HttpContext.Server.MapPath(All_path);
                    if (!System.IO.File.Exists(absolutePath))
                    {
                        string[] FileArr = All_path.Split('.');
                        var FileArr_All_path = All_path.Split();
                        string extention = FileArr[FileArr.Length - 1];
                        byte[] fileBytes = System.IO.File.ReadAllBytes(All_path);

                        if (extention == "pdf" || extention == "PDF")
                        {
                            return File(fileBytes, "application/pdf");
                        }
                        else
                        {

                            string directoryName = Path.GetDirectoryName(All_path);
                            string fileName = Path.GetFileName(All_path);
                            string destinationFilePath = HttpContext.Server.MapPath(@"~\Upload\CertificateAttachment");


                            try
                            {
                                foreach (String filename in Directory.GetFiles(directoryName, "*.*", SearchOption.AllDirectories))
                                {
                                    //your code here   
                                    System.IO.File.Copy(filename, filename.Replace(directoryName, destinationFilePath), true);
                                }

                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }


                        }
                    }


                }

            }
            catch (Exception ex)
            {

            }

            
            return View(model);
        }

        [HttpPost]
        public ActionResult printCertificate(long certificateId)
        {

            AcceptCertificate accept = new AcceptCertificate();
            accept.certificateId = certificateId;
            accept.User_Updation_Date = DateTime.Now;
            accept.User_Updation_Id = (short)Session["UserId"];

            var print = APIHandeling.Put("CertificateList?print=true", accept);
            return Json("ok");
        }
        [HttpPost]
        public ActionResult Save(Marks model)
        {
            //var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            //if (ModelState.IsValid)
            //{
            //    Ex_Im_CheckRequest_CertificateDTO model = new Ex_Im_CheckRequest_CertificateDTO();

            //    model.Publish_Date = DateTime.Now;
            //   // model.Country_Id = Context.data.co;
            //    model.Is_Export = true;
            //    model.Certificate_Type_Id =28;
            //    model.htmlCode = html;
            //    var res = APIHandeling.Post(apiName, model);
            //    return RedirectToAction("Index");
            //}
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ViewContainer(string ExaminationNumber)
        {

            return PartialView();
        }

        public JsonResult putprintCertificates(long ID)
        {
            try
            {
                AcceptCertificate accept = new AcceptCertificate();
                accept.certificateId = ID;
                accept.User_Updation_Date = DateTime.Now;
                accept.User_Updation_Id = (short)Session["UserId"];
                accept.IsAccepted = false;
                accept.ISPrint = 1;

                var res = APIHandeling.Put(apiName, accept);

                return Json("success");
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
    public class Marks
    {
        public Marks()
        {
            Lots = new List<Lot>();
        }
        public string plant { get; set; }
        public string Cat { get; set; }
        public string Part { get; set; }
        public string Status { get; set; }
        public string Purpose { get; set; }
        public string ScientificName { get; set; }
        public List<Lot> Lots { get; set; }
    }
    public class Lot
    {
        public string LotNumber { get; set; }
        public int PackageCount { get; set; }
    }
}