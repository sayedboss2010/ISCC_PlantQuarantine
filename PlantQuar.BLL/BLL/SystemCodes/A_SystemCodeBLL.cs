using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.SystemCodes;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;


namespace PlantQuar.BLL.BLL.SystemCodes
{
    public class A_SystemCodeBLL<T> : IGenericBLL<T>
    {
        private UnitOfWork uow;

        public A_SystemCodeBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                A_SystemCode entity = uow.Repository<A_SystemCode>().Findobject(Id);
                var empDTO = Mapper.Map<A_SystemCode, A_SystemCodeDTO>(entity);
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
            var count = uow.Repository<A_SystemCode>().GetData().Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<A_SystemCode>().GetData().
                    OrderBy(c => (lang == "1" ? c.ValueName : c.ValueNameEN)
          ).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<A_SystemCode, A_SystemCodeDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<A_SystemCode>().GetData().ToList();

                //if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                //{
                //    data = data.Where(a => a.Name_En.StartsWith(enName)).ToList();
                //}
                //else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                //{
                //    data = data.Where(a => a.Name_Ar.StartsWith(arName)).ToList();
                //}
                //else
                //{
                //    data = data.Where(a => (a.Name_Ar.StartsWith(arName) || a.Name_En.StartsWith(enName))).ToList();
                //}

                string lang = Device_Info[2];
                var dataDto = data.OrderBy(A => (lang == "1" ? A.ValueName : A.ValueNameEN)).Skip(index).Take(pageSize).Select(Mapper.Map<A_SystemCode, A_SystemCodeDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDto);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(T entity)
        {
            //var obj = entity as A_SystemCodeDTO;
            //return uow.Repository<A_SystemCode>().GetAny(p => (p.User_Deletion_Id == null &&
            //                            (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
            return false;
        }
        //******************************************//
        public Dictionary<string, object> Insert(T entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<A_SystemCode>(entity);
                    CModel.Id = uow.Repository<Object>().GetNextSequenceValue_Int("A_SystemCode_Seq");

                    uow.Repository<A_SystemCode>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(T entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as A_SystemCodeDTO;
                    A_SystemCode CModel = uow.Repository<A_SystemCode>().Findobject(obj.Id);

                    //obj.User_Creation_Date = CModel.User_Creation_Date;
                    //obj.User_Creation_Id = CModel.User_Creation_Id;

                    //if (CModel.User_Updation_Id != null)
                    //{
                    //    obj.User_Updation_Date = CModel.User_Updation_Date;
                    //    obj.User_Updation_Id = CModel.User_Updation_Id;
                    //}

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<A_SystemCode>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<A_SystemCode, A_SystemCodeDTO>(Co);
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

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<A_SystemCode>().GetData().
                Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.ValueName : c.ValueNameEN),
                Value = c.Id
            }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> FillDropByCode_Add(int Syscode, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<A_SystemCode>().GetData().Where(x => x.SystemCodeTypeId == Syscode).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.ValueName : c.ValueNameEN),
                Value = c.Id
            }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        // Mahmoud Saber ...
        public Dictionary<string, object> FillDrop_Edit(int Sysnumber, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<A_SystemCode>().GetData().Where(a => a.SystemCodeTypeId == Sysnumber).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.ValueName : c.ValueNameEN),
                Value = c.Id
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }


        public Dictionary<string, object> FillDrop_Plant(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<A_SystemCode>().GetData().Where(a => a.SystemCodeTypeId == 18 && a.Value == 1).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.ValueName : c.ValueNameEN),
                Value = c.Id
            }).OrderBy(a => a.DisplayText).ToList();
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }


        public Dictionary<string, object> FillDrop_Insects(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<A_SystemCode>().GetData().Where(a => a.SystemCodeTypeId == 18 && a.Value == 2).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.ValueName : c.ValueNameEN),
                Value = c.Id
            }).OrderBy(a => a.DisplayText).ToList();
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        // Mahmoud Saber ...
        public Dictionary<string, object> FillDrop_ByActivity(int activityId, List<string> Device_Info)
        {
            PlantQuarantineEntities entity = new PlantQuarantineEntities();
            string lang = Device_Info[2];

            var data = (from company in entity.Company_National
                        join activity in entity.CompanyActivities on company.ID equals activity.Company_ID
                        where activity.MainActivityType == activityId && company.User_Deletion_Id == null && company.IsActive == true
                        select new CustomOptionLongId
                        {
                            //change display lang
                            DisplayText = (lang == "1" ? company.Name_Ar : company.Name_En),
                            Value = company.ID
                        }).ToList();

            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}