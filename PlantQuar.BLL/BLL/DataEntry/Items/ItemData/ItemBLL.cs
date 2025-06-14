using AutoMapper;
using PlantQuar.BLL.BLL.DataEntry.Items.Item_Descriptions;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Controllers.DataEntry.Items.ItemData
{
    public class ItemBLL : IGenericBLL<ItemDTO>
    {
        private UnitOfWork uow;

        public ItemBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Item entity = uow.Repository<Item>().Findobject(Id);
                var empDTO = Mapper.Map<Item, ItemDTO>(entity);
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
            var count = uow.Repository<Item>().GetData().Where(p => p.User_Deletion_Id == null
             // get undeleted parent
             && p.ID != 0  //empty row
             && p.Family.User_Deletion_Id == null
             && p.Group.User_Deletion_Id == null
             ).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Item>().GetData().Where(
                    p =>  // get undeleted parent
                    p.ID != 0 && //empty row
                p.User_Deletion_Id == null &&
                p.Family.User_Deletion_Id == null &&
               p.Group.User_Deletion_Id == null)
             .Select(p => new ItemDTO
             {
                 ID = p.ID,
                 Name_Ar = p.Name_Ar,
                 Name_En = p.Name_En,
                 Scientific_Name = p.Scientific_Name,
                 Family_ID = p.Family_ID,
                 Group_ID = p.Group_ID,
                 Descreption_Ar = p.Descreption_Ar,
                 Descreption_En = p.Descreption_En,
                 Picture = p.Picture,
                 IsForbidden = p.IsForbidden,
                 IsPlantInEgypt = (bool)p.IsPlantInEgypt,
                 User_Updation_Id = p.User_Updation_Id,
                 User_Updation_Date = p.User_Updation_Date,
                 User_Deletion_Id = p.User_Deletion_Id,
                 User_Deletion_Date = p.User_Deletion_Date,
                 User_Creation_Id = p.User_Creation_Id,
                 User_Creation_Date = p.User_Creation_Date,
                 Item_Code = p.Item_Code,
                 ListItemPartType_Id = p.ItemParts.Where(c => c.Item_ID == p.ID).Select(c => c.SubPart_ID).ToList(),
                 Agriculture_17 = p.Agriculture_17
             }
                 ).ToList();
                //  ).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                //  var dataDTO = data.Select(Mapper.Map<Item, ItemDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
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
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var dd = (from ii in entities.Items
                          join gg in entities.Groups on ii.Group_ID equals gg.ID
                          join sec in entities.SecondaryClassifications on gg.SecClass_ID equals sec.ID
                          join main in entities.MainCalssifications on sec.MainClass_ID equals main.ID
                          join it in entities.Item_Type on main.Item_Type_ID equals it.Id
                          join ff in entities.Families on ii.Family_ID equals ff.ID
                          join or in entities.Orders on ff.Order_ID equals or.ID
                          join ph in entities.PhylumSubphylums on or.Phylum_ID equals ph.ID
                          join king in entities.Kingdoms on ph.Kingdom_ID equals king.ID

                          select new ItemDTO
                          {
                              ID = ii.ID,
                              Name_Ar = ii.Name_Ar,
                              Name_En = ii.Name_En,
                              Scientific_Name = ii.Scientific_Name,
                              Descreption_Ar = ii.Descreption_Ar,
                              Descreption_En = ii.Descreption_En,
                              ForbiddenReason = ii.ForbiddenReason,
                              User_Deletion_Id = ii.User_Deletion_Id,
                              IsForbidden = ii.IsForbidden,
                              Is_known_item = (bool)ii.Is_known_item,
                              // IsPlantInEgypt = (bool)ii.IsPlantInEgypt,
                              IsPlantInEgypt = (bool)ii.IsPlantInEgypt,
                              Group_ID = gg.ID,
                              SecClass_ID = sec.ID,
                              MainClass_ID = main.ID,
                              Item_Type_ID = it.Id,
                              Family_ID = ff.ID,
                              Order_ID = or.ID,
                              Phylum_ID = ph.ID,
                              Kingdom_ID = king.ID,
                              Picture = ii.Picture,
                              Item_Code = ii.Item_Code,
                              Agriculture_17 = ii.Agriculture_17
                          }).ToList();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<ItemDTO>();
                Int64 data_Count = 0;


                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = dd.Where(p => p.Name_En.StartsWith(enName)
                     && p.ID != 0   //empty row/
                    && p.User_Deletion_Id == null
// get undeleted parent
// && p.Family.User_Deletion_Id == null
//&& p.Group.User_Deletion_Id == null

).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = dd.Where(p => p.Name_Ar.StartsWith(arName)
                                     && p.User_Deletion_Id == null
                                 // get undeleted parent
                                 //&& p.Family.User_Deletion_Id == null
                                 //&& p.Group.User_Deletion_Id == null
                                 ).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = dd.Where(p => p.User_Deletion_Id == null
                                 // get undeleted parent
                                 //&& p.Family.User_Deletion_Id == null
                                 //&& p.Group.User_Deletion_Id == null
                                 ).ToList();
                }
                else
                {
                    data = dd.Where(p => (p.Name_Ar.StartsWith(arName) && p.Name_En.StartsWith(enName))
                                     && p.User_Deletion_Id == null
                                 // get undeleted parent
                                 //&& p.Family.User_Deletion_Id == null
                                 //&& p.Group.User_Deletion_Id == null
                                 ).ToList();
                }
                var dataDto = data
                //    Select(p => new ItemDTO
                //{
                //    ID = p.ID,

                //    Name_Ar = p.Name_Ar,
                //    Name_En = p.Name_En,
                //    Scientific_Name = p.Scientific_Name,


                //    Family_ID = p.Family_ID,
                //    Item_Type_ID = p.Item_Type_ID,
                //        Group_ID = p.Group_ID,
                //   // MainClass_ID =  uow.Repository<MainCalssification>().GetData().Where(a => a.Item_Type_ID == p.Item_Type_ID ).FirstOrDefault().ID,
                //        //SecClass_ID = uow.Repository<SecondaryClassification>().GetData().Where(a => a.MainClass_ID == p.MainClass_ID).FirstOrDefault().ID,
                //        Descreption_Ar = p.Descreption_Ar,
                //    Descreption_En = p.Descreption_En,
                //    Picture = p.Picture,
                //    ForbiddenReason = p.ForbiddenReason,
                //    IsForbidden = p.IsForbidden,
                //    User_Updation_Id = p.User_Updation_Id,
                //    User_Updation_Date = p.User_Updation_Date,
                //    User_Deletion_Id = p.User_Deletion_Id,
                //    User_Deletion_Date = p.User_Deletion_Date,
                //    User_Creation_Id = p.User_Creation_Id,
                //    User_Creation_Date = p.User_Creation_Date,
                //    IsPlantInEgypt = p.IsPlantInEgypt,
                //    ListItemPartType_Id = p.ItemParts.Where(c => c.Item_ID == p.ID).Select(c => c.SubPart_ID).ToList()
                //}
                // )
                .OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();//.Select(Mapper.Map<Item, ItemDTO>);
                foreach (var ite in dataDto)
                {
                    ite.ListItemPartType_Id = uow.Repository<ItemPart>().GetData().Where(c => c.Item_ID == ite.ID).Select(c => c.SubPart_ID).ToList();


                }
                switch (jtSorting)
                {
                    case "Name_Ar ASC":
                        dataDto = dataDto.OrderBy(t => t.Name_Ar).ToList();
                        break;
                    case "Name_Ar DESC":
                        dataDto = dataDto.OrderByDescending(t => t.Name_Ar).ToList();
                        break;
                    case "Name_En ASC":
                        dataDto = dataDto.OrderBy(t => t.Name_En).ToList();
                        break;
                    case "Name_En DESC":
                        dataDto = dataDto.OrderByDescending(t => t.Name_En).ToList();
                        break;
                }
                data_Count = data.Count();
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
        public Dictionary<string, object> GetItemByFamilyAndGroup(byte? itemType, int? familyId, int? groupId, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var dd = (from ii in entities.Items
                          join gg in entities.Groups on ii.Group_ID equals gg.ID
                          join sec in entities.SecondaryClassifications on gg.SecClass_ID equals sec.ID
                          join main in entities.MainCalssifications on sec.MainClass_ID equals main.ID
                          join it in entities.Item_Type on main.Item_Type_ID equals it.Id
                          join ff in entities.Families on ii.Family_ID equals ff.ID
                          join or in entities.Orders on ff.Order_ID equals or.ID
                          join ph in entities.PhylumSubphylums on or.Phylum_ID equals ph.ID
                          join king in entities.Kingdoms on ph.Kingdom_ID equals king.ID

                          select new ItemDTO
                          {
                              ID = ii.ID,
                              Name_Ar = ii.Name_Ar,
                              Name_En = ii.Name_En,
                              Scientific_Name = ii.Scientific_Name,
                              Descreption_Ar = ii.Descreption_Ar,
                              Descreption_En = ii.Descreption_En,
                              ForbiddenReason = ii.ForbiddenReason,
                              User_Deletion_Id = ii.User_Deletion_Id,
                              IsForbidden = ii.IsForbidden,
                              Is_known_item = (bool)ii.Is_known_item,
                              IsPlantInEgypt = (bool)ii.IsPlantInEgypt,
                              Group_ID = gg.ID,
                              SecClass_ID = sec.ID,
                              MainClass_ID = main.ID,
                              Item_Type_ID = it.Id,
                              Family_ID = ff.ID,
                              Order_ID = or.ID,
                              Phylum_ID = ph.ID,
                              Kingdom_ID = king.ID,
                              Picture = ii.Picture,
                              Agriculture_17 = ii.Agriculture_17
                          }).ToList();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                //ar data = new List<ItemDTO>();
                Int64 data_Count = 0;



                var dataDto = dd
                    .Where(i => i.User_Deletion_Id == null && i.Item_Type_ID == itemType && i.Family_ID == familyId && i.Group_ID == groupId)



                .OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                foreach (var ite in dataDto)
                {
                    ite.ListItemPartType_Id = uow.Repository<ItemPart>().GetData().Where(c => c.Item_ID == ite.ID).Select(c => c.SubPart_ID).ToList();


                }
                switch (jtSorting)
                {
                    case "Name_Ar ASC":
                        dataDto = dataDto.OrderBy(t => t.Name_Ar).ToList();
                        break;
                    case "Name_Ar DESC":
                        dataDto = dataDto.OrderByDescending(t => t.Name_Ar).ToList();
                        break;
                    case "Name_En ASC":
                        dataDto = dataDto.OrderBy(t => t.Name_En).ToList();
                        break;
                    case "Name_En DESC":
                        dataDto = dataDto.OrderByDescending(t => t.Name_En).ToList();
                        break;
                }
                data_Count = dataDto.Count();
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
                var Cmodel = uow.Repository<Item>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Item>().Update(Cmodel);
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

        public bool GetAny(ItemDTO entity)
        {
            var obj = entity;// as ItemDTO;
            obj.Name_Ar = obj.Name_Ar.Trim();
            obj.Name_En = obj.Name_En.Trim();
            return uow.Repository<Item>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && p.Item_Type_ID == obj.Item_Type_ID && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(ItemDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Item>(entity);
                    CModel.Name_Ar = CModel.Name_Ar.Trim();
                    CModel.Name_En = CModel.Name_En.Trim();
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Item_seq");
                    CModel = uow.Repository<Item>().InsertReturn(CModel);
                    uow.SaveChanges();
                    if (entity.ListItemPartType_Id != null)
                    {
                        #region Item Parts
                        ItemPartBLL ItemPart_Model = new ItemPartBLL();
                        ItemPart_Model.InsertRecords(CModel.ID, entity.User_Creation_Id, entity.User_Creation_Date,
                                 entity.ListItemPartType_Id, Device_Info);
                        #endregion
                    }
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
           // catch (Exception ex)
            
           //// catch (Exception ex)
           // {
           //     uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
           //     return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
           // }


            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        uow.Repository<Object>().Save_Error(this.GetType().FullName, ve.ErrorMessage, MethodBase.GetCurrentMethod().Name, Device_Info);
                             return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
                    }
                }
                throw;
            }
        }
        public Dictionary<string, object> Update(ItemDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as ItemDTO;
                    Item CModel = uow.Repository<Item>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    obj.User_Updation_Date = DateTime.Now;
                    obj.User_Updation_Id = entity.User_Updation_Id;
                    obj.Agriculture_17 =  entity.Agriculture_17;
                    //obj.Item_Code = CModel.Item_Code;
                    //EslamMaher
                    obj.IsPlantInEgypt = entity.IsPlantInEgypt == null || entity.IsPlantInEgypt == false ? false : true;
                    if (entity.Picture == null)
                    {
                        //get old one
                        entity.Picture = CModel.Picture;
                    }
                    else if (CModel.Picture != entity.Picture)
                    {
                        obj.Picture = entity.Picture;
                        //entity.Picture = CModel.Picture;
                    }
                    var Co = Mapper.Map(obj, CModel);
                    Co.Name_Ar = Co.Name_Ar.Trim();
                    Co.Name_En = Co.Name_En.Trim();
                    //Co.Is_Plant_Egypt = Co.IsPlantInEgypt;
                    uow.Repository<Item>().Update(Co);
                    uow.SaveChanges();

                    #region Item Parts
                    if (entity.ListItemPartType_Id != null)
                    {
                        ItemPartBLL ItemPart_Model = new ItemPartBLL();
                        ItemPart_Model.UpdateRecords(entity.ID, entity.User_Creation_Id, entity.User_Creation_Date,
                                 entity.ListItemPartType_Id, Device_Info);
                    }
                    #endregion

                    var empDTO = Mapper.Map<Item, ItemDTO>(Co);
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
        public Dictionary<string, object> GetItemByFamilyAndGroupAndKnown(byte? itemType, int? familyId, int? groupId, bool? known, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();

                if (familyId != null && groupId != null)
                {
                    data = uow.Repository<Item>().GetData().Where(i => i.User_Deletion_Id == null && i.Item_Type_ID == itemType && i.Family_ID == familyId && i.Group_ID == groupId && i.Is_known_item == known).
                   Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();

                }
                else if (familyId == null && groupId != null)
                {
                    data = uow.Repository<Item>().GetData().Where(i => i.User_Deletion_Id == null && i.Item_Type_ID == itemType && i.Group_ID == groupId && i.Is_known_item == known).
                   Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();

                }
                else if (familyId != null && groupId == null)
                {
                    data = uow.Repository<Item>().GetData().Where(i => i.User_Deletion_Id == null && i.Item_Type_ID == itemType && i.Family_ID == familyId && i.Is_known_item == known).
                   Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();

                } //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info, int Group_ID)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().Where(c => c.Group_ID == Group_ID).Select(c => new CustomOptionLongId
            {
                //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info, long Item_ID)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().Where(a => a.User_Deletion_Id == null && a.ID == Item_ID).
                Select(p => new CustomOptionLongId
                {
                    Value = p.ID,
                    
                    DisplayText = p.Scientific_Name,
                    
                }
                 ).ToList();
            //set default value fz 17-4-2019
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        //SARA
        public Dictionary<string, object> Fill_NotForbiddenItems(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().Where(a => a.User_Deletion_Id == null && a.IsForbidden == false).
                Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            //set default value fz 17-4-2019
            //   data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = 0 });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //END SARA

        //sayed farm
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            //  data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> Fill_PlantCategories(List<string> Device_Info, int Item_ID)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemCategory>().GetData().Where(a => a.User_Deletion_Id == null && a.Item_ID == Item_ID
            //13-5-2020 fz removed as in constrain don't need it
            // && a.IsForbidden == false
            ).
                Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //sayed
        public Dictionary<string, object> GetAll(Int64 ItemID, Int64 Farm_ID, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Farm_ItemCategories>();
                Int64 data_Count = 0;
                data = uow.Repository<Farm_ItemCategories>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               //&& p.Plant_ID == ItemID
               && p.Farm_ID == Farm_ID
               && p.User_Deletion_Id == null
           && p.User_Deletion_Id == null).ToList();
                //Complete Code
                var dataDto = data.OrderBy(A => A.ID).Select(Mapper.Map<Farm_ItemCategories, FarmPlantDTO>);

                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("ItemPart_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //sara by sayed
        public Dictionary<string, object> FillDrop_byKnown(int Group_ID, int Family_ID, bool isKnown, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().
                Where(a => a.User_Deletion_Id == null && a.Group_ID == Group_ID && a.Family_ID == Family_ID &&
                a.Is_known_item == isKnown).
                Select(c => new CustomOptionLongId
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> getItemByItemType(int ItemTypID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().
                Where(a => a.User_Deletion_Id == null && a.Item_Type_ID == ItemTypID).
                Select(c => new CustomOptionLongId
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }


        //***************************
        public Dictionary<string, object> ItemFilterByTypeFamilyAndGroup(int? itemType, int? familyId, int? groupId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();

                if (itemType != null && familyId != null && groupId != null)
                {
                    data = uow.Repository<Item>().GetData().Where(i => i.User_Deletion_Id == null && i.Item_Type_ID == itemType && i.Family_ID == familyId && i.Group_ID == groupId).
               Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();

                }
                else if (itemType != null && groupId != null)
                {
                    data = uow.Repository<Item>().GetData().Where(i => i.User_Deletion_Id == null && i.Item_Type_ID == itemType && i.Group_ID == groupId).
              Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
                }


                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> ItemFilterByTypeFamilyAndGroup_Edite(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            //  data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

    }
}
