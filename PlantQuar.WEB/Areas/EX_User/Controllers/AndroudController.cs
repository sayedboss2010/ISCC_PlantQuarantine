using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Export_CheckRequest_New;

namespace PlantQuar.WEB.Areas.EX_User.Controllers
{
    public class AndroudController : Controller
    {
        // GET: EX_User/Androud

        PlantQuarantineEntities entities = new PlantQuarantineEntities();
        public ActionResult Index(long Ex_CheckRequest_ID)
        {
           
            var asd = (from ex in entities.Ex_CheckRequest
                       join rc in entities.Ex_RequestCommittee on ex.ID equals rc.ExCheckRequest_ID
                       join ct in entities.CommitteeTypes on rc.CommitteeType_ID equals ct.ID
                       where ex.ID == Ex_CheckRequest_ID && (rc.IsPaid == true || rc.Delegation_Date >= DateTime.Now)
                       select new AndroidDTO
                       {
                           CommitteeTypeName_Ar=ct.Name_Ar,
                           Ex_CheckRequestID=ex.ID,
                           Ex_CommitteeID=rc.ID,
                           Delegation_Date=rc.Delegation_Date,
                           IsPaid_RequestCommittee=rc.IsPaid,
                           IsFinishedAll=rc.IsFinishedAll,
                       }).ToList();
                     
            return View(asd);
        }
    }
}