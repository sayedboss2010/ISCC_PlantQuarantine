using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebGrid_Image_Path_Database_EF_MVC
{
    public partial class DisplayImge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //string fileName = @"\\10.7.7.244\plant\Test56\Image_11125.png";
                try
                {
                    string fileName = @"" + Session["Path_Server"].ToString() + "";
                    Image1.Visible = fileName != "0";
                    if (fileName != "0")
                    {
                        Image1.ImageUrl = "/Areas/ASP/Controllers/HandlerCS.ashx?FileName=" + fileName;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
              

               

                //string[] images = Directory.GetFiles(@"\\10.7.7.244\plant\Test56");
                //foreach (string imageFile in images)
                //{
                //    ddlImages.Items.Add(new ListItem(Path.GetFileName(imageFile), Path.GetFileName(imageFile)));
                //}
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString["FileName"]))
            {
                var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();
                string fileName = context.Request.QueryString["FileName"];
                fileName = DomainPath + fileName;
                string contentType = "image/" + Path.GetExtension(fileName).Replace(".", "");
                try
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            // Read the file and convert it to Byte Array.
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            br.Close();
                            fs.Close();

                            // Write the file to response Stream.                    
                            context.Response.ContentType = contentType;
                            context.Response.BinaryWrite(bytes);
                            context.Response.End();
                        }
                    }
                }
                catch (Exception)
                {


                }

            }



            //if (!string.IsNullOrEmpty(context.Request.QueryString["FileName"]))
            //{
            //    string filePath = @"\\10.7.7.244\plant\Test56\";
            //    string fileName = context.Request.QueryString["FileName"];
            //    string contentType = "image/" + Path.GetExtension(fileName).Replace(".", "");
            //    using (FileStream fs = new FileStream(filePath + fileName, FileMode.Open, FileAccess.Read))
            //    {
            //        using (BinaryReader br = new BinaryReader(fs))
            //        {
            //            // Read the file and convert it to Byte Array.
            //            byte[] bytes = br.ReadBytes((Int32)fs.Length);
            //            br.Close();
            //            fs.Close();

            //            // Write the file to response Stream.                    
            //            context.Response.ContentType = contentType;
            //            context.Response.BinaryWrite(bytes);
            //            context.Response.End();
            //        }
            //    }
            //}
        }

        //protected void FetchImage(object sender, EventArgs e)
        //{
        //    //string fileName = ddlImages.SelectedItem.Value;
        //    //Image1.Visible = fileName != "0";
        //    //if (fileName != "0")
        //    //{
        //    //    Image1.ImageUrl = "HandlerCS.ashx?FileName=" + fileName;
        //    //}
        //}
    }
}