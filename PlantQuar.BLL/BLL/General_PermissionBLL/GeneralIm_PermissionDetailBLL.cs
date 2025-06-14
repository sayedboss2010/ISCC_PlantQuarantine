using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.General_Permissions;
using PlantQuar.DTO.DTO.General_Permissions.Permissions;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
// تم نقلها نبيله13-3
namespace PlantQuar.BLL.BLL.General_Permission
{
    public class GeneralIm_PermissionDetailBLL
    {
        private UnitOfWork uow;
        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        public GeneralIm_PermissionDetailBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetImPermissionDetails
          (decimal? ImPermission_Number, List<string> Device_Info)
        {
            try
            {
                string lang = Device_Info[2];
                var permissionDetail = (from cc in entities.Im_PermissionRequest
                                        join rr in entities.Im_RequestData on
                                        cc.ID equals rr.Im_PermissionRequest_ID


                                        join co in entities.Countries on
                                        rr.ExportCountry_Id equals co.ID

                                        //join coc in entities.CompanyActivities on
                                        //rr.Importer_ID equals coc.Company_ID

                                        //join sys in entities.A_SystemCode
                                        //on coc.MainActivityType equals sys.Id

                                        where cc.ImPermission_Number == ImPermission_Number

                                        select new ImPermissionPrintDetailsDTO
                                        {
                                            Im_PermissionRequest_ID = cc.ID,
                                            IsAcceppted = cc.IsAcceppted,
                                            //IsActive = cc.IsActive,
                                            //IsPaid = cc.IsPaid,
                                            Company_ID = rr.Importer_ID,
                                            Im_RequestData_ID = rr.ID,
                                            ImPermission_Number = cc.ImPermission_Number.ToString(),
                                            Ship_Name = rr.Ship_Name,
                                            ExportCountryName = lang == "1" ? co.Ar_Name : co.En_Name,
                                            Importer_ID = rr.Importer_ID,
                                            ImporterType_Id = rr.ImporterType_Id,
                                            Transport_Mean_Id = rr.Transport_Mean_Id,
                                            Shipment_Mean_Id = rr.Shipment_Mean_Id,
                                            delegateName = rr.DelegateName,
                                            delegateAddress = rr.DelegateAddress
                                            ,
                                            Renewal_Status = cc.Renewal_Status
                                            //companyActivityType=sys.ValueName
                                            //sayed2020
                                        }).FirstOrDefault();



                //              select s.ValueName , c.ID , c.Enrollment_Number,c.Enrollment_Start,
                //c.Enrollment_End from CompanyActivity c
                //join A_SystemCode s on c.MainActivityType = s.Id
                //where c.Company_ID = 54

                if (permissionDetail != null)
                {


                    if (permissionDetail.ImporterType_Id == 6)
                    {
                        permissionDetail.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == permissionDetail.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                        permissionDetail.ImporterAddress = uow.Repository<Company_National>().GetData().Where(a => a.ID == permissionDetail.Importer_ID).Select(s => lang == "1" ? s.Address_Ar : s.Address_En).FirstOrDefault();
                        permissionDetail.ImporterType = "شركة";
                        var compAct = uow.Repository<CompanyActivity>().GetData().FirstOrDefault(c => c.Company_ID == permissionDetail.Importer_ID);
                        if (compAct != null)
                        {
                            // EDIT bY ESLAM
                            //var compActType = uow.Repository<CompanyActivityType>().GetData().FirstOrDefault(f => f.ID == compAct.CompActivityType_ID);

                            //if(compActType != null)
                            //{
                            //    permissionDetail.companyActivityType = compActType.Name_Ar;
                            //    permissionDetail.companyActivityTypeEn = compActType.Name_En;
                            //}
                            permissionDetail.Enrollment_Name = compAct.Enrollment_Name;
                            permissionDetail.Enrollment_Number = compAct.Enrollment_Number;
                            permissionDetail.Enrollment_Start = compAct.Enrollment_Start;
                            permissionDetail.Enrollment_End = compAct.Enrollment_End;
                            var enrollmentType = uow.Repository<Enrollment_type>().GetData().FirstOrDefault(f => f.ID == compAct.Enrollment_type_ID);
                            //if(compActType != null)
                            //{
                            //    permissionDetail.companyActivityType = compActType.Name_Ar;
                            //    permissionDetail.companyActivityTypeEn = compActType.Name_En;
                            //}
                            if (enrollmentType != null)
                            {
                                permissionDetail.Enrollment_type_AR = enrollmentType.Ar_Name;
                                permissionDetail.Enrollment_type_EN = enrollmentType.En_Name;
                            }
                        }

                    }
                    else if (permissionDetail.ImporterType_Id == 7)
                    {
                        permissionDetail.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == permissionDetail.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                        permissionDetail.ImporterAddress = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == permissionDetail.Importer_ID).Select(s => lang == "1" ? s.Address_Ar : s.Address_En).FirstOrDefault();
                        permissionDetail.ImporterType = "هيئة";
                    }
                    else
                    {
                        permissionDetail.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == permissionDetail.Importer_ID).Select(s => s.Name).FirstOrDefault();
                        permissionDetail.ImporterAddress = uow.Repository<Person>().GetData().Where(a => a.ID == permissionDetail.Importer_ID).Select(s => s.Address).FirstOrDefault();
                        permissionDetail.ImporterType = "فرد";
                        permissionDetail.personIdNo = uow.Repository<Person>().GetData().Where(a => a.ID == permissionDetail.Importer_ID).Select(s => s.IDNumber).FirstOrDefault();
                    }
                    // enter portNational or international
                    //var porttransitId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionDetail.Im_RequestData_ID && d.ReqPortType_ID ==11).FirstOrDefault().Port_ID;
                    //var isNationalTransit = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionDetail.Im_RequestData_ID && d.ReqPortType_ID == 11).FirstOrDefault().IsNational;
                    Nullable<int> portArriveId = null;
                    var portArrive = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionDetail.Im_RequestData_ID && d.ReqPortType_ID == 10 && d.User_Deletion_Id == null).FirstOrDefault();
                    if (portArrive != null)
                    {
                        portArriveId = portArrive.Port_ID;
                    }
                    // var portTypeId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionDetail.Im_RequestData_ID).FirstOrDefault().ReqPortType_ID;
                    var xx = uow.Repository<PortNational>().GetData().Where(m => m.ID == portArriveId).FirstOrDefault();
                    if (xx != null)
                    {
                        permissionDetail.ArrivePortName = lang == "1" ? xx.Name_Ar : xx.Name_En;
                    }
                    //transport port 
                    Nullable<int> portTransId = null;
                    var portTrans = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionDetail.Im_RequestData_ID && d.ReqPortType_ID == 9 && d.User_Deletion_Id == null).FirstOrDefault();
                    if (portTrans != null)
                    {
                        portTransId = portTrans.Port_ID;
                    } //var portTypeId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID).FirstOrDefault().ReqPortType_ID;
                    var tt = uow.Repository<Port_International>().GetData().Where(m => m.ID == portTransId).FirstOrDefault();
                    if (tt != null)
                    {
                        permissionDetail.TransportPortName = lang == "1" ? tt.Name_Ar : tt.Name_En;
                    }
                    //end


