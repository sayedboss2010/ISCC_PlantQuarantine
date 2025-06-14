using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station;
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
    public class StationBLL : IGenericBLL<StationDTO>
    {
        private UnitOfWork uow;

        public StationBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                PlantQuar.DAL.Station entity = uow.Repository<PlantQuar.DAL.Station>().Findobject(Id);
                var _DTO = Mapper.Map<PlantQuar.DAL.Station, StationDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, _DTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<PlantQuar.DAL.Station>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<PlantQuar.DAL.Station>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<PlantQuar.DAL.Station, StationDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                var data = new List<PlantQuar.DAL.Station>();
                Int64 data_Count = 0;
                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<PlantQuar.DAL.Station>().GetData().Where(a => a.User_Deletion_Id == null &&
                                             a.En_Name.StartsWith(enName)).ToList();
                    data_Count = data.Count();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<PlantQuar.DAL.Station>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Ar_Name.StartsWith(arName)).ToList();
                    data_Count = data.Count();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<PlantQuar.DAL.Station>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<PlantQuar.DAL.Station>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))).ToList();
                    data_Count = data.Count();
                }
                string lang = Device_Info[2];
                var dataDTO = data.Skip(index).Take(pageSize).Select(Mapper.Map<PlantQuar.DAL.Station, StationDTO>).ToList();

                dic.Add("Count_Data", data_Count);
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
                dic.Add("Station_Data", dataDTO);

                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(StationDTO entity)
        {
            return uow.Repository<PlantQuar.DAL.Station>().GetAny(p => p.User_Deletion_Id == null &&
                                        (p.Ar_Name == entity.Ar_Name || p.En_Name == entity.En_Name)
                                        && (p.Address_Ar == entity.Address_Ar || p.Address_En == entity.Address_En)
                                        && p.CommertialRecord == entity.CommertialRecord
                                        && p.Industrial_License_Num == entity.Industrial_License_Num
                                        && p.TaxesRecord == entity.TaxesRecord
                                        && (entity.ID == 0 ? true : p.ID != entity.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(StationDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<PlantQuar.DAL.Station>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Station_seq");
                    var data = uow.Repository<PlantQuar.DAL.Station>().InsertReturn(CModel);
                    uow.SaveChanges();

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
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
        public Dictionary<string, object> Update(StationDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    PlantQuar.DAL.Station CModel = uow.Repository<PlantQuar.DAL.Station>().Findobject(entity.ID);

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        entity.User_Updation_Date = CModel.User_Updation_Date;
                        entity.User_Updation_Id = CModel.User_Updation_Id;
                    }
                    if (entity.FileUpload == null)
                    {
                        //get old one
                        entity.FileUpload = CModel.FileUpload;
                    }
                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<PlantQuar.DAL.Station>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<PlantQuar.DAL.Station, StationDTO>(Co);
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
                var Cmodel = uow.Repository<PlantQuar.DAL.Station>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<PlantQuar.DAL.Station>().Update(Cmodel);
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

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<PlantQuar.DAL.Station>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.IsActive == true)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = (int)c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<PlantQuar.DAL.Station>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = (int)c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> GetAccridatedStations(List<string> Device_Info)
        {
            try
            {
                var data = from s in uow.Repository<PlantQuar.DAL.Station>().GetData()
                           //sayed 27/6/2022
                          // join a in uow.Repository<Station_Accreditation>().GetData() on s.ID equals a.StationActivity.Station_ID
                           join a in uow.Repository<Station_Accreditation>().GetData() on s.ID equals a.Station_ID
                           where s.User_Deletion_Id == null && a.User_Deletion_Id == null && s.IsActive == true
                           && a.IsActive == true
                           select new CustomOptionLongId()
                           {
                               DisplayText = s.Ar_Name,
                               Value = s.ID
                           };
                var res = data.Distinct().ToList();
                res.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, res);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop_AddEdit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<TreatmentMainType>().GetData().Where(a => a.User_Deletion_Id == null)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

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
