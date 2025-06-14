using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmRequest
{
    public class CommitteeBLL : IGenericBLL<Ex_RequestCommitteeDTO>
    {
        private UnitOfWork uow;

        public CommitteeBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Ex_RequestCommittee entity = uow.Repository<Ex_RequestCommittee>().Findobject(Id);
                var empDTO = Mapper.Map<Ex_RequestCommittee, Ex_RequestCommitteeDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Ex_RequestCommittee>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                var data = uow.Repository<Ex_RequestCommittee>().GetData(pageSize, index, A => 1 == 1)
                    .Where(a => a.User_Deletion_Id == null).ToList();
                var dataDTO = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<Ex_RequestCommittee, Ex_RequestCommitteeDTO>);
                var data_Count = dataDTO.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("RequestCommittee_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }

        public Dictionary<string, object> GetCommittee(string CheckRequest_Number, byte CommitteeType_ID, bool WithEmployee, List<string> Device_Info)
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

        public bool GetAny(Ex_RequestCommitteeDTO entity)
        {
            //var obj = entity as Ex_RequestCommitteeDTO;
            //return uow.Repository<Ex_RequestCommittee>().GetAny(p => (p.User_Deletion_Id == null )&& (obj.ID == 0 ? true : p.ID != obj.ID));
            return false;
        }
        //******************************************//
        public Dictionary<string, object> Insert(Ex_RequestCommitteeDTO entity, List<string> Device_Info)
        {
            try
            {

                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Ex_RequestCommittee>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_RequestCommittee_seq");

                    var req_com = uow.Repository<Ex_RequestCommittee>().InsertReturn(CModel);
                    uow.SaveChanges();

                    #region Employee
                    if (entity.com_emp != null)
                    {
                        Send_Committe_Employee(req_com.ID, entity.OperationType,
                        entity.User_Creation_Date, entity.User_Creation_Id,
                        entity.com_emp, Device_Info);
                    }

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

        /// <summary>
        /// adjust employee data
        /// </summary>
        /// <param name="Employee_Status"> 1->insert employee    / 0->delete employee </param>
        public void Send_Committe_Employee(long Committee_ID, int OperationType,
            DateTime Create_date, long? Employee_Id,
            List<EmployeeDTO> com_emp, List<string> Device_Info)
        {
            CommitteeEmployeeDTO committeeEmployeeDTO = new CommitteeEmployeeDTO();

            CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();
            committeeEmployeeDTO.User_Creation_Id = Employee_Id;
            committeeEmployeeDTO.User_Creation_Date = Create_date;

            foreach (var item in com_emp)
            {
                committeeEmployeeDTO.Employee_Id = (long)item.Employee_Id;
                committeeEmployeeDTO.ISAdmin = item.ISAdmin;
                committeeEmployeeDTO.Committee_ID = Committee_ID;
                committeeEmployeeDTO.OperationType = OperationType;
                committeeEmployeeBLL.Insert(committeeEmployeeDTO, Device_Info);
            }
        }

        public Dictionary<string, object> Update(Ex_RequestCommitteeDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    long Committe_ID = entity.ID;
                    Ex_RequestCommittee CModel = uow.Repository<Ex_RequestCommittee>().Findobject(Committe_ID);




                    entity.User_Updation_Date = entity.User_Creation_Date;
                    entity.User_Updation_Id = entity.User_Creation_Id;

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;

                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<Ex_RequestCommittee>().Update(Co);
                    uow.SaveChanges();

                    #region Employee
                    /*
                     get current employees list and compare to get differance
                     new->insert
                     old->update
                     missed->delete
                     */
                    CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();

                    List<EmployeeDTO> Employee_CurrentList = (committeeEmployeeBLL.GetAll_IDs_ByCommitte(Committe_ID, Device_Info));
                    List<EmployeeDTO> EmployeeList = entity.com_emp;

                    List<EmployeeDTO> list_deleted = Employee_CurrentList.Except(EmployeeList).ToList(); //deleted list
                    long committe_Id = entity.ID;
                    if (list_deleted.Count > 0)
                    {
                        // Send_Committe_Employee(entity.ID, entity.CommitteeType_ID, list_deleted, Device_Info, 1);
                        DeleteParameters obj = new DeleteParameters();
                        foreach (var item in list_deleted)
                        {
                            obj.id = (long)item.Employee_Id;
                            obj.Userid = (short)entity.User_Updation_Id;
                            obj._DateNow = (DateTime)entity.User_Updation_Date;
                            committeeEmployeeBLL.Delete(obj, committe_Id, Device_Info);
                        }
                    }
                    List<EmployeeDTO> list_New = EmployeeList.Except(Employee_CurrentList).ToList(); //New list
                    if (list_New.Count > 0)
                    {
                        Send_Committe_Employee(committe_Id, entity.OperationType,
                         (DateTime)entity.User_Updation_Date, (short)entity.User_Updation_Id,
                            list_New, Device_Info);
                    }
                    #endregion
                    var empDTO = Mapper.Map<Ex_RequestCommittee, Ex_RequestCommitteeDTO>(Co);
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
        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Ex_RequestCommittee>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = (short)dto.Userid;
                    uow.Repository<Ex_RequestCommittee>().Update(Cmodel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, Cmodel);
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

        //public Dictionary<string, object> Committee_List()
        //{
        //    var data = uow.Repository<Ex_RequestCommittee>().GetData().Where(lab => lab.User_Deletion_Id == null)
        //        .Select(c => new CustomOptionLongId { DisplayText = c.Ex_CheckRequest.Certificate_Number, Value = c.ID }).ToList();
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        //}

        public Dictionary<string, object> UpdateStatus(DeleteParameters obj, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Ex_RequestCommittee>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.IsApproved = true;
                    Cmodel.User_Updation_Date = obj._DateNow;
                    Cmodel.User_Updation_Id = (short)obj.Userid;
                    uow.Repository<Ex_RequestCommittee>().Update(Cmodel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, Cmodel);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(long? itemId, byte? itemType, int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}
