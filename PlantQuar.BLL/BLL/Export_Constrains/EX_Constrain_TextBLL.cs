using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.DTO.Import.Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Export_Constrains
{
    public class EX_Constrain_TextBLL : IGenericBLL<EX_Constrain_Text_DTO>
    {
        private UnitOfWork uow;

        public EX_Constrain_TextBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                EX_Constrain_Text entity = uow.Repository<EX_Constrain_Text>().Findobject(Id);
                var empDTO = Mapper.Map<EX_Constrain_Text, EX_Constrain_Text_DTO>(entity);
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
            var EX_Constrain_Text = uow.Repository<EX_Constrain_Text>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, EX_Constrain_Text);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var dataDTO = new List<EX_Constrain_Text_DTO>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    dataDTO = uow.Repository<EX_Constrain_Text>().GetData()
                        .Where(a => a.User_Deletion_Id == null)
                        .OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En))
                        .Select(a => new EX_Constrain_Text_DTO
                        {
                            ConstrainText_Ar = a.ConstrainText_Ar,
                            ConstrainText_En = a.ConstrainText_En,
                            InSide_Certificate_Ar = a.InSide_Certificate_Ar,
                            InSide_Certificate_En = a.InSide_Certificate_En,
                            IsCertificate_Addtion = a.IsCertificate_Addtion,
                            IsActive = a.IsActive,
                            Constrain_Type_Name = a.EX_Constrain_Country_Item.Ar_Name
                        }).ToList();
                }
                else
                {
                    dataDTO = uow.Repository<EX_Constrain_Text>().GetData()
                        .Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En)).
                        Select(a => new EX_Constrain_Text_DTO
                        {
                            ConstrainText_Ar = a.ConstrainText_Ar,
                            ConstrainText_En = a.ConstrainText_En,
                            InSide_Certificate_Ar = a.InSide_Certificate_Ar,
                            InSide_Certificate_En = a.InSide_Certificate_En,
                            IsCertificate_Addtion = a.IsCertificate_Addtion,
                            IsActive = a.IsActive,
                            Constrain_Type_Name = a.EX_Constrain_Country_Item.Ar_Name
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
                var data = (from Ect in db.EX_Constrain_Text
                            where Ect.User_Deletion_Id == null
                            //&& Ect.IsActive == true
                            && Ect.User_Deletion_Date == null
                            && Ect.EX_Constrain_Country_Item_ID == (Fill_Lists_Type == 0 ? null : Fill_Lists_Type)
                            select new EX_Constrain_Text_DTO
                            {
                                ID = Ect.ID,
                                ConstrainText_Ar = Ect.ConstrainText_Ar,
                                ConstrainText_En = Ect.ConstrainText_En,
                                InSide_Certificate_Ar = Ect.InSide_Certificate_Ar,
                                InSide_Certificate_En = Ect.InSide_Certificate_En,
                                IsActive = Ect.IsActive,
                                EX_Constrain_Country_Item_ID = Ect.EX_Constrain_Country_Item_ID,
                                IsCertificate_Addtion = Ect.IsCertificate_Addtion,
                                Constrain_Type_Name = Ect.EX_Constrain_Country_Item.Ar_Name
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
                var Cmodel = uow.Repository<EX_Constrain_Text>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<EX_Constrain_Text>().Update(Cmodel);
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

        public bool GetAny(EX_Constrain_Text_DTO entity)
        {
            var obj = entity as EX_Constrain_Text_DTO;
            return uow.Repository<EX_Constrain_Text>().GetAny(p => p.User_Deletion_Id == null && (obj.ID == 0 ? true : p.ID != obj.ID) &&
                                        ((p.ConstrainText_Ar == obj.ConstrainText_Ar || p.ConstrainText_En == obj.ConstrainText_En)));
        }

        public Dictionary<string, object> Insert(EX_Constrain_Text_DTO entity, List<string> Device_Info)
        {
            try
            {
                entity.ConstrainText_Ar = entity.ConstrainText_Ar.Trim();
                entity.ConstrainText_En = entity.ConstrainText_En.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("EX_Constrain_Text_seq");
                    entity.ID = id;
                    var CModel = Mapper.Map<EX_Constrain_Text>(entity);
                    uow.Repository<EX_Constrain_Text>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(EX_Constrain_Text_DTO entity, List<string> Device_Info)
        {
            try
            {
                entity.ConstrainText_Ar = entity.ConstrainText_Ar.Trim();
                entity.ConstrainText_En = entity.ConstrainText_En.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as EX_Constrain_Text_DTO;
                    EX_Constrain_Text CModel = uow.Repository<EX_Constrain_Text>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    ////if (CModel.User_Updation_Id != null)
                    ////{
                    //    obj.User_Updation_Date = DateTime.Now;
                    //    obj.User_Updation_Id = CModel.User_Updation_Id;
                    //}
                    //obj.IsCertificate_Addtion = entity.IsCertificate_Addtion;
                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<EX_Constrain_Text>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<EX_Constrain_Text, EX_Constrain_Text_DTO>(Co);
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
                var data = uow.Repository<EX_Constrain_Text>().
                    GetData().Where(a => a.ID == id).Select(a =>
                    new EX_Constrain_Text_DTO()
                    {
                        ID=a.ID,
                        ConstrainText_Ar = a.ConstrainText_Ar,
                        ConstrainText_En = a.ConstrainText_En,
                        InSide_Certificate_Ar = a.InSide_Certificate_Ar,
                        InSide_Certificate_En = a.InSide_Certificate_En,
                        IsActive = a.IsActive,
                        IsAcceppted = a.IsAcceppted ?? false,
                        EX_Constrain_Country_Item_ID = a.EX_Constrain_Country_Item_ID,
                        IsCertificate_Addtion = a.IsCertificate_Addtion,
                        Constrain_Type_Name = a.EX_Constrain_Country_Item.Ar_Name,
                        EX_Constrain_Type_ID = a.EX_Constrain_Country_Item.EX_Constrain_Type_ID
                    }).SingleOrDefault();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        
        public Dictionary<string, object> GetBy_EX_Constrain_TypeId(long EX_Constrain_Type_id, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = new List<CustomOptionLongId2>();
                data = uow.Repository<EX_Constrain_Text>().GetData().Where(g => g.User_Deletion_Id == null
                && g.IsActive && g.EX_Constrain_Country_Item_ID == EX_Constrain_Type_id).Select(c => new CustomOptionLongId2
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