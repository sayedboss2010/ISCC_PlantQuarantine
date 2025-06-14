using PlantQuar.DAL;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry
{
    public class AttachmentTable_BLL
    {
        private UnitOfWork uow;
        public AttachmentTable_BLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Fill_AttachmentTableType_List(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<A_AttachmentTableType>().GetData().Where(lab => lab.IsActive == true)
                .Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
    }
}
