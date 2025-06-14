using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.Print;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.Print
{

    public class Farm_PrintBLL
    {
        private UnitOfWork uow;

        public Farm_PrintBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetFarm_Data_PrintDetails(long Farm_Data_ID,short User_Creation_Id, long RequestId, List<string> Device_Info) 
        {
            try
            {
               
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var FarmPrintDetails = (from fd in entities.FarmsDatas
                                         join fc in entities.Farm_Company on fd.ID equals fc.Farm_ID
                                         join i in entities.Items on fd.Item_ID equals i.ID
                                              where fd.ID == Farm_Data_ID
                                              select new Farm_Data_Print_DTO
                                              {
                                                  Farm_Data_ID = fd.ID,
                                                  Farm_Company_ExporterType_Id = fc.ExporterType_Id,
                                                  Farm_Company_Company_ID = fc.Company_ID,
                                                  Farm_Name_AR = fd.Name_Ar,
                                                  Farm_Name_EN = fd.Name_En,

                                                  Farm_Address_Ar = fd.Address_Ar,
                                                  Farm_Address_En = fd.Address_En,
                                                  Farm_Data_FarmCode_14 = fd.FarmCode_14,
                                                  Item_ID = i.ID,
                                               
                                                  Item_Name_AR = i.Name_Ar ,
                                                  Item_Name_EN = i.Name_En,
                                              }).FirstOrDefault();
                //username
                dbPrivilageEntities priv = new dbPrivilageEntities();
                var user = priv.PR_User.Where(p => p.Id == User_Creation_Id).FirstOrDefault();
                FarmPrintDetails.UserName = user.FullName;
                var outlet = uow.Repository<Outlet>().GetData().FirstOrDefault(o => o.ID_HR == user.Outlet_ID);
                if (outlet != null)
                {
                    FarmPrintDetails.outletName = outlet.Ar_Name ;
                    FarmPrintDetails.outletName_En = outlet.En_Name;
                }
                #region  الشركات- هيئات- افراد

                if (FarmPrintDetails.Farm_Company_ExporterType_Id == 6)
                {
                    var comp = uow.Repository<Company_National>().GetData()//.Include(c => c.Ex_ContactData.ItemCategory)
                        .Where(a => a.ID == FarmPrintDetails.Farm_Company_Company_ID)
                        .FirstOrDefault();
                    if (comp != null)
                    {
                        FarmPrintDetails.CompanyName_Ar = comp.Name_Ar;
                        FarmPrintDetails.CompanyName_En= comp.Name_En;
                        FarmPrintDetails.CompanyAddress_Ar = comp.Address_Ar;
                        FarmPrintDetails.CompanyAddress_En = comp.Address_En;
                        var compContactdata = uow.Repository<Ex_ContactData>().GetData().Where(c => c.ExporterType_Id == 6 && c.Exporter_ID == comp.ID ).ToList();

                        if(compContactdata.Count>0)
                        {
                            if(compContactdata.FirstOrDefault(m=>m.ContactType_ID==1) != null)
                            {
                                FarmPrintDetails.Company_Mobile = compContactdata.FirstOrDefault(m => m.ContactType_ID == 1).Value;
                            }
                            if (compContactdata.FirstOrDefault(m => m.ContactType_ID == 3) != null)
                            {
                                FarmPrintDetails.Company_Email = compContactdata.FirstOrDefault(m => m.ContactType_ID == 3).Value;
                            }

                        }
                    }
                }
                else if (FarmPrintDetails.Farm_Company_ExporterType_Id == 7)
                {
                    var pup = uow.Repository<Public_Organization>().GetData()
                        .Where(a => a.ID == FarmPrintDetails.Farm_Company_Company_ID).FirstOrDefault();
                    FarmPrintDetails.CompanyName_Ar = pup.Name_Ar;
                    FarmPrintDetails.CompanyName_En = pup.Name_En;
                    FarmPrintDetails.CompanyAddress_Ar = pup.Address_Ar;
                    FarmPrintDetails.CompanyAddress_En = pup.Address_En;
                    var pupContactdata = uow.Repository<Ex_ContactData>().GetData().Where(c => c.ExporterType_Id == 7 && c.Exporter_ID == pup.ID).ToList();

                    if (pupContactdata.Count > 0)
                    {
                        if (pupContactdata.FirstOrDefault(m => m.ContactType_ID == 1) != null)
                        {
                            FarmPrintDetails.Company_Mobile = pupContactdata.FirstOrDefault(m => m.ContactType_ID == 1).Value;
                        }
                        if (pupContactdata.FirstOrDefault(m => m.ContactType_ID == 3) != null)
                        {
                            FarmPrintDetails.Company_Email = pupContactdata.FirstOrDefault(m => m.ContactType_ID == 3).Value;
                        }

                    }
                }
                else
                {
                    var per = uow.Repository<Person>().GetData().Where(a => a.ID == FarmPrintDetails.Farm_Company_Company_ID).FirstOrDefault();
                    FarmPrintDetails.CompanyName_Ar = per.Name;
                    FarmPrintDetails.CompanyName_En = per.Name;
                    FarmPrintDetails.CompanyAddress_Ar = per.Address;
                    FarmPrintDetails.CompanyAddress_En = per.Address;

                    FarmPrintDetails.Company_Mobile = per.Phone.ToString();
                    FarmPrintDetails.Company_Email = per.Email;
                }

                //if (FarmPrintDetails.Farm_Company_ExporterType_Id == 6 || FarmPrintDetails.Farm_Company_ExporterType_Id == 7)
                //{
                //    var _ContactData = uow.Repository<Ex_ContactData>().GetData()
                //            .Where(a => a.ID == FarmPrintDetails.Farm_Company_Company_ID)
                //            .FirstOrDefault();
                //    //??? مرة موبيل1 ومره ايميل3
                //    FarmPrintDetails.Company_Email = _ContactData.ContactType_ID == 3 ? _ContactData.Value : "#######";
                //    FarmPrintDetails.Company_Mobile = _ContactData.ContactType_ID == 1 ? _ContactData.Value : "#######";
                //}
                #endregion

                var plantList = uow.Repository<Farm_Request_ItemCategories>().GetData().Include(c => c.Farm_ItemCategories.ItemCategory)
                    .Where(a => a.User_Deletion_Id == null 
                    && a.Farm_ItemCategories.Farm_ID == Farm_Data_ID
                    && a.Farm_Request_ID ==  RequestId).
                    Select(a => new Farm_Request_ItemCategoriesDTO
                    {
                        ID = a.ID,
                        Farm_ItemCategories_ID = a.Farm_ItemCategories_ID,
                        categoryName_Ar = a.Farm_ItemCategories.ItemCategory.Name_Ar,
                        categoryName_En = a.Farm_ItemCategories.ItemCategory.Name_En,
                        //categoryName = (lang == "1" ? a.Farm_ItemCategories.ItemCategory.Name_Ar : a.Farm_ItemCategories.ItemCategory.Name_En),
                        Area_Acres = a.Area_Acres_Quarant,
                        Quantity_Ton = a.Area_Acres_Quarant * a.Quantity_Ton__Quarant,
                        StartDate = a.Farm_Committee_Examination.FirstOrDefault().StartDate,
                        EndDate = a.Farm_Committee_Examination.FirstOrDefault().EndDate,
                        IsActive = a.IsActive,
                        //ss
                        //ItemCategories_Group_ID = a.ItemCategory.ItemCategories_Group_ID

                    }).ToList();
                
                FarmPrintDetails.plantList = plantList;

                var requestLst = uow.Repository<Farm_Request>().GetData().
                   Where(a => a.FarmsData_ID == Farm_Data_ID
                   && a.User_Deletion_Id == null
                   &&a.ID ==  RequestId).
                   Select(a => new FarmRequestDTO
                   {
                       ID = a.ID,
                       IsAcceppted = a.IsAcceppted,
                       IsActive = a.IsActive,
                       Start_Date = a.Start_Date,
                       End_Date = a.End_Date,
                       IsPaid = a.IsPaid,
                       Print_Text = a.Print_Text
                   }).ToList();


                FarmPrintDetails.requestLst= requestLst;
                //foreach (var request in requestLst)
                //{
                //    FarmPrintDetails.countryLst = uow.Repository<Farm_Country>().GetData().
                //    Where(a => a.Farm_Request_ID == request.ID && a.User_Deletion_Id == null).
                //    Select(a => new FarmCountryDTO
                //    {
                //        ID = a.ID,
                //        IsAcceppted = a.IsAcceppted,
                //        IsActive = a.IsActive,
                //        Start_Date = a.Start_Date,
                //        End_Date = a.End_Date,

                //        Country_ID = a.Country_ID,
                //        country_Name_Ar = a.Country.Ar_Name,
                //        country_Name_En = a.Country.En_Name
                //        //country_Name = (lang == "1" ? a.Country.Ar_Name : a.Country.En_Name)

                //    }).ToList();
                //}
                //eman
                var reqId = uow.Repository<Farm_Request>().GetData().
                  FirstOrDefault(a => a.FarmsData_ID == Farm_Data_ID&&a.ID== RequestId && a.User_Deletion_Id == null).ID;
                var committee= uow.Repository<Farm_Committee>().GetData().
                  FirstOrDefault(a => a.Farm_Request_ID== reqId && a.User_Deletion_Id == null);
                if(committee != null)
                {
                    var committeeId = committee.ID;
                    var androidLocation = uow.Repository<Andriod_Location>().GetData().FirstOrDefault(l => l.Committe_ID == committeeId && l.Operation_ID == 16);
                    if (androidLocation != null)
                    {
                        FarmPrintDetails.GPSRead = androidLocation.Latitude + "_" + androidLocation.Longitude;
                    }
                }
                
                //FarmPrintDetails.countryLst = uow.Repository<Farm_Country>().GetData()
                //    .Include(a=>a.Farm_Request.FarmsData_ID == Farm_Data_ID)
                //  // .Where(a =>  a.User_Deletion_Id == null)
                //   .Select(a => new FarmCountryDTO
                //   {
                //       ID = a.ID,
                //       IsAcceppted = a.IsAcceppted,
                //       IsActive = a.IsActive,
                //       Start_Date = a.Start_Date,
                //       End_Date = a.End_Date,

                //       Country_ID = a.Country_ID,
                //       country_Name = (lang == "1" ? a.Country.Ar_Name : a.Country.En_Name)

                //   }).ToList();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, FarmPrintDetails);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