                    var shipp = uow.Repository<Shipment_Mean>().GetData().Where(c => c.ID == permissionDetail.Shipment_Mean_Id).FirstOrDefault();
                    if (shipp != null)
                    {
                        permissionDetail.Shipment_MeanName = lang == "1" ? shipp.Ar_Name : shipp.En_Name;
                    }
                    var transport = uow.Repository<Transport_Mean>().GetData().Where(c => c.ID == permissionDetail.Transport_Mean_Id).FirstOrDefault();
                    if (transport != null)
                    {
                        permissionDetail.Transport_MeanName = lang == "1" ? transport.Ar_Name : transport.En_Name;
                    }
                    //var isNationalArrive = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionDetail.Im_RequestData_ID && d.ReqPortType_ID == 10).FirstOrDefault().IsNational;
                    // get companies out egypt

                    var com = uow.Repository<Im_RequestDat_Extra>().GetData().Where(i => i.Im_RequestData_ID == permissionDetail.Im_RequestData_ID).Select(v => new Importers
                    {
                        ImporterCompany = v.ImportCompany,
                        ImporterCompanyEn = v.ImportCompany_EN,
                        ImporterCompanyAddressEn = v.ImporeterCompanyAddress_EN,
                        ImporterCompanyAddress = v.ImporeterCompanyAddress
                    }).ToList();
                    permissionDetail.ImportersCompanies = com;
                    //end companies
                    //Items
                    var itemss = uow.Repository<Im_PermissionItems>().GetData().Where(i => i.Im_PermissionRequest_ID == permissionDetail.Im_PermissionRequest_ID).Select(v => new Itemss
                    {
                        Im_Initiator_ID = v.Im_Initiator_ID,
                        ImPermissionItem_ID = v.ID,
                        Size = v.Size,
                        Package_Count = v.Package_Count,
                        Package_Weight = v.Package_Weight,
                        Units_Number = v.Units_Number,
                        packageTypeID = v.Package_Type_ID,
                        GrossWeight = v.GrossWeight,
                        packageMaterialID = v.Package_Material_ID,
                        Order_Text = v.Order_Text,
                        Item_ShortName_ID = v.Item_ShortName_ID,
                        QualitativeGroup_Id = v.QualitativeGroup_Id

                    }).Distinct().ToList();

