using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.GovToVillage;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.API.Controllers.DataEntry.GovToVillage
{
    public class GovernateBLL : IGenericBLL<GovernateDTO>
    {
        private UnitOfWork uow;

        public GovernateBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Governate entity = uow.Repository<Governate>().Findobject(Id);
                var empDTO = Mapper.Map<Governate, GovernateDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Governate>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {

            ///sayed
            ///
            try
            {
                var data = new List<Governate>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    data = uow.Repository<Governate>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                }
                else
                {
                    data = uow.Repository<Governate>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                }

                var dataDTO = data.Select(Mapper.Map<Governate, GovernateDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

            //            try
            //            {
            //                string lang = Device_Info[2];
            //                var data = uow.Repository<Governate>().GetData().Where(a => a.User_Deletion_Id == null).
            //                    OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
            //                var dataDTO = data.Select(Mapper.Map<Governate, GovernateDTO>);
            //                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            //            }
            //#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            //            catch (Exception ex)
            //#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            //            {
            //                //uow.Repository<Object>().Save_Error(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            //                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                var data = new List<Governate>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Governate>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.En_Name.StartsWith(enName.Trim())).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Governate>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Ar_Name.StartsWith(arName.Trim())).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Governate>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Governate>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))).ToList();
                }
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
                string lang = Device_Info[2];
                var dataDTO = data.Skip(index).Take(pageSize).Select(Mapper.Map<Governate, GovernateDTO>);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Governate_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                //  uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(GovernateDTO obj)
        {
            // var obj = entity as GovernateDTO;
            return uow.Repository<Governate>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Ar_Name == obj.Ar_Name || p.En_Name == obj.En_Name)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Governate>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Governate>().Update(Cmodel);
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

        public Dictionary<string, object> GetAllActive(List<string> Device_Info)
        {
            var data = uow.Repository<Governate>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null).ToList();
            //set default value fz 17-4-2019
            string lang = Device_Info[2];

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, 
                data.OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList());
        }

        //******************************************//
        public Dictionary<string, object> Insert(GovernateDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Short("Governate_seq");
                    //entity.ID =int.Parse( id.ToString());
                    entity.ID = id;
                    var CModel = Mapper.Map<Governate>(entity);
                    
                    uow.Repository<Governate>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(GovernateDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as GovernateDTO;
                    Governate CModel = uow.Repository<Governate>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    Co.Ar_Name = Co.Ar_Name.Trim();
                    Co.En_Name = Co.En_Name.Trim();
                    uow.Repository<Governate>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Governate, GovernateDTO>(Co);
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


        //ADD FUNCTIONS TO FILL DROPS        
        public Dictionary<string, object> FillDrop_Add(List<string> Device_Info)
        {
            string lang =Device_Info[2];
            var data = uow.Repository<Governate>().GetData().Where(g => g.User_Deletion_Id == null)
                .Select(c => new CustomOption
                { //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            data.Insert(0, new CustomOption { DisplayText = "------------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_List(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Governate>().GetData().Where(a => a.User_Deletion_Id == null && a.IsActive == true).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            data.Insert(0, new CustomOption { DisplayText = "------------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }




    }
}