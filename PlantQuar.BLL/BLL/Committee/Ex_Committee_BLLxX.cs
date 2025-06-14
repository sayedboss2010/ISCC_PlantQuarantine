using AutoMapper;
using PlantQuar.BLL.BLL.Log;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Committee;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;


namespace PlantQuar.BLL.BLL.Committee
{
    public class Ex_Committee_BLL
    {
        private UnitOfWork uow;


        public Ex_Committee_BLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetCreationDateForRequest(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Ex_CheckRequest>().GetData().FirstOrDefault(r => r.ID == id);
            if (req != null)
            {
                var dto = Mapper.Map<EX_CheckRequest_Committee_DTO>(req);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dto);

                //  return req.User_Creation_Date.ToString();
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "");

            }
        }
        public Dictionary<string, object> checkRequestCommitteeExist(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Ex_RequestCommittee>().GetData().FirstOrDefault(r => r.ExCheckRequest_ID == id && r.Delegation_Date != null);
            if (req != null)
            {

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, true);

                //  return req.User_Creation_Date.ToString();
            }
            else
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, false);

            }
        }

        public Dictionary<string, object> GetEX_CheckRequestDetails
        (long EX_CheckRequest_ID, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var CheckRequestDetails = (from cc in entities.Ex_CheckRequest
                                           where cc.ID == EX_CheckRequest_ID
                                           select new Ex_Committee_DTO
                                           {
                                               EX_CheckRequest_ID = cc.ID,
                                               EX_CheckRequest_Number = cc.CheckRequest_Number,
                                               OutLet_Name = cc.Outlet.Ar_Name,
                                               OutLet_ID = cc.Outlet.ID,
                                               ExportCountry_Id = cc.Ex_CheckRequest_Data.FirstOrDefault().ExportCountry_Id
                                           }).FirstOrDefault();
                if (CheckRequestDetails != null)
                {
                    //foreach (var item in CheckRequestDetails)
                    //{
                    //Items
                    var itemss = (from EX_i in entities.Ex_CheckRequest_Items
                                  join isn in entities.Item_ShortName on EX_i.Item_ShortName_ID equals isn.ID
                                  where EX_i.Ex_CheckRequest_ID == EX_CheckRequest_ID
                                  group EX_i by new
                                  {
                                      EX_i.Item_ShortName_ID,
                                      isn.ShortName_Ar,
                                      isn.ShortName_En,
                                      Item_ID = isn.Item.ID,
                                      ItemName_Ar = isn.Item.Name_Ar,
                                      ItemName_En = isn.Item.Name_En,
                                      qualitiveGroupName = isn.QualitativeGroup.Name_Ar,
                                      qualitiveGroupNameEn = isn.QualitativeGroup.Name_En,
                                      //InitiatorCountry = EX_i.Im_Initiator.Country.Ar_Name,
                                      //InitiatorCountryEn = EX_i.Im_Initiator.Country.En_Name,
                                  } into grp
                                  select new EX_Items_checkReq_New
                                  {
                                      Item_ShortName_ID = grp.Key.Item_ShortName_ID,
                                      ItemShortNameAr = grp.Key.ShortName_Ar,
                                      ItemShortNameEn = grp.Key.ShortName_En,
                                      Item_ID = grp.Key.Item_ID,
                                      ItemName_Ar = grp.Key.ItemName_Ar,
                                      ItemName_En = grp.Key.ItemName_En,
                                      qualitiveGroupName = grp.Key.qualitiveGroupName,
                                      qualitiveGroupNameEn = grp.Key.qualitiveGroupNameEn,
                                      //InitiatorCountry = grp.Key.InitiatorCountry,
                                      //InitiatorCountryEn = grp.Key.InitiatorCountryEn,
                                      GrossWeight = grp.Sum(q => q.GrossWeight),
                                      Net_Weight = grp.Sum(q => q.Net_Weight),
                                  }).Distinct().ToList();
                    foreach (var itm in itemss)
                    {
                        var itemShortName = (from isn in entities.Item_ShortName
                                             where isn.ID == itm.Item_ShortName_ID
                                             select new EX_Items_checkReq_New
                                             {
                                                 subPartName = (lang == "1" ? isn.SubPart.Name_Ar : isn.SubPart.Name_En)
                                             }
                                            ).FirstOrDefault();
                        var catAndLots = (from EX_i in entities.Ex_CheckRequest_Items
                                          join v in entities.Ex_CheckRequest_Items_Lot_Category on EX_i.ID equals v.Ex_CheckRequest_Items_ID

                                          where EX_i.Ex_CheckRequest_ID == EX_CheckRequest_ID && EX_i.Item_ShortName_ID == itm.Item_ShortName_ID
                                          select new EX_categories_lots_New
                                          {
                                              ID_Lot = v.ID,
                                              categoryName = (lang == "1" ? EX_i.ItemCategory.Name_Ar : EX_i.ItemCategory.Name_En),
                                              EX_checkReqItems_ID = v.Ex_CheckRequest_Items_ID,
                                              ItemCategory_ID = EX_i.ItemCategory_ID,
                                              Size = v.Size,
                                              Package_Count = v.Package_Count,
                                              Package_Weight = v.Package_Weight,
                                              Package_Net_Weight = v.Package_Net_Weight,
                                              Units_Number = v.Units_Number,
                                              packageTypeID = v.Package_Type_ID,
                                              GrossWeight = v.GrossWeight,
                                              Package_Based_Weight = v.Package_Based_Weight,
                                              packageMaterialID = v.Package_Material_ID,
                                              Lot_Number = v.Lot_Number,
                                              Grower_Number = v.Grower_Number,
                                              Waybill = v.Waybill,
                                              Number_Wooden_Package = v.Number_Wooden_Package,
                                              Net_Weight = v.Net_Weight,

                                              ////////////// ///Hadeer 24-1-2024  //////////////////
                                              FarmCode_14 = EX_i.FarmsData.FarmCode_14,
                                              Governate_Name = EX_i.Governate.Ar_Name == null ? EX_i.FarmsData.Governate.Ar_Name : EX_i.Governate.Ar_Name,
                                              Center_Name = EX_i.Center.Ar_Name == null ? EX_i.FarmsData.Center.Ar_Name : EX_i.Center.Ar_Name,
                                              Village_Name = EX_i.Village.Ar_Name == null ? EX_i.FarmsData.Village.Ar_Name : EX_i.Village.Ar_Name,
                                              Agriculture_Hand = EX_i.Agriculture_Hand,
                                              /////// End Hadeer

                                              // عرض اللوطات اللى اتعملها فحص
                                              // 0 لم يتم تشكيل لجنة على اللوط
                                              // 1  تم تشكيل لجنة على اللوط ولم يعمل الاندريد
                                              // 2  تم تشكيل لجنة على اللوط وتم عمل الاندريد
                                              //Check_Lot_Old_ID = p.LotData_ID >0 ?  p.CommitteeResultType_ID ==null ?p.Date <DateTime.Now?0: p.CommitteeResultType_ID !=7? 1 :1 : 0:0,

                                              //Check_Lot_Old_ID = p.LotData_ID > 0 ? p.CommitteeResultType_ID > 0 ? (p.CommitteeResultType_ID != 7 ? 1:0):(p.Im_RequestCommittee.Delegation_Date >= Date_Check1 ? 1:0) : 0 ,

                                              //بيانات النبات
                                              ID_EX_Item = v.Ex_CheckRequest_Items.ID,
                                              //باقى ابعت بيانات الحاوية
                                              ContainerNumber = v.Ex_CheckRequset_Shipping_Method.ContainerNumber,
                                              containerName = entities.A_SystemCode.Where(c => c.Id == v.Ex_CheckRequset_Shipping_Method.containers_ID).FirstOrDefault().ValueName,
                                              containerType = entities.A_SystemCode.Where(c => c.Id == v.Ex_CheckRequset_Shipping_Method.containers_type_ID && c.SystemCodeTypeId == 28).FirstOrDefault().ValueName,

                                              ShipholdNumber = v.Ex_CheckRequset_Shipping_Method.ShipholdNumber,
                                              NavigationalNumber = v.Ex_CheckRequset_Shipping_Method.NavigationalNumber,

                                              Total_Weight = v.Ex_CheckRequset_Shipping_Method.Total_Weight,

                                              // نهاية بيانات الحاوية
                                              //NOURA
                                              Order_TextLot = v.Order_Text,
                                              RecordedOrNot = EX_i.ItemCategory_ID == null ? "#####" : ((bool)EX_i.ItemCategory.IsRegister ? "مسجل" : "غير مسجل"),
                                              ItemCategoryGroup = EX_i.ItemCategory_ID == null ? "#####" : EX_i.ItemCategory.ItemCategories_Group_ID == null ? "لا يوجد"
                                          : (lang == "1" ? EX_i.ItemCategory.ItemCategories_Group.Name_Ar : EX_i.ItemCategory.ItemCategories_Group.Name_En),
                                              //subPartName =,
                                              packageMaterialName = (lang == "1" ? entities.Package_Material.Where(c => c.ID == v.Package_Material_ID).FirstOrDefault().Ar_Name :
                                          entities.Package_Material.Where(c => c.ID == v.Package_Material_ID).FirstOrDefault().En_Name),

                                              packageType = (lang == "1" ? entities.Package_Type.Where(c => c.ID == v.Package_Type_ID).FirstOrDefault().Ar_Name :
                                          entities.Package_Type.Where(c => c.ID == v.Package_Type_ID).FirstOrDefault().En_Name),
                                              //Lot_Result_Status = p.IS_Status,//(p.IS_Status == null ?false : p.IS_Status),
                                          }).ToList();

                        itm.ItemCategories_lots = catAndLots;
                        foreach (var item in catAndLots)
                        {
                            var _CommitteeResult = (from icr in entities.Ex_CommitteeResult
                                                    where icr.LotData_ID == item.ID_Lot
                                                    group icr by new
                                                    {
                                                        //id= icr.ID,
                                                        Delegation_Date = icr.Ex_RequestCommittee.Delegation_Date,
                                                        CommitteeResultType_ID = icr.CommitteeResultType_ID,
                                                        Check_Lot_Old_Name = icr.LotData_ID > 0 ? icr.CommitteeResultType_ID > 0 ? "تم عمل الاندريد" : "تم التشكيل ولم يتم الاندريد" : "لم يتم العمل عليه",
                                                    } into grp
                                                    select new EX_categories_lots_New
                                                    {
                                                        //id
                                                        EX_CommitteeResult_ID = grp.Max(a => a.ID),
                                                        Delegation_Date = grp.Key.Delegation_Date,
                                                        CommitteeResultType_ID = grp.Key.CommitteeResultType_ID,

                                                    }).ToList();
                            var maxValue = _CommitteeResult.Max(x => x.EX_CommitteeResult_ID);
                            if (_CommitteeResult != null)
                            {
                                item.Delegation_Date = _CommitteeResult.Where(a => a.EX_CommitteeResult_ID == maxValue).Select(a => a.Delegation_Date).FirstOrDefault();
                                item.EX_CommitteeResult_ID = _CommitteeResult.Where(a => a.EX_CommitteeResult_ID == maxValue).Select(a => a.EX_CommitteeResult_ID).FirstOrDefault();
                                item.CommitteeResultType_ID = _CommitteeResult.Where(a => a.EX_CommitteeResult_ID == maxValue).Select(a => a.CommitteeResultType_ID).FirstOrDefault();
                            }
                            //المعامل السابقة
                            var committee_Sample = (from cr in entities.Ex_CheckRequest_SampleData
                                                    where cr.LotData_ID == item.ID_Lot
                                                    select new EX_Committee_Sample_Lot
                                                    {
                                                        LotData_ID = cr.LotData_ID,
                                                        AnalysisLabType_ID = cr.AnalysisLabType_ID,
                                                        Analysis_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisType.Name_Ar : cr.AnalysisLabType.AnalysisType.Name_En),
                                                        Lab_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisLab.Name_Ar : cr.AnalysisLabType.AnalysisLab.Name_En),
                                                    }).ToList();
                            item.list_Committee_Sample_Lot = committee_Sample;
                            //Noura
                            #region  المعالجات السابقة
                            var committee_Treatment = (from td in entities.Ex_Request_TreatmentData
                                                       where td.Ex_Request_LotData_ID == item.ID_Lot

                                                       group td by new
                                                       {
                                                           ID = td.ID,
                                                           TreatmentMethod_Name = (lang == "1" ? entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().Ar_Name :
                                                             entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().En_Name),
                                                           TreatmentType_Name = (lang == "1" ? entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().Ar_Name :
                                                             entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().En_Name),
                                                           //TreatmentMat_Name = (lang == "1" ? entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_Ar :
                                                           //  entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_En),
                                                           TreatmentMat_Name = td.TreatmentMaterial.Item.Name_Ar,
                                                       } into grp
                                                       select new EX_Committee_Treatment_Lot
                                                       {
                                                           ID = grp.Key.ID,
                                                           TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                                           TreatmentType_Name = grp.Key.TreatmentType_Name,
                                                           TreatmentMat_Name = grp.Key.TreatmentMat_Name,
                                                           TreatmentMat_Amount = grp.Sum(c => c.TreatmentMat_Amount),
                                                       }).Distinct().ToList();

                            item.list_Committee_Treatment_Lot = committee_Treatment;

                            #endregion

                            //حالة اللوط فى تعذر الفحص

                            //var Lot_Result_Is_Cancel = (from lr in entities.Ex_CommitteeResult
                            //                                     join rc in entities.Ex_RequestCommittee on lr.Committee_ID equals rc.ID
                            //                                     where lr.LotData_ID == item.ID_Lot
                            //                                     && rc.Is_Cancel == 1
                            //                                     select new EX_Lot_Result_Status
                            //                                     {
                            //                                         Is_Continue = 1,
                            //                                     }).FirstOrDefault();

                            //bool Check_lot = true;
                            //if (Lot_Result_Is_Cancel != null)
                            //{
                            //    if (Lot_Result_Is_Cancel.Is_Continue == 0)
                            //    {
                            //        Check_lot = false;
                            //    }
                            //}
                            //حالة اللوط فى الفحص لو مصاب

                            var Lot_Result_Ex_CommitteeResult = (from lr in entities.Ex_CommitteeResult
                                                                 where lr.LotData_ID == item.ID_Lot
                                                                 && lr.CommitteeResultType_ID == 3
                                                                 select new EX_Lot_Result_Status
                                                                 {
                                                                     Is_Continue = 0,
                                                                 }).FirstOrDefault();

                            bool Check_lot = true;
                            if (Lot_Result_Ex_CommitteeResult != null)
                            {
                                if (Lot_Result_Ex_CommitteeResult.Is_Continue == 0)
                                {
                                    Check_lot = false;
                                }
                            }


                            //عبير تعذر الفحص
                            //var Lot_Result_Ex_CommitteeResult = (from lr in entities.Ex_CommitteeResult
                            //                                     where lr.LotData_ID == item.ID_Lot
                            //                                     && lr.CommitteeResultType_ID == 3
                            //                                     select new EX_Lot_Result_Status
                            //                                     {
                            //                                         Is_Continue = 0,
                            //                                     }).FirstOrDefault();

                            //bool Check_lot = true;
                            //if (Lot_Result_Ex_CommitteeResult != null)
                            //{
                            //    if (Lot_Result_Ex_CommitteeResult.Is_Continue == 0)
                            //    {
                            //        Check_lot = false;
                            //    }
                            //}
                            // انتهاء بدون عمل
                            var Lot_Result_Ex_Finch = (from lr in entities.Ex_CommitteeResult
                                                       where lr.LotData_ID == item.ID_Lot
                                                       && lr.Ex_RequestCommittee.IsFinishedAll == false
                                                       select new EX_Lot_Result_Status
                                                       {
                                                           LotData_ID = item.ID_Lot,
                                                           commite_No = lr.Ex_RequestCommittee.CommitteeType_ID,

                                                           Status_Name = "",

                                                           IS_Status = 1,
                                                           IS_Status_Committee = 1,

                                                           Is_Continue = 1,

                                                       }).ToList();
                            item.list_Lot_Result_Status = Lot_Result_Ex_Finch;

                            //عند رفض العميل
                            var Lot_Result_IsApproved = (from lr in entities.Ex_CommitteeResult
                                                         where lr.LotData_ID == item.ID_Lot
                                                         && lr.Ex_RequestCommittee.IsApproved == false

                                                         select new EX_Lot_Result_Status
                                                         {
                                                             LotData_ID = item.ID_Lot,
                                                             commite_No = lr.Ex_RequestCommittee.CommitteeType_ID,

                                                             Status_Name = "",

                                                             IS_Status = 1,
                                                             IS_Status_Committee = 1,

                                                             Is_Continue = 1,

                                                         }).ToList();
                            item.list_Lot_Result_Status = Lot_Result_IsApproved;

                            //حالة اللوط فى جشنى وتم انتهاء العمل

                            var Lot_Result_Ex_CommitteeGashny = (from lr in entities.Ex_CommitteeResult
                                                                 where lr.LotData_ID == item.ID_Lot
                                                                 && lr.Ex_RequestCommittee.CommitteeType_ID == 2
                                                                 && lr.CommitteeResultType_ID == 1
                                                                 select new EX_Lot_Result_Status
                                                                 {
                                                                     Is_Continue = 1,
                                                                 }).FirstOrDefault();

                            //bool Check_lotGashny = true;
                            if (Lot_Result_Ex_CommitteeGashny != null)
                            {
                                if (Lot_Result_Ex_CommitteeGashny.Is_Continue == 1)
                                {
                                    Check_lot = false;
                                }
                            }
                            //Lot_Result_Status حالات اللوط

                            var Lot_Result_Status = (from lr in entities.Ex_CheckRequest_Items_Lot_Result
                                                     join ls in entities.Ex_CheckRequest_Lot_Result_Status on lr.IS_Status equals ls.ID

                                                     where lr.Ex_CheckRequest_Items_Lot_Category_ID == item.ID_Lot
                                                     //   && lr.IS_Status_Committee==null

                                                     select new EX_Lot_Result_Status
                                                     {
                                                         LotData_ID = lr.Ex_CheckRequest_Items_Lot_Category_ID,
                                                         commite_No = ls.CommitteeType_ID,
                                                         Status_Name = (lang == "1" ? ls.Name_AR : ls.Name_En),
                                                         Nots_Result_Status = lr.Nots,
                                                         IS_Status = lr.IS_Status,
                                                         IS_Status_Committee = (lr.IS_Status_Committee == null ? 1 : 0),
                                                         //Is_Continue =((ls.Is_Continue == true ? 1 : 0)),
                                                         Is_Continue = (Check_lot == false ? 0 : (ls.Is_Continue == true ? 1 : 0)),
                                                     }).ToList();
                            item.list_Lot_Result_Status = Lot_Result_Status;

                            if (Lot_Result_Status.Count() == 0)
                            {
                                if (Check_lot == false)
                                {
                                    var Lot_Result_Ex_CommitteeResult55 = (from lr in entities.Ex_CommitteeResult
                                                                           where lr.LotData_ID == item.ID_Lot
                                                                           && lr.CommitteeResultType_ID == 3
                                                                           select new EX_Lot_Result_Status
                                                                           {
                                                                               LotData_ID = item.ID_Lot,
                                                                               commite_No = lr.Ex_RequestCommittee.CommitteeType_ID,

                                                                               Status_Name = "",

                                                                               IS_Status = 0,
                                                                               IS_Status_Committee = 0,

                                                                               Is_Continue = 0,

                                                                           }).ToList();
                                    item.list_Lot_Result_Status = Lot_Result_Ex_CommitteeResult55;
                                }


                            }
                        }
                    }
                    CheckRequestDetails.itemsWithConstrains = itemss;
                    // }
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, CheckRequestDetails);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop_ShiftTiming(List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ShiftTiming>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.IsActive)
                .Select(c => new CustomOption
                {
                    //change display lang
                    DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                    Value = c.ID
                }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }
        public Dictionary<string, object> GetTimingMony(byte shiftId, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<ShiftTiming>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.ID == shiftId).FirstOrDefault().count;

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
        }

        public Dictionary<string, object> Insert_Committee(EX_RequestCommitteeDTO entity, List<string> Device_Info)
        {

            try
            {
                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        bool? _IsApproved = null;

                        if (entity.CommitteeType_ID == 2)//فى حالة الجشني
                            _IsApproved = true;
                        var operationType = 73; //ask
                        long Committe_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_RequestCommittee_seq");
                        if (true)
                        {

                        }

                        var RequestCommittee = new Ex_RequestCommittee
                        {

                            ID = Committe_ID,

                            ExCheckRequest_ID = entity.EX_CheckRequest_ID,
                            CommitteeType_ID = entity.CommitteeType_ID,
                            ExCommitteeCheckLocation_ID = entity.EX_CommitteeCheckLocation_ID,
                            Delegation_Date = entity.Delegation_Date,
                            StartTime = entity.StartTime,
                            EndTime = entity.EndTime,
                            IsFinishedAll = entity.IsFinishedAll,
                            //make approval is true if  geshny //(lang == "1" ? c.Name_Ar : c.Name_Ar),
                            IsApproved = _IsApproved,//?true:null,
                            Status = entity.Status,
                            User_Creation_Id = entity.User_Creation_Id,
                            User_Creation_Date = entity.User_Creation_Date,
                            //IsAgreeResult = entity.IsAgreeResult,
                        };
                        context.Ex_RequestCommittee.Add(RequestCommittee);
                        context.SaveChanges();

                        #region Employee              
                        if (entity.com_emp.Count > 0)
                        {
                            foreach (var item in entity.com_emp)
                            {
                                long _Employee_Id = long.Parse(item.Employee_Id.ToString());

                                var Comm_Employee = new CommitteeEmployee
                                {
                                    Committee_ID = Committe_ID,
                                    Employee_Id = _Employee_Id,
                                    ISAdmin = item.ISAdmin,
                                    OperationType = operationType,

                                    User_Creation_Id = entity.User_Creation_Id,
                                    User_Creation_Date = entity.User_Creation_Date,

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
                        if (entity.List_Committee_Shift != null)
                        {
                            if (entity.List_Committee_Shift.Count > 0)
                            {
                                foreach (var item in entity.List_Committee_Shift)
                                {
                                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_RequestCommittee_Shift_seq");
                                    var _Amount = decimal.Parse(item.Amount.ToString());
                                    var Committee_Shift = new Ex_RequestCommittee_Shift
                                    {
                                        ID = id,
                                        Ex_RequestCommittee_ID = Committe_ID,
                                        ShiftTiming_ID = item.ShiftTiming_ID,
                                        Count = item.Count,
                                        Amount = _Amount,
                                        User_Creation_Id = entity.User_Creation_Id,
                                        User_Creation_Date = entity.User_Creation_Date,
                                    };
                                    context.Ex_RequestCommittee_Shift.Add(Committee_Shift);
                                    context.SaveChanges();

                                }
                            }
                        }
                        #endregion

                        #region Fees Eng

                        if (entity.List_Ex_Request_Fees_Eng != null)
                        {
                            if (entity.List_Ex_Request_Fees_Eng.Count > 0)
                            {
                                foreach (var item_Fees in entity.List_Ex_Request_Fees_Eng)
                                {
                                    var id_Eng = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_RequestCommittee_Fees_ENG_seq");
                                    var _Value = decimal.Parse(item_Fees.Value.ToString());
                                    var Committee_Request_Fees_ENG = new Ex_RequestCommittee_Fees_ENG
                                    {
                                        ID = id_Eng,
                                        Ex_RequestCommittee_ID = Committe_ID,
                                        Ex_Fees_Type_ID = item_Fees.Ex_Fees_Type_ID,
                                        //Count = item_Fees.Count,
                                        Num_Eng = item_Fees.Num_Eng,
                                        Value = _Value,
                                        User_Creation_Id = entity.User_Creation_Id,
                                        User_Creation_Date = entity.User_Creation_Date,
                                    };
                                    context.Ex_RequestCommittee_Fees_ENG.Add(Committee_Request_Fees_ENG);
                                    context.SaveChanges();
                                }
                            }
                        }
                        #endregion
                        var Count_Sampel = 0;
                        var Count_TreatmentData = 0;
                        #region بيانات الرسالة
                        // الفحص
                        if (entity.List_CommitteeResult != null && entity.List_CommitteeResult.Count > 0)
                        {
                            foreach (var item in entity.List_CommitteeResult)
                            {
                                long CommitteResult_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CommitteeResult_SEQ");
                                var CommitteeResult = new Ex_CommitteeResult
                                {
                                    ID = CommitteResult_ID,
                                    Committee_ID = Committe_ID,
                                    Ex_Request_Item_Id = item.EX_Request_Item_Id,
                                    LotData_ID = item.LotData_ID,
                                    EmployeeId = item.EmployeeId,
                                    CommitteeResultType_ID = item.CommitteeResultType_ID,
                                    Date = item.Date,
                                    //IsAdminResult = item.IsAdminResult,
                                    //AdminFinalResult_Note = item.AdminFinalResult_Note,
                                    //QuantitySize = item.QuantitySize,
                                    //Weight = item.Weight,
                                    //Notes = item.Notes,
                                    IS_Total = item.IS_Total,
                                    Item_ShortName_ID = item.Item_ShortName_ID,
                                    User_Creation_Id = item.User_Creation_Id,
                                    User_Creation_Date = item.User_Creation_Date,

                                };
                                context.Ex_CommitteeResult.Add(CommitteeResult);
                                context.SaveChanges();



                            }
                        }
                        // السحب
                        if (entity.List_SampleData != null && entity.List_SampleData.Count > 0)
                        {
                            long insert_First_Row = 0;

                            decimal? Amount;
                            int? Count_Sample;




                            foreach (var Item_Analysis in entity.List_SampleData)
                            {

                                int Countnu = 0;
                                var AnalysisTypeID = context.AnalysisLabTypes.Where(a => a.ID == Item_Analysis.AnalysisLabType_ID).FirstOrDefault(); //  Item_Analysis.AnalysisLabType_ID
                                #region Check_Comity_Lot
                                var Check_AnalysisLabType_ID = context.Ex_CheckRequest_SampleData
                                    .Where(a => a.LotData_ID == Item_Analysis.LotData_ID
                              && a.AnalysisLabType.AnalysisTypeID == AnalysisTypeID.AnalysisTypeID).ToList();
                                if (Check_AnalysisLabType_ID.Count() == 0)
                                {
                                    Countnu = 1;
                                }
                                else
                                {
                                    if (Check_AnalysisLabType_ID.LastOrDefault().SampleSize == null)
                                    {
                                        if (Check_AnalysisLabType_ID.LastOrDefault().Ex_RequestCommittee.IsApproved == true)
                                        {
                                            if (Check_AnalysisLabType_ID.LastOrDefault().Ex_RequestCommittee.Delegation_Date < DateTime.Now.Date) // يقوم بتشكيل اللجنة
                                            {
                                                Countnu = 1;
                                            }
                                            else
                                            {
                                                Countnu = 0;
                                            }
                                        }
                                        else
                                        {
                                            Countnu = 1;
                                        }
                                    }
                                    else
                                    {
                                        Countnu = 0;
                                    }
                                }
                                var Check_ID = context.Ex_CheckRequest_Items_Lot_Result
                                   .Where(a => a.Ex_CheckRequest_Items_Lot_Category_ID == Item_Analysis.LotData_ID
                             && a.IS_Status == 9).FirstOrDefault();
                                if (Check_ID != null)
                                {

                                    if (Check_ID.IS_Status == 9)
                                    {
                                        Countnu = 1;
                                    }
                                }
                                #endregion 
                                if (Countnu == 1)
                                {
                                    Count_Sampel += 1;
                                    long EX_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");
                                    if (insert_First_Row == 0)
                                        insert_First_Row = EX_CheckRequest_SampleData_ID;
                                    if (insert_First_Row == EX_CheckRequest_SampleData_ID)
                                    {
                                        Amount = Item_Analysis.Amount;
                                        Count_Sample = Item_Analysis.Count_Sample;
                                    }
                                    else
                                    {
                                        Amount = null;
                                        Count_Sample = null;
                                    }
                                    var CheckRequest_SampleData = new Ex_CheckRequest_SampleData
                                    {
                                        ID = EX_CheckRequest_SampleData_ID,
                                        AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                        Ex_RequestCommittee_ID = Committe_ID,
                                        Ex_Request_Item_Id = Item_Analysis.EX_Request_Item_Id,
                                        LotData_ID = Item_Analysis.LotData_ID,
                                        Amount = Amount,
                                        Count_Sample = Count_Sample,
                                        User_Creation_Id = Item_Analysis.User_Creation_Id,
                                        User_Creation_Date = Item_Analysis.User_Creation_Date,
                                        IsPrint = Item_Analysis.IsPrint,
                                        IS_Total = Item_Analysis.IS_Total,
                                        Item_ShortName_ID = Item_Analysis.Item_ShortName_ID,
                                        Sample_BarCode = Item_Analysis.Sample_BarCode,
                                    };
                                    context.Ex_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                    context.SaveChanges();
                                }

                                //var sm = context.Ex_CheckRequest_SampleData.Where(a => a.LotData_ID == Item_Analysis.LotData_ID
                                //&& a.AnalysisLabType_ID == Item_Analysis.AnalysisLabType_ID).ToList();
                                //if (sm.Count == 0)
                                //{
                                //    //if (Item_Analysis.IS_Total == false) //جزئي
                                //    //{

                                //    //    string barcode = "";
                                //    //    //Thread.Sleep(500); // Sleep for 3 seconds
                                //    //    Random rd = new Random();
                                //    //    string rand = rd.Next(0, 100000).ToString("D5");
                                //    //    // save barcode
                                //    //    var dayofyear = "000" + DateTime.Now.DayOfYear;
                                //    //    var zx = DateTime.Now.Year.ToString().Substring(2);
                                //    //    var hour = (DateTime.Now.Hour).ToString("D" + 2);
                                //    //    var min = (DateTime.Now.Minute).ToString("D" + 2);
                                //    //    var sec = (DateTime.Now.Second).ToString("D" + 2);
                                //    //    barcode = "73" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
                                //    //}
                                //    Count_Sampel += 1;
                                //    long EX_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");
                                //    if(insert_First_Row==0)
                                //    insert_First_Row = EX_CheckRequest_SampleData_ID;
                                //    if (insert_First_Row == EX_CheckRequest_SampleData_ID)
                                //    {
                                //        Amount = Item_Analysis.Amount;
                                //        Count_Sample = Item_Analysis.Count_Sample;
                                //    }
                                //    else
                                //    {
                                //        Amount = null;
                                //        Count_Sample = null;
                                //    }
                                //    var CheckRequest_SampleData = new Ex_CheckRequest_SampleData
                                //    {
                                //        ID = EX_CheckRequest_SampleData_ID,
                                //        AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                //        Ex_RequestCommittee_ID = Committe_ID,
                                //        Ex_Request_Item_Id = Item_Analysis.EX_Request_Item_Id,
                                //        LotData_ID = Item_Analysis.LotData_ID,
                                //        Amount = Amount,
                                //        Count_Sample = Count_Sample,
                                //        User_Creation_Id = Item_Analysis.User_Creation_Id,
                                //        User_Creation_Date = Item_Analysis.User_Creation_Date,                                        
                                //        IsPrint = Item_Analysis.IsPrint,
                                //        IS_Total = Item_Analysis.IS_Total,
                                //        Item_ShortName_ID = Item_Analysis.Item_ShortName_ID,                                     
                                //        Sample_BarCode= Item_Analysis.Sample_BarCode,
                                //    };
                                //    context.Ex_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                //    context.SaveChanges();

                                //}
                                //else
                                //{
                                //    //var check_Sample_BarCode = sm.Where(a => a.Sample_BarCode != null).ToList();
                                //    //if (check_Sample_BarCode.Count == 0)
                                //    //{
                                //        // var Check_date = check_Sample_BarCode.Select(a => a.Im_RequestCommittee.Delegation_Date).LastOrDefault();
                                //        if (sm.LastOrDefault().Ex_RequestCommittee.Delegation_Date < DateTime.Now.Date) // يقوم بتشكيل اللجنة
                                //        {
                                //            Count_Sampel += 1;
                                //            long EX_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");

                                //            var CheckRequest_SampleData = new Ex_CheckRequest_SampleData
                                //            {
                                //                ID = EX_CheckRequest_SampleData_ID,
                                //                AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                //                Ex_RequestCommittee_ID = Committe_ID,
                                //                Ex_Request_Item_Id = Item_Analysis.EX_Request_Item_Id,
                                //                LotData_ID = Item_Analysis.LotData_ID,                                              
                                //                User_Creation_Id = Item_Analysis.User_Creation_Id,
                                //                User_Creation_Date = Item_Analysis.User_Creation_Date,                                              
                                //                IsPrint = Item_Analysis.IsPrint,
                                //                IS_Total = Item_Analysis.IS_Total,
                                //                Item_ShortName_ID = Item_Analysis.Item_ShortName_ID,
                                //                //Amount = context.Fees_Action.Where(a => a.ID == 9).Select(x => x.Amount).FirstOrDefault(),
                                //                Sample_BarCode = Item_Analysis.Sample_BarCode,
                                //            };
                                //            context.Ex_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                //            context.SaveChanges();
                                //        }
                                //        else if (sm.LastOrDefault().Ex_RequestCommittee.Delegation_Date == DateTime.Now.Date) // يقوم بتشكيل اللجنة
                                //        {
                                //            var Check_date_Now = sm.Select(a => a.Ex_RequestCommittee.Status).LastOrDefault();
                                //            if (Check_date_Now == true)
                                //            {
                                //                Count_Sampel += 1;
                                //                long EX_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");

                                //                var CheckRequest_SampleData = new Ex_CheckRequest_SampleData
                                //                {
                                //                    ID = EX_CheckRequest_SampleData_ID,
                                //                    AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                //                    Ex_RequestCommittee_ID = Committe_ID,
                                //                    Ex_Request_Item_Id = Item_Analysis.EX_Request_Item_Id,
                                //                    LotData_ID = Item_Analysis.LotData_ID,                                                  
                                //                    User_Creation_Id = Item_Analysis.User_Creation_Id,
                                //                    User_Creation_Date = Item_Analysis.User_Creation_Date,                                                
                                //                    IsPrint = Item_Analysis.IsPrint,
                                //                    IS_Total = Item_Analysis.IS_Total,
                                //                    Item_ShortName_ID = Item_Analysis.Item_ShortName_ID,
                                //                    Amount = context.Fees_Action.Where(a => a.ID == 9).Select(x => x.Amount).FirstOrDefault(),
                                //                };
                                //                context.Ex_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                //                context.SaveChanges();
                                //            }
                                //        }
                                //    //}
                                //    //else //  لا يوجد لجنة تم العمل عليه لانه له باركود
                                //    //{
                                //    //}
                                //}                             
                            }
                        }
                        // المعالجة
                        if (entity.CommitteeType_ID == 6)
                        {
                            if (entity.List_TreatmentMethod != null && entity.List_TreatmentMethod.Count > 0)
                            {
                                foreach (var Item_TreatmentMethod in entity.List_TreatmentMethod)
                                {

                                    int complete_TreatmentData = 1;
                                    var sum_TreatmentData = context.Ex_Request_TreatmentData
                                        .Where(a => a.Ex_Request_LotData_ID == Item_TreatmentMethod.EX_Request_LotData_ID
                                     && a.TreatmentType_ID == Item_TreatmentMethod.TreatmentType_ID
                                     && a.TreatmentMethod_ID == Item_TreatmentMethod.TreatmentMethod_ID
                                     ).Select(g => new
                                     {
                                         ID_TreatmentData = g.ID,
                                         ID_Committee = g.Ex_RequestCommittee.ID,
                                         Company_ID = g.Company_ID,//الشركة من الويب
                                         _Delegation_Date = g.Ex_RequestCommittee.Delegation_Date,//تاريخ اللجنة
                                         _Status = g.Ex_RequestCommittee.Status,//موقف الاندريد
                                         _IsFinishedAll = g.Ex_RequestCommittee.IsFinishedAll,//موقف الاندريد
                                         _Is_Start_Android = g.Ex_RequestCommittee.Is_Start_Android,// تعذر عمل اللجنة
                                         _IsApproved = g.Ex_RequestCommittee.IsApproved,//   العميل رفض اللجنة

                                     }).OrderBy(x => x.ID_Committee).ToList();
                                    //Eslam
                                    var _Net_Weight = context.Ex_CheckRequest_Items_Lot_Category.Where(a => a.ID == Item_TreatmentMethod.EX_Request_LotData_ID).FirstOrDefault().GrossWeight;
                                    if (sum_TreatmentData.Count() > 0)
                                    {
                                        if (sum_TreatmentData.LastOrDefault()._IsApproved == false)  //العميل رفض اللجنة
                                        {
                                            complete_TreatmentData = 0;
                                        }
                                        if (sum_TreatmentData.LastOrDefault()._Is_Start_Android == true)  // تعذ الفحص 
                                        {
                                            complete_TreatmentData = 0;
                                        }
                                        if (sum_TreatmentData.LastOrDefault()._Delegation_Date.Value.Date < DateTime.Now.Date)  // الوقت انتهي ولم يتم العمل  
                                        {
                                            complete_TreatmentData = 0;
                                        }
                                    }
                                    else
                                    {
                                        complete_TreatmentData = 0;
                                    }
                                    var Check_ID2 = context.Ex_CheckRequest_Items_Lot_Result
                                   .Where(a => a.Ex_CheckRequest_Items_Lot_Category_ID == Item_TreatmentMethod.EX_Request_LotData_ID
                             && a.IS_Status == 8).FirstOrDefault();
                                    if (Check_ID2 != null)
                                    {

                                        if (Check_ID2.IS_Status == 8)
                                        {
                                            complete_TreatmentData = 0;
                                        }
                                    }

                                    if (complete_TreatmentData == 0)
                                    {
                                        Count_TreatmentData += 1;
                                        long EX_Request_TreatmentData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("EX_Request_TreatmentData_SEQ");
                                        //Eslam حساب رسوم المعالجة
                                        // حساب كل 100 كيلو بنص جنيه يبقي حساب الطن ب 5 جنيه والحد الادني للرسوم 30 جنيه


                                        decimal? _amount = _Net_Weight * 0.005M;
                                        if (_amount < 30)
                                        {
                                            _amount = 30;

                                        }
                                        else
                                        {
                                            _amount = _Net_Weight * 0.005M;
                                        }


                                        var Request_TreatmentData = new Ex_Request_TreatmentData
                                        {
                                            ID = EX_Request_TreatmentData_ID,
                                            Ex_RequestCommittee_ID = Committe_ID,
                                            Ex_Request_Item_Id = Item_TreatmentMethod.EX_Request_Item_Id,
                                            Item_ShortName_ID = Item_TreatmentMethod.Item_ShortName_ID,

                                            Ex_Request_LotData_ID = Item_TreatmentMethod.EX_Request_LotData_ID,
                                            TreatmentType_ID = Item_TreatmentMethod.TreatmentType_ID,
                                            TreatmentMethod_ID = Item_TreatmentMethod.TreatmentMethod_ID,
                                            IS_Total = Item_TreatmentMethod.IS_Total,
                                            User_Creation_Id = Item_TreatmentMethod.User_Creation_Id,
                                            User_Creation_Date = Item_TreatmentMethod.User_Creation_Date,
                                            Procedures = Item_TreatmentMethod.Procedures,
                                            //  Amount = context.Fees_Action.Where(a => a.ID == 1).Select(x => x.Amount).FirstOrDefault(),
                                            Amount = _amount,
                                        };
                                        context.Ex_Request_TreatmentData.Add(Request_TreatmentData);
                                        context.SaveChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                        int Log_Check = 0;
                        if (entity.CommitteeType_ID == 3 && Count_Sampel > 0)
                        {
                            Log_Check = 1;
                            trans.Commit();
                        }
                        else if (entity.CommitteeType_ID == 1 || entity.CommitteeType_ID == 2)
                        {
                            Log_Check = 1;
                            trans.Commit();
                        }
                        else if (entity.CommitteeType_ID == 6)
                        {
                            if (Count_TreatmentData > 0)
                            {
                                Log_Check = 1;
                                trans.Commit();
                            }

                        }
                        else
                        {
                            entity = null;
                        }
                        if (Log_Check == 1)
                        {
                            Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                            dto2.ID_Table_Action = 3;
                            dto2.ID_TableActionValue = Committe_ID;
                            dto2.User_Creation_Id = entity.User_Creation_Id;
                            dto2.User_Creation_Date = DateTime.Now;
                            dto2.NOTS = " تم عمل لجنة علي الطلب ";
                            dto2.User_Type_ID = 127;
                            dto2.Type_log_ID = 133;
                            Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                            x.save_CheckRequest_Log(dto2, Device_Info);
                        }
                        else
                        {
                            entity.message = "لم يتم التشكيل";
                        }
                    }
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, entity);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Save_Lot(int lotss, List<EX_CommitteeResultDTO> CheckedItemsList, List<string> Device_Info)
        {
            try
            {
                foreach (var item in CheckedItemsList)
                {
                    long CommitteResult_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CommitteeResult_SEQ");
                    item.ID = CommitteResult_ID;
                    var Co = Mapper.Map<EX_CommitteeResultDTO, Ex_CommitteeResult>(item);
                    uow.Repository<Ex_CommitteeResult>().InsertReturn(Co);
                    uow.SaveChanges();
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, CheckedItemsList);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Save_AnalysisList(int AnalysisList, List<EX_CheckRequest_SampleDataDTO> CheckedAnalysisList, List<string> Device_Info)
        {
            try
            {

                if (CheckedAnalysisList != null && CheckedAnalysisList.Count > 0)
                {
                    foreach (var Item_Analysis in CheckedAnalysisList)
                    {
                        long EX_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");
                        Item_Analysis.ID = EX_CheckRequest_SampleData_ID;
                        var Co = Mapper.Map<EX_CheckRequest_SampleDataDTO, Ex_CheckRequest_SampleData>(Item_Analysis);
                        uow.Repository<Ex_CheckRequest_SampleData>().InsertReturn(Co);
                        uow.SaveChanges();

                    }
                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, CheckedAnalysisList);


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetountryConstrainStatus(long requestId, List<string> Device_Info)
        {
            try

            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("Ex_CheckRequest_ID", SqlDbType.BigInt);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("Ex_CheckRequest_ID", requestId.ToString());


                var data = uow.Repository<Get_Ex_CountryConstrain_DTO>().CallStored("Get_Ex_CountryConstrain", paramters_Type,
                paramters_Data, Device_Info).ToList();
                //data = data.Max();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAllTreatmentDataForShortnameId(long EX_CheckRequest_ID, long ExportCountry_Id, long shortnameId, List<string> Device_Info)

        {
            try

            {
                AllTreatmentDataForShortnameIdDTO allTreatmentDataForShortnameIdDTO = new AllTreatmentDataForShortnameIdDTO();

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                // get category data
                var categoryIdsForItems = (from Ex_CheckRequest in entities.Ex_CheckRequest
                                           join Ex_CheckRequest_Data in entities.Ex_CheckRequest_Data on Ex_CheckRequest.ID equals Ex_CheckRequest_Data.Ex_CheckRequest_ID
                                           join Ex_CheckRequest_Items in entities.Ex_CheckRequest_Items on Ex_CheckRequest.ID equals Ex_CheckRequest_Items.Ex_CheckRequest_ID
                                           where Ex_CheckRequest.ID == EX_CheckRequest_ID
                                           select new
                                           {
                                               Ex_CheckRequest_Items.ItemCategory_ID
                                           }).ToList();




                List<TreatmentMethodsDTO> treatmentMethodsList12 = new List<TreatmentMethodsDTO>();
                treatmentMethodsList12 = (
                                   from Ex_CountryConstrain in entities.Ex_CountryConstrain
                                   join Ex_CountryConstrain_Treatment in entities.Ex_CountryConstrain_Treatment on Ex_CountryConstrain.ID equals Ex_CountryConstrain_Treatment.CountryConstrain_ID
                                   where Ex_CountryConstrain.Item_ShortName_id == shortnameId && Ex_CountryConstrain.Import_Country_ID == ExportCountry_Id && Ex_CountryConstrain.ItemCategories_ID == null
                                   select new TreatmentMethodsDTO
                                   {

                                       TreatmentMethod_ID = Ex_CountryConstrain_Treatment.TreatmentMethods_ID,

                                   }
                                   ).Distinct().ToList();

                List<TreatmentMethodsDTO> treatmentMethodsList2 = new List<TreatmentMethodsDTO>();
                var lookup = treatmentMethodsList12.Select(i => i.TreatmentMethod_ID + "-" + i.TreatmentMethod_ID).ToList();

                treatmentMethodsList2 = (
                                   from TreatmentMethods in entities.TreatmentMethods
                                   where lookup.Contains(TreatmentMethods.ID + "-" + TreatmentMethods.ID)
                                   select new TreatmentMethodsDTO
                                   {
                                       Ar_Name = TreatmentMethods.Ar_Name,
                                       TreatmentMethod_ID = TreatmentMethods.ID,
                                       TreatmentType_ID = (byte)TreatmentMethods.TreatmentType_ID

                                   }
                                   ).Distinct().ToList();




                List<TreatmentTypesDTO> treatmentTypesList = new List<TreatmentTypesDTO>();


                lookup = treatmentMethodsList2.Select(i => i.TreatmentType_ID + "-" + i.TreatmentType_ID).ToList();

                treatmentTypesList = (
                               from TreatmentTypes in entities.TreatmentTypes
                               where lookup.Contains(TreatmentTypes.ID + "-" + TreatmentTypes.ID)

                               select new TreatmentTypesDTO
                               {
                                   Ar_Name = TreatmentTypes.Ar_Name,
                                   TreatmentType_ID = (byte)TreatmentTypes.ID,
                                   TreatmentMainTypeId = TreatmentTypes.MainType_ID

                               }

                               ).Distinct().ToList();




                lookup = treatmentTypesList.Select(i => i.TreatmentMainTypeId + "-" + i.TreatmentMainTypeId).ToList();

                List<TreatmentMainTypesDTO> TreatmentTypeList = new List<TreatmentMainTypesDTO>();

                TreatmentTypeList = (
                               from TreatmentMainTypes in entities.TreatmentMainTypes
                               where lookup.Contains(TreatmentMainTypes.ID + "-" + TreatmentMainTypes.ID)

                               select new TreatmentMainTypesDTO
                               {
                                   Ar_Name = TreatmentMainTypes.Ar_Name,
                                   TreatmentMainType = TreatmentMainTypes.ID,

                               }
                               ).Distinct().ToList();




                //bp => bp.BlogId = blogId


                allTreatmentDataForShortnameIdDTO.TreatmentMethodsDTO = treatmentMethodsList2;
                allTreatmentDataForShortnameIdDTO.TreatmentTypesDTO = treatmentTypesList;
                allTreatmentDataForShortnameIdDTO.TreatmentMainType = TreatmentTypeList;
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, allTreatmentDataForShortnameIdDTO);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //noura
        public Dictionary<string, object> Get_Fees(string Ex_CheckRequest_Number, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var Fees_Dat = (from ch in entities.Ex_CheckRequest
                                where ch.CheckRequest_Number == Ex_CheckRequest_Number
                                select new Fees_ALL
                                {
                                    ID = ch.ID,
                                    Fees_CheckRequest = ch.Amount,
                                    IsPaid = ch.IsPaid,
                                    Is_Paid_Check = ch.IsPaid == true ? "تم الدفع" : "لم يتم الدفع "
                                }).FirstOrDefault();

                #region رسوم  النبات
                var item_Fees = (from im_i in entities.Ex_CheckRequest_Items
                                 join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
                                 where im_i.Ex_CheckRequest.CheckRequest_Number == Ex_CheckRequest_Number
                                 group im_i by new
                                 {
                                     im_i.Item_ShortName_ID,
                                     isn.ShortName_Ar,
                                     isn.ShortName_En,
                                     Item_ID = isn.Item.ID,
                                     ItemName_Ar = isn.Item.Name_Ar,
                                     ItemName_En = isn.Item.Name_En,
                                     qualitiveGroupName = isn.QualitativeGroup.Name_Ar,
                                     qualitiveGroupNameEn = isn.QualitativeGroup.Name_En,

                                 } into grp
                                 select new Fees_Item_ALL
                                 {

                                     ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,

                                     ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
                                     //  Fees = grp.Sum(q => q.Fees),
                                     GrossWeight = grp.Sum(q => q.GrossWeight) / 1000,
                                     Net_Weight = grp.Sum(q => q.Net_Weight),
                                 }).Distinct().ToList();

                item_Fees.FirstOrDefault().Fees = uow.Repository<Ex_CheckRequest_Fees>().GetData().Where(d => d.Ex_CheckRequest.CheckRequest_Number == Ex_CheckRequest_Number).FirstOrDefault().Total_Amount;
                Fees_Dat.Fees_Item_ALL = item_Fees;
                #endregion


                #region رسم  النوبتجية
                // 
                var Fees_Item_Shift = (from sh in entities.Ex_RequestCommittee_Shift

                                       where sh.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == Ex_CheckRequest_Number
                                       group sh by new
                                       {
                                           ID = sh.ID,
                                           Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,
                                           IsPaidCommittee = sh.Ex_RequestCommittee.IsPaid

                                       } into grp
                                       select new List_Shift
                                       {
                                           ID = grp.Key.ID,
                                           Shift_Name = grp.Key.Shift_Name,
                                           IsPaidCommittee = grp.Key.IsPaidCommittee,
                                           Shift_Count = grp.Sum(q => q.Count),
                                           Shift_Amount = grp.Sum(q => q.Amount),

                                           Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
                                       }).Distinct().ToList();
                Fees_Dat.List_Shift = Fees_Item_Shift;
                #endregion

                #region رسم  المهندسين
                // 
                var Fees_Engineers = (from sh in entities.Ex_RequestCommittee_Fees_ENG


                                      where sh.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == Ex_CheckRequest_Number
                                      group sh by new
                                      {
                                          ID = sh.ID,
                                          Shift_Name = lang == "1" ? sh.EX_Fees_Type.Name : sh.EX_Fees_Type.Name,
                                          IsPaidEngineers = sh.Ex_RequestCommittee.IsPaid,
                                          Num_Eng = sh.Num_Eng,
                                          Shift_Amount = sh.Value

                                      } into grp
                                      select new List_ShiftEngineers
                                      {
                                          ID = grp.Key.ID,
                                          Shift_Name = grp.Key.Shift_Name,
                                          IsPaidEngineers = grp.Key.IsPaidEngineers,
                                          Num_Eng = grp.Key.Num_Eng,
                                          Shift_Amount = grp.Key.Shift_Amount,
                                          Shift_Sum_All = grp.Key.Num_Eng * grp.Key.Shift_Amount,

                                      }).Distinct().ToList();



                Fees_Dat.List_ShiftEngineers = Fees_Engineers;
                #endregion



                #region   رسوم السحب




                var Fees_Sample = (from sm in entities.Ex_CheckRequest_SampleData

                                   where sm.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == Ex_CheckRequest_Number
                                   && sm.Ex_RequestCommittee.CommitteeType_ID == 3 && sm.Count_Sample != null && sm.Amount != null
                                   group sm by new
                                   {
                                       ID = sm.ID,
                                       Sample_BarCode = sm.Sample_BarCode,
                                       Is_Total = sm.IS_Total,
                                       Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
                                       Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,
                                       Is_Paid_Sample2 = sm.Ex_RequestCommittee.IsPaid,
                                       Sample_Count = sm.Count_Sample,
                                       Sample_Amount = sm.Amount

                                   } into grp
                                   select new List_Sample
                                   {
                                       ID = grp.Key.ID,
                                       Sample_BarCode = grp.Key.Sample_BarCode,
                                       Laboratory_Name = grp.Key.Laboratory_Name,
                                       Sample_Name = grp.Key.Sample_Name,
                                       Is_Paid_Sample2 = grp.Key.Is_Paid_Sample2,
                                       Is_Total = grp.Key.Is_Total == false ? "جزئي" : "كلي",
                                       Sample_Count = grp.Key.Sample_Count,
                                       Sample_Amount = grp.Key.Sample_Amount,
                                       Sample_Sum_All = (grp.Key.Sample_Count) * (grp.Key.Sample_Amount),
                                   }).Distinct().ToList();

                var Fees_Sample44 = Fees_Sample.GroupBy(a => a.Sample_BarCode).Select(a => a.First()).ToList();






                Fees_Dat.List_Sample = Fees_Sample44;



                #endregion

                #region رسوم المعالجة
                //جزئى
                var Fees_Treatment = (from rq in entities.Ex_RequestCommittee
                                      join td in entities.Ex_Request_TreatmentData on rq.ID equals td.Ex_RequestCommittee_ID
                                      where td.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == Ex_CheckRequest_Number
                                     && rq.CommitteeType_ID == 6 && rq.Status == true
                                      group td by new
                                      {
                                          ID = td.ID,
                                          TreatmentMethod_Name = (lang == "1" ? entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().Ar_Name :
                                                     entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().En_Name),
                                          TreatmentType_Name = (lang == "1" ? entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().Ar_Name :
                                                     entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().En_Name),
                                          TreatmentMat_Name = (lang == "1" ? entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_Ar :
                                                     entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_En),
                                          Treatment_Amount = td.Amount
                                      } into grp
                                      select new List_Treatment
                                      {
                                          ID = grp.Key.ID,
                                          TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                          TreatmentType_Name = grp.Key.TreatmentType_Name,
                                          TreatmentMat_Name = grp.Key.TreatmentMat_Name,
                                          Treatment_Amount = grp.Key.Treatment_Amount
                                      }).Distinct().ToList();

                if (Fees_Treatment.Count() > 0)
                {
                    foreach (var item in Fees_Treatment)
                    {
                        long ID_Item = long.Parse(item.ID.ToString());
                        var _Treatment = (from ftd in entities.Fees_Transactions_Detiles
                                          where ftd.TreatmentData_ID == ID_Item
                                          select new List_Treatment
                                          {
                                              //Is_Paid_Treatment = ftd.TreatmentData_ID > 0 ? "تم الدفع" : "لم يتم الدفع"
                                          }
                                   ).ToList();
                        if (_Treatment.Count > 0)
                        {
                            item.Is_Paid_Treatment = _Treatment.FirstOrDefault().Is_Paid_Treatment;
                        }
                        else
                        {
                            item.Is_Paid_Treatment = null;
                        }
                    }

                }

                Fees_Dat.List_Treatment = Fees_Treatment;

                #endregion


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, Fees_Dat);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> GetEX_AllTreatmentDataForShortnameId(long EX_CheckRequest_ID, long ExportCountry_Id, long shortnameId, List<string> Device_Info)

        {
            try

            {
                AllTreatmentDataForShortnameIdDTO allTreatmentDataForShortnameIdDTO = new AllTreatmentDataForShortnameIdDTO();

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                // get category data
                //var categoryIdsForItems = (from Ex_CheckRequest in entities.Ex_CheckRequest
                //                           join Ex_CheckRequest_Data in entities.Ex_CheckRequest_Data on Ex_CheckRequest.ID equals Ex_CheckRequest_Data.Ex_CheckRequest_ID
                //                           join Ex_CheckRequest_Items in entities.Ex_CheckRequest_Items on Ex_CheckRequest.ID equals Ex_CheckRequest_Items.Ex_CheckRequest_ID
                //                           where Ex_CheckRequest.ID == EX_CheckRequest_ID
                //                           select new
                //                           {
                //                               Ex_CheckRequest_Items.ItemCategory_ID
                //                           }).ToList();




                //List<TreatmentMethodsDTO> treatmentMethodsList12 = new List<TreatmentMethodsDTO>();
                //treatmentMethodsList12 = (
                //                   from Ex_CountryConstrain in entities.Ex_CountryConstrain
                //                   join Ex_CountryConstrain_Treatment in entities.Ex_CountryConstrain_Treatment on Ex_CountryConstrain.ID equals Ex_CountryConstrain_Treatment.CountryConstrain_ID
                //                   where Ex_CountryConstrain.Item_ShortName_id == shortnameId
                //                   && Ex_CountryConstrain.Import_Country_ID == ExportCountry_Id
                //                   //&& Ex_CountryConstrain.ItemCategories_ID == null
                //                   select new TreatmentMethodsDTO
                //                   {

                //                       TreatmentMethod_ID = Ex_CountryConstrain_Treatment.TreatmentMethods_ID,

                //                   }
                //                   ).Distinct().ToList();

                List<TreatmentMethodsDTO> treatmentMethodsList2 = new List<TreatmentMethodsDTO>();
                var lookup = entities.EX_Choose_Treatment.Where(a => a.Ex_CheckRequest_ID == EX_CheckRequest_ID && a.Item_ShortName_id == shortnameId)
                    .Select(i => i.TreatmentMethods_ID + "-" + i.TreatmentMethods_ID).ToList();

                treatmentMethodsList2 = (
                                   from TreatmentMethods in entities.TreatmentMethods
                                       //join ctr in entities.EX_Choose_Treatment on TreatmentMethods.ID equals ctr.TreatmentMethods_ID
                                   where lookup.Contains(TreatmentMethods.ID + "-" + TreatmentMethods.ID)
                                   select new TreatmentMethodsDTO
                                   {
                                       Ar_Name = TreatmentMethods.Ar_Name,
                                       TreatmentMethod_ID = TreatmentMethods.ID,
                                       TreatmentType_ID = (byte)TreatmentMethods.TreatmentType_ID,


                                   }
                                   ).Distinct().ToList();




                List<TreatmentTypesDTO> treatmentTypesList = new List<TreatmentTypesDTO>();


                lookup = treatmentMethodsList2.Select(i => i.TreatmentType_ID + "-" + i.TreatmentType_ID).ToList();

                treatmentTypesList = (
                               from TreatmentTypes in entities.TreatmentTypes
                               where lookup.Contains(TreatmentTypes.ID + "-" + TreatmentTypes.ID)

                               select new TreatmentTypesDTO
                               {
                                   Ar_Name = TreatmentTypes.Ar_Name,
                                   TreatmentType_ID = (byte)TreatmentTypes.ID,
                                   TreatmentMainTypeId = TreatmentTypes.MainType_ID

                               }

                               ).Distinct().ToList();




                lookup = treatmentTypesList.Select(i => i.TreatmentMainTypeId + "-" + i.TreatmentMainTypeId).ToList();

                List<TreatmentMainTypesDTO> TreatmentTypeList = new List<TreatmentMainTypesDTO>();

                TreatmentTypeList = (
                               from TreatmentMainTypes in entities.TreatmentMainTypes
                               where lookup.Contains(TreatmentMainTypes.ID + "-" + TreatmentMainTypes.ID)

                               select new TreatmentMainTypesDTO
                               {
                                   Ar_Name = TreatmentMainTypes.Ar_Name,
                                   TreatmentMainType = TreatmentMainTypes.ID,

                               }
                               ).Distinct().ToList();




                allTreatmentDataForShortnameIdDTO.TreatmentMethodsDTO = treatmentMethodsList2;
                allTreatmentDataForShortnameIdDTO.TreatmentTypesDTO = treatmentTypesList;
                allTreatmentDataForShortnameIdDTO.TreatmentMainType = TreatmentTypeList;
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, allTreatmentDataForShortnameIdDTO);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


    }
}