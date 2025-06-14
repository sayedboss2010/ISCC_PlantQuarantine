using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.DataEntry.Countries;
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
    public class VillageBLL : IGenericBLL<VillageDTO>
    {
        private UnitOfWork uow;

        public VillageBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Village entity = uow.Repository<Village>().Findobject(Id);
                var empDTO = Mapper.Map<Village, VillageDTO>(entity);
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
            var count = uow.Repository<Village>().GetData().Where(p => p.User_Deletion_Id == null
             // get undeleted parent
             && p.Center.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                //var data = new List<Village>();
                //string lang = Device_Info[2];

                //if (pageSize == -1 && index == -1)
                //{
                //    data = uow.Repository<Village>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).ToList();
                //}
                //else
                //{
                //    data = uow.Repository<Village>().GetData().Where(a => a.User_Deletion_Id == null)
                //        .OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
                //}

                //PlantQuarantineEntities entities = new PlantQuarantineEntities();
                //var data1 = (from cc in entities.Centers
                //             where cc.User_Deletion_Id == null
                //             select new VillageDTO
                //             {
                //                 ID = cc.ID,
                //                 Ar_Name = cc.Ar_Name,
                //                 En_Name = cc.En_Name,


                //                 //    Center_Name = a.Center.Ar_Name,
                //                 //    Govern_ID = a.Center.Govern_ID,
                //                 //    Gov_Name=a.Center.Governate.Ar_Name,


                //                 IsActiveName = cc.IsActive == true ? "فعال" : "غير فعال",
                //             }).ToList();



                //var dataDTO = data.Select(Mapper.Map<Village, VillageDTO>);
                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data1);



                string lang = Device_Info[2];
                var dataDTO = uow.Repository<Village>().GetData().Where(p => p.User_Deletion_Id == null && p.Center.User_Deletion_Id == null).Select(a => new VillageDTO
                {
                    ID = a.ID,
                    Center_ID = a.Center_ID,
                    Center_Name = a.Center.Ar_Name,
                    Govern_ID = a.Center.Govern_ID,
                    Gov_Name = a.Center.Governate.Ar_Name,
                    Ar_Name = a.Ar_Name,
                    En_Name = a.En_Name,
                    IsActiveName = a.IsActive == true ? "فعال" : "غير فعال",
                }).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name))

             .ToList();
                //var data = uow.Repository<Village>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
                // var dataDTO = data.Select(Mapper.Map<Village, VillageDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                var data = new List<VillageDTO>();
                Int64 data_Count = 0;
           //     db.Configuration.ProxyCreationEnabled = false;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Village>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.En_Name.StartsWith(enName.Trim())).Select(a => new VillageDTO
                                            {
                                                ID = a.ID,
                                                Center_ID = a.Center_ID,
                                                Ar_Name = a.Ar_Name,
                                                En_Name = a.En_Name,
                                                IsActive = a.IsActive,
                                                Govern_ID = a.Center.Govern_ID
                                            }).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Village>().GetData().Where(a => a.User_Deletion_Id == null &&
                                            a.Ar_Name.StartsWith(arName.Trim())).Select(a => new VillageDTO
                                            {
                                                ID = a.ID,
                                                Center_ID = a.Center_ID,
                                                Ar_Name = a.Ar_Name,
                                                En_Name = a.En_Name,
                                                IsActive = a.IsActive,
                                                Govern_ID = a.Center.Govern_ID
                                            }).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Village>().GetData().Where(a => a.User_Deletion_Id == null).Select(a => new VillageDTO
                    {
                        ID = a.ID,
                        Center_ID = a.Center_ID,
                        Ar_Name = a.Ar_Name,
                        En_Name = a.En_Name,
                        IsActive = a.IsActive,
                        Govern_ID = a.Center.Govern_ID
                    }).ToList();
                }
                else
                {
                    data = uow.Repository<Village>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName))).Select(a => new VillageDTO
                             {
                                 ID = a.ID,
                                 Center_ID = a.Center_ID,
                                 Ar_Name = a.Ar_Name,
                                 En_Name = a.En_Name,
                                 IsActive = a.IsActive,
                                 Govern_ID = a.Center.Govern_ID
                             }).ToList();
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

                var dataDTO = data.Skip(index).Take(pageSize);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Village_Data", dataDTO);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetVillageByCenterId_Web(int centerId, List<string> Device_Info)
        {
            var lang = Device_Info[2];
            var data = uow.Repository<Village>().GetData().Where(a => a.IsActive == true && a.User_Deletion_Id == null && a.Center_ID == centerId).Select(c => new CustomOption
            { //change display lang
                
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Village>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Village>().Update(Cmodel);
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
        public bool GetAny(VillageDTO entity)
        {
            var obj = entity as VillageDTO;
            return uow.Repository<Village>().GetAny(p => p.User_Deletion_Id == null  &&
                                        ((p.Ar_Name == obj.Ar_Name || p.En_Name == obj.En_Name)&& p.Center_ID == obj.Center_ID) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //******************************************//
        public Dictionary<string, object> Insert(VillageDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {

                    var id = uow.Repository<Object>().GetNextSequenceValue_Short("Village_seq");
          
                    var CModel = Mapper.Map<Village>(entity);
                    CModel.ID = id;

                    uow.Repository<Village>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(VillageDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Ar_Name = entity.Ar_Name.Trim();
                entity.En_Name = entity.En_Name.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as VillageDTO;
                    Village CModel = uow.Repository<Village>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    
                    uow.Repository<Village>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Village, VillageDTO>(Co);
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
            string lang = Device_Info[2];
            var data = uow.Repository<Village>().GetData().Where(g => g.User_Deletion_Id == null && g.IsActive == true).Select(c => new CustomOption
            { //change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList(); ;
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> FillDrop_Edit(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Village>().GetData().Where(a => a.User_Deletion_Id == null).Select(c => new CustomOption
            {//change display lang
                DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                Value = c.ID }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, 
                data.OrderBy(a => a.DisplayText).ToList());
        }


        #region Depending DropDown
        public Dictionary<string, object> GetVillageByCenterId(int CenterId, List<string> Device_Info)
        {
            try
            {
                var lang = Device_Info[2];
                var data = uow.Repository<Village>().GetData().Where(a => a.User_Deletion_Id == null
                && a.Center_ID == CenterId && a.IsActive==true
                && a.Center.IsActive == true && a.Center.User_Deletion_Id==null)
                    .Select(c => new CustomOption
                    { //change display lang

                        DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                        Value = c.ID
                    }).OrderBy(a => a.DisplayText).ToList();
                data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAllUsingParamForAddEdit(int CenterId, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Village>().GetData().Where(a => a.User_Deletion_Id == null && a.Center_ID == CenterId)
                    .Select(c => new CustomOption { DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name), Value = c.ID }).ToList();
                CustomOption empty = new CustomOption();
                empty.Value = null;
                empty.DisplayText = "-";
                data.Insert(0, empty);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, 
                    data.OrderBy(a => a.DisplayText).ToList());
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        #endregion
    }
}
