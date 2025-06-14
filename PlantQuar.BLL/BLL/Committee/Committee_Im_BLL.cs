using AutoMapper;
using PlantQuar.BLL.BLL.Farm.FarmRequest;
using PlantQuar.BLL.BLL.Log;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Import.Permissions;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static PlantQuar.DTO.DTO.Committee.Committee_ImDTO;

namespace PlantQuar.BLL.BLL.Committee
{
    public class Committee_Im_BLL
    {//asdf
        private UnitOfWork uow;

        public Committee_Im_BLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetCreationDateForRequest(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Im_CheckRequest>().GetData().FirstOrDefault(r => r.ID == id);
            if (req != null)
            {
                var dto = Mapper.Map<Im_CheckRequestDTO>(req);
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
            var req = uow.Repository<Im_RequestCommittee>().GetData().FirstOrDefault(r => r.ImCheckRequest_ID == id && r.Delegation_Date != null);
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

        public Dictionary<string, object> GetImCheckRequestDetails
        (string ImCheckRequest_Number, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var CheckRequestDetails = (from cc in entities.Im_CheckRequest
                                           where cc.CheckRequest_Number == ImCheckRequest_Number
                                           select new ImRequestDetails_NewDTO
                                           {
                                               Im_CheckRequest_ID = cc.ID,
                                               ImCheckRequest_Number = cc.CheckRequest_Number,
                                               OutLet_Name = cc.Outlet.Ar_Name,
                                               OutLet_ID = cc.Outlet.ID
                                           }).FirstOrDefault();


                if (CheckRequestDetails != null)
                {
                    //foreach (var item in CheckRequestDetails)
                    //{
                    //Items
                    var itemss = (from im_i in entities.Im_CheckRequest_Items
                                  join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
                                  where im_i.Item_Permission_Number == ImCheckRequest_Number
                                  group im_i by new
                                  {
                                      //Im_Initiator_ID = grp.Key.Im_Initiator_ID,
                                      im_i.Item_ShortName_ID,
                                      isn.ShortName_Ar,
                                      isn.ShortName_En,
                                      Item_ID = isn.Item.ID,
                                      ItemName_Ar = isn.Item.Name_Ar,
                                      ItemName_En = isn.Item.Name_En,
                                      qualitiveGroupName = isn.QualitativeGroup.Name_Ar,
                                      qualitiveGroupNameEn = isn.QualitativeGroup.Name_En,
                                      InitiatorCountry = im_i.Im_Initiator.Country.Ar_Name,
                                      InitiatorCountryEn = im_i.Im_Initiator.Country.En_Name,
                                      Im_Initiator_ID = im_i.Im_Initiator.ID,
                                  } into grp
                                  select new Items_checkReq_New
                                  {
                                      Im_Initiator_ID = grp.Key.Im_Initiator_ID,
                                      Item_ShortName_ID = grp.Key.Item_ShortName_ID,
                                      ItemShortNameAr = grp.Key.ShortName_Ar,
                                      ItemShortNameEn = grp.Key.ShortName_En,
                                      Item_ID = grp.Key.Item_ID,
                                      ItemName_Ar = grp.Key.ItemName_Ar,
                                      ItemName_En = grp.Key.ItemName_En,
                                      qualitiveGroupName = grp.Key.qualitiveGroupName,
                                      qualitiveGroupNameEn = grp.Key.qualitiveGroupNameEn,
                                      InitiatorCountry = grp.Key.InitiatorCountry,
                                      InitiatorCountryEn = grp.Key.InitiatorCountryEn,
                                      GrossWeight = grp.Sum(q => q.GrossWeight),
                                      Net_Weight = grp.Sum(q => q.Net_Weight),
                                  }).Distinct().ToList();
                    foreach (var itm in itemss)
                    {
                        var itemShortName = (from isn in entities.Item_ShortName

                                             where isn.ID == itm.Item_ShortName_ID
                                             select new Items_checkReq_New
                                             {
                                                 subPartName = (lang == "1" ? isn.SubPart.Name_Ar : isn.SubPart.Name_En)
                                             }
                                            ).FirstOrDefault();
                        var catAndLots = (from im_i in entities.Im_CheckRequest_Items
                                          join v in entities.Im_CheckRequest_Items_Lot_Category on im_i.ID equals v.Im_CheckRequest_Items_ID
                                          //join im_cr in entities.Im_CheckRequest_Items_Lot_Result on v.ID equals im_cr.Im_CheckRequest_Items_Lot_Category_ID into cr
                                          //from p in cr.DefaultIfEmpty()
                                          where im_i.Item_Permission_Number == ImCheckRequest_Number 
                                          && im_i.Item_ShortName_ID == itm.Item_ShortName_ID
                                          && im_i.Im_Initiator_ID == itm.Im_Initiator_ID
                                          select new categories_lots_New
                                          {
                                              ID_Lot = v.ID,
                                              categoryName = (lang == "1" ? v.ItemCategory.Name_Ar : v.ItemCategory.Name_En),
                                              Im_checkReqItems_ID = v.Im_CheckRequest_Items_ID,
                                              ItemCategory_ID = v.ItemCategory_ID,
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
                                              //Check_Lot_Old_ID = p.LotData_ID >0 ?  p.CommitteeResultType_ID ==null ?p.Date <DateTime.Now?0: p.CommitteeResultType_ID !=7? 1 :1 : 0:0,

                                              //Check_Lot_Old_ID = p.LotData_ID > 0 ? p.CommitteeResultType_ID > 0 ? (p.CommitteeResultType_ID != 7 ? 1:0):(p.Im_RequestCommittee.Delegation_Date >= Date_Check1 ? 1:0) : 0 ,


                                              //بيانات النبات
                                              ID_IM_Item = v.Im_CheckRequest_Items.ID,
                                              //باقى ابعت بيانات الحاوية
                                              ContainerNumber = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.ContainerNumber,
                                              //containers_ID = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_ID,
                                              //containers_type_ID = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_type_ID,
                                              containerName = entities.A_SystemCode.Where(c => c.Id == v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_ID).FirstOrDefault().ValueName,
                                              containerType = entities.A_SystemCode.Where(c => c.Id == v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_type_ID && c.SystemCodeTypeId == 28).FirstOrDefault().ValueName,

                                              ShipholdNumber = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.ShipholdNumber,
                                              NavigationalNumber = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.NavigationalNumber,

                                              Total_Weight = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.Total_Weight,

                                              //Im_CommitteeResult_ID = p.ID,
                                              //Delegation_Date = p.Im_RequestCommittee.Delegation_Date,
                                              //CommitteeResultType_ID = p.CommitteeResultType_ID,
                                              //Check_Lot_Old_Name = p.LotData_ID > 0 ? p.CommitteeResultType_ID > 0 ? "تم عمل الاندريد" : "تم التشكيل ولم يتم الاندريد" : "لم يتم العمل عليه",
                                              // نهاية بيانات الحاوية
                                              //NOURA
                                              Order_TextLot = v.Order_Text,
                                              RecordedOrNot = v.ItemCategory_ID == null ? "#####" : ((bool)v.ItemCategory.IsRegister ? "مسجل" : "غير مسجل"),
                                              ItemCategoryGroup = v.ItemCategory_ID == null ? "#####" : v.ItemCategory.ItemCategories_Group_ID == null ? "لا يوجد"
                                          : (lang == "1" ? v.ItemCategory.ItemCategories_Group.Name_Ar : v.ItemCategory.ItemCategories_Group.Name_En),
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
                            var _CommitteeResult = (from icr in entities.Im_CommitteeResult
                                      where icr.LotData_ID == item.ID_Lot
                                      group icr by new
                                      {
                                          //id= icr.ID,
                                          Delegation_Date = icr.Im_RequestCommittee.Delegation_Date,
                                          CommitteeResultType_ID = icr.CommitteeResultType_ID,
                                          Check_Lot_Old_Name = icr.LotData_ID > 0 ? icr.CommitteeResultType_ID > 0 ? "تم عمل الاندريد" : "تم التشكيل ولم يتم الاندريد" : "لم يتم العمل عليه",
                                      } into grp
                                      select new categories_lots_New
                                      {
                                          //id
                                          Im_CommitteeResult_ID = grp.Max(a => a.ID),
                                          Delegation_Date = grp.Key.Delegation_Date,
                                          CommitteeResultType_ID = grp.Key.CommitteeResultType_ID,

                                      }).ToList();

                            var maxValue = _CommitteeResult.Max(x => x.Im_CommitteeResult_ID);
                            if (_CommitteeResult != null)
                            {
                                item.Delegation_Date = _CommitteeResult.Where(a => a.Im_CommitteeResult_ID == maxValue).Select(a => a.Delegation_Date).FirstOrDefault();
                                item.Im_CommitteeResult_ID = _CommitteeResult.Where(a => a.Im_CommitteeResult_ID == maxValue).Select(a => a.Im_CommitteeResult_ID).FirstOrDefault();
                                item.CommitteeResultType_ID = _CommitteeResult.Where(a => a.Im_CommitteeResult_ID == maxValue).Select(a => a.CommitteeResultType_ID).FirstOrDefault();
                            }
                            //المعامل السابقة
                            // DateTime udd =Convert.ToDateTime( DateTime.Now.ToShortDateString());
                            var committee_Sample = (from cr in entities.Im_CheckRequest_SampleData
                                                    where cr.LotData_ID == item.ID_Lot
                                                    //&& cr.SampleSize != null
                                                    //&&cr.Im_RequestCommittee.Delegation_Date >= udd
                                                    select new Committee_Sample_Lot
                                                    {
                                                        LotData_ID = cr.LotData_ID,
                                                        AnalysisLabType_ID = cr.AnalysisLabType_ID,
                                                        Analysis_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisType.Name_Ar : cr.AnalysisLabType.AnalysisType.Name_En),
                                                        Lab_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisLab.Name_Ar : cr.AnalysisLabType.AnalysisLab.Name_En),
                                                    }).ToList();
                            item.list_Committee_Sample_Lot = committee_Sample;


                            //Noura
                            #region  المعالجات السابقة


                            var committee_Treatment = (from td in entities.Im_Request_TreatmentData
                                                       where td.Im_Request_LotData_ID == item.ID_Lot

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
                                                       select new Committee_Treatment_Lot
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

                            var Lot_Result_Status = (from lr in entities.Im_CheckRequest_Items_Lot_Result
                                                     join ls in entities.Im_CheckRequest_Lot_Result_Status on lr.IS_Status equals ls.ID

                                                     where lr.Im_CheckRequest_Items_Lot_Category_ID == item.ID_Lot
                                                  //   && lr.IS_Status_Committee==null
                                                    
                                                    select new Lot_Result_Status
                                                    {
                                                        LotData_ID = lr.Im_CheckRequest_Items_Lot_Category_ID,
                                                        commite_No = ls.CommitteeType_ID,

                                                        Status_Name = (lang == "1" ? ls.Name_AR : ls.Name_En),
                                                        Nots_Result_Status = lr.Nots,
                                                        IS_Status = lr.IS_Status,
                                                        IS_Status_Committee = (lr.IS_Status_Committee == null ? 1 : 0),

                                                        Is_Continue = (ls.Is_Continue == true ? 1 : 0),

                                                    }).ToList();
                            item.list_Lot_Result_Status = Lot_Result_Status;
                        }

                        // catAndLots= catAndLots.GroupBy(a =>a.ID_Lot).Max(a=>a.Im_CommitteeResult_ID)



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

        public Dictionary<string, object> Insert_Committee(RequestCommitteeDTO entity, List<string> Device_Info)
        {
       
            try
            {
                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        
                        var operationType = 74; //ask
                        long Committe_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_RequestCommittee_seq");

                        //entity.ID = Committe_ID;

                        //var Co = Mapper.Map<RequestCommitteeDTO, Im_RequestCommittee>(entity);
                        //uow.Repository<Im_RequestCommittee>().InsertReturn(Co);
                        //uow.SaveChanges();

                        var RequestCommittee = new Im_RequestCommittee
                        {

                            ID = Committe_ID,

                            ImCheckRequest_ID = entity.ImCheckRequest_ID,
                            CommitteeType_ID = entity.CommitteeType_ID,
                            ImCommitteeCheckLocation_ID = entity.ImCommitteeCheckLocation_ID,
                            Delegation_Date = entity.Delegation_Date,
                            StartTime = entity.StartTime,
                            EndTime = entity.EndTime,
                            IsFinishedAll = entity.IsFinishedAll,
                            IsApproved = entity.IsApproved,
                            Status = entity.Status,
                            User_Creation_Id = entity.User_Creation_Id,
                            User_Creation_Date = entity.User_Creation_Date,
                            //IsAgreeResult = entity.IsAgreeResult,
                        };
                        context.Im_RequestCommittee.Add(RequestCommittee);
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
                                    var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_RequestCommittee_Shift_seq");
                                    var Committee_Shift = new Im_RequestCommittee_Shift
                                    {
                                        ID = id,
                                        Im_RequestCommittee_ID = Committe_ID,
                                        ShiftTiming_ID = item.ShiftTiming_ID,
                                        Count = item.Count,
                                        Amount = decimal.Parse(item.money.ToString()),
                                        User_Creation_Id = entity.User_Creation_Id,
                                        User_Creation_Date = entity.User_Creation_Date,
                                    };
                                    context.Im_RequestCommittee_Shift.Add(Committee_Shift);
                                    context.SaveChanges();
                                    //var CModel = Mapper.Map<RequestCommittee_ShiftDTO, Im_RequestCommittee_Shift>(item);
                                    //    uow.Repository<Im_RequestCommittee_Shift>().InsertRecord(CModel);
                                    //    uow.SaveChanges();
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
                                long CommitteResult_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CommitteeResult_SEQ");
                                var CommitteeResult = new Im_CommitteeResult
                                {
                                    ID = CommitteResult_ID,
                                    Committee_ID = Committe_ID,
                                    Im_Request_Item_Id = item.Im_Request_Item_Id,
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
                                context.Im_CommitteeResult.Add(CommitteeResult);
                                context.SaveChanges();

                                //item.ID = CommitteResult_ID;
                                //item.Committee_ID = Committe_ID;
                                ////    item.im_RequestCommittee.ID = lotssss;
                                //var Co_R = Mapper.Map<CommitteeResultDTO, Im_CommitteeResult>(item);
                                //uow.Repository<Im_CommitteeResult>().InsertReturn(Co_R);
                                //uow.SaveChanges();


                            }
                        }
                        // السحب
                        if (entity.List_SampleData != null && entity.List_SampleData.Count > 0)
                        {
                            foreach (var Item_Analysis in entity.List_SampleData)
                            {
                                var sm = context.Im_CheckRequest_SampleData.Where(a => a.LotData_ID == Item_Analysis.LotData_ID
                                && a.AnalysisLabType_ID == Item_Analysis.AnalysisLabType_ID).ToList();


                                if (sm.Count == 0)
                                {
                                    Count_Sampel += 1;
                                    long Im_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CheckRequest_SampleData_SEQ");

                                    var CheckRequest_SampleData = new Im_CheckRequest_SampleData
                                    {
                                        ID = Im_CheckRequest_SampleData_ID,
                                        AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                        Im_RequestCommittee_ID = Committe_ID,
                                        Im_Request_Item_Id = Item_Analysis.Im_Request_Item_Id,
                                        LotData_ID = Item_Analysis.LotData_ID,
                                        //WithdrawDate = Item_Analysis.WithdrawDate,
                                        //Sample_BarCode = Item_Analysis.Sample_BarCode,
                                        //SampleSize = Item_Analysis.SampleSize,
                                        //SampleRatio = Item_Analysis.SampleRatio,
                                        //IsAccepted = Item_Analysis.IsAccepted,
                                        //Notes_Ar = Item_Analysis.Notes_Ar,
                                        //RejectReason_Ar = Item_Analysis.RejectReason_Ar,
                                        //RejectReason_En = Item_Analysis.RejectReason_En,
                                        //Notes_En = Item_Analysis.Notes_En,
                                        User_Creation_Id = Item_Analysis.User_Creation_Id,
                                        User_Creation_Date = Item_Analysis.User_Creation_Date,
                                        //Admin_Confirmation = Item_Analysis.Admin_Confirmation,
                                        //Admin_User = Item_Analysis.Admin_User,
                                        //Admin_Date = Item_Analysis.Admin_Date,
                                        IsPrint = Item_Analysis.IsPrint,
                                        IS_Total = Item_Analysis.IS_Total,
                                        Item_ShortName_ID = Item_Analysis.Item_ShortName_ID,
                                        Amount= context.Fees_Action.Where(a => a.ID==9).Select(x => x.Amount).FirstOrDefault(),
                                    };
                                    context.Im_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                    context.SaveChanges();

                                }
                                else
                                {
                                    var check_Sample_BarCode = sm.Where(a => a.Sample_BarCode != null).ToList();
                                    if (check_Sample_BarCode.Count == 0)
                                    {
                                        // var Check_date = check_Sample_BarCode.Select(a => a.Im_RequestCommittee.Delegation_Date).LastOrDefault();
                                        if (sm.LastOrDefault().Im_RequestCommittee.Delegation_Date < DateTime.Now.Date) // يقوم بتشكيل اللجنة
                                        {
                                            Count_Sampel += 1;
                                            long Im_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CheckRequest_SampleData_SEQ");

                                            var CheckRequest_SampleData = new Im_CheckRequest_SampleData
                                            {
                                                ID = Im_CheckRequest_SampleData_ID,
                                                AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                                Im_RequestCommittee_ID = Committe_ID,
                                                Im_Request_Item_Id = Item_Analysis.Im_Request_Item_Id,
                                                LotData_ID = Item_Analysis.LotData_ID,
                                                //WithdrawDate = Item_Analysis.WithdrawDate,
                                                //Sample_BarCode = Item_Analysis.Sample_BarCode,
                                                //SampleSize = Item_Analysis.SampleSize,
                                                //SampleRatio = Item_Analysis.SampleRatio,
                                                //IsAccepted = Item_Analysis.IsAccepted,
                                                //Notes_Ar = Item_Analysis.Notes_Ar,
                                                //RejectReason_Ar = Item_Analysis.RejectReason_Ar,
                                                //RejectReason_En = Item_Analysis.RejectReason_En,
                                                //Notes_En = Item_Analysis.Notes_En,
                                                User_Creation_Id = Item_Analysis.User_Creation_Id,
                                                User_Creation_Date = Item_Analysis.User_Creation_Date,
                                                //Admin_Confirmation = Item_Analysis.Admin_Confirmation,
                                                //Admin_User = Item_Analysis.Admin_User,
                                                //Admin_Date = Item_Analysis.Admin_Date,
                                                IsPrint = Item_Analysis.IsPrint,
                                                IS_Total = Item_Analysis.IS_Total,
                                                Item_ShortName_ID = Item_Analysis.Item_ShortName_ID,
                                                Amount = context.Fees_Action.Where(a => a.ID == 9).Select(x => x.Amount).FirstOrDefault(),
                                            };
                                            context.Im_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                            context.SaveChanges();
                                        }
                                        else if (sm.LastOrDefault().Im_RequestCommittee.Delegation_Date == DateTime.Now.Date) // يقوم بتشكيل اللجنة
                                        {
                                            var Check_date_Now = sm.Select(a => a.Im_RequestCommittee.Status).LastOrDefault();
                                            if (Check_date_Now == true)
                                            {
                                                Count_Sampel += 1;
                                                long Im_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CheckRequest_SampleData_SEQ");

                                                var CheckRequest_SampleData = new Im_CheckRequest_SampleData
                                                {
                                                    ID = Im_CheckRequest_SampleData_ID,
                                                    AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                                    Im_RequestCommittee_ID = Committe_ID,
                                                    Im_Request_Item_Id = Item_Analysis.Im_Request_Item_Id,
                                                    LotData_ID = Item_Analysis.LotData_ID,
                                                    //WithdrawDate = Item_Analysis.WithdrawDate,
                                                    //Sample_BarCode = Item_Analysis.Sample_BarCode,
                                                    //SampleSize = Item_Analysis.SampleSize,
                                                    //SampleRatio = Item_Analysis.SampleRatio,
                                                    //IsAccepted = Item_Analysis.IsAccepted,
                                                    //Notes_Ar = Item_Analysis.Notes_Ar,
                                                    //RejectReason_Ar = Item_Analysis.RejectReason_Ar,
                                                    //RejectReason_En = Item_Analysis.RejectReason_En,
                                                    //Notes_En = Item_Analysis.Notes_En,
                                                    User_Creation_Id = Item_Analysis.User_Creation_Id,
                                                    User_Creation_Date = Item_Analysis.User_Creation_Date,
                                                    //Admin_Confirmation = Item_Analysis.Admin_Confirmation,
                                                    //Admin_User = Item_Analysis.Admin_User,
                                                    //Admin_Date = Item_Analysis.Admin_Date,
                                                    IsPrint = Item_Analysis.IsPrint,
                                                    IS_Total = Item_Analysis.IS_Total,
                                                    Item_ShortName_ID = Item_Analysis.Item_ShortName_ID,
                                                    Amount = context.Fees_Action.Where(a => a.ID == 9).Select(x => x.Amount).FirstOrDefault(),
                                                };
                                                context.Im_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                                context.SaveChanges();
                                            }
                                        }
                                    }
                                    //else //  لا يوجد لجنة تم العمل عليه لانه له باركود
                                    //{
                                    //}
                                }
                                //Item_Analysis.ID = Im_CheckRequest_SampleData_ID;
                                //Item_Analysis.Im_RequestCommittee_ID = Committe_ID;
                                ////    item.im_RequestCommittee.ID = lotssss;
                                //var Co_SM = Mapper.Map<CheckRequest_SampleDataDTO, Im_CheckRequest_SampleData>(Item_Analysis);
                                //uow.Repository<Im_CheckRequest_SampleData>().InsertReturn(Co_SM);
                                //uow.SaveChanges();

                            }
                        }
                        // المعالجة
                        if (entity.CommitteeType_ID == 14)
                        {
                            if (entity.List_TreatmentMethod != null && entity.List_TreatmentMethod.Count > 0)
                            {
                                foreach (var Item_TreatmentMethod in entity.List_TreatmentMethod)
                                {
                                    int complete_TreatmentData = 1;
                                    var sum_TreatmentData = context.Im_Request_TreatmentData
                                        
                                        .Where(a => a.Im_Request_LotData_ID == Item_TreatmentMethod.Im_Request_LotData_ID
                                     && a.TreatmentType_ID == Item_TreatmentMethod.TreatmentType_ID
                                     && a.TreatmentMethod_ID == Item_TreatmentMethod.TreatmentMethod_ID   
                                     &&a.Im_RequestCommittee.User_Deletion_Id==null
                                     ).Select(g => new {
                                         ID_TreatmentData = g.ID,
                                         ID_Committee = g.Im_RequestCommittee.ID,
                                         Company_ID = g.Company_ID,//الشركة من الويب
                                         _Delegation_Date = g.Im_RequestCommittee.Delegation_Date,//تاريخ اللجنة
                                         _Status = g.Im_RequestCommittee.Status,//موقف الاندريد
                                         _IsFinishedAll = g.Im_RequestCommittee.IsFinishedAll,//موقف الاندريد
                                         _Is_Start_Android = g.Im_RequestCommittee.Is_Start_Android,// تعذر عمل اللجنة
                                         _IsApproved = g.Im_RequestCommittee.IsApproved,//   العميل رفض اللجنة
                                         Is_Cancel= g.Im_RequestCommittee.Is_Cancel,//   العميل رفض اللجنة

                                     }).OrderBy(x => x.ID_Committee).ToList();
                                   
                                    
                                    if (sum_TreatmentData.Count() > 0)
                                    {
                                        if (sum_TreatmentData.LastOrDefault()._IsApproved == false)
                                        {
                                            complete_TreatmentData = 0;
                                        }
                                        if (sum_TreatmentData.LastOrDefault()._Status == true && sum_TreatmentData.LastOrDefault().Is_Cancel == 128)
                                        {
                                            complete_TreatmentData = 0;
                                        }
                                        if (sum_TreatmentData.LastOrDefault()._Delegation_Date < DateTime.Now.Date
                                            && sum_TreatmentData.LastOrDefault()._Status == false
                                           && sum_TreatmentData.LastOrDefault()._IsApproved == true)
                                        {
                                            complete_TreatmentData = 0;
                                        }
                                        if (sum_TreatmentData.LastOrDefault()._Delegation_Date < DateTime.Now.Date
                                           && sum_TreatmentData.LastOrDefault()._Status == false
                                          && sum_TreatmentData.LastOrDefault()._IsApproved == null)
                                        {
                                            complete_TreatmentData = 0;
                                        }


                                        //if (sum_TreatmentData.LastOrDefault()._IsApproved == false)  //العميل رفض اللجنة
                                        //{
                                        //    complete_TreatmentData = 0;
                                        //}
                                        //if (sum_TreatmentData.LastOrDefault()._Is_Start_Android == true)  // تعذ الفحص 
                                        //{
                                        //    complete_TreatmentData = 1;
                                        //}
                                        //if (sum_TreatmentData.LastOrDefault()._Status == false&& sum_TreatmentData.LastOrDefault()._Delegation_Date >= DateTime.Now)  //التاريخ خلص 
                                        //{                                       
                                        //    complete_TreatmentData = 1;
                                        //}
                                        ////if (sum_TreatmentData.LastOrDefault()._IsFinishedAll != true
                                        ////    && sum_TreatmentData.LastOrDefault()._Delegation_Date < DateTime.Now)  // الوقت انتهي ولم يتم العمل  
                                        ////{
                                        ////    complete_TreatmentData = 0;
                                        ////}
                                    }
                                    else
                                    {
                                        complete_TreatmentData = 0;
                                    }
                                    if (complete_TreatmentData == 0)
                                    {
                                        Count_TreatmentData += 1;
                                        long Im_Request_TreatmentData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_Request_TreatmentData_SEQ");

                                        var Request_TreatmentData = new Im_Request_TreatmentData
                                        {
                                            ID = Im_Request_TreatmentData_ID,
                                            Im_RequestCommittee_ID = Committe_ID,
                                            Im_Request_Item_Id = Item_TreatmentMethod.Im_Request_Item_Id,
                                            Item_ShortName_ID = Item_TreatmentMethod.Item_ShortName_ID,

                                            Im_Request_LotData_ID = Item_TreatmentMethod.Im_Request_LotData_ID,
                                            TreatmentType_ID = Item_TreatmentMethod.TreatmentType_ID,
                                            TreatmentMethod_ID = Item_TreatmentMethod.TreatmentMethod_ID,
                                            IS_Total = Item_TreatmentMethod.IS_Total,
                                            User_Creation_Id = Item_TreatmentMethod.User_Creation_Id,
                                            User_Creation_Date = Item_TreatmentMethod.User_Creation_Date,
                                            Procedures = Item_TreatmentMethod.Procedures,
                                            Amount = context.Fees_Action.Where(a => a.ID == 11).Select(x => x.Amount).FirstOrDefault(),
                                        };
                                        context.Im_Request_TreatmentData.Add(Request_TreatmentData);
                                        context.SaveChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                        int Log_Check = 0;
                        if (entity.CommitteeType_ID == 13 && Count_Sampel > 0)
                        {
                            Log_Check = 1;
                            trans.Commit();
                        }
                        else if (entity.CommitteeType_ID == 11)
                        {
                            Log_Check = 1;
                            trans.Commit();
                        }
                        else if (entity.CommitteeType_ID == 14)
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
                            dto2.ID_Table_Action = 13;
                            dto2.Im_CheckRequest_ID = entity.ImCheckRequest_ID;
                            dto2.ID_TableActionValue = Committe_ID;
                            dto2.User_Creation_Id = entity.User_Creation_Id;
                            dto2.User_Creation_Date = DateTime.Now;
                            if (entity.CommitteeType_ID == 11) { dto2.NOTS = " تم عمل لجنة فحص علي الطلب "; }
                            else if (entity.CommitteeType_ID == 13) { dto2.NOTS = " تم عمل لجنة فحص وسحب علي الطلب "; }

                            else if (entity.CommitteeType_ID == 14) { dto2.NOTS = " تم عمل لجنةمعالجة علي الطلب "; }
                            else { dto2.NOTS = " تم عمل لجنة علي الطلب "; }
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

        public Dictionary<string, object> Save_Lot(int lotss, List<Im_CommitteeResultDTO> CheckedItemsList, List<string> Device_Info)
        {
            try
            {
                //var operationType = 90; //ask
                //var id = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CommitteeResult_SEQ");
                //entity.ID = id;
                //var Co = Mapper.Map<Im_CommitteeResultDTO, Im_CommitteeResult>(entity);
                //uow.Repository<Im_CommitteeResultDTO>().InsertReturn(Co);
                //uow.SaveChanges();
                foreach (var item in CheckedItemsList)
                {
                    long CommitteResult_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CommitteeResult_SEQ");
                    item.ID = CommitteResult_ID;
                    //    item.im_RequestCommittee.ID = lotssss;
                    var Co = Mapper.Map<Im_CommitteeResultDTO, Im_CommitteeResult>(item);
                    uow.Repository<Im_CommitteeResult>().InsertReturn(Co);
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
        public Dictionary<string, object> Save_AnalysisList(int AnalysisList, List<Im_CheckRequest_SampleDataDTO> CheckedAnalysisList, List<string> Device_Info)
        {
            try
            {

                if (CheckedAnalysisList != null && CheckedAnalysisList.Count > 0)
                {
                    foreach (var Item_Analysis in CheckedAnalysisList)
                    {
                        long Im_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CheckRequest_SampleData_SEQ");
                        Item_Analysis.ID = Im_CheckRequest_SampleData_ID;
                        //    item.im_RequestCommittee.ID = lotssss;
                        var Co = Mapper.Map<Im_CheckRequest_SampleDataDTO, Im_CheckRequest_SampleData>(Item_Analysis);
                        uow.Repository<Im_CheckRequest_SampleData>().InsertReturn(Co);
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

    }
}