using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station_Pages
{
    public class Station_Committee_Delete_BLL
    {

        private UnitOfWork uow;
        public Station_Committee_Delete_BLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetAll(long OutLit_ID,List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var OutLit_Type = entities.Outlets.Where(o => o.ID == OutLit_ID).Select(o => o.IsExport).FirstOrDefault();
                if (OutLit_Type == 81)//وارد
                {
                    var Outlit_Data = (from ou in entities.Outlets
                                       join c in entities.Centers on ou.ID equals c.Outlet_ID
                                       join g in entities.Governates on c.Govern_ID equals g.ID
                                       select new Outlit_Data_DTO
                                       {
                                           IsExport = ou.IsExport,
                                           Outlet_ID = ou.ID,
                                           Outlet_Center_ID = c.ID,
                                           Outlet_Gov_Id = g.ID
                                       }).ToList();


                    var Station_Accreditation_Data_List = (from st in entities.Stations
                                                           join sar in entities.Station_Accreditation_Request on st.ID equals sar.Station_ID
                                                           join sad in entities.Station_Accreditation_Data on sar.Station_Accreditation_Data_ID equals sad.ID
                                                           join sac in entities.Station_Accreditation_Committee on sar.ID equals sac.Station_Accreditation_Request_ID
                                                           //join o in entities.Outlets on new {a= sad.Accreditation_Type_ID,b= st.Gov_Id }  equals new { o.ID,o.go } OutLit_ID  

                                                           where sac.Status == false && sac.Is_Start_Android == null && sac.IsPaid == null
                                                           select new Station_Committee_Delete_DTO
                                                           {
                                                               Station_Accreditation_Committee_ID = sac.ID,
                                                               Station_ID = st.ID,
                                                               Station_Ar_Name = st.Ar_Name,
                                                               Station_Accreditation_Data_Name = sad.Name_AR,
                                                               Station_Code = st.StationCode,
                                                               Is_Start_Android = sac.Is_Start_Android,
                                                               Accreditation_Type_ID = sad.Accreditation_Type_ID,
                                                               Station_Gov_Id = st.Gov_Id,
                                                               Station_Center_Id = st.Center_Id,

                                                           }).ToList();

                    var station_outlit_Delete = (from ou in Outlit_Data
                                                 join st in Station_Accreditation_Data_List on ou.IsExport equals st.Accreditation_Type_ID
                                                 where ou.Outlet_Gov_Id == st.Station_Gov_Id
                                                && ou.Outlet_Center_ID == st.Station_Center_Id
                                                 select new Station_Committee_Delete_DTO
                                                 {
                                                     Station_Accreditation_Committee_ID = st.Station_Accreditation_Committee_ID,
                                                     Station_ID = st.Station_ID,
                                                     Station_Ar_Name = st.Station_Ar_Name,
                                                     Station_Accreditation_Data_Name = st.Station_Accreditation_Data_Name,
                                                     Station_Code = st.Station_Code,
                                                     Is_Start_Android = st.Is_Start_Android,
                                                     Accreditation_Type_ID = st.Accreditation_Type_ID,
                                                     Station_Gov_Id = st.Station_Gov_Id,
                                                     Station_Center_Id = st.Station_Center_Id,

                                                 }).ToList();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, station_outlit_Delete);
                }

                else
                {
                    var Station_Accreditation_Data_List = (from st in entities.Stations
                                                           join sar in entities.Station_Accreditation_Request on st.ID equals sar.Station_ID
                                                           join sad in entities.Station_Accreditation_Data on sar.Station_Accreditation_Data_ID equals sad.ID
                                                           join sac in entities.Station_Accreditation_Committee on sar.ID equals sac.Station_Accreditation_Request_ID
                                                           //join o in entities.Outlets on new {a= sad.Accreditation_Type_ID,b= st.Gov_Id }  equals new { o.ID,o.go } OutLit_ID  

                                                           where sac.Status == false && sac.Is_Start_Android == null && sac.IsPaid == null
                                                           && sad.Accreditation_Type_ID == 80 && OutLit_ID == 156
                                                           select new Station_Committee_Delete_DTO
                                                           {
                                                               Station_Accreditation_Committee_ID = sac.ID,
                                                               Station_ID = st.ID,
                                                               Station_Ar_Name = st.Ar_Name,
                                                               Station_Accreditation_Data_Name = sad.Name_AR,
                                                               Station_Code = st.StationCode,
                                                               Is_Start_Android = sac.Is_Start_Android,
                                                               Accreditation_Type_ID = sad.Accreditation_Type_ID,
                                                               Station_Gov_Id = st.Gov_Id,
                                                               Station_Center_Id = st.Center_Id,

                                                           }).ToList();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Station_Accreditation_Data_List);
                }
                    
               
               
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> delete(List<long> deleted_lst, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();

                foreach (var x in deleted_lst)
                {
                    // Delete From StationAccreditationCommittee
                    var deletedStationAccreditationCommitteeRows = (from stac in entities.Station_Accreditation_Committee
                                                              where stac.ID == x
                                                              select stac).ToList();
                    foreach (var i in deletedStationAccreditationCommitteeRows)
                    { entities.Station_Accreditation_Committee.Remove(i); }


                    // Delete From StationAccreditationCommitteeImage
                    var deletedStationAccreditationCommitteeImageRows = (from staci in entities.Station_Accreditation_Committee_Imge
                                                            where staci.Station_Accreditation_Committee_id == x
                                                            select staci).ToList();
                    foreach (var i in deletedStationAccreditationCommitteeImageRows)
                    { entities.Station_Accreditation_Committee_Imge.Remove(i); }


                    //// Delete From StationAccreditationCommitteeFinalResult
                    var deletedStationAccreditationCommitteeFinalResultRows = (from stacfr in entities.Station_Accreditation_Committee_Final_Result
                                                        where stacfr.Station_Accreditation_Committee_ID == x
                                                        select stacfr).ToList();
                    foreach (var i in deletedStationAccreditationCommitteeFinalResultRows)
                    { entities.Station_Accreditation_Committee_Final_Result.Remove(i); }


                    //// Delete From StationAccreditationCommitteeCheckList
                    var deletedStationAccreditationCommitteeCheckListRows = (from stacc in entities.Station_Accreditation_Committee_CheckList
                                                              where stacc.Committee_ID == x
                                                              select stacc).ToList();
                    foreach (var i in deletedStationAccreditationCommitteeCheckListRows)
                    { entities.Station_Accreditation_Committee_CheckList.Remove(i); }


                    //// Delete From StationAccreditationRequestFeesENG
                    var deletedStationAccreditationRequestFeesENGRows = (from starfe in entities.Station_Accreditation_Request_Fees_ENG
                                                                     where starfe.Station_Accreditation_Committee_ID == x
                                                        select starfe).ToList();
                    foreach (var i in deletedStationAccreditationRequestFeesENGRows)
                    { entities.Station_Accreditation_Request_Fees_ENG.Remove(i); }


                    //// Delete From StationAccreditationCommitteeShift
                    var deletedStationAccreditationCommitteeShiftRows = (from stacs in entities.Station_Accreditation_Committee_Shift
                                                            where stacs.Station_Accreditation_Committee_ID == x
                                                            select stacs).ToList();
                    foreach (var i in deletedStationAccreditationCommitteeShiftRows)
                    { entities.Station_Accreditation_Committee_Shift.Remove(i); }


                    //// Delete From committeeEmployee
                    var deletedcommitteeEmployeeRows = (from ce in entities.CommitteeEmployees
                                                                        where ce.Committee_ID == x && ce.OperationType==79
                                                                        select ce).ToList();
                    foreach (var i in deletedcommitteeEmployeeRows)
                    { entities.CommitteeEmployees.Remove(i); }

                    //Farm_Committee CModel = uow.Repository<Farm_Committee>().Findobject(x);
                    //CModel.User_Updation_Date = null;
                    //CModel.User_Updation_Id = null;
                    //CModel.CommitteeType_ID = null;
                    //CModel.Delegation_Date = null;
                    //CModel.StartTime = null;
                    //CModel.EndTime = null;
                    //CModel.IsApproved = null; // المفروض تكون حسب التاريخ اللى جاي مع الركوست لو بينها تبقى 1 لو لا تبقى null
                    //CModel.Status = null;
                    //context.SaveChanges();
                    //uow.Repository<Farm_Committee>().Update(CModel);
                    uow.SaveChanges();
                    entities.SaveChanges();
                }


                //return null;
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, 1);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
