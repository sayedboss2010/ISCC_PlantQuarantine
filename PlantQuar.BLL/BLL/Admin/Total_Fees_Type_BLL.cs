using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Admin
{
    public class Total_Fees_Type_BLL
    {
        private UnitOfWork uow;
        PlantQuarantineEntities entities = new PlantQuarantineEntities();
        public Total_Fees_Type_BLL()
        {
            uow = new UnitOfWork();

        }
        public Dictionary<string, object> Get_Total_Fees_Type( List<string> Device_Info)
        {
            try
            {

            
                var requests = uow.Repository<Total_Fees_Type_DTO>().CallStored("Total_Fees_Type_List", null,null, Device_Info).ToList();
                //data.FirstOrDefault().List_Station_Fees = requests;
               // var Data_Total_Fees_Type = entities.Total_Fees_Type_List().ToList();

                    //(from tt in entities.Total_Fees_Type_List()
                    //                     select new Total_Fees_Type_DTO
                    //                     {
                    //                         Description = tt.Description,
                    //                         Amount_Total = tt.Amount_Total,
                    //                         حسابخاص = tt.حسابخاص,
                    //                         حسابحكومي=tt.حسابحكومي
                    //                     }).ToList();
               
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);                              
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
