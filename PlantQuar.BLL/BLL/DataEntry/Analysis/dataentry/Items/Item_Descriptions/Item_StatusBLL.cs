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
    public class Item_StatusBLL : IGenericBLL<Item_StatusDTO>
    {
        private UnitOfWork uow;
        public Item_StatusBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Item_Status entity = uow.Repository<Item_Status>().Findobject(Id);
                var empDTO = Mapper.Map<Item_Status, Item_StatusDTO>(entity);
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
            var count = uow.Repository<Item_Status>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<Item_Status>();
               


                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Item_Status>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<Item_Status>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }
                //      var data = uow.Repository<Item_Status>().GetData().Where(a => a.User_Deletion_Id == null).
                //          OrderBy(c => (lang == "1" ? c.Ar_Name : c.En_Name)
                //).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<Item_Status, Item_StatusDTO>);
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
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Item_Status>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item_Status>().GetData().Where(a => a.En_Name.StartsWith(enName)
                      && a.User_Deletion_Id == null).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item_Status>().GetData().Where(a => a.Ar_Name.StartsWith(arName)
                                          && a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item_Status>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Item_Status>().GetData().Where(a => (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))
                                          && a.User_Deletion_Id == null).ToList();
                }

                string lang = Device_Info[2];
                var dataDto = data.OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).Select(Mapper.Map<Item_Status, Item_StatusDTO>);

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Item_Status_Data", dataDto);

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
                var Cmodel = uow.Repository<Item_Status>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Item_Status>().Update(Cmodel);
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

        public bool GetAny(Item_StatusDTO entity)
        {
            var obj = entity as Item_StatusDTO;
            obj.Ar_Name = obj.Ar_Name.Trim();
            obj.En_Name = obj.En_Name.Trim();
            return uow.Repository<Item_Status>().GetAny(p => (p.User_Deletion_Id == null
            && p.Item_Type_ID == obj.Item_Type_ID
            && (p.Ar_Name == obj.Ar_Name || p.En_Name == obj.En_Name)) 
            && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(Item_StatusDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {

                    var ListDD = entity.ListItem_Type_ID;
                    foreach (var item1 in ListDD)
                    {
                        var CModel = Mapper.Map<Item_Status>(entity);
                        CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Int("Item_Status_Seq");
                        CModel.Item_Type_ID = item1.Value;
                        var CreatedModel = uow.Repository<Item_Status>().InsertReturn(CModel);
                        uow.SaveChanges();
                    }
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                }
                //if (!GetAny(entity))
                //{
                //    var CModel = Mapper.Map<Item_Status>(entity);
                //    CModel.Ar_Name = CModel.Ar_Name.Trim();
                //    CModel.En_Name = CModel.En_Name.Trim();
                //    CModel.Item_Type_ID = CModel.Item_Type_ID;
                //    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Int("Item_Status_Seq");

                //    uow.Repository<Item_Status>().InsertRecord(CModel);
                //    uow.SaveChanges();
                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                //}
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
        public Dictionary<string, object> Update(Item_StatusDTO entity, List<string> Device_Info)
        {
            try
            {
                //if (!GetAny(entity))
                //{
                //    var obj = entity as Item_StatusDTO;
                //    Item_Status CModel = uow.Repository<Item_Status>().Findobject(obj.ID);

                //    obj.User_Creation_Date = CModel.User_Creation_Date;
                //    obj.User_Creation_Id = CModel.User_Creation_Id;

                //    if (CModel.User_Updation_Id != null)
                //    {
                //        obj.User_Updation_Date = CModel.User_Updation_Date;
                //        obj.User_Updation_Id = CModel.User_Updation_Id;
                //    }

                //    var Co = Mapper.Map(obj, CModel);
                //    Co.Ar_Name = Co.Ar_Name.Trim();
                //    Co.En_Name = Co.En_Name.Trim();
                //    Co.Item_Type_ID = Co.Item_Type_ID;
                //    uow.Repository<Item_Status>().Update(Co);
                //    uow.SaveChanges();

                //    var empDTO = Mapper.Map<Item_Status, Item_StatusDTO>(Co);
                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                //}
                //else
                //{
                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                //}

                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as Item_StatusDTO;
                    Item_Status CModel = uow.Repository<Item_Status>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    obj.User_Updation_Date = CModel.User_Updation_Date;
                    obj.User_Updation_Id = CModel.User_Updation_Id;
                    
                    var Co = Mapper.Map(obj, CModel);
                    Co.Ar_Name = Co.Ar_Name.Trim();
                    Co.En_Name = Co.En_Name.Trim();
                    uow.Repository<Item_Status>().Update(Co);
                    uow.SaveChanges();

                    //var ListDD = entity.ListItem_Type_ID;
                    //foreach (var item1 in ListDD)
                    //{
                    //    var CModel = Mapper.Map<Item_Status>(entity);
                    //    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Int("Item_Status_Seq");
                    //    CModel.Item_Type_ID = item1.Value;
                    //    var CreatedModel = uow.Repository<Item_Status>().InsertReturn(CModel);
                    //    uow.SaveChanges();
                    //}
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

        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item_Status>().GetData().Where(lab => lab.User_Deletion_Id == null).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item_Status>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //// sayed 8-2020
        public Dictionary<string, object> FillPartTypeDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item_Status>().GetData().Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillPartTypeDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Item_Status>().GetData().Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

    }

}
