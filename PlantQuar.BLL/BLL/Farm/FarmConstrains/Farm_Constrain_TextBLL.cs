using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PlantQuar.DTO.HelperClasses;
using System.Reflection;
using PlantQuar.DTO.DTO.Farm.FarmConstrain;

namespace PlantQuar.BLL.BLL.Farm.FarmConstrains
{
    public class Farm_Constrain_TextBLL //: IGenericBLL<Farm_Constrain_TextDTO>
    {
        private UnitOfWork uow;

        public Farm_Constrain_TextBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, string Constrain_Type, List<string> Device_Info)
        {
            try
            {
                switch (Constrain_Type)
                {

                    case "تعليمات":
                        Farm_Constrain_Text entity = uow.Repository<Farm_Constrain_Text>().Findobject(Id);
                        var empDTO = Mapper.Map<Farm_Constrain_Text, Farm_Constrain_TextDTO>(entity);
                        empDTO.IsMotataleb = false;
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);

                    case "متطلبات":
                        Farm_CheckList entity1 = uow.Repository<Farm_CheckList>().Findobject(Id);
                        var empDTO1 = Mapper.Map<Farm_CheckList, Farm_CheckListDTO>(entity1);
                        empDTO1.IsMotataleb = true;

                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO1);


                    default:
                        return null;
                }


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {

            var count = uow.Repository<Farm_Constrain_Text>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);

        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Farm_Constrain_Text>().GetData().Where(p => p.User_Deletion_Id == null).
                    OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En)).Skip(index).Take(pageSize).ToList();
                //var data = uow.Repository<AnalysisLab>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<Farm_Constrain_Text, Farm_Constrain_TextDTO>);
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
                Dictionary<string, object> dic = new Dictionary<string, object>();
                // var data_Farm_Constrain_Text = new List<Farm_Constrain_Text>();
                //  var data_Farm_CheckList = new List<Farm_CheckList>();
                Int64 data_Count = 0;
                #region Old code


                //if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                //{
                //    data = uow.Repository<Farm_Constrain_Text>().GetData().Where(a =>
                //      a.ConstrainText_En.StartsWith(enName.Trim()) &&
                //   a.User_Deletion_Id == null).ToList();

                //}
                //else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                //{
                //    data = data.Where(a => a.ConstrainText_Ar.StartsWith(arName.Trim())).ToList();

                //    data = uow.Repository<Farm_Constrain_Text>().GetData().Where(a =>
                //        a.ConstrainText_Ar.StartsWith(arName.Trim()) &&
                //     a.User_Deletion_Id == null).ToList();
                //}
                //else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                //{
                //    data = uow.Repository<Farm_Constrain_Text>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                //    data_Count = data.Count();
                //}
                //else
                //{
                //  data = data.Where(a => (a.ConstrainText_Ar.StartsWith(arName) || a.ConstrainText_En.StartsWith(enName))).ToList();
                //  data = uow.Repository<Farm_Constrain_Text>().GetData().Where(a =>
                //  (a.ConstrainText_Ar.StartsWith(arName.Trim()) && a.ConstrainText_En.StartsWith(enName.Trim())) &&
                //a.User_Deletion_Id == null).ToList();
                //}
                //string lang = Device_Info[2];

                //var dataDto = data.OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En)).Skip(index).Take(pageSize).Select(Mapper.Map<Farm_Constrain_Text, Farm_Constrain_TextDTO>);

                //switch (jtSorting)
                //{
                //    case "ConstrainText_Ar ASC":
                //        data = data.OrderBy(t => t.ConstrainText_Ar).ToList();
                //        break;
                //    case "ConstrainText_Ar DESC":
                //        data = data.OrderByDescending(t => t.ConstrainText_Ar).ToList();
                //        break;
                //    case "ConstrainText_En ASC":
                //        data = data.OrderBy(t => t.ConstrainText_En).ToList();
                //        break;
                //    case "ConstrainText_En DESC":
                //        data = data.OrderByDescending(t => t.ConstrainText_En).ToList();
                //        break;
                //}
                //switch (jtSorting)
                //{
                //    case "ConstrainText_Ar ASC":
                //        data = data.OrderBy(t => t.ConstrainText_Ar).ToList();
                //        break;
                //    case "ConstrainText_Ar DESC":
                //        data = data.OrderByDescending(t => t.ConstrainText_Ar).ToList();
                //        break;
                //    case "ConstrainText_En ASC":
                //        data = data.OrderBy(t => t.ConstrainText_En).ToList();
                //        break;
                //    case "ConstrainText_En DESC":
                //        data = data.OrderByDescending(t => t.ConstrainText_En).ToList();
                //        break;
                //}
                #endregion
                PlantQuarantineEntities entities = new PlantQuarantineEntities();

                var data_Farm_Constrain_Text = (from Fct in entities.Farm_Constrain_Text

                                                where Fct.User_Deletion_Id == null
                                                && Fct.IsActive == true
                                                select new Farm_Constrain_Text_CheckList_DTO
                                                {
                                                    ID = Fct.ID,
                                                    ConstrainText_Ar = Fct.ConstrainText_Ar,
                                                    ConstrainText_En = Fct.ConstrainText_En,
                                                    Description_Ar = Fct.Description_Ar,
                                                    Description_En = Fct.Description_En,
                                                    Constrain_Type = "تعليمات",
                                                    IsActive = Fct.IsActive,
                                                    User_Creation_Id = Fct.User_Creation_Id,
                                                    User_Creation_Date = Fct.User_Creation_Date,
                                                    User_Updation_Id = Fct.User_Updation_Id,
                                                    User_Updation_Date = Fct.User_Updation_Date,
                                                    User_Deletion_Id = Fct.User_Deletion_Id,
                                                    User_Deletion_Date = Fct.User_Deletion_Date,
                                                }).ToList();
                var data_Farm_CheckList = (from Fct in entities.Farm_CheckList

                                           where Fct.User_Deletion_Id == null
                                           && Fct.IsActive == true
                                           select new Farm_Constrain_Text_CheckList_DTO
                                           {
                                               ID = Fct.ID,
                                               ConstrainText_Ar = Fct.ConstrainText_Ar,
                                               ConstrainText_En = Fct.ConstrainText_En,
                                               Description_Ar = Fct.Description_Ar,
                                               Description_En = Fct.Description_En,
                                               Constrain_Type = "متطلبات",
                                               IsActive = Fct.IsActive,
                                               User_Creation_Id = Fct.User_Creation_Id,
                                               User_Creation_Date = Fct.User_Creation_Date,
                                               User_Updation_Id = Fct.User_Updation_Id,
                                               User_Updation_Date = Fct.User_Updation_Date,
                                               User_Deletion_Id = Fct.User_Deletion_Id,
                                               User_Deletion_Date = Fct.User_Deletion_Date,
                                           }).ToList();
                //data_Farm_Constrain_Text = uow.Repository<Farm_Constrain_Text>().GetData().ToList();
                //data_Farm_CheckList = uow.Repository<Farm_CheckList>().GetData().ToList();
                var union_data = data_Farm_Constrain_Text.Union(data_Farm_CheckList).ToList();

                data_Count = union_data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("Farm_Constrain_Text_Data", union_data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data_Count);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> Get_Data_All(List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                PlantQuarantineEntities entities = new PlantQuarantineEntities();

                var data_Farm_Constrain_Text = (from Fct in entities.Farm_Constrain_Text

                                                where Fct.User_Deletion_Id == null
                                                // && Fct.IsActive == true
                                                select new Farm_Constrain_Text_CheckList_DTO
                                                {
                                                    ID = Fct.ID,
                                                    ConstrainText_Ar = Fct.ConstrainText_Ar,
                                                    ConstrainText_En = Fct.ConstrainText_En,
                                                    Description_Ar = Fct.Description_Ar,
                                                    Description_En = Fct.Description_En,
                                                    Constrain_Type = "تعليمات",
                                                    IsActive = Fct.IsActive,
                                                    User_Creation_Id = Fct.User_Creation_Id,
                                                    User_Creation_Date = Fct.User_Creation_Date,
                                                    User_Updation_Id = Fct.User_Updation_Id,
                                                    User_Updation_Date = Fct.User_Updation_Date,
                                                    User_Deletion_Id = Fct.User_Deletion_Id,
                                                    User_Deletion_Date = Fct.User_Deletion_Date,
                                                }).ToList();
                var data_Farm_CheckList = (from Fct in entities.Farm_CheckList

                                           where Fct.User_Deletion_Id == null
                                           //&& Fct.IsActive == true
                                           select new Farm_Constrain_Text_CheckList_DTO
                                           {
                                               ID = Fct.ID,
                                               ConstrainText_Ar = Fct.ConstrainText_Ar,
                                               ConstrainText_En = Fct.ConstrainText_En,
                                               Description_Ar = Fct.Description_Ar,
                                               Description_En = Fct.Description_En,
                                               Constrain_Type = "متطلبات",
                                               IsActive = Fct.IsActive,
                                               User_Creation_Id = Fct.User_Creation_Id,
                                               User_Creation_Date = Fct.User_Creation_Date,
                                               User_Updation_Id = Fct.User_Updation_Id,
                                               User_Updation_Date = Fct.User_Updation_Date,
                                               User_Deletion_Id = Fct.User_Deletion_Id,
                                               User_Deletion_Date = Fct.User_Deletion_Date,
                                           }).ToList();
                //data_Farm_Constrain_Text = uow.Repository<Farm_Constrain_Text>().GetData().ToList();
                //data_Farm_CheckList = uow.Repository<Farm_CheckList>().GetData().ToList();
                var union_data = data_Farm_Constrain_Text.Union(data_Farm_CheckList).ToList();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, union_data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Delete(Farm_Constrain_Text_CheckList_DTO dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel1 = uow.Repository<Farm_Constrain_Text>().Findobject(dto.ID);
                var Cmodel2 = uow.Repository<Farm_CheckList>().Findobject(dto.ID);
                if (dto.Constrain_Type == "تعليمات")
                {

                    if (Cmodel1 != null)
                    {
                        Cmodel1.User_Deletion_Date = dto.User_Deletion_Date;
                        Cmodel1.User_Deletion_Id = dto.User_Deletion_Id;
                        uow.Repository<Farm_Constrain_Text>().Update(Cmodel1);
                        uow.SaveChanges();

                    }


                }
                else if (dto.Constrain_Type == "متطلبات")
                {

                    if (Cmodel2 != null)
                    {
                        Cmodel2.User_Deletion_Date = dto.User_Deletion_Date;
                        Cmodel2.User_Deletion_Id = dto.User_Deletion_Id;
                        uow.Repository<Farm_CheckList>().Update(Cmodel2);
                        uow.SaveChanges();
                        //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, null);
                    }
                }
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, Cmodel1);
                //else if (constrain == "متطلبات")
                //{
                //    var Cmodel = uow.Repository<Farm_CheckList>().Findobject(dto.id);
                //    if (Cmodel != null)
                //    {
                //        Cmodel.User_Deletion_Date = dto._DateNow;
                //        Cmodel.User_Deletion_Id = dto.Userid;
                //        uow.Repository<Farm_CheckList>().Update(Cmodel);
                //        uow.SaveChanges();
                //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, null);
                //    }
                //    else
                //    {
                //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                //    }
                //}
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        //{
        //    try
        //    {
        //        var Cmodel = uow.Repository<Farm_Constrain_Text>().Findobject(dto.id);
        //        if (Cmodel != null)
        //        {
        //            Cmodel.User_Deletion_Date = dto._DateNow;
        //            Cmodel.User_Deletion_Id = dto.Userid;
        //            uow.Repository<Farm_Constrain_Text>().Update(Cmodel);
        //            uow.SaveChanges();
        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.DeletedScussfuly, Cmodel);
        //        }
        //        else
        //        {
        //            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        public bool GetAny(Farm_Constrain_TextDTO obj)
        {
            return uow.Repository<Farm_Constrain_Text>().GetAny(p => (p.User_Deletion_Id == null &&
                                         (p.ConstrainText_Ar == obj.ConstrainText_Ar.Trim() || p.ConstrainText_En == obj.ConstrainText_En.Trim())) && (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        public bool GetAny2(string ConstrainText_Ar, string ConstrainText_En, long ID)
        {
            return uow.Repository<Farm_CheckList>().GetAny(p => (p.User_Deletion_Id == null &&
                                         (p.ConstrainText_Ar == ConstrainText_Ar.Trim()
                                         || p.ConstrainText_En == ConstrainText_En.Trim())) && (ID == 0 ? true : p.ID != ID));

        }
        //******************************************//
        public Dictionary<string, object> Insert(Farm_Constrain_TextDTO entity, List<string> Device_Info)
        {

            try
            {
                entity.ConstrainText_Ar = entity.ConstrainText_Ar.Trim();
                entity.ConstrainText_En = entity.ConstrainText_En.Trim();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (entity.IsMotataleb == false)
                {
                    if (!GetAny(entity))
                    {
                        var id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Constrain_Text_seq");
                        entity.ID = int.Parse(id.ToString());
                        entity.ID = id;
                        var CModel = Mapper.Map<Farm_Constrain_Text>(entity);
                        uow.Repository<Farm_Constrain_Text>().InsertRecord(CModel);
                        uow.SaveChanges();
                    }
                    else
                    {
                        int x = 1;
                        dic.Add("لا يمكن ادخال هذه البيانات", x);
                      
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                    }
                }
                else
                {
                    if (!GetAny2(entity.ConstrainText_Ar, entity.ConstrainText_En, entity.ID))
                    {
                        PlantQuarantineEntities context = new PlantQuarantineEntities();
                    long _Farm_CheckList_Id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_CheckList_seq");
                    var Insert_Farm_CheckList = new Farm_CheckList
                    {
                        ID = _Farm_CheckList_Id,
                        IsActive = true,
                        ConstrainText_Ar = entity.ConstrainText_Ar,
                        ConstrainText_En = entity.ConstrainText_En,
                        Description_Ar = entity.Description_Ar,
                        Description_En = entity.Description_En,
                        User_Creation_Id = entity.User_Creation_Id,
                        User_Creation_Date = DateTime.Now,
                    };
                    context.Farm_CheckList.Add(Insert_Farm_CheckList);
                    context.SaveChanges();
                    }
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                    }
                    //context.Farm_CheckList.Add(Insert_Farm_CheckList);
                    //context.SaveChanges();
                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Update(Farm_Constrain_TextDTO obj, List<string> Device_Info)
        {
            // obj.IsUpdated = false;
            try
            {
                //  obj.ConstrainText_Ar = obj.ConstrainText_Ar.Trim();
                //  obj.ConstrainText_En = obj.ConstrainText_En.Trim();


                if (obj.IsMotataleb == false)
                {
                    if (!GetAny(obj))
                    {
                        Farm_Constrain_Text CModel = uow.Repository<Farm_Constrain_Text>().Findobject(obj.ID);

                        obj.User_Creation_Date = CModel.User_Creation_Date;
                        obj.User_Creation_Id = CModel.User_Creation_Id;

                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                        //  obj.IsUpdated = true;
                        var Co = Mapper.Map(obj, CModel);
                        uow.Repository<Farm_Constrain_Text>().Update(Co);
                        uow.SaveChanges();

                        var empDTO = Mapper.Map<Farm_Constrain_Text, Farm_Constrain_TextDTO>(Co);
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                    }
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                    }

                }
                else
                {
                    if (!GetAny2(obj.ConstrainText_Ar, obj.ConstrainText_En, obj.ID))
                    {
                        //var empDTO1 = Mapper.Map<Farm_CheckList, Farm_CheckListDTO>(entity1);
                        Farm_CheckListDTO farm_CheckListDTO = new Farm_CheckListDTO();
                        farm_CheckListDTO.IsMotataleb = obj.IsMotataleb;
                        farm_CheckListDTO.ID = obj.ID;
                        farm_CheckListDTO.ConstrainText_Ar = obj.ConstrainText_Ar;
                        farm_CheckListDTO.ConstrainText_En = obj.ConstrainText_En;
                        farm_CheckListDTO.Description_Ar = obj.Description_Ar;
                        farm_CheckListDTO.Description_En = obj.Description_En;
                        farm_CheckListDTO.IsActive = obj.IsActive;
                        // farm_CheckListDTO.IsMotataleb=obj.IsMotataleb;

                        Farm_CheckList CModel = uow.Repository<Farm_CheckList>().Findobject(obj.ID);

                        farm_CheckListDTO.User_Creation_Date = CModel.User_Creation_Date;
                        farm_CheckListDTO.User_Creation_Id = CModel.User_Creation_Id;

                        farm_CheckListDTO.User_Updation_Date = CModel.User_Updation_Date;
                        farm_CheckListDTO.User_Updation_Id = CModel.User_Updation_Id;
                        // obj.IsUpdated = true;
                        var Co = Mapper.Map(farm_CheckListDTO, CModel);
                        uow.Repository<Farm_CheckList>().Update(Co);
                        uow.SaveChanges();

                        var empDTO = Mapper.Map<Farm_CheckList, Farm_CheckListDTO>(Co);
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
                    }
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                    }
                }


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        // ADD FUNCTIONS TO FILL DROPS
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Farm_Constrain_Text>().GetData().Where(lab => lab.User_Deletion_Id == null).
                Select(c => new CustomOptionLongId
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_En),

                    Value = c.ID
                }).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            // 
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Farm_Constrain_Text>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOptionLongId
            {
                //change display lang
                DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_En),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }
    }
}
