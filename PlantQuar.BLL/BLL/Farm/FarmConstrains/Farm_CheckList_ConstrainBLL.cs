using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmConstrain;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmConstrains
{
    public class Farm_CheckList_ConstrainBLL
    {
        private UnitOfWork uow;

        public Farm_CheckList_ConstrainBLL()
        {
            uow = new UnitOfWork();
        }


        // new 

        public Dictionary<string, object> GetAll(short Country_ID, long Item_ID, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities db = new PlantQuarantineEntities();
                var data = (from Ect in db.Farm_CheckList

                            join fcc in db.Farm_Country_CheckList on Ect.ID equals fcc.Farm_CheckList_ID
                            where Ect.User_Deletion_Id == null
                         && Ect.IsActive == true
                         && fcc.IsActive == true
                         && (fcc.Country_ID == Country_ID || fcc.Country_ID == null)
                         && fcc.Item_ID == Item_ID
                            && Ect.User_Deletion_Date == null
                            && fcc.User_Deletion_Date == null

                            select new Farm_CheckList_Constrain_DTO
                            {
                                ID_Farm_CheckList = Ect.ID,
                                ID_Farm_Country_CheckList = fcc.ID,
                                ConstrainText_Ar = Ect.ConstrainText_Ar,
                                ConstrainText_En = Ect.ConstrainText_En,
                                Description_Ar = Ect.Description_Ar,
                                Description_En = Ect.Description_En,
                                IsActive = Ect.IsActive,
                                Country_Id = fcc.Country_ID,
                                Item_ID = fcc.Item_ID,
                                Item_Name = fcc.Item.Name_Ar,
                                Country_Name = fcc.Country.Ar_Name,
                                //Station_Constrain_Country_Item_ID = Ect.Station_Constrain_Country_Item_ID,
                                //Is_Androud = Ect.Is_Androud,
                                //Constrain_Type_Name = Ect.Station_Constrain_Country_Item.Ar_Name
                            }).ToList();
                //if (Country_ID != null)
                //{
                //    data = data.Where(a => a.Country_Id == Country_ID).ToList();
                //}
                //else if (Country_ID == null)
                //{
                //    data = data.Where(a => a.Country_Id == null).ToList();
                //}

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public bool GetAny(Farm_CheckList_Constrain_DTO entity)
        {
            var obj = entity as Farm_CheckList_Constrain_DTO;
            return uow.Repository<Farm_CheckList>().GetAny(p => p.User_Deletion_Id == null
            && (obj.ID_Farm_CheckList == 0 ? true : p.ID != obj.ID_Farm_CheckList) &&
                                        ((p.ConstrainText_Ar == obj.ConstrainText_Ar || p.ConstrainText_En == obj.ConstrainText_En)));
        }
        public Dictionary<string, object> Insert(Farm_CheckList_Constrain_DTO entity, List<string> Device_Info)
        {
            try
            {
                //entity.ConstrainText_Ar = entity.ConstrainText_Ar.Trim();
                //entity.ConstrainText_En = entity.ConstrainText_En.Trim();
                //if (!GetAny(entity))
                //{
                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        //long _Farm_CheckList_Id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_CheckList_seq");
                        //var Insert_Farm_CheckList = new Farm_CheckList
                        //{
                        //    ID = _Farm_CheckList_Id,
                        //    IsActive = true,
                        //    ConstrainText_Ar = entity.ConstrainText_Ar,
                        //    ConstrainText_En = entity.ConstrainText_En,
                        //    Description_Ar = entity.Description_Ar,
                        //    Description_En = entity.Description_En,
                        //    User_Creation_Id = entity.User_Creation_Id,
                        //    User_Creation_Date = DateTime.Now,
                        //};
                        //context.Farm_CheckList.Add(Insert_Farm_CheckList);
                        //context.SaveChanges();

                        if (entity.Country_Id != -1)
                        {
                            long _Farm_Country_CheckList_Id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Country_CheckList_seq");

                            var Insert_Farm_Country_CheckList = new Farm_Country_CheckList
                            {
                                ID = _Farm_Country_CheckList_Id,
                                IsActive = true,
                                Country_ID = entity.Country_Id,
                                Item_ID = entity.Item_ID,
                                Farm_CheckList_ID = entity.Farm_Constrain_Text_ID,
                                User_Creation_Id = entity.User_Creation_Id,
                                User_Creation_Date = DateTime.Now,
                            };
                            context.Farm_Country_CheckList.Add(Insert_Farm_Country_CheckList);
                            context.SaveChanges();
                        }
                        else
                        {
                            var data_Country = context.Countries.Where(co => co.IsActive == true && co.User_Deletion_Date == null).Select(a => a.ID).ToList();

                            //foreach (var item in data_Country)
                            //{
                            long _Farm_Country_CheckList_Id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Country_CheckList_seq");

                            var Insert_Farm_Country_CheckList = new Farm_Country_CheckList
                            {
                                ID = _Farm_Country_CheckList_Id,
                                IsActive = true,
                                //Country_ID = item,
                                Item_ID = entity.Item_ID,
                                Farm_CheckList_ID = entity.Farm_Constrain_Text_ID,
                                User_Creation_Id = entity.User_Creation_Id,
                                User_Creation_Date = DateTime.Now,
                            };
                            context.Farm_Country_CheckList.Add(Insert_Farm_Country_CheckList);
                            context.SaveChanges();
                            //}
                        }
                        trans.Commit();
                    }
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
                //}
                //else
                //{
                //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                //}
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> Update_Delete_Farm_CheckList(int Update_Delete, Farm_CheckList_Constrain_DTO entity_Update, List<string> Device_Info)
        {
            // 1 Update IsActive=false
            //0 Delete
            // 2 Update IsActive=true
            try
            {
                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        bool _IsActive = false;
                        if (Update_Delete == 2)
                        {
                            _IsActive = true;
                        }
                        ////Farm_CheckList CModel_Farm_CheckList = uow.Repository<Farm_CheckList>().Findobject(entity_Update.ID_Farm_CheckList);

                        ////if (Update_Delete == 1|| Update_Delete == 2)
                        ////{
                        ////    CModel_Farm_CheckList.IsActive = _IsActive;
                        ////    CModel_Farm_CheckList.User_Updation_Id = entity_Update.User_Updation_Id;
                        ////    CModel_Farm_CheckList.User_Updation_Date = DateTime.Now;
                        ////}
                        ////else
                        ////{                            
                        ////    CModel_Farm_CheckList.User_Deletion_Id = entity_Update.User_Updation_Id;
                        ////    CModel_Farm_CheckList.User_Deletion_Date = DateTime.Now;
                        ////}
                        ////uow.Repository<Farm_CheckList>().Update(CModel_Farm_CheckList);
                        ////uow.SaveChanges();

                        Farm_Country_CheckList _Farm_Country_CheckList = uow.Repository<Farm_Country_CheckList>().Findobject(entity_Update.ID_Farm_Country_CheckList);
                        if (Update_Delete == 1 || Update_Delete == 2)
                        {
                            _Farm_Country_CheckList.IsActive = _IsActive;
                            _Farm_Country_CheckList.User_Updation_Id = entity_Update.User_Updation_Id;
                            _Farm_Country_CheckList.User_Updation_Date = DateTime.Now;
                        }
                        else
                        {
                            _Farm_Country_CheckList.User_Deletion_Id = entity_Update.User_Updation_Id;
                            _Farm_Country_CheckList.User_Deletion_Date = DateTime.Now;
                        }
                        uow.Repository<Farm_Country_CheckList>().Update(_Farm_Country_CheckList);
                        uow.SaveChanges();
                    }
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity_Update);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //end new 
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Farm_CheckList entity = uow.Repository<Farm_CheckList>().Findobject(Id);
                var empDTO = Mapper.Map<Farm_CheckList, Farm_CheckList_Constrain_DTO>(entity);
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
            var Farm_CheckList = uow.Repository<Farm_CheckList>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, Farm_CheckList);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var dataDTO = new List<Farm_CheckList_Constrain_DTO>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    dataDTO = uow.Repository<Farm_CheckList>().GetData()
                        .Where(a => a.User_Deletion_Id == null)
                        .OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En))
                        .Select(a => new Farm_CheckList_Constrain_DTO
                        {
                            ConstrainText_Ar = a.ConstrainText_Ar,
                            ConstrainText_En = a.ConstrainText_En,
                            Description_Ar = a.Description_Ar,
                            Description_En = a.Description_En,
                            // Is_Androud = a.Is_Androud,
                            IsActive = a.IsActive,
                            //Constrain_Type_Name = a.Station_Constrain_Country_Item.Ar_Name
                        }).ToList();
                }
                else
                {
                    dataDTO = uow.Repository<Farm_CheckList>().GetData()
                        .Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En)).
                        Select(a => new Farm_CheckList_Constrain_DTO
                        {
                            ConstrainText_Ar = a.ConstrainText_Ar,
                            ConstrainText_En = a.ConstrainText_En,
                            Description_Ar = a.Description_Ar,
                            Description_En = a.Description_En,
                            // Is_Androud = a.Is_Androud,
                            IsActive = a.IsActive,
                            // Constrain_Type_Name = a.Station_Constrain_Country_Item.Ar_Name
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
                var data = uow.Repository<Farm_CheckList>().
                    GetData().Where(a => a.ID == id).Select(a =>
                    new Farm_CheckList_Constrain_DTO()
                    {
                        ID_Farm_CheckList = a.ID,
                        ConstrainText_Ar = a.ConstrainText_Ar,
                        ConstrainText_En = a.ConstrainText_En,
                        Description_Ar = a.Description_Ar,
                        Description_En = a.Description_En,
                        IsActive = a.IsActive,

                        //Station_Constrain_Country_Item_ID = a.Station_Constrain_Country_Item_ID,
                        //Is_Androud = a.Is_Androud,
                        //Constrain_Type_Name = a.Station_Constrain_Country_Item.Ar_Name,
                        //Constrain_Type_ID = a.Station_Constrain_Country_Item.Station_Type_ID
                    }).SingleOrDefault();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetBy_Farm_Constrain_TypeId(long Farm_Constrain_Country_Item_ID, bool Android_ID, List<string> Device_Info)
        {
            try
            {
                //string lang = Device_Info[2];
                //var data = new List<CustomOptionLongId2>();
                //data = uow.Repository<Farm_CheckList>().GetData().Where(g => g.User_Deletion_Id == null
                //&& g.IsActive
                //&& g.Station_Constrain_Country_Item_ID == Farm_Constrain_Country_Item_ID
                //&& g.Is_Androud == Android_ID)
                //.Select(c => new CustomOptionLongId2
                //{ //change display lang
                //    DisplayText = (lang == "1" ? c.ConstrainText_Ar : c.ConstrainText_En),
                //    Value = c.ID
                //}).OrderBy(a => a.DisplayText).ToList();

                ////set default value fz 17-4-2019
                //data.Insert(0, new CustomOptionLongId2() { DisplayText = "----------", Value = null });
                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, null);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
