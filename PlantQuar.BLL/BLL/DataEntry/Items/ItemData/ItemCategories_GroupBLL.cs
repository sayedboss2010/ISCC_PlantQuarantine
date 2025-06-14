using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Controllers.DataEntry.Items.ItemData
{

    public class ItemCategories_GroupBLL : IGenericBLL<ItemCategories_GroupDTO>
    {
        private UnitOfWork uow;

        public ItemCategories_GroupBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                ItemCategories_Group entity = uow.Repository<ItemCategories_Group>().Findobject(Id);
                var empDTO = Mapper.Map<ItemCategories_Group, ItemCategories_GroupDTO>(entity);
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
            var count = uow.Repository<ItemCategories_Group>().GetData().Where(p => p.User_Deletion_Id == null
             // get undeleted parent
             && p.User_Deletion_Id == null
                 && p.User_Deletion_Id == null
             ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<ItemCategories_Group>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<ItemCategories_Group>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<ItemCategories_Group>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<ItemCategories_Group, ItemCategories_GroupDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
            //try
            //{
            //    var data = uow.Repository<PortOrganization>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
            //    var dataDTO = data.Select(Mapper.Map<PortOrganization, PortOrganizationDTO>);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name,   Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }


        public Dictionary<string, object> GetAll
            (long itemId, string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<ItemCategories_Group>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<ItemCategories_Group>().GetData().
                        Where(p => p.Name_En.StartsWith(enName) && p.User_Deletion_Id == null
                        && p.User_Deletion_Id == null && p.User_Deletion_Id == null
                        /*&& p.Item_ID == itemId*/).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<ItemCategories_Group>().GetData().
                        Where(p => p.Name_Ar.StartsWith(arName) && p.User_Deletion_Id == null
                        && p.User_Deletion_Id == null && p.User_Deletion_Id == null
                        /*&& p.Item_ID == itemId*/).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<ItemCategories_Group>().GetData().
                        Where(p => p.User_Deletion_Id == null && p.User_Deletion_Id == null
                        && p.User_Deletion_Id == null /*&& p.Item_ID == itemId*/).ToList();
                }
                else
                {
                    data = uow.Repository<ItemCategories_Group>().GetData().
                        Where(p => p.Name_Ar.StartsWith(arName) && p.Name_En.StartsWith(enName) && p.User_Deletion_Id == null
                               && p.User_Deletion_Id == null && p.User_Deletion_Id == null
                               && p.Item_ID == itemId).ToList();
                }
                if(itemId != 0)
                    {
                    data = data.Where(p=>p.Item_ID == itemId).ToList();
                }
                var dataDto = data.Select(A => new ItemCategories_GroupDTO
                {
                    ID = A.ID,
                    Name_Ar = A.Name_Ar,
                    Item_ID = A.Item_ID,
                    Name_En = A.Name_En,
                    Descreption_Ar = A.Descreption_Ar,
                    Descreption_En =A.Descreption_En,                   
                    IsActive = A.IsActive,
                }).OrderBy(a => (lang == "1" ? a.Name_Ar : a.Name_En)).Skip(index).Take(pageSize).ToList();

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
                dic.Add("Item_Data", dataDto);

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
                var Cmodel = uow.Repository<ItemCategories_Group>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<ItemCategories_Group>().Update(Cmodel);
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

        public bool GetAny(ItemCategories_GroupDTO entity)
        {
            //  var obj = entity as ItemCategories_GroupDTO;
            return uow.Repository<ItemCategories_Group>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == entity.Name_Ar || p.Name_En == entity.Name_En)) && (entity.ID == 0 ? true : p.ID != entity.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(ItemCategories_GroupDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("ItemCategories_Group_SEQ");

                    entity.ID = id;
                    //  entity.Protect_Property = Upload_File.Upload_File_Data(entity.PicData, "ItemCategories_Group", entity.FileExtension);
                    var CModel = Mapper.Map<ItemCategories_Group>(entity);

                    uow.Repository<ItemCategories_Group>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(ItemCategories_GroupDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    //var obj = entity as ItemCategories_GroupDTO;
                    ItemCategories_Group CModel = uow.Repository<ItemCategories_Group>().Findobject(entity.ID);

                    entity.User_Creation_Date = CModel.User_Creation_Date;
                    entity.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        entity.User_Updation_Date = CModel.User_Updation_Date;
                        entity.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(entity, CModel);
                    uow.Repository<ItemCategories_Group>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<ItemCategories_Group, ItemCategories_GroupDTO>(Co);
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
            var data = uow.Repository<ItemCategories_Group>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Add(long? ItemId,List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemCategories_Group>().GetData().Where(lab => lab.User_Deletion_Id == null&&lab.Item_ID== ItemId).Select(c => new CustomOptionLongId
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList(); 
            //set default value fz 17-4-2019

            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemCategories_Group>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> Fill_ItemCategories(long ItemID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemCategories_Group>().GetData().Where(a => a.User_Deletion_Id == null && a.Item_ID == ItemID&&a.IsActive
            //13-5-2020 fz removed as in constrain don't need it
            // && a.IsForbidden == false
            ).
                Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //sayed
        public Dictionary<string, object> FillDrop_ByItemId(long itemId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemCategories_Group>().GetData().
                Where(a => a.User_Deletion_Id == null && a.Item_ID == itemId && a.IsActive).
                Select(c => new CustomOptionLongId
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

    }
}