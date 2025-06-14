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

    public class PlantBLL : IGenericBLL<PlantDTO>
    {
        private UnitOfWork uow;

        public PlantBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Item entity = uow.Repository<Item>().Findobject(Id);
                var empDTO = Mapper.Map<Item, PlantDTO>(entity);
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
             .Select(p => new PlantDTO
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
                 User_Updation_Id = p.User_Updation_Id,
                 User_Updation_Date = p.User_Updation_Date,
                 User_Deletion_Id = p.User_Deletion_Id,
                 User_Deletion_Date = p.User_Deletion_Date,
                 User_Creation_Id = p.User_Creation_Id,
                 User_Creation_Date = p.User_Creation_Date
                 ,
                 ListPlantPartType_Id = p.ItemParts.Where(c => c.Item_ID == p.ID).Select(c => c.SubPart_ID).ToList()
             }
                 ).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                //  var dataDTO = data.Select(Mapper.Map<Plant, PlantDTO>);
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
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Item>();
                Int64 data_Count = 0;


                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item>().GetData().Where(p => p.Name_En.StartsWith(enName)
                     && p.ID != 0   //empty row
                    && p.User_Deletion_Id == null
                // get undeleted parent
                && p.Family.User_Deletion_Id == null
                && p.Group.User_Deletion_Id == null).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item>().GetData().Where(p => p.Name_Ar.StartsWith(arName)
                                     && p.User_Deletion_Id == null
                                 // get undeleted parent
                                 && p.Family.User_Deletion_Id == null
                                 && p.Group.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Item>().GetData().Where(p => p.User_Deletion_Id == null
                                 // get undeleted parent
                                 && p.Family.User_Deletion_Id == null
                                 && p.Group.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Item>().GetData().Where(p => (p.Name_Ar.StartsWith(arName) && p.Name_En.StartsWith(enName))
                                     && p.User_Deletion_Id == null
                                 // get undeleted parent
                                 && p.Family.User_Deletion_Id == null
                                 && p.Group.User_Deletion_Id == null).ToList();
                }
                var dataDto = data.Select(p => new PlantDTO
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
                    ForbiddenReason = p.ForbiddenReason,
                    IsForbidden = p.IsForbidden,
                    User_Updation_Id = p.User_Updation_Id,
                    User_Updation_Date = p.User_Updation_Date,
                    User_Deletion_Id = p.User_Deletion_Id,
                    User_Deletion_Date = p.User_Deletion_Date,
                    User_Creation_Id = p.User_Creation_Id,
                    User_Creation_Date = p.User_Creation_Date ,
                    IsPlantInEgypt=p.IsPlantInEgypt,
                    ListPlantPartType_Id = p.ItemParts.Where(c => c.Item_ID == p.ID).Select(c => c.SubPart_ID).ToList()
                }
                 ).ToList().OrderBy(A => A.ID).Skip(index).Take(pageSize);//.Select(Mapper.Map<Plant, PlantDTO>);

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
                data_Count = data.Count();
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

        public bool GetAny(PlantDTO entity)
        {
            var obj = entity;// as PlantDTO;
            obj.Name_Ar = obj.Name_Ar.Trim();
            obj.Name_En = obj.Name_En.Trim();
            return uow.Repository<Item>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(PlantDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Item>(entity);
                    CModel.Name_Ar = CModel.Name_Ar.Trim();
                    CModel.Name_En = CModel.Name_En.Trim();
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Plant_seq");
                    CModel = uow.Repository<Item>().InsertReturn(CModel);
                    uow.SaveChanges();

                    //#region Plant Parts
                    //PlantPartBLL plantPart_Model = new PlantPartBLL();
                    //plantPart_Model.InsertRecords(CModel.ID, entity.User_Creation_Id, entity.User_Creation_Date,
                    //         entity.ListPlantPartType_Id, Device_Info);
                    //#endregion
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
        public Dictionary<string, object> Update(PlantDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as PlantDTO;
                    Item CModel = uow.Repository<Item>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    obj.User_Updation_Date = CModel.User_Updation_Date;
                    obj.User_Updation_Id = CModel.User_Updation_Id;
                    if (entity.Picture == null)
                    {
                        //get old one
                        entity.Picture = CModel.Picture;
                    }
                    var Co = Mapper.Map(obj, CModel);
                    Co.Name_Ar = Co.Name_Ar.Trim();
                    Co.Name_En = Co.Name_En.Trim();
                    uow.Repository<Item>().Update(Co);
                    uow.SaveChanges();

                    #region Plant Parts
                    PlantPartBLL plantPart_Model = new PlantPartBLL();
                    plantPart_Model.UpdateRecords(entity.ID, entity.User_Creation_Id, entity.User_Creation_Date,
                             entity.ListPlantPartType_Id, Device_Info);
                    #endregion

                    var empDTO = Mapper.Map<Item, PlantDTO>(Co);
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
            var data = uow.Repository<Item>().GetData().Select(c => new CustomOptionLongId
            {
                //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            //   data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
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

        //SARA
        public Dictionary<string, object> Fill_NotForbiddenPlants(List<string> Device_Info)
        {
            string lang = Device_Info[2];
         
          
            
            var data = uow.Repository<Item>().GetData()
               
                .Select(c => new CustomOptionLongId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            //set default value fz 17-4-2019
               data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = 0 });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> Fill_NotForbidden_In_Item_ShortName(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            PlantQuarantineEntities db = new PlantQuarantineEntities();
            var result = (from i in db.Items
                          join ish in db.Item_ShortName on i.ID equals ish.Item_ID
                          select new CustomOptionLongId
                          {
                              DisplayText = (lang == "1" ? i.Name_Ar : i.Name_En),
                              Value = i.ID
                          }).Distinct().ToList();


            //set default value fz 17-4-2019
            result.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = 0 });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, result.OrderBy(a => a.DisplayText).ToList());
        }
        //END SARA
    }
}
