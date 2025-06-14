using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.DataEntry.Committees;
using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Export_Constrains
{

    public class Ex_ConstrainActivationBLL : IGenericBLL<Ex_ConstrainActivationDTO>
    {
        private UnitOfWork uow;

        public Ex_ConstrainActivationBLL()
        {
            uow = new UnitOfWork();
        }

        
       
        public Dictionary<string, object>  GetAll(long Item_ShortName, long catId, int constrainType, int owner, int pageSize, int index, string jtSorting, List<string> Device_Info)

        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                //var data = new List<Ex_CountryConstrain>();
                Int64 data_Count = 0;
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                long? categoryId = null;
                if (catId > 0) categoryId = catId;

                var data = (from cc in entities.Ex_CountryConstrain
                            join isn in entities.Item_ShortName on cc.Item_ShortName_id equals isn.ID
                            where  cc.Import_Country_ID == owner 
                            && cc.Item_ShortName_id == Item_ShortName
                            && cc.User_Deletion_Id == null
                            && cc.IsActive == true
                            && cc.ItemCategories_ID == categoryId

                            select new Ex_ConstrainActivationDTO
                            {
                                ID = cc.ID,
                                Import_Country_ID = cc.Import_Country_ID,                              
                                IsActive = cc.IsActive,
                                //28-6-2020 constrain updates
                                //IsCertificate_Addtion = cc.IsCertificate_Addtion,
                                IsFarmAccreditation = cc.IsFarmAccreditation,
                                IsCompanyAccreditation = cc.IsCompanyAccreditation,
                                IsStationAccreditation = cc.IsStationAccreditation,

                                ArrivalPortList = cc.Ex_CountryConstrain_ArrivalPort.
                                Where(z => z.User_Deletion_Id == null && z.Ex_CountryConstrain_Id == cc.ID)
                                .Select(z => z.Port_International.ID).ToList(),

                                CountryID = cc.Ex_CountryConstrain_ArrivalPort.Where(a => a.User_Deletion_Id == null).Select
                                (c => c.Port_International.Country_ID).FirstOrDefault()

                            }
                          ).ToList();


                
                string lang = Device_Info[2];
                var dataDto = data.Skip(index).Take(pageSize);

                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
               dic.Add("CommitteeType_Data", dataDto);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //Get Any CommitteeType
        public bool GetAny(CommitteeTypeDTO entity)
        {
            var obj = entity as CommitteeTypeDTO;
            obj.Name_Ar = obj.Name_Ar.Trim();
            obj.Name_En = obj.Name_En.Trim();
            return uow.Repository<CommitteeType>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Name_Ar == obj.Name_Ar || p.Name_En == obj.Name_En))
                                        && (obj.ID == 0 ? true : p.ID != obj.ID));
        }
        //Create CommitteeType
        public Dictionary<string, object> Insert(CommitteeTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();

                if (!GetAny(entity))
                {
                    entity.ID = uow.Repository<Object>().GetNextSequenceValue_Byte("CommitteeType_seq");
                    var CModel = Mapper.Map<CommitteeType>(entity);
                    uow.Repository<CommitteeType>().InsertRecord(CModel);
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

        //Update CommitteeType
        public Dictionary<string, object> Update(CommitteeTypeDTO entity, List<string> Device_Info)
        {
            try
            {
                entity.Name_Ar = entity.Name_Ar.Trim();
                entity.Name_En = entity.Name_En.Trim();

                if (!GetAny(entity))
                {
                    var obj = entity as CommitteeTypeDTO;
                    CommitteeType CModel = uow.Repository<CommitteeType>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<CommitteeType>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<CommitteeType, CommitteeTypeDTO>(Co);
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

        //Delete CommitteeType
        public Dictionary<string, object> Delete(DeleteParameters obj, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<CommitteeType>().Findobject(obj.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = obj._DateNow;
                    Cmodel.User_Deletion_Id = obj.Userid;
                    uow.Repository<CommitteeType>().Update(Cmodel);
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

        //DROPS
        //Get CommitteeType List DDL
        public Dictionary<string, object> FillCommitteeType_List(int Lst, List<string> Device_Inf)
        {

            var data = uow.Repository<CommitteeType>().GetData().Where(x => x.User_Deletion_Id == null)
                .Select(c => new CustomOptionShortId { DisplayText = c.Name_Ar, Value = c.ID }).ToList();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        //Get CommitteeType Create & Update DDL
        public Dictionary<string, object> FillCommitteeType_AddEdit(List<string> Device_Inf)
        {
            string lang = Device_Inf[2];
            var data = uow.Repository<CommitteeType>().GetData().Where(x => x.User_Deletion_Id == null).Select(c => new CustomOptionShortId { DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En), Value = c.ID }).ToList();
            CustomOptionShortId empty = new CustomOptionShortId();
            empty.Value = null;
            empty.DisplayText = "-";
            data.Insert(0, empty);
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }

        public Dictionary<string, object> Update_Activation(Ex_CountryConstrainDTO entity, List<string> Device_Info)
        {
            try
            {
                Ex_CountryConstrain CModel = uow.Repository<Ex_CountryConstrain>().Findobject(entity.ID);
                var constrainsList = uow.Repository<Ex_CountryConstrain>().GetData()
                    .Where(c =>c.Import_Country_ID == CModel.Import_Country_ID &&
                c.Item_ShortName_id == CModel.Item_ShortName_id
                // c.IsPlant == CModel.IsPlant &&
                //c.ProdPlant_ID == CModel.ProdPlant_ID
                && c.User_Deletion_Id == null).ToList();
                foreach (var con in constrainsList)
                {
                    Ex_CountryConstrain CModel2 = uow.Repository<Ex_CountryConstrain>().Findobject(con.ID);
                    CModel2.User_Updation_Date = entity.User_Updation_Date;
                    CModel2.User_Updation_Id = entity.User_Updation_Id;
                    CModel2.IsActive = entity.IsActive;

                    //28-6-2020 constrain updates
                    //CModel2.IsCertificate_Addtion = entity.IsCertificate_Addtion;
                    CModel2.IsFarmAccreditation = entity.IsFarmAccreditation;
                    CModel2.IsCompanyAccreditation = entity.IsCompanyAccreditation;
                    CModel2.IsStationAccreditation = entity.IsStationAccreditation;
                }



                #region Arrival port
                //11-9-2019 update centers
                if (entity.ArrivalPortList.Count > 0)
                {
                    foreach (var con in constrainsList)
                    {
                        UpdateArrivalPortList(entity.ArrivalPortList, con.ID,
                     CModel.User_Updation_Id, entity.User_Updation_Date, Device_Info);
                    }
                    //   UpdateArrivalPortList(entity.ArrivalPortList, CModel.ID,
                    //CModel.User_Updation_Id, entity.User_Updation_Date, Device_Info);
                }
                #endregion

                uow.Repository<Ex_CountryConstrain>().Update(CModel);
                uow.SaveChanges();



                var empDTO = Mapper.Map<Ex_CountryConstrain, Ex_CountryConstrainDTO>(CModel);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        private void UpdateArrivalPortList(List<int> arrivalPortList,
           long Ex_CountryConstrain_Id, long? user_id, DateTime? Date_Now, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Ex_CountryConstrain_ArrivalPort>().GetData().Where(x => x.Ex_CountryConstrain_Id == Ex_CountryConstrain_Id
                && x.User_Deletion_Id == null).ToList();
                var addlst = arrivalPortList.Except(data.Select(x => x.Port_International_Id)).ToList();
                var deletelst = data.Where(x => arrivalPortList.IndexOf(x.Port_International_Id) == -1).ToList();
                InsertArrivalPortList((long)user_id, Date_Now, Ex_CountryConstrain_Id, addlst, Device_Info);
                foreach (var item in deletelst)
                {
                    item.User_Deletion_Date = Date_Now;
                    item.User_Deletion_Id = user_id;
                    uow.Repository<Ex_CountryConstrain_ArrivalPort>().Update(item);
                    uow.SaveChanges();
                    //DeleteArrivalPortList(item, Device_Info);
                }
                //  return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, arrivalPortList);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                //  return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }
        }

        private void InsertArrivalPortList(long user_id, DateTime? Date_Now, long Ex_CountryConstrain_Id,
          List<int> Ex_CountryConstrain_ArrivalPortLst, List<string> device_Info)
        {
            try
            {
                Ex_CountryConstrain_ArrivalPort dto;
                foreach (var item in Ex_CountryConstrain_ArrivalPortLst)
                {
                    dto = new Ex_CountryConstrain_ArrivalPort();
                    dto.Port_International_Id = item;
                    dto.Ex_CountryConstrain_Id = Ex_CountryConstrain_Id;
                    dto.User_Creation_Date = (DateTime)Date_Now;
                    dto.User_Creation_Id = user_id;
                    dto.Id = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CountryConstrain_ArrivalPort_seq");
                    uow.Repository<Ex_CountryConstrain_ArrivalPort>().InsertRecord(dto);
                    uow.SaveChanges();
                }
                //  return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Ex_CountryConstrain_ArrivalPortLst);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, device_Info);
                //   return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Insert(Ex_ConstrainActivationDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Update(Ex_ConstrainActivationDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(Ex_ConstrainActivationDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}
