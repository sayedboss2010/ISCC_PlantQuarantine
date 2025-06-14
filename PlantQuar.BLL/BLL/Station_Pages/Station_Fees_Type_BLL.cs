using PlantQuar.DAL;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station_Pages
{
    public class Station_Fees_Type_BLL
    {
        private UnitOfWork uow;

        public Station_Fees_Type_BLL()
        {
            uow = new UnitOfWork();
        }

        //DROPS
        public Dictionary<string, object> FillDrop_Station_Fees_Type(int Fees_Type,List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Station_Fees_Type>().GetData().Where (e => e.Fees_Type == Fees_Type)
                .Select(c => new CustomOption
                {
                    DisplayText = c.Name ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> Get_Station_Fees_Type_Mony(byte Station_Fees_Type_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Station_Fees_Type>().GetData().Where(lab =>  lab.ID == Station_Fees_Type_ID).FirstOrDefault().Value;

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
    }
}
