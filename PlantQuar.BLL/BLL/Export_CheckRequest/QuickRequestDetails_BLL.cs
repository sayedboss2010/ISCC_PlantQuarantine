using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PlantQuar.UOW.UnitOfWork;
using System.Runtime.ConstrainedExecution;
using PlantQuar.DTO.DTO.ExportRequest;
using Privilages.DAL;

namespace PlantQuar.BLL.BLL.Export_CheckRequest
{
    public class QuickRequestDetails_BLL
    {
        private UnitOfWork uow;
        private dbPrivilageEntities priv = new dbPrivilageEntities();
        public QuickRequestDetails_BLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> QuickGetExCheckRequestDetails(string Ex_CheckRequest_Number, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];

                var QuickCheckRequests = (from ecr in entities.Ex_CheckRequest
                                              //join ecri in entities.Ex_CheckRequest_Items on ecr.ID equals ecri.Ex_CheckRequest_ID
                                          join ecrd in entities.Ex_CheckRequest_Data on ecr.ID equals ecrd.Ex_CheckRequest_ID into ecrd2
                                          from ecrd in ecrd2.DefaultIfEmpty()

                                          join eccm in entities.Ex_CheckRequest_Customs_Message on ecr.ID equals eccm.Ex_CheckRequest_ID into eccm2
                                          from eccm in eccm2.DefaultIfEmpty()

                                          join cn in entities.Company_National on new { a = (long)ecrd.Importer_ID, b = (int)ecrd.ImporterType_Id } equals new { a = cn.ID, b = 6 } into cn2
                                          from cn in cn2.DefaultIfEmpty()
                                          join po in entities.Public_Organization on new { a = (long)ecrd.Importer_ID, b = (int)ecrd.ImporterType_Id } equals new { a = po.ID, b = 7 } into po2
                                          from po in po2.DefaultIfEmpty()
                                          join prn in entities.People on new { a = (long)ecrd.Importer_ID, b = (int)ecrd.ImporterType_Id } equals new { a = prn.ID, b = 8 } into prn2
                                          from prn in prn2.DefaultIfEmpty()



                                          join c in entities.Countries on ecrd.ExportCountry_Id equals c.ID into c2
                                          from c in c2.DefaultIfEmpty()
                                          where ecr.CheckRequest_Number == Ex_CheckRequest_Number
                                          select new QuickRequestDetailsDTO
                                          {

                                              ImporterType_Id = ecrd.ImporterType_Id,
                                              Amount = ecr.Amount,
                                              ImporterName = ecrd.ImporterType_Id == 6 ? (lang == "1" ? cn.Name_Ar : cn.Name_En) :
                                                                    ecrd.ImporterType_Id == 7 ? (lang == "1" ? po.Name_Ar : po.Name_En) :
                                                                     ecrd.ImporterType_Id == 8 ? (lang == "1" ? prn.Name : prn.Name_EN) : "",
                                              CompanyAddress = ecrd.ImporterType_Id == 6 ? (lang == "1" ? cn.Address_Ar : cn.Address_En) :
                                                                    ecrd.ImporterType_Id == 7 ? (lang == "1" ? po.Address_Ar : po.Address_En) :
                                                                     ecrd.ImporterType_Id == 8 ? (lang == "1" ? prn.Address : prn.Address_EN) : "",

                                              QuickCommitteList = (from rc in entities.Ex_RequestCommittee
                                                                       //join cr in entities.Ex_CommitteeResult on rc.ID equals cr.Committee_ID into c1
                                                                       //from cr in c1.DefaultIfEmpty()
                                                                   where rc.Ex_CheckRequest.ID == ecr.ID  && rc.User_Deletion_Id == null //&& rc.CommitteeType_ID == 1
                                                                   select new QuickCommitteList
                                                                   {
                                                                       // ResultTypes_Name = (lang == "1" ? cr.CommitteeResultType.Name_Ar : cr.CommitteeResultType.Name_En),
                                                                       Delegation_Date = rc.Delegation_Date,
                                                                       CreationDate = rc.User_Creation_Date,

                                                                       StartTime = rc.StartTime,
                                                                       EndTime = rc.EndTime,
                                                                       IsApproved = rc.IsApproved,
                                                                       IsFinishedAll = rc.IsFinishedAll,
                                                                       Status = rc.Status,
                                                                       // Date = cr.Date,
                                                                       //Notes = cr.Notes,
                                                                       Committee_ID = rc.ID,
                                                                       //Is_Result_Finch = cr.CommitteeResultType_ID == null ? false : true,
                                                                       // عرض  فحص اللوطات
                                                                       //IS_Total = cr.IS_Total,
                                                                       //   IS_TotalAndroid = cr.IS_Total_Android,
                                                                       QuickcommitteeEmployee_Name = (//from pru in priv.PR_User
                                                                       from CE in entities.CommitteeEmployees

                                                                       where rc.CommitteeType_ID == 1 && rc.User_Deletion_Id == null && CE.OperationType == 73 && CE.Committee_ID == rc.ID && CE.ISAdmin == true//&& pru.Id==CE.Employee_Id
                                                                       select new QuickcommitteeEmployee_Name
                                                                       {
                                                                           Employee_Id = CE.Employee_Id,
                                                                           ISAdmin = CE.ISAdmin,
                                                                           // Employee_Name  = string.IsNullOrEmpty(pru.FullName) ? pru.FullName : "لا يوجد"//priv.PR_User.Where(c => c.Id == CE.Employee_Id).FirstOrDefault().FullName
                                                                       }).ToList(),
                                                                       QuickList_Shift = (from sh in entities.Ex_RequestCommittee_Shift
                                                                                          where sh.Ex_RequestCommittee.ID == 100
                                                                                          && sh.Ex_RequestCommittee.IsApproved == true
                                                                                          group sh by new
                                                                                          {
                                                                                              ID = sh.ID,
                                                                                              sh.Ex_RequestCommittee.User_Creation_Date,
                                                                                          } into grp
                                                                                          select new QuickList_Shift
                                                                                          {
                                                                                              User_Creation_Date = grp.Key.User_Creation_Date,
                                                                                              Shift_Sum_All = grp.Sum(q => q.Count * q.Amount),
                                                                                          }).Distinct().ToList(),

                                                                       QuickcommitteeEmployee_NameConfirm = (//from pru in priv.PR_User
                                                                                                             from CE in entities.CommitteeEmployees
                                                                                                             where rc.CommitteeType_ID == 1 && rc.User_Deletion_Id == null && CE.OperationType == 73 && CE.Committee_ID == rc.ID && CE.ISAdmin == false //&& pru.Id == CE.Employee_Id
                                                                                                             select new QuickcommitteeEmployee_NameConfirm
                                                                                                             {
                                                                                                                 Employee_Id = CE.Employee_Id,
                                                                                                                 ISAdmin = CE.ISAdmin,// = lang == "1" ? "مصر" : "Egypt",
                                                                                                                                      //  Employee_NameConfirm = string.IsNullOrEmpty(pru.FullName) ? pru.FullName : "لا يوجد"//priv.PR_User.Where(c => c.Id == CE.Employee_Id).FirstOrDefault().FullName
                                                                                                             }).ToList(),
                                                                   }).ToList(),

                                              QuickList_Port = (from icrp in entities.Ex_CheckRequest_Port
                                                                join pn1 in entities.PortNationals on new { a = (int)icrp.Port_ID, b = icrp.ReqPortType_ID } equals new { a = pn1.ID, b = 9 } into pn1
                                                                from pn in pn1.DefaultIfEmpty()

                                                                join pi9 in entities.Port_International on new { a = (int)icrp.Port_ID, b = icrp.ReqPortType_ID } equals new { a = pi9.ID, b = 10 } into pi9_1
                                                                from pi9 in pi9_1.DefaultIfEmpty()

                                                                join pi11_1 in entities.Port_International on new { a = (int)icrp.Port_ID, b = icrp.ReqPortType_ID } equals new { a = pi11_1.ID, b = 11 } into pi11_1
                                                                from pi11 in pi11_1.DefaultIfEmpty()

                                                                join c9_1 in entities.Countries on pi9.Country_ID equals c9_1.ID into c9_1
                                                                from c9 in c9_1.DefaultIfEmpty()
                                                                join c11_1 in entities.Countries on pi11.Country_ID equals c11_1.ID into c11_1
                                                                from c11 in c11_1.DefaultIfEmpty()
                                                                join pt_1 in entities.Port_Type on icrp.Port_Type_ID equals pt_1.ID into pt_1
                                                                from pt in pt_1.DefaultIfEmpty()
                                                                where icrp.Ex_CheckRequest_Data_ID == ecrd.ID
                                                                select new QuickList_Port
                                                                {
                                                                    ReqPortType_ID = icrp.ReqPortType_ID,
                                                                    // الدولة الوصول 
                                                                    ExportCountryName = lang == "1" ? "مصر" : "Egypt",
                                                                    TransportPortType = lang == "1" ? pt.Name_Ar : pt.Name_En,
                                                                    TransportPortName = lang == "1" ? pi9.Name_Ar : pi9.Name_En,
                                                                    //العبور
                                                                    TransitCountry = lang == "1" ? c11.Ar_Name : c11.En_Name,
                                                                    TransitPortType = lang == "1" ? pt.Name_Ar : pt.Name_En,
                                                                    TransitPort = lang == "1" ? pi11.Name_Ar : pi11.Name_En,
                                                                    //المصدرة
                                                                    ArrivePortType = lang == "1" ? pt.Name_Ar : pt.Name_En,
                                                                    ArrivePortName = lang == "1" ? pn.Name_Ar : pn.Name_En,

                                                                }).ToList(),

                                              ItemEx_CheckRequest = (from i in entities.Ex_CheckRequest_Items
                                                                     where i.Ex_CheckRequest_ID == ecr.ID
                                                                     select new ItemEx_CheckRequestDTO
                                                                     {
                                                                         ItemNameAr = lang == "1" ? i.Item_ShortName.Item.Name_Ar : i.Item_ShortName.Item.Name_En,
                                                                         ItemShortNameAr = lang == "1" ? i.Item_ShortName.ShortName_Ar : i.Item_ShortName.ShortName_En,
                                                                         GrossWeight = i.GrossWeight,
                                                                         NetWeight = i.Net_Weight,
                                                                         Agriculture_Hand = i.Agriculture_Hand,
                                                                         item_lots = (from lot in entities.Ex_CheckRequest_Items_Lot_Category
                                                                                      join pt in entities.Package_Type on lot.Package_Type_ID equals pt.ID
                                                                                      where lot.Ex_CheckRequest_Items_ID == i.ID
                                                                                      group lot by new
                                                                                      {
                                                                                          itemId = lot.Ex_CheckRequest_Items_ID,

                                                                                          pt.Ar_Name,
                                                                                          //lot.Net_Weight
                                                                                          // lot.Lot_Number
                                                                                      } into grp
                                                                                      select new Item_lots
                                                                                      {
                                                                                          packageType = grp.Key.Ar_Name,
                                                                                          Net_Weight_Final = grp.Sum(a => a.Net_Weight),
                                                                                          Gross_Weight_Final = grp.Sum(a => a.GrossWeight),
                                                                                          Package_Count = grp.Sum(a => a.Package_Count),
                                                                                          //Lot_Number=grp.Key.Lot_Number
                                                                                          //LotCount=grp.Count(a=>a.itemId)

                                                                                      }).ToList(),
                                                                         item_lots2 = (from lot in entities.Ex_CheckRequest_Items_Lot_Category
                                                                                       join pt in entities.Package_Type on lot.Package_Type_ID equals pt.ID
                                                                                       where lot.Ex_CheckRequest_Items_ID == i.ID

                                                                                       select new Item_lots2
                                                                                       {

                                                                                           Lot_Number = lot.Lot_Number,



                                                                                       }).ToList(),
                                                                     }).ToList(),

                                              ExportCountryName = c.Ar_Name,

                                              //Comittee check


                                              //Farm_Agriculture_Hand = ecri.Agriculture_Hand,
                                              Ship_Name = ecrd.Ship_Name,
                                              Shipment_Date = eccm.Shipment_Date,
                                              ExportCompany = ecr.ExportCompany,
                                              ExportCompanyAddress = ecr.ExportCompanyAddress,
                                              User_Creation_Date = ecr.User_Creation_Date,

                                          }).ToList();