                    //end Items
                    foreach (var itt in itemss)
                    {
                        var ptype = uow.Repository<Package_Type>().GetData().Where(d => d.ID == itt.packageTypeID).FirstOrDefault();
                        if (ptype != null)
                        {
                            itt.packageType = lang == "1" ? ptype.Ar_Name : ptype.En_Name;
                        }
                        var pmat = uow.Repository<Package_Material>().GetData().Where(d => d.ID == itt.packageMaterialID).FirstOrDefault();
                        if (pmat != null)
                        {
                            itt.packageMaterial = lang == "1" ? pmat.Ar_Name : pmat.En_Name;
                        }
                        Im_Initiator initiatir = new Im_Initiator();
                        initiatir = uow.Repository<Im_Initiator>().GetData().Where(u => u.ID == itt.Im_Initiator_ID).FirstOrDefault();
                        if (initiatir != null)
                        {


                            var conId = initiatir.Country_Id;
                            if (conId != null)
                            {

                                itt.InitiatorCountry = uow.Repository<Country>().GetData().Where(c => c.ID == conId).FirstOrDefault().Ar_Name;
                            }
                            else
                            {
                                itt.InitiatorCountry = "كل الدول";
                            }

                            //ask about qualitive group
                            var qualId = initiatir.QualitativeGroup_Id;
                            if (qualId != null)
                            {
                                itt.qualitiveGroupName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault().Name_Ar;
                            }

                            var itemShortNameId = initiatir.Item_ShortName_ID;
                            if (itemShortNameId != null)
                            {
                                var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId).FirstOrDefault();
                                itt.ItemShortNameAr = lang == "1" ? itemShortName.ShortName_Ar : itemShortName.ShortName_En;
                                itt.ItemShortNameEn = itemShortName.ShortName_En;
                                var itemCatGr = itemShortName.ItemCategories_Group_ID;

                                var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
                                if (stat != null)
                                {
                                    itt.Status = lang == "1" ? stat.Ar_Name : stat.En_Name;
                                }
                                var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
                                if (prp != null)
                                {
                                    itt.Purpose = lang == "1" ? prp.Ar_Name : prp.En_Name;
                                }
                                var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
                                if (subp != null)
                                {
                                    itt.subPartName = lang == "1" ? subp.Name_Ar : subp.Name_En;
                                }
                                var itmname = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault();

                                itt.ItemName = lang == "1" ? itmname.Name_Ar : itmname.Name_En;
                            }
                        }
                        else
                        {



                            itt.InitiatorCountry = "كل الدول";



                            var qualId = itt.QualitativeGroup_Id;
                            if (qualId != null)
                            {
                                itt.qualitiveGroupName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault().Name_Ar;
                            }


                            var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itt.Item_ShortName_ID).FirstOrDefault();
                            itt.ItemShortNameAr = lang == "1" ? itemShortName.ShortName_Ar : itemShortName.ShortName_En;
                            itt.ItemShortNameEn = itemShortName.ShortName_En;
                            var itemCatGr = itemShortName.ItemCategories_Group_ID;

