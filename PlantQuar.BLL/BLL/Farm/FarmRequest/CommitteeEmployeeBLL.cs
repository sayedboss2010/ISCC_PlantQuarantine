using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Committees;
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
    public class CommitteeEmployeeBLL : IGenericBLL<CommitteeEmployeeDTO>
    {
        private UnitOfWork uow;

        public CommitteeEmployeeBLL()
        {
            uow = new UnitOfWork();
        }

        //Find CommitteeEmployee Obj
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                CommitteeEmployee entity = uow.Repository<CommitteeEmployee>().Findobject(Id);
                var empDTO = Mapper.Map<CommitteeEmployee, CommitteeTypeDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Count CommitteeEmployee
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<CommitteeEmployee>().GetData().Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        //Get CommitteeEmployee List 
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                //complete code
                return null;
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get CommitteeEmployee List Search by ArName & EnName
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                //complete code
                return null;
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //Get CommitteeEmployee List 
        public List<EmployeeDTO> GetAll_IDs_ByCommitte(long Committe_ID, List<string> Device_Info)
        {
            try
            {
                var EmployeeList = uow.Repository<CommitteeEmployee>().GetData().
                    Where(p => p.Committee_ID == Committe_ID && p.User_Deletion_Id == null).Select(p => new EmployeeDTO
                    {
                        Employee_Id = p.Employee_Id,
                        ISAdmin = p.ISAdmin
                    }).ToList();
                return EmployeeList;
                //uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, EmployeeList);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return null;//
                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public List<EmployeeDTO> GetAll_IDs_ByCommitte(long Committe_ID,int operationType, List<string> Device_Info)
        {
            try
            {
                var EmployeeList = uow.Repository<CommitteeEmployee>().GetData().
                    Where(p => p.Committee_ID == Committe_ID && p.User_Deletion_Id == null &&p.OperationType == operationType).Select(p => new EmployeeDTO
                    {
                        Employee_Id = p.Employee_Id,
                        ISAdmin = p.ISAdmin
                    }).ToList();
                return EmployeeList;
                //uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, EmployeeList);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return null;//
                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public List<EmployeeDTO> GetAll_Data_ByCommitte(long Committe_ID, int OperationType, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("Committee_ID", SqlDbType.BigInt);
                paramters_Type.Add("OperationType", SqlDbType.Int);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("Committee_ID", Committe_ID.ToString());
                paramters_Data.Add("OperationType", OperationType.ToString());

                //var EmployeeList = uow.Repository<oracle_GetEmployee_ByCommittee_Result>().CallStored("oracle_GetEmployee_ByCommittee", paramters_Type,
                //    paramters_Data, Device_Info).Select(p => new EmployeeDTO
                //    {
                //        Employee_Id = (decimal)p.Employee_Id,
                //        Employee_name = p.Employee_name,
                //        Employee_no = p.Employee_Id,
                //    }).ToList();
                //return EmployeeList;

                return null;
                //uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, EmployeeList);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return null;//
                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        //Get Any CommitteeEmployee
        public bool GetAny(CommitteeEmployeeDTO entity)
        {
            //complete code
            return false;
        }

        //Create CommitteeEmployee
        public Dictionary<string, object> Insert(CommitteeEmployeeDTO entity, List<string> Device_Info)
        {
            try
            {

                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<CommitteeEmployee>(entity);

                    uow.Repository<CommitteeEmployee>().InsertRecord(CModel);
                    uow.SaveChanges();
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

        //Update CommitteeEmployee
        public Dictionary<string, object> Update(CommitteeEmployeeDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    //complete code
                    return null;
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

        //Delete CommitteeEmployee
        public Dictionary<string, object> Delete(DeleteParameters obj,long committeeId, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<CommitteeEmployee>().GetData().FirstOrDefault(c => c.Committee_ID == committeeId && c.Employee_Id == obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, null);
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
    }
}
