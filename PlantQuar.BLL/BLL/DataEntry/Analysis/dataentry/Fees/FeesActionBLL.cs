using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.DataEntry.Fees
{
    public class FeesActionBLL : IGenericBLL<Fees_ActionDTO>
    {
        private UnitOfWork uow;

        public FeesActionBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {



            try
            {
                var data = new List<Fees_Action>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Fees_Action>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).ToList();
                }
                else
                {
                    data = uow.Repository<Fees_Action>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Fees_Action, Fees_ActionDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }


            //try
            //{
            //    var dataDTO = new List<Fees_ActionDTO>();
            //    string lang = Device_Info[2];

            //    if (pageSize == -1 && index == -1)
            //    {
            //        dataDTO = uow.Repository<Fees_ActionDTO>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).Select(a => new Fees_ActionDTO
            //        {
            //            Name_Ar = a.Name_Ar,
            //            Name_En = a.Name_En,
            //            Amount = a.Amount,
            //            MinAmount = a.MinAmount,
            //            WeightFrom = a.WeightFrom,
            //            WeightTo = a.WeightTo,
            //            IsPaidBefore = a.IsPaidBefore,
            //            IsActive = a.IsActive,
            //            IsMandatory = a.IsMandatory,
            //        }).ToList();
            //    }
            //    else
            //    {
            //        dataDTO = uow.Repository<Fees_Action>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Name_Ar : A.Name_En)).
            //            Select(a => new Fees_ActionDTO
            //            {
            //                Name_Ar = a.Name_Ar,
            //                Name_En = a.Name_En,
            //                Amount = a.Amount,
            //                MinAmount = a.MinAmount,
            //                WeightFrom = a.WeightFrom,
            //                WeightTo = a.WeightTo,
            //                IsPaidBefore = (bool)a.IsPaidBefore,
            //                IsActive = a.IsActive,
            //                IsMandatory = a.IsMandatory,
            //            }).Skip(index).Take(pageSize).ToList();
            //    }

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

                var data = new List<Fees_Action>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Fees_Action>().GetData().Where(a => a.User_Deletion_Id == null &&
                    a.Name_En.StartsWith(enName)
                 && a.User_Deletion_Id == null).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Fees_Action>().GetData().Where(a => a.User_Deletion_Id == null &&
                                      a.Name_Ar.StartsWith(arName)  // get undeleted parent
                                   && a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Fees_Action>().GetData().Where(a => a.User_Deletion_Id == null
                                   // get undeleted parent
                                   && a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Fees_Action>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Name_Ar.StartsWith(arName) && a.Name_En.StartsWith(enName))
                                  // get undeleted parent
                                  && a.User_Deletion_Id == null).ToList();
                }
                switch (jtSorting)
                {
                    case "Ar_Name ASC":
                        data = data.OrderBy(t => t.Name_Ar).ToList();
                        break;
                    case "Ar_Name DESC":
                        data = data.OrderByDescending(t => t.Name_Ar).ToList();
                        break;
                    case "En_Name ASC":
                        data = data.OrderBy(t => t.Name_En).ToList();
                        break;
                    case "En_Name DESC":
                        data = data.OrderByDescending(t => t.Name_En).ToList();
                        break;


                }
                string lang = Device_Info[2];
                //var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Fees_Action_Data", data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool GetAny(Fees_ActionDTO entity)
        {
            var obj = entity as Fees_ActionDTO;
            return uow.Repository<Fees_Action>().GetAny(p => p.User_Deletion_Id == null && (obj.ID == 0 ? true : p.ID != obj.ID) &&
                                        (p.Name_Ar == obj.Name_Ar ));
        }

        public Dictionary<string, object> Insert(Fees_ActionDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Fees_Action_seq");
                    entity.ID = id;
                    var CModel = Mapper.Map<Fees_Action>(entity);
                    if (entity.FeesType_Id == 2)
                        CModel.Item_Shift_Treatment_ID = entity.TreatmentMethodsId;
                    else if (entity.FeesType_Id == 3)
                        CModel.Item_Shift_Treatment_ID = entity.ShiftTimingId;
                    else if (entity.FeesType_Id == 5)
                        CModel.Item_Shift_Treatment_ID = entity.ItemId;
                    else if (entity.FeesType_Id == 6)
                        CModel.Item_Shift_Treatment_ID = entity.ItemShortNameId;
                    else
                        CModel.Item_Shift_Treatment_ID = null;
                    uow.Repository<Fees_Action>().InsertRecord(CModel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
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
        public Dictionary<string, object> Update(Fees_ActionDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as Fees_ActionDTO;
                    Fees_Action CModel = uow.Repository<Fees_Action>().Findobject(obj.ID);

                    if (entity.FeesType_Id == 2)
                        obj.Item_Shift_Treatment_ID = entity.TreatmentMethodsId;
                    else if (entity.FeesType_Id == 3)
                        obj.Item_Shift_Treatment_ID = entity.ShiftTimingId;
                    else if (entity.FeesType_Id == 5)
                        obj.Item_Shift_Treatment_ID = entity.ItemId;
                    else if (entity.FeesType_Id == 6)
                        obj.Item_Shift_Treatment_ID = entity.ItemShortNameId;
                    else
                        obj.Item_Shift_Treatment_ID = null;

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Fees_Action>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Fees_Action, Fees_ActionDTO>(Co);
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

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Fees_Action>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Fees_Action>().Update(Cmodel);
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


        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillFees_Process(List<string> Device_Info)
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

        public Dictionary<string, object> FillFees_Type(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = new List<CustomOption>();
            data = uow.Repository<FeesType>().GetData().Where(g => g.User_Deletion_Id == null&&g.ID!= 3
            ).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();

            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }

        public Dictionary<string, object> FillFees_Type_Action(int Fees_Process_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = new List<CustomOption>();
            data = uow.Repository<Fees_Type_Action>().GetData().Where(g => g.User_Deletion_Id == null && g.Fees_process_ID == Fees_Process_ID
            ).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();

            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }

        public Dictionary<string, object> FillItems(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = new List<CustomOptionLongId>();
            data = uow.Repository<Item>().GetData().Where(g => g.User_Deletion_Id == null
            ).Select(c => new CustomOptionLongId
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();

            //set default value fz 17-4-2019
            data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }

        public Dictionary<string, object> FillShiftTiming(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = new List<CustomOption>();
            data = uow.Repository<ShiftTiming>().GetData().Where(g => g.User_Deletion_Id == null
            ).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();

            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }
        public Dictionary<string, object> FillTreatmentMethods(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = new List<CustomOption>();
            data = uow.Repository<TreatmentMethod>().GetData().Where(g => g.User_Deletion_Id == null
            ).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();

            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
        }

        public Dictionary<string, object> GetById(long id, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Fees_Action>().GetData().Include(f=>f.Fees_Type_Action).Where(a => a.ID == id).Select(a => new Fees_ActionDTO()
                {
                    Feer_Type_Action_ID = a.Feer_Type_Action_ID,
                    FeesType_Id = a.FeesType_Id,
                    Fees_Process_ID= a.Fees_Type_Action.Fees_process_ID,
                    TreatmentMethodsId = a.Item_Shift_Treatment_ID,
                    Name_Ar = a.Name_Ar,
                    Name_En = a.Name_En,
                    Amount = a.Amount,
                    MinAmount = a.MinAmount,
                    WeightFrom = a.WeightFrom,
                    WeightTo = a.WeightTo,
                    IsPaidBefore = (bool) a.IsPaidBefore,
                    IsActive = a.IsActive,
                    IsMandatory = a.IsMandatory,

                }).SingleOrDefault();
                if (data.FeesType_Id == 2)
                    data.TreatmentMethodsId = data.TreatmentMethodsId;
                else if (data.FeesType_Id == 3)
                    data.ShiftTimingId = data.TreatmentMethodsId;
                else if (data.FeesType_Id == 5)
                    data.ItemId = data.TreatmentMethodsId;
                else if (data.FeesType_Id == 6)
                {
                    data.ItemShortNameId = data.TreatmentMethodsId;
                    var itemId = uow.Repository<Item_ShortName>().GetData().FirstOrDefault(s => s.ID == data.TreatmentMethodsId).Item_ID;
                    data.ItemId = itemId;
                }
                   

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
