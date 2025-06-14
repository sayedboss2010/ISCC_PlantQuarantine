using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.App_Start
{
    public sealed class User_Session
    {
        //use lazy keyword
        private static readonly Lazy<User_Session> Current = new 
            Lazy<User_Session>(() => new User_Session());
        #region User
        //public Int16 UserId { get; set; }
        //public long EmpId { get; set; }

        //public int GroupId { get; set; }
        //public Nullable<bool> CanPrint { get; set; }
        //public Nullable<bool> CanView { get; set; }
        //public int ModuleId { get; set; }
        //public int MenuId { get; set; }
        //public string FullName { get; set; }
        //public string Token { get; set; }
        #endregion

        public byte Language_IsAr { get; set; }
        public static User_Session GetInstance
        {
            get
            {
                #region Lazy
                return Current.Value;
                #endregion
            }
        }

        /// <summary>
        /// For Language ar-Eg->Ar , En-us->English
        /// </summary>
        //public string Language { get; set; }

        //public byte Language_IsAr { get; set; }

        public static int LogIn(CustomUserLogin user)
        {
            try
            {
                HttpContext.Current.Session["UserId"] = null;
                HttpContext.Current.Session["FullName"] = null;
                HttpContext.Current.Session["FullName_En"] = null;
                HttpContext.Current.Session["Outlet_Name_En"] = null;
                HttpContext.Current.Session["Token"] = null;
                HttpContext.Current.Session["EmpId"] = null;
                HttpContext.Current.Session["Language"] = "ar-Eg";
                HttpContext.Current.Session["Language_IsAr"] = 1;
                HttpContext.Current.Session["Outlet_Type"] = 1;
                HttpContext.Current.Session["Outlet_Hr"] = null;
                #region From API
                var res = APIHandeling.Post("Login_out", user);
                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                int StatusCode = int.Parse(lst["state_Code"].ToString());
                switch (StatusCode)
                {
                    case 1://Valid
                        {
                            JavaScriptSerializer ser = new JavaScriptSerializer();
                            var UserdataPlace = ser.Deserialize<User_LoginDTO>(lst["obj"].ToString());

                            User_Session Current = User_Session.GetInstance;
                            HttpContext.Current.Session["UserId"] = UserdataPlace.UserId;
                            HttpContext.Current.Session["FullName"]= UserdataPlace.FullName;
                            HttpContext.Current.Session["Token"] = UserdataPlace.Token;
                            HttpContext.Current.Session["EmpId"] = UserdataPlace.EmpId;
                            HttpContext.Current.Session["Outlet_ID"] = UserdataPlace.Outlet_ID;
                            HttpContext.Current.Session["Outlet_Name"] = UserdataPlace.Outlet_Ar_Name;
                            HttpContext.Current.Session["Outlet_Type"] = UserdataPlace.Outlet_Type;
                            HttpContext.Current.Session["FullName_En"] = UserdataPlace.FullName_En;
                            HttpContext.Current.Session["Outlet_Name_En"] = UserdataPlace.Outlet_En_Name;
                            HttpContext.Current.Session["Outlet_Hr"] = UserdataPlace.Outlet_Hr;
                            HttpContext.Current.Session["Outlet_Type_ID"] = UserdataPlace.Outlet_Hr;
                            //(short)Session["UserId"] = UserdataPlace.UserId;
                            //Current.FullName = UserdataPlace.FullName;
                            //Current.Token = UserdataPlace.Token;
                            //Current.EmpId = UserdataPlace.EmpId;
                            Set_Language(true);
                        }
                        break;
                    case 2://InvalidCredential
                        break;
                    case 5://LoginFromAnotherDevice

                        break;
                    case 6://
                        JavaScriptSerializer ser1 = new JavaScriptSerializer();
                        var UserdataPlace1 = ser1.Deserialize<User_LoginDTO>(lst["obj"].ToString());

                       
                        HttpContext.Current.Session["UserId"] = UserdataPlace1.UserId;
                       
                        HttpContext.Current.Session["Token"] = UserdataPlace1.Token;
                        
                        break;
                }
                return StatusCode;
                #endregion
            }
            catch (Exception ex)
            {

                return (int)Enums.User_Login.ErrorHappened;
            }
        }

        public static void Set_Language(bool is_Arabic = true)
        {
            User_Session Current = User_Session.GetInstance;
            if (is_Arabic)
            {
                HttpContext.Current.Session["Language"] = "ar-Eg";
                HttpContext.Current.Session["Language_IsAr"] = 1;
                //Current.Language = "ar-Eg";
                //Current.Language_IsAr = 1;
            }
            else
            {
                HttpContext.Current.Session["Language"] = "en-Us";
                HttpContext.Current.Session["Language_IsAr"] = 0;
                //Current.Language = "en-Us";
                //Current.Language_IsAr = 0;
            }
        }

        public void LogOut()
        {
            try
            {
                User_Session Current = User_Session.GetInstance;

                #region From API
                Dictionary<string, string> dt = new Dictionary<string, string>();
                //dt["Token"] = Current.Token;
                var tk = HttpContext.Current.Session["Token"];
                if (tk.ToString() != null)
                {
                    dt["Token"] = tk.ToString();
                    var res = APIHandeling.Put("Login_out", dt);
                }
                else
                {
                  //  RedirectResult("/Home/Login");
                }
                #endregion
                ClearSession();
                HttpContext.Current.Session.Clear();
                Current = null;
            }
            catch
            {
                //plant_db.Sp_plant_Error_Insert("User_Session", ex.Message, "logout");
            }
        }
        private void ClearSession()
        {
            try
            {
                User_Session Current = User_Session.GetInstance;
                HttpContext.Current.Session["UserId"] = null;
                HttpContext.Current.Session["FullName"] = null;
                HttpContext.Current.Session["Token"] = null;
                HttpContext.Current.Session["EmpId"] = null;
            }
            catch
            {
                //plant_db.Sp_plant_Error_Insert("User_Session", ex.Message, "clearsession");
            }

        }
    }
}