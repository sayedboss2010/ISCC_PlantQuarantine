using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Treatments;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

//AG
namespace PlantQuar.BLL.BLL.DataEntry.Treatments
{

    public class TreatmentMethodBLL : IGenericBLL<TreatmentMethodDTO>
    {
        private UnitOfWork uow;

        public TreatmentMethodBLL()
        {
            uow = new UnitOfWork();
        }

        //Find TreatmentMethod 
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                TreatmentMethod entity = uow.Repository<TreatmentMethod>().Findobject(Id);
                var empDTO = Mapper.Map<TreatmentMethod, TreatmentMethodDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Count TreatmentMethod
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<TreatmentMethod>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.TreatmentType.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }

        //Get List TreatmentMethod
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = new List<TreatmentMethod>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<TreatmentMethod>().GetData().Where(a => a.User_Deletion_Id == null && a.TreatmentType.User_Deletion_Id == null).
                        OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<TreatmentMethod>().GetData().Where(a => a.User_Deletion_Id == null && a.TreatmentType.User_Deletion_Id == null).
                        OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }
              
                var dataDTO = data.Select(Mapper.Map<TreatmentMethod, TreatmentMethodDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAllBy_TreatmentType(int TreatmentType_ID, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<TreatmentMethod>().GetData().Where(p => p.User_Deletion_Id == null
                && p.TreatmentType_ID == TreatmentType_ID && p.IsActive==true
               // get undeleted parent
               && p.TreatmentType.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<TreatmentMethod, TreatmentMethodDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get List TreatmentMethod by ArName or EnName
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                var data = new List<TreatmentMethod>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<TreatmentMethod>().GetData().Where(a => a.User_Deletion_Id == null &&
                                             a.En_Name.StartsWith(enName)
               // get undeleted parent
               && a.TreatmentType.User_Deletion_Id == null).ToList();

                    data_Count = data.Count();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<TreatmentMethod>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Ar_Name.StartsWith(arName)
               // get undeleted parent
               && a.TreatmentType.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<TreatmentMethod>().GetData().Where(a => a.User_Deletion_Id == null
               // get undeleted parent
               && a.TreatmentType.User_Deletion_Id == null).ToList();

                    data_Count = data.Count();
                }
                else
                {
                    data = uow.Repository<TreatmentMethod>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))
               // get undeleted parent
               && a.TreatmentType.User_Deletion_Id == null).ToList();

                    data_Count = data.Count();
                }
                string lang = Device_Info[2];
                var dataDTO = data.OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).Select(Mapper.Map<TreatmentMethod, TreatmentMethodDTO>);

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
                dic.Add("TreatmentMethod_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Any TreatmentMethod
        public bool GetAny(TreatmentMethodDTO entity)
        {
            var obj = entity;
            return uow.Repository<TreatmentMethod>().GetAny(p => (p.User_Deletion_Id == null && (p.Ar_Name == obj.Ar_Name || p.En_Name == obj.En_Name)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//

        //Create TreatmentMethod
        public Dictionary<string, object> Insert(TreatmentMethodDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("TreatmentMethods_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;
                    var CModel = Mapper.Map<TreatmentMethod>(entity);
                    uow.Repository<TreatmentMethod>().InsertRecord(CModel);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Update TreatmentMethod
        public Dictionary<string, object> Update(TreatmentMethodDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity;
                    TreatmentMethod CModel = uow.Repository<TreatmentMethod>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<TreatmentMethod>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<TreatmentMethod, TreatmentMethodDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }


        //Delete TreatmentMethod
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)

        {
            try
            {
                var Cmodel = uow.Repository<TreatmentMethod>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<TreatmentMethod>().Update(Cmodel);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //ADD FUNCTIONS TO FILL DROPS        

        //Get TreatmentMethod List DropDownList
        public Dictionary<string, object> GetTreatmentMethodByTypeId(int TreatmentType_ID)
        {
            var data = uow.Repository<TreatmentMethod>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive==true && a.TreatmentType_ID == TreatmentType_ID)
                .Select(c => new CustomOption { DisplayText = c.Ar_Name, Value = c.ID }).ToList();
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        //public Dictionary<string, object> FillMethod_List()
        //{
        //    var data = uow.Repository<TreatmentMethod>().GetData().Where(lab => lab.User_Deletion_Id == null)
        //        .Select(c => new CustomOptionShortId { DisplayText = c.Ar_Name, Value = c.ID }).ToList();
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        //}
        public Dictionary<string, object> FillMethod_List(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<TreatmentMethod>().GetData().Where(lab => lab.User_Deletion_Id == null)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        //Get TreatmentType List DropDownList
        public Dictionary<string, object> FillDrop_List(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<TreatmentType>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.IsActive == true)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        //Get TreatmentType Create & Update DropDownList
        public Dictionary<string, object> FillDrop_AddEdit(List<string> Device_Info)
        {
             string lang = Device_Info[2];
            var data = uow.Repository<TreatmentType>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true)
                .Select(c => new CustomOption
                {  //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        //nora
        public Dictionary<string, object> GetID_TreatmentType(int ID_TreatmentType, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<TreatmentMethod>().GetData().Where(p => p.User_Deletion_Id == null
                && p.TreatmentType_ID == ID_TreatmentType
               // get undeleted parent
               && p.TreatmentType.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<TreatmentMethod, TreatmentMethodDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }



     
        public Dictionary<string, object> GetAll(long TreatmentMethods_ID, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                Int64 data_Count = 0;
                var data = uow.Repository<TreatmentMaterial>().GetData().Where(p => p.User_Deletion_Id == null && p.IsActive==true
                                                                                 && p.TreatmentMethods_ID == TreatmentMethods_ID)
                    .OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                TreatmentMethodBLL treatmentmethodBLL = new TreatmentMethodBLL();
                var dataDto = data.Select(x => new TreatmentMaterialDTO
                {

                    ID = x.ID,
                    IsActive = x.IsActive,
                    User_Creation_Date = x.User_Creation_Date,
                    User_Creation_Id = x.User_Creation_Id,
                    User_Deletion_Date = x.User_Deletion_Date,
                    User_Deletion_Id = x.User_Deletion_Id,
                    User_Updation_Date = x.User_Updation_Date,
                    User_Updation_Id = x.User_Updation_Id,
                    Item_ID = x.Item_ID,
                    Family_ID = treatmentmethodBLL.GetFamily_IDByItem_ID(x.Item_ID, Device_Info),
                    //Family_ID=x.Item.Family_ID,
                    Group_ID= treatmentmethodBLL.GetGroup_IDByItem_ID(x.Item_ID, Device_Info),
                    //GovID = outletbll.GetGovIDByCenterID(x.ID, Device_Info),

                }).ToList();

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("centers_Data", dataDto);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public int GetFamily_IDByItem_ID(long? Item_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];

            int? Family_ID = uow.Repository<Item>().GetData().Where(x => x.ID == Item_ID).Select(x => x.Family_ID).FirstOrDefault();
            if (Family_ID == null)
                return 0;
            else
                return Family_ID.Value;
        }

        public int GetGroup_IDByItem_ID(long? Item_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];

            int? Group_ID = uow.Repository<Item>().GetData().Where(x => x.ID == Item_ID).Select(x => x.Group_ID).FirstOrDefault();
            if (Group_ID == null)
                return 0;
            else
                return Group_ID.Value;
        }


        //insert TreatmentMethod
        public Dictionary<string, object> InsertTreatmentMaterial(TreatmentMaterialDTO entity, List<string> Device_Info)
        {
            try
            {
                var data_Check = uow.Repository<TreatmentMaterial>().GetData().Where(
                    p => p.TreatmentMethods_ID == entity.TreatmentMethods_ID 
                    && p.Item_ID == entity.Item_ID && p.User_Deletion_Id == null && p.IsActive == true).ToList();

                if (data_Check.Count <= 0)
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("TreatmentMaterial_SEQ");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;
                    entity.IsActive = true;
                    var CModel = Mapper.Map<TreatmentMaterial>(entity);
                    uow.Repository<TreatmentMaterial>().InsertRecord(CModel);
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


        public Dictionary<string, object> UpdateTreatmentMaterial(TreatmentMaterialDTO entity, List<string> Device_Info)
        {
            try
            {
                
                    var obj = entity;
                TreatmentMaterial CModel = uow.Repository<TreatmentMaterial>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<TreatmentMaterial>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<TreatmentMaterial, TreatmentMaterialDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                
                
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }



    }
}
