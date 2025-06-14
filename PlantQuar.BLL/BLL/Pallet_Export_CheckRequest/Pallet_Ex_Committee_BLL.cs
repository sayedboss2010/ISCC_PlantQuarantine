using AutoMapper;
using PlantQuar.BLL.BLL.Log;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Pallet_Export_CheckRequest;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Pallet_Export_CheckRequest
{
    public class Pallet_Ex_Committee_BLL
    {
        private UnitOfWork uow;

        public Pallet_Ex_Committee_BLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetCreationDateForRequest(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Ex_CheckRequest>().GetData().FirstOrDefault(r => r.ID == id);
            if (req != null)
            {
                var dto = Mapper.Map<Pallet_EX_CheckRequest_Committee_DTO>(req);
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
                                           select new Pallet_Ex_Committee_DTO
                                           {
                                               EX_CheckRequest_ID = cc.ID,
                                               EX_CheckRequest_Number = cc.CheckRequest_Number,
                                               OutLet_Name = cc.Outlet.Ar_Name,
                                               OutLet_ID = cc.Outlet.ID,
                                               //  ExportCountry_Id=cc.Ex_CheckRequest_Data.FirstOrDefault().ExportCountry_Id
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
                                  select new Pallet_EX_Items_checkReq_New
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
                                             select new Pallet_EX_Items_checkReq_New
                                             {
                                                 subPartName = (lang == "1" ? isn.SubPart.Name_Ar : isn.SubPart.Name_En)
                                             }
                                            ).FirstOrDefault();
                        var catAndLots = (from EX_i in entities.Ex_CheckRequest_Items
                                          join v in entities.Ex_CheckRequest_Items_Lot_Category on EX_i.ID equals v.Ex_CheckRequest_Items_ID

                                          where EX_i.Ex_CheckRequest_ID == EX_CheckRequest_ID && EX_i.Item_ShortName_ID == itm.Item_ShortName_ID
                                          select new Pallet_EX_categories_lots_New
                                          {
                                              ID_Lot = v.ID,
                                              categoryName = (lang == "1" ? EX_i.ItemCategory.Name_Ar : EX_i.ItemCategory.Name_En),
                                              EX_checkReqItems_ID = v.Ex_CheckRequest_Items_ID,
                                              ItemCategory_ID = EX_i.ItemCategory_ID,
                                              Size = v.Size,
                                              Package_Count = v.Package_Count,
                                              Package_Weight = v.Package_Weight,
                                              Units_Number = v.Units_Number,
                                              packageTypeID = v.Package_Type_ID,
                                              GrossWeight = v.GrossWeight,
                                              packageMaterialID = v.Package_Material_ID,
                                              Lot_Number = v.Lot_Number,
                                              Grower_Number = v.Grower_Number,
                                              Waybill = v.Waybill,
                                              Number_Wooden_Package = v.Number_Wooden_Package,
                                              Net_Weight = v.Net_Weight,

                                              // عرض اللوطات اللى اتعملها فحص
                                              // 0 لم يتم تشكيل لجنة على اللوط
                                              // 1  تم تشكيل لجنة على اللوط ولم يعمل الاندريد
                                              // 2  تم تشكيل لجنة على اللوط وتم عمل الاندريد
                                      

                                              //بيانات النبات
                                              ID_EX_Item = v.Ex_CheckRequest_Items.ID,
                                              //باقى ابعت بيانات الحاوية
                                
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
                                                    select new Pallet_EX_categories_lots_New
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
                                                    select new Pallet_EX_Committee_Sample_Lot
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
                                                       select new Pallet_EX_Committee_Treatment_Lot
                                                       {
                                                           ID = grp.Key.ID,
                                                           TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                                           TreatmentType_Name = grp.Key.TreatmentType_Name,
                                                           TreatmentMat_Name = grp.Key.TreatmentMat_Name,
                                                           TreatmentMat_Amount = grp.Sum(c => c.TreatmentMat_Amount),
                                                       }).Distinct().ToList();

                            item.list_Committee_Treatment_Lot = committee_Treatment;

                            #endregion

                            //Lot_Result_Status حالات اللوط

                            var Lot_Result_Status = (from lr in entities.Ex_CheckRequest_Items_Lot_Result
                                                     join ls in entities.Ex_CheckRequest_Lot_Result_Status on lr.IS_Status equals ls.ID

                                                     where lr.Ex_CheckRequest_Items_Lot_Category_ID == item.ID_Lot
                                                     //   && lr.IS_Status_Committee==null

                                                     select new Pallet_EX_Lot_Result_Status
                                                     {
                                                         LotData_ID = lr.Ex_CheckRequest_Items_Lot_Category_ID,
                                                         commite_No = ls.CommitteeType_ID,

                                                         Status_Name = (lang == "1" ? ls.Name_AR : ls.Name_En),
                                                         Nots_Result_Status = lr.Nots,
                                                         IS_Status = lr.IS_Status,
                                                         IS_Status_Committee = (lr.IS_Status_Committee == null ? 1 : 0),

                                                         Is_Continue = (ls.Is_Continue == true ? 1 : 0),

                                                     }).ToList();
                            item.list_Lot_Result_Status = Lot_Result_Status;
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

        public Dictionary<string, object> Insert_Committee(Pallet_EX_RequestCommitteeDTO entity, List<string> Device_Info)
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
                        if (entity.List_Committee_Shift.Count > 0)
                        {
                            foreach (var item in entity.List_Committee_Shift)
                            {
                                var id = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_RequestCommittee_Shift_seq");
                                var Committee_Shift = new Ex_RequestCommittee_Shift
                                {
                                    ID = id,
                                    Ex_RequestCommittee_ID = Committe_ID,
                                    ShiftTiming_ID = item.ShiftTiming_ID,
                                    Count = item.Count,
                                    Amount = decimal.Parse(item.money.ToString()),
                                    User_Creation_Id = entity.User_Creation_Id,
                                    User_Creation_Date = entity.User_Creation_Date,
                                };
                                context.Ex_RequestCommittee_Shift.Add(Committee_Shift);
                                context.SaveChanges();

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
                            foreach (var Item_Analysis in entity.List_SampleData)
                            {
                                var sm = context.Ex_CheckRequest_SampleData.Where(a => a.LotData_ID == Item_Analysis.LotData_ID
                                && a.AnalysisLabType_ID == Item_Analysis.AnalysisLabType_ID).ToList();


                                if (sm.Count == 0)
                                {
                                    Count_Sampel += 1;
                                    long EX_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");

                                    var CheckRequest_SampleData = new Ex_CheckRequest_SampleData
                                    {
                                        ID = EX_CheckRequest_SampleData_ID,
                                        AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                        Ex_RequestCommittee_ID = Committe_ID,
                                        Ex_Request_Item_Id = Item_Analysis.EX_Request_Item_Id,
                                        LotData_ID = Item_Analysis.LotData_ID,
                                        User_Creation_Id = Item_Analysis.User_Creation_Id,
                                        User_Creation_Date = Item_Analysis.User_Creation_Date,
                                        IsPrint = Item_Analysis.IsPrint,
                                        IS_Total = Item_Analysis.IS_Total,
                                        Item_ShortName_ID = Item_Analysis.Item_ShortName_ID,
                                        Amount = context.Fees_Action.Where(a => a.ID == 9).Select(x => x.Amount).FirstOrDefault(),



                                        //noura barcode

                                        Sample_BarCode = Item_Analysis.Sample_BarCode,
                                    };
                                    context.Ex_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                    context.SaveChanges();

                                }
                                else
                                {
                                    var check_Sample_BarCode = sm.Where(a => a.Sample_BarCode != null).ToList();
                                    if (check_Sample_BarCode.Count == 0)
                                    {
                                        // var Check_date = check_Sample_BarCode.Select(a => a.Im_RequestCommittee.Delegation_Date).LastOrDefault();
                                        if (sm.LastOrDefault().Ex_RequestCommittee.Delegation_Date < DateTime.Now.Date) // يقوم بتشكيل اللجنة
                                        {
                                            Count_Sampel += 1;
                                            long EX_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");

                                            var CheckRequest_SampleData = new Ex_CheckRequest_SampleData
                                            {
                                                ID = EX_CheckRequest_SampleData_ID,
                                                AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                                Ex_RequestCommittee_ID = Committe_ID,
                                                Ex_Request_Item_Id = Item_Analysis.EX_Request_Item_Id,
                                                LotData_ID = Item_Analysis.LotData_ID,
                                                User_Creation_Id = Item_Analysis.User_Creation_Id,
                                                User_Creation_Date = Item_Analysis.User_Creation_Date,
                                                
                                                IsPrint = Item_Analysis.IsPrint,
                                                IS_Total = Item_Analysis.IS_Total,
                                                Item_ShortName_ID = Item_Analysis.Item_ShortName_ID,
                                                Amount = context.Fees_Action.Where(a => a.ID == 9).Select(x => x.Amount).FirstOrDefault(),
                                                Sample_BarCode = Item_Analysis.Sample_BarCode,
                                            };
                                            context.Ex_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                            context.SaveChanges();
                                        }
                                        else if (sm.LastOrDefault().Ex_RequestCommittee.Delegation_Date == DateTime.Now.Date) // يقوم بتشكيل اللجنة
                                        {
                                            var Check_date_Now = sm.Select(a => a.Ex_RequestCommittee.Status).LastOrDefault();
                                            if (Check_date_Now == true)
                                            {
                                                Count_Sampel += 1;
                                                long EX_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");

                                                var CheckRequest_SampleData = new Ex_CheckRequest_SampleData
                                                {
                                                    ID = EX_CheckRequest_SampleData_ID,
                                                    AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                                    Ex_RequestCommittee_ID = Committe_ID,
                                                    Ex_Request_Item_Id = Item_Analysis.EX_Request_Item_Id,
                                                    LotData_ID = Item_Analysis.LotData_ID,
                                                    
                                                    User_Creation_Id = Item_Analysis.User_Creation_Id,
                                                    User_Creation_Date = Item_Analysis.User_Creation_Date,
                                                  
                                                    IsPrint = Item_Analysis.IsPrint,
                                                    IS_Total = Item_Analysis.IS_Total,
                                                    Item_ShortName_ID = Item_Analysis.Item_ShortName_ID,
                                                    Amount = context.Fees_Action.Where(a => a.ID == 9).Select(x => x.Amount).FirstOrDefault(),
                                                };
                                                context.Ex_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                                context.SaveChanges();
                                            }
                                        }
                                    }
                                    //else //  لا يوجد لجنة تم العمل عليه لانه له باركود
                                    //{
                                    //}
                                }
                              

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
                                     ).Select(g => new {
                                         ID_TreatmentData = g.ID,
                                         ID_Committee = g.Ex_RequestCommittee.ID,
                                         Company_ID = g.Company_ID,//الشركة من الويب
                                         _Delegation_Date = g.Ex_RequestCommittee.Delegation_Date,//تاريخ اللجنة
                                         _Status = g.Ex_RequestCommittee.Status,//موقف الاندريد
                                         _IsFinishedAll = g.Ex_RequestCommittee.IsFinishedAll,//موقف الاندريد
                                         _Is_Start_Android = g.Ex_RequestCommittee.Is_Start_Android,// تعذر عمل اللجنة
                                         _IsApproved = g.Ex_RequestCommittee.IsApproved,//   العميل رفض اللجنة

                                     }).OrderBy(x => x.ID_Committee).ToList();
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
                                      
                                    }
                                    else
                                    {
                                        complete_TreatmentData = 0;
                                    }
                                    if (complete_TreatmentData == 0)
                                    {
                                        Count_TreatmentData += 1;
                                        long EX_Request_TreatmentData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("EX_Request_TreatmentData_SEQ");

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
                                            Amount = context.Fees_Action.Where(a => a.ID == 1).Select(x => x.Amount).FirstOrDefault(),
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

        public Dictionary<string, object> Save_Lot(int lotss, List<Pallet_EX_CommitteeResultDTO> CheckedItemsList, List<string> Device_Info)
        {
            try
            {
                foreach (var item in CheckedItemsList)
                {
                    long CommitteResult_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CommitteeResult_SEQ");
                    item.ID = CommitteResult_ID;
                    var Co = Mapper.Map<Pallet_EX_CommitteeResultDTO, Ex_CommitteeResult>(item);
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
        public Dictionary<string, object> Save_AnalysisList(int AnalysisList, List<Pallet_EX_CheckRequest_SampleDataDTO> CheckedAnalysisList, List<string> Device_Info)
        {
            try
            {

                if (CheckedAnalysisList != null && CheckedAnalysisList.Count > 0)
                {
                    foreach (var Item_Analysis in CheckedAnalysisList)
                    {
                        long EX_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");
                        Item_Analysis.ID = EX_CheckRequest_SampleData_ID;
                        var Co = Mapper.Map<Pallet_EX_CheckRequest_SampleDataDTO, Ex_CheckRequest_SampleData>(Item_Analysis);
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


                var data = uow.Repository<Pallet_Get_Ex_CountryConstrain_DTO>().CallStored("Get_Ex_CountryConstrain", paramters_Type,
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
                Pallet_AllTreatmentDataForShortnameIdDTO allTreatmentDataForShortnameIdDTO = new Pallet_AllTreatmentDataForShortnameIdDTO();

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




                List<Pallet_TreatmentMethodsDTO> treatmentMethodsList12 = new List<Pallet_TreatmentMethodsDTO>();
                treatmentMethodsList12 = (
                                   from Ex_CountryConstrain in entities.Ex_CountryConstrain
                                   join Ex_CountryConstrain_Treatment in entities.Ex_CountryConstrain_Treatment on Ex_CountryConstrain.ID equals Ex_CountryConstrain_Treatment.CountryConstrain_ID
                                   where Ex_CountryConstrain.Item_ShortName_id == shortnameId  && Ex_CountryConstrain.ItemCategories_ID == null
                                   select new Pallet_TreatmentMethodsDTO
                                   {

                                       TreatmentMethod_ID = Ex_CountryConstrain_Treatment.TreatmentMethods_ID,

                                   }
                                   ).Distinct().ToList();

                List<Pallet_TreatmentMethodsDTO> treatmentMethodsList2 = new List<Pallet_TreatmentMethodsDTO>();
                var lookup = treatmentMethodsList12.Select(i => i.TreatmentMethod_ID + "-" + i.TreatmentMethod_ID).ToList();

                treatmentMethodsList2 = (
                                   from TreatmentMethods in entities.TreatmentMethods
                                   where lookup.Contains(TreatmentMethods.ID + "-" + TreatmentMethods.ID)
                                   select new Pallet_TreatmentMethodsDTO
                                   {
                                       Ar_Name = TreatmentMethods.Ar_Name,
                                       TreatmentMethod_ID = TreatmentMethods.ID,
                                       TreatmentType_ID = (byte)TreatmentMethods.TreatmentType_ID

                                   }
                                   ).Distinct().ToList();




                List<Pallet_TreatmentTypesDTO> treatmentTypesList = new List<Pallet_TreatmentTypesDTO>();


                lookup = treatmentMethodsList2.Select(i => i.TreatmentType_ID + "-" + i.TreatmentType_ID).ToList();

                treatmentTypesList = (
                               from TreatmentTypes in entities.TreatmentTypes
                               where lookup.Contains(TreatmentTypes.ID + "-" + TreatmentTypes.ID)

                               select new Pallet_TreatmentTypesDTO
                               {
                                   Ar_Name = TreatmentTypes.Ar_Name,
                                   TreatmentType_ID = (byte)TreatmentTypes.ID,
                                   TreatmentMainTypeId = TreatmentTypes.MainType_ID

                               }

                               ).Distinct().ToList();




                lookup = treatmentTypesList.Select(i => i.TreatmentMainTypeId + "-" + i.TreatmentMainTypeId).ToList();

                List<Pallet_TreatmentMainTypesDTO> TreatmentTypeList = new List<Pallet_TreatmentMainTypesDTO>();

                TreatmentTypeList = (
                               from TreatmentMainTypes in entities.TreatmentMainTypes
                               where lookup.Contains(TreatmentMainTypes.ID + "-" + TreatmentMainTypes.ID)

                               select new Pallet_TreatmentMainTypesDTO
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
                                select new Pallet_Fees_ALL
                                {
                                    ID = ch.ID,
                                    Fees_CheckRequest = ch.Amount,
                                    Is_Paid_Check = ch.IsPaid == true ? "تم الدفع" : "لم يتم الدفع "
                                }).FirstOrDefault();

                #region رسوم  النبات
                var item_Fees = (from Ex_i in entities.Ex_CheckRequest_Items
                                 join isn in entities.Item_ShortName on Ex_i.Item_ShortName_ID equals isn.ID
                                 where Ex_i.Item_Permission_Number == Ex_CheckRequest_Number

                                 group Ex_i by new
                                 {
                                     id = Ex_i.ID,
                                     Ex_i.Item_ShortName_ID,
                                     isn.ShortName_Ar,
                                     isn.ShortName_En,
                                     Item_ID = isn.Item.ID,
                                     ItemName_Ar = isn.Item.Name_Ar,
                                     ItemName_En = isn.Item.Name_En,
                                     qualitiveGroupName = isn.QualitativeGroup.Name_Ar,
                                     qualitiveGroupNameEn = isn.QualitativeGroup.Name_En,
                                     //InitiatorCountry = Ex_i.Im_Initiator.Country.Ar_Name,
                                     //InitiatorCountryEn = Ex_i.Im_Initiator.Country.En_Name,
                                     //Is_Paid_Items = Ex_i.Im_CheckRequset_Shipping_Method.Im_CheckRequest.IsPaid == true ? "تم الدفع" : "لم يتم الدفع "
                                 } into grp
                                 select new Pallet_Fees_Item_ALL
                                 {
                                     ID = grp.Key.id,
                                     //Is_Paid_Items = grp.Key.Is_Paid_Items,
                                     ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,

                                     ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
                                     Fees = grp.Sum(q => q.Fees),
                                     GrossWeight = grp.Sum(q => q.GrossWeight) / 1000,
                                     Net_Weight = grp.Sum(q => q.Net_Weight),
                                 }).Distinct().ToList();


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

                                       } into grp
                                       select new Pallet_List_Shift
                                       {
                                           ID = grp.Key.ID,
                                           Shift_Name = grp.Key.Shift_Name,
                                           Shift_Count = grp.Sum(q => q.Count),
                                           Shift_Amount = grp.Sum(q => q.Amount),
                                           Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
                                       }).Distinct().ToList();
                foreach (var item in Fees_Item_Shift)
                {
                    long ID_Item = long.Parse(item.ID.ToString());
                    var _Shift = (from ftd in entities.Fees_Transactions_Detiles

                                  where ftd.Shift_ID == ID_Item
                                  && ftd.Fees_Transactions.TableName_ID == 4
                                  && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
                                  select new Pallet_List_Shift
                                  {
                                      Is_Paid_Shift = ftd.Shift_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
                                  }
                               ).ToList();
                    if (_Shift.Count > 0)
                        item.Is_Paid_Shift = _Shift.FirstOrDefault().Is_Paid_Shift;
                    else
                        item.Is_Paid_Shift = null;
                }

                Fees_Dat.List_Shift = Fees_Item_Shift;
                #endregion





                #region   رسوم السحب




                var Fees_Sample = (from sm in entities.Ex_CheckRequest_SampleData

                                   where sm.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == Ex_CheckRequest_Number
                                   group sm by new
                                   {

                                       ID = sm.ID,
                                       Sample_BarCode = sm.Sample_BarCode,
                                       Is_Total = sm.IS_Total,
                                       Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
                                       Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,


                                   } into grp
                                   select new Pallet_List_Sample
                                   {
                                       ID = grp.Key.ID,
                                       Sample_BarCode = grp.Key.Sample_BarCode,
                                       Laboratory_Name = grp.Key.Laboratory_Name,
                                       Sample_Name = grp.Key.Sample_Name,
                                       Is_Total = grp.Key.Is_Total == false ? "جزئي" : "كلي",
                                       Sample_Count = grp.Count(q => q.AnalysisLabType_ID != null),
                                       Sample_Amount = 200,
                                       Sample_Sum_All = grp.Count(q => q.AnalysisLabType_ID != null) * 200,
                                   }).Distinct().ToList();

                var Fees_Sample44 = Fees_Sample.GroupBy(a => a.Sample_BarCode).Select(a => a.First()).ToList();

                foreach (var item in Fees_Sample44)
                {
                    long ID_Item = long.Parse(item.ID.ToString());
                    var _Sample = (from ftd in entities.Fees_Transactions_Detiles

                                   where ftd.SampleData_ID == ID_Item
                                   && ftd.Fees_Transactions.TableName_ID == 4
                                   && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
                                   select new Pallet_List_Sample
                                   {
                                       Is_Paid_Sample = ftd.SampleData_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
                                   }
                               ).ToList();
                    if (_Sample.Count > 0)
                        item.Is_Paid_Sample = _Sample.FirstOrDefault().Is_Paid_Sample;
                    else
                        item.Is_Paid_Sample = null;
                }





                Fees_Dat.List_Sample = Fees_Sample44;



                #endregion

                #region رسوم المعالجة
                //جزئى
                var Fees_Treatment = (from td in entities.Ex_Request_TreatmentData
                                      where td.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == Ex_CheckRequest_Number
                                      && td.TreatmentMat_ID != null

                                      group td by new
                                      {
                                          Is_Total = td.IS_Total,
                                          ID = td.ID,
                                          TreatmentMethod_Name = (lang == "1" ? entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().Ar_Name :
                                                      entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().En_Name),
                                          TreatmentType_Name = (lang == "1" ? entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().Ar_Name :
                                                      entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().En_Name),
                                          TreatmentMat_Name = (lang == "1" ? entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_Ar :
                                                      entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_En),
                                          Is_Paid_Treatment = td.Ex_RequestCommittee.IsPaid == true ? "تم الدفع" : "لم يتم الدفع"

                                      } into grp
                                      select new Pallet_List_Treatment
                                      {
                                          ID = grp.Key.ID,
                                          Is_Paid_Treatment = grp.Key.Is_Paid_Treatment,
                                          TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                          TreatmentType_Name = grp.Key.TreatmentType_Name,
                                          //TreatmentMat_Amount = grp.Sum(c => c.TreatmentMat_Amount),
                                          Amount = grp.Sum(c => c.Amount),

                                      }).Distinct().ToList();



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
    }
}