using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantQuar.DTO.HelperClasses;
using System.Reflection;
using PlantQuar.DTO.DTO.DataEntry.Countries;

namespace PlantQuar.BLL.BLL.DataEntry.Countries
{
    public class Union_CountryBLL : IGenericBLL<Union_CountryDTO>
    {
        private UnitOfWork uow;

        public Union_CountryBLL()
        {
            uow = new UnitOfWork();
        }
      public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Union_Country entity = uow.Repository<Union_Country>().Findobject(Id);
                var empDTO = Mapper.Map<Union_Country,Union_CountryDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Union_Country>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Country.User_Deletion_Id == null
                && p.Union.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, count);
        }
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Union_Country>().GetData().Where(p => p.User_Deletion_Id == null
               // get undeleted parent
               && p.Country.User_Deletion_Id == null
                && p.Union.User_Deletion_Id == null).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                //var data = uow.Repository<Union_Country>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<Union_Country, Union_CountryDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                //var data = uow.Repository<Union_Country>().GetData().Where(a => a.User_Deletion_Id == null).ToList();

                //if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
                //{
                //    data = data.Where(a => a.Name_En.StartsWith(enName)).ToList();
                //}
                //else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
                //{
                //    data = data.Where(a => a.Name_Ar.StartsWith(arName)).ToList();
                //}
                //else
                //{
                //    data = data.Where(a => (a.Name_Ar.StartsWith(arName) || a.Name_En.StartsWith(enName))).ToList();
                //}

                //var dataDto = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<Union_Country, Union_CountryDTO>);
                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dataDto);

                // AYM In progress
                return null;
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<Union_Country>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<Union_Country>().Update(Cmodel);
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

        public bool GetAny(Union_CountryDTO obj)
        {
           return uow.Repository<Union_Country>().GetAny(p => p.User_Deletion_Id == null &&
                                     p.Country_ID==obj.Country_ID && p.Union_ID==obj.Union_ID
                                     && (obj.ID == 0 ? true : p.ID != obj.ID));

        }
        public void Deleterecord(Union_Country obj, List<string> Device_Info)
        {
            try
            {
                //var model = Mapper.Map<List<StationActivityCountry>>(lst);
                uow.Repository<Union_Country>().Update(obj);
                uow.SaveChanges();
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
            }
        }
        public Dictionary<string, object> InsertRecords(short user_id, DateTime Date_Now, short Country_ID, List<short> objRecords, List<string> Device_Info)
        {
            try
            {
                Union_CountryDTO dto;
                foreach (var item in objRecords)
                {
                    dto = new Union_CountryDTO();
                    dto.Union_ID = item;

                    var id = uow.Repository<Object>().GetNextSequenceValue_Short("Union_Country_seq");
                    dto.ID = id;
                                 
                    dto.Country_ID = Country_ID;
                    dto.User_Creation_Date = Date_Now;
                    dto.User_Creation_Id = user_id;
                    dto.IsActive = true;
                    Insert((dto), Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, objRecords);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> UpdateRecords(short user_id, DateTime Date_Now, short Country_ID, List<short> objRecords, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Union_Country>().GetData().Where(x => x.Country_ID == Country_ID && x.User_Deletion_Id == null && x.IsActive==true).ToList();
                var addlst = objRecords.Except(data.Select(x => x.Union_ID)).ToList();
                var deletelst = data.Where(x => objRecords.IndexOf(x.Union_ID) == -1).ToList();
                InsertRecords(user_id, Date_Now, Country_ID, addlst, Device_Info);
                foreach (var item in deletelst)
                {
                    item.User_Deletion_Date = Date_Now;
                    item.User_Deletion_Id = user_id;
                    Deleterecord(item, Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, objRecords);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }

        }

        //******************************************//
        public Dictionary<string, object> Insert(Union_CountryDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<Union_Country>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Short("Union_Country_Seq");
                    uow.Repository<Union_Country>().InsertRecord(CModel);
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
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InsertArr(Union_CountryDTO newobject2, List<string> Device_Info)
        {
            try
            {


                var CModel = Mapper.Map<Union_Country>(newobject2);
                CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Short("Union_Country_Seq");
                uow.Repository<Union_Country>().InsertRecord(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, CModel);

            }

            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }

        }
        public Dictionary<string, object> Update(Union_CountryDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as Union_CountryDTO;
                    Union_Country CModel = uow.Repository<Union_Country>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Union_Country>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Union_Country,Union_CountryDTO>(Co);
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
    }
}
