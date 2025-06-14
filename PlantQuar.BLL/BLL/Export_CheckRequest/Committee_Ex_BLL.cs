using AutoMapper;

using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace PlantQuar.BLL.BLL.Export_CheckRequest
{
   public class Committee_Ex_BLL
    {//asdf
        private UnitOfWork uow;

        public Committee_Ex_BLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetCreationDateForRequest(long? id, List<string> Device_Info)
        {
            var req = uow.Repository<Ex_CheckRequest>().GetData().FirstOrDefault(r => r.ID == id);
            if (req != null)
            {
                var dto = Mapper.Map<EX_CheckRequestDTO>(req);
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

        public Dictionary<string, object> GetImCheckRequestDetails
        (string ImCheckRequest_Number, List<string> Device_Info)
        {
            try
            {

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var CheckRequestDetails = (from cc in entities.Ex_CheckRequest
                                           where cc.CheckRequest_Number == ImCheckRequest_Number
                                           select new ExRequestDetails_NewDTO
                                           {
                                               Ex_CheckRequest_ID = cc.ID,
                                               Ex_CheckRequest_Number = cc.CheckRequest_Number,
                                               OutLet_Name = cc.Outlet.Ar_Name,
                                               OutLet_ID = cc.Outlet.ID
                                           }).FirstOrDefault();


                if (CheckRequestDetails != null)
                {
                    //foreach (var item in CheckRequestDetails)
                    //{
                    //Items
                    var itemss = (from Ex_i in entities.Ex_CheckRequest_Items
                                  join isn in entities.Item_ShortName on Ex_i.Item_ShortName_ID equals isn.ID
                                  where Ex_i. Ex_CheckRequest_ID == CheckRequestDetails.Ex_CheckRequest_ID
                                  group Ex_i by new
                                  {
                                      Ex_i.Item_ShortName_ID,
                                      isn.ShortName_Ar,
                                      isn.ShortName_En,
                                      Item_ID = isn.Item.ID,
                                      ItemName_Ar = isn.Item.Name_Ar,
                                      ItemName_En = isn.Item.Name_En,
                                      qualitiveGroupName = isn.QualitativeGroup.Name_Ar,
                                      qualitiveGroupNameEn = isn.QualitativeGroup.Name_En,
                                     // InitiatorCountry = Ex_i.Ex_Initiator.Country.Ar_Name,
                                     // InitiatorCountryEn = Ex_i.Ex_Initiator.Country.En_Name,
                                  } into grp
                                  select new Ex_Items_checkReq_New
                                  {
                                      Item_ShortName_ID = grp.Key.Item_ShortName_ID,
                                      ItemShortNameAr = grp.Key.ShortName_Ar,
                                      ItemShortNameEn = grp.Key.ShortName_En,
                                      Item_ID = grp.Key.Item_ID,
                                      ItemName_Ar = grp.Key.ItemName_Ar,
                                      ItemName_En = grp.Key.ItemName_En,
                                      qualitiveGroupName = grp.Key.qualitiveGroupName,
                                      qualitiveGroupNameEn = grp.Key.qualitiveGroupNameEn,
                                     // InitiatorCountry = grp.Key.InitiatorCountry,
                                     // InitiatorCountryEn = grp.Key.InitiatorCountryEn,
                                      GrossWeight = grp.Sum(q => q.GrossWeight),
                                      Net_Weight = grp.Sum(q => q.Net_Weight),
                                  }).Distinct().ToList();
                    foreach (var itm in itemss)
                    {
                        var itemShortName = (from isn in entities.Item_ShortName

                                             where isn.ID == itm.Item_ShortName_ID
                                             select new Ex_Items_checkReq_New
                                             {
                                                 subPartName = (lang == "1" ? isn.SubPart.Name_Ar : isn.SubPart.Name_En)
                                             }
                                            ).FirstOrDefault();
                        var catAndLots = (from Ex_i in entities.Ex_CheckRequest_Items
                                          join v in entities.Ex_CheckRequest_Items_Lot_Category on Ex_i.ID equals v.Ex_CheckRequest_Items_ID
                                          //join Ex_cr in entities.Ex_CommitteeResult on v.ID equals Ex_cr.LotData_ID into cr
                                          //from p in cr.DefaultIfEmpty()
                                          where Ex_i.Ex_CheckRequest_ID == CheckRequestDetails.Ex_CheckRequest_ID
                                          && Ex_i.Item_ShortName_ID == itm.Item_ShortName_ID
                                          select new Ex_categories_lots_New
                                          {
                                              ID_Lot = v.ID,
                                             // categoryName = (lang == "1" ? v.ItemCategory.Name_Ar : v.ItemCategory.Name_En),
                                              Ex_checkReqItems_ID = v.Ex_CheckRequest_Items_ID,
                                           //   ItemCategory_ID = v.ItemCategory_ID,
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

                                              //Check_Lot_Old_ID = p.LotData_ID > 0 ? p.CommitteeResultType_ID > 0 ? (p.CommitteeResultType_ID != 7 ? 1:0):(p.Ex_RequestCommittee.Delegation_Date >= Date_Check1 ? 1:0) : 0 ,


                                              //بيانات النبات
                                              ID_Ex_Item = v.Ex_CheckRequest_Items.ID,
                                              //باقى ابعت بيانات الحاوية
                                             // ContainerNumber = v.Ex_CheckRequest_Items.Ex_CheckRequset_Shipping_Method.ContainerNumber,
                                              //containers_ID = v.Ex_CheckRequest_Items.Ex_CheckRequset_Shipping_Method.containers_ID,
                                              //containers_type_ID = v.Ex_CheckRequest_Items.Ex_CheckRequset_Shipping_Method.containers_type_ID,
                                              //containerName = entities.A_SystemCode.Where(c => c.Id == v.Ex_CheckRequest_Items.Ex_CheckRequset_Shipping_Method.containers_ID).FirstOrDefault().ValueName,
                                             // containerType = entities.A_SystemCode.Where(c => c.Id == v.Ex_CheckRequest_Items.Ex_CheckRequset_Shipping_Method.containers_type_ID && c.SystemCodeTypeId == 28).FirstOrDefault().ValueName,

                                             // ShipholdNumber = v.Ex_CheckRequest_Items.Ex_CheckRequset_Shipping_Method.ShipholdNumber,
                                              //NavigationalNumber = v.Ex_CheckRequest_Items.Ex_CheckRequset_Shipping_Method.NavigationalNumber,

                                              //Total_Weight = v.Ex_CheckRequest_Items.Ex_CheckRequset_Shipping_Method.Total_Weight,

                                              //Ex_CommitteeResult_ID = p.ID,
                                              //Delegation_Date = p.Ex_RequestCommittee.Delegation_Date,
                                              //CommitteeResultType_ID = p.CommitteeResultType_ID,
                                              //Check_Lot_Old_Name = p.LotData_ID > 0 ? p.CommitteeResultType_ID > 0 ? "تم عمل الاندريد" : "تم التشكيل ولم يتم الاندريد" : "لم يتم العمل عليه",
                                              // نهاية بيانات الحاوية
                                              //NOURA
                                              Order_TextLot = v.Order_Text,
                                              //RecordedOrNot = v.ItemCategory_ID == null ? "#####" : ((bool)v.ItemCategory.IsRegister ? "مسجل" : "غير مسجل"),
                                              //ItemCategoryGroup = v.ItemCategory_ID == null ? "#####" : v.ItemCategory.ItemCategories_Group_ID == null ? "لا يوجد"
                                        //  : (lang == "1" ? v.ItemCategory.ItemCategories_Group.Name_Ar : v.ItemCategory.ItemCategories_Group.Name_En),
                                              //subPartName =,
                                              packageMaterialName = (lang == "1" ? entities.Package_Material.Where(c => c.ID == v.Package_Material_ID).FirstOrDefault().Ar_Name :
                                          entities.Package_Material.Where(c => c.ID == v.Package_Material_ID).FirstOrDefault().En_Name),

                                              packageType = (lang == "1" ? entities.Package_Type.Where(c => c.ID == v.Package_Type_ID).FirstOrDefault().Ar_Name :
                                          entities.Package_Type.Where(c => c.ID == v.Package_Type_ID).FirstOrDefault().En_Name),

                                          }).ToList();

                        itm.ItemCategories_lots = catAndLots;
                        foreach (var item in catAndLots)
                        {
                            var dd = (from icr in entities.Ex_CommitteeResult
                                      where icr.LotData_ID == item.ID_Lot
                                      group icr by new
                                      {
                                          //id= icr.ID,
                                          Delegation_Date = icr.Ex_RequestCommittee.Delegation_Date,
                                          CommitteeResultType_ID = icr.CommitteeResultType_ID,
                                          Check_Lot_Old_Name = icr.LotData_ID > 0 ? icr.CommitteeResultType_ID > 0 ? "تم عمل الاندريد" : "تم التشكيل ولم يتم الاندريد" : "لم يتم العمل عليه",
                                      } into grp
                                      select new Ex_categories_lots_New
                                      {
                                          //id
                                          Ex_CommitteeResult_ID = grp.Max(a => a.ID),
                                          Delegation_Date = grp.Key.Delegation_Date,
                                          CommitteeResultType_ID = grp.Key.CommitteeResultType_ID,

                                      }).ToList();

                            var maxValue = dd.Max(x => x.Ex_CommitteeResult_ID);
                            if (dd != null)
                            {
                                item.Delegation_Date = dd.Where(a=> a.Ex_CommitteeResult_ID == maxValue).Select(a => a.Delegation_Date).FirstOrDefault();
                                item.Ex_CommitteeResult_ID = dd.Where(a => a.Ex_CommitteeResult_ID == maxValue).Select(a => a.Ex_CommitteeResult_ID).FirstOrDefault();
                                item.CommitteeResultType_ID = dd.Where(a => a.Ex_CommitteeResult_ID == maxValue).Select(a => a.CommitteeResultType_ID).FirstOrDefault();
                            }
                            //المعامل السابقة
                           // DateTime udd =Convert.ToDateTime( DateTime.Now.ToShortDateString());
                            var committee_Sample = (from cr in entities.Ex_CheckRequest_SampleData
                                                    where cr.LotData_ID == item.ID_Lot 
                                                    //&& cr.SampleSize != null
                                                    //&&cr.Ex_RequestCommittee.Delegation_Date >= udd
                                                    select new Ex_Committee_Sample_Lot
                                                    {
                                                       LotData_ID= cr.LotData_ID,
                                                        AnalysisLabType_ID = cr.AnalysisLabType_ID,
                                                        Analysis_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisType.Name_Ar : cr.AnalysisLabType.AnalysisType.Name_En),
                                                        Lab_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisLab.Name_Ar : cr.AnalysisLabType.AnalysisLab.Name_En),
                                                    }).ToList();
                            item.list_Committee_Sample_Lot = committee_Sample;
                        }

                        // catAndLots= catAndLots.GroupBy(a =>a.ID_Lot).Max(a=>a.Ex_CommitteeResult_ID)



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

        public Dictionary<string, object> Insert_Committee(Ex_RequestCommitteeDTO entity, List<string> Device_Info)
        {
            try
            {
                using (PlantQuarantineEntities context = new PlantQuarantineEntities())
                {
                    using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
                    {
                        var operationType = 74; //ask
                        long Committe_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_RequestCommittee_seq");

                        //entity.ID = Committe_ID;

                        //var Co = Mapper.Map<RequestCommitteeDTO, Ex_RequestCommittee>(entity);
                        //uow.Repository<Ex_RequestCommittee>().InsertReturn(Co);
                        //uow.SaveChanges();

                        var RequestCommittee = new Ex_RequestCommittee
                        {
                            ID = Committe_ID,
                            ExCheckRequest_ID = entity.Ex_CheckRequest_ID,
                            CommitteeType_ID = entity.CommitteeType_ID,
                            ExCommitteeCheckLocation_ID = entity.Ex_CommitteeCheckLocation_ID,
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
                                    Employee_Id =_Employee_Id,
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
                                    Amount =decimal.Parse( item.money.ToString()),
                                    User_Creation_Id = entity.User_Creation_Id,
                                    User_Creation_Date = entity.User_Creation_Date,
                                };
                                context.Ex_RequestCommittee_Shift.Add(Committee_Shift);
                                context.SaveChanges();
                                //var CModel = Mapper.Map<RequestCommittee_ShiftDTO, Ex_RequestCommittee_Shift>(item);
                                //    uow.Repository<Ex_RequestCommittee_Shift>().InsertRecord(CModel);
                                //    uow.SaveChanges();
                            }
                        }

                        #endregion
                        var Count_Sampel = 0;
                        #region بيانات الرسالة
                        if (entity.List_CommitteeResult != null && entity.List_CommitteeResult.Count > 0)
                        {
                            foreach (var item in entity.List_CommitteeResult)
                            {
                                long CommitteResult_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CommitteeResult_SEQ");
                                var CommitteeResult = new Ex_CommitteeResult
                                {
                                    ID = CommitteResult_ID,
                                    Committee_ID = Committe_ID,
                                    Ex_Request_Item_Id = item.Ex_Request_Item_Id,
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

                                //item.ID = CommitteResult_ID;
                                //item.Committee_ID = Committe_ID;
                                ////    item.Ex_RequestCommittee.ID = lotssss;
                                //var Co_R = Mapper.Map<CommitteeResultDTO, Ex_CommitteeResult>(item);
                                //uow.Repository<Ex_CommitteeResult>().InsertReturn(Co_R);
                                //uow.SaveChanges();


                            }
                        }

                        if (entity.List_SampleData != null && entity.List_SampleData.Count > 0)
                        {
                            foreach (var Item_Analysis in entity.List_SampleData)
                            {
                                var sm = context.Ex_CheckRequest_SampleData.Where(a=>a.LotData_ID == Item_Analysis.LotData_ID && a.AnalysisLabType_ID == Item_Analysis.AnalysisLabType_ID 
                                //&& a.SampleRatio != null 
                                //&& a.Ex_RequestCommittee.Delegation_Date < DateTime.Now.Date
                                ).ToList();
                                if (sm.Count==0)
                                {
                                    Count_Sampel += 1;
                                        long Ex_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");

                                        var CheckRequest_SampleData = new Ex_CheckRequest_SampleData
                                        {
                                            ID = Ex_CheckRequest_SampleData_ID,
                                            AnalysisLabType_ID = Item_Analysis.AnalysisLabType_ID,
                                            Ex_RequestCommittee_ID = Committe_ID,
                                            Ex_Request_Item_Id = Item_Analysis.Ex_Request_Item_Id,
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

                                        };
                                        context.Ex_CheckRequest_SampleData.Add(CheckRequest_SampleData);
                                        context.SaveChanges();
                                  


                               
                                }
                                //Item_Analysis.ID = Ex_CheckRequest_SampleData_ID;
                                //Item_Analysis.Ex_RequestCommittee_ID = Committe_ID;
                                ////    item.Ex_RequestCommittee.ID = lotssss;
                                //var Co_SM = Mapper.Map<CheckRequest_SampleDataDTO, Ex_CheckRequest_SampleData>(Item_Analysis);
                                //uow.Repository<Ex_CheckRequest_SampleData>().InsertReturn(Co_SM);
                                //uow.SaveChanges();

                            }
                        }
                        #endregion

                        if(entity.CommitteeType_ID == 13 && Count_Sampel >0)
                        {
                            trans.Commit();
                        }
                        else if (entity.CommitteeType_ID == 11)
                        {
                            trans.Commit();
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
       
        public Dictionary<string, object> Save_Lot(int lotss, List<Ex_Committee_ResultDTO> CheckedItemsList, List<string> Device_Info)
        {
            try
            {
                //var operationType = 90; //ask
                //var id = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CommitteeResult_SEQ");
                //entity.ID = id;
                //var Co = Mapper.Map<Ex_CommitteeResultDTO, Ex_CommitteeResult>(entity);
                //uow.Repository<Ex_CommitteeResultDTO>().InsertReturn(Co);
                //uow.SaveChanges();
                foreach (var item in CheckedItemsList)
                {
                    long CommitteResult_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CommitteeResult_SEQ");
                    item.ID = CommitteResult_ID;
                    //    item.Ex_RequestCommittee.ID = lotssss;
                    var Co = Mapper.Map<Ex_Committee_ResultDTO, Ex_CommitteeResult>(item);
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

        public Dictionary<string, object> Save_AnalysisList(int AnalysisList, List<Ex_SampleDataDTO> CheckedAnalysisList, List<string> Device_Info)
        {
            try
            {

                if (CheckedAnalysisList != null && CheckedAnalysisList.Count > 0)
                {
                    foreach (var Item_Analysis in CheckedAnalysisList)
                    {
                        long Ex_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_SampleData_SEQ");
                        Item_Analysis.ID = Ex_CheckRequest_SampleData_ID;
                        //    item.Ex_RequestCommittee.ID = lotssss;
                        var Co = Mapper.Map<Ex_SampleDataDTO, Ex_CheckRequest_SampleData>(Item_Analysis);
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

    }
}
