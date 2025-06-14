using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Import.Permissions;
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
using static PlantQuar.DTO.DTO.Im_Permissions.ActivePrintDTO;

namespace PlantQuar.BLL.BLL.Im_Permissions
{
    public class List_ImPermissions_NewBLL
    {
        private UnitOfWork uow;
        //dbPrivilageEntities entities1 = new dbPrivilageEntities();

        public List_ImPermissions_NewBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetImPermissionsList(int radio_ID, long? Company_ID, long? Country_ID, long? ShortName_ID, int? Type_Item, int? Type_Company, string Im_PermissionRequest_Num, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                string _Im_PermissionRequest_Num = Im_PermissionRequest_Num;
                if (Im_PermissionRequest_Num == null)
                    _Im_PermissionRequest_Num = "";
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("lang", SqlDbType.NVarChar);
                paramters_Type.Add("radio_ID", SqlDbType.Int);
                paramters_Type.Add("Company_ID", SqlDbType.BigInt);
                paramters_Type.Add("Country_ID", SqlDbType.BigInt);
                paramters_Type.Add("ShortName_ID", SqlDbType.BigInt);
                paramters_Type.Add("Type_Item", SqlDbType.Int);
                paramters_Type.Add("Type_Company", SqlDbType.Int);
                paramters_Type.Add("Im_PermissionRequest_Num", SqlDbType.NVarChar);



                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("lang", lang.ToString());
                paramters_Data.Add("radio_ID", radio_ID.ToString());
                paramters_Data.Add("Company_ID", Company_ID.ToString());
                paramters_Data.Add("Country_ID", Country_ID.ToString());
                paramters_Data.Add("ShortName_ID", ShortName_ID.ToString());
                paramters_Data.Add("Type_Item", Type_Item.ToString());
                paramters_Data.Add("Type_Company", Type_Company.ToString());
                paramters_Data.Add("Im_PermissionRequest_Num", _Im_PermissionRequest_Num.ToString());







                var request = uow.Repository<ImPermissionsListDTO>().CallStored("Get_List_Im_PermissionRequest", paramters_Type,
                    paramters_Data, Device_Info).ToList();

                if (Country_ID > 0 && Company_ID == null && ShortName_ID == null)
                {
                    request = request.Where(a => a.ExportCountryID == Country_ID).ToList();
                }
                else if (Country_ID == null && Company_ID > 0 && ShortName_ID == null)
                {
                    request = request.Where(a => a.Company_ID == Company_ID).ToList();
                }
                else if (Country_ID == null && Company_ID == null && ShortName_ID > 0)
                {
                    request = request.Where(a => a.short_ID == ShortName_ID).ToList();
                }
                else if (Country_ID > 0 && Company_ID > 0 && ShortName_ID == null)
                {
                    request = request.Where(a => a.ExportCountryID == Country_ID && a.Company_ID == Company_ID).ToList();
                }
                else if (Country_ID > 0 && Company_ID == null && ShortName_ID > 0)
                {
                    request = request.Where(a => a.ExportCountryID == Country_ID && a.short_ID == ShortName_ID).ToList();
                }

                else if (Country_ID > 0 && Company_ID > 0 && ShortName_ID > 0)
                {
                    request = request.Where(a => a.ExportCountryID == Country_ID && a.Company_ID == Company_ID && a.short_ID == ShortName_ID).ToList();
                }

                //request.OrderBy(n => n.IsAcceppted == false).ThenBy(n => n.IsAcceppted).
                //      ThenBy(n => n.IsPaid).ThenBy(n => n.IS_Print_Ar).
                //      ThenBy(n => n.IS_Print_EN)


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);


                //PlantQuarantineEntities entities = new PlantQuarantineEntities();

                //int lang =int.Parse( Device_Info[2].ToString());
                //var permissions = entities.View_List_Im_PermissionRequest.ToList();


                //entities.GetGroupsNameByUserAndApplication(10, 1, 1).ToList();
                // .Get_List_Im_PermissionRequest(1).ToList();
                #region old


                //var permissions = (from cc in entities.Im_PermissionRequest
                //                   join rr in entities.Im_RequestData on cc.ID equals rr.Im_PermissionRequest_ID
                //                  join oo in entities.Im_OpertaionType on rr.Im_OperationType equals oo.ID
                //                   join co in entities.Countries on rr.ExportCountry_Id equals co.ID

