using PlantQuar.DTO.DTO.Import.Committee;
using PlantQuar.WEB.App_Start;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Im_CheckRequests.Controllers
{
    public class Im_Check_ComitteController : Controller
    {
        // GET: Im_CheckRequests/Im_Check_Comitte
        public ActionResult Index(long ImCheckRequest_Number)
        {
            //ImCheckRequest_Number = "70";
            var res = APIHandeling.getData("Im_Check_Comitte_API?ImCheckRequest_Number=" + ImCheckRequest_Number);
            //var Lst = res.Content.ReadAsAsync<List<Im_Check_ComitteDTO>>().Result;
            var Lst = res.Content.ReadAsAsync<List<Im_Check_ComitteDTO>>().Result;
            //var reasons = APIHandeling.getData("Im_Check_Comitte_API?List=1&refuse=1");
            //var reasonsList = reasons.Content.ReadAsAsync<List<CustomOption>>().Result;
            //ViewBag.ListReasons = reasonsList;

            return View(Lst);
            //return View("Index", Lst);
            // return View(Lst);
        }

        [HttpPost]
        public ActionResult acceptRequest(Im_Check_ComitteDTO model)
        {
            Im_Check_ComitteDTO dto = new Im_Check_ComitteDTO();

            //---add new row in export committee


            APIHandeling.Put("Im_CheckRequests_API?approve=1", dto);

            return RedirectToAction("Index", "List_ImCheckRequest");
        }
    }
}