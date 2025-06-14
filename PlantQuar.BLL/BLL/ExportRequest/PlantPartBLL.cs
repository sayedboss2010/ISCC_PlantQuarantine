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
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.ExportRequest
{

    public class PlantPartBLL : IGenericBLL<PlantPartDTO>
    {
        private UnitOfWork uow;

        public PlantPartBLL()
        {

            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
               ItemPart entity = uow.Repository<ItemPart>().Findobject(Id);
                var empDTO = Mapper.Map<ItemPart, PlantPartDTO>(entity);
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
                var dataDTO = data.Select(Mapper.Map<ItemPart, PlantPartDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAllByPlantID(Int64 PlantID, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<ItemPart>();
                Int64 data_Count = 0;
                
                data = uow.Repository<ItemPart>().GetData().Where(p => p.User_Deletion_Id == null && p.Item_ID == PlantID)
                .OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<ItemPart, PlantPartDTO>);

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("PlantPart_Data", dataDTO);

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
                var dataDto = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<ItemPart, PlantPartDTO>);

                data_Count = data.Count();
              
                dic.Add("Count_Data", data_Count);
                dic.Add("PlantPart_Data", dataDto);

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

        //public bool GetAny(T entity)
        //{
        //    //Complete Code
        //    return false;
        //}
        public bool GetAny(PlantPartDTO entity)
        {
            var obj = entity as PlantPartDTO;
           
            return uow.Repository<ItemPart>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.SubPart_ID == obj.PlantPartType_ID && p.Item_ID == obj.Plant_ID)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }       

        //******************************************//
        public Dictionary<string, object> Insert(PlantPartDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<ItemPart>(entity);
                    CModel.ID = uow.Repository<object>().GetNextSequenceValue_Long("PlantPart_seq");
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


        public Dictionary<string, object> InsertRecords(long Plant_ID, short user_id, DateTime Date_Now, List<int> objRecords, List<string> Device_Info)
        {
            try
            {
                PlantPartDTO dto;
                foreach (var item in objRecords)
                {
                    dto = new PlantPartDTO();
                    dto.PlantPartType_ID = item;
                    dto.Plant_ID = Plant_ID;
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

        public Dictionary<string, object> UpdateRecords(long Plant_ID, short user_id, DateTime Date_Now, List<int> objRecords, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<ItemPart>().GetData().Where(x => x.Item_ID == Plant_ID && x.User_Deletion_Id == null).ToList();
                var addlst = objRecords.Except(data.Select(x => x.SubPart_ID)).ToList();
                var deletelst = data.Where(x => objRecords.IndexOf(x.SubPart_ID) == -1).ToList();
                InsertRecords(Plant_ID, user_id, Date_Now, addlst, Device_Info);
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


        public Dictionary<string, object> Update(PlantPartDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as PlantPartDTO;
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

                    var empDTO = Mapper.Map<ItemPart, PlantPartDTO>(Co);
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

        //public Dictionary<string, object> PlantPart_List()
        //{
        //    var data = uow.Repository<ItemPart>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOptionLongId { DisplayText = c.SubPart.Name_Ar, Value = c.ID }).ToList();
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        //}

        //public Dictionary<string, object> GetAllUsingParamForList(int PlantId, List<string> Device_Info)
        //{
        //    try
        //    {
        //        var data = uow.Repository<ItemPart>().GetData().Where(a => a.User_Deletion_Id == null &&
        //        a.Plant_ID == PlantId)
        //            .Select(c => new CustomOption { DisplayText = c.SubPart.Name_Ar, Value = c.SubPart.ID }).ToList();
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        //public Dictionary<string, object> GetAllUsingParamForAddEdit(int PlantId, List<string> Device_Info)
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
                Select(c => new CustomOptionLongId { DisplayText =

                (lang == "1" ?   c.Item.Name_Ar + "/" + 
                c.SubPart.Name_Ar : c.Item.Name_En + "/" +
                c.SubPart.Name_En)
                    ,
                Value = c.ID }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> FillDrop_ByProduct(int plantid , List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<ItemPart>().GetData().Where(a => a.User_Deletion_Id == null && a.Item_ID == plantid)
                .Select(c => new CustomOptionLongId {
                    //change display lang
                    DisplayText = (lang == "1" ?   c.SubPart.Name_Ar:c.SubPart.Name_En),
                    Value =  c.ID// fz 31-10-2019   c.SubPart.ID
                }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> FillDrop_Allowed(bool allowed, int plantId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemPart>().GetData().Where(a => a.User_Deletion_Id == null 
            && a.Item_ID == plantId && a.IsAllowed == allowed).
            Select(c => new CustomOptionLongId { DisplayText =
            (lang == "1" ? c.SubPart.Name_Ar : c.SubPart.Name_En),
                 Value = c.ID// fz 31-10-2019 c.PlantPartType_ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //************************************************************//
        public Dictionary<string, object> FillDrop_Add(Int64 plantId, List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<ItemPart>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_ID == plantId).Select(c => new CustomOptionLongId {
                //change display lang
                DisplayText = (lang == "1" ? c.SubPart.Name_Ar : c.SubPart.Name_En),
                //DisplayText = c.SubPart.Name_Ar, 
                Value = c.ID }).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> PlantPartType_ByPlant(long plantId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ItemPart>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.Item_ID == plantId).Select(c => new CustomOptionLongId {
                //change display lang
                DisplayText = (lang == "1" ?   c.SubPart.Name_Ar:c.SubPart.Name_En),
                Value = c.SubPart.ID }).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> PlantPartTypeId_ByPlantPart(long plantPartId, List<string> Device_Info)
        {
            var data = uow.Repository<ItemPart>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.ID == plantPartId).SingleOrDefault().SubPart_ID;

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_List(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item_Status>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_AddEdit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item_Status>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            {  //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
    }
}