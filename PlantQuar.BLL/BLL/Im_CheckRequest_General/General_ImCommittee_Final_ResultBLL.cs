using AutoMapper;
using PlantQuar.BLL.BLL.Log;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Im_CheckRequest_General;
using PlantQuar.DTO.DTO.Log;
//using PlantQuar.DTO.DTO.Import.IM_Committee;
//using PlantQuar.DTO.DTO.Log;
//using PlantQuar.DTO.DTO.Shipping;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Im_CheckRequest_General
{
    public class General_ImCommittee_Final_ResultBLL
    {

        //xyza 
        private UnitOfWork uow;
        PlantQuarantineEntities entities = new PlantQuarantineEntities();
        dbPrivilageEntities priv = new dbPrivilageEntities();

        public General_ImCommittee_Final_ResultBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetImCheckRequestList_filter
            (long ImCheckRequest_Number, long item_ShortName_ID, long Lots_itemShortName_ID,
            long CommitteeTypeLst_ID, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var CheckRequestDetails = (from cc in entities.Im_CheckRequest
                                           join crd in entities.Im_CheckRequest_Data on cc.ID equals crd.Im_CheckRequest_ID
                                           join icrp in entities.Im_CheckRequest_Port on crd.ID equals icrp.Im_CheckRequest_Data_ID
                                           join pn in entities.PortNationals on icrp.Port_ID equals pn.ID
                                           //from pn in pn1.DefaultIfEmpty()
                                           where cc.ID == ImCheckRequest_Number
                                           && icrp.ReqPortType_ID == 10

                                           select new General_ImCommittee_Final_ResultDTO
                                           {
                                               Im_CheckRequest_ID = cc.ID,
                                               ImCheckRequest_Number = cc.CheckRequest_Number,
                                               OutLet_Name = cc.Outlet.Ar_Name,
                                               OutLet_ID = cc.Outlet.ID,
                                               StartDate = cc.User_Creation_Date,
                                               ArrivePortType = lang == "1" ? pn.Port_Type.Name_Ar : pn.Port_Type.Name_En,
                                               ArrivePortName = pn.Port_Type.Name_Ar + "/" + pn.Name_Ar + "/" + pn.Governate.Ar_Name,
                                               govNameAR = lang == "1" ? pn.Governate.Ar_Name : pn.Governate.En_Name,
                                           }).FirstOrDefault();



                // كل اللوطات اللى واخده موقف نهائي ايقاف
                var Lot_Final_Resalt_Type_Stop = (from lot in entities.Im_CheckRequest_Items_Lot_Category
                                                  join lor in entities.Im_CheckRequest_Items_Lot_Result on lot.ID equals lor.Im_CheckRequest_Items_Lot_Category_ID
                                                  where lot.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.Im_CheckRequest_ID == ImCheckRequest_Number
                                                && lor.Im_CheckRequest_Lot_Result_Status.Is_Continue == false
                                                  select lor.Im_CheckRequest_Items_Lot_Category_ID).ToList();
                //كل اللوطات اللى مش واخده موقف نهائي
                var Check_Lot_Count = (from lot in entities.Im_CheckRequest_Items_Lot_Category
                                       where lot.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.Im_CheckRequest_ID == ImCheckRequest_Number
                                        && !Lot_Final_Resalt_Type_Stop.Contains(lot.ID)
                                       select lot.ID).Count();

                //     && !(from lor in entities.Im_CheckRequest_Items_Lot_Result
                //        //where o.Im_CheckRequest_Lot_Result_Status.Is_Continue==0
                //        select lor.Im_CheckRequest_Items_Lot_Category_ID)
                //.Contains(lot.ID)
                //     select lot.ID).Count();

                CheckRequestDetails.Count_Lots = Check_Lot_Count;
                //var Check_Lot_Count = (from Lot in entities.Im_CheckRequest_Items_Lot_Category
                //                       where Lot.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.Im_CheckRequest_ID == ImCheckRequest_Number
                //                       && Lot.ID!(from Lot_R in entities.Im_CheckRequest_Items_Lot_Result )
                //                          )
                //                            select new ImCommittee_Final_ResultDTO
                //                            {
                //                                Count_Lots = Lot.ID

                //                            }).Count();


                var customs = (from crcm in entities.Im_CheckRequest_Customs_Message
                               where crcm.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID
                               select new General_CustomsMessage
                               {
                                   Im_CheckRequest_ID = crcm.Im_CheckRequest_ID,
                                   Customs_Certificate_Number = crcm.Customs_Certificate_Number,
                                   Shipment_Date = crcm.Shipment_Date,
                                   Certification_Date = crcm.Certification_Date,
                                   Arrival_Date = crcm.Arrival_Date,
                                   Manifest_Number = crcm.Manifest_Number,
                                   Certificate_Number_Each_Product = crcm.Certificate_Number_Each_Product,
                                   Shipping_Agency_ID = crcm.Shipping_Agency_ID,
                                   Shipping_Agency_Name = (lang == "1" ? crcm.ShippingAgency.Name_Ar : crcm.ShippingAgency.Name_En)

                               }).ToList();

                CheckRequestDetails.CustomsMessages = customs;

                var finalResult = (from crfr in entities.Im_CheckRequest_Final_Result
                                   join imfr in entities.Im_Final_Result on crfr.Im_Final_Result_ID equals imfr.ID
                                   where crfr.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID
                                   select new General_FinalResult
                                   {
                                       Im_CheckRequest_FinalResult_ID = crfr.Im_CheckRequest_ID,
                                       DateFinalResult = crfr.Date,
                                       Final_Result_EmployeeId = crfr.User_Creation_Id,
                                       Final_Result_Status = imfr.Status,
                                       Final_Result_Name = (lang == "1" ? imfr.Ar_Name : imfr.En_Name),
                                   }).ToList();

                foreach (var fRes in finalResult)
                {
                    if (fRes.Final_Result_EmployeeId != null)
                    {

                        fRes.Final_Result_Employee_Name = priv.PR_User.Where(c => c.Id == fRes.Final_Result_EmployeeId).FirstOrDefault().FullName;
                    }
                }
                CheckRequestDetails.FinalResults = finalResult;

                var CheckRequest_Visa = (from imv in entities.Im_CheckRequest_Visa
                                         where imv.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID
                                         select new General_CheckRequest_Visa
                                         {
                                             Im_CheckRequest_Visa_ID = imv.Im_CheckRequest_ID,
                                             Date_Visa = imv.Date,
                                             Visa_Result_EmployeeId = imv.User_Creation_Id,
                                             //Visa_Result_Status = imv.Status,
                                             Visa_Result_Name = (lang == "1" ? imv.Im_Visa.Description_Ar : imv.Im_Visa.Description_En),
                                         }).ToList();

                foreach (var Item_Visa in CheckRequest_Visa)
                {
                    if (Item_Visa.Visa_Result_EmployeeId != null)
                    {

                        Item_Visa.Visa_Result_Employee_Name = priv.PR_User.Where(c => c.Id == Item_Visa.Visa_Result_EmployeeId).FirstOrDefault().FullName;
                    }
                }
                CheckRequestDetails.CheckRequest_Visa = CheckRequest_Visa;

                if (CheckRequestDetails != null)
                {
                    //foreach (var item in CheckRequestDetails)
                    //{
                    //Items

                    //

                    var itemss = (from im_i in entities.Im_CheckRequest_Items
                                  join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
                                  where im_i.Item_Permission_Number == CheckRequestDetails.ImCheckRequest_Number
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
                                      InitiatorCountry = im_i.Im_Initiator.Country.Ar_Name,
                                      InitiatorCountryEn = im_i.Im_Initiator.Country.En_Name,
                                      //subPartName = (lang == "1" ? entities.SubParts.Where(c => c.ID == im_i.Item_ShortName_ID).FirstOrDefault().Ar_Name :
                                      //        entities.SubParts.Where(c => c.ID == v.Package_Type_ID).FirstOrDefault().En_Name),


                                  } into grp
                                  select new General_Items_checkReq_New
                                  {
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
                    if (item_ShortName_ID > 0)
                    {
                        itemss = itemss.Where(a => a.Item_ShortName_ID == item_ShortName_ID).Distinct().ToList();
                    }

                    foreach (var itm in itemss)
                    {

                        var itemShortName = (from isn in entities.Item_ShortName

                                             where isn.ID == itm.Item_ShortName_ID
                                             select new General_Items_checkReq_New
                                             {
                                                 subPartName = (lang == "1" ? isn.SubPart.Name_Ar : isn.SubPart.Name_En)
                                             }
                                             ).FirstOrDefault();
                        itm.subPartName = itemShortName.subPartName;
                        // uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId && a.User_Deletion_Id == null).FirstOrDefault();
                        //  var CommitteeResultType_ID = entities.Im_CommitteeResult.Where(c => c.LotData_ID == 381).Select(a=>a.CommitteeResultType_ID).Last();
                        var catAndLots = (from im_i in entities.Im_CheckRequest_Items
                                          join v in entities.Im_CheckRequest_Items_Lot_Category on im_i.ID equals v.Im_CheckRequest_Items_ID
                                          // join itg in entities.ItemCategories 

                                          //join imcr in entities.Im_CommitteeResult on im_i.ID equals imcr.LotData_ID into imcr2
                                          //from im_cr in imcr2.DefaultIfEmpty()
                                          where im_i.Item_Permission_Number == CheckRequestDetails.ImCheckRequest_Number
                                          && im_i.Item_ShortName_ID == itm.Item_ShortName_ID
                                          select new General_categories_lots_New
                                          {
                                              ID_Lot = v.ID,
                                              categoryName = (lang == "1" ? v.ItemCategory.Name_Ar : v.ItemCategory.Name_En),
                                              Im_checkReqItems_ID = v.Im_CheckRequest_Items_ID,
                                              ItemCategory_ID = v.ItemCategory_ID,
                                              Size = v.Size,
                                              Package_Count = v.Package_Count,
                                              Package_Weight = v.Package_Weight,
                                              Package_Based_Weight = v.Package_Based_Weight,
                                              Package_Net_Weight = v.Package_Net_Weight,

                                              Units_Number = v.Units_Number,
                                              packageTypeID = v.Package_Type_ID,
                                              GrossWeight = v.GrossWeight,
                                              packageMaterialID = v.Package_Material_ID,
                                              Lot_Number = v.Lot_Number,
                                              Grower_Number = v.Grower_Number,
                                              Waybill = v.Waybill,
                                              Number_Wooden_Package = v.Number_Wooden_Package,
                                              Net_Weight = v.Net_Weight,

                                              //
                                              // عرض اللوطات اللى اتعملها فحص
                                              // 0 لم يتم تشكيل لجنة على اللوط
                                              // 1  تم تشكيل لجنة على اللوط ولم يعمل الاندريد
                                              // 2  تم تشكيل لجنة على اللوط وتم عمل الاندريد
                                              //Check_Lot_Old_ID = p.LotData_ID > 0 ? 1 : 0,

                                              //Check_Lot_Old_Name = p.LotData_ID > 0 ? p.CommitteeResultType_ID > 0 ? "تم عمل الاندرويد" : "تم التشكيل ولم يتم الاندرويد" : "لم يتم العمل عليه",
                                              //بيانات النبات
                                              ID_IM_Item = v.Im_CheckRequest_Items.ID,
                                              //باقى ابعت بيانات الحاوية
                                              ContainerNumber = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.ContainerNumber,
                                              //containers_ID = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_ID,
                                              //containers_type_ID = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_type_ID,
                                              containerName = entities.A_SystemCode.Where(c => c.Id == v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_ID).FirstOrDefault().ValueName,
                                              containerType = entities.A_SystemCode.Where(c => c.Id == v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_type_ID && c.SystemCodeTypeId == 29).FirstOrDefault().ValueName,

                                              ShipholdNumber = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.ShipholdNumber,
                                              NavigationalNumber = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.NavigationalNumber,

                                              Total_Weight = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.Total_Weight,


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



                                              //CommitteeResultType_ID = entities.Im_CommitteeResult.LastOrDefault().CommitteeResultType_ID,

                                          }).ToList();
                        foreach (var item in catAndLots)
                        {
                            var Lot_Status_Result_Var = (from icr in entities.Im_CheckRequest_Items_Lot_Result

                                                         where icr.Im_CheckRequest_Items_Lot_Category_ID == item.ID_Lot
                                                         //&& icr.IS_Status_Committee == null
                                                         select new General_Lot_Status_Result
                                                         {
                                                             // نهاية بيانات الحاوية
                                                             Lot_Status_Result_ID = icr.ID,
                                                             IS_Status_Lot_Result = icr.IS_Status,
                                                             Note_Lot_Result = icr.Nots,
                                                             IS_Status_Name = icr.Im_CheckRequest_Lot_Result_Status.Name_AR,
                                                             IS_Status_Committee = icr.IS_Status_Committee,
                                                             Is_Continue = icr.Im_CheckRequest_Lot_Result_Status.Is_Continue,
                                                             // User_Name=priv.PR_User.Where(c => c.Id == icr.User_Creation_Id).FirstOrDefault().FullName,
                                                             User_Creation_Date = icr.User_Creation_Date,
                                                             User_Creation_Id = icr.User_Creation_Id,
                                                             // icr.IS_Status=icr.IS_Status,
                                                         }).ToList();

                            foreach (var iteme in Lot_Status_Result_Var)
                            {
                                if (iteme.User_Creation_Id != null)
                                {
                                    short Employee_Id = short.Parse(iteme.User_Creation_Id.ToString());
                                    iteme.User_Name = priv.PR_User.Where(c => c.Id == Employee_Id).FirstOrDefault().FullName;
                                }
                            }

                            item.Lot_Status_Result = Lot_Status_Result_Var;

                            var dd = (from icr in entities.Im_CommitteeResult
                                      where icr.LotData_ID == item.ID_Lot
                                      group icr by new
                                      {
                                          CommitteeResultType_ID = icr.CommitteeResultType_ID,
                                      } into grp
                                      select new General_categories_lots_New
                                      {
                                          Im_CommitteeResult_ID = grp.Max(a => a.ID),
                                          CommitteeResultType_ID = grp.Key.CommitteeResultType_ID,

                                      }).OrderByDescending(a => a.Im_CommitteeResult_ID).FirstOrDefault();
                            if (dd != null)
                            {


                                item.CommitteeResultType_ID = dd.CommitteeResultType_ID;
                            }
                        }
                        if (Lots_itemShortName_ID > 0)
                        {
                            catAndLots = catAndLots.Where(a => a.ID_Lot == Lots_itemShortName_ID).Distinct().ToList();
                        }

                        //لجنة الفحص
                        if (CommitteeTypeLst_ID == 0 || CommitteeTypeLst_ID == 11)
                        {
                            //noura
                            foreach (var item_Comm in catAndLots)
                            {
                                //item_Comm.Lot_Committee_Result.Is_Result_Finch = null;
                                var committee = (from rc in entities.Im_RequestCommittee
                                                 join cr in entities.Im_CommitteeResult on rc.ID equals cr.Committee_ID
                                                 join CE in entities.CommitteeEmployees on rc.ID equals CE.Committee_ID
                                                 where cr.LotData_ID == item_Comm.ID_Lot
                                                 //  && rc.CommitteeType_ID == 11
                                                  && CE.ISAdmin == true
                                                  //EslamMaher TO check opertion type of committee must be وارد 
                                                  && CE.OperationType == 74
                                                  && CE.User_Deletion_Date == null

                                                  && CE.User_Deletion_Id == null
                                                 select new General_Committee_Result_Lot
                                                 {
                                                     Committee_Result_Lot_ID = cr.ID,
                                                     ResultTypes_Name = (lang == "1" ? cr.CommitteeResultType.Name_Ar : cr.CommitteeResultType.Name_En),
                                                     EmployeeId = CE.Employee_Id,

                                                     Delegation_Date = rc.Delegation_Date,
                                                     StartTime = rc.StartTime,
                                                     EndTime = rc.EndTime,
                                                     IsApproved = rc.IsApproved,
                                                     IsFinishedAll = rc.IsFinishedAll,
                                                     Status = rc.Status,
                                                     Date = cr.Date,
                                                     Notes = cr.Notes,
                                                     ISAdmin = CE.ISAdmin,
                                                     Committee_ID = CE.Committee_ID,
                                                     Is_Result_Finch = cr.CommitteeResultType_ID == null ? false : true,
                                                     // عرض  فحص اللوطات
                                                     IS_Total = cr.IS_Total,
                                                     IS_TotalAndroid = cr.IS_Total_Android,
                                                 }).ToList();

                                try
                                {
                                    foreach (var iteme in committee)
                                    {

                                        if (iteme.EmployeeId != null)
                                        {
                                            short Employee_Comm_Id = short.Parse(iteme.EmployeeId.ToString());
                                            iteme.Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                                // بيانات المساعد فحص
                                foreach (var item_Conferm in committee)
                                {
                                    var committee_Conferm = (from cr in entities.Im_CommitteeResult
                                                             join CE in entities.CommitteeEmployees on cr.Committee_ID equals CE.Committee_ID
                                                             join rc in entities.Im_CommitteeResult_Confirm on cr.ID equals rc.Im_CommitteeResult_ID
                                                             into emp_Conferm
                                                             from rc in emp_Conferm.Where(a => a.EmployeeId == CE.Employee_Id).DefaultIfEmpty()

                                                                 //join rc2 in entities.Im_CommitteeResult_Confirm on CE.Employee_Id equals rc2.EmployeeId
                                                                 //into emp_Conferm2 from rc2 in emp_Conferm.DefaultIfEmpty()

                                                                 //.Where(A=> CE.Employee_Id == emp_Conferm.)
                                                             where cr.Committee_ID == item_Conferm.Committee_ID
                                                             && cr.LotData_ID == item_Comm.ID_Lot
                                                             && CE.ISAdmin == false
                                                               && CE.OperationType == 74
                                                             //&& CE.User_Deletion_Date == null
                                                             //    && CE.User_Deletion_Id == null
                                                             select new General_Committee_Result_Lot_Conferm
                                                             {
                                                                 EmployeeId = CE.Employee_Id,
                                                                 Notes = rc.Notes,
                                                                 Date = rc.Date,
                                                                 IsAccepted = rc.IsAccepted,
                                                                 User_Deletion_Id = CE.User_Deletion_Id,
                                                                 User_Deletion_Date = CE.User_Deletion_Date,

                                                             }).ToList();
                                    try
                                    {

                                        foreach (var iteme in committee_Conferm)
                                        {
                                            if (iteme.EmployeeId != null)
                                            {
                                                short Employee_Comm_Id = short.Parse(iteme.EmployeeId.ToString());
                                                iteme.Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                                                if (iteme.User_Deletion_Id != null)
                                                {
                                                    short Employee_Comm_Conferm = short.Parse(iteme.User_Deletion_Id.ToString());
                                                    iteme.Employee_Name_Conferm = priv.PR_User.Where(c => c.Id == Employee_Comm_Conferm).FirstOrDefault().FullName;
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {


                                    }
                                    item_Conferm.List_Committee_Result_Conferm = committee_Conferm;
                                }
                                item_Comm.Lot_Committee_Result = committee;

                                #region noura //افه
                                foreach (var item_Infection in committee)
                                {
                                    var committee_Infection = (from rc in entities.Im_RequestCommittee
                                                               join cr in entities.Im_CommitteeResult on rc.ID equals cr.Committee_ID
                                                               join CI in entities.Im_CommitteeResult_Infection on cr.ID equals CI.Im_CommitteeResult_ID
                                                               join It in entities.Items on CI.Item_ID equals It.ID
                                                               where cr.LotData_ID == item_Comm.ID_Lot
                                                               && CI.Im_CommitteeResult_ID == item_Infection.Committee_Result_Lot_ID
                                                               select new General_List_Im_CommitteeResult_Infection
                                                               {

                                                                   ID = CI.ID,
                                                                   Item_ID = CI.Item_ID,
                                                                   Im_CommitteeResult_ID = CI.Im_CommitteeResult_ID,
                                                                   Item_Name = (lang == "1" ? CI.Item.Name_Ar : CI.Item.Name_En),
                                                                   FamliyName = lang == "1" ? It.Family.Name_Ar : It.Family.Name_En,
                                                                   PhylumSubphylumName = lang == "1" ? It.Family.Order.PhylumSubphylum.Name_Ar : It.Family.Order.PhylumSubphylum.Name_En,
                                                                   LevelName = lang == "1" ? It.Family.Order.PhylumSubphylum.Level.Name_Ar : It.Family.Order.PhylumSubphylum.Level.Name_En,
                                                                   KingdomName = lang == "1" ? It.Family.Order.PhylumSubphylum.Kingdom.Name_Ar : It.Family.Order.PhylumSubphylum.Kingdom.Name_En,
                                                                   Order_Name = lang == "1" ? It.Family.Order.Name_Ar : It.Family.Order.Name_En,
                                                                   Group_Name = lang == "1" ? It.Group.Name_Ar : It.Group.Name_En,
                                                                   Secondary_Classification_Name = lang == "1" ? It.Group.SecondaryClassification.Name_Ar : It.Group.SecondaryClassification.Name_En,
                                                                   Main_Classification_Name = lang == "1" ? It.Group.SecondaryClassification.MainCalssification.Name_Ar : It.Group.SecondaryClassification.MainCalssification.Name_En,
                                                                   Description = lang == "1" ? It.Descreption_Ar : It.Descreption_En,
                                                                   Scientific_Name = It.Scientific_Name,
                                                                   Is_Forbidden_Reason = It.ForbiddenReason == null ? "---" : It.ForbiddenReason,
                                                                   ItemName = lang == "1" ? It.Name_Ar : It.Name_En,
                                                                   Is_Forbidden = It.IsForbidden == false ? "غير مسموح" : "مسموح",
                                                                   Is_Plant_Egypt = It.IsPlantInEgypt == true ? "نعم" : "لا",
                                                                   Is_Known_Item = It.Is_known_item == false ? "غير معروف" : "معروف",
                                                                   Picture = "" + It.Picture.ToString() + ""//
                                                               }).ToList();

                                    item_Infection.List_Im_CommitteeResult_Infection = committee_Infection;

                                    foreach (var item_Comm_Imge in committee_Infection)
                                    {
                                        var imagebinary = (from c in entities.A_AttachmentData_Im_CommitteeResult_Infection
                                                           where c.Im_CommitteeResult_id == item_Comm_Imge.Im_CommitteeResult_ID && c.AttachmentPath_Binary != null
                                                           select new General_List_AttachmentData_Im_CommitteeResult_Infection
                                                           {
                                                               AttachmentPath_Binary = c.AttachmentPath_Binary,


                                                           }).ToList();

                                        item_Comm.AttachmentData_Im_CommitteeResult_Infection = imagebinary;
                                    }
                                }
                                #endregion


                            }
                        }

                        // لجنة السحب
                        if (CommitteeTypeLst_ID == 0 || CommitteeTypeLst_ID == 13)
                        {
                            foreach (var item_Sample in catAndLots)
                            {
                                var committee_Sample = (from rc in entities.Im_RequestCommittee
                                                        join cr in entities.Im_CheckRequest_SampleData on rc.ID equals cr.Im_RequestCommittee_ID
                                                        join CE in entities.CommitteeEmployees on cr.Im_RequestCommittee_ID equals CE.Committee_ID into ComtEmp
                                                        from CEt in ComtEmp.DefaultIfEmpty()
                                                        where cr.LotData_ID == item_Sample.ID_Lot
                                                        // && rc.CommitteeType_ID == 11
                                                        && CEt.ISAdmin == true
                                                  //EslamMaher TO check opertion type of committee must be وارد 
                                                  && CEt.OperationType == 74
                                                  && CEt.User_Deletion_Date == null
                                                      && CEt.User_Deletion_Id == null
                                                        select new General_Committee_Sample_Lot
                                                        {
                                                            Committee_ID = rc.ID,
                                                            ID = cr.ID,
                                                            EmployeeId = CEt.Employee_Id,
                                                            AnalysisLabType_ID = cr.AnalysisLabType_ID,
                                                            Analysis_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisType.Name_Ar : cr.AnalysisLabType.AnalysisType.Name_En),
                                                            Lab_Name = (lang == "1" ? cr.AnalysisLabType.AnalysisLab.Name_Ar : cr.AnalysisLabType.AnalysisLab.Name_En),
                                                            Im_RequestCommittee_ID = cr.Im_RequestCommittee_ID,
                                                            Im_Request_Item_Id = cr.Im_Request_Item_Id,
                                                            LotData_ID = cr.LotData_ID,
                                                            WithdrawDate = cr.WithdrawDate,
                                                            Sample_BarCode = cr.Sample_BarCode,
                                                            SampleSize = cr.SampleSize,
                                                            SampleRatio = cr.SampleRatio,
                                                            //نتائج المعمل
                                                            IsAccepted = cr.IsAccepted,
                                                            Notes = (lang == "1" ? cr.Notes_Ar : cr.Notes_En),

                                                            RejectReason_Ar = cr.RejectReason_Ar,
                                                            RejectReason_En = cr.RejectReason_En,
                                                            Imge_SampleLabResult = entities.A_AttachmentData.Where(c => c.RowId == cr.ID && c.A_AttachmentTableNameId == 12).FirstOrDefault().AttachmentPath,


                                                            Admin_Confirmation = cr.Admin_Confirmation,
                                                            Admin_User = cr.Admin_User,
                                                            Admin_Date = cr.Admin_Date,
                                                            IsPrint = cr.IsPrint,

                                                            // عرض  فحص اللوطات
                                                            IS_Total = cr.IS_Total,
                                                            IS_TotalAndroid = cr.IS_Total_Android,
                                                            Syl_ALkhatima_Number = cr.Syl_ALkhatima_Number,
                                                            Delegation_Date = rc.Delegation_Date,
                                                            IsFinishedAll = rc.IsFinishedAll,
                                                            Status = rc.Status,
                                                        }).ToList();
                                try
                                {
                                    foreach (var iteme in committee_Sample)
                                    {
                                        if (iteme.EmployeeId != null)
                                        {
                                            short Employee_Comm_Id = short.Parse(iteme.EmployeeId.ToString());
                                            iteme.Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                                        }
                                    }
                                    //if (committee_Sample.Count > 0)
                                    //{
                                    //    if (committee_Sample.FirstOrDefault().EmployeeId != null)
                                    //    {
                                    //        short Employee_Comm_Id = short.Parse(committee_Sample.FirstOrDefault().EmployeeId.ToString());
                                    //        committee_Sample.FirstOrDefault().Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                                    //    }
                                    //}
                                }
                                catch (Exception)
                                {


                                }
                                // بيانات المساعد سحب

                                foreach (var item_Conferm in committee_Sample)
                                {
                                    var Sample_Conferm = (from cr in entities.Im_CheckRequest_SampleData
                                                          join CE in entities.CommitteeEmployees on cr.Im_RequestCommittee_ID equals CE.Committee_ID
                                                          join rc in entities.Im_CheckRequest_SampleData_Confirm on cr.ID equals rc.Im_CheckRequest_SampleData_ID
                                                          into emp_Conferm
                                                          from rc in emp_Conferm.Where(a => a.EmployeeId == CE.Employee_Id).DefaultIfEmpty()
                                                          where cr.Im_RequestCommittee_ID == item_Conferm.Committee_ID
                                                          && cr.LotData_ID == item_Conferm.LotData_ID
                                                          && CE.ISAdmin == false
                                                            && CE.OperationType == 74
                                                          //&& CE.User_Deletion_Date == null
                                                          //    && CE.User_Deletion_Id == null
                                                          select new General_Committee_Sample_Conferm
                                                          {
                                                              EmployeeId = CE.Employee_Id,
                                                              Notes = rc.Notes,
                                                              Date = rc.Date,
                                                              IsAccepted = rc.IsAccepted,
                                                              User_Deletion_Id = CE.User_Deletion_Id,
                                                              User_Deletion_Date = CE.User_Deletion_Date,
                                                          }).ToList();

                                    //var Sample_Conferm = (from CE in entities.CommitteeEmployees
                                    //                      join rc in entities.Im_CheckRequest_SampleData_Confirm on CE.Committee_ID equals rc.Im_CheckRequest_SampleData.Im_RequestCommittee_ID
                                    //                      into emp_Conferm
                                    //                      from rc in emp_Conferm.Where(a => a.EmployeeId == CE.Employee_Id).DefaultIfEmpty()


                                    //                      where rc.Im_CheckRequest_SampleData_ID == item_Conferm.ID
                                    //                      && CE.ISAdmin == false                                                  
                                    //                    && CE.OperationType == 74
                                    //                    && CE.User_Deletion_Date == null
                                    //                    && CE.User_Deletion_Id == null
                                    //                      select new Committee_Sample_Conferm
                                    //                      {
                                    //                          Date = rc.Date,
                                    //                          EmployeeId = CE.Employee_Id,
                                    //                          Notes = rc.Notes,
                                    //                          IsAccepted = rc.IsAccepted,
                                    //                      }).ToList();

                                    try
                                    {
                                        foreach (var iteme in Sample_Conferm)
                                        {
                                            if (iteme.EmployeeId != null)
                                            {
                                                short Employee_Comm_Id = short.Parse(iteme.EmployeeId.ToString());
                                                iteme.Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                                                if (iteme.User_Deletion_Id != null)
                                                {
                                                    short Employee_Comm_Conferm = short.Parse(iteme.User_Deletion_Id.ToString());
                                                    iteme.Employee_Name_Conferm = priv.PR_User.Where(c => c.Id == Employee_Comm_Conferm).FirstOrDefault().FullName;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    item_Conferm.List_Committee_Sample_Conferm = Sample_Conferm;
                                }

                                item_Sample.List_Lot_Committee_Sample = committee_Sample;
                            }
                        }
                        itm.ItemCategories_lots = catAndLots;


                        // لجنة المعالجة
                        if (CommitteeTypeLst_ID == 0 || CommitteeTypeLst_ID == 14)
                        {
                            foreach (var item_Treatment in catAndLots)
                            {
                                var committee_Treatment = (from rc in entities.Im_RequestCommittee
                                                           join cr in entities.Im_Request_TreatmentData on rc.ID equals cr.Im_RequestCommittee_ID
                                                           join CE in entities.CommitteeEmployees on rc.ID equals CE.Committee_ID into ComtEmp
                                                           from CEt in ComtEmp.DefaultIfEmpty()
                                                           where cr.Im_Request_LotData_ID == item_Treatment.ID_Lot
                                                           && CEt.ISAdmin == true
                                                     && CEt.OperationType == 74
                                                     && CEt.User_Deletion_Date == null
                                                     && CEt.User_Deletion_Id == null
                                                           select new General_List_Treatment_Data
                                                           {
                                                               Committee_ID = rc.ID,
                                                               ID = cr.ID,
                                                               EmployeeId = CEt.Employee_Id,
                                                               Im_RequestCommittee_ID = cr.Im_RequestCommittee_ID,
                                                               Im_Request_Item_Id = cr.Im_Request_Item_Id,
                                                               Im_Request_LotData_ID = cr.Im_Request_LotData_ID,
                                                               TreatmentType_ID = cr.TreatmentType_ID,
                                                               TreatmentType_Name = (lang == "1" ? entities.TreatmentTypes.Where(c => c.ID == cr.TreatmentType_ID).FirstOrDefault().Ar_Name :
                                                                   entities.TreatmentTypes.Where(c => c.ID == cr.TreatmentType_ID).FirstOrDefault().En_Name),
                                                               Procedures = cr.Procedures,
                                                               //cr.TreatmentMaterial.TreatmentMethod.TreatmentType.Ar_Name,
                                                               Company_ID = cr.Company_ID,
                                                               Company_Name = (lang == "1" ? entities.Company_National.Where(c => c.ID == cr.Company_ID).FirstOrDefault().Name_Ar :
                                                                   entities.TreatmentTypes.Where(c => c.ID == cr.TreatmentType_ID).FirstOrDefault().En_Name),

                                                               Station_ID = cr.Station_ID,
                                                               Station_Place = cr.Station_Place,
                                                               TreatmentMethod_ID = cr.TreatmentMethod_ID,
                                                               TreatmentMethod_Name = (lang == "1" ? entities.TreatmentMethods.Where(c => c.ID == cr.TreatmentMethod_ID).FirstOrDefault().Ar_Name :
                                                            entities.TreatmentMethods.Where(c => c.ID == cr.TreatmentMethod_ID).FirstOrDefault().En_Name),


                                                               // cr.TreatmentMaterial.TreatmentMethod.Ar_Name,
                                                               Exposure_Day = cr.Exposure_Day,
                                                               Exposure_Hour = cr.Exposure_Hour,
                                                               Exposure_Minute = cr.Exposure_Minute,
                                                               ThermalSealNumber = cr.ThermalSealNumber,
                                                               TreatmentMat_Name = cr.TreatmentMaterial.Item.Name_Ar,
                                                               Note = cr.Note,
                                                               Size = cr.Size,
                                                               TreatmentMat_Amount = cr.TreatmentMat_Amount,
                                                               Temperature = cr.Temperature,
                                                               TheDose = cr.TheDose,
                                                               Is_Cancel = rc.Is_Cancel,
                                                               IsFinishedAll = rc.IsFinishedAll,
                                                               IsApproved = rc.IsApproved,
                                                               Delegation_Date = rc.Delegation_Date,

                                                           }).ToList();
                                try
                                {
                                    foreach (var iteme in committee_Treatment)
                                    {
                                        if (iteme.EmployeeId != null)
                                        {
                                            short Employee_Comm_Id = short.Parse(iteme.EmployeeId.ToString());
                                            iteme.Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                                        }
                                    }

                                }
                                catch (Exception)
                                {


                                }
                                // بيانات المساعد معالجة

                                foreach (var item_Confirm in committee_Treatment)
                                {
                                    var Treatment_Confirm = (from cr in entities.Im_Request_TreatmentData
                                                             join CE in entities.CommitteeEmployees on cr.Im_RequestCommittee_ID equals CE.Committee_ID
                                                             join rc in entities.Im_Request_TreatmentData_Confirm on cr.ID equals rc.Im_Request_TreatmentData_ID
                                                             into emp_Conferm
                                                             from rc in emp_Conferm.Where(a => a.EmployeeId == CE.Employee_Id).DefaultIfEmpty()
                                                             where cr.Im_RequestCommittee_ID == item_Confirm.Committee_ID
                                                             && cr.Im_Request_LotData_ID == item_Confirm.Im_Request_LotData_ID
                                                             && CE.ISAdmin == false
                                                               && CE.OperationType == 74
                                                             select new General_List_Treatment_Data_Confirm
                                                             {
                                                                 EmployeeId = CE.Employee_Id,
                                                                 Notes = rc.Notes,
                                                                 Date = rc.Date,
                                                                 IsAccepted = rc.IsAccepted,
                                                                 User_Deletion_Id = CE.User_Deletion_Id,
                                                                 User_Deletion_Date = CE.User_Deletion_Date,
                                                             }).ToList();
                                    //var Treatment_Confirm = (from CE in entities.CommitteeEmployees
                                    //                         join rc in entities.Im_Request_TreatmentData_Confirm on CE.Committee_ID equals rc.Im_Request_TreatmentData.Im_RequestCommittee_ID
                                    //                         into emp_Conferm
                                    //                         from rc in emp_Conferm.Where(a => a.EmployeeId == CE.Employee_Id).DefaultIfEmpty()
                                    //                         where rc.Im_Request_TreatmentData_ID == item_Confirm.ID
                                    //                         && CE.ISAdmin == false && CE.OperationType == 74
                                    //                         && CE.User_Deletion_Date == null
                                    //                 && CE.User_Deletion_Id == null
                                    //                         select new List_Treatment_Data_Confirm
                                    //                         {
                                    //                             ID = rc.ID,
                                    //                             Date = rc.Date,
                                    //                             Notes = rc.Notes,
                                    //                             IsAccepted = rc.IsAccepted,
                                    //                             Im_Request_TreatmentData_ID = rc.Im_Request_TreatmentData_ID,
                                    //                             EmployeeId = rc.EmployeeId,
                                    //                             User_Deletion_Id = CE.User_Deletion_Id,
                                    //                             User_Deletion_Date = CE.User_Deletion_Date,

                                    //                         }).ToList();

                                    try
                                    {
                                        if (Treatment_Confirm.Count() > 0)
                                        {


                                            foreach (var iteme in Treatment_Confirm)
                                            {
                                                if (iteme.EmployeeId != null)
                                                {
                                                    short Employee_Comm_Id = short.Parse(iteme.EmployeeId.ToString());
                                                    iteme.Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                                                    if (iteme.User_Deletion_Id != null)
                                                    {
                                                        short Employee_Comm_Conferm = short.Parse(iteme.User_Deletion_Id.ToString());
                                                        iteme.Employee_Name_Conferm = priv.PR_User.Where(c => c.Id == Employee_Comm_Conferm).FirstOrDefault().FullName;
                                                    }
                                                }
                                            }
                                        }
                                        //foreach (var item in Treatment_Confirm)
                                        //{
                                        //    if (item.EmployeeId != null)
                                        //    {
                                        //        short Employee_Comm_Id = short.Parse(item.EmployeeId.ToString());
                                        //        item.Employee_Name = priv.PR_User.Where(c => c.Id == Employee_Comm_Id).FirstOrDefault().FullName;
                                        //    }
                                        //}
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    item_Confirm.List_Treatment_Confirm = Treatment_Confirm;
                                }

                                item_Treatment.List_Treatment = committee_Treatment;
                            }
                        }
                        itm.ItemCategories_lots = catAndLots;
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


        public Dictionary<string, object> FillItem_TypeDrop_Add(long req, List<string> Device_Info)
        {
            string lang = Device_Info[2];

            try
            {
                

                var CheckRequestDetails = (from cc in entities.Im_CheckRequest
                                           where cc.ID == req
                                           select new General_ImCommittee_Final_ResultDTO
                                           {
                                               Im_CheckRequest_ID = cc.ID,
                                               ImCheckRequest_Number = cc.CheckRequest_Number,
                                               OutLet_Name = cc.Outlet.Ar_Name,
                                               OutLet_ID = cc.Outlet.ID,
                                               StartDate = cc.User_Creation_Date

                                           }).FirstOrDefault();




                var itemss = (from im_i in entities.Im_CheckRequest_Items
                              join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
                              where im_i.Item_Permission_Number == CheckRequestDetails.ImCheckRequest_Number
                              group im_i by new
                              {
                                  im_i.Item_ShortName_ID,
                                  isn.ShortName_Ar,
                                  isn.ShortName_En,

                              } into grp
                              select new General_Items_checkReq_New
                              {
                                  Item_ShortName_ID = grp.Key.Item_ShortName_ID,
                                  ItemShortNameAr = grp.Key.ShortName_Ar,
                                  ItemShortNameEn = grp.Key.ShortName_En,

                              }).Distinct().ToList();

                //  itemss.Insert(0, new CustomOptionLongId { DisplayText = "-----اختار المنتج-----", Value = null });
                //       var lis = itemss.SelectMany(a => new CustomOptionLongId { DisplayText = "المنتج", Value = 0 }, A => new CustomOptionLongId { DisplayText = a.ItemShortNameAr, Value = a.Item_ShortName_ID });
                var lis = itemss.Select(a => new CustomOptionLongId { DisplayText = a.ItemShortNameAr, Value = a.Item_ShortName_ID });

                // itemss.Insert(0, new Items_checkReq_New() {  ItemShortNameAr= "-اختار المنتج" });
                // itemss.Insert(0, new cu { DisplayText = "-----اختار المنتج-----", Value = null });
                //var data = uow.Repository<lis>().GetData()
                //    .Where(a => a.Id != 2)
                //    .Select(c => new CustomOption
                //    { //change display lang
                //        DisplayText = (lang == "1" ? c.Name_Ar : c.Name_En),
                //        Value = c.Id
                //    }).OrderBy(a => a.Value).ToList();



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, lis);


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }






        }
        public Dictionary<string, object> FillLot_TypeDrop_Add(long Req, long ItemShortNameID, List<string> Device_Info)
        {

            string lang = Device_Info[2];
            try
            {
                var catAndLots = (from im_i in entities.Im_CheckRequest_Items
                                  join v in entities.Im_CheckRequest_Items_Lot_Category on im_i.ID equals v.Im_CheckRequest_Items_ID
                                  where im_i.Im_CheckRequset_Shipping_Method.Im_CheckRequest_ID == Req
                                  && im_i.Item_ShortName_ID == ItemShortNameID
                                  select new General_categories_lots_New
                                  {
                                      ID_Lot = v.ID,
                                      Lot_Number = v.Lot_Number,

                                  }).ToList();



                var lis = catAndLots.Select(a => new CustomOptionLongId { DisplayText = a.Lot_Number, Value = a.ID_Lot });


                //  var lis = catAndLots.Select(a => new CustomOptionLongId { DisplayText = a.ItemShortNameAr, Value = a.Item_ShortName_ID });

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, lis);


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, Device_Info);




            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }






        }

        public Dictionary<string, object> Insert_Lot_Result(General_Im_CheckRequest_Items_Lot_ResultDTO Lot_ResultList, List<string> Device_Info)
        {
            try
            {

                if (Lot_ResultList != null)
                {
                    //var checkResult = entities.Im_CheckRequest_Items_Lot_Result.Where(a => a.IS_Status == Lot_ResultList.IS_Status
                    // && a.Im_CheckRequest_Items_Lot_Category_ID == Lot_ResultList.Im_CheckRequest_Items_Lot_Category_ID).Count();
                    //if (checkResult == 0)
                    //{
                    var checkResult_Fales = uow.Repository<Im_CheckRequest_Items_Lot_Result>().GetData().
                        Where(a => a.Im_CheckRequest_Items_Lot_Category_ID == Lot_ResultList.Im_CheckRequest_Items_Lot_Category_ID).ToList();
                    foreach (var item in checkResult_Fales)
                    {
                        item.IS_Status_Committee = false;
                        uow.Repository<Im_CheckRequest_Items_Lot_Result>().Update(item);
                        uow.SaveChanges();
                    }


                    long Lot_ResultList_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CheckRequest_Items_Lot_Result_SEQ");
                    Lot_ResultList.ID = Lot_ResultList_ID;
                    //    item.im_RequestCommittee.ID = lotssss;
                    var Co = Mapper.Map<General_Im_CheckRequest_Items_Lot_ResultDTO, Im_CheckRequest_Items_Lot_Result>(Lot_ResultList);
                    uow.Repository<Im_CheckRequest_Items_Lot_Result>().InsertReturn(Co);
                    uow.SaveChanges();




                    #region log Action

                    Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                    dto2.ID_Table_Action = 16;
                    dto2.ID_TableActionValue = Lot_ResultList_ID;
                    dto2.User_Creation_Id = Lot_ResultList.User_Creation_Id;
                    dto2.User_Creation_Date = DateTime.Now;
                    dto2.NOTS = "حفظ موقف الحجر ";
                    dto2.User_Type_ID = 127;
                    dto2.Type_log_ID = 133;
                    Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                    x.save_CheckRequest_Log(dto2, Device_Info);

                    #endregion
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, Lot_ResultList);
                    //}
                    //else
                    //{
                    //    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, "No insert");
                    //}
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, Lot_ResultList);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Insert_Im_CheckRequest_Visa(General_Im_CheckRequest_VisaDTO CheckRequest_Visa, List<string> Device_Info)
        {
            try
            {
                if (CheckRequest_Visa != null)
                {
                    long Im_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CheckRequest_Visa_SEQ");
                    CheckRequest_Visa.ID = Im_CheckRequest_SampleData_ID;
                    //    item.im_RequestCommittee.ID = lotssss;
                    var Co = Mapper.Map<General_Im_CheckRequest_VisaDTO, Im_CheckRequest_Visa>(CheckRequest_Visa);
                    uow.Repository<Im_CheckRequest_Visa>().InsertReturn(Co);
                    uow.SaveChanges();

                    #region log Action

                    Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                    dto2.ID_Table_Action = 17;
                    dto2.ID_TableActionValue = Im_CheckRequest_SampleData_ID;
                    dto2.User_Creation_Id = CheckRequest_Visa.User_Creation_Id;
                    dto2.User_Creation_Date = DateTime.Now;
                    dto2.NOTS = "تم حفظ التأشيرة ";
                    dto2.User_Type_ID = 127;
                    dto2.Type_log_ID = 133;
                    Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                    x.save_CheckRequest_Log(dto2, Device_Info);

                    #endregion
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, CheckRequest_Visa);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> Insert_Im_CheckRequest_Final_Result(int Im_CheckRequest_Final_Result, General_Im_CheckRequest_Final_ResultDTO Im_CheckRequest_Final_ResultList, List<string> Device_Info)
        {
            try
            {
                if (Im_CheckRequest_Final_ResultList != null)
                {

                    long Im_CheckRequest_SampleData_ID = uow.Repository<Object>().GetNextSequenceValue_Long("Im_CheckRequest_Final_Result_SEQ");
                    Im_CheckRequest_Final_ResultList.ID = Im_CheckRequest_SampleData_ID;
                    //    item.im_RequestCommittee.ID = lotssss;
                    var Co = Mapper.Map<General_Im_CheckRequest_Final_ResultDTO, Im_CheckRequest_Final_Result>(Im_CheckRequest_Final_ResultList);
                    uow.Repository<Im_CheckRequest_Final_Result>().InsertReturn(Co);
                    uow.SaveChanges();


                    #region log Action

                    Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                    dto2.ID_Table_Action = 20;
                    dto2.ID_TableActionValue = Im_CheckRequest_SampleData_ID;
                    dto2.User_Creation_Id = Im_CheckRequest_Final_ResultList.User_Creation_Id;
                    dto2.User_Creation_Date = DateTime.Now;
                    dto2.NOTS = "حفظ الموقف النهائى ";
                    dto2.User_Type_ID = 127;
                    dto2.Type_log_ID = 133;
                    Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                    x.save_CheckRequest_Log(dto2, Device_Info);

                    #endregion
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, Im_CheckRequest_Final_ResultList);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //noura
        public Dictionary<string, object> FillVisaLabResultDrop_Edit(int VisaResult, List<string> Device_Info)
        {
            string lang = Device_Info[2];

            var data = uow.Repository<Im_Visa>().GetData()
                .Where(a => a.ID == VisaResult)
            .Select(c => new General_Im_Visa_DataDTO
            {
                ID = c.ID,
                Ar_Name = c.Ar_Name,
                Description_Ar = c.Description_Ar,

            }).FirstOrDefault();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, CheckRequestDetails);
            //return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data.OrderBy(a => a.DisplayText).ToList());
        }
        public Dictionary<string, object> FillLotStatusLst_AddEDIT(List<string> Device_Info)
        {
            string lang = Device_Info[2];

            var data = uow.Repository<Im_CheckRequest_Lot_Result_Status>().GetData()
                 .Select(c => new CustomOption
                 {

                     DisplayText = c.Name_AR + "/" + (c.Is_Continue == true ? "يوجد اجراء" : "لا يوجد اجراء اخر"),
                     Value = c.ID,
                     Value2 = c.Is_Continue
                 }).OrderBy(a => a.Value).ToList();

            data.Insert(0, new CustomOption() { DisplayText = "----------", Value = 0 });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);

        }


        public Dictionary<string, object> Get_Fees(string ImCheckRequest_Number, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var Fees_Dat = (from ch in entities.Im_CheckRequest
                                where ch.CheckRequest_Number == ImCheckRequest_Number
                                select new General_Fees_ALL
                                {
                                    ID = ch.ID,
                                    Fees_CheckRequest = ch.Amount,
                                    Is_Paid_Check = ch.IsPaid == true ? "تم الدفع" : "لم يتم الدفع "
                                    // Is_Paid_Check=ftd.Fees_Action_ID == 4 ? "تم الدفع" : "لم يتم الدفع "    
                                }).FirstOrDefault();


                //long ID_CheckRequest = long.Parse(Fees_Dat.ID.ToString());
                //var Fess_Check = (from ftd in entities.Fees_Transactions_Detiles
                //                  where ftd.Fees_Transactions.TableName_ID == 4
                //                  && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
                //                  && ftd.Fees_Action_ID == 4
                //                  select new Fees_ALL
                //                  {
                //                      Is_Paid_Check = ftd.Fees_Action_ID == 4 ? "تم الدفع" : "لم يتم الدفع "
                //                  }
                //           ).ToList();
                //if (Fess_Check.Count > 0)
                //{

                //    Fees_Dat.Is_Paid_Check = Fess_Check.FirstOrDefault().Is_Paid_Check;
                //}


                #region رسوم  النبات
                var item_Fees = (from im_i in entities.Im_CheckRequest_Items
                                 join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
                                 where im_i.Item_Permission_Number == ImCheckRequest_Number

                                 group im_i by new
                                 {
                                     id = im_i.ID,
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
                                     Is_Paid_Items = im_i.Im_CheckRequset_Shipping_Method.Im_CheckRequest.IsPaid == true ? "تم الدفع" : "لم يتم الدفع "
                                 } into grp
                                 select new General_Fees_Item_ALL
                                 {
                                     ID = grp.Key.id,
                                     Is_Paid_Items = grp.Key.Is_Paid_Items,
                                     ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,

                                     ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
                                     Fees = grp.Sum(q => q.Fees),
                                     GrossWeight = grp.Sum(q => q.GrossWeight) / 1000,
                                     Net_Weight = grp.Sum(q => q.Net_Weight),



                                     //Fees_Action_ID = grp.Key.Fees_Action_ID,
                                     //SUM_Shift_Fees_Item
                                     //Shift_Item_All
                                 }).Distinct().ToList();



                //foreach (var item in item_Fees)
                //{
                //    long ID_Item = long.Parse(item.ID.ToString());
                //    var ddd = (from ftd in entities.Fees_Transactions_Detiles
                //               where ftd.Items_ID == ID_Item
                //               && ftd.Fees_Transactions.TableName_ID == 4
                //               && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
                //               select new Fees_Item_ALL
                //               {
                //                   Is_Paid_Items = ftd.Items_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
                //               }
                //               ).ToList();
                //    if (ddd.Count > 0)
                //        item.Is_Paid_Items = ddd.FirstOrDefault().Is_Paid_Items;
                //    //else
                //    //    item.Is_Paid_Items = "";
                //}

                Fees_Dat.Fees_Item_ALL = item_Fees;
                #endregion

                //#region رسم  النوبتجية

                //var Fees_Item_Shift = (from sh in entities.Im_RequestCommittee_Shift

                //                       where sh.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number

                //                       && sh.Im_RequestCommittee.IsFinishedAll != null
                //                       group sh by new
                //                       {
                //                           ID = sh.ID,
                //                           Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,
                //                           Is_Paid_Shift = sh.Im_RequestCommittee.IsPaid == true ? "تم الدفع" : "لم يتم الدفع "
                //                       } into grp
                //                       select new List_Shift
                //                       {
                //                           ID = grp.Key.ID,
                //                           Is_Paid_Shift = grp.Key.Is_Paid_Shift,
                //                           Shift_Name = grp.Key.Shift_Name,
                //                           Shift_Count = grp.Sum(q => q.Count),
                //                           Shift_Amount = grp.Sum(q => q.Amount),
                //                           Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
                //                       }).Distinct().ToList();



                //Fees_Dat.List_Shift = Fees_Item_Shift;

                //#endregion


                #region رسم  النوبتجية
                // 
                var Fees_Item_Shift = (from sh in entities.Im_RequestCommittee_Shift

                                       where sh.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
                                       group sh by new
                                       {
                                           ID = sh.ID,
                                           Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,

                                       } into grp
                                       select new General_List_Shift
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
                                  select new General_List_Shift
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



                //#region رسوم السحب
                ////جزئى
                //var Fees_Sample = (from sm in entities.Im_CheckRequest_SampleData

                //                   where sm.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
                //                   && sm.Sample_BarCode!=null
                //                   group sm by new
                //                   {
                //                       ID = sm.ID,
                //                       Sample_BarCode = sm.Sample_BarCode,
                //                       Is_Total = sm.IS_Total,
                //                      // IS_Total_Android = sm.IS_Total_Android,
                //                       Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
                //                       Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,
                //                       Is_Paid_Sample=sm.Im_RequestCommittee.IsPaid == true ? "تم الدفع" : "لم يتم الدفع ",
                //                   } into grp
                //                   select new List_Sample
                //                   {

                //                       ID = grp.Key.ID,
                //                       Sample_BarCode = grp.Key.Sample_BarCode,
                //                       Laboratory_Name = grp.Key.Laboratory_Name,
                //                       Sample_Name = grp.Key.Sample_Name,
                //                       Is_Total = grp.Key.Is_Total == false ? "جزئي" : "كلي",
                //                       //IS_Total_Android = grp.Key.IS_Total_Android == false ? "جزئي" : "كلي",
                //                       Sample_Count = grp.Count(q => q.Sample_BarCode != null),
                //                       Sample_Amount = grp.Sum(c => c.Amount),
                //                       //Sample_Sum_All = grp.Sum(q => q.Sample_BarCode * q.Amount),
                //                       Is_Paid_Sample = grp.Key.Is_Paid_Sample ,

                //                   }).Distinct().ToList();
                //var Fees_Sample44 = Fees_Sample.GroupBy(a => a.Sample_BarCode).Select(a => a.First()).ToList();

                ////foreach (var item in Fees_Sample44)
                ////{
                ////    long ID_Item = long.Parse(item.ID.ToString());
                ////    var _Sample = (from ftd in entities.Fees_Transactions_Detiles

                ////                   where ftd.SampleData_ID == ID_Item
                ////                   && ftd.Fees_Transactions.TableName_ID == 4
                ////                   && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
                ////                   select new List_Sample
                ////                   {
                ////                       Is_Paid_Sample = ftd.SampleData_ID > 0 ? "تم الدفع" : "لم يتم الدفع "

                ////                   }
                ////               ).ToList();
                ////    if (_Sample.Count > 0)
                ////        item.Is_Paid_Sample = _Sample.FirstOrDefault().Is_Paid_Sample;
                ////    else
                ////        item.Is_Paid_Sample = null;
                ////}
                //Fees_Dat.List_Sample = Fees_Sample44;

                //#endregion


                #region   رسوم السحب




                var Fees_Sample = (from sm in entities.Im_CheckRequest_SampleData

                                   where sm.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
                                   group sm by new
                                   {
                                       //Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
                                       //Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,

                                       ID = sm.ID,
                                       Sample_BarCode = sm.Sample_BarCode,
                                       Is_Total = sm.IS_Total,
                                       Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
                                       Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,


                                   } into grp
                                   select new General_List_Sample
                                   {



                                       //Laboratory_Name = grp.Key.Laboratory_Name,
                                       //Sample_Name = grp.Key.Sample_Name,


                                       //Sample_Count = grp.Count(q => q.AnalysisLabType_ID != null),
                                       //Sample_Amount = 200,
                                       //Sample_Sum_All = grp.Count(q => q.AnalysisLabType_ID != null) * 200,


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
                                   select new General_List_Sample
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


                //CheckRequestDetails.List_Sample = Fees_Sample;

                #endregion

                #region رسوم المعالجة
                //جزئى
                var Fees_Treatment = (from td in entities.Im_Request_TreatmentData
                                      where td.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
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
                                          Is_Paid_Treatment = td.Im_RequestCommittee.IsPaid == true ? "تم الدفع" : "لم يتم الدفع"

                                      } into grp
                                      select new General_List_Treatment
                                      {
                                          ID = grp.Key.ID,
                                          Is_Paid_Treatment = grp.Key.Is_Paid_Treatment,
                                          TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
                                          TreatmentType_Name = grp.Key.TreatmentType_Name,
                                          //TreatmentMat_Amount = grp.Sum(c => c.TreatmentMat_Amount),
                                          Amount = grp.Sum(c => c.Amount),

                                      }).Distinct().ToList();
                //var Fees_Treatment2 = Fees_Treatment.GroupBy(a => a.ID).Select(a => a.First()).ToList();

                //foreach (var item in Fees_Treatment)
                //{
                //    long ID_Item = long.Parse(item.ID.ToString());
                //    var _Treatment = (from ftd in entities.Fees_Transactions_Detiles


                //                      where ftd.TreatmentData_ID == ID_Item
                //                      //&& ftd.Fees_Transactions.TableName_ID == 4
                //                      //&& ftd.TreatmentData_ID !=null                                     
                //                      select new List_Treatment
                //                      {
                //                          Treatment_Amount = ftd.Fees_Transactions.Amount_Total,
                //                          Is_Paid_Treatment = ftd.TreatmentData_ID > 0 ? "تم الدفع" : "لم يتم الدفع"
                //                      }
                //               ).ToList();
                //    if (_Treatment.Count > 0)
                //    {
                //        item.Is_Paid_Treatment = _Treatment.FirstOrDefault().Is_Paid_Treatment;
                //        item.Treatment_Amount = _Treatment.FirstOrDefault().Treatment_Amount;
                //    }
                //    else
                //    {
                //        item.Is_Paid_Treatment = null;
                //        item.Treatment_Amount = null;
                //    }
                //}
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
        //public Dictionary<string, object> Get_Fees(string ImCheckRequest_Number, List<string> Device_Info)
        //{
        //    try
        //    {
        //        PlantQuarantineEntities entities = new PlantQuarantineEntities();
        //        string lang = Device_Info[2];
        //        var Fees_Dat = (from ch in entities.Im_CheckRequest
        //                        where ch.CheckRequest_Number == ImCheckRequest_Number
        //                        select new Fees_ALL
        //                        {
        //                            ID = ch.ID,
        //                            Fees_CheckRequest = ch.Amount,
        //                            Is_Paid_Check = ch.IsPaid == true ? "تم الدفع" : "لم يتم الدفع "
        //                            // Is_Paid_Check=ftd.Fees_Action_ID == 4 ? "تم الدفع" : "لم يتم الدفع "    
        //                        }).FirstOrDefault();


        //        //long ID_CheckRequest = long.Parse(Fees_Dat.ID.ToString());
        //        //var Fess_Check = (from ftd in entities.Fees_Transactions_Detiles
        //        //                  where ftd.Fees_Transactions.TableName_ID == 4
        //        //                  && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
        //        //                  && ftd.Fees_Action_ID == 4
        //        //                  select new Fees_ALL
        //        //                  {
        //        //                      Is_Paid_Check = ftd.Fees_Action_ID == 4 ? "تم الدفع" : "لم يتم الدفع "
        //        //                  }
        //        //           ).ToList();
        //        //if (Fess_Check.Count > 0)
        //        //{

        //        //    Fees_Dat.Is_Paid_Check = Fess_Check.FirstOrDefault().Is_Paid_Check;
        //        //}


        //        #region رسوم  النبات
        //        var item_Fees = (from im_i in entities.Im_CheckRequest_Items
        //                         join isn in entities.Item_ShortName on im_i.Item_ShortName_ID equals isn.ID
        //                         where im_i.Item_Permission_Number == ImCheckRequest_Number

        //                         group im_i by new
        //                         {
        //                             id = im_i.ID,
        //                             im_i.Item_ShortName_ID,
        //                             isn.ShortName_Ar,
        //                             isn.ShortName_En,
        //                             Item_ID = isn.Item.ID,
        //                             ItemName_Ar = isn.Item.Name_Ar,
        //                             ItemName_En = isn.Item.Name_En,
        //                             qualitiveGroupName = isn.QualitativeGroup.Name_Ar,
        //                             qualitiveGroupNameEn = isn.QualitativeGroup.Name_En,
        //                             InitiatorCountry = im_i.Im_Initiator.Country.Ar_Name,
        //                             InitiatorCountryEn = im_i.Im_Initiator.Country.En_Name,
        //                             Is_Paid_Items = im_i.Im_CheckRequset_Shipping_Method.Im_CheckRequest.IsPaid == true ? "تم الدفع" : "لم يتم الدفع "
        //                         } into grp
        //                         select new Fees_Item_ALL
        //                         {
        //                             ID = grp.Key.id,
        //                             Is_Paid_Items = grp.Key.Is_Paid_Items,
        //                             ItemShortName = lang == "1" ? grp.Key.ShortName_Ar : grp.Key.ShortName_En,

        //                             ItemName = lang == "1" ? grp.Key.ItemName_Ar : grp.Key.ItemName_En,
        //                             Fees = grp.Sum(q => q.Fees),
        //                             GrossWeight = grp.Sum(q => q.GrossWeight) / 1000,
        //                             Net_Weight = grp.Sum(q => q.Net_Weight),



        //                             //Fees_Action_ID = grp.Key.Fees_Action_ID,
        //                             //SUM_Shift_Fees_Item
        //                             //Shift_Item_All
        //                         }).Distinct().ToList();



        //        //foreach (var item in item_Fees)
        //        //{
        //        //    long ID_Item = long.Parse(item.ID.ToString());
        //        //    var ddd = (from ftd in entities.Fees_Transactions_Detiles
        //        //               where ftd.Items_ID == ID_Item
        //        //               && ftd.Fees_Transactions.TableName_ID == 4
        //        //               && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
        //        //               select new Fees_Item_ALL
        //        //               {
        //        //                   Is_Paid_Items = ftd.Items_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
        //        //               }
        //        //               ).ToList();
        //        //    if (ddd.Count > 0)
        //        //        item.Is_Paid_Items = ddd.FirstOrDefault().Is_Paid_Items;
        //        //    //else
        //        //    //    item.Is_Paid_Items = "";
        //        //}

        //        Fees_Dat.Fees_Item_ALL = item_Fees;
        //        #endregion

        //        #region رسم  النوبتجية
        //        var Fees_Item_Shift = (from sh in entities.Im_RequestCommittee_Shift

        //                               where sh.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number

        //                               && sh.Im_RequestCommittee.IsFinishedAll != null
        //                               group sh by new
        //                               {
        //                                   ID = sh.ID,
        //                                   Shift_Name = lang == "1" ? sh.ShiftTiming.Name_Ar : sh.ShiftTiming.Name_En,
        //                                   Is_Paid_Shift = sh.Im_RequestCommittee.IsPaid == true ? "تم الدفع" : "لم يتم الدفع "
        //                               } into grp
        //                               select new List_Shift
        //                               {
        //                                   ID = grp.Key.ID,
        //                                   Is_Paid_Shift = grp.Key.Is_Paid_Shift,
        //                                   Shift_Name = grp.Key.Shift_Name,
        //                                   Shift_Count = grp.Sum(q => q.Count),
        //                                   Shift_Amount = grp.Sum(q => q.Amount),
        //                                   Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
        //                               }).Distinct().ToList();

        //        //foreach (var item in Fees_Item_Shift)
        //        //{
        //        //    long ID_Item = long.Parse(item.ID.ToString());
        //        //    var _Shift = (from ftd in entities.Fees_Transactions_Detiles

        //        //                  where ftd.Shift_ID == ID_Item
        //        //                  && ftd.Fees_Transactions.TableName_ID == 4
        //        //                  && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
        //        //                  select new List_Shift
        //        //                  {
        //        //                      Is_Paid_Shift = ftd.Shift_ID > 0 ? "تم الدفع" : "لم يتم الدفع "
        //        //                  }
        //        //               ).ToList();
        //        //    if (_Shift.Count > 0)
        //        //        item.Is_Paid_Shift = _Shift.FirstOrDefault().Is_Paid_Shift;
        //        //    else
        //        //        item.Is_Paid_Shift = null;
        //        //}

        //        Fees_Dat.List_Shift = Fees_Item_Shift;

        //        #endregion

        //        #region رسوم السحب
        //        //جزئى
        //        var Fees_Sample = (from sm in entities.Im_CheckRequest_SampleData

        //                           where sm.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
        //                           && sm.Sample_BarCode!=null
        //                           group sm by new
        //                           {
        //                               ID = sm.ID,
        //                               Sample_BarCode = sm.Sample_BarCode,
        //                               Is_Total = sm.IS_Total,
        //                              // IS_Total_Android = sm.IS_Total_Android,
        //                               Laboratory_Name = lang == "1" ? sm.AnalysisLabType.AnalysisLab.Name_Ar : sm.AnalysisLabType.AnalysisLab.Name_En,
        //                               Sample_Name = lang == "1" ? sm.AnalysisLabType.AnalysisType.Name_Ar : sm.AnalysisLabType.AnalysisType.Name_En,
        //                               Is_Paid_Sample=sm.Im_RequestCommittee.IsPaid == true ? "تم الدفع" : "لم يتم الدفع ",
        //                           } into grp
        //                           select new List_Sample
        //                           {

        //                               ID = grp.Key.ID,
        //                               Sample_BarCode = grp.Key.Sample_BarCode,
        //                               Laboratory_Name = grp.Key.Laboratory_Name,
        //                               Sample_Name = grp.Key.Sample_Name,
        //                               Is_Total = grp.Key.Is_Total == false ? "جزئي" : "كلي",
        //                               //IS_Total_Android = grp.Key.IS_Total_Android == false ? "جزئي" : "كلي",
        //                               Sample_Count = grp.Count(q => q.Sample_BarCode != null),
        //                               Sample_Amount = grp.Sum(c => c.Amount),
        //                               //Sample_Sum_All = grp.Sum(q => q.Sample_BarCode * q.Amount),
        //                               Is_Paid_Sample = grp.Key.Is_Paid_Sample ,

        //                           }).Distinct().ToList();
        //        var Fees_Sample44 = Fees_Sample.GroupBy(a => a.Sample_BarCode).Select(a => a.First()).ToList();

        //        //foreach (var item in Fees_Sample44)
        //        //{
        //        //    long ID_Item = long.Parse(item.ID.ToString());
        //        //    var _Sample = (from ftd in entities.Fees_Transactions_Detiles

        //        //                   where ftd.SampleData_ID == ID_Item
        //        //                   && ftd.Fees_Transactions.TableName_ID == 4
        //        //                   && ftd.Fees_Transactions.Table_ID == Fees_Dat.ID
        //        //                   select new List_Sample
        //        //                   {
        //        //                       Is_Paid_Sample = ftd.SampleData_ID > 0 ? "تم الدفع" : "لم يتم الدفع "

        //        //                   }
        //        //               ).ToList();
        //        //    if (_Sample.Count > 0)
        //        //        item.Is_Paid_Sample = _Sample.FirstOrDefault().Is_Paid_Sample;
        //        //    else
        //        //        item.Is_Paid_Sample = null;
        //        //}
        //        Fees_Dat.List_Sample = Fees_Sample44;

        //        #endregion

        //        #region رسوم المعالجة
        //        //جزئى
        //        var Fees_Treatment = (from td in entities.Im_Request_TreatmentData
        //                              where td.Im_RequestCommittee.Im_CheckRequest.CheckRequest_Number == ImCheckRequest_Number
        //                              && td.TreatmentMat_ID !=null

        //                              group td by new
        //                              {
        //                                  Is_Total = td.IS_Total,
        //                                  ID = td.ID,
        //                                  TreatmentMethod_Name = (lang == "1" ? entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().Ar_Name :
        //                                              entities.TreatmentMethods.Where(c => c.ID == td.TreatmentMethod_ID).FirstOrDefault().En_Name),
        //                                  TreatmentType_Name = (lang == "1" ? entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().Ar_Name :
        //                                              entities.TreatmentTypes.Where(c => c.ID == td.TreatmentType_ID).FirstOrDefault().En_Name),
        //                                  TreatmentMat_Name = (lang == "1" ? entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_Ar :
        //                                              entities.TreatmentMaterials.Where(c => c.ID == td.TreatmentMat_ID).FirstOrDefault().Item.Name_En),
        //                                  Is_Paid_Treatment = td.Im_RequestCommittee.IsPaid==true ? "تم الدفع" : "لم يتم الدفع"

        //                              } into grp
        //                              select new List_Treatment
        //                              {
        //                                  ID = grp.Key.ID,
        //                                  Is_Paid_Treatment = grp.Key.Is_Paid_Treatment,
        //                                  TreatmentMethod_Name = grp.Key.TreatmentMethod_Name,
        //                                  TreatmentType_Name = grp.Key.TreatmentType_Name,
        //                                  //TreatmentMat_Amount = grp.Sum(c => c.TreatmentMat_Amount),
        //                                  Amount = grp.Sum(c => c.Amount),

        //                              }).Distinct().ToList();
        //        //var Fees_Treatment2 = Fees_Treatment.GroupBy(a => a.ID).Select(a => a.First()).ToList();

        //        //foreach (var item in Fees_Treatment)
        //        //{
        //        //    long ID_Item = long.Parse(item.ID.ToString());
        //        //    var _Treatment = (from ftd in entities.Fees_Transactions_Detiles


        //        //                      where ftd.TreatmentData_ID == ID_Item
        //        //                      //&& ftd.Fees_Transactions.TableName_ID == 4
        //        //                      //&& ftd.TreatmentData_ID !=null                                     
        //        //                      select new List_Treatment
        //        //                      {
        //        //                          Treatment_Amount = ftd.Fees_Transactions.Amount_Total,
        //        //                          Is_Paid_Treatment = ftd.TreatmentData_ID > 0 ? "تم الدفع" : "لم يتم الدفع"
        //        //                      }
        //        //               ).ToList();
        //        //    if (_Treatment.Count > 0)
        //        //    {
        //        //        item.Is_Paid_Treatment = _Treatment.FirstOrDefault().Is_Paid_Treatment;
        //        //        item.Treatment_Amount = _Treatment.FirstOrDefault().Treatment_Amount;
        //        //    }
        //        //    else
        //        //    {
        //        //        item.Is_Paid_Treatment = null;
        //        //        item.Treatment_Amount = null;
        //        //    }
        //        //}
        //        Fees_Dat.List_Treatment = Fees_Treatment;

        //        #endregion


        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, Fees_Dat);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

        public Dictionary<string, object> Delete_Emp_Confirm(long Committee_ID, long Employee_ID, short UserId, List<string> Device_Info)
        {
            try
            {
                if (Committee_ID != null && Employee_ID != null) // الوارد
                {
                    var datafarm = uow.Repository<CommitteeEmployee>().GetData().
                        Where(a => a.Committee_ID == Committee_ID && a.Employee_Id == Employee_ID
                        && a.OperationType == 74 && a.ISAdmin == false).FirstOrDefault();
                    if (datafarm != null)
                    {
                        datafarm.User_Deletion_Date = DateTime.Now;
                        datafarm.User_Deletion_Id = UserId;
                        uow.Repository<CommitteeEmployee>().Update(datafarm);
                        uow.SaveChanges();
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "success");
                    }
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid Delete");

                    }
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid Delete");

                }

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }



        public Dictionary<string, object> Update_Status_Confirm(long Status_Id, bool Status, List<string> Device_Info)
        {
            try
            {
                if (Status_Id != null && Status != null) // الوارد
                {
                    Im_CheckRequest_Items_Lot_Result CModel = uow.Repository<Im_CheckRequest_Items_Lot_Result>().Findobject(Status_Id);
                    CModel.IS_Status_Committee = Status;
                    uow.Repository<Im_CheckRequest_Items_Lot_Result>().Update(CModel);
                    uow.SaveChanges();
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "succes");
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid Delete");
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
