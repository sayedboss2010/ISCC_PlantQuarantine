using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace PlantQuar.DTO.HelperClasses
{
    public static class API_HelperFunctions
    {
        public static HttpStatusCode getStatusCode(int state_code)
        {
            switch (state_code)
            {
                case -1:
                    return HttpStatusCode.InternalServerError;
                case 13:
                    //multiple simultaneous updates.
                    return HttpStatusCode.Conflict; //409 
                default:
                    return HttpStatusCode.Accepted;
            }
        }

        public static List<string> Get_DeviceInfo()
        {
            #region fz

            //string host = Dns.GetHostName();
            //IPHostEntry ip = Dns.GetHostEntry(host);
            //string IP=ip.AddressList[0].ToString();
            // var s = Dns.GetHostEntry(Dns.GetHostName());
            //Request.ServerVariables["LOCAL_ADDR"];
            string ipAddress = HttpContext.Current.Request.UserHostAddress;

            var request = HttpContext.Current.Request;
            string UserIp = ipAddress;// request.UserHostAddress;
            string Is_Web = (request.UserAgent != null) ? (!(request.UserAgent.IndexOf("andr", StringComparison.OrdinalIgnoreCase) >= 0)).ToString() : "true";

            string lang = (request.Headers.GetValues("lang") != null) ? request.Headers.GetValues("lang").First() : "1";
            #endregion
            return new List<string> { UserIp, Is_Web, lang };
        }
    }
}