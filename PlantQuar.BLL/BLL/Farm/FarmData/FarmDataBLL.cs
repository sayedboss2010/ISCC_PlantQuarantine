using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;
using PlantQuar.DTO.DTO.DataEntry.LookUp;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Farm.FarmData
{
    public class FarmDataBLL
    {
        private UnitOfWork uow;

        public FarmDataBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                FarmsData entity = uow.Repository<FarmsData>().Findobject(Id);
                var empDTO = Mapper.Map<FarmsData, FarmsDataDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<FarmsData>().GetData().Where(p => p.User_Deletion_Id == null
             // get undeleted parent

             ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<FarmsData>().GetData(pageSize, index, A => 1 == 1).Where(a => a.User_Deletion_Id == null
            // && a.Farm_Company.Where(c=>c.Farm_ID==a.ID).Select(c=>c.User_Deletion_Id)==null&&
            // a.FarmPlants.Where(c => c.Farm_ID == a.ID).Select(c => c.User_Deletion_Id) == null
            ).Select(x => new FarmsDataDTO()
            {
                Address_Ar = x.Address_Ar,
                Address_En = x.Address_En,
                FarmCode_14 = x.FarmCode_14,
                ID = x.ID,
                GPSRead = x.GPSRead,
                IsApproved = x.IsApproved,
                Status = x.Status,
                ThePivot = x.ThePivot,
                User_Creation_Date = x.User_Creation_Date,
                User_Creation_Id = x.User_Creation_Id,
                User_Deletion_Date = x.User_Deletion_Date,
                User_Deletion_Id = x.User_Deletion_Id,
                User_Updation_Date = x.User_Updation_Date,
                User_Updation_Id = x.User_Updation_Id,
                Village_ID = x.Village_ID,
                IsActive = (bool)x.IsActive,
                Name_Ar = x.Name_Ar,
                Name_En = x.Name_En,
                Center_Id = (short)x.Center_Id,
                Govern_ID = (short)x.Govern_ID
            }).ToList();
                //  var dataDTO = data.Select(Mapper.Map<FarmsData, FarmsDataDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string FarmCode_14, int pageSize, int index, List<string> Device_Info)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            try
            {
                var data = new List<FarmsDataDTO>();
                Int64 data_Count = 0;

                if (!string.IsNullOrEmpty(FarmCode_14))
                {
                    data = uow.Repository<FarmsData>().GetData().Where(a => a.User_Deletion_Id == null
                    && a.FarmCode_14.StartsWith(FarmCode_14)).Select(x => new FarmsDataDTO()
                    {
                        Address_Ar = x.Address_Ar,
                        Address_En = x.Address_En,
                        //Company_ID = x.Farm_Company.Where(c => c.User_Deletion_Id == null).Select(c => c.Company_ID).FirstOrDefault(),
                        FarmCode_14 = x.FarmCode_14,
                        FileUpload = x.FileUpload,
                        FileUpload_File = x.FileUpload,
                        ID = x.ID,
                        GPSRead = x.GPSRead,
                        IsApproved = x.IsApproved,
                        Status = x.Status,
                        ThePivot = x.ThePivot,
                        User_Creation_Date = x.User_Creation_Date,
                        User_Creation_Id = x.User_Creation_Id,
                        User_Deletion_Date = x.User_Deletion_Date,
                        User_Deletion_Id = x.User_Deletion_Id,
                        User_Updation_Date = x.User_Updation_Date,
                        User_Updation_Id = x.User_Updation_Id,
                        Village_ID = x.Village_ID,
                        IsActive = (bool)x.IsActive,
                        Name_Ar = x.Name_Ar,
                        Name_En = x.Name_En,
                        Center_Id = (short)x.Center_Id,
                        Govern_ID = (short)x.Govern_ID,
                        //requestAccepted=x.IsActive
                    }).ToList();
                }
                else
                {
                    data = uow.Repository<FarmsData>().GetData().Where(a => a.User_Deletion_Id == null
                   ).Select(x => new FarmsDataDTO()
                   {
                       Address_Ar = x.Address_Ar,
                       Address_En = x.Address_En,
                       //Company_ID = x.Farm_Company.Where(c => c.User_Deletion_Id == null).Select(c => c.Company_ID).FirstOrDefault(),
                       FarmCode_14 = x.FarmCode_14,
                       FileUpload = x.FileUpload,
                       FileUpload_File = x.FileUpload,
                       ID = x.ID,
                       GPSRead = x.GPSRead,
                       IsApproved = x.IsApproved,
                       Status = x.Status,
                       ThePivot = x.ThePivot,
                       User_Creation_Date = x.User_Creation_Date,
                       User_Creation_Id = x.User_Creation_Id,
                       User_Deletion_Date = x.User_Deletion_Date,
                       User_Deletion_Id = x.User_Deletion_Id,
                       User_Updation_Date = x.User_Updation_Date,
                       User_Updation_Id = x.User_Updation_Id,
                       IsActive = (bool)x.IsActive,
                       Village_ID = x.Village_ID,
                       Name_Ar = x.Name_Ar,
                       Name_En = x.Name_En,
                       Center_Id = (short)x.Center_Id,
                       Govern_ID = (short)x.Govern_ID
                   }).ToList();
                }


                var dataDto = data.Skip(index).Take(pageSize).ToList();

                data_Count = data.Count;
                dic.Add("Count_Data", data_Count);
                dic.Add("FarmsData", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dataDto);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(FarmsDataDTO entity)
        {
            var obj = entity as FarmsDataDTO;

            if (obj.ID > 0)
            {
                return uow.Repository<FarmsData>().GetAny(p => (p.User_Deletion_Id == null &&
                                       (p.FarmCode_14 == obj.FarmCode_14) && p.ID != obj.ID));
            }
            else
            {
                return uow.Repository<FarmsData>().GetAny(p => (p.User_Deletion_Id == null &&
                                       (p.FarmCode_14 == obj.FarmCode_14)));
            }
        }

        //CheckFarmCode
        public Dictionary<string, object> CheckFarmCode(string FarmCode_14, long FarmId, List<string> Device_Info)
        {
            try
            {
                if (FarmId > 0)
                {
                    var d = uow.Repository<FarmsData>().GetAny(p => (p.User_Deletion_Id == null &&
                                       (p.FarmCode_14 == FarmCode_14) && p.ID != FarmId));

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, d);
                }
                else
                {
                    var d = uow.Repository<FarmsData>().GetAny(p => (p.User_Deletion_Id == null &&
                                      (p.FarmCode_14 == FarmCode_14)));

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, d);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Insert(FarmsDataDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<FarmsData>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("FarmsData_seq");
                    CModel.IS_OnlineOffline = false;
                    CModel = uow.Repository<FarmsData>().InsertReturn(CModel);
                    uow.SaveChanges();

                    entity.ID = CModel.ID;
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

        public Dictionary<string, object> Update(FarmsDataDTO entity, List<string> Device_Info)
        {
            try
            {

                if (!GetAny(entity))
                {
                    var obj = entity as FarmsDataDTO;
                    FarmsData CModel = uow.Repository<FarmsData>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    if (!(obj.Item_ID > 0))
                    {
                        obj.Item_ID = CModel.Item_ID;
                    }
                    obj.IS_OnlineOffline = true;
                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<FarmsData>().Update(Co);
                    uow.SaveChanges();

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, obj);
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
        //UpdatePlant
        public Dictionary<string, object> UpdatePlant(FarmsDataDTO entity, List<string> Device_Info)
        {
            try
            {
                var obj = entity as FarmsDataDTO;
                FarmsData CModel = uow.Repository<FarmsData>().Findobject(obj.ID);

                CModel.Item_ID = obj.Item_ID;
                CModel.User_Updation_Date = obj.User_Updation_Date;
                CModel.User_Updation_Id = obj.User_Updation_Id;

                uow.Repository<FarmsData>().Update(CModel);
                uow.SaveChanges();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, CModel);
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
                var Cmodel = uow.Repository<FarmsData>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    //delete farm company
                    var company = uow.Repository<Farm_Company>().GetData()
                        .Where(c => c.Farm_ID == obj.id && c.User_Deletion_Id == null).SingleOrDefault();
                    if (company != null)
                    {
                        company.User_Deletion_Date = obj._DateNow;
                        company.User_Deletion_Id = obj.Userid;
                        uow.Repository<Farm_Company>().Update(company);
                        uow.SaveChanges();
                    }

                    //delete farm plant
                    var plant = uow.Repository<Farm_ItemCategories>().GetData()
                        .Where(c => c.Farm_ID == obj.id && c.User_Deletion_Id == null).ToList();
                    if (plant != null)
                    {
                        if (plant.Count > 0)
                        {
                            foreach (var p in plant)
                            {
                                p.User_Deletion_Date = obj._DateNow;
                                p.User_Deletion_Id = obj.Userid;
                                uow.Repository<Farm_ItemCategories>().Update(p);
                                uow.SaveChanges();
                            }
                        }
                    }

                    //delete farm country
                    var country = uow.Repository<Farm_Country>().GetData()
                        .Where(c => c.Farm_Request.FarmsData_ID == obj.id && c.User_Deletion_Id == null).ToList();
                    if (country != null)
                    {
                        if (country.Count > 0)
                        {
                            foreach (var c in country)
                            {
                                c.User_Deletion_Date = obj._DateNow;
                                c.User_Deletion_Id = obj.Userid;
                                uow.Repository<Farm_Country>().Update(c);
                                uow.SaveChanges();
                            }
                        }
                    }

                    //farm request
                    var request = uow.Repository<Farm_Request>().GetData()
                        .Where(c => c.FarmsData_ID == obj.id && c.User_Deletion_Id == null).ToList();
                    if (request != null)
                    {
                        if (request.Count > 0)
                        {
                            foreach (var r in request)
                            {
                                r.User_Deletion_Date = obj._DateNow;
                                r.User_Deletion_Id = obj.Userid;
                                uow.Repository<Farm_Request>().Update(r);
                                uow.SaveChanges();
                            }
                        }
                    }

                    //farm
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<FarmsData>().Update(Cmodel);
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

        //**********************//
        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<FarmsData>().GetData()
                .Select(c => new CustomOptionLongId
                {
                    //DisplayText = c.Address_Ar,
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<FarmsData>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            {
                //DisplayText = c.Address_Ar,
                //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> GetFarmDataByVillageId(int VillageId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<FarmsData>().GetData().Where(a => a.User_Deletion_Id == null && a.Village_ID == VillageId
                && a.Village.IsActive == true && a.Village.User_Deletion_Id == null).Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert,
                    data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetlistItemParts(int ItemID, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                ItemCategory entity = uow.Repository<ItemCategory>().Findobject(ItemID);
                var itemCatDTO = Mapper.Map<ItemCategory, ItemCategoryDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, itemCatDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //***********************************************//
        //*************************SARA*****************//
        public Dictionary<string, object> GetAll(List<string> Device_Info)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            try
            {
                string lang = Device_Info[2];

                var data = new List<FarmsDataDTO>();
                Int64 data_Count = 0;

                data = uow.Repository<FarmsData>().GetData().Where(a => a.User_Deletion_Id == null && a.IS_OnlineOffline != true).Select(x => new FarmsDataDTO()
                {
                    ID = x.ID,
                    Name_Ar = x.Name_Ar,
                    Name_En = x.Name_En,
                    FarmCode_14 = x.FarmCode_14,
                    VillageName = (lang == "1" ? x.Village.Ar_Name : x.Village.En_Name),
                    CenterName = (lang == "1" ? x.Center.Ar_Name : x.Center.En_Name),
                    GoveName = (lang == "1" ? x.Governate.Ar_Name : x.Governate.En_Name),
                    Item_ID = x.Item_ID,
                    PlantName = (lang == "1" ? x.Item.Name_Ar : x.Item.Name_En),

                }).ToList();


                data_Count = data.Count;
                dic.Add("Count_Data", data_Count);
                dic.Add("FarmsData", data.OrderBy(f => f.ID));

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(string FarmCode_14, List<string> Device_Info)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string lang = Device_Info[2];

            try
            {
                Int64 data_Count = 0;

                var data = uow.Repository<FarmsData>().GetData().Where(a => a.User_Deletion_Id == null
                && a.FarmCode_14.StartsWith(FarmCode_14)).Select(x => new FarmsDataDTO()
                {
                    ID = x.ID,
                    Name_Ar = x.Name_Ar,
                    Name_En = x.Name_En,
                    FarmCode_14 = x.FarmCode_14,
                    VillageName = (lang == "1" ? x.Village.Ar_Name : x.Village.En_Name),
                    CenterName = (lang == "1" ? x.Center.Ar_Name : x.Center.En_Name),
                    GoveName = (lang == "1" ? x.Governate.Ar_Name : x.Governate.En_Name),
                    Item_ID = x.Item_ID,
                    PlantName = (lang == "1" ? x.Item.Name_Ar : x.Item.Name_En),

                }).ToList();

                data_Count = data.Count;
                dic.Add("Count_Data", data_Count);
                dic.Add("FarmsData", data.OrderBy(f => f.ID));

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetById(long id, List<string> Device_Info)
        {
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            try
            {
                string lang = Device_Info[2];
                var farmdata = uow.Repository<FarmsData>().GetData().Where(a => a.ID == id).Select(x => new FarmsDataDTO()
                {
                    Address_Ar = x.Address_Ar,
                    Address_En = x.Address_En,
                    FarmCode_14 = x.FarmCode_14,
                    FileUpload = x.FileUpload,
                    FileUpload_File = x.FileUpload,
                    ID = x.ID,
                    GPSRead = x.GPSRead,
                    IsApproved = x.IsApproved,
                    Status = x.Status,
                    ThePivot = x.ThePivot,
                    User_Creation_Date = x.User_Creation_Date,
                    User_Creation_Id = x.User_Creation_Id,
                    User_Deletion_Date = x.User_Deletion_Date,
                    User_Deletion_Id = x.User_Deletion_Id,
                    User_Updation_Date = x.User_Updation_Date,
                    User_Updation_Id = x.User_Updation_Id,
                    Village_ID = x.Village_ID,
                    Name_Ar = x.Name_Ar,
                    Name_En = x.Name_En,
                    Center_Id = (short)x.Center_Id,
                    Govern_ID = (short)x.Govern_ID,
                    VillageName = (lang == "1" ? x.Village.Ar_Name : x.Village.En_Name),
                    CenterName = (lang == "1" ? x.Center.Ar_Name : x.Center.En_Name),
                    GoveName = (lang == "1" ? x.Governate.Ar_Name : x.Governate.En_Name),
                    Item_ID = x.Item_ID,
                    IsActive = (bool)x.IsActive,
                    IS_OnlineOffline = x.IS_OnlineOffline,
                    PlantName = x.Item_ID > 0 ? (lang == "1" ? x.Item.Name_Ar : x.Item.Name_En) : "",
                    ScientificName = x.Item_ID > 0 ? x.Item.Scientific_Name : "",
                    group_Id = x.Item_ID > 0 ? (int)x.Item.Group_ID : 0,
                    secClass_Id = x.Item_ID > 0 ? (int)x.Item.Group.SecClass_ID : 0,
                    mainClass_Id = x.Item_ID > 0 ? (int)x.Item.Group.SecondaryClassification.MainClass_ID : 0,
                    isKnown = x.Item_ID > 0 ? (bool)x.Item.Is_known_item : false

                }).SingleOrDefault();

                var exporterType = uow.Repository<Farm_Company>().GetData().
                    Where(a => a.Farm_ID == id && a.User_Deletion_Id == null).FirstOrDefault().ExporterType_Id;

                exporterType = (exporterType == null ? 0 : exporterType);

                var exporterType_name =
                           (lang == "1" ? uow.Repository<A_SystemCode>().GetData().Where(s => s.Id == exporterType && s.SystemCodeTypeId == 3).FirstOrDefault().ValueName
                            : uow.Repository<A_SystemCode>().GetData().Where(s => s.Id == exporterType && s.SystemCodeTypeId == 3).FirstOrDefault().ValueNameEN);

                farmdata.ownerData = uow.Repository<Farm_Company>().GetData().
                   Where(a => a.Farm_ID == id && a.User_Deletion_Id == null).
                   Select(a => new Farm_CompanyDTO
                   {
                       ID = a.ID,
                       Company_ID = exporterType == 6 ? a.Company_ID : null,
                       Org_ID = exporterType == 7 ? a.Company_ID : null,
                       Person_ID = exporterType == 8 ? a.Company_ID : null,
                       ExporterType_Id = (int)exporterType,
                       exporterType_name = exporterType_name,
                       Start_Date = a.Start_Date,
                       End_Date = a.End_Date,
                       IsAcive = a.IsAcive
                   }).FirstOrDefault();

                if (exporterType == 6)//company
                {
                    farmdata.ownerData.companyData = uow.Repository<Company_National>().GetData().
                    Where(a => a.ID == farmdata.ownerData.Company_ID && a.User_Deletion_Id == null).
                    Select(a => new CompanyNationalDTO
                    {
                        ID = a.ID,
                        compName = (lang == "1" ? a.Name_Ar : a.Name_En),
                        compOwnerName = (lang == "1" ? a.Owner_Ar : a.Owner_En),
                        GoveName = (lang == "1" ? a.Center.Governate.Ar_Name : a.Center.Governate.En_Name),
                        CenterName = (lang == "1" ? a.Center.Ar_Name : a.Center.En_Name),
                        VillageName = (lang == "1" ? a.Village.Ar_Name : a.Village.En_Name),
                        address = (lang == "1" ? a.Address_Ar : a.Address_En),
                        IsActive = (bool)a.IsActive
                    }).FirstOrDefault();
                }
                else if (exporterType == 7)//public org
                {
                    farmdata.ownerData.orgData = uow.Repository<Public_Organization>().GetData().
                    Where(a => a.ID == farmdata.ownerData.Org_ID).
                    Select(a => new Public_OrganizationDTO
                    {
                        ID = a.ID,
                        orgName = (lang == "1" ? a.Name_Ar : a.Name_En),
                        orgAddress = (lang == "1" ? a.Address_Ar : a.Address_En),
                        orgTypeName = (lang == "1" ? a.PublicOrganization_Type.Name_Ar : a.PublicOrganization_Type.Name_En),
                        IsNational = a.IsNational,
                        IsActive = a.IsActive
                    }).FirstOrDefault();
                }
                else if (exporterType == 8)//person
                {
                    farmdata.ownerData.personData = uow.Repository<Person>().GetData().
                    Where(a => a.ID == farmdata.ownerData.Person_ID).
                    Select(a => new PersonDTO
                    {
                        ID = a.ID,
                        Name = a.Name,
                        personIdType_Name = (lang == "1" ? a.A_SystemCode.ValueName : a.A_SystemCode.ValueNameEN),
                        Address = a.Address,
                        IDNumber = a.IDNumber,
                        nationality = (lang == "1" ? a.Country.Ar_Name : a.Country.En_Name),
                        Phone = a.Phone,
                        Email = a.Email,
                        Job = a.Job

                    }).FirstOrDefault();
                }
                var plantList = uow.Repository<Farm_Request_ItemCategories>().GetData().Include(c => c.Farm_ItemCategories.ItemCategory)
                     .Where(a => a.User_Deletion_Id == null && a.Farm_ItemCategories.Farm_ID == id).
                     Select(a => new Farm_Request_ItemCategoriesDTO
                     {
                         ID = a.ID,
                         Farm_ItemCategories_ID = a.Farm_ItemCategories_ID,
                         categoryName = (lang == "1" ? a.Farm_ItemCategories.ItemCategory.Name_Ar : a.Farm_ItemCategories.ItemCategory.Name_En),
                         Area_Acres = a.Farm_ItemCategories.Area_Acres,
                         //Quantity_Ton = a.Quantity_Ton..ff,
                         StartDate = a.Farm_ItemCategories.StartDate,
                         EndDate = a.Farm_ItemCategories.EndDate,
                         IsActive = a.IsActive,
                         //ss
                         //ItemCategories_Group_ID = a.ItemCategory.ItemCategories_Group_ID

                     }).ToList();


                farmdata.plantList = plantList;
                //farmdata.plantList = uow.Repository<Farm_ItemCategories>().GetData().
                //     Where(a => a.Farm_ID == id && a.User_Deletion_Id == null).
                //     Select(a => new Farm_ItemCategoriesDTO
                //     {
                //         ItemCategories_ID = a.ItemCategories_ID,
                //         CategoryName = (lang == "1" ? a.ItemCategory.Name_Ar : a.ItemCategory.Name_En),
                //         Area_Acres = a.Area_Acres,
                //         Quantity_Ton = a.Quantity_Ton,
                //         StartDate = a.StartDate,
                //         EndDate = a.EndDate,
                //         IsActive = a.IsActive,

                //         ItemCategories_Group_ID = a.ItemCategory.ItemCategories_Group_ID

                //     }).ToList();

                farmdata.requestLst = uow.Repository<Farm_Request>().GetData().
                    Where(a => a.FarmsData_ID == id && a.User_Deletion_Id == null).
                    Select(a => new FarmRequestDTO
                    {
                        ID = a.ID,
                        IsAcceppted = a.IsAcceppted,
                        IsActive = a.IsActive,
                        Start_Date = a.Start_Date,
                        End_Date = a.End_Date,
                        IsPaid = a.IsPaid,
                        Start_Date_Request = a.Start_Date_Request,
                        End_Date_Request = a.End_Date_Request,
                        Fees = a.Fees,
                        Fees_Actual = a.Fees_Actual

                    }).ToList();

                foreach (var request in farmdata.requestLst)
                {
                    request.countryLst = uow.Repository<Farm_Country>().GetData().
                    Where(a => a.Farm_Request_ID == request.ID && a.User_Deletion_Id == null).
                    Select(a => new FarmCountryDTO
                    {
                        ID = a.ID,
                        IsAcceppted = a.IsAcceppted,
                        IsActive = a.IsActive,
                        Start_Date = a.Start_Date,
                        End_Date = a.End_Date,

                        Country_ID = a.Country_ID,
                        country_Name = (lang == "1" ? a.Country.Ar_Name : a.Country.En_Name)

                    }).ToList();
                }

                farmdata.attachmentList = uow.Repository<A_AttachmentData>().GetData().
                    Where(a => a.RowId == id && a.User_Deletion_Id == null && a.A_AttachmentTableNameId == 5).
                    Select(a => new A_AttachmentDataDTO
                    {
                        Id = a.Id,
                        Attachment_TypeName = a.Attachment_TypeName,
                        Attachment_Number = a.Attachment_Number,
                        AttachmentPath = a.AttachmentPath,
                        AttachmentPath_Binary = a.AttachmentPath_Binary,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate
                    }).ToList();
                var List_Farm_Constrain_AnalysisTypes = (from fc in entities.Farm_Constrain
                                                         join fd in entities.FarmsDatas on fc.Item_ID equals fd.Item_ID
                                                         join fr in entities.Farm_Request on fd.ID equals fr.FarmsData_ID
                                                         // join fc2 in entities.Farm_Country on fr.ID equals fc2.Farm_Request_ID //AND fc.Country_Id = fc2.Country_Id
                                                         join fc2 in entities.Farm_Country on new { a = fr.ID, b = fc.Country_Id } equals new { a = fc2.Farm_Request_ID, b = fc2.Country_ID }
                                                         join at1 in entities.AnalysisTypes on fc.AnalysisType_ID equals at1.ID
                                                         where fr.ID == id
                                                         select new Farm_Requst_ListDTO
                                                         {
                                                             farm_Id = fd.ID,
                                                             AnalysisType_ID = fc.AnalysisType_ID,
                                                             AnalysisType_Name = at1.Name_Ar,
                                                             ConstrainText_text = fc.Farm_Constrain_Text.ConstrainText_Ar,
                                                             ConstrainText_ID = fc.Farm_Constrain_Text.ID
                                                         }).Distinct().ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, farmdata);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //*******************************************
        //GetFarmRequest
        public Dictionary<string, object> GetFarmRequest(long id, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                var request = uow.Repository<Farm_Request>().GetData().
                    Where(a => a.FarmsData_ID == id && a.User_Deletion_Id == null && a.IS_OnlineOffline == false).
                    Select(a => new FarmRequestDTO
                    {
                        ID = a.ID
                    }).FirstOrDefault();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //Insert_FarmRequest
        public Dictionary<string, object> Insert_FarmRequest(FarmRequestDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny_FarmRequest(entity))
                {
                    var CModel = Mapper.Map<Farm_Request>(entity);
                    CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Farm_Request_seq");
                    CModel = uow.Repository<Farm_Request>().InsertReturn(CModel);
                    uow.SaveChanges();

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, CModel);
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

        private bool GetAny_FarmRequest(FarmRequestDTO entity)
        {
            var obj = entity;
            return uow.Repository<Farm_Request>().GetAny(p => p.User_Deletion_Id == null &&
                                        p.FarmsData_ID == obj.FarmsData_ID &&
                                       p.IS_OnlineOffline == false &&
                                        (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        //********************************************************//
        //GetFarmCountry
        public Dictionary<string, object> GetFarmCountry(long id, long RequestID, List<string> Device_Info)
        {
            try
            {
                //sss

                string lang = Device_Info[2];
                var countryList = uow.Repository<Farm_Country>().GetData()
                   .Where(a => a.Farm_Request_ID == RequestID && a.User_Deletion_Id == null).
                    Select(a => new FarmCountryDTO
                    {
                        ID = a.ID,
                        IsAcceppted = (bool)a.IsAcceppted,
                        IsActive = (bool)a.IsActive,
                        Start_Date = a.Start_Date,
                        End_Date = a.End_Date,

                        Country_ID = a.Country_ID,
                        country_Name = (lang == "1" ? a.Country.Ar_Name : a.Country.En_Name)

                    }).ToList();


                //var countryList = uow.Repository<Farm_Country>().GetData().
                //    Where(a => a.Farm_Request.FarmsData_ID == id && a.User_Deletion_Id == null).
                //    Select(a => new FarmCountryDTO
                //    {
                //        ID = a.ID,
                //        Farm_Request_ID = a.Farm_Request_ID,
                //        Country_ID = a.Country_ID,
                //        IsAcceppted = (bool)a.IsAcceppted,
                //        IsActive = (bool)a.IsActive,
                //        Start_Date = a.Start_Date,
                //        End_Date = a.End_Date

                //    }).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, countryList);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //GetFarmAttachments
        public Dictionary<string, object> GetFarmAttachments(long id, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                var attachmentList = uow.Repository<A_AttachmentData>().GetData().
                    Where(a => a.RowId == id && a.User_Deletion_Id == null && a.A_AttachmentTableNameId == 5).
                    Select(a => new A_AttachmentDataDTO
                    {
                        Id = a.Id,
                        Attachment_TypeName = a.Attachment_TypeName,
                        Attachment_Number = a.Attachment_Number,
                        AttachmentPath = a.AttachmentPath,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate
                    }).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, attachmentList);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //GetFarmPlantCat
        public Dictionary<string, object> GetFarmPlantCat(long id, long RequestID, List<string> Device_Info)
        {
            try
            {
                //ss

                string lang = Device_Info[2];
                var plantList = uow.Repository<Farm_Request_ItemCategories>().GetData().Include(c => c.Farm_ItemCategories.ItemCategory)
                    .Where(a => a.User_Deletion_Id == null && a.Farm_Request_ID == RequestID).
                    Select(a => new Farm_Request_ItemCategoriesDTO
                    {
                        ID = a.ID,
                        Farm_ItemCategories_ID = a.Farm_ItemCategories_ID,
                        categoryName = (lang == "1" ? a.Farm_ItemCategories.ItemCategory.Name_Ar : a.Farm_ItemCategories.ItemCategory.Name_En),
                        Area_Acres = a.Farm_ItemCategories.Area_Acres,
                        //Quantity_Ton = a.Quantity_Ton..ff,
                        StartDate = a.Farm_ItemCategories.StartDate,
                        EndDate = a.Farm_ItemCategories.EndDate,
                        IsActive = a.IsActive,
                        //ss
                        //ItemCategories_Group_ID = a.ItemCategory.ItemCategories_Group_ID

                    }).ToList();
                foreach (var item in plantList)
                {

                    var Commit = uow.Repository<Farm_Committee>().GetData()
                     .Where(a => a.Farm_Request_ID == RequestID).FirstOrDefault();
                    if (Commit != null)
                    {

                        var quntity = uow.Repository<Farm_Committee_Examination>().GetData()
                           .Where(a => a.FarmCommittee_ID == Commit.ID
                           && a.Farm_Request_ItemCategories_ID == item.ID).FirstOrDefault();
                        if (quntity != null)
                        {
                            item.Quantity_Ton = quntity.Quantity_Ton * item.Area_Acres;
                        }

                    }
                }

                //var plantList = uow.Repository<Farm_ItemCategories>().GetData().
                //    Where(a => a.Farm_ID == id && a.User_Deletion_Id == null).
                //    Select(a => new Farm_ItemCategoriesDTO
                //    {
                //        ID = a.ID,
                //        ItemCategories_ID = a.ItemCategories_ID,
                //        StartDate = a.StartDate,
                //        EndDate = a.EndDate,
                //        IsActive = a.IsActive,
                //        Area_Acres = a.Area_Acres,
                //        Quantity_Ton = a.Quantity_Ton,
                //        ItemCategories_Group_ID = a.ItemCategory.ItemCategories_Group_ID
                //    }).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, plantList);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //***********************************************//
        //GetCompanyList
        public Dictionary<string, object> GetCompanyList(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Company_National>().GetData().Where(c => c.IsActive == true && c.User_Deletion_Id == null)
                .Select(c => new CustomOptionLongId
                {
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).ToList();
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> GetCompanyById(long companyId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Company_National>().GetData().Where(c => c.ID == companyId)
                .Select(a => new CompanyNationalDTO
                {
                    ID = a.ID,
                    compName = (lang == "1" ? a.Name_Ar : a.Name_En),
                    compOwnerName = (lang == "1" ? a.Owner_Ar : a.Owner_En),
                    GoveName = (lang == "1" ? a.Center.Governate.Ar_Name : a.Center.Governate.En_Name),
                    CenterName = (lang == "1" ? a.Center.Ar_Name : a.Center.En_Name),
                    VillageName = (lang == "1" ? a.Village.Ar_Name : a.Village.En_Name),
                    address = (lang == "1" ? a.Address_Ar : a.Address_En),
                    IsActive = (bool)a.IsActive

                }).SingleOrDefault();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        //****************************************************//
        public Dictionary<string, object> GetUnionCountries(int unionId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Union_Country>().GetData()
                .Where(c => c.IsActive == true && c.User_Deletion_Id == null && c.Union_ID == unionId
                && c.Country.IsActive == true && c.Country.User_Deletion_Id == null)
                .Select(c => new CustomOption
                {
                    DisplayText = (lang == "1" ? c.Country.Ar_Name : c.Country.En_Name),
                    Value = c.Country.ID
                }).ToList();
            data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        //GetUnionCountries_List
        public Dictionary<string, object> GetUnionCountries_List(int unionId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Union_Country>().GetData().Where(c => c.Union_ID == unionId)
                .Select(c => new CustomOption
                {
                    DisplayText = (lang == "1" ? c.Country.Ar_Name : c.Country.En_Name),
                    Value = c.Country.ID
                }).ToList();
            data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        //Get_Countries
        public Dictionary<string, object> Get_Countries(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Country>().GetData().Where(c => c.IsActive == true && c.User_Deletion_Id == null)
                .Select(c => new CustomOption
                {
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).ToList();
            data.Insert(0, new CustomOption() { DisplayText = (lang == "1" ? "كل الدول" : "All Countries"), Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        //***************************************//
        //Get_Persons
        public Dictionary<string, object> Get_Persons(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Person>().GetData().Where(c => c.IsActive == true && c.User_Deletion_Id == null)
                .Select(c => new CustomOptionLongId
                {
                    DisplayText = c.Name,
                    Value = c.ID
                }).ToList();
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        //GetPersonById_IdNum
        public Dictionary<string, object> GetPersonById_IdNum(long personId, string personIdNum, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = new PersonDTO();
            if (personId > 0)
            {
                data = uow.Repository<Person>().GetData().Where(c => c.ID == personId)
                .Select(a => new PersonDTO
                {
                    ID = a.ID,
                    Name = a.Name,
                    Person_IDType = a.Person_IDType,
                    personIdType_Name = (lang == "1" ? a.A_SystemCode.ValueName : a.A_SystemCode.ValueNameEN),
                    IDNumber = a.IDNumber,
                    nationality = (lang == "1" ? a.Country.Ar_Name : a.Country.En_Name),
                    Country_ID = a.Country_ID,
                    Address = a.Address,
                    IsActive = (bool)a.IsActive,
                    Job = a.Job,
                    Phone = a.Phone,
                    Email = a.Email

                }).SingleOrDefault();
            }
            else if (!string.IsNullOrEmpty(personIdNum))
            {
                data = uow.Repository<Person>().GetData().Where(c => c.IDNumber == personIdNum)
               .Select(a => new PersonDTO
               {
                   ID = a.ID,
                   Name = a.Name,
                   Person_IDType = a.Person_IDType,
                   personIdType_Name = (lang == "1" ? a.A_SystemCode.ValueName : a.A_SystemCode.ValueNameEN),
                   IDNumber = a.IDNumber,
                   nationality = (lang == "1" ? a.Country.Ar_Name : a.Country.En_Name),
                   Country_ID = a.Country_ID,
                   Address = a.Address,
                   IsActive = (bool)a.IsActive,
                   Job = a.Job,
                   Phone = a.Phone,
                   Email = a.Email

               }).SingleOrDefault();
            }

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        //**********************************************//
        //Get_PublicOrgType
        public Dictionary<string, object> Get_PublicOrgType(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<PublicOrganization_Type>().GetData().Where(c => c.User_Deletion_Id == null)
                .Select(c => new CustomOption
                {
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).ToList();
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }

        public Dictionary<string, object> Get_PublicOrg_ByType(int orgType_Id, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Public_Organization>().GetData().
                Where(c => c.User_Deletion_Id == null && c.IsActive == true && c.PublicOrgType_ID == orgType_Id)
                .Select(c => new CustomOptionLongId
                {
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).ToList();
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        //GetPublicOrgById
        public Dictionary<string, object> GetPublicOrgById(long publicOrgId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Public_Organization>().GetData().Where(c => c.ID == publicOrgId)
                .Select(a => new Public_OrganizationDTO
                {
                    ID = a.ID,
                    orgName = (lang == "1" ? a.Name_Ar : a.Name_En),
                    orgAddress = (lang == "1" ? a.Address_Ar : a.Address_En),
                    IsActive = a.IsActive,
                    IsNational = a.IsNational

                }).SingleOrDefault();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }

        //*************************************************************************************//
        //Insert_FarmCountryReq
        public Dictionary<string, object> Insert_FarmCountryReq(FarmCountryDTO entity, List<string> Device_Info)
        {
            try
            {
                if (entity.Country_ID > 0)
                {
                    if (!GetAny_FarmCountry(entity))
                    {
                        var CModel = Mapper.Map<Farm_Country>(entity);
                        CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Farm_Country_seq");
                        CModel = uow.Repository<Farm_Country>().InsertReturn(CModel);
                        uow.SaveChanges();

                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
                    }
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                    }
                }
                else
                {
                    if (entity.UnionId > 0)
                    {
                        //all union countries
                        foreach (var item in uow.Repository<Union_Country>().GetData().
                            Where(u => u.Union_ID == entity.UnionId && u.IsActive == true && u.User_Deletion_Id == null
                            && u.Country.IsActive == true && u.Country.User_Deletion_Id == null).
                            Select(u => u.Country_ID).Distinct().ToList())
                        {
                            entity.Country_ID = item;

                            if (!GetAny_FarmCountry(entity))
                            {
                                var CModel = Mapper.Map<Farm_Country>(entity);
                                CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Farm_Country_seq");
                                CModel = uow.Repository<Farm_Country>().InsertReturn(CModel);
                                uow.SaveChanges();
                            }
                        }

                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
                    }
                    else
                    {
                        //all countries
                        foreach (var item in uow.Repository<Country>().GetData().
                            Where(u => u.User_Deletion_Id == null && u.IsActive == true).Select(u => u.ID).ToList())
                        {
                            entity.Country_ID = item;

                            if (!GetAny_FarmCountry(entity))
                            {
                                var CModel = Mapper.Map<Farm_Country>(entity);
                                CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Farm_Country_seq");
                                CModel = uow.Repository<Farm_Country>().InsertReturn(CModel);
                                uow.SaveChanges();
                            }
                        }

                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //Update_FarmCountryReq
        public Dictionary<string, object> Update_FarmCountryReq(FarmCountryDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny_FarmCountry(entity))
                {
                    Farm_Country CModel = uow.Repository<Farm_Country>().Findobject(entity.ID);

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        entity.User_Updation_Date = CModel.User_Updation_Date;
                        entity.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<Farm_Country>().Update(Co);
                    uow.SaveChanges();

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
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
        //Delete_FarmCountryReq
        public Dictionary<string, object> Delete_FarmCountryReq(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Farm_Country>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Farm_Country>().Update(Cmodel);
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
        public bool GetAny_FarmCountry(FarmCountryDTO entity)
        {
            var obj = entity;
            return uow.Repository<Farm_Country>().GetAny(p => p.User_Deletion_Id == null &&
                                        p.Farm_Request_ID == obj.Farm_Request_ID &&
                                        p.Country_ID == obj.Country_ID &&
                                        p.Start_Date == obj.Start_Date &&
                                         p.End_Date == obj.End_Date &&
                                        (obj.ID == 0 ? true : p.ID != obj.ID));

        }

        //*****************************************************************************************//
        public bool GetAny_FarmCompany(Farm_CompanyDTO entity)
        {
            var obj = entity;
            return uow.Repository<Farm_Company>().GetAny(p => p.User_Deletion_Id == null &&
                                        p.Farm_ID == obj.Farm_ID &&
                                        p.ExporterType_Id == obj.ExporterType_Id &&
                                        p.Company_ID == obj.Company_ID &&
                                        (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        //Insert Company
        public Dictionary<string, object> Insert_FarmCompany(Farm_CompanyDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny_FarmCompany(entity))
                {
                    Farm_CompanyDTO dto = new Farm_CompanyDTO();
                    dto.ExporterType_Id = entity.ExporterType_Id;

                    if (entity.ExporterType_Id == 6)
                    {
                        dto.Company_ID = entity.Company_ID;
                    }
                    else if (entity.ExporterType_Id == 7)
                    {
                        dto.Company_ID = entity.Org_ID;
                    }
                    else if (entity.ExporterType_Id == 8)
                    {
                        dto.Company_ID = entity.Person_ID;
                    }
                    dto.Farm_ID = entity.Farm_ID;
                    dto.IsAcive = entity.IsAcive;
                    dto.Start_Date = entity.Start_Date;
                    dto.End_Date = entity.End_Date;
                    dto.User_Creation_Date = entity.User_Creation_Date;
                    dto.User_Creation_Id = entity.User_Creation_Id;

                    var CModel = Mapper.Map<Farm_Company>(dto);
                    CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Farm_Company_seq");
                    CModel = uow.Repository<Farm_Company>().InsertReturn(CModel);
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
        //Update Company
        public Dictionary<string, object> Update_FarmCompany(Farm_CompanyDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny_FarmCompany(entity))
                {
                    if (entity.ExporterType_Id == 8)
                    {
                        //person
                        var person = entity.personData;
                        var personDB = uow.Repository<Person>().GetData().Where(p => p.IDNumber == person.IDNumber).FirstOrDefault();

                        if (personDB != null)
                        {
                            entity.Company_ID = personDB.ID;
                        }
                        else
                        {
                            var personModel = Mapper.Map<Person>(person);
                            personModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Person_seq");
                            personModel = uow.Repository<Person>().InsertReturn(personModel);
                            uow.SaveChanges();

                            entity.Company_ID = personModel.ID;
                        }
                    }

                    Farm_CompanyDTO dto = new Farm_CompanyDTO();
                    dto.ID = entity.ID;
                    dto.ExporterType_Id = entity.ExporterType_Id;
                    dto.Company_ID = entity.Company_ID;
                    dto.Farm_ID = entity.Farm_ID;
                    dto.IsAcive = entity.IsAcive;
                    dto.Start_Date = entity.Start_Date;
                    dto.End_Date = entity.End_Date;

                    var CModel = uow.Repository<Farm_Company>().Findobject(dto.ID);

                    dto.User_Creation_Date = CModel.User_Creation_Date;
                    dto.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        dto.User_Updation_Date = CModel.User_Updation_Date;
                        dto.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(dto, CModel);
                    uow.Repository<Farm_Company>().Update(Co);
                    uow.SaveChanges();

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
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
        //DELETE company
        public Dictionary<string, object> Delete_FarmCompany(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Farm_Company>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Farm_Company>().Update(Cmodel);
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
        //****************************************************************************************//
        //Get_PlantCategoryList
        public Dictionary<string, object> Get_PlantCategoryList(long plantId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemCategory>().GetData().Where(c => c.User_Deletion_Id == null && c.Item_ID == plantId)
                .Select(c => new CustomOptionLongId
                {
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).ToList();
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        //Get_PlantCategoryListByGrp
        public Dictionary<string, object> Get_PlantCategoryListByGrp(long catGrpId, long plantId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemCategory>().GetData().
                Where(c => c.User_Deletion_Id == null && c.Item_ID == plantId && c.ItemCategories_Group_ID == catGrpId)
                .Select(c => new CustomOptionLongId
                {
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).ToList();
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        //GetScientificName
        public Dictionary<string, object> GetScientificName(long plantId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().Where(c => c.ID == plantId)
                .Select(a => a.Scientific_Name).SingleOrDefault();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> Insert_FarmPlant(Farm_ItemCategoriesDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny_FarmPlant(entity))
                {

                    var CModel = Mapper.Map<Farm_ItemCategories>(entity);
                    CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("Farm_ItemCategories_seq");
                    CModel = uow.Repository<Farm_ItemCategories>().InsertReturn(CModel);
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
        //Update Company
        public Dictionary<string, object> Update_FarmPlant(Farm_ItemCategoriesDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny_FarmPlant(entity))
                {
                    var CModel = uow.Repository<Farm_ItemCategories>().Findobject(entity.ID);

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        entity.User_Updation_Date = CModel.User_Updation_Date;
                        entity.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<Farm_ItemCategories>().Update(Co);
                    uow.SaveChanges();

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
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
        //DELETE company
        public Dictionary<string, object> Delete_FarmPlant(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<Farm_ItemCategories>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<Farm_ItemCategories>().Update(Cmodel);
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

        //GetAny_FarmPlant
        public bool GetAny_FarmPlant(Farm_ItemCategoriesDTO entity)
        {
            var obj = entity;
            return uow.Repository<Farm_ItemCategories>().GetAny(p => p.User_Deletion_Id == null &&
                                       p.Farm_ID == obj.Farm_ID &&
                                       p.ItemCategories_ID == obj.ItemCategories_ID &&
                                       p.StartDate == obj.StartDate &&
                                       p.EndDate == obj.EndDate &&
                                        (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        //********************************************************************//
        //Insert_FarmAttachment
        public Dictionary<string, object> Insert_FarmAttachment(A_AttachmentDataDTO entity, List<string> Device_Info)
        {
            try
            {
                var CModel = Mapper.Map<A_AttachmentData>(entity);
                CModel.Id = uow.Repository<object>().GetNextSequenceValue_Long("A_AttachmentData_seq");
                CModel = uow.Repository<A_AttachmentData>().InsertReturn(CModel);
                uow.SaveChanges();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //Update_FarmAttachment
        public Dictionary<string, object> Update_FarmAttachment(A_AttachmentDataDTO entity, List<string> Device_Info)
        {
            try
            {
                var CModel = uow.Repository<A_AttachmentData>().Findobject(entity.Id);

                entity.User_Creation_Date = CModel.User_Creation_Date;
                entity.User_Creation_Id = CModel.User_Creation_Id;

                if (CModel.User_Updation_Id != null)
                {
                    entity.User_Updation_Date = CModel.User_Updation_Date;
                    entity.User_Updation_Id = CModel.User_Updation_Id;
                }

                var Co = Mapper.Map(entity, CModel);
                uow.Repository<A_AttachmentData>().Update(Co);
                uow.SaveChanges();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Delete_FarmAttachment(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<A_AttachmentData>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<A_AttachmentData>().Update(Cmodel);
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

        //**************************************************//
        //Update_RequestAccept
        public Dictionary<string, object> Update_RequestAccept(FarmRequestDTO entity, List<string> Device_Info)
        {
            try
            {
                Farm_Request CModel = uow.Repository<Farm_Request>().Findobject(entity.ID);

                CModel.IsAcceppted = entity.IsAcceppted;
                CModel.User_Updation_Date = entity.User_Updation_Date;
                CModel.User_Updation_Id = entity.User_Updation_Id;

                uow.Repository<Farm_Request>().Update(CModel);
                uow.SaveChanges();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetByRequest(long farmId, long requestId, List<string> Device_Info)
        {
            try
            { 
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var farmdata = uow.Repository<FarmsData>().GetData().Where(a => a.ID == farmId)
                    .Select(x => new FarmsDataDTO()
                    {
                        Address_Ar = x.Address_Ar,
                        Address_En = x.Address_En,
                        FarmCode_14 = x.FarmCode_14,
                        FileUpload = x.FileUpload,
                        FileUpload_File = x.FileUpload,
                        ID = x.ID,
                        GPSRead = x.GPSRead,
                        IsApproved = x.IsApproved,
                        Status = x.Status,
                        ThePivot = x.ThePivot,
                        User_Creation_Date = x.User_Creation_Date,
                        User_Creation_Id = x.User_Creation_Id,
                        User_Deletion_Date = x.User_Deletion_Date,
                        User_Deletion_Id = x.User_Deletion_Id,
                        User_Updation_Date = x.User_Updation_Date,
                        User_Updation_Id = x.User_Updation_Id,
                        Village_ID = x.Village_ID,
                        Name_Ar = x.Name_Ar,
                        Name_En = x.Name_En,
                        Center_Id = (short)x.Center_Id,
                        Govern_ID = (short)x.Govern_ID,
                        VillageName = (lang == "1" ? x.Village.Ar_Name : x.Village.En_Name),
                        CenterName = (lang == "1" ? x.Center.Ar_Name : x.Center.En_Name),
                        GoveName = (lang == "1" ? x.Governate.Ar_Name : x.Governate.En_Name),
                        Item_ID = x.Item_ID,
                        IsActive = (bool)x.IsActive,

                        PlantName = x.Item_ID > 0 ? (lang == "1" ? x.Item.Name_Ar : x.Item.Name_En) : "",
                        ScientificName = x.Item_ID > 0 ? x.Item.Scientific_Name : "",
                        group_Id = x.Item_ID > 0 ? (int)x.Item.Group_ID : 0,
                        secClass_Id = x.Item_ID > 0 ? (int)x.Item.Group.SecClass_ID : 0,
                        mainClass_Id = x.Item_ID > 0 ? (int)x.Item.Group.SecondaryClassification.MainClass_ID : 0,
                        isKnown = x.Item_ID > 0 ? (bool)x.Item.Is_known_item : false

                    }).SingleOrDefault();

                var exporterType = uow.Repository<Farm_Company>().GetData().
                    Where(a => a.Farm_ID == farmId && a.User_Deletion_Id == null).FirstOrDefault().ExporterType_Id;

                exporterType = (exporterType == null ? 0 : exporterType);
                var exporterType_name = "0";
                if (exporterType != 0)
                {
                     exporterType_name =
                               (lang == "1" ? uow.Repository<A_SystemCode>().GetData().Where(s => s.Id == exporterType && s.SystemCodeTypeId == 3).FirstOrDefault().ValueName
                                : uow.Repository<A_SystemCode>().GetData().Where(s => s.Id == exporterType && s.SystemCodeTypeId == 3).FirstOrDefault().ValueNameEN);
                }
                farmdata.ownerData = uow.Repository<Farm_Company>().GetData().
                   Where(a => a.Farm_ID == farmId && a.User_Deletion_Id == null).
                   Select(a => new Farm_CompanyDTO
                   {
                       Company_ID = exporterType == 6 ? a.Company_ID : null,
                       Org_ID = exporterType == 7 ? a.Company_ID : null,
                       Person_ID = exporterType == 8 ? a.Company_ID : null,
                       ExporterType_Id = (int)exporterType,
                       exporterType_name = exporterType_name,
                       Start_Date = a.Start_Date,
                       End_Date = a.End_Date,
                       IsAcive = a.IsAcive
                   }).FirstOrDefault();
                //Edit_Eslam
                if (exporterType == 6)//company
                {


                    farmdata.ownerData.companyData = uow.Repository<Company_National>().GetData().
                    Where(a => a.ID == farmdata.ownerData.Company_ID && a.User_Deletion_Id == null).
                    Select(a => new CompanyNationalDTO
                    {
                        ID = a.ID,
                        compName = (lang == "1" ? a.Name_Ar : a.Name_En),
                        compOwnerName = (lang == "1" ? a.Owner_Ar : a.Owner_En),
                        GoveName = (lang == "1" ? a.Center.Governate.Ar_Name : a.Center.Governate.En_Name),
                        CenterName = (lang == "1" ? a.Center.Ar_Name : a.Center.En_Name),
                        VillageName = (lang == "1" ? a.Village.Ar_Name : a.Village.En_Name),
                        address = (lang == "1" ? a.Address_Ar : a.Address_En),

                        IsActive = (bool)a.IsActive
                    }).FirstOrDefault();

                    farmdata.ownerData.Contact_Data = uow.Repository<Ex_ContactData>()
                        .GetData().Include(f => f.ContactType).
                   Where(a => a.Exporter_ID == farmdata.ownerData.companyData.ID && a.ExporterType_Id == 6).
                   Select(a => new ContactTypeDTO
                   {
                       Name_Ar = (lang == "1" ? a.ContactType.Name_Ar : a.ContactType.Name_En),
                       Value = a.Value
                   }).ToList();



                }
                else if (exporterType == 7)//public org
                {
                    farmdata.ownerData.orgData = uow.Repository<Public_Organization>().GetData().
                    Where(a => a.ID == farmdata.ownerData.Org_ID).
                    Select(a => new Public_OrganizationDTO
                    {
                        ID = a.ID,
                        orgName = (lang == "1" ? a.Name_Ar : a.Name_En),
                        orgAddress = (lang == "1" ? a.Address_Ar : a.Address_En),
                        orgTypeName = (lang == "1" ? a.PublicOrganization_Type.Name_Ar : a.PublicOrganization_Type.Name_En),
                        IsNational = a.IsNational,
                        IsActive = a.IsActive
                    }).FirstOrDefault();
                }
                else if (exporterType == 8)//person
                {
                    farmdata.ownerData.personData = uow.Repository<Person>().GetData().
                    Where(a => a.ID == farmdata.ownerData.Person_ID).
                    Select(a => new PersonDTO
                    {
                        ID = a.ID,
                        Name = a.Name,
                        personIdType_Name = (lang == "1" ? a.A_SystemCode.ValueName : a.A_SystemCode.ValueNameEN),
                        Address = a.Address,
                        IDNumber = a.IDNumber,
                        nationality = (lang == "1" ? a.Country.Ar_Name : a.Country.En_Name),
                        Phone = a.Phone,
                        Email = a.Email,
                        Job = a.Job

                    }).FirstOrDefault();
                }

                var plantList = uow.Repository<Farm_Request_ItemCategories>().GetData().Include(c => c.Farm_ItemCategories.ItemCategory)
                     .Where(a => a.User_Deletion_Id == null && a.Farm_Request_ID == requestId).
                     Select(a => new Farm_Request_ItemCategoriesDTO
                     {
                         ID = a.ID,
                         Farm_ItemCategories_ID = a.Farm_ItemCategories_ID,
                         categoryName = (lang == "1" ? a.Farm_ItemCategories.ItemCategory.Name_Ar : a.Farm_ItemCategories.ItemCategory.Name_En),
                         Area_Acres = a.Farm_ItemCategories.Area_Acres,
                         //Quantity_Ton = a.Quantity_Ton..ff,
                         StartDate = a.Farm_ItemCategories.StartDate,
                         EndDate = a.Farm_ItemCategories.EndDate,
                         IsActive = a.IsActive,
                         //ss
                         //ItemCategories_Group_ID = a.ItemCategory.ItemCategories_Group_ID

                     }).ToList();
                foreach (var item in plantList)
                {
                    //var isActive = uow.Repository<Farm_Request_ItemCategories>().GetData()
                    //   .Where(a => a.Farm_Request_ID == requestId
                    //   && a.Farm_ItemCategories_ID == item.ID).ToList();
                    //if (plantList.Count() > 0)
                    //{

                    var Commit = uow.Repository<Farm_Committee>().GetData()
                     .Where(a => a.Farm_Request_ID == requestId).FirstOrDefault();
                    if (Commit != null)
                    {

                        var quntity = uow.Repository<Farm_Committee_Examination>().GetData()
                           .Where(a => a.FarmCommittee_ID == Commit.ID
                           && a.Farm_Request_ItemCategories_ID == item.ID).FirstOrDefault();
                        if (quntity != null)
                        {
                            item.Quantity_Ton = quntity.Quantity_Ton * item.Area_Acres;
                        }

                        //}
                    }
                }

                farmdata.plantList = plantList;

                farmdata.requestLst = uow.Repository<Farm_Request>().GetData().
                    Where(a => a.FarmsData_ID == farmId && a.User_Deletion_Id == null && a.ID == requestId && a.IS_OnlineOffline == true).
                    Select(a => new FarmRequestDTO
                    {
                        ID = a.ID,
                        IsAcceppted = (bool)a.IsAcceppted,
                        IsActive = (bool)a.IsActive,
                        Start_Date = a.Start_Date,
                        End_Date = a.End_Date,
                        IsPaid = a.IsPaid,
                        Start_Date_Request = a.Start_Date_Request,
                        End_Date_Request = a.End_Date_Request,
                        Fees = a.Fees,
                        Fees_Actual = a.Fees_Actual

                    }).ToList();
                //ss
                foreach (var request in farmdata.requestLst)
                {
                    request.countryLst = uow.Repository<Farm_Country>().GetData().
                    Where(a => a.Farm_Request_ID == request.ID && a.User_Deletion_Id == null).
                    Select(a => new FarmCountryDTO
                    {
                        ID = a.ID,
                        IsAcceppted = (bool)a.IsAcceppted,
                        IsActive = (bool)a.IsActive,
                        Start_Date = a.Start_Date,
                        End_Date = a.End_Date,

                        Country_ID = a.Country_ID,
                        country_Name = (lang == "1" ? a.Country.Ar_Name : a.Country.En_Name)

                    }).ToList();
                }

                farmdata.attachmentList = uow.Repository<A_AttachmentData>().GetData().
                    Where(a => a.RowId == farmId && a.User_Deletion_Id == null && a.A_AttachmentTableNameId == 5).
                    Select(a => new A_AttachmentDataDTO
                    {
                        Id = a.Id,
                        Attachment_TypeName = a.Attachment_TypeName,
                        Attachment_Number = a.Attachment_Number,
                        AttachmentPath = a.AttachmentPath,
                        AttachmentPath_Binary = a.AttachmentPath_Binary,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate
                    }).ToList();

                //farmdata._Farm_Requst_List = (from fc in entities.Farm_Constrain
                //                              join fd in entities.FarmsDatas on fc.Item_ID equals fd.Item_ID
                //                              join fr in entities.Farm_Request on fd.ID equals fr.FarmsData_ID
                //                              // join fc2 in entities.Farm_Country on fr.ID equals fc2.Farm_Request_ID //AND fc.Country_Id = fc2.Country_Id
                //                              join fc2 in entities.Farm_Country on new { a = fr.ID, b = fc.Country_Id } equals new { a = fc2.Farm_Request_ID, b = fc2.Country_ID }
                //                              join at1 in entities.AnalysisTypes on fc.AnalysisType_ID equals at1.ID into at11
                //                              from at1 in at11.DefaultIfEmpty()
                //                              where fr.ID == requestId
                //                              select new Farm_Requst_ListDTO
                //                              {
                //                                  farm_Id = fd.ID,
                //                                  AnalysisType_ID = fc.AnalysisType_ID,
                //                                  AnalysisType_Name = at1.Name_Ar,
                //                                  ConstrainText_text = fc.Farm_Constrain_Text.ConstrainText_Ar,
                //                                  ConstrainText_ID = fc.Farm_Constrain_Text.ID
                //                              }).Distinct().ToList();

                farmdata._Farm_Requst_List = (from fc in entities.Farm_Constrain
                                              join fd in entities.FarmsDatas on fc.Item_ID equals fd.Item_ID
                                              join fr in entities.Farm_Request on fd.ID equals fr.FarmsData_ID
                                              // join fc2 in entities.Farm_Country on fr.ID equals fc2.Farm_Request_ID //AND fc.Country_Id = fc2.Country_Id
                                              join fc2 in entities.Farm_Country on fr.ID equals fc2.Farm_Request_ID into fc22// new { a = fr.ID, b = fc.Country_Id } equals new { a = fc2.Farm_Request_ID, b = fc2.Country_ID }
                                              from fc2 in fc22.DefaultIfEmpty()
                                              join at1 in entities.AnalysisTypes on fc.AnalysisType_ID equals at1.ID into at11
                                              from at1 in at11.DefaultIfEmpty()
                                              where fr.ID == requestId
                                              select new Farm_Requst_ListDTO
                                              {
                                                  farm_Id = fd.ID,
                                                  AnalysisType_ID = fc.AnalysisType_ID,
                                                  AnalysisType_Name = at1.Name_Ar,
                                                  ConstrainText_text = fc.Farm_Constrain_Text.ConstrainText_Ar,
                                                  ConstrainText_ID = fc.Farm_Constrain_Text.ID
                                              }).Distinct().ToList();
                ////اسباب الرفض
                farmdata.Farm_Requst_RefuseReason_List = (from frrr in entities.Farm_Request_Refuse_Reason


                                                          where frrr.Farm_Request_ID == requestId
                                                          select new Farm_Requst_RefuseReason_ListDTO
                                                          {
                                                              Nots = frrr.Nots,
                                                              Refuse_Reason = frrr.Refuse_Reason.Name_Ar,
                                                          }).Distinct().ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, farmdata);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> UpdateApprovedFarm(FarmsDataDTO entity, List<string> Device_Info)
        {
            try
            {
                var obj = entity as FarmsDataDTO;
                FarmsData CModel = uow.Repository<FarmsData>().Findobject(obj.ID);

                CModel.FarmCode_14 = obj.FarmCode_14;
                CModel.IsActive = obj.IsActive;
                CModel.User_Updation_Date = obj.User_Updation_Date;
                CModel.User_Updation_Id = obj.User_Updation_Id;

                uow.Repository<FarmsData>().Update(CModel);
                uow.SaveChanges();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        //*******************sayed 25-8-2022 **************************

        //refuse reason
        public Dictionary<string, object> FillDrop_RefuseReason(int refuse, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Refuse_Reason>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.IsActive == true && lab.IsExport == 78);
            //if (refuse == 1)
            //{
            //    data = data.Where(res => res.Refused_stopped == 84);
            //}
            //else
            //{
            //    data = data.Where(res => res.Refused_stopped == 83);
            //}
            var data2 = data.Select(c => new CustomOption
            {
                //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data2.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data2);
        }

        public Dictionary<string, object> InsertReasons(ReasonsList_FarmDTO dto, List<string> Device_Info)
        {
            try
            {
                Farm_Request CModel = uow.Repository<Farm_Request>().Findobject(dto.Farm_Request_ID);

                CModel.IsAcceppted = false;
                CModel.IsStatus = false;
                CModel.User_Updation_Date = dto.User_Updation_Date;
                CModel.User_Updation_Id = dto.User_Updation_Id;
                uow.Repository<Farm_Request>().Update(CModel);
                uow.SaveChanges();

                ReasonsList_FarmDTO rr = new ReasonsList_FarmDTO();
                foreach (var id in dto.refuseReasonsIds)
                {

                    rr.Farm_Request_ID = dto.Farm_Request_ID;
                    rr.Refuse_Reason_ID = id;
                    rr.Nots = dto.Nots;
                    rr.User_Creation_Id = dto.User_Creation_Id;
                    rr.User_Creation_Date = dto.User_Creation_Date;
                    InsertReason(rr, Device_Info);
                }


                //var obj = entity as FarmsDataDTO;
                //FarmsData CModel = uow.Repository<FarmsData>().Findobject(obj.ID);

                //CModel.FarmCode_14 = obj.FarmCode_14;
                //CModel.IsActive = obj.IsActive;
                //CModel.User_Updation_Date = obj.User_Updation_Date;
                //CModel.User_Updation_Id = obj.User_Updation_Id;

                //uow.Repository<FarmsData>().Update(CModel);
                //uow.SaveChanges();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dto.Farm_Request_ID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> InsertReason(ReasonsList_FarmDTO entity, List<string> Device_Info)
        {
            try
            {
                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Request_Refuse_Reason_SEQ");
                entity.ID = idd;
                var CModel = Mapper.Map<Farm_Request_Refuse_Reason>(entity);

                uow.Repository<Farm_Request_Refuse_Reason>().InsertRecord(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}