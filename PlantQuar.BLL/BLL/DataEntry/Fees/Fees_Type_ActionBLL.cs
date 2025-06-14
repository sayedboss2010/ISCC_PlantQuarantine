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
using PlantQuar.DTO.DTO.DataEntry.Fees;

namespace PlantQuar.BLL.BLL.DataEntry.Fees
{
  public  class Fees_Type_ActionBLL : IGenericBLL<Fees_Type_ActionDTO>
    {
        private UnitOfWork uow;

        public Fees_Type_ActionBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {

                Fees_Type_Action entity = uow.Repository<Fees_Type_Action>().Findobject(Id);
                var empDTO = Mapper.Map<Fees_Type_Action, Fees_Type_ActionDTO>(entity);
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

            var count = uow.Repository<Fees_Type_Action>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);

        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {

            try
            {
                var data = new List<Fees_Type_Action>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Fees_Type_Action>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<Fees_Type_Action>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Fees_Type_Action, Fees_Type_ActionDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }



            //try
            //{
            //    string lang = Device_Info[2];
            //    var data = uow.Repository<Fees_Type_Action>().GetData().Where(p => p.User_Deletion_Id == null).
            //        OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
            //    //var data = uow.Repository<AnalysisLab>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
            //    var dataDTO = data.Select(Mapper.Map<Fees_Type_Action, Fees_Type_ActionDTO>);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);

            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<Fees_Type_Action>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Fees_Type_Action>().GetData().Where(a =>
                      a.Name_En.StartsWith(enName.Trim()) &&
                   a.User_Deletion_Id == null).ToList();

                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = data.Where(a => a.Name_Ar.StartsWith(arName.Trim())).ToList();

                    data = uow.Repository<Fees_Type_Action>().GetData().Where(a =>
                        a.Name_Ar.StartsWith(arName.Trim()) &&
                     a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Fees_Type_Action>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                    data_Count = data.Count();
                }
                else
                {
                    data = data.Where(a => (a.Name_Ar.StartsWith(arName) || a.Name_En.StartsWith(enName))).ToList();
                    data = uow.Repository<Fees_Type_Action>().GetData().Where(a =>
                    (a.Name_Ar.StartsWith(arName.Trim()) && a.Name_En.StartsWith(enName.Trim())) &&
                  a.User_Deletion_Id == null).ToList();
                }
                string lang = Device_Info[2];

                var dataDto = data.OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).Select(Mapper.Map<Fees_Type_Action, Fees_Type_ActionDTO>);

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
                dic.Add("FeesType_Data", dataDto);

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
                var Cmodel = uow.Repository<Fees_Type_Action>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Fees_Type_Action>().Update(Cmodel);
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

        public bool GetAny(Fees_Type_ActionDTO obj)
        {
            return uow.Repository<Fees_Type_Action>().GetAny(p => (p.User_Deletion_Id == null &&
                                         (p.Name_Ar == obj.Name_Ar.Trim() || p.Name_En == obj.Name_En.Trim())) && (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        //******************************************//
        public Dictionary<string, object> Insert(Fees_Type_ActionDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Byte("FeesType_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;
                    var CModel = Mapper.Map<Fees_Type_Action>(entity);
                    uow.Repository<Fees_Type_Action>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(Fees_Type_ActionDTO obj, List<string> Device_Info)
        {
            try
            {
                obj.Name_Ar = obj.Name_Ar.Trim();
                obj.Name_En = obj.Name_En.Trim();

                if (!GetAny(obj))
                {
                    Fees_Type_Action CModel = uow.Repository<Fees_Type_Action>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;

                    obj.User_Updation_Date = CModel.User_Updation_Date;
                    obj.User_Updation_Id = CModel.User_Updation_Id;

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Fees_Type_Action>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Fees_Type_Action, Fees_Type_ActionDTO>(Co);
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

        // ADD FUNCTIONS TO FILL DROPS
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Fees_Type_Action>().GetData().Where(lab => lab.User_Deletion_Id == null).
                Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),

                    Value = c.ID
                }).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            // 
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Fees_Type_Action>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            {
                //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData,
                data.OrderBy(a => a.DisplayText).ToList());

        }
        //public Dictionary<string, object> FillProcess_Add(List<string> Device_Info)
        //{
        //    string lang = Device_Info[2];
        //    var data = uow.Repository<Fees_process>().GetData().
        //        Select(c => new CustomOption
        //        {
        //            //change display lang
        //            DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),

        //            Value = c.ID
        //        }).ToList(); ;
        //    //set default value fz 17-4-2019
        //    data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
        //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        //    // 
        //}

        public Dictionary<string, object> FillFees_Process(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOption>();
                data = uow.Repository<Fees_process>().GetData().Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();

                //set default value fz 17-4-2019
                data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}