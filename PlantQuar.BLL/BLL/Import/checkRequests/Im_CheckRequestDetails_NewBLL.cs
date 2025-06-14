using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Import.Permissions;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Import.checkRequests
{
    public class Im_CheckRequestDetails_NewBLL
    {
        private UnitOfWork uow;

        public Im_CheckRequestDetails_NewBLL()
        {
            uow = new UnitOfWork();
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
                                               OutLet_Name= cc.Outlet.Ar_Name,
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
                                           im_i.Item_ShortName_ID,
                                           isn.ShortName_Ar,
                                           isn.ShortName_En,
                                          Item_ID = isn.Item.ID,
                                          ItemName_Ar=isn.Item.Name_Ar,
                                          ItemName_En=   isn.Item.Name_En,
                                          qualitiveGroupName=  isn.QualitativeGroup.Name_Ar,
                                          qualitiveGroupNameEn= isn.QualitativeGroup.Name_En,
                                          InitiatorCountry=im_i.Im_Initiator.Country.Ar_Name,
                                          InitiatorCountryEn=  im_i.Im_Initiator.Country.En_Name,
                                      } into grp
                                      select new Items_checkReq_New
                                      {
                                          Item_ShortName_ID = grp.Key.Item_ShortName_ID,
                                          ItemShortNameAr = grp.Key.ShortName_Ar,
                                          ItemShortNameEn = grp.Key.ShortName_En,
                                          Item_ID = grp.Key.Item_ID ,
                                          ItemName_Ar = grp.Key.ItemName_Ar,
                                          ItemName_En = grp.Key.ItemName_En,
                                          qualitiveGroupName = grp.Key.qualitiveGroupName,
                                          qualitiveGroupNameEn = grp.Key.qualitiveGroupNameEn,
                                          InitiatorCountry = grp.Key.InitiatorCountry,
                                          InitiatorCountryEn= grp.Key.InitiatorCountryEn,
                                          GrossWeight= grp.Sum(q => q.GrossWeight),
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
                                              //join im_cr in entities.Im_CommitteeResult on v.ID equals im_cr.LotData_ID into cr
                                              //from p in cr.DefaultIfEmpty()
                                              where im_i.Item_Permission_Number == ImCheckRequest_Number && im_i.Item_ShortName_ID == itm.Item_ShortName_ID
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
                                                  ID_IM_Item =v.Im_CheckRequest_Items.ID,
                                                  //باقى ابعت بيانات الحاوية
                                                  ContainerNumber =v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.ContainerNumber,
                                                  //containers_ID = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_ID,
                                                  //containers_type_ID = v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_type_ID,
                                                  containerName = entities.A_SystemCode.Where(c => c.Id == v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_ID).FirstOrDefault().ValueName,
                                                  containerType = entities.A_SystemCode.Where(c => c.Id == v.Im_CheckRequest_Items.Im_CheckRequset_Shipping_Method.containers_type_ID && c.SystemCodeTypeId == 29).FirstOrDefault().ValueName,

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

                                              }).ToList();

                        itm.ItemCategories_lots = catAndLots;
                        foreach (var item in catAndLots)
                        {
                            var dd = (from icr in entities.Im_CommitteeResult
                                      where icr.LotData_ID == item.ID_Lot
                                      group icr by new
                                      {
                                          Delegation_Date=  icr.Im_RequestCommittee.Delegation_Date,
                                          CommitteeResultType_ID=icr.CommitteeResultType_ID    ,
                                          Check_Lot_Old_Name = icr.LotData_ID > 0 ? icr.CommitteeResultType_ID > 0 ? "تم عمل الاندريد" : "تم التشكيل ولم يتم الاندريد" : "لم يتم العمل عليه",
                                      } into grp
                                      select new categories_lots_New
                                      {
                                          Im_CommitteeResult_ID =grp.Max(a=>a.ID),
                                          Delegation_Date = grp.Key.Delegation_Date,
                                          CommitteeResultType_ID = grp.Key.CommitteeResultType_ID,
                                    
                                      }).FirstOrDefault();
                            if (dd != null)
                            {
                                item.Delegation_Date = dd.Delegation_Date;
                                item.Im_CommitteeResult_ID = dd.Im_CommitteeResult_ID;
                                item.CommitteeResultType_ID = dd.CommitteeResultType_ID;
                            }
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
        //approve request
       
    }
}
