using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Im_CheckRequest_General;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Im_CheckRequest_General
{
    public class Genaral_List_Im_checkRequestsBLL
    {
        private UnitOfWork uow;

        public Genaral_List_Im_checkRequestsBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetImCheckRequestList_filter
          (short IsApproved, string requestnumber, short userId, List<string> Device_Info)
        {
            try
            {
                dbPrivilageEntities priv = new dbPrivilageEntities();

                var user = priv.PR_User.Where(p => p.Id == userId).FirstOrDefault();
                var outlet_Data = uow.Repository<Outlet>().GetData().FirstOrDefault(o => o.ID_HR == user.Outlet_ID);

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                List<General_ImCheckRequestListDTO> requests = (from cc in entities.Im_CheckRequest
                                                        join rr in entities.Im_CheckRequest_Data on cc.ID equals rr.Im_CheckRequest_ID
                                                        join co in entities.Countries on rr.ExportCountry_Id equals co.ID
                                                        join chfr in entities.Im_CheckRequest_Final_Result on cc.ID equals chfr.Im_CheckRequest_ID into ims
                                                        // join frr in entities.Im_Final_Result on ims.ID equals frr.Im_CheckRequest_Final_Result

                                                        // join im in entities.Im_RequestCommittee on cc.ID equals im.ImCheckRequest_ID into ims
                                                        from chfr in ims.DefaultIfEmpty()

                                                        where/* cc.IsPaid == true &&*/ cc.Outlet_ID == outlet_Data.ID

                                                        select new General_ImCheckRequestListDTO
                                                        {
                                                            Im_CheckRequest_ID = cc.ID,
                                                            ImCheckRequest_Number = cc.CheckRequest_Number,
                                                            Creation_Date = (DateTime)cc.User_Creation_Date,
                                                            IsAccepted = cc.IsAccepted,
                                                            ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                                                            Importer_ID = rr.Importer_ID,
                                                            ImporterType_Id = rr.ImporterType_Id,

                                                            Outlet_ID = cc.Outlet_ID,

                                                            Closed_Request = chfr.Im_Final_Result.Status
                                                            //CommitteeID = im == null? 0:im.ID,
                                                            //CommitteeType_ID= im == null ?6 : im.CommitteeType_ID
                                                        }).ToList();//.Skip(20).Take(20).OrderBy(a=>a.Im_CheckRequest_ID).
                if (!string.IsNullOrEmpty(requestnumber))
                {
                    requests = requests.Where(c => c.ImCheckRequest_Number == requestnumber).ToList();
                }
                List<General_ImCheckRequestListDTO> newRequests = new List<General_ImCheckRequestListDTO>();
                foreach (var re in requests)
                {
                    //if(re.CommitteeType_ID == 0 || re.CommitteeType_ID == 11)
                    {
                        newRequests.Add(re);
                    }
                }
                foreach (var req in newRequests)
                {
                    //req.PageSize = 100;

                    req.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == req.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault();
                    //req.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == rr.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault(),
                    //req.ImporterName = req.ImporterType_Id == 6 ? uow.Repository<Company_National>().GetData().Where(a => a.ID == req.ImporterType_Id).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault()
                    //: req.ImporterType_Id == 7 ? uow.Repository<Public_Organization>().GetData().Where(a => a.ID == req.ImporterType_Id).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault()
                    //: uow.Repository<Person>().GetData().Where(a => a.ID == req.ImporterType_Id).Select(s => s.Name).FirstOrDefault();

                    //req.Closed_Request = uow.Repository<Im_CheckRequest_Final_Result>().GetData().Where(a => a.Im_CheckRequest_ID == req.Im_CheckRequest_ID)
                    //                .Select(s => s.Im_Final_Result.Status).FirstOrDefault();
                    //    req.Closed_Request = uow.Repository<Im_CheckRequest_Final_Result>().GetData().Where(a => a.Im_CheckRequest_ID == req.Im_CheckRequest_ID)
                    //        .Select(s =>s.Im_Final_Result.Status).Last();
                    //    //req.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == req.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault();

                    if (req.ImporterType_Id == 6)
                    {
                        req.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                    }
                    else if (req.ImporterType_Id == 7)
                    {
                        req.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                    }
                    else
                    {
                        req.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => s.Name).FirstOrDefault();
                    }
                }

                //الطلب انتهى

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, newRequests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //public Dictionary<string, object> GetImCheckRequestList_filter2
        //  (short userId, long? outlet, string DateFrom, string DateEnd, int selectApproveId, int FinalResultListId, List<string> Device_Info)
        //{
        //    try
        //    {
        //        dbPrivilageEntities priv = new dbPrivilageEntities();

        //            var user = priv.PR_User.Where(p => p.Id == userId).FirstOrDefault();
        //            var outlet_Data = uow.Repository<Outlet>().GetData().FirstOrDefault(o => o.ID_HR == user.Outlet_ID);

        //        DateTime _dateFrom = DateTime.Parse(DateFrom);
        //        DateTime _dateEnd = DateTime.Parse(DateEnd) ;
        //        _dateEnd = _dateEnd.AddDays(1);

        //        PlantQuarantineEntities entities = new PlantQuarantineEntities();
        //        string lang = Device_Info[2];
        //        //List<ImCheckRequestListDTO> requests = (from cc in entities.Im_CheckRequest
        //        //                                        join rr in entities.Im_CheckRequest_Data on cc.ID equals rr.Im_CheckRequest_ID
        //        //                                        join co in entities.Countries on rr.ExportCountry_Id equals co.ID
        //        //                                        join chfr in entities.Im_CheckRequest_Final_Result on cc.ID equals chfr.Im_CheckRequest_ID into ims
        //        //                                        // join frr in entities.Im_Final_Result on ims.ID equals frr.Im_CheckRequest_Final_Result

        //        //                                        // join im in entities.Im_RequestCommittee on cc.ID equals im.ImCheckRequest_ID into ims
        //        //                                        from chfr in ims.DefaultIfEmpty()

        //        //                                        where/* cc.IsPaid == true &&*/ cc.Outlet_ID == outlet_Data.ID 
        //        //                                        &&(cc.User_Creation_Date>= _dateFrom && cc.User_Creation_Date<= _dateEnd)
        //        //                                        //&&cc.IsAccepted== selectApproveId
        //        //                                        //&& cc.CheckRequest_Number == "362022062912560086"
        //        //                                        select new ImCheckRequestListDTO
        //        //                                        {
        //        //                                            Im_CheckRequest_ID = cc.ID,
        //        //                                            ImCheckRequest_Number = cc.CheckRequest_Number,
        //        //                                            Creation_Date = (DateTime)cc.User_Creation_Date,
        //        //                                            IsAccepted = cc.IsAccepted,
        //        //                                            ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
        //        //                                            Importer_ID = rr.Importer_ID,
        //        //                                            ImporterType_Id = rr.ImporterType_Id,

        //        //                                            Outlet_ID = cc.Outlet_ID,

        //        //                                            Closed_Request = chfr.Im_Final_Result.Status,
        //        //                                            //CommitteeID = im == null? 0:im.ID,
        //        //                                            //CommitteeType_ID= im == null ?6 : im.CommitteeType_ID


        //        //                                            Im_Final_Result_ID = chfr.Im_Final_Result_ID,
        //        //                                            Name_Final_Result = lang == "1" ? chfr.Im_Final_Result.Ar_Name : chfr.Im_Final_Result.En_Name,


        //        //                                        }).ToList();//.Skip(20).Take(20).OrderBy(a=>a.Im_CheckRequest_ID).

        //        Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
        //        paramters_Type.Add("Outlet_ID", SqlDbType.BigInt);      

        //        Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
        //        paramters_Data.Add("Outlet_ID", outlet_Data.ID.ToString());


        //        List<ImCheckRequestListDTO> requests = uow.Repository<ImCheckRequestListDTO>().CallStored("List_ImCheckRequest_Procedure_Name", paramters_Type,
        //        paramters_Data, Device_Info).ToList();


        //        requests = requests.Where(a=>a.User_Creation_Date >= _dateFrom && a.User_Creation_Date <= _dateEnd).ToList();
        //        //List<ImCheckRequestListDTO> requests = (from cc in entities.List_ImCheckRequest_Procedure_Name(outlet_Data.ID)
        //        //                                        where (cc.User_Creation_Date >= _dateFrom && cc.User_Creation_Date <= _dateEnd)
        //        //                                        select new ImCheckRequestListDTO
        //        //                                        {
        //        //                                            Im_CheckRequest_ID = cc.Im_CheckRequest_ID,
        //        //                                            ImCheckRequest_Number = cc.ImCheckRequest_Number,
        //        //                                            Creation_Date = (DateTime)cc.User_Creation_Date,
        //        //                                            IsAccepted = cc.IsAccepted,
        //        //                                            ExportCountryName = cc.ExportCountryName,
        //        //                                            Importer_ID = cc.Importer_ID,
        //        //                                            ImporterType_Id = cc.ImporterType_Id,
        //        //                                            Outlet_ID = cc.Outlet_ID,
        //        //                                            Closed_Request = cc.Closed_Request,
        //        //                                            Im_Final_Result_ID = cc.Im_Final_Result_ID,
        //        //                                            Name_Final_Result = cc.Name_Final_Result,
        //        //                                        }).ToList();

        //        //bool CheckRequestStatus;
        //        switch (selectApproveId)
        //        {

        //            case 2:
        //                requests = requests.Where(a => a.IsAccepted == false).ToList();
        //                break;
        //            case 3:
        //                requests = requests.Where(a => a.IsAccepted == null).ToList();
        //                break;
        //            case 4:
        //                requests = requests.Where(a => a.Closed_Request == false).ToList();
        //                break;
        //            case 5:
        //                requests = requests.Where(a => a.IsAccepted == true && a.Closed_Request == null).ToList();
        //                break;
        //            case 6:
        //            case 7:
        //                if (selectApproveId != null && FinalResultListId > 0)
        //                {
        //                    requests = requests.Where(a => a.Im_Final_Result_ID == FinalResultListId).ToList();
        //                }
        //                else if (selectApproveId == 6 && FinalResultListId == 0)
        //                {
        //                    requests = requests.Where(a => a.Closed_Request == true).ToList();
        //                }
        //                else if (selectApproveId == 7 && FinalResultListId == 0)
        //                {
        //                    requests = requests.Where(a => a.Closed_Request == false).ToList();
        //                }
        //                break;

        //            default:
        //                break;
        //        }



        //        //List<ImCheckRequestListDTO> newRequests = new List<ImCheckRequestListDTO>();
        //        //foreach (var re in requests)
        //        //{
        //        //    //if(re.CommitteeType_ID == 0 || re.CommitteeType_ID == 11)
        //        //    {
        //        //        newRequests.Add(re);
        //        //    }
        //        //}
        //        foreach (var req in requests)
        //        {
        //            //req.PageSize = 100;

        //            req.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == req.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault();

        //            if (req.ImporterType_Id == 6)
        //            {
        //                req.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

        //            }
        //            else if (req.ImporterType_Id == 7)
        //            {
        //                req.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

        //            }
        //            else
        //            {
        //                req.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => s.Name).FirstOrDefault();
        //            }
        //        }

        //        //الطلب انتهى

        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message + "+++"+ ex.InnerException.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}
        public Dictionary<string, object> GetImCheckRequestList_filter2
       (short userId, long? outlet, string DateFrom, string DateEnd, int selectApproveId, int FinalResultListId
            , string CheckRequest_Number, long Company_ID, short operation_type, List<string> Device_Info)
        {
            try
            {
                DateTime _startDate = DateTime.Parse(DateFrom); //DateTime.ParseExact(DateFrom, "dd/MM/yyyy", null);
                DateTime _endDate = DateTime.Parse(DateEnd); //DateTime.ParseExact(DateEnd, "dd/MM/yyyy", null);
                dbPrivilageEntities priv = new dbPrivilageEntities();
                if (CheckRequest_Number == null)
                {
                    CheckRequest_Number = "";
                }
                //var user = priv.PR_User.Where(p => p.Id == userId).FirstOrDefault();
                //var outlet_Data = uow.Repository<Outlet>().GetData().FirstOrDefault(o => o.ID_HR == user.Outlet_ID);

                DateTime _dateFrom = DateTime.Parse(DateFrom);
                DateTime _dateEnd = DateTime.Parse(DateEnd);
                _dateEnd = _dateEnd.AddDays(1);
                
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("long", SqlDbType.BigInt);
                paramters_Type.Add("outlet_User", SqlDbType.BigInt);
                paramters_Type.Add("operation_type", SqlDbType.TinyInt);
                paramters_Type.Add("selectApproveId", SqlDbType.Int);
                paramters_Type.Add("FinalResultListId", SqlDbType.Int);
                paramters_Type.Add("CheckRequest_Number", SqlDbType.NVarChar);
                paramters_Type.Add("Company_ID", SqlDbType.BigInt);

                //Add date Eslam
                paramters_Type.Add("dayStart", SqlDbType.Int);
                paramters_Type.Add("monthStart", SqlDbType.Int);
                paramters_Type.Add("yearStart", SqlDbType.Int);
                paramters_Type.Add("dayEnd", SqlDbType.Int);
                paramters_Type.Add("monthEnd", SqlDbType.Int);
                paramters_Type.Add("yearEnd", SqlDbType.Int);
                //End
                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("long", Device_Info[2]);
                paramters_Data.Add("outlet_User", outlet.ToString());
                paramters_Data.Add("operation_type", operation_type.ToString());
                paramters_Data.Add("selectApproveId", selectApproveId.ToString());
                paramters_Data.Add("FinalResultListId", FinalResultListId.ToString());
                paramters_Data.Add("CheckRequest_Number", CheckRequest_Number.ToString());
                paramters_Data.Add("Company_ID", Company_ID.ToString());
                //Add date data Eslam
                paramters_Data.Add("dayStart", _startDate.Day.ToString());
                paramters_Data.Add("monthStart", _startDate.Month.ToString());
                paramters_Data.Add("yearStart", _startDate.Year.ToString());
                paramters_Data.Add("dayEnd", _endDate.Day.ToString());
                paramters_Data.Add("monthEnd", _endDate.Month.ToString());
                paramters_Data.Add("yearEnd", _endDate.Year.ToString());
                //End
                var requests = uow.Repository<General_ImCheckRequestListDTO>().CallStored("General_List_ImCheckRequest_Data", paramters_Type,
                paramters_Data, Device_Info).ToList();

                #region MyRegion


                //List<ImCheckRequestListDTO> requests = (from cc in entities.Im_CheckRequest
                //                                        join rr in entities.Im_CheckRequest_Data on cc.ID equals rr.Im_CheckRequest_ID
                //                                        join co in entities.Countries on rr.ExportCountry_Id equals co.ID
                //                                        join chfr in entities.Im_CheckRequest_Final_Result on cc.ID equals chfr.Im_CheckRequest_ID into ims
                //                                        // join frr in entities.Im_Final_Result on ims.ID equals frr.Im_CheckRequest_Final_Result

                //                                        // join im in entities.Im_RequestCommittee on cc.ID equals im.ImCheckRequest_ID into ims
                //                                        from chfr in ims.DefaultIfEmpty()

                //                                        join a_sc in entities.A_SystemCode on rr.ImporterType_Id equals a_sc.Id

                //                                        join cn in entities.Company_National  on new {a= (long?)rr.ImporterType_Id, b= (long?)rr.Importer_ID }  equals new { a = (long?)6, b = (long?)cn.ID } into cn1
                //                                        from cn in cn1.DefaultIfEmpty()
                //                                        join po in entities.Public_Organization on new { a = (long?)rr.ImporterType_Id, b = (long?)rr.Importer_ID } equals new { a = (long?)7, b = (long?)po.ID } into po1
                //                                        from po in po1.DefaultIfEmpty()
                //                                        join pr in entities.People on new { a = (long?)rr.ImporterType_Id, b = (long?)rr.Importer_ID } equals new { a = (long?)8, b = (long?)pr.ID } into pr1
                //                                        from pr in pr1.DefaultIfEmpty()                                                           

                //                                        where cc.Outlet_ID == outlet_Data.ID


                //                                        select new ImCheckRequestListDTO
                //                                        {
                //                                            Im_CheckRequest_ID = cc.ID,
                //                                            ImCheckRequest_Number = cc.CheckRequest_Number,
                //                                            Creation_Date = (DateTime)cc.User_Creation_Date,
                //                                            IsAccepted = cc.IsAccepted,
                //                                            ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                //                                            Importer_ID = rr.Importer_ID,
                //                                            ImporterType_Id = rr.ImporterType_Id,
                //                                            ImporterTypeName=lang == "1" ? a_sc.ValueName : a_sc.ValueName,
                //                                            Outlet_ID = cc.Outlet_ID,

                //                                            Closed_Request = chfr.Im_Final_Result.Status,
                //                                            //CommitteeID = im == null? 0:im.ID,
                //                                            //CommitteeType_ID= im == null ?6 : im.CommitteeType_ID

                //                                            Im_CheckRequest_Final_Result_ID= chfr.ID,
                //                                            Im_Final_Result_ID = chfr.Im_Final_Result_ID,
                //                                            Name_Final_Result = lang == "1" ? chfr.Im_Final_Result.Ar_Name : chfr.Im_Final_Result.En_Name,
                //                                            ImporterName= rr.ImporterType_Id == 6 ?(lang == "1" ? cn.Name_Ar : cn.Name_En)
                //                                                                    :rr.ImporterType_Id == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En) 
                //                                                                    : rr.ImporterType_Id == 8 ? (lang == "1" ? pr.Name : cn.Name_En) 
                //                                                                    :""

                //                                        }).ToList();//.Skip(20).Take(20).OrderBy(a=>a.Im_CheckRequest_ID).
                //var Check_Final_Result_all = requests.Where(a => a.Closed_Request == true).Select(s => s.Im_CheckRequest_Final_Result_ID).ToList();//استثناء اللى واخد موقف نهائي

                //requests = requests.Where(a => !Check_Final_Result_all.Contains(a.Im_CheckRequest_Final_Result_ID)).ToList();
                //if (CheckRequest_Number == null)
                //{
                //    if (Company_ID == 0)
                //    {
                //        requests = requests.Where(a => a.Creation_Date >= _dateFrom && a.Creation_Date <= _dateEnd).ToList();
                //        //bool CheckRequestStatus;
                //        switch (selectApproveId)
                //        {
                //            //case 1:
                //            //    var Check_Final_Result_all = requests.Where(a => a.Closed_Request == true).Select(s => s.Im_CheckRequest_Final_Result_ID).ToList();//استثناء اللى واخد موقف نهائي

                //            //    requests = requests.Where(a => !Check_Final_Result_all.Contains(a.Im_CheckRequest_Final_Result_ID)).ToList();
                //            //    break;
                //            case 2:
                //                requests = requests.Where(a => a.IsAccepted == false).ToList();
                //                break;
                //            case 3:
                //                requests = requests.Where(a => a.IsAccepted == null).ToList();
                //                break;
                //            case 4:
                //                requests = requests.Where(a => a.Closed_Request == false).ToList();
                //                break;
                //            case 5:
                //                requests = requests.Where(a => a.IsAccepted == true && a.Closed_Request == null).ToList();
                //                break;
                //            case 6:
                //            case 7:
                //                if (selectApproveId != null && FinalResultListId > 0)
                //                {
                //                    requests = requests.Where(a => a.Im_Final_Result_ID == FinalResultListId).ToList();
                //                }
                //                else if (selectApproveId == 6 && FinalResultListId == 0)
                //                {
                //                    var Check_Final_Result = requests.Where(a => a.Closed_Request == false).Select(s => s.Im_CheckRequest_ID).ToList();//استثناء اللى واخد موقف نهائي

                //                    requests = requests.Where(a => a.Closed_Request == true
                //                   && !Check_Final_Result.Contains(a.Im_CheckRequest_ID)
                //                    ).ToList();
                //                }
                //                else if (selectApproveId == 7 && FinalResultListId == 0)
                //                {
                //                    requests = requests.Where(a => a.Closed_Request == false).ToList();
                //                }
                //                break;

                //            default:
                //                break;
                //        }
                //    }

                //}
                //else
                //{

                //    requests = requests.Where(a => a.ImCheckRequest_Number== CheckRequest_Number).ToList();
                //}

                //if (Company_ID != 0)
                //{
                //    requests = requests.Where(a => a.Importer_ID == Company_ID).ToList();
                //}

                #endregion

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message + "+++" + ex.InnerException.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetImCheckRequestList_filter
            (short IsApproved, short userId, long? outlet, List<string> Device_Info)
        {
            try
            {
                dbPrivilageEntities priv = new dbPrivilageEntities();
                if (outlet == null)
                {
                    var user = priv.PR_User.Where(p => p.Id == userId).FirstOrDefault();
                    var outlet_Data = uow.Repository<Outlet>().GetData().FirstOrDefault(o => o.ID_HR == user.Outlet_ID);
                    outlet = outlet_Data.ID;
                }
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                List<General_ImCheckRequestListDTO> requests = (from cc in entities.Im_CheckRequest
                                                        join rr in entities.Im_CheckRequest_Data on cc.ID equals rr.Im_CheckRequest_ID
                                                        join co in entities.Countries on rr.ExportCountry_Id equals co.ID
                                                        join chfr in entities.Im_CheckRequest_Final_Result on cc.ID equals chfr.Im_CheckRequest_ID into ims
                                                        // join frr in entities.Im_Final_Result on ims.ID equals frr.Im_CheckRequest_Final_Result

                                                        // join im in entities.Im_RequestCommittee on cc.ID equals im.ImCheckRequest_ID into ims
                                                        from chfr in ims.DefaultIfEmpty()

                                                        where outlet > 0 ? cc.Outlet_ID == outlet : cc.Outlet_ID == null
                                                        //&&
                                                        //cc.CheckRequest_Number=="36202124101346"
                                                        select new General_ImCheckRequestListDTO
                                                        {

                                                            Im_CheckRequest_ID = cc.ID,
                                                            ImCheckRequest_Number = cc.CheckRequest_Number,
                                                            Creation_Date = (DateTime)cc.User_Creation_Date,
                                                            IsAccepted = cc.IsAccepted,
                                                            ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                                                            Importer_ID = rr.Importer_ID,
                                                            ImporterType_Id = rr.ImporterType_Id,

                                                            Outlet_ID = cc.Outlet_ID,

                                                            Closed_Request = chfr.Im_Final_Result.Status,
                                                            Im_Final_Result_ID = chfr.Im_Final_Result_ID,
                                                            Name_Final_Result = lang == "1" ? chfr.Im_Final_Result.Ar_Name : chfr.Im_Final_Result.En_Name,

                                                            //CommitteeID = im == null? 0:im.ID,
                                                            //CommitteeType_ID= im == null ?6 : im.CommitteeType_ID
                                                        }).ToList();//.Skip(20).Take(20).OrderBy(a=>a.Im_CheckRequest_ID).

                List<General_ImCheckRequestListDTO> newRequests = new List<General_ImCheckRequestListDTO>();
                foreach (var re in requests)
                {
                    //if(re.CommitteeType_ID == 0 || re.CommitteeType_ID == 11)
                    {
                        newRequests.Add(re);
                    }
                }
                foreach (var req in newRequests)
                {
                    //req.PageSize = 100;

                    req.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == req.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault();
                    //req.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == rr.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault(),
                    //req.ImporterName = req.ImporterType_Id == 6 ? uow.Repository<Company_National>().GetData().Where(a => a.ID == req.ImporterType_Id).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault()
                    //: req.ImporterType_Id == 7 ? uow.Repository<Public_Organization>().GetData().Where(a => a.ID == req.ImporterType_Id).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault()
                    //: uow.Repository<Person>().GetData().Where(a => a.ID == req.ImporterType_Id).Select(s => s.Name).FirstOrDefault();

                    //req.Closed_Request = uow.Repository<Im_CheckRequest_Final_Result>().GetData().Where(a => a.Im_CheckRequest_ID == req.Im_CheckRequest_ID)
                    //                .Select(s => s.Im_Final_Result.Status).FirstOrDefault();
                    //    req.Closed_Request = uow.Repository<Im_CheckRequest_Final_Result>().GetData().Where(a => a.Im_CheckRequest_ID == req.Im_CheckRequest_ID)
                    //        .Select(s =>s.Im_Final_Result.Status).Last();
                    //    //req.ImporterTypeName = uow.Repository<A_SystemCode>().GetData().Where(a => a.Id == req.ImporterType_Id).Select(s => lang == "1" ? s.ValueName : s.ValueNameEN).FirstOrDefault();

                    if (req.ImporterType_Id == 6)
                    {
                        req.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                    }
                    else if (req.ImporterType_Id == 7)
                    {
                        req.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();

                    }
                    else
                    {
                        req.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == req.Importer_ID).Select(s => s.Name).FirstOrDefault();
                    }
                }

                //الطلب انتهى

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, newRequests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> FillItem_TypeDrop_Add(List<string> Device_Info)
        {
            try
            {
                // [Im_Final_Result]
                string lang = Device_Info[2];
                var data = uow.Repository<Im_Final_Result>().GetData().Where(a => a.IsActive == true).Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Ar_Name : c.En_Name),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList(); ;
                //set default value fz 17-4-2019
                data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
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
