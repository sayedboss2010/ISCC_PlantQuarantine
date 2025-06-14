using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Company
{
    public class CompanyActivityBLL : IGenericBLL<CompanyActivityDTO>
    {
        private UnitOfWork uow;

        public CompanyActivityBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                CompanyActivity entity = uow.Repository<CompanyActivity>().Findobject(Id);
                var empDTO = Mapper.Map<CompanyActivity, CompanyActivityDTO>(entity);
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
            var count = uow.Repository<CompanyActivity>().GetData().Where(p => p.User_Deletion_Id == null
              // get undeleted parent
              && p.CompanyActivityType.User_Deletion_Id == null
                ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<CompanyActivity>().GetData().Where(a => a.User_Deletion_Id == null
              // get undeleted parent
              && a.CompanyActivityType.User_Deletion_Id == null).OrderBy(A => A.Enrollment_Name).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<CompanyActivity, CompanyActivityDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool GetAny(CompanyActivityDTO entity)
        {
            var obj = entity as CompanyActivityDTO;
            return uow.Repository<CompanyActivity>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.MainActivityType == obj.MainActivityType && p.Company_ID == obj.Company_ID)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        public Dictionary<string, object> Insert(CompanyActivityDTO entity, List<string> Device_Info)
        {
            try
            {
                //entity.Enrollment_Name = entity.Enrollment_Name.Trim();

                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<CompanyActivity>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("CompanyActivity_seq");

                    var model = uow.Repository<CompanyActivity>().InsertReturn(CModel);
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

        public Dictionary<string, object> Update(CompanyActivityDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as CompanyActivityDTO;
                    CompanyActivity CModel = uow.Repository<CompanyActivity>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<CompanyActivity>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<CompanyActivity, CompanyActivityDTO>(Co);
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
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<CompanyActivity>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<CompanyActivity>().Update(Cmodel);
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

        public Dictionary<string, object> GetCompanyActivityByCompany_ID(int Company_ID, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                Int64 data_Count = 0;
                var data = uow.Repository<CompanyActivity>().GetData().Where(p => p.User_Deletion_Id == null && p.Company_ID == Company_ID
             && p.CompanyActivityType.User_Deletion_Id == null
             ).Select(x => new CompanyActivityDTO
             {
                 Company_ID = x.Company_ID,
                 ID = x.ID,
                 Enrollment_End = x.Enrollment_End,
                 Enrollment_Start = x.Enrollment_Start,
                 IsActive = x.IsActive,
                 CompActivityType_ID = x.CompActivityType_ID,
                 User_Creation_Date = x.User_Creation_Date,
                 User_Creation_Id = x.User_Creation_Id,
                 User_Deletion_Date = x.User_Deletion_Date,
                 User_Deletion_Id = x.User_Deletion_Id,
                 User_Updation_Date = x.User_Updation_Date,
                 User_Updation_Id = x.User_Updation_Id,
                 Enrollment_Name = x.Enrollment_Name,
                 Enrollment_Number = x.Enrollment_Number,
                 Enrollment_type_ID=x.Enrollment_type_ID,
                 MainActivityType = x.MainActivityType
             });
                data_Count = data.Count();
                string lang = Device_Info[2];
                var _data = data.OrderBy(A => (lang == "1" ? A.Enrollment_Name : A.Enrollment_Name)).Skip(index).Take(pageSize).ToList();
                dic.Add("Count_Data", data_Count);
                dic.Add("CompanyActivity_Data", _data);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
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
            var data = uow.Repository<CompanyActivity>().GetData().Where(lab => lab.User_Deletion_Id == null)
                    .Select(c => new CustomOptionLongId
                    {
                        //change display lang
                        DisplayText = (lang == "1" ? c.Enrollment_Name : c.Enrollment_Name),
                        Value = c.ID
                    }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<CompanyActivity>().GetData().Where(a => a.User_Deletion_Id == null)
                   .Select(c => new CustomOptionLongId
                   {
                       //change display lang
                       DisplayText = (lang == "1" ? c.Enrollment_Name : c.Enrollment_Name),
                       Value = c.ID
                   }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}
