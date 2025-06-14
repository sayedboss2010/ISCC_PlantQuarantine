using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Import.Committee;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Import.Committee
{

    public class Im_Check_ComitteBLL : IGenericBLL<Im_Check_ComitteDTO>
    {
        private UnitOfWork uow;
        public Im_Check_ComitteBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(Im_Check_ComitteDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Im_Check_ComitteResult
       (long ImCheckRequest_Number, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var committee = (from rc in entities.Im_RequestCommittee
                                 join cr in entities.Im_CommitteeResult on rc.ID equals cr.Committee_ID
                                 join lot in entities.Im_CheckRequest_Items_Lot_Category on cr.Im_Request_Item_Id equals lot.ID
                                 join itm in entities.Im_CheckRequest_Items on lot.Im_CheckRequest_Items_ID equals itm.ID
                                 join ini in entities.Im_Initiator on itm.Im_Initiator_ID equals ini.ID
                                 join sn in entities.Item_ShortName on ini.Item_ShortName_ID equals sn.ID
                                 join i in entities.Items on sn.Item_ID equals i.ID
                                 //join ccf in entities.Im_CommitteeResult_Confirm on cr.ID equals ccf.Im_CommitteeResult_ID
                                 where rc.ImCheckRequest_ID == ImCheckRequest_Number
                                 && rc.CommitteeType_ID == 11
                                 select new Im_Check_ComitteDTO
                                 {
                                     Item_Name_Ar = i.Name_Ar,
                                     ShortName_Ar = sn.ShortName_Ar,
                                     EmployeeId = cr.EmployeeId,
                                     // EmployeeId = cr.EmployeeId,
                                     ID = rc.ID,
                                     ImCheckRequest_ID = rc.ImCheckRequest_ID,
                                     Delegation_Date = rc.Delegation_Date,
                                     StartTime = rc.StartTime,
                                     EndTime = rc.EndTime,
                                     IsApproved = rc.IsApproved,
                                     IsFinishedAll = rc.IsFinishedAll,
                                     Status = rc.Status,

                                     Date = cr.Date,
                                     //Weight = cr.Weight,
                                     //QuantitySize = cr.QuantitySize,
                                     //IsAdminFinalResult = cr.IsAdminFinalResult,
                                     Notes = cr.Notes,

                                     //ccf.IsAccepted,
                                     // Notes=ccf.Notes

                                 }).ToList();

                foreach (var item in committee)
                {
                    var execution = (from rc in entities.Im_RequestCommittee
                                     join cr in entities.Im_Execution on rc.ID equals cr.Im_RequestCommittee_Id
                                     join itm in entities.Im_Execution_Items on cr.ID equals itm.Im_Execution_Id
                                     join ccl in entities.Im_CommitteeCheckLocation on rc.ImCommitteeCheckLocation_ID equals ccl.ID

                                     where
                                     rc.CommitteeType_ID == 9
                                     && rc.ID == item.ID
                                     //9-- كود لجنه الاعدام = 

                                     select new Im_Execution_CommDTO
                                     {
                                         ID = rc.ID,
                                         ImCheckRequest_ID = rc.ImCheckRequest_ID,
                                         Delegation_Date = rc.Delegation_Date,
                                         StartTime = rc.StartTime,
                                         EndTime = rc.EndTime,
                                         IsApproved = rc.IsApproved,
                                         IsFinishedAll = rc.IsFinishedAll,
                                         Status = rc.Status,
                                         Execution_Place = cr.Execution_Place,
                                         Execution_Method = cr.Execution_Method,
                                         Execution_File = cr.Execution_File,
                                         //Im_PermissionItems = rc,
                                         //Im_ItemsLotDivision=itm.Im_PermissionItems
                                         //NationalID = v.ShippingAgency.Name_Ar
                                     }).ToList();
                    //,itm.GrossWeight,itm.Im_ItemsLotDivision_ID,itm.Im_PermissionItem_ID
                    item.ImExecutionComm = execution;

                    var receiveCommittee = (from dc in entities.Im_PermissionItem_Division_Custody_DismissCommittee
                                            join rc in entities.Im_RequestCommittee on dc.Im_RequestCommittee_Id equals rc.ID
                                            join crc in entities.Im_PermissionItem_Division_Custody_ReceiveCommittee on dc.ID equals crc.Im_PermissionItem_Division_Custody_DismissCommittee_Id

                                            where
                                            rc.CommitteeType_ID == 8
                                            && rc.ID == item.ID
                                            //8-- كود لجنه الاستلام = 
                                            select new Im_ReceiveCommitteeDTO
                                            {
                                                ID = rc.ID,
                                                ImCheckRequest_ID = rc.ImCheckRequest_ID,
                                                Delegation_Date = rc.Delegation_Date,
                                                StartTime = rc.StartTime,
                                                EndTime = rc.EndTime,
                                                IsApproved = rc.IsApproved,
                                                IsFinishedAll = rc.IsFinishedAll,
                                                Status = rc.Status,
                                                Dismiss_Date = dc.Dismiss_Date,
                                                DismissTime = dc.DismissTime,
                                                Im_PermissionItem_Division_Custody_Id = dc.Im_PermissionItem_Division_Custody_Id,
                                                Receive_Date = crc.Receive_Date,
                                                ReceiveTime = crc.ReceiveTime,
                                                //IsApproved=crc.IsApproved,
                                                //Status= crc.Status
                                                //NationalID = v.ShippingAgency.Name_Ar
                                            }).ToList();
                    item.Im_ReceiveCommittee = receiveCommittee;

                 //   var custodyPlace = uow.Repository<Im_CustodyPlace>()
                 //.GetData().Where(i => i.Im_CheckRequest_ID == item.ImCheckRequest_ID)
                 //.Select(v => new Im_CustodyPlaceDTO
                 //{//Im_CustodyPlaceType
                 //    Im_CheckRequest_ID = v.Im_CheckRequest_ID,
                 //    Ar_Desc = v.Ar_Desc,
                 //    En_Desc = v.En_Desc,
                 //    CustodyPlaceType = v.Im_CustodyPlaceType.Name_Ar,
                 //    Storage_capacity = v.Storage_capacity,
                 //    Center_Name = v.Center.Ar_Name,
                 //    Address = v.Address,
                 //    Owner_Name = v.Owner_Name,
                 //    NationalID = v.NationalID,
                 //    Phone = v.Phone,
                 //    PreviewQuantityDuration = v.PreviewQuantityDuration,
                 //    DateStored = v.DateStored,
                 //    Quantity = v.Quantity,
                 //    IsApproved = v.IsApproved,
                 //    Status = v.Status
                 //}).ToList();

                    //item.CustodyPlace = custodyPlace;

                    var dismissCommittee = (from dc in entities.Im_PermissionItem_Division_Custody_DismissCommittee
                                            join dvc in entities.Im_PermissionItem_Division_Custody on dc.Im_PermissionItem_Division_Custody_Id equals dvc.ID
                                            join cp in entities.Im_CustodyPlace on dvc.Im_CustodyPlace_Id equals cp.ID

                                            where dc.Im_RequestCommittee_Id == item.ID

                                            select new Im_DismissCommitteeDTO
                                            {
                                                ID = dc.ID,
                                                Im_PermissionItem_Division_Custody_Id = dc.Im_PermissionItem_Division_Custody_Id,
                                                Im_RequestCommittee_Id = dc.Im_RequestCommittee_Id,
                                                Dismiss_Date = dc.Dismiss_Date,
                                                DismissTime = dc.DismissTime,
                                                IsApproved = dc.IsApproved,
                                                Status = dc.Status,
                                                Lock_Lead = dc.Lock_Lead,
                                                Notes = dc.Notes,
                                                Driver_Name = dc.Im_PermissionItem_Division_Custody.Driver_Name,

                                                Driver_National_Id = dc.Im_PermissionItem_Division_Custody.Driver_National_Id,
                                                GrossWeight = dc.Im_PermissionItem_Division_Custody.GrossWeight,
                                            }).ToList();

                    item.Im_DismissCommittee = dismissCommittee;
                }

                ///////////////ESLAM///////////////

                #region MyRegion


                //if (CheckRequestDetails != null)
                //{


                //    if (CheckRequestDetails.ImporterType_Id == 6)
                //    {
                //        CheckRequestDetails.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                //        CheckRequestDetails.ImporterAddress = uow.Repository<Company_National>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Address_Ar : s.Address_En).FirstOrDefault();
                //        CheckRequestDetails.ImporterType = "شركة";
                //    }
                //    else if (CheckRequestDetails.ImporterType_Id == 7)
                //    {
                //        CheckRequestDetails.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                //        CheckRequestDetails.ImporterAddress = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Address_Ar : s.Address_En).FirstOrDefault();
                //        CheckRequestDetails.ImporterType = "هيئة";
                //    }
                //    else
                //    {
                //        CheckRequestDetails.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => s.Name).FirstOrDefault();
                //        CheckRequestDetails.ImporterAddress = uow.Repository<Person>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => s.Address).FirstOrDefault();
                //        CheckRequestDetails.ImporterType = "فرد";
                //    }
                //    // enter portNational or international
                //    //var porttransitId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == CheckRequestDetails.Im_RequestData_ID && d.ReqPortType_ID ==11).FirstOrDefault().Port_ID;
                //    //var isNationalTransit = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == CheckRequestDetails.Im_RequestData_ID && d.ReqPortType_ID == 11).FirstOrDefault().IsNational;
                //    Nullable<int> portArriveId = null;
                //    var portArrive = uow.Repository<Im_CheckRequest_Port>().GetData().Where(d => d.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID && d.ReqPortType_ID == 10 && d.User_Deletion_Id == null).FirstOrDefault();
                //    if (portArrive != null)
                //    {
                //        portArriveId = portArrive.Port_ID;
                //    }
                //    // var portTypeId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == CheckRequestDetails.Im_RequestData_ID).FirstOrDefault().ReqPortType_ID;
                //    var xx = uow.Repository<PortNational>().GetData().Include(i => i.Port_Type).Where(m => m.ID == portArriveId).FirstOrDefault();
                //    if (xx != null)
                //    {
                //        CheckRequestDetails.ArrivePortName = xx.Name_Ar;
                //        CheckRequestDetails.ArrivePortType = lang == "1" ? xx.Port_Type.Name_Ar : xx.Port_Type.Name_En;
                //    }

                //    //transport port 
                //    Nullable<int> portTransId = null;
                //    var portTrans = uow.Repository<Im_CheckRequest_Port>().GetData().Where(d => d.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID && d.ReqPortType_ID == 9 && d.User_Deletion_Id == null).FirstOrDefault();
                //    if (portTrans != null)
                //    {
                //        portTransId = portTrans.Port_ID;
                //    } //var portTypeId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID).FirstOrDefault().ReqPortType_ID;
                //    var tt = uow.Repository<Port_International>().GetData().Include(i => i.Port_Type).Where(m => m.ID == portTransId).FirstOrDefault();
                //    if (tt != null)
                //    {
                //        CheckRequestDetails.TransportPortName = tt.Name_Ar;
                //        CheckRequestDetails.TransportPortType = lang == "1" ? tt.Port_Type.Name_Ar : tt.Port_Type.Name_En;
                //    }
                //    //end

                //    var shipp = uow.Repository<Shipment_Mean>().GetData().Where(c => c.ID == CheckRequestDetails.Shipment_Mean_Id).FirstOrDefault();
                //    if (shipp != null)
                //    {
                //        CheckRequestDetails.Shipment_MeanName = shipp.Ar_Name;
                //    }
                //    var transport = uow.Repository<Transport_Mean>().GetData().Where(c => c.ID == CheckRequestDetails.Transport_Mean_Id).FirstOrDefault();
                //    if (transport != null)
                //    {
                //        CheckRequestDetails.Transport_MeanName = transport.Ar_Name;
                //    }
                //    //var isNationalArrive = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == CheckRequestDetails.Im_RequestData_ID && d.ReqPortType_ID == 10).FirstOrDefault().IsNational;
                //    // get companies out egypt

                //    var com = uow.Repository<Im_CheckRequestData_Extra>().GetData().Where(i => i.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID).Select(v => new Importers
                //    {
                //        ImporterCompany = v.ImportCompany,
                //        ImporterCompanyAddress = v.ImporeterCompanyAddress,
                //        ImporterCompanyEn = v.ImportCompany_EN,
                //        ImporterCompanyAddressEn = v.ImporeterCompanyAddress_EN
                //    }).ToList();
                //    CheckRequestDetails.ImportersCompanies = com;
                //    //end companies
                //    var shippingMethods = uow.Repository<Im_CheckRequset_Shipping_Method>().GetData().Where(c => c.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID).Select(n => new checkRequestShipping
                //    {
                //        ID = n.ID,
                //        containers_ID = n.containers_ID,
                //        containers_type_ID = n.containers_type_ID,
                //        ShipholdNumber = n.ShipholdNumber,
                //        ContainerNumber = n.ContainerNumber,
                //        NavigationalNumber = n.NavigationalNumber,
                //        Total_Weight = n.Total_Weight
                //    }).ToList();
                //    // distinct items for constrains
                //    var initiatorsId = new List<long?>();

                //    foreach (var ship in shippingMethods)
                //    {
                //        var container = uow.Repository<A_SystemCode>().GetData().FirstOrDefault(c => c.Id == ship.containers_ID && c.SystemCodeTypeId == 28);
                //        if (container != null)
                //        {
                //            ship.containerName = lang == "1" ? container.ValueName : container.ValueNameEN;
                //        }
                //        var containertype = uow.Repository<A_SystemCode>().GetData().FirstOrDefault(c => c.Id == ship.containers_type_ID && c.SystemCodeTypeId == 29);
                //        if (container != null)
                //        {
                //            ship.containerType = lang == "1" ? containertype.ValueName : containertype.ValueNameEN;
                //        }
                //        //Items
                //        var itemss = uow.Repository<Im_CheckRequest_Items>().GetData().Where(i => i.Im_CheckRequset_Shipping_Method_ID == ship.ID)
                //            .Select(v => new Items_checkReq
                //            {
                //                Im_Initiator_ID = v.Im_Initiator_ID,
                //                ImcheckReqItem_ID = v.ID,
                //                ImcheckReqshippedMethod_ID = v.Im_CheckRequset_Shipping_Method_ID,
                //                Size = v.Size,
                //                Package_Count = v.Package_Count,
                //                Package_Weight = v.Package_Weight,
                //                Units_Number = v.Units_Number,
                //                packageTypeID = v.Package_Type_ID,
                //                GrossWeight = v.GrossWeight

                //            }).Distinct().ToList();
                //        var ids = uow.Repository<Im_CheckRequest_Items>().GetData().Where(i => i.Im_CheckRequset_Shipping_Method_ID == ship.ID)
                //            .Select(i => i.Im_Initiator_ID).Distinct().ToList();
                //        initiatorsId.AddRange(ids);
                //        //end Items
                //        foreach (var itt in itemss)
                //        {
                //            var initiatir = uow.Repository<Im_Initiator>().GetData().Include(f => f.Country).Where(u => u.ID == itt.Im_Initiator_ID).FirstOrDefault();

                //            itt.InitiatorCountry = initiatir.Country.Ar_Name;
                //            //ask about qualitive group
                //            var qualId = initiatir.QualitativeGroup_Id;
                //            if (qualId != null)
                //            {
                //                itt.qualitiveGroupName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault().Name_Ar;
                //            }

                //            var itemShortNameId = initiatir.Item_ShortName_ID;
                //            if (itemShortNameId != null)
                //            {
                //                var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId).FirstOrDefault();
                //                itt.ItemShortNameAr = itemShortName.ShortName_Ar;
                //                itt.ItemShortNameEn = itemShortName.ShortName_En;

                //                var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
                //                if (stat != null)
                //                {
                //                    itt.Status = stat.Ar_Name;
                //                }
                //                var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
                //                if (prp != null)
                //                {
                //                    itt.Purpose = prp.Ar_Name;
                //                }
                //                var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
                //                if (subp != null)
                //                {
                //                    itt.subPartName = subp.Name_Ar;
                //                }

                //                itt.ItemName = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault().Name_Ar;
                //            }

                //            //List categories And lots

                //            var catAndLots = uow.Repository<Im_CheckRequest_Items_Lot_Category>().GetData().Where(d => d.Im_CheckRequest_Items_ID == itt.ImcheckReqItem_ID).Select(v => new categories_lots
                //            {
                //                Im_checkReqItems_ID = v.Im_CheckRequest_Items_ID,
                //                ItemCategory_ID = v.ItemCategory_ID,
                //                Size = v.Size,
                //                Package_Count = v.Package_Count,
                //                Package_Weight = v.Package_Weight,
                //                Units_Number = v.Units_Number,
                //                packageTypeID = v.Package_Type_ID,
                //                GrossWeight = v.GrossWeight,
                //                packageMaterialID = v.Package_Material_ID,
                //                Lot_Number = v.Lot_Number,



                //            })
                //              .ToList();
                //            foreach (var ctt in catAndLots)
                //            {
                //                var ptypec = uow.Repository<Package_Type>().GetData().Where(d => d.ID == ctt.packageTypeID).FirstOrDefault();
                //                if (ptypec != null)
                //                {
                //                    ctt.packageType = ptypec.Ar_Name;
                //                }


                //                var categ = uow.Repository<ItemCategory>().GetData().Where(g => g.ID == ctt.ItemCategory_ID).FirstOrDefault();
                //                if (categ != null)
                //                {
                //                    ctt.categoryName = categ.Name_Ar;
                //                }



                //            }
                //            itt.ItemCategories_lots = catAndLots;

                //        }
                //        //constrains
                //        foreach (var init in itemss)
                //        {
                //            var initiatorId = init.Im_Initiator_ID;
                //            var initiatir = uow.Repository<Im_Initiator>().GetData().Where(u => u.ID == initiatorId).FirstOrDefault();

                //            var ItemShortName = initiatir.Item_ShortName_ID;
                //            var qualId = initiatir.QualitativeGroup_Id;
                //            var conTextAr = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

                //                 r.Im_CountryConstrain_Text.ConstrainText_Ar
                //            //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                //            ).ToList();
                //            var conTextEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>


                //             r.Im_CountryConstrain_Text.ConstrainText_En
                //            ).ToList();
                //            var constr = new constrains();
                //            constr.texts_Ar = conTextAr;
                //            constr.text_En = conTextEn;
                //            var pp = new List<ports>();

                //            if (ItemShortName != null)
                //            {
                //                pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Item_ShortName_ID == ItemShortName && p.IsActive == true).Select(w => new ports
                //                {

                //                    portId = w.Port_National_Id,
                //                    portTypeId = w.Port_Type_ID
                //                }).ToList();


                //            }
                //            if (qualId != null)
                //            {
                //                pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Id_QualitativeGroup == qualId && p.IsActive == true).Select(w => new ports
                //                {

                //                    portId = w.Port_National_Id,
                //                    portTypeId = w.Port_Type_ID
                //                }).ToList();


                //            }
                //            foreach (var prt in pp)
                //            {
                //                if (prt.portTypeId != null)
                //                {
                //                    var pTName = uow.Repository<Port_Type>().GetData().Where(m => m.ID == prt.portTypeId).FirstOrDefault();

                //                    if (pTName != null)
                //                    {
                //                        prt.portType = pTName.Name_Ar;

                //                    }

                //                }
                //                if (prt.portId != null)
                //                {
                //                    var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId).FirstOrDefault();

                //                    if (pName != null)
                //                    {
                //                        prt.portName = pName.Name_Ar;

                //                    }
                //                }
                //            }
                //            constr.itemConstrainPorts = pp;


                //            init.Itemconstrains = constr;

                //        }
                //        ship.Items_checkReq = itemss;
                //    }

                //    CheckRequestDetails.checkRequestShipping = shippingMethods;
                //    //Attachments
                //    CheckRequestDetails.Attachments = uow.Repository<A_AttachmentData>()
                //        .GetData().Where(v => v.RowId == CheckRequestDetails.Im_CheckRequest_ID && v.A_AttachmentTableNameId == 8 && v.User_Deletion_Id == null)
                //        .Select(x => new Attachments
                //        {
                //            Attachment_Number = x.Attachment_Number,
                //            AttachmentPath = x.AttachmentPath,
                //            Attachment_TypeName = x.Attachment_TypeName,
                //            StartDate = x.StartDate,
                //            EndDate = x.EndDate
                //        }).ToList();

                //    //emn
                //    initiatorsId = initiatorsId.Distinct().ToList();
                //    var itemsWithConstrains = new List<Items_checkReq>();
                //    foreach (var ids in initiatorsId)
                //    {
                //        var initiatir = uow.Repository<Im_Initiator>().GetData().Include(f => f.Country).Where(u => u.ID == ids).FirstOrDefault();
                //        var itt = new Items_checkReq();
                //        itt.InitiatorCountry = initiatir.Country.Ar_Name;
                //        //ask about qualitive group
                //        var qualId = initiatir.QualitativeGroup_Id;
                //        if (qualId != null)
                //        {
                //            itt.qualitiveGroupName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault().Name_Ar;
                //        }

                //        var itemShortNameId = initiatir.Item_ShortName_ID;
                //        if (itemShortNameId != null)
                //        {
                //            var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId).FirstOrDefault();
                //            itt.ItemShortNameAr = itemShortName.ShortName_Ar;
                //            itt.ItemShortNameEn = itemShortName.ShortName_En;

                //            var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
                //            if (stat != null)
                //            {
                //                itt.Status = stat.Ar_Name;
                //            }
                //            var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
                //            if (prp != null)
                //            {
                //                itt.Purpose = prp.Ar_Name;
                //            }
                //            var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
                //            if (subp != null)
                //            {
                //                itt.subPartName = subp.Name_Ar;
                //            }

                //            itt.ItemName = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault().Name_Ar;
                //        }

                //        //var ItemShortName = initiatir.Item_ShortName_ID;
                //        //var qualId = initiatir.QualitativeGroup_Id;
                //        var conTextAr = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == ids && i.IsActive == true).Select(r =>

                //             r.Im_CountryConstrain_Text.ConstrainText_Ar
                //        //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                //        ).ToList();
                //        var conTextEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == ids && i.IsActive == true).Select(r =>


                //         r.Im_CountryConstrain_Text.ConstrainText_En
                //        ).ToList();
                //        var constr = new constrains();
                //        constr.texts_Ar = conTextAr;
                //        constr.text_En = conTextEn;
                //        var pp = new List<ports>();

                //        if (itemShortNameId != null)
                //        {
                //            pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Item_ShortName_ID == itemShortNameId && p.IsActive == true).Select(w => new ports
                //            {

                //                portId = w.Port_National_Id,
                //                portTypeId = w.Port_Type_ID
                //            }).ToList();


                //        }
                //        if (qualId != null)
                //        {
                //            pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Id_QualitativeGroup == qualId && p.IsActive == true).Select(w => new ports
                //            {

                //                portId = w.Port_National_Id,
                //                portTypeId = w.Port_Type_ID
                //            }).ToList();


                //        }
                //        foreach (var prt in pp)
                //        {
                //            if (prt.portTypeId != null)
                //            {
                //                var pTName = uow.Repository<Port_Type>().GetData().Where(m => m.ID == prt.portTypeId).FirstOrDefault();

                //                if (pTName != null)
                //                {
                //                    prt.portType = pTName.Name_Ar;

                //                }

                //            }
                //            if (prt.portId != null)
                //            {
                //                var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId).FirstOrDefault();

                //                if (pName != null)
                //                {
                //                    prt.portName = pName.Name_Ar;

                //                }
                //            }
                //        }
                //        constr.itemConstrainPorts = pp;


                //        itt.Itemconstrains = constr;
                //        itemsWithConstrains.Add(itt);
                //    }
                //    CheckRequestDetails.itemsWithConstrains = itemsWithConstrains;

                //    ///////////////ESLAM///////////////


                //    var customs = uow.Repository<Im_CheckRequest_Customs_Message>()
                //         .GetData().Where(i => i.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID)
                //         .Select(v => new CustomsMessage
                //         {
                //             Im_CheckRequest_ID = v.Im_CheckRequest_ID,
                //             Customs_Certificate_Number = v.Customs_Certificate_Number,
                //             Shipment_Date = v.Shipment_Date,
                //             Certification_Date = v.Certification_Date,
                //             Arrival_Date = v.Arrival_Date,
                //             Manifest_Number = v.Manifest_Number,
                //             Shipping_Agency_ID = v.Shipping_Agency_ID,
                //             Certificate_Number_Each_Product = v.Certificate_Number_Each_Product,
                //             Shipping_Agency_Name = v.ShippingAgency.Name_Ar
                //         }).ToList();

                //    CheckRequestDetails.CustomsMessages = customs;
                //    ///////////////ESLAM///////////////
                //}
                #endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, committee);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> GetImExecutionCommittee
(string ImCheckRequest_Number, List<string> Device_Info)
        {
            {
                try
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                    string lang = Device_Info[2];
                    var data = (from rc in entities.Im_RequestCommittee
                                join cr in entities.Im_Execution on rc.ID equals cr.Im_RequestCommittee_Id
                                join itm in entities.Im_Execution_Items on cr.ID equals itm.Im_Execution_Id
                                join ccl in entities.Im_CommitteeCheckLocation on rc.ImCommitteeCheckLocation_ID equals ccl.ID
                                where
                                rc.CommitteeType_ID == 9
                                //9-- كود لجنه الاعدام = 
                                ////join itg in entities.ItemCategories on rc.ItemCategories_ID equals itg.ID
                                ////join it in entities.Items on itg.Item_ID equals it.ID
                                ////// from fcec in iis.DefaultIfEmpty()
                                //where lot.FarmCommittee_ID == FarmCommittee_ID
                                select new Im_Execution_CommDTO
                                {
                                    ID = rc.ID,
                                    ImCheckRequest_ID = rc.ImCheckRequest_ID,
                                    Delegation_Date = rc.Delegation_Date,
                                    StartTime = rc.StartTime,
                                    EndTime = rc.EndTime,
                                    IsApproved = rc.IsApproved,
                                    IsFinishedAll = rc.IsFinishedAll,
                                    Status = rc.Status,
                                    Execution_Place = cr.Execution_Place,
                                    Execution_Method = cr.Execution_Method,
                                    Execution_File = cr.Execution_File
                                }).ToList();

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
                }
                catch (Exception ex)
                {
                    uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
                }
            }
        }

        public Dictionary<string, object> GetImReceiveCommittee
(string ImCheckRequest_Number, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var data = (from dc in entities.Im_PermissionItem_Division_Custody_DismissCommittee
                            join rc in entities.Im_RequestCommittee on dc.Im_RequestCommittee_Id equals rc.ID
                            join crc in entities.Im_PermissionItem_Division_Custody_ReceiveCommittee on dc.ID equals crc.Im_PermissionItem_Division_Custody_DismissCommittee_Id

                            where
                            rc.CommitteeType_ID == 8




                            //9-- كود لجنه الاعدام = 
                            ////join itg in entities.ItemCategories on rc.ItemCategories_ID equals itg.ID
                            ////join it in entities.Items on itg.Item_ID equals it.ID
                            ////// from fcec in iis.DefaultIfEmpty()
                            //where lot.FarmCommittee_ID == FarmCommittee_ID


                            //select new Im_ReceiveCommitteeDTO
                            select new Im_ReceiveCommitteeDTO
                            {
                                ID = rc.ID,
                                ImCheckRequest_ID = rc.ImCheckRequest_ID,
                                Delegation_Date = rc.Delegation_Date,
                                StartTime = rc.StartTime,
                                EndTime = rc.EndTime,
                                IsApproved = rc.IsApproved,
                                IsFinishedAll = rc.IsFinishedAll,
                                Status = rc.Status,
                                Dismiss_Date = dc.Dismiss_Date,
                                DismissTime = dc.DismissTime,
                                Im_PermissionItem_Division_Custody_Id = dc.Im_PermissionItem_Division_Custody_Id,
                                Receive_Date = crc.Receive_Date,
                                ReceiveTime = crc.ReceiveTime,
                                //IsApproved=crc.IsApproved,
                                //Status= crc.Status
                            }).ToList();
                #region MyRegion


                //if (CheckRequestDetails != null)
                //{


                //    if (CheckRequestDetails.ImporterType_Id == 6)
                //    {
                //        CheckRequestDetails.ImporterName = uow.Repository<Company_National>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                //        CheckRequestDetails.ImporterAddress = uow.Repository<Company_National>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Address_Ar : s.Address_En).FirstOrDefault();
                //        CheckRequestDetails.ImporterType = "شركة";
                //    }
                //    else if (CheckRequestDetails.ImporterType_Id == 7)
                //    {
                //        CheckRequestDetails.ImporterName = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Name_Ar : s.Name_En).FirstOrDefault();
                //        CheckRequestDetails.ImporterAddress = uow.Repository<Public_Organization>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => lang == "1" ? s.Address_Ar : s.Address_En).FirstOrDefault();
                //        CheckRequestDetails.ImporterType = "هيئة";
                //    }
                //    else
                //    {
                //        CheckRequestDetails.ImporterName = uow.Repository<Person>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => s.Name).FirstOrDefault();
                //        CheckRequestDetails.ImporterAddress = uow.Repository<Person>().GetData().Where(a => a.ID == CheckRequestDetails.Importer_ID).Select(s => s.Address).FirstOrDefault();
                //        CheckRequestDetails.ImporterType = "فرد";
                //    }
                //    // enter portNational or international
                //    //var porttransitId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == CheckRequestDetails.Im_RequestData_ID && d.ReqPortType_ID ==11).FirstOrDefault().Port_ID;
                //    //var isNationalTransit = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == CheckRequestDetails.Im_RequestData_ID && d.ReqPortType_ID == 11).FirstOrDefault().IsNational;
                //    Nullable<int> portArriveId = null;
                //    var portArrive = uow.Repository<Im_CheckRequest_Port>().GetData().Where(d => d.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID && d.ReqPortType_ID == 10 && d.User_Deletion_Id == null).FirstOrDefault();
                //    if (portArrive != null)
                //    {
                //        portArriveId = portArrive.Port_ID;
                //    }
                //    // var portTypeId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == CheckRequestDetails.Im_RequestData_ID).FirstOrDefault().ReqPortType_ID;
                //    var xx = uow.Repository<PortNational>().GetData().Include(i => i.Port_Type).Where(m => m.ID == portArriveId).FirstOrDefault();
                //    if (xx != null)
                //    {
                //        CheckRequestDetails.ArrivePortName = xx.Name_Ar;
                //        CheckRequestDetails.ArrivePortType = lang == "1" ? xx.Port_Type.Name_Ar : xx.Port_Type.Name_En;
                //    }

                //    //transport port 
                //    Nullable<int> portTransId = null;
                //    var portTrans = uow.Repository<Im_CheckRequest_Port>().GetData().Where(d => d.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID && d.ReqPortType_ID == 9 && d.User_Deletion_Id == null).FirstOrDefault();
                //    if (portTrans != null)
                //    {
                //        portTransId = portTrans.Port_ID;
                //    } //var portTypeId = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == permissionPrintDetails.Im_RequestData_ID).FirstOrDefault().ReqPortType_ID;
                //    var tt = uow.Repository<Port_International>().GetData().Include(i => i.Port_Type).Where(m => m.ID == portTransId).FirstOrDefault();
                //    if (tt != null)
                //    {
                //        CheckRequestDetails.TransportPortName = tt.Name_Ar;
                //        CheckRequestDetails.TransportPortType = lang == "1" ? tt.Port_Type.Name_Ar : tt.Port_Type.Name_En;
                //    }
                //    //end

                //    var shipp = uow.Repository<Shipment_Mean>().GetData().Where(c => c.ID == CheckRequestDetails.Shipment_Mean_Id).FirstOrDefault();
                //    if (shipp != null)
                //    {
                //        CheckRequestDetails.Shipment_MeanName = shipp.Ar_Name;
                //    }
                //    var transport = uow.Repository<Transport_Mean>().GetData().Where(c => c.ID == CheckRequestDetails.Transport_Mean_Id).FirstOrDefault();
                //    if (transport != null)
                //    {
                //        CheckRequestDetails.Transport_MeanName = transport.Ar_Name;
                //    }
                //    //var isNationalArrive = uow.Repository<Im_Request_Port>().GetData().Where(d => d.Im_RequestData_ID == CheckRequestDetails.Im_RequestData_ID && d.ReqPortType_ID == 10).FirstOrDefault().IsNational;
                //    // get companies out egypt

                //    var com = uow.Repository<Im_CheckRequestData_Extra>().GetData().Where(i => i.Im_CheckRequest_Data_ID == CheckRequestDetails.Im_CheckRequestData_ID).Select(v => new Importers
                //    {
                //        ImporterCompany = v.ImportCompany,
                //        ImporterCompanyAddress = v.ImporeterCompanyAddress,
                //        ImporterCompanyEn = v.ImportCompany_EN,
                //        ImporterCompanyAddressEn = v.ImporeterCompanyAddress_EN
                //    }).ToList();
                //    CheckRequestDetails.ImportersCompanies = com;
                //    //end companies
                //    var shippingMethods = uow.Repository<Im_CheckRequset_Shipping_Method>().GetData().Where(c => c.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID).Select(n => new checkRequestShipping
                //    {
                //        ID = n.ID,
                //        containers_ID = n.containers_ID,
                //        containers_type_ID = n.containers_type_ID,
                //        ShipholdNumber = n.ShipholdNumber,
                //        ContainerNumber = n.ContainerNumber,
                //        NavigationalNumber = n.NavigationalNumber,
                //        Total_Weight = n.Total_Weight
                //    }).ToList();
                //    // distinct items for constrains
                //    var initiatorsId = new List<long?>();

                //    foreach (var ship in shippingMethods)
                //    {
                //        var container = uow.Repository<A_SystemCode>().GetData().FirstOrDefault(c => c.Id == ship.containers_ID && c.SystemCodeTypeId == 28);
                //        if (container != null)
                //        {
                //            ship.containerName = lang == "1" ? container.ValueName : container.ValueNameEN;
                //        }
                //        var containertype = uow.Repository<A_SystemCode>().GetData().FirstOrDefault(c => c.Id == ship.containers_type_ID && c.SystemCodeTypeId == 29);
                //        if (container != null)
                //        {
                //            ship.containerType = lang == "1" ? containertype.ValueName : containertype.ValueNameEN;
                //        }
                //        //Items
                //        var itemss = uow.Repository<Im_CheckRequest_Items>().GetData().Where(i => i.Im_CheckRequset_Shipping_Method_ID == ship.ID)
                //            .Select(v => new Items_checkReq
                //            {
                //                Im_Initiator_ID = v.Im_Initiator_ID,
                //                ImcheckReqItem_ID = v.ID,
                //                ImcheckReqshippedMethod_ID = v.Im_CheckRequset_Shipping_Method_ID,
                //                Size = v.Size,
                //                Package_Count = v.Package_Count,
                //                Package_Weight = v.Package_Weight,
                //                Units_Number = v.Units_Number,
                //                packageTypeID = v.Package_Type_ID,
                //                GrossWeight = v.GrossWeight

                //            }).Distinct().ToList();
                //        var ids = uow.Repository<Im_CheckRequest_Items>().GetData().Where(i => i.Im_CheckRequset_Shipping_Method_ID == ship.ID)
                //            .Select(i => i.Im_Initiator_ID).Distinct().ToList();
                //        initiatorsId.AddRange(ids);
                //        //end Items
                //        foreach (var itt in itemss)
                //        {
                //            var initiatir = uow.Repository<Im_Initiator>().GetData().Include(f => f.Country).Where(u => u.ID == itt.Im_Initiator_ID).FirstOrDefault();

                //            itt.InitiatorCountry = initiatir.Country.Ar_Name;
                //            //ask about qualitive group
                //            var qualId = initiatir.QualitativeGroup_Id;
                //            if (qualId != null)
                //            {
                //                itt.qualitiveGroupName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault().Name_Ar;
                //            }

                //            var itemShortNameId = initiatir.Item_ShortName_ID;
                //            if (itemShortNameId != null)
                //            {
                //                var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId).FirstOrDefault();
                //                itt.ItemShortNameAr = itemShortName.ShortName_Ar;
                //                itt.ItemShortNameEn = itemShortName.ShortName_En;

                //                var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
                //                if (stat != null)
                //                {
                //                    itt.Status = stat.Ar_Name;
                //                }
                //                var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
                //                if (prp != null)
                //                {
                //                    itt.Purpose = prp.Ar_Name;
                //                }
                //                var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
                //                if (subp != null)
                //                {
                //                    itt.subPartName = subp.Name_Ar;
                //                }

                //                itt.ItemName = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault().Name_Ar;
                //            }

                //            //List categories And lots

                //            var catAndLots = uow.Repository<Im_CheckRequest_Items_Lot_Category>().GetData().Where(d => d.Im_CheckRequest_Items_ID == itt.ImcheckReqItem_ID).Select(v => new categories_lots
                //            {
                //                Im_checkReqItems_ID = v.Im_CheckRequest_Items_ID,
                //                ItemCategory_ID = v.ItemCategory_ID,
                //                Size = v.Size,
                //                Package_Count = v.Package_Count,
                //                Package_Weight = v.Package_Weight,
                //                Units_Number = v.Units_Number,
                //                packageTypeID = v.Package_Type_ID,
                //                GrossWeight = v.GrossWeight,
                //                packageMaterialID = v.Package_Material_ID,
                //                Lot_Number = v.Lot_Number,



                //            })
                //              .ToList();
                //            foreach (var ctt in catAndLots)
                //            {
                //                var ptypec = uow.Repository<Package_Type>().GetData().Where(d => d.ID == ctt.packageTypeID).FirstOrDefault();
                //                if (ptypec != null)
                //                {
                //                    ctt.packageType = ptypec.Ar_Name;
                //                }


                //                var categ = uow.Repository<ItemCategory>().GetData().Where(g => g.ID == ctt.ItemCategory_ID).FirstOrDefault();
                //                if (categ != null)
                //                {
                //                    ctt.categoryName = categ.Name_Ar;
                //                }



                //            }
                //            itt.ItemCategories_lots = catAndLots;

                //        }
                //        //constrains
                //        foreach (var init in itemss)
                //        {
                //            var initiatorId = init.Im_Initiator_ID;
                //            var initiatir = uow.Repository<Im_Initiator>().GetData().Where(u => u.ID == initiatorId).FirstOrDefault();

                //            var ItemShortName = initiatir.Item_ShortName_ID;
                //            var qualId = initiatir.QualitativeGroup_Id;
                //            var conTextAr = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>

                //                 r.Im_CountryConstrain_Text.ConstrainText_Ar
                //            //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                //            ).ToList();
                //            var conTextEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == initiatorId && i.IsActive == true).Select(r =>


                //             r.Im_CountryConstrain_Text.ConstrainText_En
                //            ).ToList();
                //            var constr = new constrains();
                //            constr.texts_Ar = conTextAr;
                //            constr.text_En = conTextEn;
                //            var pp = new List<ports>();

                //            if (ItemShortName != null)
                //            {
                //                pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Item_ShortName_ID == ItemShortName && p.IsActive == true).Select(w => new ports
                //                {

                //                    portId = w.Port_National_Id,
                //                    portTypeId = w.Port_Type_ID
                //                }).ToList();


                //            }
                //            if (qualId != null)
                //            {
                //                pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Id_QualitativeGroup == qualId && p.IsActive == true).Select(w => new ports
                //                {

                //                    portId = w.Port_National_Id,
                //                    portTypeId = w.Port_Type_ID
                //                }).ToList();


                //            }
                //            foreach (var prt in pp)
                //            {
                //                if (prt.portTypeId != null)
                //                {
                //                    var pTName = uow.Repository<Port_Type>().GetData().Where(m => m.ID == prt.portTypeId).FirstOrDefault();

                //                    if (pTName != null)
                //                    {
                //                        prt.portType = pTName.Name_Ar;

                //                    }

                //                }
                //                if (prt.portId != null)
                //                {
                //                    var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId).FirstOrDefault();

                //                    if (pName != null)
                //                    {
                //                        prt.portName = pName.Name_Ar;

                //                    }
                //                }
                //            }
                //            constr.itemConstrainPorts = pp;


                //            init.Itemconstrains = constr;

                //        }
                //        ship.Items_checkReq = itemss;
                //    }

                //    CheckRequestDetails.checkRequestShipping = shippingMethods;
                //    //Attachments
                //    CheckRequestDetails.Attachments = uow.Repository<A_AttachmentData>()
                //        .GetData().Where(v => v.RowId == CheckRequestDetails.Im_CheckRequest_ID && v.A_AttachmentTableNameId == 8 && v.User_Deletion_Id == null)
                //        .Select(x => new Attachments
                //        {
                //            Attachment_Number = x.Attachment_Number,
                //            AttachmentPath = x.AttachmentPath,
                //            Attachment_TypeName = x.Attachment_TypeName,
                //            StartDate = x.StartDate,
                //            EndDate = x.EndDate
                //        }).ToList();

                //    //emn
                //    initiatorsId = initiatorsId.Distinct().ToList();
                //    var itemsWithConstrains = new List<Items_checkReq>();
                //    foreach (var ids in initiatorsId)
                //    {
                //        var initiatir = uow.Repository<Im_Initiator>().GetData().Include(f => f.Country).Where(u => u.ID == ids).FirstOrDefault();
                //        var itt = new Items_checkReq();
                //        itt.InitiatorCountry = initiatir.Country.Ar_Name;
                //        //ask about qualitive group
                //        var qualId = initiatir.QualitativeGroup_Id;
                //        if (qualId != null)
                //        {
                //            itt.qualitiveGroupName = uow.Repository<QualitativeGroup>().GetData().Where(y => y.Id == qualId).FirstOrDefault().Name_Ar;
                //        }

                //        var itemShortNameId = initiatir.Item_ShortName_ID;
                //        if (itemShortNameId != null)
                //        {
                //            var itemShortName = uow.Repository<Item_ShortName>().GetData().Where(a => a.ID == itemShortNameId).FirstOrDefault();
                //            itt.ItemShortNameAr = itemShortName.ShortName_Ar;
                //            itt.ItemShortNameEn = itemShortName.ShortName_En;

                //            var stat = uow.Repository<Item_Status>().GetData().Where(s => s.ID == itemShortName.Item_Status_ID).FirstOrDefault();
                //            if (stat != null)
                //            {
                //                itt.Status = stat.Ar_Name;
                //            }
                //            var prp = uow.Repository<Item_Purpose>().GetData().Where(s => s.ID == itemShortName.Item_Purpose_ID).FirstOrDefault();
                //            if (prp != null)
                //            {
                //                itt.Purpose = prp.Ar_Name;
                //            }
                //            var subp = uow.Repository<SubPart>().GetData().Where(s => s.ID == itemShortName.SubPart_ID).FirstOrDefault();
                //            if (subp != null)
                //            {
                //                itt.subPartName = subp.Name_Ar;
                //            }

                //            itt.ItemName = uow.Repository<Item>().GetData().Where(a => a.ID == itemShortName.Item_ID).FirstOrDefault().Name_Ar;
                //        }

                //        //var ItemShortName = initiatir.Item_ShortName_ID;
                //        //var qualId = initiatir.QualitativeGroup_Id;
                //        var conTextAr = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == ids && i.IsActive == true).Select(r =>

                //             r.Im_CountryConstrain_Text.ConstrainText_Ar
                //        //text_En = r.Im_CountryConstrain_Text.ConstrainText_En,
                //        ).ToList();
                //        var conTextEn = uow.Repository<Im_Constrain_Initiator_Text>().GetData().Include(k => k.Im_CountryConstrain_Text).Where(i => i.Im_Initiator_ID == ids && i.IsActive == true).Select(r =>


                //         r.Im_CountryConstrain_Text.ConstrainText_En
                //        ).ToList();
                //        var constr = new constrains();
                //        constr.texts_Ar = conTextAr;
                //        constr.text_En = conTextEn;
                //        var pp = new List<ports>();

                //        if (itemShortNameId != null)
                //        {
                //            pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Item_ShortName_ID == itemShortNameId && p.IsActive == true).Select(w => new ports
                //            {

                //                portId = w.Port_National_Id,
                //                portTypeId = w.Port_Type_ID
                //            }).ToList();


                //        }
                //        if (qualId != null)
                //        {
                //            pp = uow.Repository<Im_CountryConstrain_ArrivalPort>().GetData().Where(p => p.Id_QualitativeGroup == qualId && p.IsActive == true).Select(w => new ports
                //            {

                //                portId = w.Port_National_Id,
                //                portTypeId = w.Port_Type_ID
                //            }).ToList();


                //        }
                //        foreach (var prt in pp)
                //        {
                //            if (prt.portTypeId != null)
                //            {
                //                var pTName = uow.Repository<Port_Type>().GetData().Where(m => m.ID == prt.portTypeId).FirstOrDefault();

                //                if (pTName != null)
                //                {
                //                    prt.portType = pTName.Name_Ar;

                //                }

                //            }
                //            if (prt.portId != null)
                //            {
                //                var pName = uow.Repository<PortNational>().GetData().Where(m => m.ID == prt.portId).FirstOrDefault();

                //                if (pName != null)
                //                {
                //                    prt.portName = pName.Name_Ar;

                //                }
                //            }
                //        }
                //        constr.itemConstrainPorts = pp;


                //        itt.Itemconstrains = constr;
                //        itemsWithConstrains.Add(itt);
                //    }
                //    CheckRequestDetails.itemsWithConstrains = itemsWithConstrains;

                //    ///////////////ESLAM///////////////


                //    var customs = uow.Repository<Im_CheckRequest_Customs_Message>()
                //         .GetData().Where(i => i.Im_CheckRequest_ID == CheckRequestDetails.Im_CheckRequest_ID)
                //         .Select(v => new CustomsMessage
                //         {
                //             Im_CheckRequest_ID = v.Im_CheckRequest_ID,
                //             Customs_Certificate_Number = v.Customs_Certificate_Number,
                //             Shipment_Date = v.Shipment_Date,
                //             Certification_Date = v.Certification_Date,
                //             Arrival_Date = v.Arrival_Date,
                //             Manifest_Number = v.Manifest_Number,
                //             Shipping_Agency_ID = v.Shipping_Agency_ID,
                //             Certificate_Number_Each_Product = v.Certificate_Number_Each_Product,
                //             Shipping_Agency_Name = v.ShippingAgency.Name_Ar
                //         }).ToList();

                //    CheckRequestDetails.CustomsMessages = customs;
                //    ///////////////ESLAM///////////////
                //}
                #endregion
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        //        //EslamScriptEXECUTION
        //        select rc.ID, rc.ImCheckRequest_ID, rc.Delegation_Date, rc.StartTime[بدابة الوقت]
        //, rc.EndTime[نهاية وقت اللجنة], rc.IsApproved , rc.IsFinishedAll[خلص_ولا], rc.Status[قبول_ورفض_العميل]
        //, cr.Execution_Place, cr.Execution_Method, cr.Execution_File

        //from Im_RequestCommittee rc
        //inner join Im_Execution cr on rc.ID= cr.Im_RequestCommittee_Id
        //inner join Im_Execution_Items itm on itm.Im_Execution_Id = cr.ID
        //inner join Im_CommitteeCheckLocation ccl on ccl.ID= rc.ImCommitteeCheckLocation_ID
        //where rc.CommitteeType_ID= 9-- كود لجنه الاعدام= 9
        //                 //END_EslamScriptEXECUTION
        //    {
        //        Dictionary<string, object> dic = new Dictionary<string, object>();

        //        Int64 data_Count = 0;


        //        PlantQuarantineEntities entities = new PlantQuarantineEntities();
        //        var data = (from rc in entities.Im_RequestCommittee
        //                    join cr in entities.Im_CommitteeResult on rc.ID equals cr.Committee_ID
        //                    join lot in entities.Im_CheckRequest_Items_Lot_Category  on   cr.Im_Request_Item_Id equals lot.ID
        //                    join itm in entities.Im_CheckRequest_Items  on  lot.Im_CheckRequest_Items_ID equals itm.ID
        //                    join ini in entities.Im_Initiator  on   itm.Im_Initiator_ID equals ini.ID
        //                    join sn in entities.Item_ShortName  on  ini.Item_ShortName_ID equals sn.ID 
        //                    join i in entities.Items  on  sn.Item_ID equals i.ID 
        //                    join ccf in entities.Im_CommitteeResult_Confirm  on  cr.ID equals ccf.Im_CommitteeResult_ID 
        //                    where rc.ImCheckRequest_ID == 70

        //                    ////join itg in entities.ItemCategories on rc.ItemCategories_ID equals itg.ID
        //                    ////join it in entities.Items on itg.Item_ID equals it.ID
        //                    ////// from fcec in iis.DefaultIfEmpty()
        //                    //where lot.FarmCommittee_ID == FarmCommittee_ID
        //                    select new Im_Check_ComitteDTO
        //                    {
        //                     Item_Name_Ar=  i.Name_Ar,
        //                        //sn.ShortName_Ar,
        //                        //rc.ID,rc.ImCheckRequest_ID,
        //                        //rc.Delegation_Date,
        //                        //rc.StartTime,
        //                        //rc.EndTime,
        //                        //rc.IsApproved ,
        //                        //rc.IsFinishedAll , 
        //                        //rc.Status,
        //                        //cr.EmployeeId,
        //                        //cr.Date,
        //                        //cr.Weight,
        //                        //cr.QuantitySize,
        //                        //cr.IsAdminFinalResult,
        //                        //cr.Notes,
        //                        //ccf.EmployeeId,
        //                        //ccf.IsAccepted,
        //                        //ccf.Notes

        //                    }).ToList();
        //        //eman
        //        //getno of employee for committee
        //        var emps = uow.Repository<CommitteeEmployee>().GetData().Where(c => c.Committee_ID == FarmCommittee_ID && c.OperationType == 78 && c.ISAdmin == false).ToList();
        //        var noEmp = emps.Count();
        //        dbPrivilageEntities priv = new dbPrivilageEntities();

        //        //get emps result
        //        foreach (var exam in data)
        //        {
        //            //eman admin name 
        //            var adminname = uow.Repository<CommitteeEmployee>().GetData().Where(c => c.Committee_ID == FarmCommittee_ID && c.OperationType == 78 && c.ISAdmin == true).FirstOrDefault();
        //            exam.AdminName = priv.PR_User.Where(p => p.Id == adminname.Employee_Id).Select(e => e.FullName).FirstOrDefault();


        //            var empsres = uow.Repository<Farm_Committee_Examination_Confirm>().GetData().Where(c => c.Farm_Committee_Exmination_ID == exam.ID).ToList();
        //            exam.IsTotalRes = false;

        //            if (empsres.Count == noEmp)
        //            {
        //                if (exam.IsAccepted_Admin != null)
        //                {
        //                    exam.IsTotalRes = true;
        //                }
        //            }
        //            exam.employeeRes = empsres.Select(v => new empResult
        //            {
        //                Notes_Confirm = v.Notes,
        //                IsAccepted_Confirm = v.IsAccepted,
        //                Date = v.Date,
        //                EmployeeId = v.EmployeeId,
        //                empName = priv.PR_User.Where(p => p.Id == v.EmployeeId).Select(e => e.FullName).FirstOrDefault()
        //            }).ToList();

        //        }
        //        //check if all confirmed
        //        var ifAllConfirmed = 1;
        //        var notConfirm = data.Where(n => n.Admin_Confirmation == null).ToList();
        //        if (notConfirm.Count > 0)
        //        {
        //            ifAllConfirmed = 0;
        //        }
        //        var ifAppearCategories = 1;
        //        if (data.Where(b => b.IsTotalRes == false).Count() > 0)
        //        {
        //            ifAppearCategories = 0;

        //        }

        //        string lang = Device_Info[2];
        //        data_Count = data.Count();

        //        dic.Add("Count_Data", data_Count);
        //        dic.Add("Farm_Committee_Examination_Data", data);
        //        dic.Add("ifAllConfirmed", ifAllConfirmed);
        //        dic.Add("ifAppearCategories", ifAppearCategories);

        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}










        public Dictionary<string, object> Insert(Im_Check_ComitteDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Update(Im_Check_ComitteDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }

}