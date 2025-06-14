using PlantQuar.DAL;
using PlantQuar.DTO.DTO.General_Permissions.Permissions;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.General_Permission
{
    public class General_Im_PermissionRequestPrintBLL
    {
        private UnitOfWork uow;

        public General_Im_PermissionRequestPrintBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetImPermissionPrintDetails
           (decimal? ImPermission_Number, short User_Creation_Id, int OperationCode, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var permissionPrintDetails = (from cc in entities.Im_PermissionRequest
                                              join rr in entities.Im_RequestData on cc.ID equals rr.Im_PermissionRequest_ID
                                              //join oo in entities.Im_OpertaionType on rr.Im_OperationType equals oo.ID
                                              join co in entities.Countries on rr.ExportCountry_Id equals co.ID
                                              where cc.ImPermission_Number == ImPermission_Number & rr.Im_OperationType == 13

                                              select new ImPermissionPrintDetailsDTO
                                              {
                                                  Im_PermissionRequest_ID = cc.ID,
                                                  IS_Print_Ar = cc.IS_Print_Ar,
                                                  IS_Print_EN = cc.IS_Print_EN,
                                                  Im_RequestData_ID = rr.ID,
                                                  ImPermission_Number = cc.ImPermission_Number.ToString(),
                                                  Ship_Name = rr.Ship_Name,
                                                  ExportCountryName = co.Ar_Name,

                                                  ExportCountryNameEn = co.En_Name,
                                                  Transport_Mean_Id = rr.Transport_Mean_Id,
                                                  Shipment_Mean_Id = rr.Shipment_Mean_Id,
                                                  Importer_ID = rr.Importer_ID,
                                                  ImporterType_Id = rr.ImporterType_Id,
                                                  Renewal_Status = cc.Renewal_Status,
                                                  End_Date = cc.End_Date,
                                                  Start_Date = cc.Start_Date,
                                                  Print_Count = cc.Print_Count

                                              }).FirstOrDefault();

                //username
                dbPrivilageEntities priv = new dbPrivilageEntities();
                var user = priv.PR_User.Where(p => p.Id == User_Creation_Id).FirstOrDefault();
                permissionPrintDetails.UserName = user.FullName;
                permissionPrintDetails.UserNameEn = user.FullNameEn;
                var outlet = uow.Repository<Outlet>().GetData().FirstOrDefault(o => o.ID_HR == user.Outlet_ID);
                if (outlet != null)
                {
                    permissionPrintDetails.outletName = outlet.Ar_Name;
                    permissionPrintDetails.outletName_En = outlet.En_Name;
                }


                //
                if (permissionPrintDetails.ImporterType_Id == 6)
                {
                    var comp = uow.Repository<Company_National>().GetData().Where(a => a.ID == permissionPrintDetails.Importer_ID).FirstOrDefault();
                    if (comp != null)
                    {
                        permissionPrintDetails.ImporterName = comp.Name_Ar;
                        permissionPrintDetails.ImporterNameEn = comp.Name_En;

                        permissionPrintDetails.ImporterAddress = comp.Address_Ar;
                        permissionPrintDetails.ImporterAddressEn = comp.Address_En;
                    }


                }
                else if (permissionPrintDetails.ImporterType_Id == 7)
                {
                    var pup = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == permissionPrintDetails.Importer_ID).FirstOrDefault();
                    permissionPrintDetails.ImporterName = pup.Name_Ar;
                    permissionPrintDetails.ImporterNameEn = pup.Name_En;

                    permissionPrintDetails.ImporterAddress = pup.Address_Ar;
                    permissionPrintDetails.ImporterAddressEn = pup.Address_En;

                }
                else
                {
                    var per = uow.Repository<Person>().GetData().Where(a => a.ID == permissionPrintDetails.Importer_ID).FirstOrDefault();
                    permissionPrintDetails.ImporterName = per.Name;
                    permissionPrintDetails.ImporterNameEn = per.Name;

                    permissionPrintDetails.ImporterAddress = per.Address;

                    permissionPrintDetails.ImporterAddressEn = per.Address;
                }
                // enter portNational or international
                //var porttransitId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID && d.ReqPortType_ID ==11).FirstOrDefault().Port_ID;
                //var isNationalTransit = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID && d.ReqPortType_ID == 11).FirstOrDefault().IsNational;

                Nullable<int> portArriveId = null;
                var portArrive = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID && d.ReqPortType_ID == 10 && d.User_Deletion_Id == null).FirstOrDefault();
                if (portArrive != null)
                {
                    if (portArrive.Port_ID != null)
                    {
                        portArriveId = (int)portArrive.Port_ID;
                    }
                } //var portTypeId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID).FirstOrDefault().ReqPortType_ID;
                var xx = uow.Repository<PortNational>().GetData().Where(m => m.ID == portArriveId).FirstOrDefault();
                if (xx != null)
                {
                    permissionPrintDetails.ArrivePortName = xx.Name_Ar;
                    permissionPrintDetails.ArrivePortNameEn = xx.Name_En;
                }
                else
                {
                    permissionPrintDetails.ArrivePortName = "اي ميناء في مصر";
                    permissionPrintDetails.ArrivePortNameEn = "Any Port In Egypt";
                }

                //transport port 
                Nullable<int> portTransId = null;
                var portTrans = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID && d.ReqPortType_ID == 9 && d.User_Deletion_Id == null).FirstOrDefault();
                if (portTrans != null)
                {
                    portTransId = portTrans.Port_ID;
                } //var portTypeId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID).FirstOrDefault().ReqPortType_ID;
                var tt = uow.Repository<Port_International>().GetData().Where(m => m.ID == portTransId).FirstOrDefault();
                if (tt != null)
                {
                    permissionPrintDetails.TransportPortName = tt.Name_Ar;
                    permissionPrintDetails.TransportPortNameEn = tt.Name_En;
                }
                //end
                var shipp = uow.Repository<Shipment_Mean>().GetData().Where(c => c.ID == permissionPrintDetails.Shipment_Mean_Id).FirstOrDefault();
                if (shipp != null)
                {
                    permissionPrintDetails.Shipment_MeanName = shipp.Ar_Name;
                    permissionPrintDetails.Shipment_MeanNameEn = shipp.En_Name;
                }
                else
                {
                    permissionPrintDetails.Shipment_MeanName = "####";
                    permissionPrintDetails.Shipment_MeanNameEn = "####";
                }
                var transport = uow.Repository<Transport_Mean>().GetData().Where(c => c.ID == permissionPrintDetails.Transport_Mean_Id).FirstOrDefault();
                if (transport != null)
                {
                    permissionPrintDetails.Transport_MeanName = transport.Ar_Name;
                    permissionPrintDetails.Transport_MeanNameEn = transport.En_Name;
                }
                else
                {
                    permissionPrintDetails.Transport_MeanName = "####";
                    permissionPrintDetails.Transport_MeanNameEn = "####";
                }
                //var isNationalArrive = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID && d.ReqPortType_ID == 10).FirstOrDefault().IsNational;
                // get companies out egypt

                var com = uow.Repository<Im_RequestDat_Extra>().GetData().Where(i => i.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID).Select(v => new Importers
                {
                    ImporterCompany = v.ImportCompany,
                    //ImporterCompanyEn = v.
                    ImporterCompanyAddress = v.ImporeterCompanyAddress,
                    ImporterCompanyEn = v.ImportCompany_EN,
                    ImporterCompanyAddressEn = v.ImporeterCompanyAddress_EN
                }).ToList();
                permissionPrintDetails.ImportersCompanies = com;
                //end companies
                //Items
                var itemss = uow.Repository<Im_PermissionItems>().GetData().Where(i => i.Im_PermissionRequest_ID == permissionPrintDetails.Im_PermissionRequest_ID).Select(v => new Itemss
                {
                    Im_Initiator_ID = v.Im_Initiator_ID,
                    ImPermissionItem_ID = v.ID,
                    Size = v.Size,
                    Package_Count = v.Package_Count,
                    Package_Weight = v.Package_Weight,
                    Units_Number = v.Units_Number,
                    packageTypeID = v.Package_Type_ID,
                    packageMaterialID = v.Package_Material_ID,
                    GrossWeight = v.GrossWeight,
                    Order_Text = v.Order_Text == null ? "####" : v.Order_Text

                }).Distinct().ToList();

                //end Items
                foreach (var itt in itemss)
                {
                    var ptype = uow.Repository<Package_Type>().GetData().Where(d => d.ID == itt.packageTypeID).FirstOrDefault();
                    if (ptype != null)
                    {
                        itt.packageType = ptype.Ar_Name;
                        itt.packageTypeEn = ptype.En_Name;
                    }
                    else
                    {
                        itt.packageType = "####";
                        itt.packageTypeEn = "####";
                    }
                    var pmat = uow.Repository<Package_Material>().GetData().Where(d => d.ID == itt.packageMaterialID).FirstOrDefault();
                    if (pmat != null)
                    {
                        itt.packageMaterial = pmat.Ar_Name;
                        itt.packageMaterialEn = pmat.En_Name;
                    }
                    else
                    {
                        itt.packageMaterial = "####";
                        itt.packageMaterialEn = "####";
                    }
                    if (itt.Im_Initiator_ID > 0)
                    {


                        var initiatir = uow.Repository<Im_Initiator>().GetData().Where(u => u.ID == itt.Im_Initiator_ID).FirstOrDefault();
                        var conId = initiatir.Country_Id;
                        if (conId != null)
                        {

                            itt.InitiatorCountry = uow.Repository<Country>().GetData().Where(c => c.ID == conId).FirstOrDefault().Ar_Name;
                            itt.InitiatorCountryEn = uow.Repository<Country>().GetData().Where(c => c.ID == conId).FirstOrDefault().En_Name;
                        }
                        else
                        {
                            itt.InitiatorCountry = "كل الدول";
                            itt.InitiatorCountryEn = "All Country";
                        }
                        //itt.InitiatorCountry = initiatir.Country.Ar_Name;
                        //itt.InitiatorCountryEn = initiatir.Country.En_Name;
                        //ask about qualitive group
                        var qualId = initiatir.QualitativeGroup_Id;
                        if (qualId != null)
                        {
                            var qual = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault();
                            itt.qualitiveGroupName = qual.Name_Ar;
                            itt.qualitiveGroupNameEn = qual.Name_En;
                            itt.Status = "####";
                            itt.StatusEn = "####";
                            itt.Purpose = "####";
                            itt.PurposeEn = "####";
                            itt.subPartName = "####";
                            itt.subPartNameEn = "####";
                            itt.ItemName = "####";
                            itt.ScientificNameAr = "####";

                        }

                        var itemShortNameId = initiatir.Item_ShortName_ID;
                        if (itemShortNameId != null)
                        {
                            var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId).FirstOrDefault();
                            itt.ItemShortNameAr = itemShortName.ShortName_Ar;
                            itt.ItemShortNameEn = itemShortName.ShortName_En;
                            var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
                            if (stat != null)
                            {
                                itt.Status = stat.Ar_Name;
                                itt.StatusEn = stat.En_Name;
                            }
                            else
                            {
                                itt.Status = "####";
                                itt.StatusEn = "####";
                            }
                            var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
                            if (prp != null)
                            {
                                itt.Purpose = prp.Ar_Name;
                                itt.PurposeEn = prp.En_Name;
                            }
                            else
                            {
                                itt.Purpose = "####";
                                itt.PurposeEn = "####";
                            }
                            var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
                            if (subp != null)
                            {
                                itt.subPartName = subp.Name_Ar;
                                itt.subPartNameEn = subp.Name_En;
                            }
                            else
                            {
                                itt.subPartName = "####";
                                itt.subPartNameEn = "####";
                            }
                            var itmm = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault();
                            itt.ItemName = itmm.Name_Ar;
                            itt.ScientificNameAr = itmm.Scientific_Name == null ? "####" : itmm.Scientific_Name;
                        }
                    }

                    else
                    {




                        itt.InitiatorCountry = "كل الدول";
                        itt.InitiatorCountryEn = "All Country";

                        var qualId = itt.QualitativeGroup_Id;
                        if (qualId != null)
                        {
                            var qual = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault();
                            itt.qualitiveGroupName = qual.Name_Ar;
                            itt.qualitiveGroupNameEn = qual.Name_En;
                            itt.Status = "####";
                            itt.StatusEn = "####";
                            itt.Purpose = "####";
                            itt.PurposeEn = "####";
                            itt.subPartName = "####";
                            itt.subPartNameEn = "####";
                            itt.ItemName = "####";
                            itt.ScientificNameAr = "####";

                        }



                        if (itt.Item_ShortName_ID > 0)
                        {


                            var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itt.Item_ShortName_ID).FirstOrDefault();

                            itt.ItemShortNameAr = itemShortName.ShortName_Ar;
                            itt.ItemShortNameEn = itemShortName.ShortName_En;
                            var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
                            if (stat != null)
                            {
                                itt.Status = stat.Ar_Name;
                                itt.StatusEn = stat.En_Name;
                            }
                            else
                            {
                                itt.Status = "####";
                                itt.StatusEn = "####";
                            }
                            var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
                            if (prp != null)
                            {
                                itt.Purpose = prp.Ar_Name;
                                itt.PurposeEn = prp.En_Name;
                            }
                            else
                            {
                                itt.Purpose = "####";
                                itt.PurposeEn = "####";
                            }
                            var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
                            if (subp != null)
                            {
                                itt.subPartName = subp.Name_Ar;
                                itt.subPartNameEn = subp.Name_En;
                            }
                            else
                            {
                                itt.subPartName = "####";
                                itt.subPartNameEn = "####";
                            }

                            var itmm = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault();
                            itt.ItemName = itmm.Name_Ar;
                            itt.ScientificNameAr = itmm.Scientific_Name == null ? "####" : itmm.Scientific_Name;
                        }
                    }

                    //List categories

                    var cat = uow.Repository<Im_PermissionItems_Category>().GetData().Where(d => d.Im_PermissionItems_ID == itt.ImPermissionItem_ID).Select(v => new categories
                    {
                        Im_PermissionItems_ID = v.Im_PermissionItems_ID,
                        Im_PermissionItemsCategory_ID = v.ID,
                        Size = v.Size,
                        Package_Count = v.Package_Count,
                        Package_Weight = v.Package_Weight,
                        Units_Number = v.Units_Number,
                        packageTypeID = v.Package_Type_ID,
                        GrossWeight = v.GrossWeight

                    })
                            .ToList();
                    foreach (var ctt in cat)
                    {
                        var ptypec = uow.Repository<Package_Type>().GetData().Where(d => d.ID == ctt.packageTypeID).FirstOrDefault();
                        if (ptypec != null)
                        {
                            ctt.packageType = ptypec.Ar_Name;
                        }
                        var categoryName1 = uow.Repository<Im_PermissionItems_Category>().GetData().Where(g => g.ID == ctt.Im_PermissionItemsCategory_ID).FirstOrDefault();

                        if (categoryName1 != null)
                        {
                            if (categoryName1.ItemCategory_ID != null)
                            {
                                var categ = uow.Repository<ItemCategory>().GetData().Where(g => g.ID == categoryName1.ItemCategory_ID).FirstOrDefault();

                                ctt.categoryName = categ.Name_Ar;
                                ctt.categoryNameEn = categ.Name_En;

                            }
                            else if (categoryName1.ItemCategoryGroup_ID != null)
                            {
                                var categgr = uow.Repository<ItemCategories_Group>().GetData().Where(g => g.ID == categoryName1.ItemCategoryGroup_ID).FirstOrDefault();
                                ctt.categoryName = categgr.Name_Ar;
                                ctt.categoryNameEn = categgr.Name_En;

                            }
                        }



                    }
                    itt.ItemCategories = cat;

                }
                permissionPrintDetails.Items = itemss;

                //constrains
                foreach (var init in permissionPrintDetails.Items)
                {
                    //Constrain
                    if (init.Im_Initiator_ID > 0)
                    {


                        var initiatorId = init.Im_Initiator_ID;
                        var initiatir = uow.Repository<Im_Initiator>().GetData().Where(u => u.ID == initiatorId).FirstOrDefault();

                        var ItemShortName = initiatir.Item_ShortName_ID;
                        var qualId = initiatir.QualitativeGroup_Id;
                        var conTextAr = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

                             r.Im_CountryConstrain_Text.InSide_Certificate_Ar
                        //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                        ).ToList();
                        var conTextEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>


                         r.Im_CountryConstrain_Text.InSide_Certificate_En
                        ).ToList();
                        var constr = new constrains();
                        constr.texts_Ar = conTextAr;
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

                                }

                            }
                            if (prt.portId != null)
                            {
                                var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId).FirstOrDefault();

                                if (pName != null)
                                {
                                    prt.portName = pName.Name_Ar;

                                }
                            }
                        }
                        constr.itemConstrainPorts = pp;


                        init.Itemconstrains = constr;
                    }
                    else
                    {




                        var ItemShortName = init.Item_ShortName_ID;
                        var qualId = init.QualitativeGroup_Id;
                        //var conTextAr = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

                        //     r.Im_CountryConstrain_Text.InSide_Certificate_Ar

                        //).ToList();
                        //var conTextEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>


                        // r.Im_CountryConstrain_Text.InSide_Certificate_En
                        //).ToList();
                        var constr = new constrains();
                        //constr.texts_Ar = conTextAr;
                        // constr.text_En = conTextEn;
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

                                }

                            }
                            if (prt.portId != null)
                            {
                                var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId).FirstOrDefault();

                                if (pName != null)
                                {
                                    prt.portName = pName.Name_Ar;

                                }
                            }
                        }
                        constr.itemConstrainPorts = pp;


                        init.Itemconstrains = constr;
                    }
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, permissionPrintDetails);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> printPermissionArabic(ImPermissionIsPrintDTO dto, List<string> Device_Info)
        //public Dictionary<string, object> printPermissionArabic(ImPermissionIsPrintDTO dto, int OperationCode, List<string> Device_Info)
        {
            try
            {
                Im_PermissionRequest CModel = uow.Repository<Im_PermissionRequest>().Findobject(dto.Im_PermissionRequest_ID);
                CModel.IS_Print_Ar = dto.IS_Print_Ar;
                if (CModel.Start_Date == null)
                {
                    var dat = DateTime.Now;
                    CModel.Start_Date = dat;
                }
                if (CModel.End_Date == null)
                {
                    DateTime d2 = DateTime.Now.AddMonths(3);
                    CModel.End_Date = d2;
                }

                short _ID_Table_Action = 0;
                string _NOTS = "";
                if (CModel.Renewal_Status == 0)
                {
                    _ID_Table_Action = 1;
                    _NOTS = "تمت الطباعة العربي";
                }
                else
                {
                    _ID_Table_Action = 5;
                    _NOTS = "تمت الطباعة العربي للتجديد";
                }



                //CModel.Print_Count = CModel.Print_Count == null? 1:++CModel.Print_Count;
                Table_Action_Log newLog = new Table_Action_Log();
                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Table_Action_Log_seq");
                newLog.ID = idd;
                newLog.ID_Table_Action = _ID_Table_Action;
                newLog.NOTS = _NOTS;
                newLog.ID_TableActionValue = dto.Im_PermissionRequest_ID;
                newLog.User_Creation_Id = dto.User_Creation_Id;
                newLog.User_Creation_Date = DateTime.Now;
                uow.Repository<Table_Action_Log>().InsertRecord(newLog);
                uow.SaveChanges();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, dto);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> printPermissionEnglish(ImPermissionIsPrintDTO dto, List<string> Device_Info)
        //public Dictionary<string, object> printPermissionEnglish(ImPermissionIsPrintDTO dto, int OperationCode, List<string> Device_Info)
        {
            try
            {
                Im_PermissionRequest CModel = uow.Repository<Im_PermissionRequest>().Findobject(dto.Im_PermissionRequest_ID);
                CModel.IS_Print_EN = dto.IS_Print_EN;
                if (CModel.Start_Date == null)
                {
                    var dat = DateTime.Now;
                    CModel.Start_Date = dat;
                }
                if (CModel.End_Date == null)
                {
                    DateTime d2 = DateTime.Now.AddMonths(3);
                    CModel.End_Date = d2;
                }

                short _ID_Table_Action = 0;
                string _NOTS = "";
                if (CModel.Renewal_Status == 0)
                {
                    _ID_Table_Action = 2;
                    _NOTS = "تمت الطباعة English";
                }
                else
                {
                    _ID_Table_Action = 6;
                    _NOTS = "تمت الطباعة English للتجديد";
                }


                //CModel.Print_Count = CModel.Print_Count == null? 1:++CModel.Print_Count;
                Table_Action_Log newLog = new Table_Action_Log();
                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Table_Action_Log_seq");
                newLog.ID = idd;
                newLog.ID_Table_Action = _ID_Table_Action;
                newLog.NOTS = _NOTS;
                newLog.ID_TableActionValue = dto.Im_PermissionRequest_ID;
                newLog.User_Creation_Id = dto.User_Creation_Id;
                newLog.User_Creation_Date = DateTime.Now;
                uow.Repository<Table_Action_Log>().InsertRecord(newLog);

                uow.SaveChanges();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, dto);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
