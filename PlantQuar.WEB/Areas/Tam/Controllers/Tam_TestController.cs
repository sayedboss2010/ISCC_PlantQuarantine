using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Tam.Controllers
{
    public class Tam_TestController : Controller
    {
        // GET: Tam/Tam_Test
        public ActionResult Index()
        {
            Get_CardData();
            //await configSendGridasync(message);
            return View();
        }
        //private async Task configSendGridasync(IdentityMessage message)
        //{
        //    var myMessage = new SendGridMessage();
        //    myMessage.AddTo(message.Destination);
        //    myMessage.From = new System.Net.Mail.MailAddress(
        //                        "Joe@contoso.com", "Joe S.");
        //    myMessage.Subject = message.Subject;
        //    myMessage.Text = message.Body;
        //    myMessage.Html = message.Body;

        //    var credentials = new NetworkCredential(
        //               ConfigurationManager.AppSettings["mailAccount"],
        //               ConfigurationManager.AppSettings["mailPassword"]
        //               );

        //    // Create a Web transport for sending email.
        //    var transportWeb = new Web(credentials);

        //    // Send the email.
        //    if (transportWeb != null)
        //    {
        //        await transportWeb.DeliverAsync(myMessage);
        //    }
        //    else
        //    {
        //        Trace.TraceError("Failed to create Web transport.");
        //        await Task.FromResult(0);
        //    }
        //}
    

    private void Get_CardData()
        {
            //ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
            var _reader = new AppSettingsReader();
            string Complete_url = "/CardPortalServices/services/carddata?nid=101008643019";
            var _Url_IP = _reader.GetValue("Url_IP", Type.GetType("System.String")).ToString();
            Uri myUri = new Uri(_Url_IP + Complete_url);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(myUri);
            httpWebRequest.ContentType = "application/json";
            string authInfo = "momp:PA$$wordMOMP20";
            authInfo = Convert.ToBase64String(Encoding.ASCII.GetBytes(authInfo));
            //httpWebRequest.Method = Type_API;

            httpWebRequest.Headers.Add("Authorization", "Basic " + authInfo);
            httpWebRequest.Credentials = new NetworkCredential("momp", "PA$$wordMOMP20");

            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))       

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //String filename = "TestFile1.txt";





              
                
                //var  CardDataList = (from result
                //                  select new CardData()
                //                  {

                //                  }).ToList();
                //List<LoginModel> Userloginvalues;
                //Userloginvalues = result.ToList();

                #region MyRegion


                //DataTable dt = jDt.JsonStringToDataTable(result);
                //if (dt.Rows[0][0].ToString() == "200")
                //{
                //    try
                //    {
                //        CardDataList = (from DataRow dr in dt.Rows
                //                        select new CardData()
                //                        {
                //                            responseCode = dr["responseCode"].ToString(),
                //                            responseMsg = dr["responseMsg"].ToString(),
                //                            cardid = dr["cardid"].ToString(),
                //                            Card_Code = dr["cardCode"].ToString(),
                //                            Nid = dr["nid"].ToString(),
                //                            governorateCode = dr["governorateCode"].ToString(),
                //                            supplyDeptCode = dr["supplyDeptCode"].ToString(),
                //                            supplySubDeptCode = dr["supplySubDeptCode"].ToString(),
                //                            supplyCode = dr["supplyCode"].ToString(),
                //                            grocerCode = dr["grocerCode"].ToString(),
                //                            cardStatus = dr["cardStatus"].ToString(),
                //                            MemberName = dr["name"].ToString(),
                //                            totalNumber = dr["totalNumber"].ToString(),
                //                            beneficiaryNumber = dr["beneficiaryNumber"].ToString(),
                //                            cardType = dr["cardType"].ToString(),
                //                            govName = dr["govName"].ToString(),
                //                            deptName = dr["deptName"].ToString(),
                //                            subDeptName = dr["subDeptName"].ToString(),
                //                            officeName = dr["officeName"].ToString(),
                //                            statusName = dr["statusName"].ToString(),
                //                            groccerName = dr["groccerName"].ToString(),
                //                            cardTypeName = dr["cardTypeName"].ToString(),
                //                            errortype = dr["errortype"].ToString(),
                //                            mobile = dr["mobile"].ToString(),
                //                            memberid = dr["memberid"].ToString(),
                //                            cardId = dr["cardId"].ToString(),
                //                            relationaStatus = dr["relationaStatus"].ToString(),
                //                            serviceTamween = dr["serviceTamween"].ToString(),
                //                            serviceBread = dr["serviceBread"].ToString(),
                //                            status = dr["status"].ToString(),
                //                            name1 = dr["name1"].ToString(),
                //                            name2 = dr["name2"].ToString(),
                //                            name3 = dr["name3"].ToString(),
                //                            name4 = dr["name4"].ToString(),
                //                            sepFlag = dr["sepFlag"].ToString(),
                //                            addReason = dr["addReason"].ToString(),
                //                            newRelation = dr["newRelation"].ToString(),
                //                            actionFlag = dr["actionFlag"].ToString(),
                //                            actionOnMember = dr["actionOnMember"].ToString(),
                //                            beneficariesErrors = dr["beneficariesErrors"].ToString(),
                //                            sourceCardCode = dr["sourceCardCode"].ToString(),
                //                            hasErrors = dr["hasErrors"].ToString(),
                //                            addingFlag = dr["addingFlag"].ToString(),

                //                        }).ToList();
                //    }
                //    catch
                //    {

                //        CardDataList = (from DataRow dr in dt.Rows
                //                        select new CardData()
                //                        {
                //                            responseCode = dr["responseCode"].ToString(),
                //                            responseMsg = dr["responseMsg"].ToString(),
                //                            Nid = dr["nid"].ToString(),
                //                            Card_Code = dr["cardCode"].ToString(),
                //                            //relationaStatus = dr["relationaStatus"].ToString(),
                //                            MemberName = dr["name"].ToString(),
                //                            govName = dr["govName"].ToString(),
                //                            deptName = dr["deptName"].ToString(),
                //                            subDeptName = dr["subDeptName"].ToString(),
                //                            officeName = dr["officeName"].ToString(),
                //                            cardid = dr["cardid"].ToString(),
                //                            governorateCode = dr["governorateCode"].ToString(),
                //                            supplyDeptCode = dr["supplyDeptCode"].ToString(),
                //                            supplySubDeptCode = dr["supplySubDeptCode"].ToString(),
                //                            supplyCode = dr["supplyCode"].ToString(),
                //                            grocerCode = dr["grocerCode"].ToString(),
                //                            cardStatus = dr["cardStatus"].ToString(),
                //                            totalNumber = dr["totalNumber"].ToString(),

                //                            beneficiaryNumber = dr["beneficiaryNumber"].ToString(),
                //                            cardType = dr["cardType"].ToString(),
                //                        }).ToList();
                //    }

                //}
                //else
                //{
                //    CardDataList = (from DataRow dr in dt.Rows
                //                    select new CardData()
                //                    {
                //                        responseCode = dr["responseCode"].ToString(),
                //                        responseMsg = dr["responseMsg"].ToString(),
                //                    }).ToList();
                //}
                #endregion
            }
        }

        public class CardData
        {
            public string responseCode { get; set; }
            public string responseMsg { get; set; }

            public string cardid { get; set; }
            public string Card_Code { get; set; }
            public string Nid { get; set; }
            public string governorateCode { get; set; }
            public string supplyDeptCode { get; set; }
            public string supplySubDeptCode { get; set; }
            public string supplyCode { get; set; }
            public string grocerCode { get; set; }
            public string cardStatus { get; set; }
            public string MemberName { get; set; }
            public string totalNumber { get; set; }
            public string beneficiaryNumber { get; set; }
            public string cardType { get; set; }
            public string govName { get; set; }
            public string deptName { get; set; }
            public string subDeptName { get; set; }
            public string officeName { get; set; }
            public string statusName { get; set; }
            public string groccerName { get; set; }
            public string cardTypeName { get; set; }
            public string errortype { get; set; }
            public string mobile { get; set; }


            public string memberid { get; set; }
            public string cardId { get; set; }
            //public string nid { get; set; }
            //public string name { get; set; }
            public string relationaStatus { get; set; }
            public string serviceTamween { get; set; }
            public string serviceBread { get; set; }
            public string status { get; set; }
            public string name1 { get; set; }
            public string name2 { get; set; }
            public string name3 { get; set; }
            public string name4 { get; set; }
            public string sepFlag { get; set; }
            public string addReason { get; set; }
            public string newRelation { get; set; }
            public string actionFlag { get; set; }
            public string actionOnMember { get; set; }
            public string beneficariesErrors { get; set; }
            public string sourceCardCode { get; set; }
            public string hasErrors { get; set; }
            public string addingFlag { get; set; }

        }
    }
}