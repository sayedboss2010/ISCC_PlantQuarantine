using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmRequest
{
    public class EmployeeBLL<T> : IGenericBLL<T>
    {
        private UnitOfWork uow;

        //   List<CustomOption> Emp_List;

        public EmployeeBLL()
        {
            uow = new UnitOfWork();
            //Emp_List = new List<CustomOption>();
            //Emp_List.Add(new CustomOption() { DisplayText = "أحمد الحجار", Value = 1 });
            //Emp_List.Add(new CustomOption() { DisplayText = "محمد السيد", Value = 2 });
            //Emp_List.Add(new CustomOption() { DisplayText = "مصطفى حسام الدين", Value = 3 });
            //Emp_List.Add(new CustomOption() { DisplayText = "علا", Value = 4 });
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                //complete code form oracle
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "");
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            //complete code form oracle
            var count = 10;
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, null);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAllEmp( List<string> Device_Info)
        {
            try
            {
                dbPrivilageEntities prv = new dbPrivilageEntities();
                var users = prv.PR_User.ToList();
                var _DTO = Mapper.Map<List<PR_User>, List<User>>(users);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _DTO);
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
        public bool GetAny(T entity)
        {
            //complete code form oracle
            return true;
        }
        //******************************************//
        public Dictionary<string, object> Insert(T entity, List<string> Device_Info)
        {
            try
            {
                //complete code form oracle
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "");

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Update(T entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    //complete code form oracle

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "");
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
            //complete code
            throw new NotImplementedException();
        }

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            //complete code form oracle

            var data = new List<CustomOption>();
            data.Add(new CustomOption() { DisplayText = "أحمد الحجار", Value = 1 });
            data.Add(new CustomOption() { DisplayText = "محمد السيد", Value = 2 });
            data.Add(new CustomOption() { DisplayText = "مصطفى حسام الدين", Value = 3 });
            data.Add(new CustomOption() { DisplayText = "علا", Value = 4 });
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());

            //complete code form oracle
            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "");
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            //complete code form oracle

            var data = new List<CustomOption>();
            data.Add(new CustomOption() { DisplayText = "أحمد الحجار", Value = 1 });
            data.Add(new CustomOption() { DisplayText = "محمد السيد", Value = 2 });
            data.Add(new CustomOption() { DisplayText = "مصطفى حسام الدين", Value = 3 });
            data.Add(new CustomOption() { DisplayText = "علا", Value = 4 });
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}
