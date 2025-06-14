using PlantQuar.WEB.App_Start;
using Privilages.DAL;
using System.Linq;
using System.Web.Mvc;

namespace PlantQuar.WEB.Controllers
{
    public class DrawMenuController : BaseController
    {
        // GET: DrawMenu
        dbPrivilageEntities db = new dbPrivilageEntities();

        public ActionResult Index()
        {
            User_Session Current = User_Session.GetInstance;
            if (Current != null)
            {

                var UserId = (short)Session["UserId"];
                int Language_IsAr = (int)Session["Language_IsAr"];
                var data = db.GetGroupsNameByUserAndApplication(UserId, 1, Language_IsAr).ToList();
                return PartialView(data);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Modul(int id)
        {
             User_Session Current = User_Session.GetInstance;

            if (Current != null)
            {
                Session["GID"] = id;
                var UserId = (short)Session["UserId"];
                int Language_IsAr = (int)Session["Language_IsAr"];
                var data = db.GetModulNameByUserAndApplicationandGroup(UserId, id, Language_IsAr).ToList();
                return PartialView(data);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult SubIndex(int Id, bool Is_Group = false)
        {
            User_Session Current = User_Session.GetInstance;

            if (Current != null)
            {
                int GroupID = (int)Session["GID"];

                var UserId = (short)Session["UserId"];
                int Language_IsAr = (int)Session["Language_IsAr"];
                var data = db.GetMenuUser(1, Id, UserId, GroupID, Language_IsAr).ToList();
                if (data.Where(a => a.Id == 100).Select(a => a.Id).FirstOrDefault() == 100)
                {
                    Session["CanView"] = data.Where(a => a.Id == 100).Select(a => a.CanView).FirstOrDefault();
                    Session["CanAdd"] = data.Where(a => a.Id == 100).Select(a => a.CanAdd).FirstOrDefault();
                    Session["CanEdit"] = data.Where(a => a.Id == 100).Select(a => a.CanEdit).FirstOrDefault();
                    Session["CanDelete"] = data.Where(a => a.Id == 100).Select(a => a.CanDelete).FirstOrDefault();
                }
                    //Session["CanAdd"], Session["CanEdit"],Session["CanDelete"]
                return PartialView(data);
            }
            else
            {
                return Redirect("/");
            }
        }
        [HttpPost]
        public JsonResult SetMenuID(int Id)
        {
            //User_Session Current = User_Session.GetInstance;
            Session["MenuId"] = Id;
            //Current.MenuId = Id;
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public JsonResult SetModuleID(int moduleId)
        {
            //User_Session Current = User_Session.GetInstance;
            Session["ModuleId"] = moduleId;
            //Current.ModuleId = moduleId;
            //  Current.ModuleId = moduleId;
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public JsonResult SetGroupID(int GroupId)
        {
            Session["GroupId"] = GroupId;
            //  Current.GroupId = GroupId;
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}