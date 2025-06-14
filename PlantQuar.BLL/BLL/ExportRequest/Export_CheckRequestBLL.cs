using AutoMapper;
using PlantQuar.BLL.BLL.ExportImport;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequest
{
   public class Export_CheckRequestBLL : IGenericBLL<Export_CheckRequestDTO>
    {
        private UnitOfWork uow;
        public Export_CheckRequestBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> Get_PlantConstrain
            (int plantId, int purposeId, int statusId, int partType, int importerCountryId, int transitCountryId, List<string> Device_Info)
        {
            var constrains = uow.Repository<Custom_Constrains>().SQLQuery
               ("GetPlantConstrain @ProdPlant_ID, @PlantPart_ID, @ProductStatus_ID, @Purpose_ID, @ImportCountry, @TransitCountry, @IsExport, @ISCount",
                 new SqlParameter("ProdPlant_ID", SqlDbType.BigInt) { Value = (Int64)plantId },
                 new SqlParameter("PlantPart_ID", SqlDbType.TinyInt) { Value = (byte)partType },
                 new SqlParameter("ProductStatus_ID", SqlDbType.TinyInt) { Value = (byte)statusId },
                 new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = (byte)purposeId },
                 new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importerCountryId },
                 new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitCountryId },
                 new SqlParameter("IsExport", SqlDbType.Bit) { Value = 1 },
                 new SqlParameter("ISCount", SqlDbType.Bit) { Value = 0 }
               ).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, constrains);
        }

        public Dictionary<string, object> Get_ProductConstrain
            (int productId, int purposeId, int statusId, int importerCountryId, int transitCountryId, List<string> Device_Info)
        {
            var constrains = uow.Repository<Custom_Constrains>().SQLQuery
               ("GetProductConstrain @ProdPlant_ID, @ProductStatus_ID, @Purpose_ID, @ImportCountry, @TransitCountry, @IsExport, @ISCount",
                 new SqlParameter("ProdPlant_ID", SqlDbType.BigInt) { Value = (Int64)productId },
                 new SqlParameter("ProductStatus_ID", SqlDbType.TinyInt) { Value = (byte)statusId },
                 new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = (byte)purposeId },
                 new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importerCountryId },
                 new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitCountryId },
                 new SqlParameter("IsExport", SqlDbType.Bit) { Value = 1 },
                 new SqlParameter("ISCount", SqlDbType.Bit) { Value = 0 }
               ).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, constrains);
        }

        public Dictionary<string, object> Get_LiableAliveConstrain
            (long liableId, int purposeId, int statusId, int phaseId, long importerCountryId, long transitCountryId, List<string> Device_Info)
        {
            try
            {

                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("ProdPlant_ID", SqlDbType.BigInt);
                paramters_Type.Add("BiologicalPhase_ID", SqlDbType.Int);
                paramters_Type.Add("Purpose_ID", SqlDbType.Int);
                paramters_Type.Add("LiableStatus_ID", SqlDbType.Int);
                paramters_Type.Add("ImportCountry", SqlDbType.BigInt);
                paramters_Type.Add("TransitCountry", SqlDbType.BigInt);
                paramters_Type.Add("IsExport", SqlDbType.Bit);
                paramters_Type.Add("ISCount", SqlDbType.Bit);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("ProdPlant_ID", liableId.ToString());
                paramters_Data.Add("BiologicalPhase_ID", phaseId.ToString());
                paramters_Data.Add("Purpose_ID", purposeId.ToString());
                paramters_Data.Add("LiableStatus_ID", statusId.ToString());
                paramters_Data.Add("ImportCountry", importerCountryId.ToString());
                paramters_Data.Add("TransitCountry", transitCountryId.ToString());
                paramters_Data.Add("IsExport", "1");
                paramters_Data.Add("ISCount", "0");

                var constrains = uow.Repository<Custom_Constrains>()
                    .CallStored("GetLiableItems_AliveConstrain", paramters_Type,
                      paramters_Data, Device_Info).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, constrains);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);

            }
        }

        public Dictionary<string, object> Get_Liable_NotAliveConstrain
            (int liableId, int purposeId, int statusId, int importerCountryId, int transitCountryId, List<string> Device_Info)
        {

            //   @ProdPlant_ID bigint,
            //   @LiableStatus_ID    int,
            //   @Purpose_ID tinyint, 
            //   @ImportCountry bigint,
            //   @TransitCountry bigint  ,
            //   @IsExport bit = 1,
            //   @ISCount bit = 0

            //var constrains = uow.Repository<Custom_Constrains>().SQLQuery
            //   ("GetLiableItems_NotAliveConstrain @ProdPlant_ID, @LiableStatus_ID, @Purpose_ID, @ImportCountry, @TransitCountry, @IsExport, @ISCount",
            //     new SqlParameter("ProdPlant_ID", SqlDbType.BigInt) { Value = (Int64)liableId },
            //     new SqlParameter("LiableStatus_ID", SqlDbType.Int) { Value = statusId },
            //     new SqlParameter("Purpose_ID", SqlDbType.TinyInt) { Value = (byte)purposeId },
            //     new SqlParameter("ImportCountry", SqlDbType.BigInt) { Value = (Int64)importerCountryId },
            //     new SqlParameter("TransitCountry", SqlDbType.BigInt) { Value = (Int64)transitCountryId },
            //      new SqlParameter("IsExport", SqlDbType.Bit) { Value = 1 },
            //     new SqlParameter("ISCount", SqlDbType.Bit) { Value = 0 }
            //   ).ToList();


            Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
            paramters_Type.Add("ProdPlant_ID", SqlDbType.BigInt);
            paramters_Type.Add("LiableStatus_ID", SqlDbType.Int);
            paramters_Type.Add("Purpose_ID", SqlDbType.Int);
            paramters_Type.Add("ImportCountry", SqlDbType.BigInt);
            paramters_Type.Add("TransitCountry", SqlDbType.BigInt);
            paramters_Type.Add("IsExport", SqlDbType.Bit);
            paramters_Type.Add("ISCount", SqlDbType.Bit);

            Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
            paramters_Data.Add("ProdPlant_ID", liableId.ToString());
            paramters_Data.Add("LiableStatus_ID", statusId.ToString());
            paramters_Data.Add("Purpose_ID", purposeId.ToString());
            paramters_Data.Add("ImportCountry", importerCountryId.ToString());
            paramters_Data.Add("TransitCountry", transitCountryId.ToString());
            paramters_Data.Add("IsExport", "1");
            paramters_Data.Add("ISCount", "0");

            var constrains = uow.Repository<Custom_Constrains>()
                .CallStored("GetLiableItems_NotAliveConstrain", paramters_Type,
                  paramters_Data, Device_Info).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, constrains);
        }

        //**************************************************************************************//
        public Dictionary<string, object> GetAll_ByUser_Date(short User_Id, DateTime check_Date, List<string> Device_Info)
        {
            ExportImportActivityBLL exportImportActivityBLL = new ExportImportActivityBLL();
            var request = exportImportActivityBLL.GetAll_ByUser_Date(User_Id, check_Date, 1, 1, Device_Info)["obj"];
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);
        }
        public Dictionary<string, object> Get_ExportRequestFor_Admin(short CommitteeType_ID, short IsApproved, string requestnumber, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("IsApproved", SqlDbType.SmallInt);
                paramters_Type.Add("CommitteeType_ID", SqlDbType.SmallInt);
                paramters_Type.Add("requestNumber", SqlDbType.VarChar);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("IsApproved", IsApproved.ToString());
                paramters_Data.Add("CommitteeType_ID", CommitteeType_ID.ToString());
                paramters_Data.Add("requestNumber", requestnumber == null ? "" : requestnumber);
                var request = uow.Repository<CheckRequest_AdminGetData_Result>().CallStored("CheckRequest_AdminGetData", paramters_Type, paramters_Data, Device_Info).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public Dictionary<string, object> Get_Check_RequestData(long checkRequest_Id, string CheckRequest_Number,
            byte Committee_Type_Id,
           byte IsGetLotData, byte IsGetConstrainData, List<string> Device_Info)
        {
            Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
            paramters_Type.Add("CheckRequest_Id", SqlDbType.BigInt);
            paramters_Type.Add("CheckRequest_Number", SqlDbType.VarChar);
            paramters_Type.Add("Committee_Type_Id", SqlDbType.TinyInt);
            paramters_Type.Add("IsGetLotData", SqlDbType.Char);
            paramters_Type.Add("IsGetConstrainData", SqlDbType.Char);

            Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
            paramters_Data.Add("CheckRequest_Id", checkRequest_Id.ToString());
            paramters_Data.Add("CheckRequest_Number", CheckRequest_Number);
            paramters_Data.Add("Committee_Type_Id", Committee_Type_Id.ToString());
            paramters_Data.Add("IsGetLotData", IsGetLotData.ToString());
            paramters_Data.Add("IsGetConstrainData", IsGetConstrainData.ToString());

            dynamic request;
            //for android group
            if (!bool.Parse(Device_Info[1]))
            {
                //android
                request = uow.Repository<CheckRequest_GetData_ResultDTO>().CallStored("CheckRequest_GetData_Android", paramters_Type,
            paramters_Data, Device_Info).FirstOrDefault();//.ToList(); it must return only 1 element
            }
            else
            {
                request = uow.Repository<CheckRequest_GetData_ResultDTO>().CallStored("CheckRequest_GetData", paramters_Type,
            paramters_Data, Device_Info).FirstOrDefault();//.ToList(); it must return only 1 element

            }
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);
        }

        public Dictionary<string, object> ApproveCheckReq(Export_CheckRequestDTO dto, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest CModel = uow.Repository<Ex_CheckRequest>().Findobject(dto.ID);
                CModel.IsAcceppted = dto.IsAcceppted;

                uow.SaveChanges();

                var empDTO = Mapper.Map<Ex_CheckRequest, Export_CheckRequestDTO>(CModel);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> FillDrop_RefuseReason(int refuse, List<string> Device_Info)
        {
            string lang = Device_Info[2];
            var data = uow.Repository<Refuse_Reason>().GetData()
                .Where(lab => lab.User_Deletion_Id == null 
                && lab.IsActive == true 
                && (lab.IsExport == 80 || lab.IsExport == 82));
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

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Insert(Export_CheckRequestDTO entity, List<string> Device_Info)
        {
            try
            {
                var CModel = Mapper.Map<Ex_CheckRequest>(entity);
                CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_seq");
                CModel = uow.Repository<Ex_CheckRequest>().InsertReturn(CModel);
                uow.SaveChanges();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, CModel.ID);
            }
            catch
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
            }
        }

        //InsertCustome
        public Dictionary<string, object> InsertCustomeRequest(Custome_ExCheckRequest checkReq, List<string> Device_Info)
        {
            try
            {
                Export_CheckRequestDTO req = new Export_CheckRequestDTO();
                Ex_CheckRequest_ExtraDTO extraReq = new Ex_CheckRequest_ExtraDTO();

                req.IS_OnlineOffline = false;
                //************************************//
                #region Request
                req.ExporterType_Id = checkReq.exporterTypeId;
                switch (checkReq.exporterTypeId)
                {
                    case 6:
                        req.Exporter_ID = checkReq.exportCompanyId;
                        extraReq.OwnerName = checkReq.ownerName_company;
                        extraReq.OwnerAddress = checkReq.ownerAddress_company;
                        extraReq.DelegateName = checkReq.delegateName_company;
                        extraReq.DelegateAddress = checkReq.delegateAddress_company;
                        break;
                    case 7:
                        req.Exporter_ID = checkReq.exportPublicOrgId;
                        extraReq.OwnerName = checkReq.ownerName_publicOrg;
                        extraReq.OwnerAddress = checkReq.ownerAddress_publicOrg;
                        extraReq.DelegateName = checkReq.delegateName_publicOrg;
                        extraReq.DelegateAddress = checkReq.delegateAddress_publicOrg;
                        break;
                    case 8:
                        ex_PersonDTO person = new ex_PersonDTO();
                        person.Name = checkReq.personName;
                        person.Address = checkReq.personAddress;
                        person.Country_ID = checkReq.personNationality;
                        person.Email = checkReq.personMail;
                        person.IDNumber = checkReq.personIDNum;
                        person.Job = checkReq.personJob;
                        person.Person_IDType = checkReq.personIdType;
                        person.Phone = checkReq.personPhone;

                        var personDal = Mapper.Map<Person>(person);
                        personDal.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Person_seq");
                        personDal = uow.Repository<Person>().InsertReturn(personDal);
                        uow.SaveChanges();

                        req.Exporter_ID = personDal.ID;
                        break;
                }

                req.IsActive = true;

                req.Outlet_ID = checkReq.outletID;
                req.GeneralAdmin_ID = checkReq.generalAdminID;
                req.ImportCountry_Id = checkReq.imporeterCountryId;

                req.User_Creation_Id = checkReq.User_Creation_Id;
                req.User_Creation_Date = checkReq.User_Creation_Date;

                req.Transport_Mean_Id = checkReq.transportMeanId;
                req.Shipment_Mean_Id = checkReq.shipmentMeanId;

                //إدارة/خدمة مصدرين + منفذ/قسم/محطة + التاريخ
                string reqNum = checkReq.generalAdminID + checkReq.outletID.ToString()
                    + checkReq.stationId.ToString() + checkReq.exporterTypeId + req.Exporter_ID
                    + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString()
                    + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                req.CheckRequest_Number = reqNum;

                var checkRequest = Mapper.Map<Ex_CheckRequest>(req);
                checkRequest.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_seq");
                checkRequest = uow.Repository<Ex_CheckRequest>().InsertReturn(checkRequest);
                uow.SaveChanges();
                req.ID = checkRequest.ID;
                //********************//
                extraReq.CheckRequest_ID = req.ID;
                extraReq.Reciever_Name = checkReq.recieverName;
                extraReq.Ship_Name = checkReq.shipName;
                extraReq.ImportCompany = checkReq.importerCompany;
                extraReq.ImporeterCompanyAddress = checkReq.importerCompanyAddress;

                Ex_CheckRequest_ExtraBll checkRequest_Extra = new Ex_CheckRequest_ExtraBll();
                checkRequest_Extra.Insert(extraReq, API_HelperFunctions.Get_DeviceInfo());

                #endregion
                //************************************//
                #region CheckPlace
                if (checkReq.IsStation)
                {
                    ExportRequest_ApprovedStationDTO stationDTO = new ExportRequest_ApprovedStationDTO();
                    stationDTO.Station_ID = checkReq.stationId;
                    stationDTO.CheckRequest_ID = req.ID;
                    stationDTO.IsActive = true;

                    stationDTO.User_Creation_Id = checkReq.User_Creation_Id;
                    stationDTO.User_Creation_Date = checkReq.User_Creation_Date;

                    ExportRequest_ApprovedStationBll stationBll = new ExportRequest_ApprovedStationBll();
                    stationBll.Insert(stationDTO, API_HelperFunctions.Get_DeviceInfo());
                }
                else
                {
                    //NotApproved Place ExportRequest_UnapprovedPlacesDTO
                    ExportRequest_UnapprovedPlacesDTO place = new ExportRequest_UnapprovedPlacesDTO();

                    place.Address_Ar = checkReq.checkplaceAddress;
                    place.CheckRequest_ID = req.ID;

                    ExportRequest_UnapprovedPlacesBll unApprovedBll = new ExportRequest_UnapprovedPlacesBll();
                    unApprovedBll.Insert(place, API_HelperFunctions.Get_DeviceInfo());
                }
                #endregion
                //***********************************//
                #region PORTS
                List<ExportRequest_PortDTO> ports = new List<ExportRequest_PortDTO>();

                ExportRequest_PortDTO shipment_portObj = new ExportRequest_PortDTO();
                //شحن 9
                shipment_portObj.Export_CheckRequest_ID = req.ID;
                shipment_portObj.Port_ID = checkReq.shipmentPortId;
                shipment_portObj.ReqPortType_ID = 9;
                shipment_portObj.User_Creation_Id = checkReq.User_Creation_Id;
                shipment_portObj.User_Creation_Date = checkReq.User_Creation_Date;

                ports.Add(shipment_portObj);
                //عبور 11
                ExportRequest_PortDTO transit_portObj = new ExportRequest_PortDTO();

                transit_portObj.Export_CheckRequest_ID = req.ID;
                transit_portObj.Port_ID = checkReq.transitPortId;
                transit_portObj.ReqPortType_ID = 11;
                transit_portObj.User_Creation_Id = checkReq.User_Creation_Id;
                transit_portObj.User_Creation_Date = checkReq.User_Creation_Date;

                ports.Add(transit_portObj);
                //وصول 10
                ExportRequest_PortDTO arrival_portObj = new ExportRequest_PortDTO();

                arrival_portObj.Export_CheckRequest_ID = req.ID;
                arrival_portObj.Port_ID = checkReq.arrivalPortId;
                arrival_portObj.ReqPortType_ID = 10;
                arrival_portObj.User_Creation_Id = checkReq.User_Creation_Id;
                arrival_portObj.User_Creation_Date = checkReq.User_Creation_Date;
                ports.Add(arrival_portObj);

                ExportRequest_PortBll portBll = new ExportRequest_PortBll();
                portBll.InsertList(ports, API_HelperFunctions.Get_DeviceInfo());
                #endregion
                //***********************************//
                //exSayed
                //#region Items               
                //foreach (var entity in checkReq.plants)
                //{
                //    ExportRequest_ItemDTO item = new ExportRequest_ItemDTO();
                //    item.CheckRequest_ID = req.ID;
                //    item.IsPlant = Custom_ExPlants.IsPlant;
                //    item.ProdPlant_ID = entity.Plant_ID;

                //    ExportRequest_ItemBll itemBll = new ExportRequest_ItemBll();
                //    Dictionary<string, object> dicItem = itemBll.Insert(item, API_HelperFunctions.Get_DeviceInfo());
                //    item.ID = long.Parse(dicItem["obj"].ToString());

                //    Con_Ex_Im_PlantsDTO plantData = new Con_Ex_Im_PlantsDTO();
                //    plantData.Con_Ex_Im_ID = item.ID;
                //    plantData.ItemType_ID = 35;
                //    plantData.PlantCat_ID = entity.PlantCat_ID;
                //    plantData.PlantPartType_ID = entity.PlantPartType_ID;
                //    plantData.ProductStatus_ID = entity.ProductStatus_ID;
                //    plantData.Purpose_ID = entity.Purpose_ID;

                //    Con_Ex_Im_PlantsBll plantDataBll = new Con_Ex_Im_PlantsBll();
                //    plantDataBll.Insert(plantData, API_HelperFunctions.Get_DeviceInfo());

                //    RequestLotDataBLL lotBll = new RequestLotDataBLL();
                //    lotBll.InsertList(entity.lotData, item.ID, API_HelperFunctions.Get_DeviceInfo());
                //}
                //foreach (var entity in checkReq.products)
                //{
                //    ExportRequest_ItemDTO item = new ExportRequest_ItemDTO();
                //    item.CheckRequest_ID = req.ID;
                //    item.IsPlant = Custom_ExProducts.IsPlant;
                //    item.ProdPlant_ID = entity.Product_ID;

                //    ExportRequest_ItemBll itemBll = new ExportRequest_ItemBll();
                //    Dictionary<string, object> dicItem = itemBll.Insert(item, API_HelperFunctions.Get_DeviceInfo());
                //    item.ID = long.Parse(dicItem["obj"].ToString());

                //    Con_Ex_Im_ProductsDTO productData = new Con_Ex_Im_ProductsDTO();
                //    productData.Con_Ex_Im_ID = item.ID;
                //    productData.ItemType_ID = 35;
                //    productData.ProductStatus_ID = entity.ProductStatus_ID;
                //    productData.Purpose_ID = entity.Purpose_ID;
                //    //sayed
                //    //Con_Ex_Im_ProductsBll productDataBll = new Con_Ex_Im_ProductsBll();
                //   // productDataBll.Insert(productData, API_HelperFunctions.Get_DeviceInfo());

                //    RequestLotDataBLL lotBll = new RequestLotDataBLL();
                //    lotBll.InsertList(entity.lotData, item.ID, API_HelperFunctions.Get_DeviceInfo());
                //}
                //foreach (var entity in checkReq.aliveItems)
                //{
                //    ExportRequest_ItemDTO item = new ExportRequest_ItemDTO();
                //    item.CheckRequest_ID = req.ID;
                //    item.IsPlant = Custom_ExAliveLiableItems.IsPlant;
                //    item.ProdPlant_ID = entity.alive_ID;

                //    ExportRequest_ItemBll itemBll = new ExportRequest_ItemBll();
                //    Dictionary<string, object> dicItem = itemBll.Insert(item, API_HelperFunctions.Get_DeviceInfo());
                //    item.ID = long.Parse(dicItem["obj"].ToString());

                //    Con_Ex_Im_AliveDTO aliveData = new Con_Ex_Im_AliveDTO();
                //    aliveData.Con_Ex_Im_ID = item.ID;
                //    aliveData.ItemType_ID = 35;
                //    aliveData.Purpose_ID = entity.Purpose_ID;
                //    aliveData.BiologicalPhase_ID = entity.BiologicalPhase;
                //    aliveData.LiableStatus_ID = entity.Status_ID;

                //    Con_Ex_Im_LiableItems_AliveBll aliveDataBll = new Con_Ex_Im_LiableItems_AliveBll();
                //    aliveDataBll.Insert(aliveData, API_HelperFunctions.Get_DeviceInfo());

                //    RequestLotDataBLL lotBll = new RequestLotDataBLL();
                //    lotBll.InsertList(entity.lotData, item.ID, API_HelperFunctions.Get_DeviceInfo());
                //}
                //foreach (var entity in checkReq.notAliveItems)
                //{
                //    ExportRequest_ItemDTO item = new ExportRequest_ItemDTO();
                //    item.CheckRequest_ID = req.ID;
                //    item.IsPlant = Custom_ExNotAliveLiableItems.IsPlant;
                //    item.ProdPlant_ID = entity.notAlive_ID;

                //    ExportRequest_ItemBll itemBll = new ExportRequest_ItemBll();
                //    Dictionary<string, object> dicItem = itemBll.Insert(item, API_HelperFunctions.Get_DeviceInfo());
                //    item.ID = long.Parse(dicItem["obj"].ToString());
                //    //Con_Ex_Im_LiableItems_NotAlive
                //    Con_Ex_Im_NotAliveDTO notAliveData = new Con_Ex_Im_NotAliveDTO();
                //    notAliveData.Con_Ex_Im_ID = item.ID;
                //    notAliveData.ItemType_ID = 35;
                //    notAliveData.Purpose_ID = entity.Purpose_ID;
                //    notAliveData.LiableStatus_ID = entity.Status_ID;

                //    Con_Ex_Im_LiableItems_NotAliveBll notAliveDataBll = new Con_Ex_Im_LiableItems_NotAliveBll();
                //    notAliveDataBll.Insert(notAliveData, API_HelperFunctions.Get_DeviceInfo());

                //    RequestLotDataBLL lotBll = new RequestLotDataBLL();
                //    lotBll.InsertList(entity.lotData, item.ID, API_HelperFunctions.Get_DeviceInfo());
                //}
                //#endregion
                //************************************//          
                #region files
                List<ex_A_AttachmentDataDTO> files = new List<ex_A_AttachmentDataDTO>();
                foreach (var entity in checkReq.filesAttached)
                {
                    ex_A_AttachmentDataDTO file = new ex_A_AttachmentDataDTO();
                    file.AttachmentPath = entity.AttachmentPath;
                    file.Attachment_Number = entity.Attachment_Number;
                    file.Attachment_TypeName = entity.Attachment_TypeName;
                    file.StartDate = entity.startDate;
                    file.EndDate = entity.endDate;

                    file.A_AttachmentTableNameId = 3;
                    file.RowId = req.ID;

                    files.Add(file);
                }
                A_AttachmentDataBLL attachments = new A_AttachmentDataBLL();
                attachments.InsertList(files, API_HelperFunctions.Get_DeviceInfo());
                #endregion
                //************************************//          
                #region fees
                List<Ex_CheckRequest_FeesDTO> fees = new List<Ex_CheckRequest_FeesDTO>();
                foreach (var entity in checkReq.feesList)
                {
                    Ex_CheckRequest_FeesDTO exFees = new Ex_CheckRequest_FeesDTO();
                    exFees.Ex_CheckRequest_ID = req.ID;
                    exFees.FixedFeesAmount_ID = (byte)entity.FixedFeesAmount_ID;
                    exFees.Amount = entity.FeeValue;

                    fees.Add(exFees);
                }
                CheckReq_FeesBLL feesBll = new CheckReq_FeesBLL();
                feesBll.InsertList(fees, API_HelperFunctions.Get_DeviceInfo());
                #endregion
                //***********************************//
                //ImportCompanies
                List<Ex_Request_ImportCompanyDTO> impComp = new List<Ex_Request_ImportCompanyDTO>();
                foreach (var comp in checkReq.ImpCompanies)
                {
                    Ex_Request_ImportCompanyDTO company = new Ex_Request_ImportCompanyDTO();
                    company.CheckRequest_ID = req.ID;
                    company.ImportCompany = comp.ImportCompany;
                    company.ImporeterCompanyAddress = comp.ImporeterCompanyAddress;
                    company.Reciever_Name = comp.Reciever_Name;

                    impComp.Add(company);
                }
                Ex_Request_ImportCompanyBLL impBll = new Ex_Request_ImportCompanyBLL();
                impBll.InsertList(impComp, API_HelperFunctions.Get_DeviceInfo());

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, req.ID);
            }
            catch (Exception ex)
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.ErrorHappened, null);
            }
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest entity = uow.Repository<Ex_CheckRequest>().Findobject(Id);
                var empDTO = Mapper.Map<Ex_CheckRequest, Export_CheckRequestDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO.CheckRequest_Number);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> FindCreationDate(object Id, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest entity = uow.Repository<Ex_CheckRequest>().Findobject(Id);
                var empDTO = Mapper.Map<Ex_CheckRequest, Export_CheckRequestDTO>(entity);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, empDTO.User_Creation_Date);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update(Export_CheckRequestDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Update_PaidPatch(DeleteParameters checkRequest_data, List<string> Device_Info)
        {
            Ex_CheckRequest entity = uow.Repository<Ex_CheckRequest>().Findobject(checkRequest_data.id);
            entity.IsPaid = true;
            entity.User_Updation_Id = (short)checkRequest_data.Userid;
            entity.User_Updation_Date = checkRequest_data._DateNow;
            uow.SaveChanges();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, entity);
        }

        public bool GetAny(Export_CheckRequestDTO entity)
        {
            throw new NotImplementedException();
        }

        //*************************************************************************//

        public Dictionary<string, object> GetRequestDataByID(long requestId, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("CheckRequest_Id", SqlDbType.BigInt);
                paramters_Type.Add("CheckRequest_Number", SqlDbType.VarChar);
                paramters_Type.Add("IsGetLotData", SqlDbType.Char);
                paramters_Type.Add("IsGetConstrainData", SqlDbType.Char);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("CheckRequest_Id", requestId.ToString());
                paramters_Data.Add("CheckRequest_Number", "");
                paramters_Data.Add("IsGetLotData", "1");
                paramters_Data.Add("IsGetConstrainData", "1");

                var request = uow.Repository<CheckRequest_GetData_ResultDTO>().CallStored("CheckRequest_GetData", paramters_Type, paramters_Data, Device_Info).FirstOrDefault();

                if (!String.IsNullOrEmpty(request.Item_Data))
                {
                    XMLToClass<ExportRequest_XmlDTO> xML = new XMLToClass<ExportRequest_XmlDTO>();
                    request.Item_Data_xml = (ExportRequest_XmlDTO)xML.ConvertXMLToClass("ExportRequest_XmlDTO", request.Item_Data);
                }

                if (!String.IsNullOrEmpty(request.Attachment_Data))
                {
                    XMLToClass<AttachmentData_Xml> xML = new XMLToClass<AttachmentData_Xml>();
                    request.AttachmentData_Xml = (AttachmentData_Xml)xML.ConvertXMLToClass("AttachmentData_Xml", request.Attachment_Data);
                }

                if (!String.IsNullOrEmpty(request.ImportCompany_data))
                {
                    XMLToClass<Ex_Request_ImportCompanyXML> xML = new XMLToClass<Ex_Request_ImportCompanyXML>();
                    request.ImportCompany_xml = (Ex_Request_ImportCompanyXML)xML.ConvertXMLToClass("Ex_Request_ImportCompanyXML", request.ImportCompany_data);
                }
                if (!String.IsNullOrEmpty(request.Request_Fees))
                {
                    XMLToClass<Ex_Request_FeesXMLDTO> xML = new XMLToClass<Ex_Request_FeesXMLDTO>();
                    request.Request_Fees_xml = (Ex_Request_FeesXMLDTO)xML.ConvertXMLToClass("Ex_Request_FeesXMLDTO", request.Request_Fees);

                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> InsertReasons(ReasonsListReqIdDTO dto, List<string> Device_Info)
        {
            try
            {

                Ex_CheckRequest_RefuseReasonDTO rr = new Ex_CheckRequest_RefuseReasonDTO();
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
        public Dictionary<string, object> InsertReason(Ex_CheckRequest_RefuseReasonDTO entity, List<string> Device_Info)
        {
            try
            {

                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_RefuseReason_SEQ");
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




        public Dictionary<string, object> InsertStoppingReasons(ReasonsListReqIdDTO dto, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Ex_CheckRequest>().GetData().Where(a => a.CheckRequest_Number == dto.checkRequestNumber).FirstOrDefault();
                if (data != null)
                {
                    data.IsAcceppted = false;
                    uow.Repository<Ex_CheckRequest>().Update(data);
                    uow.SaveChanges();

                    Ex_CheckRequest_RefuseReasonDTO rr = new Ex_CheckRequest_RefuseReasonDTO();
                    foreach (var id in dto.refuseReasonsIds)
                    {

                        rr.Ex_CheckRequest_Id = data.ID;
                        rr.Refuse_Reason_Id = id;
                        rr.User_Creation_Id = dto.User_Creation_Id;
                        rr.User_Creation_Date = dto.User_Creation_Date;
                        InsertReason(rr, Device_Info);
                    }




                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "success");
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid check Request Number");

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
