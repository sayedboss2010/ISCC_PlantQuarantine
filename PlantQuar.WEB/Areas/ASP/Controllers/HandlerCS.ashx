<%@ WebHandler Language="C#" Class="HandlerCS" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;

public class HandlerCS : IHttpHandler
{
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}