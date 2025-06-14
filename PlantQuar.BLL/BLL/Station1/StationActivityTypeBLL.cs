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

namespace PlantQuar.BLL.BLL.Station
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
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
                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<StationActivityType>().GetData(pageSize, index, A => 1 == 1).Where(x => x.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<StationActivityType>().GetData().Where(x => x.User_Deletion_Id == null).ToList();
                }
                data.Insert(0, new StationActivityType() { Ar_Name = "----------", ID = 0 });

                var dataDTO = data.Select(Mapper.Map<StationActivityType, StationActivityTypeDTO>);





                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                
                Int64 data_Count = 0;
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                //var dd = (from sta in entities.StationActivityTypes
                //          join sm in entities.TreatmentMethods on  sta.TreatmentMethods_ID equals sm.ID 
                //          join tt in entities.TreatmentTypes on sm.TreatmentType_ID equals tt.ID
                //          join tm in entities.TreatmentMainTypes on tt.MainType_ID equals tm.ID
                //          select new StationActivityTypeDTO
                //          {
                //              ID = sta.ID,
                //             Ar_Name= sta.Ar_Name,
                //             En_Name= sta.En_Name,
                //             Descreption_Ar = sta.Descreption_Ar,
                //             Descreption_En = sta.Descreption_En,
                //             IsActive = sta.IsActive,
                //             User_Creation_Date = sta.User_Creation_Date,
                //             User_Creation_Id = sta.User_Creation_Id,
                //             User_Deletion_Date= sta.User_Deletion_Date,
                //             User_Deletion_Id= sta.User_Deletion_Id,
                //             TreatmentMethods_ID = sta.TreatmentMethods_ID,
                //             Treatment_Id = tt.ID,

                //          }).ToList();
                var data = new List<StationActivityType>();
                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null &&
                                             a.En_Name.StartsWith(enName)).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Ar_Name.StartsWith(arName)).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))).ToList();
                }
               
                string lang = Device_Info[2];
                var dataDTO = data.OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).Select(Mapper.Map<StationActivityType, StationActivityTypeDTO>).ToList();
                foreach (var ddd in dataDTO)
                {
                    var treatmentMethod = uow.Repository<TreatmentMethod>().GetData().FirstOrDefault(t => t.ID == ddd.TreatmentMethods_ID);
                    if (treatmentMethod != null)
                    {
                        var treatmentType = uow.Repository<TreatmentType>().GetData().FirstOrDefault(t => t.ID == treatmentMethod.TreatmentType_ID);
                        if (treatmentType != null)
                        {
                            ddd.Treatment_Id = treatmentType.ID; 
                            var treatmentMain = uow.Repository<TreatmentMainType>().GetData().FirstOrDefault(t => t.ID == treatmentType.MainType_ID);
                            if(treatmentMain != null)
                            {
                                ddd.TreatmentMain_Id = treatmentMain.ID;
                            }

                        }
                    }
                }
                data_Count = data.Count();

                
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
                dic.Add("StationActivityType_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(StationActivityTypeDTO obj)
        {
            return uow.Repository<StationActivityType>().GetAny(a => a.Ar_Name == obj.Ar_Name && a.En_Name == obj.En_Name && a.User_Deletion_Id == null && a.ID != obj.ID);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool IsTreatment(int ActID)
        {
            byte? ActTypeID = uow.Repository<StationActivity>().GetData().ToList().Where(x => x.ID == ActID).Select(x => x.StationActivityType_ID).SingleOrDefault();
            return uow.Repository<StationActivityType>().GetData().Where(x => x.ID == ActTypeID /*&& x.IsTreatment == true*/).Any();

        }
        public Dictionary<string, object> FillDrop_List(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<StationActivityType>().GetData().Where(lab => lab.User_Deletion_Id == null)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<StationActivityType>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data.OrderBy(a => a.DisplayText).ToList());
        }
      
    }
}
