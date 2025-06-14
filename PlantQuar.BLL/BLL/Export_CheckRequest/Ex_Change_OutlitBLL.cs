using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Outlets;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using PlantQuar.WEB.App_Start;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Export_CheckRequest
{
   public class Ex_Change_OutlitBLL
    {
        private UnitOfWork uow;

        public Ex_Change_OutlitBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetExCheckRequestList_filter (short IsApproved, short OutlitUserID, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                dbPrivilageEntities priv = new dbPrivilageEntities();
                Int64 data_Count = 0;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                var user = priv.PR_User.Where(p => p.Id == OutlitUserID).FirstOrDefault();
                var outlet = (from o in entities.Outlets
                              where o.ID_HR == user.Outlet_ID
                              select new
                              {
                                  o.PortNational_ID
                              }
                              ).FirstOrDefault();


                if (outlet != null)
                {
                    string lang = Device_Info[2];
                    var requests = (from cc in entities.Ex_CheckRequest

                                    join rr in entities.Ex_CheckRequest_Data on cc.ID equals rr.Ex_CheckRequest_ID
                                    join cp in entities.Ex_CheckRequest_Port on rr.ID equals cp.Ex_CheckRequest_Data_ID
                                    join po in entities.PortNationals on cp.Port_ID equals po.ID
                                    where cc.Outlet_ID == null 
                                    &&cp.ReqPortType_ID == 10
                                    && cp.Port_ID == outlet.PortNational_ID
                                    select new Ex_Change_OutlitDTO
                                    {
                                        ID = cc.ID,
                                        ExCheckRequest_Number = cc.CheckRequest_Number,
                                        Port_ID = cp.Port_ID,
                                        port_Name = lang == "1" ? po.Name_Ar : po.Name_Ar,
                                        Gov_ID = po.Govern_ID, 
                                        Gov_Name = lang == "1" ? po.Governate.Ar_Name : po.Governate.En_Name
                                    }).ToList();
                    List<Ex_Change_OutlitDTO> newRequests = new List<Ex_Change_OutlitDTO>();
                    foreach (var re in requests)
                    {

                        newRequests.Add(re);

                    }
                    

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, newRequests);
                   
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, outlet);
                }
                
            }
            catch (Exception ex)
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> Update(List <EX_CheckRequestDTO> entity, List<string> Device_Info)
        {
            try
            {
                if (entity != null)
                {


                    foreach (var item in entity)
                    {
                        Ex_CheckRequest CModel = uow.Repository<Ex_CheckRequest>().Findobject(item.ID);
                        CModel.Outlet_ID = item.Outlet_ID;

                        uow.SaveChanges();
                        
                    }
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "Success");
                }
                else
                return null;
               
              
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
