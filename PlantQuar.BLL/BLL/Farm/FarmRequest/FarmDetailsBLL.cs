using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmRequest
{
    public class FarmDetailsBLL
    {
        private UnitOfWork uow;
        public FarmDetailsBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetFarmData(long farmCountryRequestId, List<string> Device_Info)
        {

            try
            {

                //Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                //paramters_Type.Add("farmCountryRequestId", SqlDbType.BigInt);
                //paramters_Type.Add("Language_IsAr", SqlDbType.NVarChar);



                //Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                //paramters_Data.Add("farmCountryRequestId", farmCountryRequestId.ToString());
                //paramters_Data.Add("Language_IsAr", Device_Info[2]);  //"2018-12-26"


                //var data = uow.Repository<Farm_Get_Data_ResultDTO>().CallStored("Farm_Get_Data", paramters_Type,
                //paramters_Data, Device_Info).FirstOrDefault();

                var lang = Device_Info[2];
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var details = (from fr in entities.Farm_Request
                               join fd in entities.FarmsDatas on fr.FarmsData_ID equals fd.ID
                               join v in entities.Villages on fd.Village_ID equals v.ID into vs
                               join c in entities.Centers on fd.Center_Id equals c.ID into cs
                               join g in entities.Governates on fd.Govern_ID equals g.ID
                               join fc in entities.Farm_Company on fd.ID equals fc.Farm_ID
                               join cm in entities.Company_National on fc.Company_ID equals cm.ID
                               from v in vs.DefaultIfEmpty()
                               from c in cs.DefaultIfEmpty()
                               where fr.ID == farmCountryRequestId
                               select new Farm_Get_Data_ResultDTO
                               {
                                   requestId = fr.ID,
                                   farmId = fd.ID,
                                   farmName = lang =="1"? fd.Name_Ar:fd.Name_En,
                                   farmAddress = lang == "1" ?fd.Address_Ar : fd.Address_En,
                                   villageName = lang == "1" ? v.Ar_Name :v.En_Name,
                                   centerName = lang == "1" ? c.Ar_Name : c.En_Name,
                                   governorateName = lang == "1" ?g.Ar_Name :g.En_Name,
                                   companyName = lang == "1" ?cm.Name_Ar:cm.Name_En,
                                   Start_Date = fc.Start_Date,
                                   End_Date = fc.End_Date,
                                   IsAcceppted = fr.IsAcceppted
                               }).FirstOrDefault();


                details.countryName  = uow.Repository<Farm_Country>().GetData().Include(s => s.Country).Where(f => f.Farm_Request_ID == farmCountryRequestId).Select(c => c.Country.Ar_Name).ToList();

                details.farmItemCategories = uow.Repository<Farm_ItemCategories>().GetData().Include(b=>b.ItemCategory).Where(f => f.Farm_ID == details.farmId).Select(v => new farmItemCategories
                {
                    ItemCategories_ID = v.ItemCategories_ID,
                    Area_Acres = v.Area_Acres,
                    Quantity_Ton = v.Quantity_Ton,
                    categoryName = v.ItemCategory.Name_Ar

                }).ToList();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, details);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> ApproveFarm(Farm_Get_Data_ResultDTO dto, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var CountRequst = (from fd in entities.Farm_Request
                                   where fd.FarmsData_ID == dto.farmId
                                   && fd.Is_Final_requst != true
                                   select fd).Count();
                if (CountRequst == 0)
                {
                    Farm_Request CModel = uow.Repository<Farm_Request>().Findobject(dto.requestId);
                    CModel.IsAcceppted = dto.IsAcceppted;

                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Farm_Request, FarmRequestDTO>(CModel);
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
        public bool GetAny(Farm_CommitteeDTO entity)
        {
            //var sdsds=  Is_Final_requst
            return uow.Repository<Farm_Request>().GetAny(p => (p.Is_Final_requst == true && p.ID== entity.Farm_Request_ID));
            //return uow.Repository<Ex_RequestCommittee>().GetAny(p => (p.User_Deletion_Id == null )&& (obj.ID == 0 ? true : p.ID != obj.ID));
            
        }
        public Dictionary<string, object> Insert(Farm_CommitteeDTO entity, List<string> Device_Info)
        {
            try
            {
                //if (!GetAny(entity))
                //{
                    //var CountRequst = uow.Repository<Farm_Request>().GetCount();
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                   
                    //var CountRequst = (from fd in entities.Farm_Request
                    //                   where fd.FarmsData_ID == entity.FarmsData_ID
                    //                   &&fd.Is_Final_requst != true
                    //                   select fd).Count();
                    //if (CountRequst == 0)
                    //{
                        var CModel = Mapper.Map<Farm_Committee>(entity);
                        CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_seq");

                        var req_com = uow.Repository<Farm_Committee>().InsertReturn(CModel);
                        uow.SaveChanges();
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                //    }
                ////}
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
    }
}
