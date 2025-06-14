using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Outlets;
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

namespace PlantQuar.BLL.BLL.Import.checkRequests
{
   public class List_Im_checkRequestsOutletBLL
    {
        private UnitOfWork uow;

        public List_Im_checkRequestsOutletBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetImCheckRequestList_filter (short IsApproved, short OutlitUserID, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                dbPrivilageEntities priv = new dbPrivilageEntities();
                Int64 data_Count = 0;
                Dictionary<string, object> dic = new Dictionary<string, object>();

                var user = priv.PR_User.Where(p => p.Id == OutlitUserID).FirstOrDefault();
                var outlet = (from o in entities.Outlets
                              //join co in entities.Center_Outlet on o.ID equals co.Outlet_ID
                              //join Center in entities.Centers on o.ID equals Center.Outlet_ID
                              where o.ID_HR == user.Outlet_ID
                              select new
                              {
                                  o.PortNational_ID
                              }
                              ).FirstOrDefault();


                //  uow.Repository<Outlet>().GetData().FirstOrDefault(o => o.ID_HR == user.Outlet_ID);

                if (outlet.PortNational_ID != null)
                {
                    string lang = Device_Info[2];
                    var requests = (from cc in entities.Im_CheckRequest

                                    join rr in entities.Im_CheckRequest_Data on cc.ID equals rr.Im_CheckRequest_ID
                                    join cp in entities.Im_CheckRequest_Port on rr.ID equals cp.Im_CheckRequest_Data_ID
                                    join po in entities.PortNationals on cp.Port_ID equals po.ID
                                    where cc.Outlet_ID == null 
                                    &&cp.ReqPortType_ID == 10
                                    && cp.Port_ID == outlet.PortNational_ID
                                    select new ImCheckRequestListOutlitDTO
                                    {
                                        ID = cc.ID,
                                        ImCheckRequest_Number = cc.CheckRequest_Number,
                                        Port_ID = cp.Port_ID,
                                        port_Name = lang == "1" ? po.Name_Ar : po.Name_Ar,
                                        Gov_ID = po.Govern_ID, //po.Governate.Ar_Name
                                        Gov_Name = lang == "1" ? po.Governate.Ar_Name : po.Governate.En_Name
                                    }).ToList();
                    List<ImCheckRequestListOutlitDTO> newRequests = new List<ImCheckRequestListOutlitDTO>();
                    foreach (var re in requests)
                    {

                        newRequests.Add(re);

                    }
                    //  var dataDto = requests.OrderByDescending(A => A.ID).Skip(index).Take(pageSize)
                    //.Select(x => new ImCheckRequestListOutlitDTO()
                    //{
                    //    ID = x.ID,
                    //    ImCheckRequest_Number = x.ImCheckRequest_Number,
                    //    Port_ID = x.Port_ID,
                    //    port_Name = x.port_Name,
                    //    Gov_ID = x.Gov_ID,
                    //    Gov_Name = x.Gov_Name
                    //}).ToList();

                    ////var dataDto = requests.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<Im_CheckRequest, ImCheckRequestListOutlitDTO>);
                    //data_Count = requests.Count();
                    //dic.Add("Count_Data", data_Count);
                    //dic.Add("Country_Data", dataDto);


                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, newRequests);
                    //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, null);
                }
                
            }
            catch (Exception ex)
            {
                //uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, NULL);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> Update(List <Im_CheckRequestDTO> entity, List<string> Device_Info)
        {
            try
            {
                if (entity != null)
                {


                    foreach (var item in entity)
                    {
                        Im_CheckRequest CModel = uow.Repository<Im_CheckRequest>().Findobject(item.ID);
                        CModel.Outlet_ID = item.Outlet_ID;

                        uow.SaveChanges();
                        
                    }
                    //var empDTO = Mapper.Map<Im_CheckRequest, Im_CheckRequestDTO>();
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
