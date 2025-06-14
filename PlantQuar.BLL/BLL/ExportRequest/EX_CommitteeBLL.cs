using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequest
{
   public class EX_CommitteeBLL
    {
        private UnitOfWork uow;

        public EX_CommitteeBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> FillCommitteeType_List(int Lst, List<string> Device_Inf)
        {           
            string lang = Device_Inf[2];
            var data = uow.Repository<CommitteeType>().GetData().Where(lab => lab.User_Deletion_Id == null)
                .Select(c => new CustomOptionShortId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionShortId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

            //var data = uow.Repository<CommitteeType>().GetData().Where(x => x.User_Deletion_Id == null)
            //    .Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest entity = uow.Repository<Ex_CheckRequest>().Findobject(Id);
                var empDTO = Mapper.Map<Ex_CheckRequest, Export_CheckRequestDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO.CheckRequest_Number);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FindCreationDate(object Id, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest entity = uow.Repository<Ex_CheckRequest>().Findobject(Id);
                var empDTO = Mapper.Map<Ex_CheckRequest, Export_CheckRequestDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO.User_Creation_Date);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(string Employee_No, string Employee_Name, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                try
                {

//                    SELECT cast(v_emp.Id as bigint) as Employee_Id--v_emp.EmpId as Employee_Id
//,v_emp.EmpId as Employee_no   ,v_emp.FullName as Employee_name
//	  , cast(0 AS bit) AS ISAdmin
//  FROM dbPrivilage_New.dbo.PR_User v_emp
//  WHERE
//    (len(@Employee_Name) = 0 or v_emp.FullName   like N'' + @Employee_Name + '%')
//and(len(@Employee_No) = 1 or v_emp.EmpId = @Employee_No)

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    Int64 data_Count = 0;

                    Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();

                    paramters_Type.Add("Employee_No", SqlDbType.BigInt);
                    paramters_Type.Add("Employee_Name", SqlDbType.NVarChar);

                    Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                    paramters_Data.Add("Employee_No", (Employee_No == null ? "0" : Employee_No));
                    paramters_Data.Add("Employee_Name", (Employee_Name == null ? "" : Employee_Name));

                    var data = uow.Repository<oracle_GetEmployee_Result>().CallStored("oracle_GetEmployee", paramters_Type,
                        paramters_Data, Device_Info).ToList();

                    data_Count = data.Count();
                    dic.Add("Count_Data", data_Count);
                    dic.Add("CommitteeEmployee_Data", data);

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
                }
                catch (Exception ex)
                {
                    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //public bool GetAny(EX_CommitteeDTO entity)
        //{
        //    //var obj = entity as Ex_RequestCommitteeDTO;
        //    //return uow.Repository<Ex_RequestCommittee>().GetAny(p => (p.User_Deletion_Id == null )&& (obj.ID == 0 ? true : p.ID != obj.ID));
        //    return false;
        //}


        //public Dictionary<string, object> Insert(EX_CommitteeDTO entity, List<string> Device_Info)
        //{
        //    try
        //    {
        //        if (!GetAny(entity))
        //        {
        //            using (PlantQuarantineEntities context = new PlantQuarantineEntities())
        //            {
        //                using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
        //                {
        //                    var operationType = 73;
        //                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_RequestCommittee_seq");
        //                    var Comm_In = new Ex_RequestCommittee
        //                    {
        //                        ID = id,
        //                        ExCheckRequest_ID = entity.ExCheckRequest_ID,
        //                        CommitteeType_ID = entity.CommitteeType_ID,
        //                        Delegation_Date = entity.Delegation_Date,
        //                        StartTime = entity.StartTime,
        //                        EndTime = entity.EndTime,
        //                        IsApproved = entity.IsApproved,
        //                        Status = entity.Status,
        //                        User_Creation_Id = entity.User_Creation_Id,
        //                        User_Creation_Date = entity.User_Creation_Date,
        //                    };
        //                    context.Ex_RequestCommittee.Add(Comm_In);
        //                    context.SaveChanges();


        //                    //var CModel = Mapper.Map<Ex_RequestCommittee>(entity);
        //                    //CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_RequestCommittee_seq");

        //                    //var req_com = uow.Repository<Ex_RequestCommittee>().InsertReturn(CModel);

        //                    //uow.SaveChanges();

        //                    #region Employee
        //                    #region Employee    
        //                    if (entity.com_emp != null)
        //                    {
        //                        if (entity.com_emp.Count > 0)
        //                        {
        //                            foreach (var item in entity.com_emp)
        //                            {
        //                                long _Employee_Id = long.Parse(item.Employee_Id.ToString());
        //                                var Comm_Employee = new CommitteeEmployee
        //                                {
        //                                    Committee_ID = Comm_In.ID,
        //                                    Employee_Id = _Employee_Id,
        //                                    ISAdmin = item.ISAdmin,
        //                                    OperationType = operationType,

        //                                    User_Creation_Id = entity.User_Creation_Id,
        //                                    User_Creation_Date = entity.User_Creation_Date,

        //                                };
        //                                context.CommitteeEmployees.Add(Comm_Employee);
        //                                context.SaveChanges();
        //                            }
        //                            //CommitteeBLL committeeBLL = new CommitteeBLL();
        //                            //committeeBLL.Send_Committe_Employee(entity.ID, operationType,
        //                            //    (DateTime)entity.User_Creation_Date, entity.User_Creation_Id, entity.com_emp, Device_Info);
        //                        }
        //                    }
        //                    #endregion



        //                    //if (entity.com_emp != null)
        //                    //{
        //                    //    Send_Committe_Employee(req_com.ID, entity.OperationType,
        //                    //    entity.User_Creation_Date, entity.User_Creation_Id,
        //                    //    entity.com_emp, Device_Info);
        //                    //}

        //                    #endregion
        //                    trans.Commit();
        //                }


        //            }

        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
        //        }
        //        else
        //        {
        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        //public Dictionary<string, object> Update(EX_CommitteeDTO entity, List<string> Device_Info)
        //{
        //    try
        //    {
        //        if (!GetAny(entity))
        //        {
        //            using (PlantQuarantineEntities context = new PlantQuarantineEntities())
        //            {
        //                using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
        //                {
        //                    var operationType = 73; //ask
        //                    long Committe_ID = entity.ID;
        //                    Ex_RequestCommittee CModel = uow.Repository<Ex_RequestCommittee>().Findobject(Committe_ID);
        //                    entity.User_Updation_Date = entity.User_Creation_Date;
        //                    entity.User_Updation_Id = entity.User_Creation_Id;
        //                    entity.User_Creation_Date = CModel.User_Creation_Date;
        //                    entity.User_Creation_Id = CModel.User_Creation_Id;

        //                    var Co = Mapper.Map(entity, CModel);                            
        //                    uow.Repository<Ex_RequestCommittee>().Update(Co);
        //                    uow.SaveChanges();

        //                    #region Employee              
        //                    if (entity.com_emp.Count > 0)
        //                    {
        //                        foreach (var item in entity.com_emp)
        //                        {
        //                            long _Employee_Id = long.Parse(item.Employee_Id.ToString());
        //                            var Comm_Employee = new CommitteeEmployee
        //                            {
        //                                Committee_ID = Committe_ID,
        //                                Employee_Id = _Employee_Id,
        //                                ISAdmin = item.ISAdmin,
        //                                OperationType = operationType,

        //                                User_Creation_Id = entity.User_Creation_Id,
        //                                User_Creation_Date = entity.User_Creation_Date,

        //                            };
        //                            context.CommitteeEmployees.Add(Comm_Employee);
        //                            context.SaveChanges();
        //                        }
        //                        //CommitteeBLL committeeBLL = new CommitteeBLL();
        //                        //committeeBLL.Send_Committe_Employee(entity.ID, operationType,
        //                        //    (DateTime)entity.User_Creation_Date, entity.User_Creation_Id, entity.com_emp, Device_Info);
        //                    }
        //                    #endregion

        //                    trans.Commit();
        //                }
        //            }                    
        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
        //        }
        //        else
        //        {
        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}


        public void Send_Committe_Employee(long Committee_ID, int OperationType,
         DateTime Create_date, long? Employee_Id,
         List<EX_EmployeeDTO> com_emp, List<string> Device_Info)
        {
            EX_EmployeeDTO committeeEmployeeDTO = new EX_EmployeeDTO();

            var CModel = Mapper.Map<CommitteeEmployee>(committeeEmployeeDTO);
            //CommitteeEmployeeBLL committeeEmployeeBLL = new CommitteeEmployeeBLL();
            committeeEmployeeDTO.User_Creation_Id =Employee_Id;
            committeeEmployeeDTO.User_Creation_Date = Create_date;

            foreach (var item in com_emp)
            {
                committeeEmployeeDTO.Employee_Id = (long)item.Employee_Id;
                committeeEmployeeDTO.ISAdmin = item.ISAdmin;
                committeeEmployeeDTO.Committee_ID = Committee_ID;
                committeeEmployeeDTO.OperationType = OperationType;

                uow.Repository<CommitteeEmployee>().InsertRecord(CModel);
                uow.SaveChanges();
                //CModel.Insert(committeeEmployeeDTO, Device_Info);
            }
        }

    }
}
