using System;
using System.IO;

using System.Net.Http;
using System.Web;
using System.Web.Mvc;

using System.Web.Http;
using System.Linq;
using System.Configuration;

namespace PlantQuar.WEB.API
{
    public class FileUpload_SaveDataController:ApiController
    {
        //For Web
        public string Upload_File_Data(HttpPostedFileBase postedFileBase, string Folder_Name)
        {
            try
            {
                var br = new BinaryReader(postedFileBase.InputStream);
                return Upload_File_Data_Array(br.ReadBytes(postedFileBase.ContentLength), Folder_Name,
                   Path.GetExtension(postedFileBase.FileName));
            }
            catch(Exception ex)
            {
                //uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.StackTrace, MethodBase.GetCurrentMethod().Name, Device_Info);

                return ex.Message;
            }
        }

        public string Upload_File_Path_NetworkShare(HttpPostedFileBase file,string Type_Imge)
        {
            try
            {

                var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();
                //var html = richTxt.Replace("src=\"", "src=\"" + DomainPath);
                string fname;
                fname = Path.GetFileName(file.FileName);
                NetworkShare.DisconnectFromShare(@"\\" + DomainPath + "\\plant", true); //Disconnect in case we are currently connected with our credentials;
                NetworkShare.ConnectToShare(@"\\" + DomainPath + "\\plant", "administrator", "asd@123"); //Connect with the new credentials
                string AllPath = "";


                
                //var html = richTxt.Replace("src=\"", "src=\"" + DomainPath);
              
                
                //NetworkShare.DisconnectFromShare(@"\\" + DomainPath + "\\plant", true); //Disconnect in case we are currently connected with our credentials;
                //NetworkShare.ConnectToShare(@"\\" + DomainPath + "\\plant", "administrator", "asd@123"); //Connect with the new credentials
                //if (!Directory.Exists(@"\\" + DomainPath + "\\plant\\Test56"))
                //    Directory.CreateDirectory(@"\\" + DomainPath + "\\plant\\Test56");

                //file.SaveAs(Path.Combine(@"\\" + DomainPath + "\\plant\\Test56\\" + fname));






                #region وارد           
                if (Type_Imge.Split('_')[0] == "Import")
                {
                    AllPath = @"\\" + DomainPath + "\\plant\\Import";
                    if (!Directory.Exists(AllPath))
                    {
                        Directory.CreateDirectory(AllPath);
                    }

                    if (!Directory.Exists(AllPath + "\\CheckRequest"))
                    {
                        Directory.CreateDirectory(AllPath + "\\CheckRequest");
                    }


                    if (!Directory.Exists(AllPath + "\\SampleData"))
                    {
                        Directory.CreateDirectory(AllPath + "\\SampleData");
                    }

                    if (!Directory.Exists(AllPath + "\\Permission"))
                    {
                        Directory.CreateDirectory(AllPath + "\\Permission");
                    }

                    string Path_year = Create_Year("\\plant\\Import\\" + Type_Imge.Split('_')[1]);

                    AllPath = Path_year + fname;


                    file.SaveAs(Path.Combine(DomainPath +"\\"+AllPath));

                }
                #endregion
                #region هيئة           
               else if (Type_Imge.Split('_')[0] == "Organization")
                {
                    AllPath = @"\\" + DomainPath + "\\plant\\Organization";
                    if (!Directory.Exists(AllPath))
                    {
                        Directory.CreateDirectory(AllPath);
                    }
                    if (!Directory.Exists(AllPath + "\\"+ Type_Imge.Split('_')[1]))
                    {
                        Directory.CreateDirectory(AllPath + "\\"+Type_Imge.Split('_')[1]);
                    }
                    string Path_year = Create_Year("\\plant\\Organization\\" + Type_Imge.Split('_')[1]);

                    AllPath = Path_year + fname;


                    file.SaveAs(Path.Combine(DomainPath + "\\" + AllPath));

                }
                #endregion
                #region مزرعة           
                else if (Type_Imge.Split('_')[0] == "Farm")
                {
                    AllPath = @"\\" + DomainPath + "\\plant\\Farm";
                    if (!Directory.Exists(AllPath))
                    {
                        Directory.CreateDirectory(AllPath);
                    }
                    string Path_year = Create_Year("\\plant\\Farm\\");

                    if (!Directory.Exists(@"\\" + DomainPath + Path_year + "\\" + Type_Imge.Split('_')[1]))
                    {
                        Directory.CreateDirectory(@"\\" + DomainPath + Path_year + "\\" + Type_Imge.Split('_')[1]);
                    }


                    AllPath = Path_year +"\\" + Type_Imge.Split('_')[1] + "\\" + fname;


                    file.SaveAs(Path.Combine(DomainPath + "\\" + AllPath));

                }
                #endregion
                #region محطة           
                else if (Type_Imge.Split('_')[0] == "Station")
                {
                    AllPath = @"\\" + DomainPath + "\\plant\\Station";
                    if (!Directory.Exists(AllPath))
                    {
                        Directory.CreateDirectory(AllPath);
                    }
                    if (!Directory.Exists(AllPath + "\\" + Type_Imge.Split('_')[1]))
                    {
                        Directory.CreateDirectory(AllPath + "\\" + Type_Imge.Split('_')[1]);
                    }
                    string Path_year = Create_Year("\\plant\\Station\\" + Type_Imge.Split('_')[1]);

                    AllPath = Path_year + fname;


                    file.SaveAs(Path.Combine(DomainPath + "\\" + AllPath));

                }
                #endregion
                #region أشخاص           
                else if (Type_Imge.Split('_')[0] == "Person")
                {
                    AllPath = @"\\" + DomainPath + "\\plant\\Person";
                    if (!Directory.Exists(AllPath))
                    {
                        Directory.CreateDirectory(AllPath);
                    }
                    if (!Directory.Exists(AllPath + "\\" + Type_Imge.Split('_')[1]))
                    {
                        Directory.CreateDirectory(AllPath + "\\" + Type_Imge.Split('_')[1]);
                    }
                    string Path_year = Create_Year("\\plant\\Person\\" + Type_Imge.Split('_')[1]);

                    AllPath = Path_year + fname;


                    file.SaveAs(Path.Combine(DomainPath + "\\" + AllPath));

                }
                #endregion
                #region شركة           
                else if (Type_Imge.Split('_')[0] == "Company")
                {
                    AllPath = @"\\" + DomainPath + "\\plant\\Company";
                    if (!Directory.Exists(AllPath))
                    {
                        Directory.CreateDirectory(AllPath);
                    }
                    if (!Directory.Exists(AllPath + "\\" + Type_Imge.Split('_')[1]))
                    {
                        Directory.CreateDirectory(AllPath + "\\" + Type_Imge.Split('_')[1]);
                    }
                    string Path_year = Create_Year("\\plant\\Company\\" + Type_Imge.Split('_')[1]);

                    AllPath = Path_year + fname;


                    file.SaveAs(Path.Combine(DomainPath + "\\" + AllPath));

                }
                #endregion
                #region نبات           
                else if (Type_Imge.Split('_')[0] == "Item")
                {
                    AllPath = @"\\" + DomainPath + "\\plant\\Item";
                    if (!Directory.Exists(AllPath))
                    {
                        Directory.CreateDirectory(AllPath);
                    }
                    string Path_year = Create_Year("\\plant\\Item\\");

                    if (!Directory.Exists(@"\\" + DomainPath + Path_year + "\\" + Type_Imge.Split('_')[1]))
                    {
                        Directory.CreateDirectory(@"\\" + DomainPath + Path_year);
                    }


                    AllPath = Path_year + "\\" + fname;


                    file.SaveAs(Path.Combine(DomainPath + "\\" + AllPath));

                    //AllPath = @"\\" + DomainPath + "\\plant\\Item";
                    //if (!Directory.Exists(AllPath))
                    //{
                    //    Directory.CreateDirectory(AllPath);
                    //}

                    //if (!Directory.Exists(AllPath + "\\1"))
                    //{
                    //    Directory.CreateDirectory(AllPath + "\\1");
                    //}


                    //if (!Directory.Exists(AllPath + "\\2"))
                    //{
                    //    Directory.CreateDirectory(AllPath + "\\2");
                    //}

                    //if (!Directory.Exists(AllPath + "\\3"))
                    //{
                    //    Directory.CreateDirectory(AllPath + "\\3");
                    //}

                    //if (!Directory.Exists(AllPath + "\\4"))
                    //{
                    //    Directory.CreateDirectory(AllPath + "\\4");
                    //}

                    //if (!Directory.Exists(AllPath + "\\5"))
                    //{
                    //    Directory.CreateDirectory(AllPath + "\\5");
                    //}

                    //if (!Directory.Exists(AllPath + "\\6"))
                    //{
                    //    Directory.CreateDirectory(AllPath + "\\6");
                    //}
                    //string Path_year = Create_Year("\\plant\\Item\\" + Type_Imge.Split('_')[1]);

                    //AllPath = Path_year + fname;


                    //file.SaveAs(Path.Combine(DomainPath + "\\" + AllPath));
                }
                #endregion
                return AllPath;
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
                return null;
            }
        }

