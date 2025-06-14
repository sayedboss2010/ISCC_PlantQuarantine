using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.FA_Farm.Controllers
{
    public class Farm_Committee_DeleteController : BaseController
    {
        // GET: FA_Farm/Farm_Committee_Delete

        string apiName = "Farm_Committee_DeleteAPI";
        public ActionResult Index()
        {
            var res = APIHandeling.getData("Farm_Committee_DeleteAPI?List=1");

            var lst = res.Content.ReadAsAsync<List<FarmCommitteeDeleteDTO>>().Result;//object
            return View(lst);
        }


        //deleted_lst

        [HttpPost]
        public JsonResult DeleteFarm_List(List<long> deleted_lst)
        {

            APIHandeling.Put("Farm_Committee_DeleteAPI", deleted_lst);


            return Json("succ");
        }

        //public ActionResult DeleteFarm_List(List<long> deleted_lst)
        //{
        //    var res = APIHandeling.getData("Farm_Committee_DeleteAPI?deleted_lst="+ deleted_lst);

        //    var lst = res.Content.ReadAsAsync<List<FarmCommitteeDeleteDTO>>().Result;//object
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}
    }
}