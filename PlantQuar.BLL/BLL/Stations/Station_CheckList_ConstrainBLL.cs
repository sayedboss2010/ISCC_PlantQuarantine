using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.StationNew;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Stations
{
    public class Station_CheckList_ConstrainBLL : IGenericBLL<Station_CheckList_Constrain_DTO>
    {
        private UnitOfWork uow;

        public Station_CheckList_ConstrainBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Station_CheckList entity = uow.Repository<Station_CheckList>().Findobject(Id);
                var empDTO = Mapper.Map<Station_CheckList, Station_CheckList_Constrain_DTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetCount()
        {
            var Station_CheckList = uow.Repository<Station_CheckList>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, Station_CheckList);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var dataDTO = new List<Station_CheckList_Constrain_DTO>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    dataDTO = uow.Repository<Station_CheckList>().GetData()
                        .Where(a => a.User_Deletion_Id == null)
                        .OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En))
                        .Select(a => new Station_CheckList_Constrain_DTO
                        {
                            ConstrainText_Ar = a.ConstrainText_Ar,
                            ConstrainText_En = a.ConstrainText_En,
                            Description_Ar = a.Description_Ar,
                            Description_En = a.Description_En,
                            Is_Androud = a.Is_Androud,
                            IsActive = a.IsActive,
                            Constrain_Type_Name = a.Station_Constrain_Country_Item.Ar_Name,


                        }).ToList();
                }
                else
                {
                    dataDTO = uow.Repository<Station_CheckList>().GetData()
                        .Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En)).
                        Select(a => new Station_CheckList_Constrain_DTO
                        {
                            ConstrainText_Ar = a.ConstrainText_Ar,
                            ConstrainText_En = a.ConstrainText_En,
                            Description_Ar = a.Description_Ar,
                            Description_En = a.Description_En,
                            Is_Androud = a.Is_Androud,
                            IsActive = a.IsActive,
                            Constrain_Type_Name = a.Station_Constrain_Country_Item.Ar_Name,

                        }).Skip(index).Take(pageSize).ToList();
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, byte? Fill_Lists_Type, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities db = new PlantQuarantineEntities();
                var data = (from Ect in db.Station_CheckList
                            where Ect.User_Deletion_Id == null
                            //&& Ect.IsActive == true
                            && Ect.User_Deletion_Date == null
                            && Ect.Station_Constrain_Country_Item_ID == (Fill_Lists_Type == 0 ? null : Fill_Lists_Type)
                            select new Station_CheckList_Constrain_DTO
                            {
                                ID = Ect.ID,
                                ConstrainText_Ar = Ect.ConstrainText_Ar,
                                ConstrainText_En = Ect.ConstrainText_En,
                                Description_Ar = Ect.Description_Ar,
                                Description_En = Ect.Description_En,
                                IsActive = Ect.IsActive,
                                Station_Constrain_Country_Item_ID = Ect.Station_Constrain_Country_Item_ID,
                                Is_Androud = Ect.Is_Androud,
                                Constrain_Type_Name = Ect.Station_Constrain_Country_Item.Ar_Name
                            }).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
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
                var Cmodel = uow.Repository<Station_CheckList>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Station_CheckList>().Update(Cmodel);
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

        //public bool GetAny(Station_CheckList_Constrain_DTO entity)
        //{
        //    var obj = entity as Station_CheckList_Constrain_DTO;
        //    //return uow.Repository<Station_CheckList>().GetAny(p => p.User_Deletion_Id == null && (obj.ID == 0 ? true : p.ID != obj.ID) &&
        //    //                            ((p.ConstrainText_Ar == obj.ConstrainText_Ar || p.ConstrainText_En == obj.ConstrainText_En)));
        //    return uow.Repository<Station_CheckList>().GetAny(p => p.User_Deletion_Id == null && (obj.ID == 0 ? true : p.ID != obj.ID) &&
        //                              ((p.ConstrainText_Ar == obj.ConstrainText_Ar || p.ConstrainText_En == obj.ConstrainText_En)));
        //}

        public bool GetAny(Station_CheckList_Constrain_DTO entity)
        {
            var obj = entity as Station_CheckList_Constrain_DTO;
            //return uow.Repository<Station_CheckList>().GetAny(p => p.User_Deletion_Id == null && (obj.ID == 0 ? true : p.ID != obj.ID) &&
            //                            ((p.ConstrainText_Ar == obj.ConstrainText_Ar || p.ConstrainText_En == obj.ConstrainText_En)));
            if (obj.ID == 0)
            {
                return uow.Repository<Station_CheckList>().GetAny(p => p.User_Deletion_Id == null &&
                                          ((p.ConstrainText_Ar == obj.ConstrainText_Ar || p.ConstrainText_En == obj.ConstrainText_En)));
            }
            else
            {
                return uow.Repository<Station_CheckList>().GetAny(p => p.User_Deletion_Id == null && p.ID != obj.ID &&
                                      ((p.ConstrainText_Ar == obj.ConstrainText_Ar || p.ConstrainText_En == obj.ConstrainText_En)));
            }
        }

        public Dictionary<string, object> Insert(Station_CheckList_Constrain_DTO entity, List<string> Device_Info)
        {
            try
            {
                entity.ConstrainText_Ar = entity.ConstrainText_Ar.Trim();
                entity.ConstrainText_En = entity.ConstrainText_En.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Station_CheckList_seq");
                    entity.ID = id;
                    var CModel = Mapper.Map<Station_CheckList>(entity);
                    uow.Repository<Station_CheckList>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(Station_CheckList_Constrain_DTO entity, List<string> Device_Info)
        {
            try
            {
                entity.ConstrainText_Ar = entity.ConstrainText_Ar.Trim();
                entity.ConstrainText_En = entity.ConstrainText_En.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as Station_CheckList_Constrain_DTO;
                    Station_CheckList CModel = uow.Repository<Station_CheckList>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    ////if (CModel.User_Updation_Id != null)
                    ////{
                    //    obj.User_Updation_Date = DateTime.Now;
                    //    obj.User_Updation_Id = CModel.User_Updation_Id;
                    //}
                    //obj.Is_Androud = entity.Is_Androud;
                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Station_CheckList>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Station_CheckList, Station_CheckList_Constrain_DTO>(Co);
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
            var data = new List<CustomOption>();
            data = uow.Repository<EX_Constrain_Type>().GetData().Where(g => g.User_Deletion_Id == null && g.IsActive
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
                var data = uow.Repository<Station_CheckList>().
                    GetData().Where(a => a.ID == id).Select(a =>
                    new Station_CheckList_Constrain_DTO()
                    {
                        ID = a.ID,
                        ConstrainText_Ar = a.ConstrainText_Ar,
                        ConstrainText_En = a.ConstrainText_En,
                        Description_Ar = a.Description_Ar,
                        Description_En = a.Description_En,
                        IsActive = a.IsActive,
                        Number_Check = a.Number_Check,
                        Station_Constrain_Country_Item_ID = a.Station_Constrain_Country_Item_ID,
                        Is_Androud = a.Is_Androud,
                        Constrain_Type_Name = a.Station_Constrain_Country_Item.Ar_Name,
                        Constrain_Type_ID = a.Station_Constrain_Country_Item.Station_Type_ID
                    }).SingleOrDefault();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetBy_Station_Constrain_TypeId(long Station_Constrain_Country_Item_ID, bool Android_ID, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId2>();
                data = uow.Repository<Station_CheckList>().GetData().Where(g => g.User_Deletion_Id == null
                && g.IsActive
                && g.Station_Constrain_Country_Item_ID == Station_Constrain_Country_Item_ID
                && g.Is_Androud == Android_ID).Select(c => new CustomOptionLongId2
                { //change display lang
                    DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();

                //set default value fz 17-4-2019
                data.Insert(0, new CustomOptionLongId2() { DisplayText = "----------", Value = null });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


    }
}