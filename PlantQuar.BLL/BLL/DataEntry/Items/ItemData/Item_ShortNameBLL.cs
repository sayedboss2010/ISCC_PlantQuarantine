using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.DataEntry.Items.ItemData
{
    public class Item_ShortNameBLL : IGenericBLL<Item_ShortNameDTO>
    {
        private UnitOfWork uow;

        public Item_ShortNameBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {

            try
            {
                Item_ShortName entity = uow.Repository<Item_ShortName>().Findobject(Id);
                var empDTO = Mapper.Map<Item_ShortName, Item_ShortNameDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount(List<string> Device_Info)
        {
            try
            {
                var count = uow.Repository<Item_ShortName>().GetData().Where(p => p.User_Deletion_Id == null
                 // get undeleted parent
                 // && p.Item.User_Deletion_Id == null 
                 //&& p.PlantPart.User_Deletion_Id == null
                 && p.Item_Purpose.User_Deletion_Id == null && p.Item_Status.User_Deletion_Id == null
                  ).Count();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Item_ShortName>().GetData().Where(p => p.User_Deletion_Id == null
                      // get undeleted parent
                      // && p.Item.User_Deletion_Id == null 
                      // && p.PlantPart.User_Deletion_Id == null
                      && p.Item_Purpose.User_Deletion_Id == null && p.Item_Status.User_Deletion_Id == null
                    ).OrderBy(A => (lang == "1") ? A.ShortName_Ar : A.ShortName_En).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<Item_ShortName, Item_ShortNameDTO>);
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
                string lang = Device_Info[2];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Item_ShortName>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item_ShortName>().GetData().Where(p =>
                    p.ShortName_En.StartsWith(enName) && p.User_Deletion_Id == null
                     // get undeleted parent
                     //&& p.Plant.User_Deletion_Id == null 
                     // && p.PlantPart.User_Deletion_Id == null
                     && p.Item_Purpose.User_Deletion_Id == null && p.Item_Status.User_Deletion_Id == null
                      ).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item_ShortName>().GetData().Where(p =>
                                   p.ShortName_Ar.StartsWith(arName) && p.User_Deletion_Id == null
                                    // get undeleted parent
                                    // && p.Plant.User_Deletion_Id == null
                                    // && p.PlantPart.User_Deletion_Id == null
                                    && p.Item_Purpose.User_Deletion_Id == null && p.Item_Status.User_Deletion_Id == null
                                     ).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item_ShortName>().GetData().Where(p => p.User_Deletion_Id == null
                                    // get undeleted parent
                                    // && p.Plant.User_Deletion_Id == null 
                                    //&& p.PlantPart.User_Deletion_Id == null
                                    && p.Item_Purpose.User_Deletion_Id == null && p.Item_Status.User_Deletion_Id == null
                                     ).ToList();
                }
                else
                {
                    data = uow.Repository<Item_ShortName>().GetData().Where(p =>
                            (p.ShortName_Ar.StartsWith(arName) && p.ShortName_En.StartsWith(enName))
                                   && p.User_Deletion_Id == null
                                    // get undeleted parent
                                    //&& p.Item.User_Deletion_Id == null 
                                    // && p.PlantPart.User_Deletion_Id == null
                                    && p.Item_Purpose.User_Deletion_Id == null && p.Item_Status.User_Deletion_Id == null
                                     ).ToList();
                }
                //Mapper.Map<Item_ShortName, Plant_ShortNameDTO>
                var dataDto = data
                    .Select(x => new Item_ShortNameDTO
                    {
                        ID = x.ID,
                        Item_ID = x.Item_ID,
                        IsKnown = x.Item_ID == null ? true : (bool)uow.Repository<Item>().GetData().
                                            Where(a => a.ID == x.Item_ID).FirstOrDefault().Is_known_item,
                        SubPart_ID = x.SubPart_ID == null ? -1 : uow.Repository<ItemPart>().GetData().
                                            Where(a => a.SubPart_ID == x.SubPart_ID && a.Item_ID == x.Item_ID).FirstOrDefault().ID,
                        Item_Status_ID = x.Item_Status_ID,
                        Item_Purpose_ID = x.Item_Purpose_ID,
                        ShortName_Ar = x.ShortName_Ar,
                        ShortName_En = x.ShortName_En,
                        ExportStatus = x.ExportStatus,
                        ImportStatus = x.ImportStatus,
                        Reason = x.Reason,
                        ItemCategories_Group_ID = x.ItemCategories_Group_ID,
                        QualitativeGroup_Id = x.QualitativeGroup_Id,
                        Item_Type_ID = x.Item_Type_ID,
                        Product_ID = x.Product_ID,
                        Is_ImportTaxFree = (bool)x.Is_ImportTaxFree,
                     
                        //totItems = x.Product_ID != null ? uow.Repository<Product>().GetData().Where(a => a.ID == x.Product_ID).FirstOrDefault().Item_ID : null


                    }).OrderBy(A => (lang == "1") ? A.ShortName_Ar : A.ShortName_En).Skip(index).Take(pageSize).ToList();

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Plant_ShortName_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetShortNames(long itemId, List<string> Device_Info)
        {
            try
            {

                Dictionary<string, object> dic = new Dictionary<string, object>();

 


                var data = uow.Repository<Item_ShortName>().GetData()
                    .Where(i => i.User_Deletion_Id == null &&
                 i.Item_ID == itemId).ToList();




                var dataDto = data.Select(x => new Item_ShortNameDTO
                {
                    //ID = x.ID,
                    Item_ID = x.Item_ID,                                       
                    Item_Status_ID = x.Item_Status_ID,
                    Item_Purpose_ID = x.Item_Purpose_ID,
                    ShortName_Ar = x.ShortName_Ar,
                    ShortName_En = x.ShortName_En,
                    //ExportStatus = x.ExportStatus,
                    //ImportStatus = x.ImportStatus,
                    //Reason = x.Reason,
                    //QualitativeGroup_Id = x.QualitativeGroup_Id,
                    //Item_Type_ID = x.Item_Type_ID,
                    //Product_ID = x.Product_ID,
                    //Is_ImportTaxFree = (bool)x.Is_ImportTaxFree,

                }).
               ToList();




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetShortNamesFilter(long? itemId, byte? itemType,int pageSize, int index, List<string> Device_Info)
        {
            try
            {

                Dictionary<string, object> dic = new Dictionary<string, object>();

                Int64 data_Count = 0;



                var data = uow.Repository<Item_ShortName>().GetData()
                    .Where(i => i.User_Deletion_Id == null &&
                    i.Item_Type_ID == itemType && i.Item_ID == itemId).ToList();




               var dataDto = data.Select(x => new Item_ShortNameDTO
                {
                    ID = x.ID,
                    Item_ID = x.Item_ID,
                    IsKnown = x.Item_ID == null ? true : (bool)uow.Repository<Item>().GetData().
                                            Where(a => a.ID == x.Item_ID).FirstOrDefault().Is_known_item,
                    SubPart_ID = x.SubPart_ID == null ? -1 : uow.Repository<ItemPart>().GetData().
                                            Where(a => a.SubPart_ID == x.SubPart_ID && a.Item_ID == x.Item_ID).FirstOrDefault().ID,
                    Item_Status_ID = x.Item_Status_ID,
                    Item_Purpose_ID = x.Item_Purpose_ID,
                    ShortName_Ar = x.ShortName_Ar,
                    ShortName_En = x.ShortName_En,
                    ExportStatus = x.ExportStatus,
                    ImportStatus = x.ImportStatus,
                    Reason = x.Reason,
                   ItemCategories_Group_ID = x.ItemCategories_Group_ID,
                   QualitativeGroup_Id = x.QualitativeGroup_Id,
                    Item_Type_ID = x.Item_Type_ID,
                    Product_ID = x.Product_ID,
                    Is_ImportTaxFree = (bool)x.Is_ImportTaxFree,

                    //totItems = x.Product_ID != null ? uow.Repository<Product>().GetData().Where(a => a.ID == x.Product_ID).FirstOrDefault().Item_ID : null


                }).
                OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                
                //switch (jtSorting)
                //{
                //    case "Name_Ar ASC":
                //        dataDto = dataDto.OrderBy(t => t.ShortName_Ar).ToList();
                //        break;
                //    case "Name_Ar DESC":
                //        dataDto = dataDto.OrderByDescending(t => t.ShortName_Ar).ToList();
                //        break;
                //    case "Name_En ASC":
                //        dataDto = dataDto.OrderBy(t => t.ShortName_En).ToList();
                //        break;
                //    case "Name_En DESC":
                //        dataDto = dataDto.OrderByDescending(t => t.ShortName_En).ToList();
                //        break;
                //}
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
                var Cmodel = uow.Repository<Item_ShortName>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Item_ShortName>().Update(Cmodel);
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

        public bool GetAny(Item_ShortNameDTO entity)
        { 
            var obj = entity as Item_ShortNameDTO;
            obj.ShortName_Ar = obj.ShortName_Ar.Trim();
            obj.ShortName_En = obj.ShortName_En.Trim();
            if (entity.IsKnown == true)
            {
                return uow.Repository<Item_ShortName>().GetAny(p => p.User_Deletion_Id == null
                                        && (obj.ID == 0 ? true : p.ID != obj.ID)
                                        && p.Item_ID == obj.Item_ID && (( p.SubPart_ID == obj.SubPart_ID &&
                                       p.Item_Status_ID == obj.Item_Status_ID && p.Item_Purpose_ID == obj.Item_Purpose_ID&&p.ItemCategories_Group_ID == obj.ItemCategories_Group_ID) ||
                                        (p.ShortName_Ar == obj.ShortName_Ar || p.ShortName_En == obj.ShortName_En)));

            }
            else
            {
                return uow.Repository<Item_ShortName>().GetAny(p => p.User_Deletion_Id == null
                                        && (obj.ID == 0 ? true : p.ID != obj.ID)
                                        && p.Item_ID == obj.Item_ID
                                        &&
                                        (p.ShortName_Ar == obj.ShortName_Ar || p.ShortName_En == obj.ShortName_En));


            }
        }
        //******************************************//
        public Dictionary<string, object> Insert(Item_ShortNameDTO entity, List<string> Device_Info)
        {
            try
            {
                if (entity.SubPart_ID != null)
                {
                    entity.SubPart_ID =
                        (uow.Repository<ItemPart>().GetData().Where(a => a.ID == entity.SubPart_ID).FirstOrDefault().SubPart_ID);
                }

                if (!GetAny(entity))
                {

                    var CModel = Mapper.Map<Item_ShortName>(entity);
                    CModel.ShortName_Ar = CModel.ShortName_Ar.Trim();
                    CModel.ShortName_En = CModel.ShortName_En.Trim();

                    //int? subpartId = 
                    //(CModel.SubPart_ID != null) ? 
                    //uow.Repository<PlantPart>().GetData().Where(a => a.ID == CModel.SubPart_ID).FirstOrDefault().SubPart_ID
                    //:  null;


                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Item_ShortName_seq");

                    uow.Repository<Item_ShortName>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(Item_ShortNameDTO entity, List<string> Device_Info)
        {
            try
            {
                if (entity.SubPart_ID != null)
                {
                    entity.SubPart_ID = uow.Repository<ItemPart>().GetData().Where(a => a.ID == entity.SubPart_ID).FirstOrDefault().SubPart_ID;
                }
                if (!GetAny(entity))
                {
                    var obj = entity as Item_ShortNameDTO;
                    Item_ShortName CModel = uow.Repository<Item_ShortName>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    Co.ShortName_Ar = Co.ShortName_Ar.Trim();
                    Co.ShortName_En = Co.ShortName_En.Trim();
                    //eman
                    //var subpartId = CModel.SubPart_ID != null ? uow.Repository<PlantPart>().GetData().Where(a => a.ID == CModel.SubPart_ID).FirstOrDefault().SubPart_ID : -1;



                    uow.Repository<Item_ShortName>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Item_ShortName, Item_ShortNameDTO>(Co);
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
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Item_ShortName>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.ShortName_Ar : c.ShortName_En)
                    ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Item_ShortName>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.ShortName_Ar : c.ShortName_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop_ShortName_Im_Permission(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var data = (from ipi in  entities.Im_PermissionItems 
                            join inor in  entities.Im_Initiator  on ipi.Im_Initiator_ID equals inor.ID
                            join isn in entities.Item_ShortName  on inor.Item_ShortName_ID equals isn.ID into isn1
                            from isn in isn1.DefaultIfEmpty()
                            join qg in entities.QualitativeGroups  on inor.QualitativeGroup_Id equals qg.Id    into qg1
                            from qg in qg1.DefaultIfEmpty()
                            select new CustomOptionLongId
                            {
                                DisplayText = isn.ID ==null ? qg.Name_Ar: isn.ShortName_Ar  ,                             
                                Value = isn.ID == null ? qg.Id : isn.ID,                                
                            }).Distinct().OrderBy(a => a.DisplayText).ToList();

                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //public Dictionary<string, object> Get_PlantShortNameId(long plantId, byte purposeId, byte statusId, long partType)
        //{
        //    var data = uow.Repository<Item_ShortName>().GetData().Where(a => a.Plant_ID == plantId && a.Purpose_ID == purposeId &&
        //    a.ProductStatus_ID == statusId && a.PlantPart_ID == partType && a.User_Deletion_Id == null).SingleOrDefault();

        //    if (data != null)
        //    {
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.ID);


        //    }
        //    else
        //    {
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, null);

        //    }
        //}
        ////SARA
        //public Dictionary<string, object> Get_PlantShortName(int plantId, int purposeId, int statusId, long partType)
        //{
        //    var data = uow.Repository<Item_ShortName>().GetData().Where(a => a.Plant_ID == plantId && a.Purpose_ID == purposeId &&
        //    a.ProductStatus_ID == statusId && a.PlantPart_ID == partType && a.User_Deletion_Id == null).SingleOrDefault();

        //    Dictionary<string, string> data_return = new Dictionary<string, string>();
        //    data_return["shortName"] = "-----";
        //    data_return["ExportStatus"] = "True";
        //    data_return["HSCODE"] = "-----";

        //    if (data != null)
        //    {               
        //        if (data.ShortName_Ar != null) data_return["shortName"] = data.ShortName_Ar;
        //        data_return["ExportStatus"] = data.ExportStatus.ToString();
        //        if (data.HSCode != null) data_return["HSCODE"] = data.HSCode.ToString();
        //    }
        //    //set default value fz 17-4-2019
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data_return);
        //}
        ////



        //eman 12-8-2020

        public Dictionary<string, object> GetItems(byte itemTypeId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Item>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_Type_ID == itemTypeId).Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En)
                    ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetItems_IsKnown(byte itemTypeId, bool isKnown, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Item>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_Type_ID == itemTypeId && lab.Is_known_item == isKnown).Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En)
                    ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetItemsTypes(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Item_Type>().GetData().Where(a=>a.Id != 2).Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En)
                    ,
                    Value = c.Id
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetTotalItems(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Item>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En)
                   ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetItemStatus(byte itemTypeId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();

                if (itemTypeId == 0)
                {
                    data = uow.Repository<Item_Status>().GetData().Where(lab => lab.User_Deletion_Id == null&&lab.IsActive == true).Select(c => new CustomOptionLongId
                    {
                        //change display lang
                        DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name)
                  ,
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
                }
                else
                {
                    data = uow.Repository<Item_Status>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_Type_ID == itemTypeId && lab.IsActive == true).Select(c => new CustomOptionLongId
                    {
                        //change display lang
                        DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name)
                    ,
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
                }

                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetItemPurpose(byte itemTypeId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId>();

                if (itemTypeId == 0)
                {
                    data = uow.Repository<Item_Purpose>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.IsActive == true).Select(c => new CustomOptionLongId
                    {
                        //change display lang
                        DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name)
                  ,
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
                }
                else
                {
                    data = uow.Repository<Item_Purpose>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_Type_ID == itemTypeId && lab.IsActive == true).Select(c => new CustomOptionLongId
                    {
                        //change display lang
                        DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name)
                    ,
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
                }

                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetQualitiveGroup(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<QualitativeGroup>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.IsActive == true).Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En)
                    ,
                    Value = c.Id
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Get_ItemParts(long ItemId, byte itemTypeId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = new List<CustomOptionLongId>();

            if (ItemId == -1)
            {
                data = uow.Repository<ItemPart>().GetData().
                Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.SubPart.Name_Ar : c.SubPart.Name_En),
                    //DisplayText = c.PlantPartType.Name_Ar, 
                    Value = c.ID
                }).ToList();

            }
            else
            {
                data = uow.Repository<ItemPart>().GetData().Include(r => r.SubPart).
                Where(lab => lab.User_Deletion_Id == null && lab.Item_ID == ItemId && lab.SubPart.Item_Type_ID == itemTypeId).Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.SubPart.Name_Ar : c.SubPart.Name_En),
                    //DisplayText = c.PlantPartType.Name_Ar, 
                    Value = c.ID
                }).ToList();
            }

            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        //public Dictionary<string, object> GetProducts(long? itemId, List<string> Device_Info)
        //{
        //    try
        //    {
        //        string lang = Device_Info[2];
        //        var data = uow.Repository<Product>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_ID == itemId).Select(c => new CustomOptionLongId
        //        {
        //            //change display lang
        //            DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En)
        //            ,
        //            Value = c.ID
        //        }).OrderBy(a => a.DisplayText).ToList();
        //        //set default value fz 17-4-2019
        //        data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        //eman for constrains
        public Dictionary<string, object> GetMainClassifications(byte itemTypeId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<MainCalssification>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_Type_ID == itemTypeId).Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En)
                    ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetSecondaryClassifications(int MainClass_ID, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<SecondaryClassification>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.MainClass_ID == MainClass_ID).Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En)
                    ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetGroups(int SecClass_ID, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Group>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.SecClass_ID == SecClass_ID).Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En)
                    ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Fill_GroupPlants(int groupId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().Where(a => a.User_Deletion_Id == null && a.Group_ID == groupId).
                Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> Fill_GroupPlantsWithKnown(int groupId,bool known, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item>().GetData().Where(a => a.User_Deletion_Id == null && a.Group_ID == groupId && a.Is_known_item == known).
                Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //public Dictionary<string, object> GetProductsByGroupId(int groupId, List<string> Device_Info)
        //{
        //    try
        //    {
        //        string lang = Device_Info[2];
        //        var data = uow.Repository<Product>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Group_ID == groupId).Select(c => new CustomOptionLongId
        //        {
        //            //change display lang
        //            DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En)
        //             ,
        //            Value = c.ID
        //        }).OrderBy(a => a.DisplayText).ToList();
        //        //set default value fz 17-4-2019
        //        data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        public Dictionary<string, object> GetItemShortNames(long itemId, bool isProduct, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<Item_ShortName>();
                if (isProduct == true)
                {
                    data = uow.Repository<Item_ShortName>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Product_ID == itemId && lab.ImportStatus==true).ToList();
                }
                else
                {
                    data = uow.Repository<Item_ShortName>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_ID == itemId && lab.ImportStatus == true).ToList();
                }
                var datareturn = data.Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.ShortName_Ar : c.ShortName_En)
                     ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                datareturn.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, datareturn);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetItemShortNamesNoImportStatus(long itemId, bool isProduct, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<Item_ShortName>();
                if (isProduct == true)
                {
                    data = uow.Repository<Item_ShortName>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Product_ID == itemId ).ToList();
                }
                else
                {
                    data = uow.Repository<Item_ShortName>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_ID == itemId ).ToList();
                }
                var datareturn = data.Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.ShortName_Ar : c.ShortName_En)
                     ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                datareturn.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, datareturn);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }



        public Dictionary<string, object> Fill_Initiators(long plantShortNameId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Im_Initiator>().GetData().Include(d => d.Country).
                Where(a => a.User_Deletion_Id == null && a.Item_ShortName_ID == plantShortNameId && a.IsActive == true && a.Initiator_Status == 37).
                Select(c => new CustomOptionLongId { DisplayText = c.Country == null?"كل دول العالم":c.Country.Ar_Name, Value = c.ID }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> Fill_InitiatorsQualG(short QualGId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Im_Initiator>().GetData().Include(d => d.Country).Where(a => a.User_Deletion_Id == null && a.IsActive == true && a.QualitativeGroup_Id == QualGId&&a.Initiator_Status==37).
                Select(c => new CustomOptionLongId { DisplayText = c.Country == null ? "كل دول العالم" : c.Country.Ar_Name, Value = c.ID }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> GetItemShortName(long itemId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<Item_ShortName>();
                
                    data = uow.Repository<Item_ShortName>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_ID == itemId && lab.ImportStatus == true).ToList();
               
                var datareturn = data.Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.ShortName_Ar : c.ShortName_En)
                     ,
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
                //set default value fz 17-4-2019
                datareturn.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, datareturn);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Fill_GroupPlants2(int groupId, List<string> Device_Info)
        {
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            string lang = Device_Info[2];

            var data = (from it in entities.Items
                        join ish in entities.Item_ShortName on it.ID equals ish.Item_ID
                        join sss in entities.Im_Initiator on ish.ID equals sss.Item_ShortName_ID
                        where it.Group_ID == groupId
                        //&&
                        //ish.Item_ID != null
                        //&& sss.Plant_ShortName_ID != null
                        select new CustomOptionLongId { DisplayText = (lang == "1" ? it.Name_Ar : it.Name_En), Value = it.ID }).ToList();

            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());

            //string lang = Device_Info[2];
            //var data = uow.Repository<Item>().GetData().Include(o => o.Item_ShortName).Where(a => a.User_Deletion_Id == null && a.Group_ID == groupId && a.Item_ShortName != null).
            //    Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            ////set default value fz 17-4-2019
            //data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> Fill_GroupPlantsForproduct2(int groupId, List<string> Device_Info)
        {
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            string lang = Device_Info[2];

            var data = (from it in entities.Items
                       // join pr in entities.Products on it.ID equals pr.Item_ID
                        join ish in entities.Item_ShortName on it.ID equals ish.Product_ID
                        join sss in entities.Im_Initiator on ish.ID equals sss.Item_ShortName_ID
                        where it.Group_ID == groupId
                        //&&
                        //ish.Product_ID != null
                        //&& pr.Item_ID != null
                        select new CustomOptionLongId { DisplayText = (lang == "1" ? it.Name_Ar : it.Name_En), Value = it.ID }).ToList();

            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());

            //string lang = Device_Info[2];
            //var data = uow.Repository<Item>().GetData().Include(o => o.Item_ShortName).Where(a => a.User_Deletion_Id == null && a.Group_ID == groupId && a.Item_ShortName != null).
            //    Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            ////set default value fz 17-4-2019
            //data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //public Dictionary<string, object> GetProductsByGroupId2(int groupId, List<string> Device_Info)
        //{
        //    try
        //    {
        //        PlantQuarantineEntities entities = new PlantQuarantineEntities();
        //        string lang = Device_Info[2];

        //        var data = (from pr in entities.Products

        //                    join ish in entities.Item_ShortName on pr.ID equals ish.Product_ID
        //                    join sss in entities.Im_Initiator on ish.ID equals sss.Item_ShortName_ID
        //                    where pr.Group_ID == groupId
        //                    //&&
        //                    //ish.Product_ID != null

        //                    select new CustomOptionLongId { DisplayText = (lang == "1" ? pr.Name_Ar : pr.Name_En), Value = pr.ID }).ToList();

        //        data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });

        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());

        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        //***************************************//
        //SARA
        //public Dictionary<string, object> Get_ShortNameDetails(int ShortName, List<string> Device_Info)
        //{
        //    try
        //    {
        //        string lang = Device_Info[2];

        //        var data = uow.Repository<Item_ShortName>().GetData().
        //            Where(a => a.User_Deletion_Id == null && a.ID == ShortName).
        //            Select(c => new Item_ShortNameDTO
        //            {
        //                SubPart_Name = (lang == "1" ? c.SubPart.Name_Ar : c.SubPart.Name_En),
        //                Status_Name = (lang == "1" ? c.Item_Status.Ar_Name : c.Item_Status.En_Name),
        //                Purpose_Name = (lang == "1" ? c.Item_Purpose.Ar_Name : c.Item_Purpose.En_Name),

        //            }).SingleOrDefault();
        //        //set default value fz 17-4-2019                
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        //sara by sayed
        public Dictionary<string, object> Get_ShortNameDetails(int ShortName, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                var data = uow.Repository<Item_ShortName>().GetData().
                    Where(a => a.User_Deletion_Id == null && a.ID == ShortName).
                    Select(c => new Item_ShortNameDTO
                    {
                        SubPart_Name = (lang == "1" ? c.SubPart.Name_Ar : c.SubPart.Name_En),
                        Status_Name = (lang == "1" ? c.Item_Status.Ar_Name : c.Item_Status.En_Name),
                        Purpose_Name = (lang == "1" ? c.Item_Purpose.Ar_Name : c.Item_Purpose.En_Name),

                    }).SingleOrDefault();
                //set default value fz 17-4-2019                
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAllShortName( List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];

                var data = uow.Repository<Item_ShortName>().GetData().
                    Where(a => a.User_Deletion_Id == null ).
                    Select(c => new Item_ShortNameDTO
                    {
                        ID=c.ID,
                        ShortName_Ar=c.ShortName_Ar,
                        ImportStatus=c.ImportStatus,
                        SubPart_Name =  c.SubPart.Name_Ar ,
                        Status_Name =  c.Item_Status.Ar_Name ,
                        Purpose_Name =  c.Item_Purpose.Ar_Name 

                    }).ToList();

                //set default value fz 17-4-2019                
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //hader
        public Dictionary<string, object> GetScientific_Name(long itemId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                
                var data = uow.Repository<Item>().GetData().
                  Where(a => a.User_Deletion_Id == null && a.ID == itemId).
                  Select(c => new ItemDTO
                  {
                      Scientific_Name = c.Scientific_Name,


                  }).SingleOrDefault();                          
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}