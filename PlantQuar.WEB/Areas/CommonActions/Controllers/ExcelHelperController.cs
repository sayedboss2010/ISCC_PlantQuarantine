using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PlantQuar.DTO.DTO.DataEntry.Treatments;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.Controllers;
using System.Reflection;

namespace PlantQuar.WEB.Areas.CommonActions.Controllers
{
    public class ExcelHelperController : Controller
    {
        // GET: CommonActions/ExcelHelper
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult saveexcel(string apiName)
        {
            try
            {
//                string classname = "TreatmentMaterialDTO";

//                Type t = Type.GetType("PlantQuar.DTO.DTO.DataEntry.Treatments.TreatmentMaterialDTO, PlantQuar.DTO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
//, true);


//                var  instance = Activator.CreateInstance(t) as object;

                //  string qualifiedName = typeof(TreatmentMaterialDTO).AssemblyQualifiedName;

                //   string typeName = "PlantQuar.DTO.DTO.DataEntry.Treatments.TreatmentMaterialDTO"; // Type.FullName
                //   string assemblyName = "PlantQuar.DTO.DTO.DataEntry.Treatments"; // MyAssembly.FullName or MyAssembly.GetName().Name
                //  // string assemblyQualifiedName = Assembly.CreateQualifiedName(assemblyName, typeName);
                //   Type myClassType = Type.GetType(qualifiedName);


                //var x = Type.GetType("PlantQuar.DTO.DTO.DataEntry.Treatments.TreatmentMaterialDTO, Assembly.Name");


                var res = APIHandeling.getData(apiName + "?pageSize=-1&index=-1");
                var lst = res.Content.ReadAsAsync<List<object>>().Result;//object
                return  Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}