                            var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
                            if (stat != null)
                            {
                                itt.Status = lang == "1" ? stat.Ar_Name : stat.En_Name;
                            }
                            var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
                            if (prp != null)
                            {
                                itt.Purpose = lang == "1" ? prp.Ar_Name : prp.En_Name;
                            }
                            var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
                            if (subp != null)
                            {
                                itt.subPartName = lang == "1" ? subp.Name_Ar : subp.Name_En;
                            }
                            var itmname = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault();

                            itt.ItemName = lang == "1" ? itmname.Name_Ar : itmname.Name_En;

                        }
                        //List categories

                        var cat = uow.Repository<Im_PermissionItems_Category>().GetData()
                            .Where(d => d.Im_PermissionItems_ID == itt.ImPermissionItem_ID).Select(v => new categories
                            {
                                Im_PermissionItems_ID = v.Im_PermissionItems_ID,
                                Im_PermissionItemsCategory_ID = v.ID,
                                Size = v.Size,
                                Package_Count = v.Package_Count,
                                Package_Weight = v.Package_Weight,
                                Units_Number = v.Units_Number,
                                packageTypeID = v.Package_Type_ID,
                                GrossWeight = v.GrossWeight,
                                Order_Text = v.Order_Text,
                                packageMaterialID = v.Package_Material_ID,


                            })
                           .ToList();
                        foreach (var ctt in cat)
                        {
                            var ptypec = uow.Repository<Package_Type>().GetData().Where(d => d.ID == ctt.packageTypeID).FirstOrDefault();
                            if (ptypec != null)
                            {
                                ctt.packageType = lang == "1" ? ptypec.Ar_Name : ptypec.En_Name;
                            }
                            var pmatc = uow.Repository<Package_Material>().GetData().Where(d => d.ID == ctt.packageMaterialID).FirstOrDefault();
                            if (pmatc != null)
                            {
                                ctt.packageMaterial = lang == "1" ? pmatc.Ar_Name : pmatc.En_Name;
                            }
                            var categoryName1 = uow.Repository<Im_PermissionItems_Category>().GetData().Where(g => g.ID == ctt.Im_PermissionItemsCategory_ID).FirstOrDefault();

                            if (categoryName1 != null)
                            {
                                if (categoryName1.ItemCategory_ID != null)
                                {
                                    var categoryy = uow.Repository<ItemCategory>().GetData().Where(g => g.ID == categoryName1.ItemCategory_ID).FirstOrDefault();
                                    ctt.categoryName = categoryy.Name_Ar;
                                    ctt.IsRegister = categoryy.IsRegister;
                                    ctt.RegisterReason = categoryName1.Reason_Entry;
                                    var catGr = uow.Repository<ItemCategories_Group>().GetData().FirstOrDefault(a => a.ID == categoryy.ItemCategories_Group_ID);
                                    if (catGr != null)
                                    {
                                        ctt.itemCatGroup = lang == "1" ? catGr.Name_Ar : catGr.Name_En;
                                    }
                                }
                                else if (categoryName1.ItemCategoryGroup_ID != null)
                                {
                                    ctt.categoryName = uow.Repository<ItemCategories_Group>().GetData().Where(g => g.ID == categoryName1.ItemCategoryGroup_ID).FirstOrDefault().Name_Ar;

                                }
                            }



                        }
                        itt.ItemCategories = cat;

                    }

                    permissionDetail.Items = itemss;
                    //Attachments
                    permissionDetail.Attachments = uow.Repository<A_AttachmentData>().GetData().Where(v => v.RowId == permissionDetail.Im_PermissionRequest_ID && v.A_AttachmentTableNameId == 4 && v.User_Deletion_Id == null).Select(x => new Attachments
                    {
                        Attachment_Number = x.Attachment_Number,
                        AttachmentPath = x.AttachmentPath,
                        Attachment_TypeName = x.Attachment_TypeName,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate
                    }).ToList();

                    //constrains
                    foreach (var init in permissionDetail.Items)
                    {
                        var initiatorId = init.Im_Initiator_ID;
                        var initiatir = uow.Repository<Im_Initiator>().GetData().Where(u => u.ID == initiatorId).FirstOrDefault();
                        long? ItemShortName = 0;
                        short? qualId = 0;
                        if (initiatir != null)
                        {
                            ItemShortName = initiatir.Item_ShortName_ID;
                            qualId = initiatir.QualitativeGroup_Id;
                        }

                        var conTextAr = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

                             r.Im_CountryConstrain_Text.InSide_Certificate_Ar
                        //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                        ).ToList();
                        var conTextEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>


                         r.Im_CountryConstrain_Text.InSide_Certificate_En
                        ).ToList();
                        var constr = new constrains();
                        constr.texts_Ar = lang == "1" ? conTextAr : conTextEn;
                        constr.text_En = conTextEn;
                        var pp = new List<ports>();

                        if (ItemShortName != null)
                        {
                            pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Item_ShortName_ID == ItemShortName && p.IsActive == true).Select(w => new ports
                            {

                                portId = w.Port_National_Id,
                                portTypeId = w.Port_Type_ID
                            }).ToList();


                        }
                        if (qualId != null)
                        {
                            pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Id_QualitativeGroup == qualId && p.IsActive == true).Select(w => new ports
                            {

                                portId = w.Port_National_Id,
                                portTypeId = w.Port_Type_ID
                            }).ToList();


                        }
                        foreach (var prt in pp)
                        {
                            if (prt.portTypeId != null)
                            {
                                var pTName = uow.Repository<Port_Type>().GetData().Where(m => m.ID == prt.portTypeId).FirstOrDefault();

                                if (pTName != null)
                                {
                                    prt.portType = pTName.Name_Ar;
                                    prt.portTypeEn = pTName.Name_En;
                                }

                            }
                            if (prt.portId != null)
                            {
                                var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId).FirstOrDefault();

                                if (pName != null)
                                {
                                    prt.portName = pName.Name_Ar;
                                    prt.portNameEn = pName.Name_En;

                                }
                            }
                        }
                        constr.itemConstrainPorts = pp;
                        //constr.conCountry = lang == "1" ? initiatir.Country.Ar_Name:initiatir.Country.En_Name;

                        long? conId = 0;
                        if (initiatir != null)
                        {
                            conId = initiatir.Country_Id;
                        }
                        if (conId != 0)
                        {

                            constr.conCountry = uow.Repository<Country>().GetData().Where(c => c.ID == conId).FirstOrDefault().Ar_Name;
                        }
                        else
                        {
                            constr.conCountry = "كل الدول";
                        }



                        init.Itemconstrains = constr;

                    }










                    // permission.Add(permissionDetail);
                }




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, permissionDetail);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> ApproveImPermision(ImPermissionPrintDetailsDTO dto, List<string> Device_Info)
        {
            try
            {
                ImPermission_Approve(dto.Im_PermissionRequest_ID, dto.IsAcceppted, dto.User_Creation_Id);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, dto);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        private void ImPermission_Approve(long Im_PermissionRequest_ID, bool? IsAcceppted, short? User_Creation_Id, int Action_Nou = 0)
        {
            Im_PermissionRequest CModel = uow.Repository<Im_PermissionRequest>().Findobject(Im_PermissionRequest_ID);
            CModel.IsAcceppted = IsAcceppted;
            if (Action_Nou == 1)
            {
                CModel.IsPaid = true;
            }
            uow.SaveChanges();
            //if (dto.Renewal_Status > 0)
            //{
            //    CModel.IsPaid = true;
            //}
            short _ID_Table_Action = 0;
            string _NOTS = "";
            if (Action_Nou == 0)
            {
                if (CModel.Renewal_Status == 0)
                {
                    if (IsAcceppted == true)
                    {
                        _ID_Table_Action = 3;
                        _NOTS = "تمت الموافقة";
                    }
                    else
                    {
                        _ID_Table_Action = 21;
                        _NOTS = "تم الرفض";
                    }
                }
                else
                {

                    if (IsAcceppted == true)
                    {
                        _ID_Table_Action = 7;
                        _NOTS = "تمت الموافقة على التجديد";
                    }
                    else
                    {
                        _ID_Table_Action = 22;
                        _NOTS = "تم رفض التجديد";
                    }
                }
            }
            else// حالة الدفع بدون رسوم
            {
                _ID_Table_Action = 29;
                _NOTS = "دفع بدون رسوم ";
            }
            Table_Action_Log newLog = new Table_Action_Log();
            var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Table_Action_Log_seq");
            newLog.ID = idd;
            newLog.ID_Table_Action = _ID_Table_Action;
            newLog.NOTS = _NOTS;
            newLog.ID_TableActionValue = Im_PermissionRequest_ID;
            newLog.User_Creation_Id = User_Creation_Id;
            newLog.User_Creation_Date = DateTime.Now;

            uow.Repository<Table_Action_Log>().InsertRecord(newLog);
            uow.SaveChanges();
        }

        public Dictionary<string, object> getSegal(long dto, List<string> Device_Info)
        {
            try
            {
                var permission = (from c in entities.CompanyActivities
                                  where c.Company_ID == dto


                                  join s in entities.A_SystemCode on
                                        c.MainActivityType equals s.Id

                                  join e in entities.Enrollment_type
                                  on c.Enrollment_type_ID equals e.ID
                                  select new ImPermissionPrintDetailsDTO
                                  {
                                      ArrivePortName = s.ValueName,
                                      Importer_ID = c.ID,
                                      Enrollment_End = c.Enrollment_End,
                                      Enrollment_Number = c.Enrollment_Number,
                                      Enrollment_Start = c.Enrollment_Start,
                                      delegateName = e.Ar_Name

                                      //sayed2020
                                  }).ToList();



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, permission);
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
        public Dictionary<string, object> InsertReasons(Im_PermissionRequest_RefuseReasonDTO dto, List<string> Device_Info)
        {
            try
            {
                ImPermission_Approve(dto.Im_PermissionRequest_Id, false, dto.User_Creation_Id);
                Im_PermissionRequest_RefuseReasonDTO rr = new Im_PermissionRequest_RefuseReasonDTO();
                foreach (var id in dto.refuseReasonsIds)
                {
                    var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Im_PermissionRequest_RefuseReason_seq");
                    rr.ID = idd;
                    rr.Im_PermissionRequest_Id = dto.Im_PermissionRequest_Id;
                    rr.Refuse_Reason_Id = id;
                    rr.User_Creation_Id = dto.User_Creation_Id;
                    rr.User_Creation_Date = dto.User_Creation_Date;
                    var CModel = Mapper.Map<Im_PermissionRequest_RefuseReasonDTO, Im_PermissionRequest_RefuseReason>(rr);
                    uow.Repository<Im_PermissionRequest_RefuseReason>().InsertRecord(CModel);
                    uow.SaveChanges();
                    //  InsertReason(rr, Device_Info);
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dto.Im_PermissionRequest_Id);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InsertReason(Im_PermissionRequest_RefuseReasonDTO entity, List<string> Device_Info)
        {
            try
            {

                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Im_PermissionRequest_RefuseReason_seq");
                entity.ID = idd;
                var CModel = Mapper.Map<Im_PermissionRequest_RefuseReason>
                    (entity);

                uow.Repository<Im_PermissionRequest_RefuseReason>().InsertRecord(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, entity);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        // get reafuseReason for impermissionID
        public Dictionary<string, object> getReafuseReasonByPerrmissionId
            (long Im_permissionId, List<string> Device_Info)
        {
            try
            {
                var permission = (
                              from c in entities.Im_PermissionRequest_RefuseReason
                              join s in entities.Refuse_Reason on
                                        c.Refuse_Reason_Id equals s.ID
                              join m in entities.Im_PermissionRequest
                              on c.Im_PermissionRequest_Id equals m.ID
                              where m.ImPermission_Number == Im_permissionId

                              select new Im_Permission_RefuseReasonDTO
                              {
                                  Name_Ar = s.Name_Ar,
                                  Note = c.Nots

                                  //sayed2020
                              }).ToList();



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, permission);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> InsertNoPaid(ImPermissionPrintDetailsDTO dto, List<string> Device_Info)
        {
            try
            {
                ImPermission_Approve(dto.Im_PermissionRequest_ID, true, dto.User_Creation_Id, 1);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dto.Im_PermissionRequest_ID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


    }
}
