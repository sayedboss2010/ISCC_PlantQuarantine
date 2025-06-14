using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Treatments;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry.Treatments
{

    public class TreatmentTypeBLL : IGenericBLL<TreatmentTypeDTO>
    {
        private UnitOfWork uow;

        public TreatmentTypeBLL()
        {
            uow = new UnitOfWork();
        }

        //Find TreatmentType by Id
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                TreatmentType entity = uow.Repository<TreatmentType>().Findobject(Id);
                var empDTO = Mapper.Map<TreatmentType, TreatmentTypeDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Count TreatmentType 
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<TreatmentType>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.TreatmentMainType.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        //Get List TreatmentType
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {

            try
            {
                var data = new List<TreatmentType>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<TreatmentType>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<TreatmentType>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<TreatmentType, TreatmentTypeDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
            //try
            //{
            //    var data = uow.Repository<TreatmentType>().GetData().Where(p => p.User_Deletion_Id == null
            //   // get undeleted parent
            //   && p.TreatmentMainType.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
            //    //var data = uow.Repository<TreatmentType>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
            //    var dataDTO = data.Select(Mapper.Map<TreatmentType, TreatmentTypeDTO>);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }

        //Get List TreatmentType by ArName or EnName
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
             {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<TreatmentType>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = data = uow.Repository<TreatmentType>().GetData().Where(a =>
                       a.En_Name.StartsWith(enName.Trim()) &&
                                            a.TreatmentMainType.User_Deletion_Id == null &&

                    a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    //data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();

                    data = data = uow.Repository<TreatmentType>().GetData().Where(a =>
                         a.Ar_Name.StartsWith(arName.Trim()) &&
                                              a.TreatmentMainType.User_Deletion_Id == null &&

                      a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<TreatmentType>().GetData().Where(a =>
                                         a.TreatmentMainType.User_Deletion_Id == null &&
a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    //data = data.Where(a => (a.Ar_Name.StartsWith(arName) || a.En_Name.StartsWith(enName))).ToList();
                    data = uow.Repository<TreatmentType>().GetData().Where(a =>
                    (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName)) &&
                     a.TreatmentMainType.User_Deletion_Id == null &&
                  a.User_Deletion_Id == null).ToList();
                }

                string lang = Device_Info[2];
                var dataDto = data.OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).Select(Mapper.Map<TreatmentType, TreatmentTypeDTO>);

                data_Count = dataDto.Count();
                switch (jtSorting)
                {
                    case "Ar_Name ASC":
                        data = data.OrderBy(t => t.Ar_Name).ToList();
                        break;
                    case "Ar_Name DESC":
                        data = data.OrderByDescending(t => t.Ar_Name).ToList();
                        break;
                    case "En_Name ASC":
                        data = data.OrderBy(t => t.En_Name).ToList();
                        break;
                    case "En_Name DESC":
                        data = data.OrderByDescending(t => t.En_Name).ToList();
                        break;
                }
                dic.Add("Count_Data", data_Count);
                dic.Add("TreatmentType_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Any TreatmentType
        public bool GetAny(TreatmentTypeDTO entity)
        {
            var obj = entity;
            return uow.Repository<TreatmentType>().GetAny(p => (p.User_Deletion_Id == null &&
                                            //9-11-2019
                                            p.MainType_ID==entity.MainType_ID &&
                                        (p.Ar_Name == obj.Ar_Name || p.En_Name == obj.En_Name)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        //******************************************//
        //Create TreatmentType
        public Dictionary<string, object> Insert(TreatmentTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name= entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("TreatmentType_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;
                    var CModel = Mapper.Map<TreatmentType>(entity);
                    uow.Repository<TreatmentType>().InsertRecord(CModel);
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

        //Update TreatmentType
        public Dictionary<string, object> Update(TreatmentTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as TreatmentTypeDTO;
                    TreatmentType CModel = uow.Repository<TreatmentType>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<TreatmentType>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<TreatmentType, TreatmentTypeDTO>(Co);
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

        //Delete TreatmentMaterial
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<TreatmentType>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<TreatmentType>().Update(Cmodel);
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
        //Get TreatmentType List DDL 
        public Dictionary<string, object> FillDrop_List(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<TreatmentType>().GetData().Where(a => a.User_Deletion_Id == null)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        //Get TreatmentType Create Edit DDL
        public Dictionary<string, object> FillDrop_AddEdit(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<TreatmentType>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //Get TreatmentType By TreatmentMain_Id
        public Dictionary<string, object> TreatmentTypeByTreatmentMain_Id(int TreatmentMain_Id)
        {
            var data = uow.Repository<TreatmentType>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true && a.MainType_ID == TreatmentMain_Id)
                .Select(c => new CustomOption { DisplayText = c.Ar_Name, Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            CustomOption empty = new CustomOption();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}
