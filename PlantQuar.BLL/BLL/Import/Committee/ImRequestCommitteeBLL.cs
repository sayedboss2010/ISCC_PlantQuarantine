using AutoMapper;
using PlantQuar.BLL.BLL.Farm.FarmRequest;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Import.Committee
{
    public class ImRequestCommitteeBLL
    {
        private UnitOfWork uow;

        public ImRequestCommitteeBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetCreationDateForRequest(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Im_CheckRequest>().GetData().FirstOrDefault(r => r.ID == id);
            if (req != null)
            {
                var dto = Mapper.Map<Im_CheckRequestDTO>(req);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dto);

                //  return req.User_Creation_Date.ToString();
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "");

            }
        }
        public Dictionary<string, object> checkRequestCommitteeExist(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Im_RequestCommittee>().GetData().FirstOrDefault(r => r.ImCheckRequest_ID == id && r.Delegation_Date != null);
            if (req != null)
            {

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, true);

                //  return req.User_Creation_Date.ToString();
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, false);

            }
        }
        public Dictionary<string, object> checkDissmissCommitteeExist(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Im_RequestCommittee>().GetData().FirstOrDefault(r => r.ImCheckRequest_ID == id && r.CommitteeType_ID == 7);
            if (req != null)
            {

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, true);

                //  return req.User_Creation_Date.ToString();
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, false);

            }
        }
        public Dictionary<string, object> DismissCommitteeExist(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Im_PermissionItem_Division_Custody_DismissCommittee>().GetData().FirstOrDefault(r => r.Im_RequestCommittee_Id == id);
            if (req != null)
            {

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, true);

                //  return req.User_Creation_Date.ToString();
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, false);

            }
        }
        public Dictionary<string, object> ReceiveCommitteeExist(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Im_PermissionItem_Division_Custody_ReceiveCommittee>().GetData().FirstOrDefault(r => r.Im_RequestCommittee_Id == id);
            if (req != null)
            {

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, true);

                //  return req.User_Creation_Date.ToString();
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, false);

            }
        }
        public Dictionary<string, object> executionCommitteeExist(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Im_Execution>().GetData().FirstOrDefault(r => r.Im_RequestCommittee_Id == id);
            if (req != null)
            {

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, true);

                //  return req.User_Creation_Date.ToString();
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, false);

            }
        }
        public Dictionary<string, object> GetReqCommittee(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Im_RequestCommittee>().GetData().FirstOrDefault(r => r.ImCheckRequest_ID == id);
            if (req != null)
            {

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, req.ID);

                //  return req.User_Creation_Date.ToString();
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, 0);

            }
        }
        public Dictionary<string, object> GetDismissCommittee(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Im_PermissionItem_Division_Custody_DismissCommittee>().GetData().FirstOrDefault(r => r.Im_RequestCommittee_Id == id);
            if (req != null)
            {

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, req.ID);

                //  return req.User_Creation_Date.ToString();
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, 0);

            }
        }

        public Dictionary<string, object> Insert_Committee(Im_RequestCommitteeDTO entity, List<string> Device_Info)
        {
            try
            {
                long Committe_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_RequestCommittee_seq");
                entity.ID = Committe_ID;
                var Co = Mapper.Map<Im_RequestCommitteeDTO, Im_RequestCommittee>(entity);
                uow.Repository<Im_RequestCommittee>().InsertReturn(Co);
                uow.SaveChanges();

                var operationType = 74; //ask                                      
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Insert_Employee(Im_RequestCommitteeDTO entity, List<string> Device_Info)
        {
            try
            {


                var operationType = 74; //ask

                #region Employee

                CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();

                if (entity.com_emp.Count > 0)
                {
                    CommitteeBLL committeeBLL = new CommitteeBLL();
                    committeeBLL.Send_Committe_Employee(entity.ID, operationType,
                        (DateTime)entity.User_Creation_Date, entity.User_Creation_Id, entity.com_emp, Device_Info);
                }
                #endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InsertDismissCommittee(Im_PermissionItem_Division_Custody_DismissCommitteeDTO entity, List<string> Device_Info)
        {
            try
            {
                var operationType = 91; //ask
                var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_PermissionItem_Division_Custody_DismissCommittee_seq");
                entity.ID = id;
                var Co = Mapper.Map<Im_PermissionItem_Division_Custody_DismissCommitteeDTO, Im_PermissionItem_Division_Custody_DismissCommittee>(entity);
                uow.Repository<Im_PermissionItem_Division_Custody_DismissCommittee>().InsertReturn(Co);
                uow.SaveChanges();

                #region Employee

                CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();

                if (entity.com_emp.Count > 0)
                {
                    CommitteeBLL committeeBLL = new CommitteeBLL();
                    committeeBLL.Send_Committe_Employee(Co.ID, operationType,
                        (DateTime)entity.User_Creation_Date, entity.User_Creation_Id, entity.com_emp, Device_Info);
                }
                #endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InsertDismissCommittee(Im_RequestCommitteeDTO entity, List<string> Device_Info)
        {
            try
            {


                //var operationType = 91; //ask
                //var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_RequestCommittee_seq");
                //entity.ID = id;
                //var Co = Mapper.Map<Im_RequestCommitteeDTO, Im_RequestCommittee>(entity);
                //uow.Repository<Im_RequestCommittee>().InsertReturn(Co);
                //uow.SaveChanges();

                //#region Employee

                //CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();

                //if (entity.com_emp.Count > 0)
                //{
                //    CommitteeBLL committeeBLL = new CommitteeBLL();
                //    committeeBLL.Send_Committe_Employee(Co.ID, operationType,
                //        (DateTime)entity.User_Creation_Date, entity.User_Creation_Id, entity.com_emp, Device_Info);
                //}
                //#endregion
                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, id);

                return null;
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InserReceiveCommittee(Im_PermissionItem_Division_Custody_ReceiveCommitteeDTO entity, List<string> Device_Info)
        {
            try
            {
                var operationType = 92; //ask
                var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_PermissionItem_Division_Custody_ReceiveCommittee_seq");
                entity.ID = id;
                var Co = Mapper.Map<Im_PermissionItem_Division_Custody_ReceiveCommitteeDTO, Im_PermissionItem_Division_Custody_ReceiveCommittee>(entity);
                uow.Repository<Im_PermissionItem_Division_Custody_ReceiveCommittee>().InsertReturn(Co);
                uow.SaveChanges();

                #region Employee

                CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();

                if (entity.com_emp.Count > 0)
                {
                    CommitteeBLL committeeBLL = new CommitteeBLL();
                    committeeBLL.Send_Committe_Employee(Co.ID, operationType,
                        (DateTime)entity.User_Creation_Date, entity.User_Creation_Id, entity.com_emp, Device_Info);
                }
                #endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InserReceiveCommittee(Im_RequestCommitteeDTO entity, List<string> Device_Info)
        {
            return null;
            //try
            //{
            //    var operationType = 92; //ask
            //    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_RequestCommittee_seq");
            //    entity.ID = id;
            //    var Co = Mapper.Map<Im_RequestCommitteeDTO, Im_RequestCommittee>(entity);
            //    uow.Repository<Im_RequestCommittee>().InsertReturn(Co);
            //    uow.SaveChanges();

            //    #region Employee

            //    CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();

            //    if (entity.com_emp.Count > 0)
            //    {
            //        CommitteeBLL committeeBLL = new CommitteeBLL();
            //        committeeBLL.Send_Committe_Employee(Co.ID, operationType,
            //            (DateTime)entity.User_Creation_Date, entity.User_Creation_Id, entity.com_emp, Device_Info);
            //    }
            //    #endregion
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, id);
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }
        public Dictionary<string, object> InserExecutionCommittee(Im_ExecutionDTO entity, List<string> Device_Info)
        {
            try
            {
                var operationType = 90; //ask
                var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_Execution_seq");
                entity.ID = id;
                var Co = Mapper.Map<Im_ExecutionDTO, Im_Execution>(entity);
                uow.Repository<Im_Execution>().InsertReturn(Co);
                uow.SaveChanges();

                #region Employee

                CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();

                if (entity.com_emp.Count > 0)
                {
                    CommitteeBLL committeeBLL = new CommitteeBLL();
                    committeeBLL.Send_Committe_Employee(Co.ID, operationType,
                        DateTime.Now, entity.User_Creation_Id, entity.com_emp, Device_Info);
                }
                #endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InserExecutionCommittee(Im_RequestCommitteeDTO entity, List<string> Device_Info)
        {
            return null;
            //try
            //{
            //    var operationType = 90; //ask
            //    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_RequestCommittee_seq");
            //    entity.ID = id;
            //    var Co = Mapper.Map<Im_RequestCommitteeDTO, Im_RequestCommittee>(entity);
            //    uow.Repository<Im_RequestCommittee>().InsertReturn(Co);
            //    uow.SaveChanges();

            //    #region Employee

            //    CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();

            //    if (entity.com_emp.Count > 0)
            //    {
            //        CommitteeBLL committeeBLL = new CommitteeBLL();
            //        committeeBLL.Send_Committe_Employee(Co.ID, operationType,
            //            DateTime.Now, entity.User_Creation_Id, entity.com_emp, Device_Info);
            //    }
            //    #endregion
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, id);
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }
        public Dictionary<string, object> SaveCommittee(string CheckRequest_Number, byte CommitteeType_ID, bool WithEmployee, List<string> Device_Info)
        {
            try
            {
                /*
                        get the check request data ->found->get check date
                                                                        ->not found->not found
                      get committe data where Status=0->still opened
                            */
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("CheckRequest_Number", SqlDbType.NVarChar);
                paramters_Type.Add("CommitteeType_ID", SqlDbType.TinyInt);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("CheckRequest_Number", CheckRequest_Number.ToString());
                paramters_Data.Add("CommitteeType_ID", CommitteeType_ID.ToString());  //"2018-12-26"

                var request = uow.Repository<CheckRequest_GetCommitte_Data_ResultDTO>().CallStored("CheckRequest_GetCommitte_Data", paramters_Type,
                    paramters_Data, Device_Info).FirstOrDefault();
                List<EmployeeDTO> EmployeeList = new List<EmployeeDTO>();
                if (WithEmployee && request.Committe_Id > 0)
                {
                    //get committee Employee
                    CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();
                    int OperationType = 0;
                    EmployeeList = committeeEmployeeBLL.GetAll_Data_ByCommitte((long)request.Committe_Id, OperationType, Device_Info);
                    request.Employee_list = EmployeeList;
                }
                //CheckRequest_GetCommitte_Data_ResultDTO data =new  CheckRequest_GetCommitte_Data_ResultDTO ();
                //data.Add("request", dic["obj"]);
                //data.Add("EmployeeList", EmployeeList);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Save_Lot(int lotss , List<Im_CommitteeResultDTO> CheckedItemsList, List<string> Device_Info)
        {
            try
            {
                //var operationType = 90; //ask
                //var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CommitteeResult_SEQ");
                //entity.ID = id;
                //var Co = Mapper.Map<Im_CommitteeResultDTO, Im_CommitteeResult>(entity);
                //uow.Repository<Im_CommitteeResultDTO>().InsertReturn(Co);
                //uow.SaveChanges();
                foreach (var item in CheckedItemsList)
                {
                    long CommitteResult_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CommitteeResult_SEQ");
                    item.ID = CommitteResult_ID;
                //    item.im_RequestCommittee.ID = lotssss;
                    var Co = Mapper.Map<Im_CommitteeResultDTO, Im_CommitteeResult>(item);
                    uow.Repository<Im_CommitteeResult>().InsertReturn(Co);
                    uow.SaveChanges();
                 
                }             


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, CheckedItemsList);


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Save_AnalysisList(int AnalysisList ,  List<Im_CheckRequest_SampleDataDTO> CheckedAnalysisList, List<string> Device_Info)
        {
            try
            {
               
                if (CheckedAnalysisList != null && CheckedAnalysisList.Count > 0)
                {
                    foreach (var Item_Analysis in CheckedAnalysisList)
                    {
                        long Im_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CheckRequest_SampleData_SEQ");
                        Item_Analysis.ID = Im_CheckRequest_SampleData_ID;
                        //    item.im_RequestCommittee.ID = lotssss;
                        var Co = Mapper.Map<Im_CheckRequest_SampleDataDTO, Im_CheckRequest_SampleData>(Item_Analysis);
                        uow.Repository<Im_CheckRequest_SampleData>().InsertReturn(Co);
                        uow.SaveChanges();

                    }
                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, CheckedAnalysisList);


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
