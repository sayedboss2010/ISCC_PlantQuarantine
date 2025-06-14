using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station_Pages
{
    public class Station_Accreditation_Data_Entry_BLL
    {
        private UnitOfWork uow;

        public Station_Accreditation_Data_Entry_BLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetBy_Station_Constrain_TypeId(long EX_Constrain_Type_id, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId2>();
                data = uow.Repository<Station_Constrain_Type>().GetData().Where(g => g.User_Deletion_Id == null
                && g.IsActive).Select(c => new CustomOptionLongId2
                { //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
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

        public Dictionary<string, object> Insert_Station_Accreditation_Data(Station_Accreditation_Data_Entry_DTO entity, List<string> Device_Info)
        {
            try
            {

                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        int Complete_Count = 0;
                        var Station_Accreditation_Data_NameCount = context.Station_Accreditation_Data.Where(a => a.Name_AR == entity.Name_AR
                      && a.Accreditation_Type_ID == entity.Accreditation_Type_ID).ToList();
                        if (Station_Accreditation_Data_NameCount.Count() == 0)
                        {
                            var id = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Data_SEQ");

                            var model = new Station_Accreditation_Data();
                            model.ID = id;
                            model.Name_AR = entity.Name_AR;
                            model.Name_En = entity.Name_En;
                            model.StationActivityType_ID = entity.StationActivityType_ID;
                            model.Accreditation_Type_ID = entity.Accreditation_Type_ID;
                            model.DescriptionMore_AR = entity.DescriptionMore_AR;
                            model.Description_Ar = entity.Description_Ar;
                            model.Description_En = entity.Description_En;
                            model.DescriptionMore_EN = entity.DescriptionMore_EN;
                            model.IsActive = entity.IsActive;
                            model.User_Creation_Date = entity.User_Creation_Date;
                            model.User_Creation_Id = entity.User_Creation_Id;

                            context.Station_Accreditation_Data.Add(model);
                            context.SaveChanges();

                            #region Station_Country  
                            if (entity.List_Station_Country != null)
                            {
                                if (entity.List_Station_Country.Count > 0)
                                {
                                    foreach (var item in entity.List_Station_Country)
                                    {
                                        var id_Country = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Data_Country_seq");
                                        var Data_Country = new Station_Accreditation_Data_Country
                                        {
                                            ID = id_Country,
                                            Station_Accreditation_Data_ID = id,
                                            CountryID = item.CountryID,
                                            IsActive = item.IsActive,
                                            User_Creation_Id = entity.User_Creation_Id,
                                            User_Creation_Date = entity.User_Creation_Date,
                                        };
                                        context.Station_Accreditation_Data_Country.Add(Data_Country);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Station_Item_ShortName              
                            if (entity.List_Station_Item_ShortName != null)
                            {
                                if (entity.List_Station_Item_ShortName.Count > 0)
                                {
                                    foreach (var item_ShortName in entity.List_Station_Item_ShortName)
                                    {
                                        var id_Item_ShortName = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Data_Item_ShortName_seq");
                                        var Data_Item_ShortName = new Station_Accreditation_Data_Item_ShortName
                                        {
                                            ID = id_Item_ShortName,
                                            Station_Accreditation_Data_ID = id,
                                            Item_ShortName_ID = item_ShortName.Item_ShortName_ID,
                                            IsActive = item_ShortName.IsActive,
                                            User_Creation_Id = entity.User_Creation_Id,
                                            User_Creation_Date = entity.User_Creation_Date,
                                        };
                                        context.Station_Accreditation_Data_Item_ShortName.Add(Data_Item_ShortName);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Station_CheckList              
                            if (entity.List_Station_CheckList != null)
                            {
                                if (entity.List_Station_CheckList.Count > 0)
                                {
                                    foreach (var item_Station_Check in entity.List_Station_CheckList)
                                    {
                                        Complete_Count = 1;
                                        var id_Station_Accreditation_CheckList = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_CheckList_seq");
                                        var Data_Station_Accreditation_CheckList = new Station_Accreditation_CheckList
                                        {
                                            ID = id_Station_Accreditation_CheckList,
                                            Station_Accreditation_Data_ID = id,
                                            Station_CheckList_ID = item_Station_Check.Station_CheckList_ID,
                                            IsActive = true,
                                            User_Creation_Id = entity.User_Creation_Id,
                                            User_Creation_Date = entity.User_Creation_Date,
                                        };
                                        context.Station_Accreditation_CheckList.Add(Data_Station_Accreditation_CheckList);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Station_A_AttachmentData             
                            if (entity.List_Station_Attachment.Count > 0)
                            {
                                foreach (var item_A_AttachmentData in entity.List_Station_Attachment)
                                {
                                    var id_Station_A_AttachmentData = uow.Repository<Object>().GetNextSequenceValue_Long("A_AttachmentData_Station_seq");
                                    var Data_Station_A_AttachmentData = new A_AttachmentData_Station
                                    {
                                        Id = id_Station_A_AttachmentData,
                                        A_AttachmentTableNameId = 18,
                                        RowId = id,
                                        A_AttachmentTableType_ID = item_A_AttachmentData.A_AttachmentTableType_ID,
                                        Attachment_Number = item_A_AttachmentData.Attachment_Number,
                                        Attachment_TypeName = item_A_AttachmentData.Attachment_TypeName,

                                        //Station_CheckList_ID = item_Station_Check.Station_CheckList_ID,
                                        //IsActive = true,
                                        User_Creation_Id = entity.User_Creation_Id,
                                        User_Creation_Date = entity.User_Creation_Date,
                                    };
                                    context.A_AttachmentData_Station.Add(Data_Station_A_AttachmentData);
                                    context.SaveChanges();
                                }
                            }
                            #endregion

                            if (Complete_Count == 1)
                            {
                                entity.Message = "تم الحفظ بنجاح";
                                trans.Commit();
                            }
                            else
                            {
                                entity.Message = "لا يمكن الحفظ  لعدم وجود متطلبات";
                            }

                            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);

                        }
                        else
                        {
                            entity.Message = "الاسم موجود مسبقا";
                            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "Count");
                            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop_List(int? StationActivityType_ID, int? Accreditation_Type_ID, List<string> Device_Info)
        {
            string lang = Device_Info[2];


            var data = uow.Repository<Station_Accreditation_Data>().GetData().
                Where(lab => lab.User_Deletion_Id == null && lab.IsActive == true
              ).ToList();

            if (StationActivityType_ID != null && Accreditation_Type_ID == null)
            {
                data = data.Where(a => a.StationActivityType_ID == StationActivityType_ID).ToList();
            }
            if (StationActivityType_ID == null && Accreditation_Type_ID != null)
            {
                data = data.Where(a => a.Accreditation_Type_ID == Accreditation_Type_ID).ToList();
            }

            if (StationActivityType_ID != null && Accreditation_Type_ID != null)
            {
                data = data.Where(a => a.Accreditation_Type_ID == Accreditation_Type_ID
                && a.StationActivityType_ID == StationActivityType_ID
               ).ToList();
            }
            var data_Option = data.Select(c => new CustomOptionLongId
            {
                DisplayText = (lang == "1" ? c.Name_AR : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();

            //set default value fz 17-4-2019
            data_Option.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, data_Option);
        }

        public Dictionary<string, object> GetById(long id, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data_Station_Accreditation = uow.Repository<Station_Accreditation_Data>().
                    GetData().Where(a => a.ID == id).Select(a =>
                    new Station_Accreditation_Data_Entry_DTO()
                    {
                        ID = a.ID,
                        Name_AR = a.Name_AR,
                        Name_En = a.Name_En,
                        Description_Ar = a.Description_Ar,
                        Description_En = a.Description_En,
                        DescriptionMore_AR = a.DescriptionMore_AR,
                        DescriptionMore_EN = a.DescriptionMore_EN,
                        StationActivityType_ID = a.StationActivityType_ID,
                        Accreditation_Type_ID = a.Accreditation_Type_ID,
                        MainTreatment_Name = a.StationActivityType.TreatmentMethod.TreatmentType.TreatmentMainType.Ar_Name,
                        TreatmentType_Name = a.StationActivityType.TreatmentMethod.TreatmentType.Ar_Name,
                        TreatmentMethod_Name = a.StationActivityType.TreatmentMethod.Ar_Name,
                        IsActive = (bool)a.IsActive,
                    }).FirstOrDefault();



                #region Fill_Station_Accreditation_Data_Country
                try
                {
                    PlantQuarantineEntities db = new PlantQuarantineEntities();
                    var Fill_Station_Accreditation_Data_Country = (from de in db.Station_Accreditation_Data_Country
                                                                   join c in db.Countries on de.CountryID equals c.ID
                                                                   join uc in db.Union_Country.Where(o => o.IsActive == true && o.User_Deletion_Id ==null && o.User_Deletion_Date == null) on c.ID equals uc.Country_ID  into uc1
                                                                   from uc in uc1.DefaultIfEmpty()
                                                                   join u in db.Unions on uc.Union_ID equals u.ID into u1 
                                                                   from u in u1.DefaultIfEmpty() 
                                                                   where de.Station_Accreditation_Data_ID == data_Station_Accreditation.ID
                                                                 

                                                                   select new Station_Accreditation_Data_CountryDTO()
                                                                   {
                                                                       Id = de.ID,
                                                                       CountryID = de.CountryID,
                                                                       Union_Id = uc.Union_ID,
                                                                       Country_Name = c.Ar_Name ,
                                                                       Union_Name = u.Ar_Name == null ? "كل الدول" : u.Ar_Name,
                                                                       IsActive = de.IsActive,
                                                                   }).ToList();
                    data_Station_Accreditation.List_Station_Country = Fill_Station_Accreditation_Data_Country;
                }
                catch (Exception ex)
                {

                }

                #endregion

                #region Fill_Station_Accreditation_Data_Item_ShortName
                var Fill_Station_Accreditation_Data_Item_ShortName = uow.Repository<Station_Accreditation_Data_Item_ShortName>()
                .GetData().Where(a => a.Station_Accreditation_Data_ID == data_Station_Accreditation.ID).Select(a =>
                new Station_Accreditation_Data_Item_ShortNameDTO()
                {
                    ID = a.ID,
                    Item_ShortName_ID = a.Item_ShortName.ID,
                    ShortName_Name = a.Item_ShortName.ShortName_Ar,
                    Item_Name = a.Item_ShortName.Item.Name_Ar,
                    // Union_Name=a.Country.Union_Country.FirstOrDefault().Union.Ar_Name,
                    IsActive = a.IsActive,
                }).ToList();
                data_Station_Accreditation.List_Station_Item_ShortName = Fill_Station_Accreditation_Data_Item_ShortName;
                #endregion

                #region Fill_Station_Accreditation_CheckList
                var Fill_Station_Accreditation_CheckList = uow.Repository<Station_Accreditation_CheckList>()
                .GetData().Where(a => a.Station_Accreditation_Data_ID == data_Station_Accreditation.ID).Select(a =>
                new Station_CheckList_DTO()
                {
                    Station_Accreditation_Data_ID = a.ID,
                    Station_CheckList_ID = a.Station_CheckList.ID,
                    Station_Constrain_Type_Name = a.Station_CheckList.Station_Constrain_Country_Item.Station_Constrain_Type.Ar_Name,
                    Station_Accreditation_Country_Item_Name = a.Station_CheckList.Station_Constrain_Country_Item.Ar_Name,
                    Station_CheckList_Android_Name = a.Station_CheckList.Is_Androud == true ? "متطلبات" : "تعليمات",
                    Station_Accreditation_Text_Name = a.Station_CheckList.ConstrainText_Ar,
                    InSide_Certificate_Ar = a.Station_CheckList.Description_Ar,
                    IsActive = a.IsActive,
                }).ToList();
                data_Station_Accreditation.List_Station_CheckList = Fill_Station_Accreditation_CheckList;
                #endregion


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data_Station_Accreditation);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Edite_Station_Accreditation_Data(Station_Accreditation_Data_Entry_DTO entity, List<string> Device_Info)
        {
            try
            {

                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        int Complete_Count = 0;
                        var Station_Accreditation_Data_NameCount = context.Station_Accreditation_Data.Where(a => a.Name_AR == entity.Name_AR
                      && a.Accreditation_Type_ID == entity.Accreditation_Type_ID && a.ID != entity.ID).ToList();
                        if (Station_Accreditation_Data_NameCount.Count() == 0)
                        {
                            var id = entity.ID;
                            Station_Accreditation_Data model = uow.Repository<Station_Accreditation_Data>().Findobject(id);
                            model.Name_AR = entity.Name_AR;
                            model.Name_En = entity.Name_En;
                            model.StationActivityType_ID = entity.StationActivityType_ID;
                            model.Accreditation_Type_ID = entity.Accreditation_Type_ID;
                            model.DescriptionMore_AR = entity.DescriptionMore_AR;
                            model.Description_Ar = entity.Description_Ar;
                            model.Description_En = entity.Description_En;
                            model.DescriptionMore_EN = entity.DescriptionMore_EN;
                            model.IsActive = entity.IsActive;
                            model.User_Creation_Date = entity.User_Creation_Date;
                            model.User_Creation_Id = entity.User_Creation_Id;
                            uow.Repository<Station_Accreditation_Data>().Update(model);
                            uow.SaveChanges();


                            #region Station_Country  
                            if (entity.List_Station_Country != null)
                            {
                                if (entity.List_Station_Country.Count > 0)
                                {
                                    foreach (var item in entity.List_Station_Country)
                                    {
                                        //Update
                                        if (item.Id > 0)
                                        {
                                            Station_Accreditation_Data_Country Update_Data_Country = uow.Repository<Station_Accreditation_Data_Country>().Findobject(item.Id);
                                            Update_Data_Country.IsActive = item.IsActive;
                                            uow.Repository<Station_Accreditation_Data_Country>().Update(Update_Data_Country);
                                            uow.SaveChanges();
                                        }
                                        //Insert
                                        else
                                        {
                                            var id_Country = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Data_Country_seq");
                                            var Data_Country = new Station_Accreditation_Data_Country
                                            {
                                                ID = id_Country,
                                                Station_Accreditation_Data_ID = id,
                                                CountryID = item.CountryID,
                                                IsActive = item.IsActive,
                                                User_Creation_Id = entity.User_Creation_Id,
                                                User_Creation_Date = entity.User_Creation_Date,
                                            };
                                            context.Station_Accreditation_Data_Country.Add(Data_Country);
                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Station_Item_ShortName              
                            if (entity.List_Station_Item_ShortName != null)
                            {
                                if (entity.List_Station_Item_ShortName.Count > 0)
                                {
                                    foreach (var item_ShortName in entity.List_Station_Item_ShortName)
                                    {
                                        //update
                                        if (item_ShortName.Station_Accreditation_Data_ID > 0)
                                        {
                                            Station_Accreditation_Data_Item_ShortName Update_Item_ShortName =
                                                uow.Repository<Station_Accreditation_Data_Item_ShortName>().Findobject(item_ShortName.Station_Accreditation_Data_ID);
                                            Update_Item_ShortName.IsActive = item_ShortName.IsActive;
                                            uow.Repository<Station_Accreditation_Data_Item_ShortName>().Update(Update_Item_ShortName);
                                            uow.SaveChanges();
                                        }
                                        else // insert 
                                        {
                                            var id_Item_ShortName = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Data_Item_ShortName_seq");
                                            var Data_Item_ShortName = new Station_Accreditation_Data_Item_ShortName
                                            {
                                                ID = id_Item_ShortName,
                                                Station_Accreditation_Data_ID = id,
                                                Item_ShortName_ID = item_ShortName.Item_ShortName_ID,
                                                IsActive = item_ShortName.IsActive,
                                                User_Creation_Id = entity.User_Creation_Id,
                                                User_Creation_Date = entity.User_Creation_Date,
                                            };
                                            context.Station_Accreditation_Data_Item_ShortName.Add(Data_Item_ShortName);
                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Station_CheckList              
                            if (entity.List_Station_CheckList != null)
                            {
                                if (entity.List_Station_CheckList.Count > 0)
                                {
                                    foreach (var item_Station_Check in entity.List_Station_CheckList)
                                    {
                                        if (item_Station_Check.Station_Accreditation_Data_ID > 0)//update
                                        {
                                            Station_Accreditation_CheckList Update_CheckList =
                                               uow.Repository<Station_Accreditation_CheckList>().Findobject(item_Station_Check.Station_Accreditation_Data_ID);
                                            Update_CheckList.IsActive = item_Station_Check.IsActive;
                                            uow.Repository<Station_Accreditation_CheckList>().Update(Update_CheckList);
                                            uow.SaveChanges();
                                            Complete_Count = 1;
                                        }
                                        else //insert
                                        {
                                            var id_Station_Accreditation_CheckList = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_CheckList_seq");
                                            var Data_Station_Accreditation_CheckList = new Station_Accreditation_CheckList
                                            {
                                                ID = id_Station_Accreditation_CheckList,
                                                Station_Accreditation_Data_ID = id,
                                                Station_CheckList_ID = item_Station_Check.Station_CheckList_ID,
                                                IsActive = true,
                                                User_Creation_Id = entity.User_Creation_Id,
                                                User_Creation_Date = entity.User_Creation_Date,
                                            };
                                            context.Station_Accreditation_CheckList.Add(Data_Station_Accreditation_CheckList);
                                            context.SaveChanges();
                                            Complete_Count = 1;
                                        }
                                    }
                                }
                            }

                            var Count_CheckList = context.Station_Accreditation_CheckList.Where(a => a.Station_Accreditation_Data_ID == id).ToList();
                            if (Count_CheckList.Count > 0)
                            {
                                Complete_Count = 1;
                            }
                            #endregion

                            //#region Station_A_AttachmentData             
                            //if (entity.List_Station_Attachment.Count > 0)
                            //{
                            //    foreach (var item_A_AttachmentData in entity.List_Station_Attachment)
                            //    {
                            //        var id_Station_A_AttachmentData = uow.Repository<Object>().GetNextSequenceValue_Long("A_AttachmentData_Station_seq");
                            //        var Data_Station_A_AttachmentData = new A_AttachmentData_Station
                            //        {
                            //            Id = id_Station_A_AttachmentData,
                            //            A_AttachmentTableNameId = 18,
                            //            RowId = id,
                            //            A_AttachmentTableType_ID = item_A_AttachmentData.A_AttachmentTableType_ID,
                            //            Attachment_Number = item_A_AttachmentData.Attachment_Number,
                            //            Attachment_TypeName = item_A_AttachmentData.Attachment_TypeName,

                            //            //Station_CheckList_ID = item_Station_Check.Station_CheckList_ID,
                            //            //IsActive = true,
                            //            User_Creation_Id = entity.User_Creation_Id,
                            //            User_Creation_Date = entity.User_Creation_Date,
                            //        };
                            //        context.A_AttachmentData_Station.Add(Data_Station_A_AttachmentData);
                            //        context.SaveChanges();
                            //    }
                            //}
                            //#endregion
                            if (Complete_Count == 1)
                            {
                                entity.Message = "تم التعديل بنجاح";
                                trans.Commit();
                            }
                            else
                            {
                                entity.Message = "لا يمكن الحفظ  لعدم وجود متطلبات";
                            }

                            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                        }
                        else
                        {
                            entity.Message = "الاسم موجود مسبقا";
                            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "Count");
                            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, null);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                entity.Message = "يوجد خطأ ما";
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