                //                   join sc in entities.A_SystemCode on rr.ImporterType_Id equals sc.Id
                //                   join cn in entities.Company_National on rr.Importer_ID equals cn.ID  into cnn from _CompanyNational in cnn.DefaultIfEmpty() where rr.ImporterType_Id == 6

                //                   join po in entities.Public_Organization on rr.Importer_ID equals po.ID   into puo from puor in puo.DefaultIfEmpty() where rr.ImporterType_Id == 7
                //                //   join po in entities.pe on rr.Importer_ID equals po.ID into puo from puor in puo.DefaultIfEmpty() where rr.ImporterType_Id == 7


                //               //    join ipi in  entities.Im_PermissionItems on cc.ID equals ipi.Im_PermissionRequest_ID
                //                  // join prrr in entities.Person on rr.Importer_ID equals po.ID

                //                   //where cc.Im_CheckRequest_ID==null
                //                   //&& cc.User_Deletion_Id  == null
                //                   //&&cc.IS_Print_Ar != true 
                //                   //   && cc.IS_Print_EN != true
                //                   //   &&cc.IsAcceppted !=false
                //                 //  from oo in oos.DefaultIfEmpty()

                //                    select new ImPermissionsListDTO
                //                    {
                //                        Im_PermissionRequest_ID = cc.ID,
                //                        ImPermission_Number = cc.ImPermission_Number.ToString(),
                //                        Arrival_Date = cc.Arrival_Date,
                //                        operationTypeName = oo != null ? oo.Name_Ar : "",
                //                        ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                //                        Importer_ID = rr.Importer_ID,
                //                        ImporterType_Id = rr.ImporterType_Id,
                //                        IsAcceppted = cc.IsAcceppted,
                //                        IsPaid = cc.IsPaid,
                //                        IS_Print_Ar = cc.IS_Print_Ar,
                //                        IS_Print_EN = cc.IS_Print_EN,
                //                        Im_CheckRequest_ID = cc.Im_CheckRequest_ID,
                //                        Start_Date = cc.Start_Date,
                //                        End_Date = cc.End_Date,
                //                        Renewal_Status = cc.Renewal_Status,
                //                        Print_Count = cc.Print_Count,
                //                        ImporterName=  rr.ImporterType_Id == 6 ? (lang == "1" ? _CompanyNational.Name_Ar : _CompanyNational.Name_En) :
                //                                                rr.ImporterType_Id == 7 ? (lang == "1" ? puor.Name_Ar : puor.Name_En):"" ,
                //                                              //  rr.ImporterType_Id == 8 ? (lang == "1" ? _CompanyNational.Name_Ar : _CompanyNational.Name_En) 

                //                    }).ToList();
                ///////////////// get user permssion for print 

                //foreach (var per in permissions)
                //{
                //    per.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == per.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault();

                //    if (per.ImporterType_Id == 6)
                //    {
                //        per.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == per.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                //    }
                //    else if (per.ImporterType_Id == 7)
                //    {
                //        per.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == per.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                //    }
                //    else
                //    {
                //        per.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == per.Importer_ID).Select(s => s.Name).FirstOrDefault();

                //    }

                //    var initiator = uow.Repository<Im_PermissionItems>().GetData().Where(i => i.Im_PermissionRequest_ID == per.Im_PermissionRequest_ID).FirstOrDefault();
                //    if (initiator != null)
                //    {
                //        var initiatorId = initiator.Im_Initiator_ID;
                //        var initiatir = uow.Repository<Im_Initiator>().GetData().Where(u => u.ID == initiatorId).FirstOrDefault();

                //        var qualId = initiatir.QualitativeGroup_Id;
                //        if (qualId != null)
                //        {
                //            per.shortName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault().Name_Ar;
                //        }

                //        var itemShortNameId = initiatir.Item_ShortName_ID;
                //        if (itemShortNameId != null)
                //        {
                //            var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId).FirstOrDefault();
                //            per.shortName = lang == "1" ? itemShortName.ShortName_Ar : itemShortName.ShortName_En;
                //        }
                //    } 
                //}

