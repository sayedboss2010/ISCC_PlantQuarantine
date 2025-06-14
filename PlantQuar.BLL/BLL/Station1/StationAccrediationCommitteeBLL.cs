using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station
{
    public class StationAccrediationCommitteeBLL
    {
        private UnitOfWork uow;
        public StationAccrediationCommitteeBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetAll(string stationCode, int? Status, short? stationActivityType, long Outlit_ID, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("stationCode", SqlDbType.NVarChar);
                paramters_Type.Add("Status", SqlDbType.Int);
                paramters_Type.Add("Language_IsAr", SqlDbType.NVarChar);
                paramters_Type.Add("stationActivityType", SqlDbType.SmallInt);
                paramters_Type.Add("OutLit_ID", SqlDbType.BigInt);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("stationCode", (stationCode != null ? stationCode : ""));
                paramters_Data.Add("Status", Status.ToString());
                paramters_Data.Add("Language_IsAr", Device_Info[2]);
                paramters_Data.Add("stationActivityType", stationActivityType.ToString());
                paramters_Data.Add("OutLit_ID", Outlit_ID.ToString());
                //var request = uow.Repository<object>().CallStored("Station_Accrediation_Committee_GetData", paramters_Type,
                //   paramters_Data, Device_Info).ToList();
                var request = uow.Repository<Station_Accrediation_Committee_GetData_DTO>().CallStored("Station_Accrediation_Committee_GetData", paramters_Type,
                    paramters_Data, Device_Info).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, request);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll2(string stationCode, int? Status, short? stationActivityType, string DateFrom, string DateEnd, long Outlit_ID, int? stationAccrTypeLstId, int? CompanyNameLst_Id, int? StationActivityType_ID, List<string> Device_Info)
        {
            try
            {
                //DateTime _startDate = DateTime.Parse(DateFrom); //DateTime.ParseExact(DateFrom, "dd/MM/yyyy", null);
                //DateTime _endDate = DateTime.Parse(DateEnd); //DateTime.ParseExact(DateEnd, "dd/MM/yyyy", null);
                //                                             //_startDate.Day, _startDate.Month, _startDate.Year, _endDate.Day, _endDate.Month, _endDate.Year, 
                //                                             //     @dayStart int= 12, @monthStart int= 12, @yearStart int= 2022,
                //                                             //@dayEnd int= 14, @monthEnd int= 12, @yearEnd int= 2022
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("stationCode", SqlDbType.NVarChar);
                paramters_Type.Add("Status", SqlDbType.Int);

                paramters_Type.Add("Language_IsAr", SqlDbType.NVarChar);
                paramters_Type.Add("stationActivityType", SqlDbType.SmallInt);
                paramters_Type.Add("OutLit_ID", SqlDbType.BigInt);
                ////Add date Eslam
                //paramters_Type.Add("dayStart", SqlDbType.Int);
                //paramters_Type.Add("monthStart", SqlDbType.Int);
                //paramters_Type.Add("yearStart", SqlDbType.Int);
                //paramters_Type.Add("dayEnd", SqlDbType.Int);
                //paramters_Type.Add("monthEnd", SqlDbType.Int);
                //paramters_Type.Add("yearEnd", SqlDbType.Int);
                //End
                paramters_Type.Add("stationAccrTypeLstId", SqlDbType.Int);
                paramters_Type.Add("CompanyNameLst_Id", SqlDbType.Int);
                paramters_Type.Add("StationActivityType_ID", SqlDbType.Int);


                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                if (stationCode == null)
                {
                    stationCode = "";
                }

                paramters_Data.Add("stationCode", stationCode);
                paramters_Data.Add("Status", Status.ToString());
                paramters_Data.Add("Language_IsAr", Device_Info[2]);
                paramters_Data.Add("stationActivityType", stationActivityType.ToString());
                paramters_Data.Add("OutLit_ID", Outlit_ID.ToString());
                //Add date data Eslam
                //paramters_Data.Add("dayStart", _startDate.Day.ToString());
                //paramters_Data.Add("monthStart", _startDate.Month.ToString());
                //paramters_Data.Add("yearStart", _startDate.Year.ToString());
                //paramters_Data.Add("dayEnd", _endDate.Day.ToString());
                //paramters_Data.Add("monthEnd", _endDate.Month.ToString());
                //paramters_Data.Add("yearEnd", _endDate.Year.ToString());
                //End
                paramters_Data.Add("stationAccrTypeLstId", stationAccrTypeLstId.ToString());
                paramters_Data.Add("CompanyNameLst_Id", CompanyNameLst_Id.ToString());
                paramters_Data.Add("StationActivityType_ID", StationActivityType_ID.ToString());
                //var request = uow.Repository<object>().CallStored("Station_Accrediation_Committee_GetData", paramters_Type,
                //   paramters_Data, Device_Info).ToList();
                var request = uow.Repository<Station_Accrediation_Committee_GetData_DTO>().CallStored("Station_List_Stord", paramters_Type,
                    paramters_Data, Device_Info).ToList();
                //PlantQuarantineEntities entities = new PlantQuarantineEntities();
                //var test  = (from sac in entities.Station_Accreditation_Committee
                //                   join r in request on sac.Station_Accreditation_Request_ID equals r.Station_Accreditation_Request_ID
                                                                  
                //                   select new Station_Accrediation_Committee_GetData_DTO
                //                   {
                //                       //IsExport = r.IsExport,
                //                       //Outlet_ID = r.Outlet_ID,
                //                       //Ar_Name = r.Ar_Name,
                //                       //Outlet_Center_ID = r.Outlet_Center_ID,
                //                       //Outlet_Center_Name = r.Outlet_Center_Name,
                //                       //Outlet_Gov_Id = r.Outlet_Gov_Id,
                //                       //Outlet_Gov_Name = r.Outlet_Gov_Name,
                //                       //station_Status = r.station_Status,
                //                       //station_btn = r.station_btn,
                //                       //Station_ID = r.Station_ID,
                //                       //Ar_Name1 = r.Ar_Name1,
                //                       //StationCode = r.StationCode,
                //                       //Company_ID = r.Company_ID,
                //                       //Company_Name = r.Company_Name,
                //                       //Gov_Id = r.Gov_Id,
                //                       //Center_Id = r.Center_Id,
                //                       //Village_Id = r.Village_Id,
                //                       //Gov_Ar_Name = r.Gov_Ar_Name,
                //                       //Center_Ar_Name = r.Center_Ar_Name,
                //                       //Village_Ar_Name = r.Village_Ar_Name,
                //                       //station_IsActive = r.station_IsActive,
                //                       //station_IsAccepted = r.station_IsAccepted,
                //                       //Station_User_Deletion_Id = r.Station_User_Deletion_Id,
                //                       //Station_Accreditation_Data_ID = r.Station_Accreditation_Data_ID,
                //                       //Station_Accreditation_Request_Type_ID = r.Station_Accreditation_Request_Type_ID,
                //                       //Station_Accreditation_Request_ISActive = r.Station_Accreditation_Request_ISActive,
                //                       //Station_Accreditation_Request_ISpaid = r.Station_Accreditation_Request_ISpaid,
                //                       //Station_Accreditation_Request_IsAccepted = r.Station_Accreditation_Request_IsAccepted,
                //                       //Is_Final_requst = r.Is_Final_requst,
                //                       //Station_Accreditation_Data_Name = r.Station_Accreditation_Data_Name,
                //                       //Station_Accreditation_Data_IsActive = r.Station_Accreditation_Data_IsActive,
                //                       //Station_Accreditation_Request_Type_Name = r.Station_Accreditation_Request_Type_Name,
                //                       //Station_Accreditation_Request_Type_IsActive = r.Station_Accreditation_Request_Type_IsActive,
                //                       //Station_Accreditation_Request_ID = r.Station_Accreditation_Request_ID,
                //                       //Accreditation_Type_ID = r.Accreditation_Type_ID,
                //                       //StationActivityType_ID = r.StationActivityType_ID,
                //                       //Request_User_Deletion_Id = r.Request_User_Deletion_Id,
                //                       //Station_Accreditation_Committee_ID = r.Station_Accreditation_Committee_ID,
                //                       //Delegation_Date = r.Delegation_Date,
                //                       //StartTime = r.StartTime,
                //                       //EndTime = r.EndTime,
                //                       //IsApproved = r.IsApproved,
                //                       //IsPaid = r.IsPaid,
                //                       //Status = r.Status,
                //                       //IsAccepted = r.IsAccepted,
                //                       //Is_Start_Android = r.Is_Start_Android,
                //                       //Is_Cancel = r.Is_Cancel,
                //                       Committee_User_Deletion_Id = r.Committee_User_Deletion_Id,

                //                   }).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, request);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //Eslam Fill Company Name List
        public Dictionary<string, object> FillCompanyNameLst_AddEDIT(List<string> Device_Info)
        {
            PlantQuarantineEntities db = new PlantQuarantineEntities();
            string lang = Device_Info[2];
            var data = (from c in db.Company_National 
                        join s in db.Stations on c.ID equals s.Company_ID
                       where  c.User_Deletion_Id == null && c.IsActive == true
                   select new CustomOptionLongId
                   {                      
                       DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                       Value = c.ID
                   }).Distinct().ToList();
            //uow.Repository<Company_National>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true)
            //       .Select(c => new CustomOptionLongId
            //       {
            //           //change display lang
            //           DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
            //           Value = c.ID
            //       }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "-----أختــر-----", Value = -1 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //Eslam Fill Station_Accreditation_Request_Type List
        public Dictionary<string, object> FillStation_Accreditation_Request_Type_AddEDIT(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Station_Accreditation_Request_Type>().GetData().Where(a => a.IsActive == true)
                   .Select(c => new CustomOptionLongId
                   {
                       //change display lang
                       DisplayText = (lang == "1" ? c.Name_AR : c.Name_EN),
                       Value = c.ID
                   }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "-----أختــر-----", Value = -1 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }



        //Eslam  Fill_StationActivityLst_AddEDIT List
        public Dictionary<string, object> Fill_StationActivityLst_AddEDIT(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<StationActivityType>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null)
                   .Select(c => new CustomOptionLongId
                   {
                       //change display lang
                       DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                       Value = c.ID
                   }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = -1 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}