                string admin = ""; string confirm = ""; var fullname = "";
                if (QuickCheckRequests.Count() > 0 && QuickCheckRequests.FirstOrDefault().QuickCommitteList != null && QuickCheckRequests.FirstOrDefault().QuickCommitteList.Count() > 0)
                {
                    if (QuickCheckRequests.FirstOrDefault().QuickCommitteList.FirstOrDefault().QuickcommitteeEmployee_Name != null && QuickCheckRequests.FirstOrDefault().QuickCommitteList.FirstOrDefault().QuickcommitteeEmployee_Name.Count() > 0)
                    {




                        foreach (var item in QuickCheckRequests.FirstOrDefault().QuickCommitteList.FirstOrDefault().QuickcommitteeEmployee_Name)
                        {
                            admin = priv.PR_User.Where(c => c.Id == item.Employee_Id).FirstOrDefault().FullName;
                        }
                        foreach (var item in QuickCheckRequests.FirstOrDefault().QuickCommitteList.FirstOrDefault().QuickcommitteeEmployee_NameConfirm)
                        {
                            try
                            {
                                confirm = priv.PR_User.Where(c => c.Id == item.Employee_Id).FirstOrDefault().FullName;

                            }
                            catch (Exception)
                            {
                                throw new Exception();


                            }


                        }

                        fullname = "الأدمن :-" + admin +
                           " المساعد :-" + confirm + ".";




                        QuickCheckRequests.FirstOrDefault().QuickCommitteList.FirstOrDefault().committeeFullEmployee_Name = fullname;
                    }
                }
                else
                {
                    QuickCheckRequests.FirstOrDefault().CommitteeNotFound = "لا يوجد لجان";
                }
                //var committee = (from rc in entities.Ex_RequestCommittee
                //                 join cr in entities.Ex_CommitteeResult on rc.ID equals cr.Committee_ID into c1
                //                 from cr in c1.DefaultIfEmpty()
                //                 join Lot_R in entities.Ex_CheckRequest_Items_Lot_Category on cr.LotData_ID equals Lot_R.ID into c2
                //                 from Lot_R in c2.DefaultIfEmpty()
                //                     // join CE in entities.CommitteeEmployees on rc.ID equals CE.Committee_ID
                //                 where rc.Ex_CheckRequest.ID == QuickCheckRequests.FirstOrDefault().Ex_CheckRequest_ID
                //                    && rc.CommitteeType_ID == 1
                //                      && rc.User_Deletion_Id == null
                //                 // && CE.OperationType == 73

                //                 select new Committee_Lot_AcceptCertificate
                //                 {
                //                     Lot_Number = Lot_R.Lot_Number,
                //                     ResultTypes_Name = (lang == "1" ? cr.CommitteeResultType.Name_Ar : cr.CommitteeResultType.Name_En),
                //                     //EmployeeId = CE.Employee_Id,
                //                     Delegation_Date = rc.Delegation_Date,
                //                     StartTime = rc.StartTime,
                //                     EndTime = rc.EndTime,
                //                     IsApproved = rc.IsApproved,
                //                     IsFinishedAll = rc.IsFinishedAll,
                //                     Status = rc.Status,
                //                     Date = cr.Date,
                //                     Notes = cr.Notes,
                //                     //ISAdmin = CE.ISAdmin,
                //                     Committee_ID = rc.ID,
                //                     Is_Result_Finch = cr.CommitteeResultType_ID == null ? false : true,
                //                     // عرض  فحص اللوطات
                //                     IS_Total = cr.IS_Total,
                //                     IS_TotalAndroid = cr.IS_Total_Android,
                //                 }).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, QuickCheckRequests);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}