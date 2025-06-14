using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Company;
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

namespace PlantQuar.BLL.BLL.Stations
{
    public class StationActivityTypeBLL : IGenericBLL<StationActivityTypeDTO>
    {
        private UnitOfWork uow;

        public StationActivityTypeBLL()
        {

            uow = new UnitOfWork();
        }
               public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                StationActivityType entity = uow.Repository<StationActivityType>().Findobject(Id);
                var _DTO = Mapper.Map<StationActivityType, StationActivityTypeDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, _DTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<StationActivityType>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<StationActivityType>();
                if(pageSize == -1 && index == -1)
                {
                    data = uow.Repository<StationActivityType>().GetData(pageSize, index, A => 1 == 1).Where(x => x.User_Deletion_Id == null).ToList();
                }
                else
                {
                     data = uow.Repository<StationActivityType>().GetData().Where(x => x.User_Deletion_Id == null).ToList();
                }
                var dataDTO = data.Select(Mapper.Map<StationActivityType, StationActivityTypeDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }



        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var dd = (from gg in entities.StationActivityTypes
                              //join sec in entities.SecondaryClassifications on gg.SecClass_ID equals sec.ID
                              //join main in entities.MainCalssifications on sec.MainClass_ID equals main.ID
                              //join it in entities.Item_Type on main.Item_Type_ID equals it.Id
                              where gg.User_Deletion_Id == null

                          select new StationActivityTypeDTO
                          {
                              ID = gg.ID,
                              Ar_Name = gg.Ar_Name,
                              En_Name = gg.En_Name,
                              Descreption_Ar = gg.Descreption_Ar,
                              Descreption_En = gg.Descreption_En,
                              IsTreatment = gg.IsTreatment,
                              IsActive = gg.IsActive,

                              TreatmentMain_Id = gg.TreatmentMethod.TreatmentType.MainType_ID,
                              Treatment_Id = gg.TreatmentMethod.TreatmentType_ID,
                              TreatmentMethods_ID = gg.TreatmentMethods_ID,

                          }).ToList();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<StationActivityTypeDTO>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = dd.Where(a => a.En_Name.StartsWith(enName.Trim())
                // get undeleted parent
                // && a.SecondaryClassification.User_Deletion_Id == null
                //&& a.SecondaryClassification.MainCalssification.User_Deletion_Id == null
                )
                .ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = dd.Where(a =>  a.Ar_Name.StartsWith(arName.Trim())
                // get undeleted parent
                // && a.SecondaryClassification.User_Deletion_Id == null
                // && a.SecondaryClassification.MainCalssification.User_Deletion_Id == null
                ).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = dd.Where(a => a.Ar_Name.StartsWith(arName.Trim()) && a.En_Name.StartsWith(enName.Trim())
                // get undeleted parent
                // && a.SecondaryClassification.User_Deletion_Id == null
                //  && a.SecondaryClassification.MainCalssification.User_Deletion_Id == null
                ).ToList();

                }
                else
                {
                    data = dd.ToList();

                }

                switch (jtSorting)
                {
                    case "Name_Ar ASC":
                        data = data.OrderBy(t => t.Ar_Name).ToList();
                        break;
                    case "Name_Ar DESC":
                        data = data.OrderByDescending(t => t.Ar_Name).ToList();
                        break;
                    case "Name_En ASC":
                        data = data.OrderBy(t => t.En_Name).ToList();
                        break;
                    case "Name_En DESC":
                        data = data.OrderByDescending(t => t.Ar_Name).ToList();
                        break;

                }
                string lang = Device_Info[2];
                var dataDTO = data.Skip(index).Take(pageSize).ToList();

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Group_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = new Dictionary<string, object>();

        //        var data = new List<StationActivityType>();
        //        Int64 data_Count = 0;
        //        if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
        //        {
        //            data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null &&
        //                                     a.En_Name.StartsWith(enName)).ToList();
        //        }
        //        else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
        //        {
        //            data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null &&
        //                                    a.Ar_Name.StartsWith(arName)).ToList();
        //        }
        //        else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
        //        {
        //            data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
        //        }
        //        else
        //        {
        //            data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null &&
        //                     (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))).ToList();
        //        }

        //        data = data.im
        //        string lang = Device_Info[2];
        //        var dataDTO = data.OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).Select(Mapper.Map<StationActivityType, StationActivityTypeDTO>);
        //        data_Count = data.Count();

        //        dic.Add("Count_Data", data_Count);
        //        switch (jtSorting)
        //        {
        //            case "Ar_Name ASC":
        //                data = data.OrderBy(t => t.Ar_Name).ToList();
        //                break;
        //            case "Ar_Name DESC":
        //                data = data.OrderByDescending(t => t.Ar_Name).ToList();
        //                break;
        //            case "En_Name ASC":
        //                data = data.OrderBy(t => t.En_Name).ToList();
        //                break;
        //            case "En_Name DESC":
        //                data = data.OrderByDescending(t => t.En_Name).ToList();
        //                break;
        //        }
        //        dic.Add("StationActivityType_Data", dataDTO);

        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        public bool GetAny(StationActivityTypeDTO obj)
        {
            return uow.Repository<StationActivityType>().GetAny(a => a.Ar_Name == obj.Ar_Name && a.En_Name==obj.En_Name &&a.User_Deletion_Id==null &&a.ID != obj.ID);
        }
        //******************************************//
        public Dictionary<string, object> Insert(StationActivityTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("StationActivityType_seq");
                    var CModel = Mapper.Map<StationActivityType>(entity);
                    CModel.ID = id;
                    uow.Repository<StationActivityType>().InsertRecord(CModel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Update(StationActivityTypeDTO obj, List<string> Device_Info)
        {
            try
            {
                obj.Ar_Name = obj.Ar_Name.Trim();
                obj.En_Name = obj.En_Name.Trim();
                if (!GetAny(obj))
                {
                    StationActivityType CModel = uow.Repository<StationActivityType>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }
                    if(obj.IsTreatment == false)
                    {
                        obj.TreatmentMethods_ID = null;
                    }
                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<StationActivityType>().Update(Co);
                    uow.SaveChanges();

                    var _DTO = Mapper.Map<StationActivityType, StationActivityTypeDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, _DTO);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<StationActivityType>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<StationActivityType>().Update(Cmodel);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //public bool IsTreatment(int ActID)
        //{
        //        byte? ActTypeID = uow.Repository<StationActivity>().GetData().ToList().Where(x => x.ID == ActID).Select(x => x.StationActivityType_ID).SingleOrDefault();
        //        return uow.Repository<StationActivityType>().GetData().Where(x => x.ID == ActTypeID ).Any();
           
        //}
        public Dictionary<string, object> FillDrop_List( List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<StationActivityType>().GetData().Where(lab => lab.User_Deletion_Id == null )
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> FillDrop_Edit( List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> Find_ID(int Id, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var Data = (from gg in entities.StationActivityTypes
                            where gg.ID == Id
                            select new StationActivityTypeDTO
                            {
                                ID = gg.ID,
                                TreatmentMethods_Name = gg.TreatmentMethod.TreatmentType.TreatmentMainType.Ar_Name,
                                TreatmentMain_Name = gg.TreatmentMethod.TreatmentType.Ar_Name,
                                Treatment_Name = gg.TreatmentMethod.Ar_Name,
                            }).ToList();

                //var _DTO = Mapper.Map<StationActivityType, StationActivityTypeDTO>(Data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, Data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
