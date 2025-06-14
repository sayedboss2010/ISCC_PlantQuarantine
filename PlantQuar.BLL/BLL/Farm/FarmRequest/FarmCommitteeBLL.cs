using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmRequest
{
    public class FarmCommitteeBLL
    {
        private UnitOfWork uow;

        public FarmCommitteeBLL()
        {
            uow = new UnitOfWork();
        }
        public bool GetAny(Farm_CommitteeDTO entity)
        {
            //var obj = entity as Ex_RequestCommitteeDTO;
            //return uow.Repository<Ex_RequestCommittee>().GetAny(p => (p.User_Deletion_Id == null )&& (obj.ID == 0 ? true : p.ID != obj.ID));
            return false;
        }
        public Dictionary<string, object> Insert(Farm_CommitteeDTO entity, List<string> Device_Info)
        {
            try
            {

                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Farm_Committee>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_seq");

                    var req_com = uow.Repository<Farm_Committee>().InsertReturn(CModel);
                    uow.SaveChanges();

                    #region Employee

                    #endregion


                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update(Farm_CommitteeDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {

                    DateTime? User_Updation_Date = entity.User_Updation_Date;
                    short? User_Updation_Id = entity.User_Updation_Id;

                    long Committe_ID = entity.ID;
                    Farm_Committee CModel = uow.Repository<Farm_Committee>().Findobject(Committe_ID);

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;
                    //entity.IsPaid = CModel.IsPaid;
                    //entity.IsApproved = CModel.IsApproved;
                    entity.Amount_Total = CModel.Amount_Total;
                    //entity.Status = CModel.Status;
                    //entity.CommitteeType_ID = CModel.CommitteeType_ID;

                    if (CModel.User_Updation_Id != null)
                    {
                        entity.User_Updation_Date = User_Updation_Date;
                        entity.User_Updation_Id = User_Updation_Id;
                    }
                    //entity.StartTime = CModel.StartTime;//  new TimeSpan( 23, 00, 00);
                    //entity.EndTime = CModel.EndTime;// new TimeSpan(23, 00, 00);


                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<Farm_Committee>().Update(Co);
                    uow.SaveChanges();

                    #region Employee
                   
                    CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();
                     
                    List<EmployeeDTO> Employee_CurrentList = (committeeEmployeeBLL.GetAll_IDs_ByCommitte(Committe_ID,entity.OperationType, Device_Info));
                    List<EmployeeDTO> EmployeeList = entity.com_emp;

                    List<EmployeeDTO> list_deleted = Employee_CurrentList.Except(EmployeeList).ToList(); //deleted list
                    long committe_Id = entity.ID;
                    if (list_deleted.Count > 0)
                    {
                         DeleteParameters obj = new DeleteParameters();
                        foreach (var item in list_deleted)
                        {
                            obj.id =(long)item.Employee_Id;
                            obj.Userid = (short)entity.User_Updation_Id;
                            obj._DateNow = (DateTime)entity.User_Updation_Date;
                            committeeEmployeeBLL.Delete(obj, committe_Id, Device_Info);
                        }
                    }
                    List<EmployeeDTO> list_New = EmployeeList.Except(Employee_CurrentList).ToList(); //New list
                    if (list_New.Count > 0)
                    {
                        CommitteeBLL committeeBLL = new CommitteeBLL();
                        committeeBLL.Send_Committe_Employee(committe_Id, entity.OperationType,
                            (DateTime)User_Updation_Date, User_Updation_Id, list_New, Device_Info);
                    }
                    #endregion
                    if (entity.CommitteeType_ID == 5 || entity.CommitteeType_ID == 12)
                    {
                         var farmReqId = uow.Repository<Farm_Committee>().GetData().Where(c => c.ID == Committe_ID).FirstOrDefault().Farm_Request_ID;

 

                        var farmcategories = uow.Repository<Farm_Request_ItemCategories>().GetData().Where(f => f.Farm_Request_ID ==farmReqId).ToList();
                        Farm_Committee_ExaminationDTO dto;
                         foreach (var cat in farmcategories)
                        {
                            dto = new Farm_Committee_ExaminationDTO();
                             dto.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_Examination_seq");
                            dto.Farm_Request_ItemCategories_ID = cat.ID;
                            dto.IsAccepted = null;
                            dto.FarmCommittee_ID = entity.ID;
                            dto.User_Creation_Id = (short)entity.User_Updation_Id;
                            dto.User_Creation_Date = (System.DateTime)entity.User_Updation_Date;

                            var obj = Mapper.Map<Farm_Committee_Examination>(dto);

                            uow.Repository<Farm_Committee_Examination>().InsertRecord(obj);
                            uow.SaveChanges();
                        }
                    }
                     var existcon = uow.Repository<Farm_Committee_Constrain>().GetData().Where(k => k.Farm_Committee_ID == entity.ID).ToList();
                    if(existcon.Count <= 0)
                    {
                        var req = uow.Repository<Farm_Request>().GetData().Include(f => f.FarmsData).Where(r => r.ID == entity.Farm_Request_ID).FirstOrDefault();
                        var itemId = req.FarmsData.Item_ID;
                        var countriesReq = uow.Repository<Farm_Country>().GetData().Where(c => c.Farm_Request_ID == entity.Farm_Request_ID).Select(d => d.Country_ID).ToList();
                        Farm_Committee_ConstrainDTO dto2;
                        foreach (var cn in countriesReq)
                        {
                            var farmConstrain = uow.Repository<Farm_Constrain>().GetData().Where(k => k.Item_ID == itemId && k.Country_Id == cn).ToList();


                            foreach (var con in farmConstrain)
                            {
                                dto2 = new Farm_Committee_ConstrainDTO();

                                dto2.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_Constrain_seq");
                                dto2.Farm_Committee_ID = entity.ID;
                                dto2.Farm_Constrain_ID = con.ID;
                                  var obj2 = Mapper.Map<Farm_Committee_Constrain>(dto2);

                                uow.Repository<Farm_Committee_Constrain>().InsertRecord(obj2);
                                uow.SaveChanges();

                            }
                        }
                    }
                   
                    //end 
                    var empDTO = Mapper.Map<Farm_Committee, Farm_CommitteeDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Farm_Committee entity = uow.Repository<Farm_Committee>().Findobject(Id);
                string lang = Device_Info[2];

                var empDTO = Mapper.Map<Farm_Committee, Farm_CommitteeDTO>(entity);
                //empDTO.farmcode = entity.Farm_Country_Request.FarmsData.FarmCode_14;
                empDTO.farmName = lang == "1" ? entity.Farm_Request.FarmsData.Name_Ar : entity.Farm_Request.FarmsData.Name_En;

                //var farm = uow.Repository<Farm_Committee>()
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, empDTO);

                //need to adjust code Farm_Country_Request
                //return null;

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> ConfirmFarmCommittee(Farm_Committee_ConfirmDTO entity, List<string> Device_Info)
        {
            try
            {
                var CModel = Mapper.Map<Farm_Committee_Examination_Confirm>(entity);
                CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_Confirm_seq");

                var CreatedModel = uow.Repository<Farm_Committee_Examination_Confirm>().InsertReturn(CModel);
                uow.SaveChanges();

                #region Android Location Saving
                Andriod_LocationDTO andriod_LocationDTO = new Andriod_LocationDTO();
                Andriod_LocationBLL androidLocationBLL = new Andriod_LocationBLL();

                andriod_LocationDTO.Committe_ID = CreatedModel.ID;
                andriod_LocationDTO.IsExport = true;
                andriod_LocationDTO.Operation_ID = (byte)Enums.Android_Operation.FormCommitte;
                andriod_LocationDTO.User_Id = entity.EmployeeId;
                andriod_LocationDTO.Latitude = entity.Latitude;
                andriod_LocationDTO.Longitude = entity.Longitude;
                andriod_LocationDTO.Created_Date = (DateTime)entity.Date;

                androidLocationBLL.Insert(andriod_LocationDTO, Device_Info);

                #endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> InsertFarmCommittee(Farm_SampleDataDTO data, List<string> Device_Info)
        {
            try
            {
                var CModel = Mapper.Map<Farm_SampleData>(data);
                CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_SampleData_seq");

                var CreatedModel = uow.Repository<Farm_SampleData>().InsertReturn(CModel);

                uow.SaveChanges();

                //update the committe to be finished

                Update_Finish(data.FarmCommittee_ID, (short)data.User_Creation_Id, (DateTime)data.User_Creation_Date, Device_Info);

                #region Android Location Saving
                Andriod_LocationDTO andriod_LocationDTO = new Andriod_LocationDTO();
                Andriod_LocationBLL androidLocationBLL = new Andriod_LocationBLL();

                andriod_LocationDTO.Committe_ID = CreatedModel.FarmCommittee_ID;
                andriod_LocationDTO.IsExport = true;
                andriod_LocationDTO.Operation_ID = (byte)Enums.Android_Operation.FormCommitte;
                andriod_LocationDTO.User_Id = data.User_Creation_Id;
                andriod_LocationDTO.Latitude = data.Latitude;
                andriod_LocationDTO.Longitude = data.Longitude;
                andriod_LocationDTO.Created_Date = (DateTime)data.WithdrawDate;

                androidLocationBLL.Insert(andriod_LocationDTO, Device_Info);

                #endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, null);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        private Dictionary<string, object> Update_Finish(Int64 committee_ID, short user_Creation_Id, DateTime user_Creation_Date, List<string> Device_Info)
        {
            try
            {
                Farm_Committee CModel = uow.Repository<Farm_Committee>().Findobject(committee_ID);
                CModel.Status = true;
                CModel.User_Updation_Id = user_Creation_Id;
                CModel.User_Updation_Date = user_Creation_Date;

                uow.Repository<Farm_Committee>().Update(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, CModel);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Find_CommitteeResult(Int64 FramCommitte_Id, List<string> Device_Info)
        {
            try
            {
                Farm_SampleDataDTO entity = uow.Repository<Farm_SampleData>()
                    .GetData().Where(a => a.FarmCommittee_ID == FramCommitte_Id)
                    .Select(f => new Farm_SampleDataDTO()
                    {
                        WithdrawDate = f.WithdrawDate,
                        Sample_BarCode = f.Sample_BarCode,
                        SampleSize = f.SampleSize,
                        SampleRatio = f.SampleRatio,
                        IsAccepted = f.IsAccepted,
                        RejectReason_Ar = f.RejectReason_Ar,
                        Notes_Ar = f.Notes_Ar
                       ,
                        AnalysiType_Name_Ar = f.AnalysisLabType.AnalysisType.Name_Ar,
                        AnalysiLab_Name_Ar = f.AnalysisLabType.AnalysisLab.Name_Ar
                    }
                 ).FirstOrDefault();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
