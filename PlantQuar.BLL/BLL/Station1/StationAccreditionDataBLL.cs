using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.DTO.SystemCodes;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Station
{
   public class StationAccreditionDataBLL
    {
        private UnitOfWork uow;
        PlantQuarantineEntities entities = new PlantQuarantineEntities();


        public StationAccreditionDataBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetAccreditationTypes(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<A_SystemCode>().GetData().Where(p => p.SystemCodeTypeId == 21).
                    OrderBy(A => (lang == "1" ? A.ValueName : A.ValueNameEN)).ToList();
                //var data = uow.Repository<AnalysisLab>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
                var dataDTO = data.Select(Mapper.Map<A_SystemCode, A_SystemCodeDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetStationCheckLists(List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var data = uow.Repository<Station_CheckList>().GetData().Where(p => p.User_Deletion_Id == null) 
                    //OrderBy(A => (lang == "1" ? A.ValueName : A.ValueNameEN)).
                    . ToList();
                //var data = uow.Repository<AnalysisLab>().GetData(pageSize, index, A => 1 == 1).Where(p => p.User_Deletion_Id == null).ToList();
                var dataDTO = ///Mapper.Map<Station_CheckList, Station_CheckListDTO>(data) ;// (Mapper.Map<Station_CheckList, Station_CheckListDTO>)data;
                    data.Select(Mapper.Map<Station_CheckList, Station_CheckListDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dataDTO);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
         public Dictionary<string, object> GetTreatmentData(int ActivityTypeId,List<string> Device_Info)
         {
 



            var dd = (
                from s in entities.StationActivityTypes
                join t in entities.TreatmentMethods on s.TreatmentMethods_ID equals t.ID
                      join tt in entities.TreatmentTypes on t.TreatmentType_ID equals tt.ID
                      join tm in entities.TreatmentMainTypes on tt.MainType_ID equals tm.ID
                where s.ID == ActivityTypeId
                select new TreatmentDataDto
                {
                        Treatment_Main_Name_Ar=tm.Ar_Name,   
                        Treatment_Main_Name_En=tm.En_Name,   
                        Treatment_Type_Name_En=tt.Ar_Name,   
                        Treatment_Type_Name_Ar=tt.En_Name,   

                      }).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dd);

        }
          
         public Dictionary<string, object> GetCountryUnion(int UnionId,List<string> Device_Info)
         {

 


            var dd = (
                from uc in entities.Union_Country 
                join u in entities.Unions on uc.Union_ID equals u.ID
                      join c in entities.Countries on uc.Country_ID equals c.ID
                       
                where uc.ID == UnionId
                select new CustomOption
                {
                         DisplayText=c.Ar_Name,
                         Value=c.ID

                      }).ToList();
            dd.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dd);

        }
          
        public Dictionary<string, object> Insert(StationAccreditionDataDTO entity, List<string> Device_Info)
        {
            try
            {
               
               
                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Data_SEQ");

                 

                var model = new  Station_Accreditation_Data();
                entity.ID = id;
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


                entities.Station_Accreditation_Data.Add(model);

                // save the changes
                entities.SaveChanges();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);
                
                
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
          public Dictionary<string, object> InsertCountryAccredition(StationAccreditionDataCountryDTO dto, List<string> Device_Info)
        {
            try
            {

                for (int i=0;i<dto.CountryData.Count;i++)
                {

                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Data_Country_seq");
                    var model = new Station_Accreditation_Data_Country();
                    model.ID = id;
                    model.CountryID = dto.CountryData[i].CountryID;
                    model.IsActive = dto.CountryData[i].IsActive;
                     model.User_Creation_Date = dto.User_Creation_Date;
                    model.User_Creation_Id = dto.User_Creation_Id;                  
                     model.Station_Accreditation_Data_ID = dto.Station_Accreditation_Data_ID;

                    entities.Station_Accreditation_Data_Country.Add(model);
                    // save the changes
                    entities.SaveChanges();

                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dto);
                
                
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> InsertShortNameAccredition(List<Fees_Constrain_Data_Item_ShortNameDTO> shortNameList, List<string> Device_Info)
        {
            try
            {

                for (int i = 0; i < shortNameList.Count; i++)
                {

                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_Data_Item_ShortName_seq");
                    var model = new Station_Accreditation_Data_Item_ShortName();
                    model.ID = id;
                    model.Station_Accreditation_Data_ID = shortNameList[0].Station_Accreditation_Data_ID;
                    model.Item_ShortName_ID = shortNameList[i].Item_ShortName_ID;
                    model.IsActive =true;
                    model.User_Creation_Date = shortNameList[0].User_Creation_Date;
                    model.User_Creation_Id = shortNameList[0].User_Creation_Id;



                    


                    entities.Station_Accreditation_Data_Item_ShortName.Add(model);

                    // save the changes
                    entities.SaveChanges();

                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, shortNameList);


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
          public Dictionary<string, object> InsertConstrainListAccredition(
              List<Station_Accredition_CheckListDTO>  checkListDTOs, 
              List<string> Device_Info)
        {
            try
            {

                for (int i = 0; i < checkListDTOs.Count; i++)
                {

                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Station_Accreditation_CheckList_seq");
                    var model = new Station_Accreditation_CheckList();
                    model.ID = id;
                    model.Station_Accreditation_Data_ID = checkListDTOs[0].Station_Accreditation_Data_ID;
                    model.Station_CheckList_ID = checkListDTOs[i].Station_CheckList_ID;
                    model.IsActive =true;
                    model.User_Creation_Date = checkListDTOs[0].User_Creation_Date;
                    model.User_Creation_Id = checkListDTOs[0].User_Creation_Id;
                   


                    entities.Station_Accreditation_CheckList.Add(model);

                    // save the changes
                    entities.SaveChanges();

                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, checkListDTOs);


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


    }
}
