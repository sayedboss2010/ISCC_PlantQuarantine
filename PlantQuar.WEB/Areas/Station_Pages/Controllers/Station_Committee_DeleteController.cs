using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Station_Pages.Controllers
{
    public class Station_Committee_DeleteController : BaseController
    {
        // GET: Station_Pages/Station_Committee_Delete
        public ActionResult Index()
        {
            if (Session["Outlet_ID"] != null)
            {

                var OutLit_ID = Session["Outlet_ID"].ToString();
                var res = APIHandeling.getData("Station_Committee_Delete_API?List=1&OutLit_ID=" + OutLit_ID);

                var lst = res.Content.ReadAsAsync<List<Station_Committee_Delete_DTO>>().Result;//object
                return View(lst);
            }
            return null;
        }

        public JsonResult DeleteStationCommittee_List(List<long> deleted_lst)
        {

            APIHandeling.Put("Station_Committee_Delete_API", deleted_lst);


            return Json("succ");
        }
    }
}