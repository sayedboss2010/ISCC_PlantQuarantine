using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station_Pages
{
    public class StationDetails_BLL
    {

        private UnitOfWork uow;

        public StationDetails_BLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetStationData(long stationId, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("stationId", SqlDbType.BigInt);
                paramters_Type.Add("Language_IsAr", SqlDbType.NVarChar);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("stationId", stationId.ToString());
                paramters_Data.Add("Language_IsAr", Device_Info[2]);  //"2018-12-26"

                List<Station_Get_Data_ResultDTO> data = uow.Repository<Station_Get_Data_ResultDTO>().CallStored("Station_Get_Data", paramters_Type,
                paramters_Data, Device_Info).ToList();
                // get all attaches 
                data[0].Attachments = uow.Repository<A_AttachmentData_Station>().GetData().Where(x => x.RowId == stationId).Select(

                    c => new Attachments
                    {
                        AttachmentPath = c.AttachmentPath,
                        Attachment_Name = c.Attachment_TypeName,
                        Attachment_Number = c.Attachment_Number,
                        Attachment_TypeName = c.Attachment_TypeName,
                        EndDate = c.EndDate,
                        StartDate = c.StartDate
                    }
                    )
                    .ToList();
                long? companyid = data[0].Company_ID;
                data[0]._CompanyActivitys = uow.Repository<CompanyActivity>().GetData().Where(x => x.Company_ID == companyid).Select(

                   ca => new CompanyActivityDTO
                   {
                       CompActivityType__Name = ca.A_SystemCode.ValueName,
                       Enrollment_Name = ca.Enrollment_Name,
                       Enrollment_Number = ca.Enrollment_Number,
                       Enrollment_Start = ca.Enrollment_Start,
                       Enrollment_End = ca.Enrollment_End,
                       Enrollment_type_Name = ca.Enrollment_type.Ar_Name,

                   }
                   )
                   .ToList();
                data[0].ImporterContacts = uow.Repository<Ex_ContactData>()
                      .GetData().Include(f => f.ContactType).
                 Where(a => a.Exporter_ID == companyid && a.ExporterType_Id == 6).
                 Select(a => new ContactTypeDTO
                 {
                     Name_Ar = a.ContactType.Name_Ar,
                     Value = a.Value
                 }).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> ApproveStation(Station_Get_Data_ResultDTO dto, List<string> Device_Info)
        {
            try
            {
                Station_Accreditation_Committee CModel = uow.Repository<Station_Accreditation_Committee>().Findobject(dto.requestId);
                CModel.IsApproved = dto.IsApproved;

                uow.SaveChanges();

                var empDTO = Mapper.Map<Station_Accreditation_Committee, Station_Accrediation_Committee_GetData_DTO>(CModel);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update_Station_Details(Station_Get_Data_ResultDTO entity, List<string> Device_Info)
        {
            try
            {
                PlantQuar.DAL.Station CModel_Station = uow.Repository<PlantQuar.DAL.Station>().Findobject(entity.StationId);
                if (entity.IsActive_Request == true)
                {
                    CModel_Station.StationCode = entity.StationCode;
                }
                CModel_Station.IsActive = entity.IsActive_Request;
                CModel_Station.Notes_Reject = entity.Notes_Reject;
                uow.Repository<PlantQuar.DAL.Station>().Update(CModel_Station);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop_List1(List<string> Device_Info)
        {

            string lang = Device_Info[2];
            var data = new List<Station_Get_Data_ResultDTO>();
            data = uow.Repository<Station_Fees_Type>().GetData().Select(c => new Station_Get_Data_ResultDTO
            {
                Station_Fees_Id = c.ID,
                Station_Fees_Money = c.Value,
                Station_Fees_Id_Type = c.Fees_Type
            }).ToList();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }


        public Dictionary<string, object> Insert_Station_Accreditation_Request(long requestId, bool ISActive, List<Station_Request_Fees_DTO> Selection_Fees_List, List<string> Device_Info)
        {
            try
            {
                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        var CModel = uow.Repository<Station_Accreditation_Request>().Findobject(requestId);

                        CModel.IsActive = ISActive;
                        if (ISActive == false)
                        {
                            CModel.IsAccepted = false;
                            CModel.Is_Final_requst = true;
                        }
                        uow.Repository<Station_Accreditation_Request>().Update(CModel);
                        uow.SaveChanges();

                        #region _Station_Accreditation_Request_Fees

                        if (ISActive == true)
                        {
                            if (Selection_Fees_List != null)
                            {

                                foreach (var item in Selection_Fees_List)
                                {
                                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Request_Fees_SEQ");


                                    var Station_Fees = new Station_Accreditation_Request_Fees
                                    {
                                        ID = id,
                                        Station_Accreditation_Request_ID = item.Station_Accreditation_Request_ID,
                                        Station_Fees_Type_ID = item.Station_Fees_Type_ID,
                                        Value = item.Value,
                                        User_Creation_Id = item.User_Creation_Id,
                                        User_Creation_Date = DateTime.Now,
                                    };
                                    context.Station_Accreditation_Request_Fees.Add(Station_Fees);
                                    context.SaveChanges();
                                }
                            }

                        }
                        #endregion

                        trans.Commit();
                    }
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update_Start_Stop_Request(long Reqest_id, bool IsAccepted, List<string> Device_Info)
        {
            try
            {
                var CModel = uow.Repository<Station_Accreditation_Request>().Findobject(Reqest_id);
                //if (CModel.IsAccepted == true)
                //{
                //    CModel.IsAccepted = IsAccepted;
                //}
                //else if (CModel.IsAccepted == false)
                //{
                CModel.IsAccepted = IsAccepted;
                CModel.Is_Final_requst = true;
                //}
                uow.Repository<Station_Accreditation_Request>().Update(CModel);
                uow.SaveChanges();

                PlantQuarantineEntities Db = new PlantQuarantineEntities();
                // القديم كله False
                var Check_Reqst_Final_Old = Db.Station_Accreditation.Where(a => a.Station_Accreditation_Request_ID == Reqest_id).ToList();
                for (int i = 0; i < Check_Reqst_Final_Old.Count(); i++)
                {
                    Station_Accreditation CModel_Check_Reqst_Final_Old = uow.Repository<Station_Accreditation>()
                        .Findobject(Check_Reqst_Final_Old[i].ID);
                    CModel_Check_Reqst_Final_Old.IsActive = false;
                    uow.Repository<Station_Accreditation>().Update(CModel_Check_Reqst_Final_Old);
                    uow.SaveChanges();
                }




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> GetStationData_Print(long stationId, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("stationId", SqlDbType.BigInt);


                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("stationId", stationId.ToString());


                var data = uow.Repository<Station_Get_EN_Print_DTO>().CallStored("Station_Get_EN_Print", paramters_Type,
                paramters_Data, Device_Info).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}