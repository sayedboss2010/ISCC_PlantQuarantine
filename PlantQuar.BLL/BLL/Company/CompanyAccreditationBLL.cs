using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Company
{
    public class CompanyAccreditationBLL : IGenericBLL<CompanyAccreditationDTO>
    {
        private UnitOfWork uow;

        public CompanyAccreditationBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                CompanyAccreditation entity = uow.Repository<CompanyAccreditation>().Findobject(Id);
                var empDTO = Mapper.Map<CompanyAccreditation, CompanyAccreditationDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<CompanyAccreditation>().GetData().Where(p => p.User_Deletion_Id == null).Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, count);
        }


        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<CompanyAccreditation>().GetData().Where(a => a.User_Deletion_Id == null
                        // get undeleted parent
                        && a.Country.User_Deletion_Id == null &&
                        a.Country.User_Deletion_Id == null
                                  //  && a.Plant.User_Deletion_Id == null
                                  ).OrderBy(A => A.ID).Skip(index).Take(pageSize).ToList();
                var dataDTO = data.Select(Mapper.Map<CompanyAccreditation, CompanyAccreditationDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public bool GetAny(CompanyAccreditationDTO obj)
        {
            //  var obj = entity as CompanyAccreditationDTO;
            return uow.Repository<CompanyAccreditation>().GetAny(p => (p.User_Deletion_Id == null &&
                                        (p.Item_ShortName_id == obj.Item_ShortName_id && p.Company_ID == obj.Company_ID &&
                                        p.Country_ID == obj.Country_ID)) && (obj.ID == 0 ? true : p.ID != obj.ID));
        }

        public Dictionary<string, object> Insert(CompanyAccreditationDTO entity, List<string> Device_Info)
        {
            try
            {


                if (!GetAny(entity))
                {
                    var CModel = Mapper.Map<CompanyAccreditation>(entity);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("CompanyAccreditation_seq");
                    uow.Repository<CompanyAccreditation>().InsertRecord(CModel);
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

        public Dictionary<string, object> Delete(DeleteParameters dto, List<string> Device_Info)
        {
            try
            {
                var Cmodel = uow.Repository<CompanyAccreditation>().Findobject(dto.id);
                if (Cmodel != null)
                {
                    Cmodel.User_Deletion_Date = dto._DateNow;
                    Cmodel.User_Deletion_Id = dto.Userid;
                    uow.Repository<CompanyAccreditation>().Update(Cmodel);
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

        public Dictionary<string, object> Update(CompanyAccreditationDTO entity, List<string> Device_Info)
        {
            try
            {
                if (!GetAny(entity))
                {
                    var obj = entity as CompanyAccreditationDTO;
                    CompanyAccreditation CModel = uow.Repository<CompanyAccreditation>().Findobject(obj.ID);

                    obj.User_Creation_Date = CModel.User_Creation_Date;
                    obj.User_Creation_Id = CModel.User_Creation_Id;
                    if (CModel.User_Updation_Id != null)
                    {
                        obj.User_Updation_Date = CModel.User_Updation_Date;
                        obj.User_Updation_Id = CModel.User_Updation_Id;
                    }

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<CompanyAccreditation>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<CompanyAccreditation, CompanyAccreditationDTO>(Co);
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

        //public Dictionary<string, object> GetAll(string arName, string enName, int pageSize, int index, List<string> Device_Info)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = new Dictionary<string, object>();
        //        var data = new List<CompanyAccreditation>();
        //        Int64 data_Count = 0;

        //        if (string.IsNullOrEmpty(arName) && !string.IsNullOrEmpty(enName))
        //        {
        //            data = data = uow.Repository<CompanyAccreditation>().GetData().Where(a =>
        //               a.En_Name.StartsWith(enName.Trim()) &&
        //            a.User_Deletion_Id == null).ToList();

        //        }
        //        else if (!string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
        //        {
        //            //data = data.Where(a => a.Ar_Name.StartsWith(arName.Trim())).ToList();

        //            data = data = uow.Repository<CompanyAccreditation>().GetData().Where(a =>
        //                 a.Ar_Name.StartsWith(arName.Trim()) &&
        //              a.User_Deletion_Id == null).ToList();
        //        }
        //        else if (string.IsNullOrEmpty(arName) && string.IsNullOrEmpty(enName))
        //        {
        //            data = uow.Repository<CompanyAccreditation>().GetData().Where(a => a.User_Deletion_Id == null).ToList();
        //            data_Count = data.Count();
        //        }
        //        else
        //        {
        //            //data = data.Where(a => (a.Ar_Name.StartsWith(arName) || a.En_Name.StartsWith(enName))).ToList();
        //            data = uow.Repository<CompanyAccreditation>().GetData().Where(a =>
        //            (a.Ar_Name.StartsWith(arName) && a.En_Name.StartsWith(enName)) &&
        //          a.User_Deletion_Id == null).ToList();
        //        }

        //        var dataDto = data.OrderBy(A => A.ID).Skip(index).Take(pageSize).Select(Mapper.Map<CompanyAccreditation, CompanyAccreditationDTO>);

        //        data_Count = data.Count();
        //        dic.Add("Count_Data", data_Count);
        //        dic.Add("Shipment_Mean_Data", dataDto);

        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        public Dictionary<string, object> GetAllbyCompanyNationalID(long Company_ID, int pageSize, int index, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities pp = new PlantQuarantineEntities();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = new List<CompanyAccreditationDTO>();
                Int64 data_Count = 0;
                data = (from ee in pp.CompanyAccreditations
                        where (ee.User_Deletion_Id == null && ee.Company_ID == Company_ID)
                        select new CompanyAccreditationDTO
                        {
                            ID = ee.ID,
                            Country_ID = ee.Country_ID,
                            StartDate = ee.StartDate,
                            EndDate = ee.EndDate,
                            Company_ID = ee.Company_ID,
                            itemTypeLst = ee.Item_ShortName.Item.Item_Type_ID,
                            groupLst = ee.Item_ShortName.Item.Group_ID,
                            plantLst = ee.Item_ShortName.Item_ID,
                            Item_ShortName_id = ee.Item_ShortName_id,
                            IsActive = ee.IsActive,
                        }).ToList();
                    
                    
                    //uow.Repository<CompanyAccreditation>().GetData().Where(a => a.User_Deletion_Id == null
                    //  // get undeleted parent
                    //  //&& a.Country.User_Deletion_Id == null &&
                    //  //a.Country.User_Deletion_Id == null
                    //  //&& a.Product.User_Deletion_Id == null
                    //  && a.Company_ID == Company_ID
                    //            ).OrderBy(A => A.ID).ToList();
               // var dataDTO = data.Skip(index).Take(pageSize).Select(Mapper.Map<CompanyAccreditation, CompanyAccreditationDTO>);
               //dataDTO.FirstOrDefault().itemTypeLst = 1;
                data_Count = data.Count();
                dic.Add("Count_Data", data_Count);
                dic.Add("CompanyAccreditation_Data", data);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
                //     return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
