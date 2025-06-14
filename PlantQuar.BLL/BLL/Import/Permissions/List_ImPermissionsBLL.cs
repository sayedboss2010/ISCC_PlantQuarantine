using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Import.Permissions;
using PlantQuar.UOW.UnitOfWork;
using PlantQuar.WEB.App_Start;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static PlantQuar.DTO.DTO.Im_Permissions.ActivePrintDTO;

namespace PlantQuar.BLL.BLL.Im_Permissions
{
    public class List_ImPermissionsBLL
    {
        private UnitOfWork uow;
        //dbPrivilageEntities entities1 = new dbPrivilageEntities();

        public List_ImPermissionsBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetImPermissionsList
           (List<string> Device_Info)
        {
            try
            {
               
                PlantQuarantineEntities entities = new PlantQuarantineEntities();

                string lang = Device_Info[2];
                var permissions = (from cc in entities.Im_PermissionRequest
                                   join rr in entities.Im_RequestData on cc.ID equals rr.Im_PermissionRequest_ID
                                   join oo in entities.Im_OpertaionType on rr.Im_OperationType equals oo.ID into oos
                                   join co in entities.Countries on rr.ExportCountry_Id equals co.ID

                                  //where cc.Im_CheckRequest_ID==null
                                  //&& cc.User_Deletion_Id  == null
                                  //&&cc.IS_Print_Ar != true 
                                  //   && cc.IS_Print_EN != true
                                  //   &&cc.IsAcceppted !=false
                                   from oo in oos.DefaultIfEmpty()

                                    select new ImPermissionsListDTO
                                    {
                                        Im_PermissionRequest_ID = cc.ID,
                                        ImPermission_Number = cc.ImPermission_Number,
                                        Arrival_Date = cc.Arrival_Date,
                                        operationTypeName = oo != null ? oo.Name_Ar : "",
                                        ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                                        Importer_ID = rr.Importer_ID,
                                        ImporterType_Id = rr.ImporterType_Id,
                                        IsAcceppted = cc.IsAcceppted,
                                        IsPaid = cc.IsPaid,
                                        IS_Print_Ar = cc.IS_Print_Ar,
                                        IS_Print_EN = cc.IS_Print_EN,
                                        Im_CheckRequest_ID = cc.Im_CheckRequest_ID,
                                        Start_Date = cc.Start_Date,
                                        End_Date = cc.End_Date,
                                        Renewal_Status = cc.Renewal_Status,
                                        Print_Count = cc.Print_Count,


                                    }).ToList();
                /////////////// get user permssion for print 
                
                foreach (var per in permissions)
                {
                    per.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == per.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault();

                    if (per.ImporterType_Id == 6)
                    {
                        per.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == per.Importer_ID).Select(s => lang=="1"?s.Name_Ar:s.Name_En).FirstOrDefault();

                    }
                    else if (per.ImporterType_Id == 7)
                    {
                        per.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == per.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                    }
                    else
                    {
                        per.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == per.Importer_ID).Select(s =>  s.Name).FirstOrDefault();

                    }

                    var initiator = uow.Repository<Im_PermissionItems>().GetData().Where(i => i.Im_PermissionRequest_ID == per.Im_PermissionRequest_ID).FirstOrDefault();
                    if(initiator != null)
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







              //  permissions = (List<ImPermissionsListDTO>)permissions.OrderBy(per => per.IsAcceppted == null);

              

                //|| n.IsAcceppted == true)

                //.ThenBy(n => n.IsPaid).ThenBy(n => n.IS_Print_Ar).ThenBy(n => n.IS_Print_EN).ThenBy(n => n.IsAcceppted == false).ToList()

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData    
                    ,permissions.OrderBy
                    (n=>n.IsAcceppted==false).ThenBy(n=>n.IsAcceppted).
                    ThenBy(n => n.IsPaid).ThenBy(n => n.IS_Print_Ar).
                    ThenBy(n => n.IS_Print_EN));
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        
        public Dictionary<string, object> GetImPermissionsList_filter
           (int Isacceppted,decimal? ImPermission_Number, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var permissions = (from cc in entities.Im_PermissionRequest
                                   join rr in entities.Im_RequestData on cc.ID equals rr.Im_PermissionRequest_ID
                                   join oo in entities.Im_OpertaionType on rr.Im_OperationType equals oo.ID into oos
                                   join co in entities.Countries on rr.ExportCountry_Id equals co.ID
                                   from oo in oos.DefaultIfEmpty()

                                   select new ImPermissionsListDTO
                                   {
                                       Im_PermissionRequest_ID = cc.ID,
                                       ImPermission_Number = cc.ImPermission_Number,
                                       Arrival_Date = cc.Arrival_Date,
                                       operationTypeName = oo != null ? oo.Name_Ar : "",
                                       ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                                       Importer_ID = rr.Importer_ID,
                                       ImporterType_Id = rr.ImporterType_Id,
                                       IsAcceppted = cc.IsAcceppted,
                                       IsPaid = cc.IsPaid,
                                       IS_Print_Ar = cc.IS_Print_Ar,
                                       IS_Print_EN = cc.IS_Print_EN
                                       ,Print_Count=cc.Print_Count,
                                       Renewal_Status=cc.Renewal_Status,
                                       Start_Date=cc.Start_Date,
                                       End_Date=cc.End_Date
                                   }).ToList();



               // var permissions1 = (
               //from cc in entities1.PR_GroupModuleMenuPrivilage
               //join rr in entities1.PR_UserGroup
               //on cc.PR_GroupId equals rr.PR_GroupId

               //where cc.PR_MenuId ==  && cc.PR_ModuleId == modelID
               //&& cc.PR_GroupId == GroupId && rr.PR_UserId == userID
               //select new ImPermissionsListDTO
               //{

               //    CanPrint = cc.CanPrint

               //}).FirstOrDefault();

                if (ImPermission_Number != null)
                {
                    permissions = permissions.Where(p => p.ImPermission_Number == ImPermission_Number).ToList();

                }






                //isacceppted = 1 null, =2 false ,=3 true
                if(Isacceppted == 1)
                {
                    permissions = permissions.Where(p => p.IsAcceppted == null).ToList();

                }
                else if (Isacceppted == 2)
                {
                    permissions = permissions.Where(p => p.IsAcceppted == false).ToList();

                }
                else if (Isacceppted == 3)
                {
                    permissions = permissions.Where(p => p.IsAcceppted == true).ToList();

                }
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
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
         public Dictionary<string, object> GetImPermissionsList_filter_ActivePrint
           (decimal? ImPermission_Number, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var permissions = (from cc in entities.Im_PermissionRequest
                                   join rr in entities.Im_RequestData on cc.ID equals rr.Im_PermissionRequest_ID
                                   join oo in entities.Im_OpertaionType on rr.Im_OperationType equals oo.ID into oos
                                   join co in entities.Countries on rr.ExportCountry_Id equals co.ID
                                   from oo in oos.DefaultIfEmpty()

                                   select new ImPermissionsListDTO
                                   {
                                       Im_PermissionRequest_ID = cc.ID,
                                       ImPermission_Number = cc.ImPermission_Number,
                                       Arrival_Date = cc.Arrival_Date,
                                       operationTypeName = oo != null ? oo.Name_Ar : "",
                                       ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                                       Importer_ID = rr.Importer_ID,
                                       ImporterType_Id = rr.ImporterType_Id,
                                       
                                       IsPaid = cc.IsPaid,
                                       IS_Print_Ar = cc.IS_Print_Ar,
                                       IS_Print_EN = cc.IS_Print_EN
                                   }).ToList();



               // var permissions1 = (
               //from cc in entities1.PR_GroupModuleMenuPrivilage
               //join rr in entities1.PR_UserGroup
               //on cc.PR_GroupId equals rr.PR_GroupId

               //where cc.PR_MenuId ==  && cc.PR_ModuleId == modelID
               //&& cc.PR_GroupId == GroupId && rr.PR_UserId == userID
               //select new ImPermissionsListDTO
               //{

               //    CanPrint = cc.CanPrint

               //}).FirstOrDefault();

                if (ImPermission_Number != null)
                {
                    permissions = permissions.Where(p => p.ImPermission_Number == ImPermission_Number).ToList();

                }






        
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
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }












        }
        public Dictionary<string, object> Update(ActivePrintDto dto, List<string> Device_Info)
        {
           PlantQuarantineEntities db = new PlantQuarantineEntities();
            
            try
            {
                 var f = db.Im_PermissionRequest.FirstOrDefault(x => x.ImPermission_Number ==dto.ImPermission_Number_ID);
                if (f != null)
                {
                    f.IS_Print_Ar = dto.IS_Print_Ar;
                    f.IS_Print_EN = dto.IS_Print_EN;



                    //Table_Action_Log newLog = new Table_Action_Log();
                    //var idd =                        uow.Repository<Object>().GetNextSequenceValue_Long("Table_Action_Log_seq");
                    //newLog.ID = idd;
                    //newLog.ID_Table_Action = 1;
                    //newLog.NOTS = "Active Im_PermissionRequest Print  ";
                    //newLog.ID_TableActionValue = f.ID;
                    //newLog.User_Creation_Id = dto.User_Creation_Id;
                    //newLog.User_Creation_Date = DateTime.Now;
                    //uow.Repository<Table_Action_Log>().InsertRecord(newLog);

                    //uow.SaveChanges();
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