        private string Create_Year(string Path_year)
        {
            var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            if (!Directory.Exists(DomainPath +"\\"+Path_year + "\\" + year))
            {
                Directory.CreateDirectory(DomainPath + "\\" + Path_year + "\\" + year);
            }               

            if (!Directory.Exists(DomainPath + "\\" + Path_year + "\\" + year + "\\" + month))
            {
                Directory.CreateDirectory(DomainPath + "\\" + Path_year + "\\" + year + "\\" + month);
            }
            return Path_year +"\\"+ year + "\\" + month + "\\" ;
        }

        public Boolean IfExisit_DeleteFile(string fPath)
        {
            var exisit = System.IO.File.Exists(fPath);
            if (exisit)
            {
                System.IO.File.Delete(fPath);
            }

            return exisit;
        }

        public static string Upload_File_Data_Array(byte[] picData, string Folder_Name, string FileExtension)
        {
            try
            {
                // For Android Group
                DateTime date = DateTime.Now;
                string fName = Path.GetFileName("fle_" +
                     date.Year + date.DayOfYear + date.Hour + date.Minute + date.Second
                    );
                //var ext = Path.GetExtension(File_Url.FileName);
                string varlname = "Upload/" + Folder_Name;
                 string fPathlocal = HttpContext.Current.Server.MapPath("~/" + varlname);
               string physicalPath = ConfigurationManager.AppSettings["physicalPath"].ToString();
                string fPath = Path.Combine(physicalPath, varlname);
                //file://10.5.1.41/upload/Farm_SampleData/fle_2021110104536.pdf
                // string fPath = Server.MapPath("10.5.1.6/upload/" + varlname);
                if (!Directory.Exists(fPathlocal))
                {
                    Directory.CreateDirectory(fPathlocal);
                }
                if (!Directory.Exists(fPath))
                {
                    Directory.CreateDirectory(fPath);
                }
                string fPathName = Path.Combine(fPath, fName) + FileExtension;
                string fPathNameLocal = Path.Combine(fPathlocal, fName) + FileExtension;
                //if (!File.Exists(fPathName))
                //{
                //    File.Create(fPathName);
                //}
                //postedFileBase.SaveAs(fPathName);
                File.WriteAllBytes(fPathName, picData);
                File.WriteAllBytes(fPathNameLocal, picData);
                return varlname + "/" + fName + FileExtension;
                //return fPathName;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public static  string GetLatestFile(string FolderName)
        {
            var directory = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/" + FolderName));
            var fileLastUpdated = (from f in directory.GetFiles()
                                   orderby f.LastWriteTime descending
                                   select f).First();
            return fileLastUpdated.Name;
        }
    }
}