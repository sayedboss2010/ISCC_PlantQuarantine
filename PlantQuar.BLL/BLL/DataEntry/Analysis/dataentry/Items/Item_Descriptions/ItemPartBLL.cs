using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Items.Item_Descriptions;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry.Items.Item_Descriptions
{
    public class ItemPartBLL : IGenericBLL<ItemPartDTO>
    {
        private UnitOfWork uow;

        public ItemPartBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                ItemPart entity = uow.Repository<ItemPart>().Findobject(Id);
                var empDTO = Mapper.Map<ItemPart, ItemPartDTO>(entity);
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
            var count = uow.Repository<ItemPart>().GetData().Where(p => p.User_Deletion_Id == null
                 // get undeleted parent
                 && p.Item.User_Deletion_Id == null
             && p.SubPart.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<ItemPart>().GetData().Where(p => p.User_Deletion_Id == null
                // get undeleted parent
                && p.Item.User_Deletion_Id == null && p.SubPart.User_Deletion_Id == null)
                .OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<ItemPart, ItemPartDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAllByItemID(Int64 ItemID, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<ItemPart>();
                Int64 data_Count = 0;

                data = uow.Repository<ItemPart>().GetData().Where(p => p.User_Deletion_Id == null && p.Item_ID == ItemID)
                .OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(x => new ItemPartDTO
                {
                    ID = x.ID,
                    Item_ID = x.Item_ID,
                    SubPart_ID = x.SubPart_ID,
                    IsAllowed = x.IsAllowed,
                    SubPart_Type_ID = (int)uow.Repository<SubPart>().GetData().Where(v => v.ID == x.SubPart_ID).FirstOrDefault().SubPart_Type_ID,

                });


                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("ItemPart_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
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
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<ItemPart>();
                Int64 data_Count = 0;
                data = uow.Repository<ItemPart>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Item.User_Deletion_Id == null
           && p.SubPart.User_Deletion_Id == null).ToList();
                //Complete Code
                var dataDto = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<ItemPart, ItemPartDTO>);

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

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<ItemPart>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<ItemPart>().Update(Cmodel);
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

        public bool GetAny(ItemPartDTO entity)
        {
            var obj = entity as ItemPartDTO;

            return uow.Repository<ItemPart>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.SubPart_ID == obj.SubPart_ID && p.Item_ID == obj.Item_ID)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        //******************************************//
        public Dictionary<string, object> Insert(ItemPartDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<ItemPart>(entity);
                    CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("ItemPart_seq");
                    uow.Repository<ItemPart>().InsertRecord(CModel);
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


        public Dictionary<string, object> InsertRecords(long Item_ID, short user_id, DateTime Date_Now, List<int> objRecords, List<string> Device_Info)
        {
            try
            {
                ItemPartDTO dto;
                foreach (var item in objRecords)
                {
                    dto = new ItemPartDTO();
                    dto.SubPart_ID = item;
                    dto.Item_ID = Item_ID;
                    dto.User_Creation_Date = Date_Now;
                    dto.User_Creation_Id = user_id;
                    Insert(dto, Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, objRecords);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> UpdateRecords(long Item_ID, short user_id, DateTime Date_Now, List<int> objRecords, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<ItemPart>().GetData().Where(x => x.Item_ID == Item_ID && x.User_Deletion_Id == null).ToList();
                var addlst = objRecords.Except(data.Select(x => x.SubPart_ID)).ToList();
                var deletelst = data.Where(x => objRecords.IndexOf(x.SubPart_ID) == -1).ToList();
                InsertRecords(Item_ID, user_id, Date_Now, addlst, Device_Info);
                foreach (var item in deletelst)
                {
                    Delete(new DeleteParameters() { id = item.ID, Userid = user_id, _DateNow = Date_Now }, Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, objRecords);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }

        }


        public Dictionary<string, object> Update(ItemPartDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as ItemPartDTO;
                    ItemPart CModel = uow.Repository<ItemPart>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<ItemPart>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<ItemPart, ItemPartDTO>(Co);
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

        //public Dictionary<string, object> ItemPart_List()
        //{
        //    var data = uow.Repository<ItemPart>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOptionLongId { DisplayText = c.SubPart.Name_Ar, Value = c.ID }).ToList();
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        //}

        //public Dictionary<string, object> GetAllUsingParamForList(int ItemId, List<string> Device_Info)
        //{
        //    try
        //    {
        //        var data = uow.Repository<ItemPart>().GetData().Where(a => a.User_Deletion_Id == null &&
        //        a.Item_ID == ItemId)
        //            .Select(c => new CustomOption { DisplayText = c.SubPart.Name_Ar, Value = c.SubPart.ID }).ToList();
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        //public Dictionary<string, object> GetAllUsingParamForAddEdit(int ItemId, List<string> Device_Info)
        //{
        //    try
        //    {
        //        var data = uow.Repository<SubPart>().GetData().Where(a => a.User_Deletion_Id == null)
        //            .Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
        //        CustomOption empty = new CustomOption();
        //        empty.Value = null;
        //        empty.DisplayText = "-";
        //        data.Insert(0, empty);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());

        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        public Dictionary<string, object> FillDrop_AddEDITLIST(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemPart>().GetData().Where(a => a.User_Deletion_Id == null).
                Select(c => new CustomOptionLongId
                {
                    DisplayText =

                (lang == "1" ? c.Item.Name_Ar + "/" +
                c.SubPart.Name_Ar : c.Item.Name_En + "/" +
                c.SubPart.Name_En)
                    ,
                    Value = c.ID
                }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> FillDrop_ByProduct(int Item_Type_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<SubPart>().GetData().Where(a => a.User_Deletion_Id == null && a.Item_Type_ID == Item_Type_ID)
                .Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID// fz 31-10-2019   c.SubPart.ID
                }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> FillDrop_Allowed(bool allowed, int ItemId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemPart>().GetData().Where(a => a.User_Deletion_Id == null
            && a.Item_ID == ItemId && a.IsAllowed == allowed).
            Select(c => new CustomOptionLongId
            {
                DisplayText =
            (lang == "1" ? c.SubPart.Name_Ar : c.SubPart.Name_En),
                Value = c.ID// fz 31-10-2019 c.SubPart_ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //************************************************************//
        public Dictionary<string, object> FillDrop_Add(Int64 ItemId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemPart>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_ID == ItemId).Select(c => new CustomOptionLongId
            {
                //change display lang
                DisplayText = (lang == "1" ? c.SubPart.Name_Ar : c.SubPart.Name_En),
                //DisplayText = c.SubPart.Name_Ar, 
                Value = c.ID
            }).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> SubPart_ByItem(long ItemId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemPart>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_ID == ItemId).Select(c => new CustomOptionLongId
            {
                //change display lang
                DisplayText = (lang == "1" ? c.SubPart.Name_Ar : c.SubPart.Name_En),
                Value = c.SubPart.ID
            }).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> SubPartId_ByItemPart(long ItemPartId, List<string> Device_Info)
        {
            var data = uow.Repository<ItemPart>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.ID == ItemPartId).SingleOrDefault().SubPart_ID;

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }


        // Mahmoud Saber ...
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
        public Dictionary<string, object> GetAll(int ItemID, string arName, string enName, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<ItemPart>();
                Int64 data_Count = 0;
                data = uow.Repository<ItemPart>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Item.User_Deletion_Id == null
           && p.SubPart.User_Deletion_Id == null).ToList();
                //Complete Code
                var dataDto = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<ItemPart, ItemPartDTO>);

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

    }
}


