using AutoMapper;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.ExportRequest;

using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Transactions;

namespace PlantQuar.BLL.BLL.ExportRequest
{
    public class RequestLotDataBLL : IGenericBLL<Ex_Request_LotDataDTO>
    {
        private UnitOfWork uow;

        public RequestLotDataBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> GetCount()
        {
            var count = uow.Repository<Ex_Request_LotData>().GetData().Count();
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, count);
        }
        public Dictionary<string, object> GetAll(int CheckRequest_ID = 0,int type=2, 
            int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null, List<string> Device_Info=null)
        {
            try
            {
                //if(type==2)
                //{
                //}
                //else{
                //    var data2 = uow.Repository<Ex_Request_LotData>().GetData().Where(a => a.Ex_Request_Item.CheckRequest_ID == CheckRequest_ID).ToList();
                //   var da = from chk in uow.Repository<Ex_CheckRequest>().GetData()
                //            join Req_Item in uow.Repository<Ex_Request_Item>().GetData()
                //            on chk.ID equals Req_Item.ProdPlant_ID

                //            join Req_Lot in uow.Repository<Ex_Request_LotData>().GetData()
                //            on Req_Item.ID equals Req_Lot.Ex_Request_Item_ID

                //            //join Req_Lot in uow.Repository<Ex_Request_LotData>().GetData()



                //            select new
                //           {
                //               //EmployeeName = e.Name,
                //               //DepartmentName = d.Name
                //           };
                //}

                var data = uow.Repository<Ex_Request_LotData>().GetData().Where(a=>a.Ex_Request_Item.CheckRequest_ID == CheckRequest_ID).ToList();
                var dataDto = data.Select(Mapper.Map<Ex_Request_LotData, Ex_Request_LotDataDTO>);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dataDto);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> Update(Ex_Request_LotDataDTO entity, List<string> Device_Info)
        {
            try
            {
                    var obj = entity as Ex_Request_LotDataDTO;
                    Ex_Request_LotData CModel = uow.Repository<Ex_Request_LotData>().Findobject(obj.ID);

                    //obj.User_Creation_Date = CModel.User_Creation_Date;
                    //obj.User_Creation_Id = CModel.User_Creation_Id;
                    //if (CModel.User_Updation_Id != null)
                    //{
                    //    obj.User_Updation_Date = CModel.User_Updation_Date;
                    //    obj.User_Updation_Id = CModel.User_Updation_Id;
                    //}

                    var Co = Mapper.Map(obj, CModel);
                    uow.Repository<Ex_Request_LotData>().Update(Co);
                    uow.SaveChanges();

                    var empDTO = Mapper.Map<Ex_Request_LotData, Ex_Request_LotDataDTO>(Co);
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetLotByCheckRequest_Id(long checkRequestNumber_Id, List<string> Device_Info)
        {
            Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
            paramters_Type.Add("checkRequestNumber_Id", SqlDbType.BigInt);

            Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
            paramters_Data.Add("checkRequestNumber_Id", checkRequestNumber_Id.ToString());

            var request = uow.Repository<CheckRequest_GetLotData_Result>().CallStored("CheckRequest_GetLotData", paramters_Type,
                paramters_Data, Device_Info).ToList();

            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);
        }


        public bool GetAny(Int64? CheckRequest_ID)
        {
           
            return uow.Repository<Ex_Request_LotData>().GetAny(p => p.Ex_Request_Item.CheckRequest_ID== CheckRequest_ID);
            
        }

        public Dictionary<string, object> Insert(List<Ex_Request_LotDataDTO> dataToSend, Int64? CheckRequest_ID, 
            int? type, byte? TreatmentTypeId, Int64? Company_NationalId, Int64? StationId, 
            decimal? Size, byte TreatmentMethodId, byte? TreatmentMaterialId, decimal? TreatmentMat_Amount, 
            decimal? TheDose, int? Exposure_Minute, int? Exposure_Hour, int? Exposure_Day,
            decimal? Temperature, byte ThermalSealNumber, int Committee_ID, List<string> Device_Info)
        {
            try
            {
                if (GetAny(CheckRequest_ID))
                {

                    using (var txscope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        try
                        {

                            Ex_Request_TreatmentDataDTO EX_T = new Ex_Request_TreatmentDataDTO();
                            //EX_T.CheckRequest_ID = CheckRequest_ID;
                            EX_T.TreatmentType_ID = TreatmentTypeId;
                            EX_T.Company_ID = Company_NationalId;
                            EX_T.Station_ID = StationId;
                            EX_T.TreatmentMethod_ID = TreatmentMethodId;
                            EX_T.TreatmentMat_ID = TreatmentMaterialId;
                            EX_T.TreatmentMat_Amount = TreatmentMat_Amount;
                            EX_T.TheDose = TheDose;
                            EX_T.Size = Size;
                            EX_T.Exposure_Minute = Exposure_Minute;
                            EX_T.Exposure_Hour = Exposure_Hour;
                            EX_T.Exposure_Day = Exposure_Day;
                            EX_T.Temperature = Temperature;
                            EX_T.Committee_ID = Committee_ID;
                        

                            var CModel = Mapper.Map<Ex_Request_TreatmentData>(EX_T);
                            CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_Request_TreatmentData_Seq");
                            uow.Repository<Ex_Request_TreatmentData>().InsertRecord(CModel);
                            txscope.Complete();
                            uow.SaveChanges();

                            //fz 3-2-2020 need to get current result ID
                            //foreach (var item in dataToSend)
                            //{
                            //    var CModel2 = Mapper.Map<Ex_Request_Treatment_LotData>(item);
                            //    if (CModel2.ID == EX_T.TreatmentType_ID)
                            //    {
                            //        uow.Repository<Ex_Request_Treatment_LotData>().InsertRecord(CModel2);
                            //        uow.SaveChanges();
                            //    }



                            //}
                        }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                        catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                        {

                            txscope.Dispose();
                        }
                    }                      
                    //var obj = entity as Ex_Request_LotDataDTO;
                    //var CModel = Mapper.Map<Ex_Request_LotData>(obj);

                    //if (CModel.Ex_Request_Item.IsPlant == 0)
                    //{
                    //    CModel.Ex_Request_Item.Ex_CheckRequest.
                    //}
                    //else {

                    //}
                  



                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dataToSend);
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.RepeatedData, null);
                }
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName,ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
              
        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Insert(Ex_Request_LotDataDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(Ex_Request_LotDataDTO entity)
        {
            throw new NotImplementedException();
        }

        //****************************************************//
        //SARA
        public Dictionary<string, object> InsertList(List<LotData> lots, long itemID, List<string> Device_Info)
        {
            try
            {
                foreach (var lot in lots)
                {
                    ExportRequest_LotDataDTO lotData = new ExportRequest_LotDataDTO();
                    lotData.Ex_Request_Item_ID = itemID;
                    lotData.Farm_ID = lot.Farm_ID;
                    lotData.Gross_Weight = lot.Gross_Weight;
                    lotData.Lot_Number = lot.Lot_Number;
                    lotData.Net_Weight = lot.Net_Weight;
                    lotData.Package_Count = lot.Package_Count;
                    lotData.Package_Material_ID = lot.Package_Material_ID;
                    lotData.Package_Type_ID = lot.Package_Type_ID;
                    lotData.Package_Weight = lot.Package_Weight;
                    lotData.PlantingPlace = lot.farmAddress;
                   
                    var CModel = Mapper.Map<Ex_Request_LotData>(lotData);
                    CModel.ID = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_Request_LotData_Seq");
                    uow.Repository<Ex_Request_LotData>().InsertRecord(CModel);
                    uow.SaveChanges();
                }
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, itemID);
            }
            catch
            {
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.ErrorHappened, null);
            }
        }
        //**************************************************//
    }
}
