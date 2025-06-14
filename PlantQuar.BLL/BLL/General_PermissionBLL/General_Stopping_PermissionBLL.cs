using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Im_Permissions;
using PlantQuar.UOW.UnitOfWork;
using PlantQuar.WEB.App_Start;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static PlantQuar.DTO.DTO.General_Permissions.ActivePrintDTO;


namespace PlantQuar.BLL.BLL.General_PermissionBLL
{
    
    public class General_Stopping_PermissionBLL
    {
        private UnitOfWork uow;
        //dbPrivilageEntities entities1 = new dbPrivilageEntities();

        public General_Stopping_PermissionBLL()
        {
            uow = new UnitOfWork();
        }
        #region

        #endregion


        public Dictionary<string, object> GetImPermissionsList_filter_ActivePrint
          (long? ImPermission_Number,int OperationCode, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var permissions = (from ipr in entities.Im_PermissionRequest
                                   join rr in entities.Im_RequestData on ipr.ID equals rr.Im_PermissionRequest_ID
                                   join oo in entities.Im_OpertaionType on rr.Im_OperationType equals oo.ID into oos
                                   join co in entities.Countries on rr.ExportCountry_Id equals co.ID
                                   from oo in oos.DefaultIfEmpty()
                                   where ipr.ImPermission_Number == ImPermission_Number & oo.ID == OperationCode
                                   select new Stopping_PermissionsDTO
                                   {
                                       Im_PermissionRequest_ID = ipr.ID,
                                       Arrival_Date = ipr.Arrival_Date,
                                       IS_Print_Ar = ipr.IS_Print_Ar,
                                       IS_Print_EN = ipr.IS_Print_EN,

                                       ImPermission_Number = ipr.ImPermission_Number,

                                       operationTypeName = oo != null ? oo.Name_Ar : "",
                                       ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                                       Importer_ID = rr.Importer_ID,
                                       ImporterType_Id = rr.ImporterType_Id,
                                       IsAcceppted = ipr.IsAcceppted,
                                       IsPaid = ipr.IsPaid,
                                       Print_Count = ipr.Print_Count,
                                       Renewal_Status = ipr.Renewal_Status,
                                       Start_Date = ipr.Start_Date,
                                       End_Date = ipr.End_Date,
                                       Im_CheckRequest_ID = ipr.Im_CheckRequest_ID
                                   }).ToList();
                if (permissions.Count > 0)
                {
                    foreach (var per in permissions)
                    {
                        per.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == per.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault();
                        if (per.ImporterType_Id == 6)
                        {
                            per.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == per.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                        }
                        else if (per.ImporterType_Id == 7)
                        {
                            per.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == per.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                        }
                        else
                        {
                            per.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == per.Importer_ID).Select(s => s.Name).FirstOrDefault();

                        }

                        var initiator = uow.Repository<Im_PermissionItems>().GetData().Where(i => i.Im_PermissionRequest_ID == per.Im_PermissionRequest_ID).FirstOrDefault();
                        if (initiator != null)
                        {
                            var initiatorId = initiator.Im_Initiator_ID;
                            var initiatir = uow.Repository<Im_Initiator>().GetData().Where(u => u.ID == initiatorId).FirstOrDefault();

                            var qualId = initiatir.QualitativeGroup_Id;
                            if (qualId != null)
                            {
                                per.shortName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault().Name_Ar;
                            }

                            var itemShortNameId = initiatir.Item_ShortName_ID;
                            if (itemShortNameId != null)
                            {
                                var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId).FirstOrDefault();
                                per.shortName = lang == "1" ? itemShortName.ShortName_Ar : itemShortName.ShortName_En;
                            }
                        }

                    }

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, permissions);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.CehckData, null);
                    //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, null);

                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update(ActivePrintDto dto,   List<string> Device_Info)
        //public Dictionary<string, object> Update(ActivePrintDto dto, int OperationCode, List<string> Device_Info)
        {
            PlantQuarantineEntities db = new PlantQuarantineEntities();
            try
            {


                var f = db.Im_PermissionRequest.FirstOrDefault(x => x.ImPermission_Number ==
              dto.Im_PermissionRequestID);
                if (f != null)
                {
                    f.Im_CheckRequest_ID = 0;
                    //f.IS_Print_EN = dto.IS_Print_EN;



                    Table_Action_Log newLog = new Table_Action_Log();
                    var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Table_Action_Log_seq");
                    newLog.ID = idd;
                    newLog.ID_Table_Action = 9;
                    newLog.NOTS = "Active Im_PermissionRequest Print  ";
                    newLog.ID_TableActionValue = f.ID;
                    newLog.User_Creation_Id = dto.User_Creation_Id;
                    newLog.User_Creation_Date = DateTime.Now;
                    uow.Repository<Table_Action_Log>().InsertRecord(newLog);

                    uow.SaveChanges();
                    db.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, dto);

                }
                else
                {
                    //  uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RecordNotExist, null);
                }


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update(ActivePrintDto dto, long? ImPermission_Number, int OperationCode, List<string> Device_Info)
        {
            PlantQuarantineEntities db = new PlantQuarantineEntities();

            try
            {
                var f = (from ipr in db.Im_PermissionRequest
                         where ipr.ImPermission_Number == ImPermission_Number
                         select new Stopping_PermissionsDTO
                         {
                             Im_PermissionRequest_ID = ipr.ID,
                             Im_CheckRequest_ID = (long)ipr.Im_CheckRequest_ID
                         }).FirstOrDefault();
                if (f != null)
                {
                    f.Im_CheckRequest_ID = 0;
                    db.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, dto);

                }
                else
                {
                    //  uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RecordNotExist, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }


}
