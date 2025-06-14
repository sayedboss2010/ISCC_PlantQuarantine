using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.ExportRequest
{

    public class PlantCategoryBLL : IGenericBLL<PlantCategoryDTO>
    {
        private UnitOfWork uow;

        public PlantCategoryBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                ItemCategory entity = uow.Repository<ItemCategory>().Findobject(Id);
                var empDTO = Mapper.Map<ItemCategory, PlantCategoryDTO>(entity);
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
            var count = uow.Repository<ItemCategory>().GetData().Where(p => p.User_Deletion_Id == null
             // get undeleted parent
             && p.Company_National.User_Deletion_Id == null
                 && p.Item.User_Deletion_Id == null
             ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<ItemCategory>().GetData().Where(p => p.User_Deletion_Id == null
             // get undeleted parent
             && p.Company_National.User_Deletion_Id == null
                 && p.Item.User_Deletion_Id == null
             ).OrderBy(c => (lang == "1" ? c.Name_Ar : c.Name_En)).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<ItemCategory, PlantCategoryDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
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
                string lang = Device_Info[2];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<ItemCategory>();
                Int64 data_Count = 0;


                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<ItemCategory>().GetData().Where(p =>
                    p.Name_En.StartsWith(enName) && p.User_Deletion_Id == null
            // get undeleted parent
            && p.Company_National.User_Deletion_Id == null
                 && p.Item.User_Deletion_Id == null
            ).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<ItemCategory>().GetData().Where(p =>
                    p.Name_Ar.StartsWith(arName) && p.User_Deletion_Id == null
            // get undeleted parent
            && p.Company_National.User_Deletion_Id == null
                  && p.Item.User_Deletion_Id == null
            ).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<ItemCategory>().GetData().Where(p => p.User_Deletion_Id == null
                               // get undeleted parent
                               && p.Company_National.User_Deletion_Id == null
                                   && p.Item.User_Deletion_Id == null
                               ).ToList();
                }
                else
                {
                    data = uow.Repository<ItemCategory>().GetData().Where(p =>
                                     (p.Name_Ar.StartsWith(arName) && p.Name_En.StartsWith(enName))
                                       && p.User_Deletion_Id == null
                               // get undeleted parent
                               && p.Company_National.User_Deletion_Id == null
                                     && p.Item.User_Deletion_Id == null
                               ).ToList();
                }

                var dataDto = data
                .Select(A => new PlantCategoryDTO
                {
                    ID = A.ID,
                    Name_Ar = A.Name_Ar,
                    Plant_ID = A.Item_ID,
                    Name_En = A.Name_En,
                    Company_ID = A.Company_ID,
                    Register_NumDate = A.Register_NumDate,
                    Register_EndDate = A.Register_EndDate,
                    IsRegister = A.IsRegister,
                    TimeOut = A.TimeOut,
                    IsForbidden = A.IsForbidden,
                    CurrentStatus = A.CurrentStatus,
                    Protect_Property = A.Protect_Property,
                    Protect_Property_File = A.Protect_Property,
                    Notes = A.Notes,
                    Resolution_Number = A.Resolution_Number,
                    Resolution_Date = (A.Resolution_Date != null) ? A.Resolution_Date.Value : 0

                    //Resolution_Date = (A.Resolution_Date!=null)? A.Resolution_Date.Value.Year:0

                })
                .OrderBy(a => (lang=="1"?a.Name_Ar:a.Name_En)).Skip(index).Take(pageSize).ToList();

                data_Count = data.Count();
                switch (jtSorting)
                {
                    case "Name_Ar ASC":
                        data = data.OrderBy(t => t.Name_Ar).ToList();
                        break;
                    case "Name_Ar DESC":
                        data = data.OrderByDescending(t => t.Name_Ar).ToList();
                        break;
                    case "Name_En ASC":
                        data = data.OrderBy(t => t.Name_En).ToList();
                        break;
                    case "Name_En DESC":
                        data = data.OrderByDescending(t => t.Name_En).ToList();
                        break;
                }
                dic.Add("Count_Data", data_Count);
                dic.Add("Plant_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<ItemCategory>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<ItemCategory>().Update(Cmodel);
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

        public bool GetAny(PlantCategoryDTO entity)
        {
            //  var obj = entity as PlantCategoryDTO;
            return uow.Repository<ItemCategory>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == entity.Name_Ar || p.Name_En == entity.Name_En)) && (entity.ID == 0 ? true : p.ID != entity.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(PlantCategoryDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("PlantCategories_seq");

                    entity.ID = id;
                    //  entity.Protect_Property = Upload_File.Upload_File_Data(entity.PicData, "PlantCategory", entity.FileExtension);
                    var CModel = Mapper.Map<ItemCategory>(entity);
                  
                    uow.Repository<ItemCategory>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(PlantCategoryDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    //var obj = entity as PlantCategoryDTO;
                    ItemCategory CModel = uow.Repository<ItemCategory>().Findobject(entity.ID);

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        entity.User_Updation_Date = CModel.User_Updation_Date;
                        entity.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<ItemCategory>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<ItemCategory, PlantCategoryDTO>(Co);
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
            var data = uow.Repository<ItemCategory>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<ItemCategory>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> Fill_PlantCategories(int plantID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemCategory>().GetData().Where(a => a.User_Deletion_Id == null && a.Item_ID == plantID 
            //13-5-2020 fz removed as in constrain don't need it
           // && a.IsForbidden == false
            ).
                Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //public Dictionary<string, object> GetAllUsingParamForList(int Plant_ID)
        //{
        //    try
        //    {
        //        var data = uow.Repository<ItemCategory>().GetData().Where(a => a.User_Deletion_Id == null && a.Plant_ID == Plant_ID).ToList();

        //        var dataDto = data.OrderBy(A => A.ID).Select(Mapper.Map<PlantCategory, PlantCategoryDTO>);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name,   Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        //public Dictionary<string, object> GetAllUsingParamForAddEdit(int Plant_ID)
        //{
        //    try
        //    {
        //        var data = uow.Repository<ItemCategory>().GetData().Where(a => a.User_Deletion_Id == null && a.Plant_ID == Plant_ID).ToList();

        //        var dataDto = data.OrderBy(A => A.ID).Select(Mapper.Map<PlantCategory, PlantCategoryDTO>);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name,   Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
    }
}