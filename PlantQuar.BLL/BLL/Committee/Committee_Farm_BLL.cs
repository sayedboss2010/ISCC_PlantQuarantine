using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Committee
{
    public class Committee_Farm_BLL
    {
        private UnitOfWork uow;

        public Committee_Farm_BLL()
        {
            uow = new UnitOfWork();
        }

        //get committee type for request
        public Dictionary<string, object> getCommitteeTypeForrequest(long Farm_Requst_List, List<string> Device_Info)
        {
            try
            {
                long?[] requestId = { 910010 };
                string constrain = "";
                var req = uow.Repository<Farm_Request>().GetData().Include(f => f.FarmsData).Where(r => requestId.Contains(r.ID)).FirstOrDefault();
                var itemId = req.FarmsData.Item_ID;
                var countriesReq = uow.Repository<Farm_Country>().GetData().Include(c => c.Country).Where(c => requestId.Contains(c.Farm_Request_ID)).ToList();

                if (countriesReq.Count > 0)
                {
                    constrain = "لا بد من ادخال دول على الطلب  :";
                }
                var ispreview = false;
                var isAnalysis = false;
                //List<short> analysisTypeIdList = new List<short>();
                var committeeTypeId = 0;//*
                int countriesNo = countriesReq.Count;
                int counConNo = 0;
                List<string> countries = new List<string>();//*

                farmCountryConstrainReturnDTO data = new farmCountryConstrainReturnDTO();


                foreach (var cn in countriesReq)
                {
                    var farmConstrain = uow.Repository<Farm_Constrain>().GetData().Where(k => k.Item_ID == itemId && k.Country_Id == cn.Country_ID && k.IsActive == true).ToList();
                    //tolist
                    if (farmConstrain.Count > 0)
                    {
                        counConNo++;
                    }
                    else
                    {
                        countries.Add(cn.Country.Ar_Name);
                    }
                    foreach (var con in farmConstrain)
                    {

                        if (con.Is_Preview == true)
                        {
                            ispreview = true;
                        }
                        if (con.AnalysisType_ID != null)
                        {
                            isAnalysis = true;
                        }

                    }

                }
                if (ispreview == true && isAnalysis == true)
                {
                    committeeTypeId = 12;
                }
                else if (isAnalysis == true)
                {
                    committeeTypeId = 3;
                }
                else if (ispreview == true)
                {
                    committeeTypeId = 5;
                }
                if (countriesNo != counConNo)
                {
                    constrain = "لا بد من ادخال اشتراطات للدول :";
                    foreach (var ccc in countries)
                    {
                        constrain += ccc;
                    }
                }
                data.committeeTypeId = committeeTypeId;
                data.constrainText = constrain;
                data.countries = countries;
                // }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> getCommitteeAnalysisTypeForrequest(int analtype, List<Farm_Requst_ListDTO> requestId, List<string> Device_Info)
        {
            //long?[] requestId1 = requestId.ToArray();
            var requestId1 = requestId.Select(a => a.reqId).ToArray();
            //long?[] requestId1 = { 910004, 910008, 910010, 910011 };
            //long? [] requestId1 = {   910010 };
            try
            {
                var req = uow.Repository<Farm_Request>().GetData().Include(f => f.FarmsData).Where(r => requestId1.Contains(r.ID)).ToList();

                //var itemId = req.FarmsData.Item_ID;

                var countriesReq = uow.Repository<Farm_Country>().GetData().Where(c => requestId1.Contains(c.Farm_Request_ID)).Select(d => d.Country_ID).ToList();
                // var countriesReq = uow.Repository<Farm_Country>().GetData().Where(c => c.Farm_Request_ID == requestId).Select(d => d.Country_ID).ToList();
                string lang = Device_Info[2];
                List<CustomOptionLongId> analysisTypeIdList = new List<CustomOptionLongId>();
                var listIds = new List<long>();
                //en
                foreach (var item in req)
                {
                    foreach (var cn in countriesReq)
                    {
                        var farmConstrain = uow.Repository<Farm_Constrain>().GetData().Include(y => y.AnalysisType).Where(k => k.Item_ID == item.FarmsData.Item_ID && (k.Country_Id == cn || k.Country_Id == null)).ToList();
                        CustomOptionLongId at;
                        foreach (var con in farmConstrain)
                        {
                            if (con.AnalysisType_ID != null)
                            {
                                at = new CustomOptionLongId();
                                at.Value = con.AnalysisType_ID;
                                at.DisplayText = lang == "1" ? con.AnalysisType.Name_Ar : con.AnalysisType.Name_En;
                                if (!listIds.Contains((long)at.Value))
                                {
                                    listIds.Add((long)at.Value);
                                    analysisTypeIdList.Add(at);
                                }
                            }

                        }
                    }
                }
                analysisTypeIdList = analysisTypeIdList.Distinct().ToList();
                analysisTypeIdList.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = -1 });
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, analysisTypeIdList);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Get_Data_Committee(List<Farm_Requst_ListDTO> Req_List, List<string> Device_Info)
        {

            var requestId1 = Req_List.Select(a => a.reqId).ToArray();
            //var list_farm_Id = Req_List.Select(a => a.farm_Id).ToArray();
            // var list_farm_Commty_Id = Req_List.Select(a => a.Farm_Committee_ID).ToArray();
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var List_ItemCategories = (from fd in entities.FarmsDatas
                                           join fr in entities.Farm_Request on fd.ID equals fr.FarmsData_ID
                                           join fic in entities.Farm_ItemCategories on fd.ID equals fic.Farm_ID
                                           join fric in entities.Farm_Request_ItemCategories 
                                           on new { a = (long?)fr.ID, b = (long?)fic.ID } equals new { a = fric.Farm_Request_ID, b = fric.Farm_ItemCategories_ID }
                                           //join fric in entities.Farm_Request_ItemCategories  on fr.ID equals fric.Farm_Request_ID //&& fic.ID equals fric.Farm_ItemCategories_ID
                                           // join fric in entities.Farm_Request_ItemCategories  on new { a = fr.ID, b = fic.ID } equals new { a = fric.Farm_Request_ID, b = fric.Farm_ItemCategories_ID }
                                           join i in entities.Items on fd.Item_ID equals i.ID
                                           join ic in entities.ItemCategories on fic.ItemCategories_ID equals ic.ID

                                           where requestId1.Contains(fr.ID)
                                           select new Farm_Requst_ListDTO
                                           {
                                               farm_Id = fd.ID,
                                               farm_Name = fd.Name_Ar,
                                               reqId = fr.ID,
                                               Item_ID = i.ID,
                                               FarmCode_14 = fd.FarmCode_14,
                                               Item_Name = i.Name_Ar,
                                               ItemCategory_ID = fric.ID,
                                               ItemCategory_Name = ic.Name_Ar
                                               //committeeTypeId=fd.c
                                           }).ToList();

                //                SELECT*
                //from  Farm_Constrain fc
                //join FarmsData fd on fc.Item_ID = fd.Item_ID
                //join Farm_Request fr on fd.ID = fr.FarmsData_ID
                //join Farm_Committee fcom on fr.ID = fcom.Farm_Request_ID
                //join Farm_Request_ItemCategories fri on fr.ID = fri.Farm_Request_ID
                //join Farm_Country fc2
                //on  fr.ID = fc2.Farm_Request_ID AND fc.Country_Id = fc2.Country_ID

                //join dbo.AnalysisLabType at1 on fc.AnalysisType_ID = at1.ID
                //var test= entities.Farm_Constrain.Include(x=>x.)

                var List_Farm_Constrain_AnalysisTypes = (from fc in entities.Farm_Constrain
                                                         join fd in entities.FarmsDatas on fc.Item_ID equals fd.Item_ID
                                                         join fr in entities.Farm_Request on fd.ID equals fr.FarmsData_ID
                                                         join fcom in entities.Farm_Committee on fr.ID equals fcom.Farm_Request_ID
                                                         join fri in entities.Farm_Request_ItemCategories on fr.ID equals fri.Farm_Request_ID
                                                         // join fc2 in entities.Farm_Country on fr.ID equals fc2.Farm_Request_ID //AND fc.Country_Id = fc2.Country_Id
                                                         join fc2 in entities.Farm_Country on fr.ID equals fc2.Farm_Request_ID
                                                         //  on new { a = fr.ID, b = fc.Country_Id  }equals new { a = fc2.Farm_Request_ID, b = (fc2.Country_ID ) } 
                                                         join at1 in entities.AnalysisTypes on fc.AnalysisType_ID equals at1.ID into at11
                                                         from at1 in at11.DefaultIfEmpty()
                                                             //join fccl in entities.Farm_Country_CheckList on new { a = (long)fc2.ID, b = (long?)fccl.Country_ID, c = (bool?)fccl1.IsActive } equals new { a = fccl.Country_ID, b = null, c = true } into fccl1

                                                             //from fccl in fccl1.DefaultIfEmpty()
                                                         where requestId1.Contains(fr.ID)
                                                         select new Farm_Requst_ListDTO
                                                         {
                                                             Farm_Constrain_ID = fc.ID,
                                                             farm_Id = fd.ID,
                                                             AnalysisType_ID = fc.AnalysisType_ID,
                                                             AnalysisType_Name = at1.Name_Ar,
                                                             ConstrainText_text = fc.Farm_Constrain_Text.ConstrainText_Ar,
                                                             ConstrainText_ID = fc.Farm_Constrain_Text.ID,
                                                             Farm_Committee_ID = fcom.ID,
                                                             ItemCategory_ID = fri.ID
                                                         }).Distinct().ToList();


                ////var _Farm_Constrain_CheckList = (from fccl in entities.Farm_Country_CheckList
                //// where fccl.Farm_CheckList.IsActive == true
                //// && fccl.Farm_CheckList.User_Deletion_Date == null
                //// && fccl.Farm_CheckList.User_Deletion_Id == null
                ////&& fccl.IsActive == true
                ////&&fccl.Country_ID == null
                ////&&fccl.User_Deletion_Date == null
                ////&&fccl.User_Deletion_Id == null
                //// select new Farm_Country_CheckList_DTO
                //// {                                                     
                ////     Constrain_CheckList_ID = fccl.ID,
                ////     Constrain_CheckList_text = fccl.Farm_CheckList.ConstrainText_Ar,
                //// }).Distinct().ToList();
                //foreach (var Committee_ID in list_farm_Commty_Id)
                //{
                //    foreach (var item in _Farm_Constrain_CheckList)
                //    {

                //        var _Farm_Constrain_CheckList1 = (from fccl in entities.Farm_Country_CheckList
                //                                         where fccl.Farm_CheckList.IsActive == true
                //                                        && fccl.IsActive == true
                //                                        && fccl.Country_ID == null
                //                                         select new Farm_Country_CheckList_DTO
                //                                         {
                //////                                             Constrain_CheckList_ID = fccl.ID,
                //////                                             Constrain_CheckList_text = fccl.Farm_CheckList.ConstrainText_Ar,
                //////                                             Farm_Committee_ID = Committee_ID
                //    }).Distinct().ToList();

                //    }    
                //}

                //var List_Farm_Constrain_CheckList = (from fc in entities.Farm_Country
                //                                     join fr in entities.Farm_Request on fc.Farm_Request_ID equals fr.ID
                //                                     join fcm in entities.Farm_Committee on fr.ID equals fcm.Farm_Request_ID
                //                                     join fccl in entities.Farm_Country_CheckList on fc.Country_ID equals fccl.Country_ID
                //                                     join fd in entities.FarmsDatas on fccl.Item_ID equals fd.Item_ID
                //                                     where requestId1.Contains(fc.Farm_Request_ID)
                //                                     && fccl.Farm_CheckList.IsActive == true
                //                                    && fccl.Farm_CheckList.User_Deletion_Date == null
                //                                    && fccl.Farm_CheckList.User_Deletion_Id == null
                //                                    && fccl.IsActive == true
                //                                    //&& fccl.Country_ID != null
                //                                    && fccl.User_Deletion_Date == null
                //                                    && fccl.User_Deletion_Id == null
                //                                     select new Farm_Country_CheckList_DTO
                //                                     {
                //                                         farm_Id = fc.Farm_Request.FarmsData_ID,
                //                                         farm_Name = fc.Farm_Request.FarmsData.Name_Ar,
                //                                         Country_Name = fc.Country.Ar_Name,
                //                                         Farm_Committee_ID = fcm.ID,
                //                                         Constrain_CheckList_ID = fccl.ID,
                //                                         Constrain_CheckList_text = fccl.Farm_CheckList.ConstrainText_Ar,
                //                                     }).Distinct().ToList();

                //                select distinct fcc.* from Farm_Country_CheckList fccl
                //join FarmsData fd on fccl.Item_ID = fd.Item_ID
                //join Farm_Request fr on fr.FarmsData_ID = fd.id

                //     join Farm_CheckList fcc on fcc.id = fccl.Farm_CheckList_ID
                var List_Farm_Constrain_CheckList = (from fccl in entities.Farm_Country_CheckList
                                                     join fd in entities.FarmsDatas on fccl.Item_ID equals fd.Item_ID
                                                     join fr in entities.Farm_Request on fd.ID equals fr.FarmsData_ID
                                                     join c in entities.Countries on fccl.Country_ID equals c.ID into at11
                                                     from c in at11.DefaultIfEmpty()
                                                     where requestId1.Contains(fr.ID)
                                                     && fccl.Farm_CheckList.IsActive == true
                                                    && fccl.Farm_CheckList.User_Deletion_Date == null
                                                    && fccl.Farm_CheckList.User_Deletion_Id == null
                                                    && fccl.IsActive == true
                                                    //&& fccl.Country_ID != null
                                                    && fccl.User_Deletion_Date == null
                                                    && fccl.User_Deletion_Id == null

                                                     select new Farm_Country_CheckList_DTO
                                                     {
                                                         farm_Id = fr.FarmsData_ID,
                                                         farm_Name = fr.FarmsData.Name_Ar,
                                                         Country_Name = c.Ar_Name,
                                                         //Farm_Committee_ID = fcm.ID,
                                                         Constrain_CheckList_ID = fccl.ID,
                                                         Constrain_CheckList_text = fccl.Farm_CheckList.ConstrainText_Ar,
                                                     }).GroupBy(x => x.Constrain_CheckList_text).Select(x => x.FirstOrDefault()).ToList();
                //var List_Farm_Constrain_CheckList = _Farm_Constrain_CheckList.Concat(List_Farm_Country).ToList();

                //var Farm_Request_Data = List_ItemCategories.Concat(List_Farm_Constrain_AnalysisTypes).ToList();

                //Farm_Request_Data.FirstOrDefault().List_Farm_Country_CheckList = List_Farm_Constrain_CheckList.ToList();

                if (List_Farm_Constrain_AnalysisTypes.Count > 0)
                {
                    var Farm_Request_Data = (from lc in List_ItemCategories
                                             join lfc in List_Farm_Constrain_AnalysisTypes on lc.farm_Id equals lfc.farm_Id
                                             select new Farm_Requst_ListDTO
                                             {
                                                 farm_Id = lc.farm_Id,
                                                 farm_Name = lc.farm_Name,
                                                 reqId = lc.reqId,
                                                 Item_ID = lc.Item_ID,
                                                 Item_Name = lc.Item_Name,
                                                 ItemCategory_ID = lc.ItemCategory_ID,
                                                 ItemCategory_Name = lc.ItemCategory_Name,

                                                 AnalysisType_ID = lfc.AnalysisType_ID,
                                                 AnalysisType_Name = lfc.AnalysisType_Name,
                                                 ConstrainText_ID = lfc.ConstrainText_ID,
                                                 ConstrainText_text = lfc.ConstrainText_text,
                                                 Farm_Committee_ID = lfc.Farm_Committee_ID,
                                                 List_Farm_Country_CheckList = List_Farm_Constrain_CheckList.ToList()
                                             }).ToList();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Farm_Request_Data);
                }
                else
                {
                    var Farm_Request_Data = (from lc in List_ItemCategories

                                             select new Farm_Requst_ListDTO
                                             {
                                                 farm_Id = lc.farm_Id,
                                                 farm_Name = lc.farm_Name,
                                                 reqId = lc.reqId,
                                                 Item_ID = lc.Item_ID,
                                                 Item_Name = lc.Item_Name,
                                                 ItemCategory_ID = lc.ItemCategory_ID,
                                                 ItemCategory_Name = lc.ItemCategory_Name,
                                                 List_Farm_Country_CheckList = List_Farm_Constrain_CheckList.ToList()
                                             }).ToList();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Farm_Request_Data);
                }


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Insert_Committee(Farm_Committee_Requst_All_DTO entity, List<string> Device_Info)
        {
            try
            {
                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        var operationType = 78; //ask
                                                //long Committe_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_RequestCommittee_seq");
                        short _User_Creation_Id = 1;
                        var _User_Creation_Date = DateTime.Now;
                        foreach (var item in entity.List_Committee)
                        {
                            _User_Creation_Id = item.User_Creation_Id;
                            #region Farm_Request      
                            //var x = context.Farm_Request.SingleOrDefault(a => a.ID == item.Farm_Request_ID);
                            //var y= context.Farm_Request.First(a=>a.IsStatus==false);
                            //x.
                            Farm_Request CModel_Farm_Request = uow.Repository<Farm_Request>().Findobject(item.Farm_Request_ID);
                            //CModel_Farm_Request.IsStatus = false;

                            //////context.Farm_Request.(CModel_Farm_Request);
                            ////context.SaveChanges();
                            //uow.Repository<Farm_Request>().Update(CModel_Farm_Request);
                            //uow.SaveChanges();
                            #endregion
                            #region Farm_Committee
                            Farm_Committee CModel = uow.Repository<Farm_Committee>().Findobject(item.ID);

                          

                            var committeId = item.ID;

                            //العميل رفض او اللجنه اتلغت او العميل مردش سواء الوقت انتهى او لا

                            Farm_Committee CModel2 = new Farm_Committee();
                          
                            if (CModel.Is_Cancel==true || CModel.IsApproved==false||CModel.Status==false)
                            {
                                CModel.Delegation_Date = null;

                                var ID_SampleData = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_SEQ");

                                CModel2.User_Creation_Date = _User_Creation_Date;
                                CModel2.User_Creation_Id = _User_Creation_Id;                           
                                CModel2.ID = ID_SampleData;                           
                                CModel2.Farm_Request_ID = CModel.Farm_Request_ID;

                                uow.Repository<Farm_Committee>().InsertRecord(CModel2);
                                uow.SaveChanges();
                                 
                                committeId = CModel2.ID;
                            }


                              CModel = uow.Repository<Farm_Committee>().Findobject(committeId);


                            CModel.User_Updation_Date = item.User_Updation_Date;
                            CModel.User_Updation_Id = item.User_Updation_Id;
                            CModel.CommitteeType_ID = item.CommitteeType_ID;
                            CModel.Delegation_Date = item.Delegation_Date = item.Delegation_Date;
                            CModel.StartTime = item.StartTime;
                            CModel.EndTime = item.EndTime;
                            if (entity.List_ShiftTiming == null)
                            {
                                CModel.IsPaid = true;
                            }
                            // المفروض تكون حسب التاريخ اللى جاي مع الركوست لو بينها تبقى 1 لو لا تبقى null

                            if (CModel.Delegation_Date >= CModel_Farm_Request.Start_Date_Request && CModel.Delegation_Date <= CModel_Farm_Request.End_Date_Request)
                            {
                                CModel.IsApproved = true;
                            }
                            else
                            {
                                CModel.IsApproved = null;
                            }
                            CModel.Status = false;
                            //context.SaveChanges();
                            uow.Repository<Farm_Committee>().Update(CModel);
                            uow.SaveChanges();
                            #endregion

                            #region Employee              
                            if (entity.List_emp.Count > 0)
                            {
                                foreach (var item_Emp in entity.List_emp)
                                {
                                    long _Employee_Id = long.Parse(item_Emp.Employee_Id.ToString());

                                    var Comm_Employee = new CommitteeEmployee
                                    {
                                        Committee_ID = committeId,
                                        Employee_Id = _Employee_Id,
                                        ISAdmin = item_Emp.ISAdmin,
                                        OperationType = operationType,
                                        User_Creation_Id = item.User_Creation_Id,
                                        User_Creation_Date = item.User_Creation_Date,
                                    };
                                    context.CommitteeEmployees.Add(Comm_Employee);
                                    context.SaveChanges();
                                }
                                //CommitteeBLL committeeBLL = new CommitteeBLL();
                                //committeeBLL.Send_Committe_Employee(entity.ID, operationType,
                                //    (DateTime)entity.User_Creation_Date, entity.User_Creation_Id, entity.com_emp, Device_Info);
                            }
                            #endregion

                            #region Shift

                            if (entity.List_ShiftTiming != null)
                            {
                                if (entity.List_ShiftTiming.Count > 0)
                                {
                                    foreach (var item_Shift in entity.List_ShiftTiming)
                                    {
                                        var id = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_Shift_seq");
                                        var _Amount = decimal.Parse(item_Shift.Amount.ToString());
                                        var Committee_Shift = new Farm_Committee_Shift
                                        {
                                            ID = id,
                                            Farm_Committee_ID = committeId,
                                            ShiftTiming_ID = item_Shift.ShiftTiming_ID,
                                            Count = item_Shift.Count,
                                            Amount = _Amount,
                                            User_Creation_Id = item.User_Creation_Id,
                                            User_Creation_Date = item.User_Creation_Date,
                                        };
                                        context.Farm_Committee_Shift.Add(Committee_Shift);
                                        context.SaveChanges();
                                        //var CModel = Mapper.Map<RequestCommittee_ShiftDTO, Im_RequestCommittee_Shift>(item);
                                        //    uow.Repository<Im_RequestCommittee_Shift>().InsertRecord(CModel);
                                        //    uow.SaveChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Examination
                            var farmcategories = uow.Repository<Farm_Request_ItemCategories>().GetData().Where(f => f.Farm_Request_ID == item.Farm_Request_ID).ToList();
                            foreach (var cat in farmcategories)
                            {
                                var id_Examination = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_Examination_seq");
                                var Comm_Examination = new Farm_Committee_Examination
                                {

                                    ID = id_Examination,
                                    Farm_Request_ItemCategories_ID = cat.ID,
                                    IsAccepted = null,
                                    FarmCommittee_ID = committeId,
                                    User_Creation_Id = item.User_Creation_Id,
                                    User_Creation_Date = item.User_Creation_Date,
                                };
                                context.Farm_Committee_Examination.Add(Comm_Examination);
                                context.SaveChanges();
                                //var obj = Mapper.Map<Farm_Committee_Examination>(dto);

                                //uow.Repository<Farm_Committee_Examination>().InsertRecord(obj);
                                //uow.SaveChanges();
                            }
                            #endregion
                            #region
                            ////حفظ الاشتراطات للمزرعة كلها
                            //#region Committee_Constrain
                            ////if (entity.List_Constrain_Text.Count > 0)
                            ////{

                            //    var _List_Constrain_Text = (from fd in context.FarmsDatas
                            //                                     join fr in context.Farm_Request on fd.ID equals fr.FarmsData_ID
                            //                                     join fc in context.Farm_Country on fr.ID equals fc.Farm_Request_ID
                            //                                     join fcc in context.Farm_Constrain on new
                            //                                     { a = (short?)fc.Country_ID, b = (long?)fd.Item_ID } equals new { a = fcc.Country_Id, b = fcc.Item_ID }
                            //                                     where fr.ID == item.Farm_Request_ID
                            //                                     &&fcc.IsActive==true
                            //                                     select new Farm_Committee_ConstrainDTO
                            //                                     {
                            //                                         Farm_Constrain_ID = fcc.ID,
                            //                                     }).ToList();
                            //    foreach (var item__Constrain in _List_Constrain_Text)
                            //    {
                            //        var farmConstrain_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_Constrain_seq");
                            //        var Comm_Employee = new Farm_Committee_Constrain
                            //        {
                            //            ID = farmConstrain_ID,
                            //            Farm_Committee_ID = item.ID,
                            //            Farm_Constrain_ID = item__Constrain.Farm_Constrain_ID
                            //        };
                            //        context.Farm_Committee_Constrain.Add(Comm_Employee);
                            //        context.SaveChanges();
                            //    }
                            ////}
                            //#endregion

                            //#region Farm_Committee_CheckList
                            //var _Farm_Committee_CheckList = (from fd in context.FarmsDatas
                            //                                 join fr in context.Farm_Request on fd.ID equals fr.FarmsData_ID
                            //                                 join fc in context.Farm_Country on fr.ID equals fc.Farm_Request_ID
                            //                                 join fcc in context.Farm_Country_CheckList on new
                            //                                 { a = (short?)fc.Country_ID, b = (long?)fd.Item_ID } equals new { a = fcc.Country_ID, b = fcc.Item_ID }
                            //                                 where fr.ID == item.Farm_Request_ID
                            //                                 && fcc.IsActive == true
                            //                                 select new Farm_Committee_CheckList_DTO
                            //                                 {
                            //                                     Farm_Country_CheckList_ID = fcc.ID,
                            //                                 }).ToList();


                            //foreach (var CheckList in _Farm_Committee_CheckList)
                            //{

                            foreach (var CheckList in entity.List_CheckList)
                            {
                                var ID_Committee_CheckList = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_Committee_CheckList_seq");
                                var Comm_Committee_CheckList = new Farm_Committee_CheckList
                                {
                                    ID = ID_Committee_CheckList,
                                    FarmCommittee_ID = committeId,
                                    Farm_Country_CheckList_ID = CheckList.Farm_Country_CheckList_ID,
                                    User_Creation_Id = _User_Creation_Id,
                                    User_Creation_Date = _User_Creation_Date,
                                };
                                context.Farm_Committee_CheckList.Add(Comm_Committee_CheckList);
                                context.SaveChanges();
                            }
                            //#endregion
                            #endregion

                            //context.Database.ExecuteSqlCommand(
                            //       "EXEC Farm_Inser_Commit @Farm_Request, @Farm_Committee_ID, @User_Creation_Id",
                            //       new SqlParameter("@Farm_Request", item.Farm_Request_ID),
                            //       new SqlParameter("@Farm_Committee_ID", item.ID),
                            //       new SqlParameter("@User_Creation_Id", item.User_Creation_Id)

                            //   ); context.SaveChanges();
                            //          Farm_Inser_Commit data = uow.Repository<Station_Get_Data_ResultDTO>().CallStored("Station_Get_Data", paramters_Type,
                            //paramters_Data, Device_Info).ToList();
                            //context.Farm_Inser_Commit("Farm_Inser_Commit", paramters_Type,paramters_Data, Device_Info).ToList();
                            //context.Farm_Inser_Commit(item.Farm_Request_ID, item.ID, _User_Creation_Id);
                            //context.SaveChanges();
                            //context.Farm_Inser_Commit(item.Farm_Request_ID, item.ID, _User_Creation_Id);

                        }

                        #region Farm_SampleData
                        if (entity.List_Farm_SampleData != null)
                        {
                            Random rd = new Random();
                            foreach (var cat in entity.List_Farm_SampleData)
                            {

                                var ID_SampleData = uow.Repository<Object>().GetNextSequenceValue_Long("Farm_SampleData_Item_seq");
                                string rand = rd.Next(0, 100000).ToString("D5");
                                // save barcode
                                var dayofyear = "000" + DateTime.Now.DayOfYear;
                                var zx = DateTime.Now.Year.ToString().Substring(2);
                                var hour = (DateTime.Now.Hour).ToString("D" + 2);
                                var min = (DateTime.Now.Minute).ToString("D" + 2);
                                var sec = (DateTime.Now.Second).ToString("D" + 2);
                                string barcode = "78" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;

                                var Comm_SampleData_Item = new Farm_SampleData_Item
                                {
                                    ID = ID_SampleData,
                                    FarmCommittee_ID = cat.FarmCommittee_ID,
                                    Farm_Request_ItemCategories_ID = cat.Farm_Request_ItemCategories_ID,
                                    AnalysisLabType_ID = cat.AnalysisLabType_ID,
                                    Sample_BarCode = barcode,
                                    User_Creation_Id = _User_Creation_Id,
                                    User_Creation_Date = _User_Creation_Date,
                                };
                                context.Farm_SampleData_Item.Add(Comm_SampleData_Item);
                                context.SaveChanges();
                            }
                        }
                        #endregion


                        trans.Commit();
                        //foreach (var item in entity.List_Committee)
                        //{
                        //    Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                        //    paramters_Type.Add("Farm_Request", SqlDbType.BigInt);
                        //    paramters_Type.Add("Farm_Committee_ID", SqlDbType.BigInt);
                        //    paramters_Type.Add("User_Creation_Id ", SqlDbType.BigInt);
                        //    Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                        //    paramters_Data.Add("Farm_Request", item.Farm_Request_ID.ToString());
                        //    paramters_Data.Add("Farm_Committee_ID", item.ID.ToString());
                        //    paramters_Data.Add("User_Creation_Id", item.User_Creation_Id.ToString());
                        //    uow.Repository<Farm_Inser_Commit_DTO>().CallStored("Farm_Inser_Commit", paramters_Type,
                        //        paramters_Data, Device_Info).ToList();
                        //}

                        //                        DECLARE @Farm_Request bigint = 424,
                        //@Farm_Committee_ID bigint = 41,
                        //@User_Creation_Id bigint = 1

                    }
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch (Exception ex)
            {
                foreach (var item in entity.List_Committee)
                {
                    //#region Farm_Request                      
                    //Farm_Request CModel_Farm_Request = uow.Repository<Farm_Request>().Findobject(item.Farm_Request_ID);
                    //CModel_Farm_Request.IsStatus = null;
                    //uow.Repository<Farm_Request>().Update(CModel_Farm_Request);
                    //uow.SaveChanges();
                    //#endregion

                    #region Farm_Committee
                    Farm_Committee CModel = uow.Repository<Farm_Committee>().Findobject(item.ID);
                    CModel.User_Updation_Date = null;
                    CModel.User_Updation_Id = null;
                    CModel.CommitteeType_ID = null;
                    CModel.Delegation_Date = null;
                    CModel.StartTime = null;
                    CModel.EndTime = null;
                    CModel.IsApproved = null; // المفروض تكون حسب التاريخ اللى جاي مع الركوست لو بينها تبقى 1 لو لا تبقى null
                    CModel.Status = null;
                    //context.SaveChanges();
                    uow.Repository<Farm_Committee>().Update(CModel);
                    uow.SaveChanges();
                    #endregion
                }
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }

    //public static void ExecuteStoredProcedure(this ObjectContext context, string storedProcName, params object[] parameters)
    //{
    //    string command = "EXEC " + storedProcName + " @caseid, @userid, @warnings";

    //    context.ExecuteStoreCommand(command, parameters);
    //}
}
