using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Admin
{
  public  class Table_Action_LogBLL
    {
        PlantQuarantineEntities entities = new PlantQuarantineEntities(); 

        private UnitOfWork uow;
        
        public Table_Action_LogBLL()
        {

            uow = new UnitOfWork();
        }



        public Dictionary<string,object> getAllTableNameLog( List<string> Device_Info)
        {

            try
            {

            
            var tablesLoged = uow.Repository<Fees_TableName>().GetData().ToList();
                var dataDTO = tablesLoged.Select(Mapper.Map<Fees_TableName, LogTableNamecsDTO>).ToList();
                dataDTO.Insert(0, new LogTableNamecsDTO(){ Id = 0, TableName=null, Description=null });
                //Table_Action_Log_API
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dataDTO);
        }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int) DTO.HelperClasses.Enums.Error.Exception, null);
    }


           



        }


          public Dictionary<string,object> getLogBYID(decimal id ,int tableName, List<string> Device_Info)
        {

            var tablesLoged= new List<logDataDTO>();

            try
            {

                /*
                 * tableName values
                 1->FarmRequest
                 2->FarmCommitte
                3->Im_PermissionRequest

                 */
                if (tableName == 3)
                {
                      tablesLoged = (

                   from Im_log in entities.Table_Action_Log
                   join Im_request in entities.Im_PermissionRequest
                   on Im_log.ID_TableActionValue equals Im_request.ID
                   join Im_action in entities.Table_Action
                   on Im_log.ID_Table_Action equals Im_action.ID
                   where Im_request.ImPermission_Number == id
                   select new logDataDTO
                   {
                       ID = Im_log.ID,
                       ImPermission_Number = Im_request.ImPermission_Number,
                       Name_Ar = Im_action.Name_Ar,
                       User_Creation_Date = Im_log.User_Creation_Date
                   }
                   ).ToList();

                }
                else if (tableName == 1)
                {
                      tablesLoged = (

                   from Im_log in entities.Table_Action_Log
                   join Im_request in entities.Farm_Request
                   on Im_log.ID_TableActionValue equals Im_request.ID
                   join Im_action in entities.Table_Action
                   on Im_log.ID_Table_Action equals Im_action.ID
                   where Im_request.FarmsData_ID == id
                   select new logDataDTO
                   {
                       ID = Im_log.ID,
                       ImPermission_Number = Im_request.FarmsData_ID,
                       Name_Ar = Im_action.Name_Ar,
                       User_Creation_Date = Im_log.User_Creation_Date
                   }
                   ).ToList();
                }
                else if (tableName == 2)
                {
                      tablesLoged = (

                   from Im_log in entities.Table_Action_Log
                   join Im_request in entities.Farm_Committee
                   on Im_log.ID_TableActionValue equals Im_request.ID
                   join Im_action in entities.Table_Action
                   on Im_log.ID_Table_Action equals Im_action.ID
                   where Im_request.Farm_Request_ID == id
                   select new logDataDTO
                   {
                       ID = Im_log.ID,
                       ImPermission_Number = Im_request.Farm_Request_ID,
                       Name_Ar = Im_action.Name_Ar,
                       User_Creation_Date = Im_log.User_Creation_Date
                   }
                   ).ToList();
                }
               
               
                
                
                
                
                
                
                
                
              //  var dataDTO = tablesLoged.Select(Mapper.Map<Fees_TableName, LogTableNamecsDTO>).ToList();

                //Table_Action_Log_API
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, tablesLoged);
        }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int) DTO.HelperClasses.Enums.Error.Exception, null);
    }


           



        }







    }
}
