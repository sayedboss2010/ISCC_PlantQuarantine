using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace PlantQuar.DTO.HelperClasses
{
    public static class Upload_File
    {
        public static string Upload_File_Data(HttpPostedFileBase File_Url, string Folder_Name)
        {
            try
            {
                DateTime date = DateTime.Now;


                string fName = Path.GetFileName("fle_" +
                     date.Year + date.DayOfYear + date.Hour + date.Minute + date.Second
                    );
               // var ext = Path.GetExtension(File_Url.FileName);
                string varlname = "Upload/" + Folder_Name;
                string fPath = HttpContext.Current.Server.MapPath("~/" + varlname);

                if (!Directory.Exists(fPath))
                {
                    Directory.CreateDirectory(fPath);
                }
                string fPathName = Path.Combine(fPath, fName);
                File_Url.SaveAs(fPathName);
                return varlname + "/" + fName;

                //string fName = "PlantIMG" + DateTime.Now.Year + DateTime.Now.DayOfYear + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                //var ext = Path.GetExtension(Picture1.FileName);
                ////path inside a folder
                //string fPath = Server.MapPath("~/Upload/Plant/");

                //if (!Directory.Exists(fPath))
                //{
                //    Directory.CreateDirectory(fPath);
                //}
                //string fPathName = Path.Combine(fPath, fName + ext);

            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return null;
            }
        }

        //public static string Upload_File_Data(byte[] picData, string Folder_Name, string FileExtension)
        //{
        //    try
        //    {
        //        DateTime date = DateTime.Now;
        //        string fName = Path.GetFileName("fle_" +
        //             date.Year + date.DayOfYear + date.Hour + date.Minute + date.Second
        //            );
        //        //var ext = Path.GetExtension(File_Url.FileName);
        //        string varlname = "Upload/" + Folder_Name;
        //        string fPath = HttpContext.Current.Server.MapPath("~/" + varlname);

        //        if (!Directory.Exists(fPath))
        //        {
        //            Directory.CreateDirectory(fPath);
        //        }
        //        string fPathName = Path.Combine(fPath, fName)+FileExtension;
        //        File.WriteAllBytes(fPathName, picData);
        //        return varlname + "/" + fName+ FileExtension;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

       
    }
}