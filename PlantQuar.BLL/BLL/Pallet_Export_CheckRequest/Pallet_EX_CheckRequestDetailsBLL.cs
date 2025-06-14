using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_CheckRequest;

using PlantQuar.DTO.DTO.Pallet_Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace PlantQuar.BLL.BLL.Pallet_Export_CheckRequest
{
  public  class Pallet_EX_CheckRequestDetailsBLL
    {
        private UnitOfWork uow;

        public Pallet_EX_CheckRequestDetailsBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetExCheckRequestDetails
         (string EXCheckRequest_Number, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                //var CheckRequestDetails = entities.Ex_CheckRequestDetails(1, EXCheckRequest_Number).ToList();

                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();

                paramters_Type.Add("lang", SqlDbType.Int);
                paramters_Type.Add("CheckRequest_Number", SqlDbType.NVarChar);


                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("lang", lang);
                paramters_Data.Add("CheckRequest_Number", EXCheckRequest_Number);
                var CheckRequestDetails = uow.Repository<Pallet_EXRequestDetailsDTO>().CallStored("Pallet_Ex_CheckRequestDetails", paramters_Type,
                    paramters_Data, Device_Info).ToList();
                var _EXCheckRequest_DataId = (Int64)CheckRequestDetails.FirstOrDefault().Ex_CheckRequestData_ID;                           
                //                 }
                foreach (var check in CheckRequestDetails)
                {
                    //Add Company Activity
                    var CompanyActivities_Details = (from ca in entities.CompanyActivities
                                                         //join cat in entities.Enrollment_type on ca.Enrollment_type_ID equals cat.ID
                                                     where ca.Company_ID == check.Importer_ID
                                                     && ca.IsActive == true
                                                     select new CompanyActivitysDTO_Pallets
                                                     {

                                                         CompActivityType__Name = lang == "1" ? ca.A_SystemCode.ValueName : ca.A_SystemCode.ValueNameEN,
                                                         Enrollment_Name = ca.Enrollment_Name,
                                                         Enrollment_Number = ca.Enrollment_Number,
                                                         Enrollment_Start = ca.Enrollment_Start,
                                                         Enrollment_End = ca.Enrollment_End,
                                                         Enrollment_type_Name = lang == "1" ? ca.Enrollment_type.Ar_Name : ca.Enrollment_type.En_Name,

                                                     }).ToList();
                    check._CompanyActivitys = CompanyActivities_Details;
                    check.ExportsContacts = uow.Repository<Ex_ContactData>()
                      .GetData().Include(f => f.ContactType).
                 Where(a => a.Exporter_ID == check.Importer_ID && a.ExporterType_Id == 6).
                 Select(a => new ContactTypeDTO_Pallets
                 {
                     Name_Ar = (lang == "1" ? a.ContactType.Name_Ar : a.ContactType.Name_En),
                     Value = a.Value
                 }).ToList();
                    /////////////////////////////




                    //Items
                    var itemss = uow.Repository<Ex_CheckRequest_Items>().GetData().Where(i => i.Ex_CheckRequest_ID == check.Ex_CheckRequest_ID).
                        Select(v => new Items_checkReq_Pallets
                        {
                            //eslam 
                            Ex_Items_checkReqID = v.ID,
                            Size = v.Size,
                            Package_Count = v.Package_Count,
                            Package_Weight = v.Package_Weight,
                            Units_Number = v.Units_Number,
                            packageTypeID = v.Package_Type_ID,
                            GrossWeight = v.GrossWeight,
                            Net_Weight = v.Net_Weight,
                            Fees = v.Fees,
                            Item_ShortName_ID = v.Item_ShortName_ID,
                            Order_TextItem = v.Order_Text,
                            SubPart_id = v.SubPart_id,
                            Package_Material_ID = v.Package_Material_ID,
                            Is_LotDivision = v.Is_LotDivision,
                            Accept_Date = v.Accept_Date,
                            IsAccepted = v.IsAccepted,
                            FarmsData = v.FarmsData.Name_Ar,
                            ItemCategoryName = v.ItemCategory.Name_Ar,
                            Agriculture_Hand = v.Agriculture_Hand
                        }).Distinct().ToList();
                    //end Items AND GET DATE FOR ALL LOTS AND ITEMS




                    foreach (var itt in itemss)
                    {

                        if (itt.Item_ShortName_ID != null)
                        {
                            var ism = uow.Repository<Item_ShortName>().GetData().
                        Where(i => i.ID == itt.Item_ShortName_ID && i.User_Deletion_Id == null).FirstOrDefault();
                            itt.ItemShortNameAr = ism.ShortName_Ar;
                            itt.ItemShortNameEn = ism.ShortName_En;

                            if (itt.Purpose != null)
                            {
                                itt.Purpose = (lang == "1" ? ism.Item_Purpose.Ar_Name : ism.Item_Purpose.En_Name);
                            }
                            if (itt.Status != null)
                            {
                                itt.Status = (lang == "1" ? ism.Item_Status.Ar_Name : ism.Item_Status.En_Name);
                            }
                            if (itt.ItemName != null)
                            {
                                itt.ItemName = (lang == "1" ? ism.Item.Name_Ar : ism.Item.Name_En);

                            }
                            if (itt.SubPart_Name != null)
                            {
                                itt.SubPart_Name = (lang == "1" ? ism.SubPart.Name_Ar : ism.SubPart.Name_En);

                            }
                        }


                        var itemShortNameId = itt.Item_ShortName_ID;//13;//
                        var itemCategories_ID = itt.ItemCategory_ID;

                        //Eslam Get Constrain   
                        #region getConstrain

                        //var ExportCountryId = CheckRequestDetails.FirstOrDefault().TransportCountryList.Select(a => a.TransportCountryID).FirstOrDefault();
                        //var TransiteCountryId = CheckRequestDetails.FirstOrDefault().TransiteCountryList.Select(a => a.TransiteCountryID).FirstOrDefault();
                        
                        #endregion

                        if (itemShortNameId != null)
                        {
                            var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId && a.User_Deletion_Id == null).FirstOrDefault();
                            itt.ItemShortNameAr = itemShortName.ShortName_Ar;
                            itt.ItemShortNameEn = itemShortName.ShortName_En;
                            //الجزء النباتي
                            //itt.SubPart_Name = (lang == "1" ? itemShortName.SubPart.Name_Ar : itemShortName.SubPart.Name_En);
                            if (itt.ItemName != null)
                            {
                                itt.ItemName = (lang == "1" ? itemShortName.Item.Name_Ar : itemShortName.Item.Name_En);
                            }
                            var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
                            if (stat != null)
                            {
                                itt.Status = stat.Ar_Name;
                            }
                            var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
                            if (prp != null)
                            {
                                itt.Purpose = prp.Ar_Name;
                            }
                            var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
                            if (subp != null)
                            {
                                itt.subPartName = subp.Name_Ar;
                            }

                            itt.ItemName = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault().Name_Ar;
                        }
                        //List categories And lots

                        var catAndLots = uow.Repository<Ex_CheckRequest_Items_Lot_Category>().GetData().Where(d => d.Ex_CheckRequest_Items_ID == itt.Ex_Items_checkReqID)
                            .Select(v => new categories_lots_Pallets
                            {
                                //eslam
                                ID = v.ID,
                                Ex_CheckRequest_Items_ID = v.Ex_CheckRequest_Items_ID,

                                Size = v.Size,
                                Package_Count = v.Package_Count,
                                Package_Weight = v.Package_Weight,
                                Units_Number = v.Units_Number,
                                packageTypeID = v.Package_Type_ID,
                                Order_TextLot = v.Order_Text,
                                packageMaterialID = v.Package_Material_ID,
                                Lot_Number = v.Lot_Number,
                                Grower_Number = v.Grower_Number,
                                Waybill = v.Waybill,
                                Number_Wooden_Package = v.Number_Wooden_Package,
                                GrossWeight = v.GrossWeight,
                                Net_Weight = v.Net_Weight,
                                Package_Based_Weight = v.Package_Based_Weight,
                                Package_Net_Weight = v.Package_Net_Weight,
                                Reason_Entry = v.Reason_Entry,
                                Based_Weight = v.Based_Weight,

                            })
                          .ToList();


                        foreach (var ctt in catAndLots)
                        {
                            var ptypec = uow.Repository<Package_Type>().GetData().Where(d => d.ID == ctt.packageTypeID).FirstOrDefault();
                            if (ptypec != null)
                            {
                                ctt.packageType = (lang == "1" ? ptypec.Ar_Name : ptypec.En_Name);//ptypec.Ar_Name;
                            }


                            var categ = uow.Repository<ItemCategory>().GetData().Where(g => g.ID == ctt.ItemCategory_ID).FirstOrDefault();
                            if (categ != null)
                            {
                                ctt.categoryName = (lang == "1" ? categ.Name_Ar : categ.Name_En);//categ.Name_Ar;
                                ctt.RecordedOrNot = ((bool)categ.IsRegister ? "مسجل" : "غير مسجل");
                                if (categ.ItemCategories_Group_ID == null)
                                {
                                    ctt.ItemCategoryGroup = "لا يوجد";
                                }
                                else
                                {
                                    var ccc = uow.Repository<ItemCategories_Group>().GetData().Where(d => d.ID == categ.ItemCategories_Group_ID).FirstOrDefault();
                                    ctt.ItemCategoryGroup = (lang == "1" ? ccc.Name_Ar : ccc.Name_En);
                                }

                            }
                            var pckmtr = uow.Repository<Package_Material>().GetData().Where(g => g.ID == ctt.packageMaterialID).FirstOrDefault();
                            if (pckmtr != null)
                            {
                                ctt.packageMaterialName = (lang == "1" ? pckmtr.Ar_Name : pckmtr.En_Name);
                            }


                        }
                        itt.Pallets_ItemCategories_lots = catAndLots;

                    }
                    check.Items_checkReq_Pallets = itemss;
                    // Attachments
                    //Attachments


                }
                var _Ex_CheckRequest_ID = CheckRequestDetails.FirstOrDefault().Ex_CheckRequest_ID;
                var _EXCheckRequest_Number = CheckRequestDetails.FirstOrDefault().EXCheckRequest_Number;


                //Attachments
                var attach = uow.Repository<A_AttachmentData_Ex_CheckRequest>().GetData()
                                          .Where(v => v.Ex_CheckRequest_ID == _Ex_CheckRequest_ID && v.User_Deletion_Id == null)
                                           .Select(x => new Attachments_Pallets
                                           {
                                               Attachment_Number = x.Attachment_Number,
                                               AttachmentPath = x.AttachmentPath,
                                               Attachment_TypeName = x.Attachment_TypeName,
                                               StartDate = x.StartDate,
                                               EndDate = x.EndDate,
                                               Attachment_Name = (lang == "1" ? x.A_AttachmentTableType.Ar_Name : x.A_AttachmentTableType.En_Name)


                                           }).ToList();
                CheckRequestDetails.FirstOrDefault().Attachments_Pallets = attach;

                //Attachments
                //الرسوم Fees Eslam
                var item_Fees = (from im_i in entities.Ex_CheckRequest_Items
                                 join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
                                 where im_i.Ex_CheckRequest_ID == _Ex_CheckRequest_ID
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
                                 select new Fees_Item_Pallets
                                 {

                                     ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,

                                     ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
                                     Fees = grp.Sum(q => q.Fees),
                                     GrossWeight = grp.Sum(q => q.GrossWeight) / 1000,
                                     Net_Weight = grp.Sum(q => q.Net_Weight),
                                 }).Distinct().ToList();
                CheckRequestDetails.FirstOrDefault().Fees_Item_All_Pallets = item_Fees;
                CheckRequestDetails.FirstOrDefault().Fees_Item_Shift_All_Pallets = (from cms in entities.Ex_RequestCommittee_Shift
                                                                            where cms.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                                                            select new Fees_Item_Shift_Pallets
                                                                            {
                                                                                Amount_Per_Shift = cms.Amount,
                                                                                Count_Per_Shift = cms.Count,
                                                                                total_Per_Shift = (cms.Count * cms.Amount)
                                                                            }).ToList();

                var _total_Per_Shift = CheckRequestDetails.FirstOrDefault().Fees_Item_Shift_All_Pallets.Select(z => z.total_Per_Shift).Sum();
                CheckRequestDetails.FirstOrDefault().Shift_Item_All = _total_Per_Shift;

                #region رسم  النوبتجية
                // 
                var Fees_Item_Shift = (from sh in entities.Ex_RequestCommittee_Shift

                                       where sh.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                       group sh by new
                                       {
                                           ID = sh.ID,
                                           Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,

                                       } into grp
                                       select new List_Shift_Pallets
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
                                  && ftd.Fees_Transactions.Table_ID == _Ex_CheckRequest_ID
                                  select new List_Shift_Pallets
                                  {
                                      Is_Paid_Shift = ftd.Shift_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
                                  }
                               ).ToList();
                    if (_Shift.Count > 0)
                        item.Is_Paid_Shift = _Shift.FirstOrDefault().Is_Paid_Shift;
                    else
                        item.Is_Paid_Shift = null;
                }

                CheckRequestDetails.FirstOrDefault().List_Shift_Pallets = Fees_Item_Shift;
                #endregion


                #region   رسوم السحب




                var Fees_Sample = (from sm in entities.Ex_CheckRequest_SampleData

                                   where sm.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                   group sm by new
                                   {
                                       ID = sm.ID,
                                       Sample_BarCode = sm.Sample_BarCode,
                                       Is_Total = sm.IS_Total,
                                       Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
                                       Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,


                                   } into grp
                                   select new List_Sample_Pallets
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
                                   && ftd.Fees_Transactions.Table_ID == _Ex_CheckRequest_ID
                                   select new List_Sample_Pallets
                                   {
                                       Is_Paid_Sample = ftd.SampleData_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
                                   }
                               ).ToList();
                    if (_Sample.Count > 0)
                        item.Is_Paid_Sample = _Sample.FirstOrDefault().Is_Paid_Sample;
                    else
                        item.Is_Paid_Sample = null;
                }
                CheckRequestDetails.FirstOrDefault().List_Sample_Pallets = Fees_Sample44;




                #endregion


                #region رسوم المعالجة
                //جزئى


                var Fees_Treatment = (from rq in entities.Ex_RequestCommittee
                                      join td in entities.Ex_Request_TreatmentData on rq.ID equals td.Ex_RequestCommittee_ID
                                      where td.Ex_RequestCommittee.Ex_CheckRequest.CheckRequest_Number == _EXCheckRequest_Number
                                     && rq.CommitteeType_ID == 14 && rq.Status == true
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
                                      select new List_Treatment_Pallets
                                      {
                                          ID = grp.Key.ID,
                                          TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                          TreatmentType_Name = grp.Key.TreatmentType_Name,
                                          TreatmentMat_Name = grp.Key.TreatmentMat_Name,
                                          Treatment_Amount = grp.Key.Treatment_Amount
                                      }).Distinct().ToList();
                //var Fees_Treatment2 = Fees_Treatment.GroupBy(a => a.ID).Select(a => a.First()).ToList();
                if (Fees_Treatment.Count() > 0)
                {
                    foreach (var item in Fees_Treatment)
                    {
                        long ID_Item = long.Parse(item.ID.ToString());
                        var _Treatment = (from ftd in entities.Fees_Transactions_Detiles
                                          where ftd.TreatmentData_ID == ID_Item
                                          select new List_Treatment_Pallets
                                          {
                                              Is_Paid_Treatment = ftd.TreatmentData_ID > 0 ? "تم الدفع" : "لم يتم الدفع"
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
                CheckRequestDetails.FirstOrDefault().List_Treatment_Pallets = Fees_Treatment;


                #endregion


                decimal item_Fees_Total = 0;


                if (item_Fees != null)
                {
                    item_Fees_Total = item_Fees.Select(a => a.Fees).Sum().Value;
                }

                var Sum_List_Sample = CheckRequestDetails.FirstOrDefault().List_Sample_Pallets.Select(c => c.Sample_Sum_All).Sum();



                CheckRequestDetails.FirstOrDefault().SUM_Shift_Fees_Item = 10 + _total_Per_Shift + item_Fees_Total + Sum_List_Sample;
                //                    ///////////////ESLAM///////////////
                //                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, CheckRequestDetails);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        //approve request
        public Dictionary<string, object> ApproveCheckReq(Pallet_EXRequestDetailsDTO dto, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest CModel = uow.Repository<Ex_CheckRequest>().Findobject(dto.ID);
                CModel.IsAccepted = dto.IsAccepted;
                CModel.IsActive = dto.IsAccepted;
                uow.SaveChanges();

                var empDTO = Mapper.Map<Ex_CheckRequest, Pallet_EXRequestDetailsDTO>(CModel);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        //refuse reason 
        public Dictionary<string, object> FillDrop_RefuseReason(int refuse, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Refuse_Reason>().GetData().Where(lab => lab.User_Deletion_Id == null && lab.IsActive == true && (lab.IsExport == 81 || lab.IsExport == 82));
            if (refuse == 1)
            {
                data = data.Where(res => res.Refused_stopped == 84);
            }
            else
            {
                data = data.Where(res => res.Refused_stopped == 83);
            }
            var data2 = data.Select(c => new CustomOption
            {
                //change display lang
                DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                Value = c.ID
            }).OrderBy(a => a.DisplayText).ToList();
            //set default value fz 17-4-2019
            data2.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data2);
        }
        public Dictionary<string, object> InsertReasons(ReasonsListReqIdDTO dto, List<string> Device_Info)
        {
            try
            {
                EX_CheckRequest_RefuseReasonDTO rr = new EX_CheckRequest_RefuseReasonDTO();
                foreach (var id in dto.refuseReasonsIds)
                {

                    rr.Ex_CheckRequest_Id = dto.checkReqId;
                    rr.Refuse_Reason_Id = id;
                    rr.User_Creation_Id = dto.User_Creation_Id;
                    rr.User_Creation_Date = dto.User_Creation_Date;
                    InsertReason(rr, Device_Info);
                }




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dto.checkReqId);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InsertReason(EX_CheckRequest_RefuseReasonDTO entity, List<string> Device_Info)
        {
            try
            {

                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_RefuseReason_seq");
                entity.ID = idd;
                var CModel = Mapper.Map<Ex_CheckRequest_RefuseReason>(entity);

                uow.Repository<Ex_CheckRequest_RefuseReason>().InsertRecord(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> saveItemFees(Items_checkReq item, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest_Items CModel = uow.Repository<Ex_CheckRequest_Items>().Findobject((long)item.Ex_Items_checkReqID);
                CModel.Fees = item.Fees;

                uow.SaveChanges();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, item.Ex_Items_checkReqID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
