using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Import.Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Import.Constrains
{
    public class Im_CountryConstrainBLL : IGenericBLL<Im_CountryConstrain_TextDTO>
    {
        private UnitOfWork uow;

        public Im_CountryConstrainBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Im_CountryConstrain_Text entity = uow.Repository<Im_CountryConstrain_Text>().Findobject(Id);
                var empDTO = Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>(entity);
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
            var Im_CountryConstrain_Text = uow.Repository<Im_CountryConstrain_Text>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, Im_CountryConstrain_Text);
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var dataDTO = new List<Im_CountryConstrain_TextDTO>();
                string lang = Device_Info[2];

                if (pageSize == -1 && index == -1)
                {
                    dataDTO = uow.Repository<Im_CountryConstrain_TextDTO>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En)).Select(a => new Im_CountryConstrain_TextDTO
                    {
                        ConstrainText_Ar = a.ConstrainText_Ar,
                        ConstrainText_En = a.ConstrainText_En,
                        InSide_Certificate_Ar = a.InSide_Certificate_Ar,
                        InSide_Certificate_En = a.InSide_Certificate_En

                    }).ToList();
                }
                else
                {
                    dataDTO = uow.Repository<Im_CountryConstrain_Text>().GetData().Where(a => a.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.ConstrainText_Ar : A.ConstrainText_En)).
                        Select(a => new Im_CountryConstrain_TextDTO
                        {
                            ConstrainText_Ar = a.ConstrainText_Ar,
                            ConstrainText_En = a.ConstrainText_En,
                            InSide_Certificate_Ar = a.InSide_Certificate_Ar,
                            InSide_Certificate_En = a.InSide_Certificate_En
                        }).Skip(index).Take(pageSize).ToList();
                }

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
            //    var data = uow.Repository<Center>().GetData().Where(p => p.User_Deletion_Id == null
            // // get undeleted parent
            // && p.Governate.User_Deletion_Id == null).OrderBy(A => (lang == "1" ? A.Ar_Name : A.En_Name)).Skip(index).Take(pageSize).ToList();
            //    //var data = uow.Repository<Center>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
            //    var dataDTO = data.Select(Mapper.Map<Center, CenterDTO>);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dataDTO);
            //}
            //catch (Exception ex)
            //{
            //    uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            //}
        }

        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, string jtSorting, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                var data = new List<Im_CountryConstrain_Text>();
                Int64 data_Count = 0;

                if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_CountryConstrain_Text>().GetData().Where(a => a.User_Deletion_Id == null &&
                    a.ConstrainText_En.StartsWith(enName)
                 && a.User_Deletion_Id == null).ToList();
                }
                else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_CountryConstrain_Text>().GetData().Where(a => a.User_Deletion_Id == null &&
                                      a.ConstrainText_Ar.StartsWith(arName)  // get undeleted parent
                                   && a.User_Deletion_Id == null).ToList();
                }
                else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                {
                    data = uow.Repository<Im_CountryConstrain_Text>().GetData().Where(a => a.User_Deletion_Id == null
                                   // get undeleted parent
                                   && a.User_Deletion_Id == null).ToList();
                }
                else
                {
                    data = uow.Repository<Im_CountryConstrain_Text>().GetData().Where(a => a.User_Deletion_Id == null &&
                             (a.ConstrainText_Ar.StartsWith(arName) && a.ConstrainText_En.StartsWith(enName))
                                  // get undeleted parent
                                  && a.User_Deletion_Id == null).ToList();
                }
                switch (jtSorting)
                {
                    case "Ar_Name ASC":
                        data = data.OrderBy(t => t.ConstrainText_Ar).ToList();
                        break;
                    case "Ar_Name DESC":
                        data = data.OrderByDescending(t => t.ConstrainText_Ar).ToList();
                        break;
                    case "En_Name ASC":
                        data = data.OrderBy(t => t.ConstrainText_En).ToList();
                        break;
                    case "En_Name DESC":
                        data = data.OrderByDescending(t => t.ConstrainText_En).ToList();
                        break;


                }
                string lang = Device_Info[2];
                //var dataDto = data.Skip(index).Take(pageSize).Select(Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>);
                data_Count = data.Count();

                dic.Add("Count_Data", data_Count);
                dic.Add("Im_CountryConstrain_Text_Data", data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
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
                var Cmodel = uow.Repository<Im_CountryConstrain_Text>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Im_CountryConstrain_Text>().Update(Cmodel);
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

        public bool GetAny(Im_CountryConstrain_TextDTO entity)
        {
            var obj = entity as Im_CountryConstrain_TextDTO;
            return uow.Repository<Im_CountryConstrain_Text>().GetAny(p => p.User_Deletion_Id == null && (obj.ID == 0 ? true : p.ID != obj.ID) &&
                                        ((p.ConstrainText_Ar == obj.ConstrainText_Ar || p.ConstrainText_En == obj.ConstrainText_En)));
        }

        public Dictionary<string, object> Insert(Im_CountryConstrain_TextDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.ConstrainText_Ar = entity.ConstrainText_Ar.Trim();
                entity.ConstrainText_En = entity.ConstrainText_En.Trim();
                if (!GetAny(entity))
                {
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CountryConstrain_Text_seq");
                    entity.ID = id;
                    var CModel = Mapper.Map<Im_CountryConstrain_Text>(entity);
                    uow.Repository<Im_CountryConstrain_Text>().InsertRecord(CModel);
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
        public Dictionary<string, object> Update(Im_CountryConstrain_TextDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.ConstrainText_Ar = entity.ConstrainText_Ar.Trim();
                entity.ConstrainText_En = entity.ConstrainText_En.Trim();
                if (!GetAny(entity))
                {
                    var obj = entity as Im_CountryConstrain_TextDTO;
                    Im_CountryConstrain_Text CModel = uow.Repository<Im_CountryConstrain_Text>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Im_CountryConstrain_Text>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Im_CountryConstrain_Text, Im_CountryConstrain_TextDTO>(Co);
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
            data = uow.Repository<Im_Constrain_Type>().GetData().Where(g => g.User_Deletion_Id == null&&g.IsActive
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
                var data = uow.Repository<Im_CountryConstrain_Text>().
                    GetData().Where(a => a.ID == id).Select(a => 
                    new Im_CountryConstrain_TextDTO()
                {
                    ConstrainText_Ar = a.ConstrainText_Ar,
                    ConstrainText_En = a.ConstrainText_En,
                    InSide_Certificate_Ar = a.InSide_Certificate_Ar,
                    InSide_Certificate_En = a.InSide_Certificate_En,
                    IsActive = a.IsActive,
                    IsAcceppted = a.IsAcceppted ?? false,
                    Im_Constrain_Type_ID = a.Im_Constrain_Type_ID
                }).SingleOrDefault();

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