                #endregion
                //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData    
                //    ,permissions.OrderBy
                //    (n=>n.IsAcceppted==false).ThenBy(n=>n.IsAcceppted).
                //    ThenBy(n => n.IsPaid).ThenBy(n => n.IS_Print_Ar).
                //    ThenBy(n => n.IS_Print_EN));
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> GetImPermissionsList_filter
           (int Isacceppted, decimal? ImPermission_Number, List<string> Device_Info)
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
                                   where cc.ImPermission_Number == ImPermission_Number
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
                                       ,
                                       Print_Count = cc.Print_Count,
                                       Renewal_Status = cc.Renewal_Status,
                                       Start_Date = cc.Start_Date,
                                       End_Date = cc.End_Date
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

                //if (ImPermission_Number != null)
                //{
                //    permissions = permissions.Where(p => p.ImPermission_Number == ImPermission_Number).ToList();

                //}






                //isacceppted = 1 null, =2 false ,=3 true
                if (Isacceppted == 1)
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
                                   where cc.ImPermission_Number == ImPermission_Number
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
                var f = db.Im_PermissionRequest.FirstOrDefault(x => x.ImPermission_Number ==
              dto.ImPermission_Number);
                if (f != null)
                {
                    f.IS_Print_Ar = dto.IS_Print_Ar;
                    f.IS_Print_EN = dto.IS_Print_EN;
                    db.SaveChanges();
                    //Table_Action_Log newLog = new Table_Action_Log();
                    //var idd =uow.Repository<Object>().GetNextSequenceValue_Long("Table_Action_Log_seq");
                    //newLog.ID = idd;
                    //newLog.ID_Table_Action = 1;
                    //newLog.NOTS = "Active Im_PermissionRequest Print  ";
                    //newLog.ID_TableActionValue = f.ID;
                    //newLog.User_Creation_Id = dto.User_Creation_Id;
                    //newLog.User_Creation_Date = DateTime.Now;
                    //uow.Repository<Table_Action_Log>().InsertRecord(newLog);

                    //uow.SaveChanges();

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



        //eslam With Paginaginion
        public Dictionary<string, object> GetImPermissionsList2(string SearchALL, int radio_ID, long? Company_ID, long? Country_ID, long? ShortName_ID, int? Type_Item, int? Type_Company, string Im_PermissionRequest_Num, int CurrentPage, int pageSize, List<string> Device_Info)
        {
            if (SearchALL == null)
            {
                try
                {
                    radio_ID = 5;
                    string lang = Device_Info[2];
                    string _Im_PermissionRequest_Num = Im_PermissionRequest_Num;
                    if (Im_PermissionRequest_Num == null)
                        _Im_PermissionRequest_Num = "";
                    Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                    paramters_Type.Add("lang", SqlDbType.NVarChar);
                    paramters_Type.Add("radio_ID", SqlDbType.Int);
                    paramters_Type.Add("Company_ID", SqlDbType.BigInt);
                    paramters_Type.Add("Country_ID", SqlDbType.BigInt);
                    paramters_Type.Add("ShortName_ID", SqlDbType.BigInt);
                    paramters_Type.Add("Type_Item", SqlDbType.Int);
                    paramters_Type.Add("Type_Company", SqlDbType.Int);
                    paramters_Type.Add("Im_PermissionRequest_Num", SqlDbType.NVarChar);



                    Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                    paramters_Data.Add("lang", lang.ToString());
                    paramters_Data.Add("radio_ID", radio_ID.ToString());
                    paramters_Data.Add("Company_ID", Company_ID.ToString());
                    paramters_Data.Add("Country_ID", Country_ID.ToString());
                    paramters_Data.Add("ShortName_ID", ShortName_ID.ToString());
                    paramters_Data.Add("Type_Item", Type_Item.ToString());
                    paramters_Data.Add("Type_Company", Type_Company.ToString());
                    paramters_Data.Add("Im_PermissionRequest_Num", _Im_PermissionRequest_Num.ToString());





                    var allrequest = uow.Repository<ImPermissionsListDTO>().CallStored("Get_List_Im_PermissionRequest", paramters_Type,
                      paramters_Data, Device_Info).Count();

                    var request = uow.Repository<ImPermissionsListDTO>().CallStored("Get_List_Im_PermissionRequest", paramters_Type,
                        paramters_Data, Device_Info).OrderByDescending(b => b.Im_PermissionRequest_ID)
                   .Skip((CurrentPage - 1) * pageSize)
                   .Take(pageSize).ToList();

                    if (Country_ID > 0 && Company_ID == null && ShortName_ID == null)
                    {
                        request = request.Where(a => a.ExportCountryID == Country_ID).ToList();
                    }
                    else if (Country_ID == null && Company_ID > 0 && ShortName_ID == null)
                    {
                        request = request.Where(a => a.Company_ID == Company_ID).ToList();
                    }
                    else if (Country_ID == null && Company_ID == null && ShortName_ID > 0)
                    {
                        request = request.Where(a => a.short_ID == ShortName_ID).ToList();
                    }
                    else if (Country_ID > 0 && Company_ID > 0 && ShortName_ID == null)
                    {
                        request = request.Where(a => a.ExportCountryID == Country_ID && a.Company_ID == Company_ID).ToList();
                    }
                    else if (Country_ID > 0 && Company_ID == null && ShortName_ID > 0)
                    {
                        request = request.Where(a => a.ExportCountryID == Country_ID && a.short_ID == ShortName_ID).ToList();
                    }

                    else if (Country_ID > 0 && Company_ID > 0 && ShortName_ID > 0)
                    {
                        request = request.Where(a => a.ExportCountryID == Country_ID && a.Company_ID == Company_ID && a.short_ID == ShortName_ID).ToList();
                    }



                    var TotalResults = allrequest;
                    if (TotalResults > 0)
                    {
                        request.FirstOrDefault().TotalResults = TotalResults;

                        request.FirstOrDefault().TotalPages = (int)Math.Ceiling(TotalResults / (double)pageSize);
                    }



                    return uow.Repository<Object>().DataReturn((int)TotalResults, request);



                }
                catch (Exception ex)
                {
                    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
                }
            }
            else
            {
                try
                {
                    radio_ID = 5;
                    string lang = Device_Info[2];
                    string _Im_PermissionRequest_Num = Im_PermissionRequest_Num;
                    if (Im_PermissionRequest_Num == null)
                        _Im_PermissionRequest_Num = "";
                    Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                    paramters_Type.Add("lang", SqlDbType.NVarChar);
                    paramters_Type.Add("radio_ID", SqlDbType.Int);
                    paramters_Type.Add("Company_ID", SqlDbType.BigInt);
                    paramters_Type.Add("Country_ID", SqlDbType.BigInt);
                    paramters_Type.Add("ShortName_ID", SqlDbType.BigInt);
                    paramters_Type.Add("Type_Item", SqlDbType.Int);
                    paramters_Type.Add("Type_Company", SqlDbType.Int);
                    paramters_Type.Add("Im_PermissionRequest_Num", SqlDbType.NVarChar);



                    Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                    paramters_Data.Add("lang", lang.ToString());
                    paramters_Data.Add("radio_ID", radio_ID.ToString());
                    paramters_Data.Add("Company_ID", Company_ID.ToString());
                    paramters_Data.Add("Country_ID", Country_ID.ToString());
                    paramters_Data.Add("ShortName_ID", ShortName_ID.ToString());
                    paramters_Data.Add("Type_Item", Type_Item.ToString());
                    paramters_Data.Add("Type_Company", Type_Company.ToString());
                    paramters_Data.Add("Im_PermissionRequest_Num", _Im_PermissionRequest_Num.ToString());





                    var allrequest = uow.Repository<ImPermissionsListDTO>().CallStored("Get_List_Im_PermissionRequest", paramters_Type,
                      paramters_Data, Device_Info).ToList();

                    var request = uow.Repository<ImPermissionsListDTO>().CallStored("Get_List_Im_PermissionRequest", paramters_Type,
                        paramters_Data, Device_Info).Where(b => b.ImPermission_Number.ToString().Contains(SearchALL))
                   .ToList();

            

                   var TotalResults = allrequest.Count();
                    if (TotalResults > 0)
                    {
                        request.FirstOrDefault().TotalResults = TotalResults;

                       request.FirstOrDefault().TotalPages = (int)Math.Ceiling(TotalResults / (double)pageSize);
                    }



                    return uow.Repository<Object>().DataReturn(0, request);



                }
                catch (Exception ex)
                {
                    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
                }

            }
        }






    }
}
