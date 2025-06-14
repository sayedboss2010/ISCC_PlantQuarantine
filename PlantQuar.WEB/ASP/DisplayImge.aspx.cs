using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebGrid_Image_Path_Database_EF_MVC
{
    public partial class WebForm1 : System.Web.UI.Page
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
                        Image1.ImageUrl = "HandlerCS.ashx?FileName=" + fileName;
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