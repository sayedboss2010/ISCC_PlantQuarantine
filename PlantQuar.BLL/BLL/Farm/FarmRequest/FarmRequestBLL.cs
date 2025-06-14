using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Farm.FarmRequest
{
    public class FarmRequestBLL
    {
        private UnitOfWork uow;
        public FarmRequestBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetAll(string farmcode, int? Status, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("FarmCode", SqlDbType.NVarChar);
                paramters_Type.Add("Status", SqlDbType.Int);
                paramters_Type.Add("Language_IsAr", SqlDbType.NVarChar);

                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("FarmCode", (farmcode != null ? farmcode : ""));
                paramters_Data.Add("Status", Status.ToString());
                paramters_Data.Add("Language_IsAr", Device_Info[2]);

                var request = uow.Repository<Farm_Committee_GetData_DTO>().CallStored("Farm_Committee_GetData", paramters_Type,
                    paramters_Data, Device_Info).ToList();

                foreach (var req in request)
                {
                    var cat = uow.Repository<Farm_Request_ItemCategories>().GetData().Where(r => r.Farm_Request_ID == req.requestId).ToList();
                    if (cat.Count > 0)
                    {
                        req.hasCategories = true;
                    }
                    else
                    {
                        req.hasCategories = false;
                    }
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, request);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetAllByFarmId(string farmcode, int? Status, long? farmId, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                paramters_Type.Add("FarmCode", SqlDbType.NVarChar);
                paramters_Type.Add("Status", SqlDbType.Int);
                paramters_Type.Add("farmId", SqlDbType.BigInt);
                paramters_Type.Add("Language_IsAr", SqlDbType.NVarChar);


                Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                paramters_Data.Add("FarmCode", (farmcode != null ? farmcode : ""));
                paramters_Data.Add("Status", Status.ToString());
                paramters_Data.Add("farmId", (farmId == null ? "0" : farmId.ToString()));
                paramters_Data.Add("Language_IsAr", Device_Info[2]);


                var request = uow.Repository<Farm_Committee_GetData_DTO>().CallStored("Farm_Committee_GetData", paramters_Type,
                    paramters_Data, Device_Info).ToList();



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, request);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetFarms(bool? Is_Status, List<string> Device_Info)
        {
            try
            {

                var request = uow.Repository<FarmsData>().GetData()
                    .Include(g => g.Item)
                    .Where(g => g.User_Deletion_Id == null && g.IS_OnlineOffline == true)
                    .Select(x => new FarmsListDTO
                    {
                        FarmID = x.ID,
                        FarmCode_14 = x.FarmCode_14,
                        Name_Ar = x.Name_Ar,
                        Name_En = x.Name_En,
                        ItemName = x.Item.Name_Ar
                    }).OrderByDescending(f => f.FarmID).ToList();
                var requestDTO = new List<FarmsListDTO>();

                foreach (var Req in request)
                {
                    var Rq = uow.Repository<Farm_Request>().GetData()
                          .Where(g => g.User_Deletion_Id == null && g.FarmsData_ID == Req.FarmID && g.IsStatus == Is_Status)
                      .ToList();
                    if (Rq.Count() > 0)
                    {
                        requestDTO.Add(Req);
                    }
                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, requestDTO);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetFarms(List<string> Device_Info)
        {
            try
            {
                DateTime _dateFrom = DateTime.Parse(DateTime.Now.ToString());
                DateTime _dateEnd = DateTime.Parse(DateTime.Now.AddDays(+7).ToString());

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var request = (from fd in entities.FarmsDatas
                               join fr in entities.Farm_Request on fd.ID equals fr.FarmsData_ID
                               join fc in entities.Farm_Committee on fr.ID equals fc.Farm_Request_ID
                               where fd.User_Deletion_Id == null
                               && fd.IS_OnlineOffline == true
                               && (fr.Start_Date_Request >= _dateFrom
                               && fr.End_Date_Request <= _dateEnd)
                               && fc.Delegation_Date == null
                               && fr.IsAcceppted == true
                               && fr.IsStatus == null
                               // &&fd.ID==780
                               select new FarmsListDTO
                               {
                                   FarmID = fd.ID,
                                   FarmCode_14 = fd.FarmCode_14 == null ? "" : fd.FarmCode_14,
                                   Name_Ar = fd.Name_Ar,
                                   Name_En = fd.Name_En,
                                   ItemName = fd.Item.Name_Ar,
                                   Farm_Request_ID = fr.ID,
                                   Farm_Committee_ID = fc.ID,
                               }).OrderByDescending(f => f.FarmID).ToList();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, request);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetFarmsByItem(long? itemId, List<string> Device_Info)
        {
            try
            {
                var request = uow.Repository<FarmsData>().GetData().Include(g => g.Item).Where(g => g.User_Deletion_Id == null && g.Item_ID == itemId && g.IS_OnlineOffline == true).Select(x => new FarmsListDTO
                {
                    FarmID = x.ID,
                    FarmCode_14 = x.FarmCode_14,
                    Name_Ar = x.Name_Ar,
                    Name_En = x.Name_En,
                    ItemName = x.Item.Name_Ar
                }).OrderByDescending(f => f.FarmID).ToList();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, request);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetFarms(int Is_Status, long? itemId, int? govId, int? centerId, int? villageId, string Date_From, string Date_End, List<string> Device_Info)
        {
            try
            {
                bool? isStatus = null;
                if (Is_Status == 2)
                {
                    isStatus = true;
                }
                else if (Is_Status == 3)
                {
                    isStatus = false;
                }
                DateTime _dateFrom = DateTime.Parse(Date_From);
                DateTime _dateEnd = DateTime.Parse(Date_End);

                PlantQuarantineEntities entities = new PlantQuarantineEntities();

                var request = (from fd in entities.FarmsDatas
                               join fr in entities.Farm_Request on fd.ID equals fr.FarmsData_ID
                               where fd.User_Deletion_Id == null && fd.IS_OnlineOffline == true
                               //&& fr.Start_Date_Request != null && fr.End_Date_Request != null
                               //&& fr.Start_Date_Request >= _dateFrom && fr.End_Date_Request <= _dateEnd
                               //&& fr.IsPaid == true 
                               //&&fr.IsStatus == null
                               select new FarmsListDTO
                               {

                                   FarmID = fd.ID,
                                   FarmCode_14 = fd.FarmCode_14 == null ? "" : fd.FarmCode_14,
                                   Name_Ar = fd.Name_Ar,
                                   Name_En = fd.Name_En,
                                   ItemName = fd.Item.Name_Ar,
                                   itemId = fd.Item_ID,
                                   Center_Id = fd.Center_Id,
                                   Village_ID = fd.Village_ID,
                                   Govern_ID = fd.Govern_ID,
                                   IsStatus = Is_Status,
                                   IsStatus_Requst = fr.IsStatus,
                                   Farm_Request_ID = fr.ID,
                                   IsAcceppted = fr.IsAcceppted,
                                   IsPaid = fr.IsPaid,
                                   Start_Date_Request = fr.Start_Date_Request,
                                   End_Date_Request = fr.End_Date_Request,
                                //   Is_Final_requst = fr.Is_Final_requst
                               }).OrderByDescending(f => f.FarmID).ToList();
                if (itemId != null)
                {
                    request = request.Where(i => i.itemId == itemId).ToList();
                }
                if (govId != null)
                {
                    request = request.Where(i => i.Govern_ID == govId).ToList();
                }
                if (centerId != null)
                {
                    request = request.Where(i => i.Center_Id == centerId).ToList();
                }
                if (villageId != null)
                {
                    request = request.Where(i => i.Village_ID == villageId).ToList();
                }

                //var requestDTO = new List<FarmsListDTO>();
                //if(Is_Status != -1)
                //{
                //    foreach (var Req in request)
                //    {
                //        var Rq = uow.Repository<Farm_Request>().GetData()
                //              .Where(g => g.User_Deletion_Id == null 
                //              && g.FarmsData_ID == Req.FarmID 
                //              && g.IsStatus == isStatus
                //               && g.Start_Date_Request >= _dateFrom && g.End_Date_Request <= _dateEnd
                //              )
                //          .ToList();
                //        if (Rq.Count() > 0)
                //        {
                //            requestDTO.Add(Req);
                //        }
                //    }
                //}


                //else
                //{
                //    foreach (var Req in request)
                //    {
                //        var Rq = uow.Repository<Farm_Request>().GetData()
                //              .Where(g => g.User_Deletion_Id == null 
                //              && g.FarmsData_ID == Req.FarmID
                //              && g.IsStatus == true
                //              && g.Start_Date_Request >= _dateFrom
                //             // && g.End_Date_Request <= _dateEnd
                //             )
                //          .ToList();

                //        if (Rq.Count() > 0)
                //        {
                //            Req.IsStatus = 2;
                //        }
                //        requestDTO.Add(Req);
                //    }
                //}
                var request_Committe = (from re in request
                                        join fc in entities.Farm_Committee on re.Farm_Request_ID equals fc.Farm_Request_ID
                                        select new FarmsListDTO
                                        {
                                            Farm_Committee_ID = fc.ID,
                                            FarmID = re.FarmID,
                                            FarmCode_14 = re.FarmCode_14,
                                            Name_Ar = re.Name_Ar,
                                            Name_En = re.Name_En,
                                            ItemName = re.ItemName,
                                            itemId = re.itemId,
                                            Center_Id = re.Center_Id,
                                            Village_ID = re.Village_ID,
                                            Govern_ID = re.Govern_ID,
                                            IsStatus = Is_Status,
                                            Farm_Request_ID = re.Farm_Request_ID,
                                            IsAcceppted = re.IsAcceppted,
                                            IsPaid = re.IsPaid,
                                            Start_Date_Request = re.Start_Date_Request,
                                            End_Date_Request = re.End_Date_Request,
                                            Delegation_Date = fc.Delegation_Date,
                                            CommitteeType_ID = fc.CommitteeType_ID,
                                            IsStatus_Committe = fc.Status,
                                            Is_Cancel= fc.Is_Cancel
                                        }).ToList();
                switch (Is_Status)
                {
                    case 1:// لم يتخذ إجراء على المزرعة
                        request = request.Where(a => a.IsAcceppted == null).ToList();
                        break;
                    case 2://تم الموافقة على المزرعة 
                        request = request.Where(a => a.IsAcceppted == true).ToList();
                        break;
                    case 3:// تم رفض المزرعة
                        request = request.Where(a => a.IsAcceppted == false).ToList();
                        break;
                    case 4: // تشكيل لجنة

                        request = request_Committe.Where(a => a.Start_Date_Request >= _dateFrom
                                 && a.Delegation_Date == null
                                 && a.IsAcceppted == true
                                  && a.IsStatus_Requst == null  
                                 && a.IsPaid == true
                                 ||a.Is_Cancel==true).ToList();
                        //request = (from re in request
                        //           join fc in entities.Farm_Committee on re.Farm_Request_ID equals fc.Farm_Request_ID
                        //           where re.Start_Date_Request >= _dateFrom
                        //           //&& re.End_Date_Request <= _dateEnd)
                        //           && fc.Delegation_Date == null
                        //           && re.IsAcceppted == true
                        //           && re.IsStatus_Requst == null
                        //           && re.IsPaid == true
                        //           select new FarmsListDTO
                        //           {
                        //               Farm_Committee_ID = fc.ID,
                        //               FarmID = re.FarmID,
                        //               FarmCode_14 = re.FarmCode_14,
                        //               Name_Ar = re.Name_Ar,
                        //               Name_En = re.Name_En,
                        //               ItemName = re.ItemName,
                        //               itemId = re.itemId,
                        //               Center_Id = re.Center_Id,
                        //               Village_ID = re.Village_ID,
                        //               Govern_ID = re.Govern_ID,
                        //               IsStatus = Is_Status,
                        //               Farm_Request_ID = re.Farm_Request_ID,
                        //               IsAcceppted = re.IsAcceppted,
                        //               IsPaid = re.IsPaid,
                        //               Start_Date_Request = re.Start_Date_Request,
                        //               End_Date_Request = re.End_Date_Request,
                        //           }).ToList();
                        break;

                    case 5: // لجنة معاينة
                    case 12://لجنة معاينة وسحب
                        request = request_Committe.Where(a => a.Delegation_Date != null
                                   && a.CommitteeType_ID != null
                                   && a.CommitteeType_ID == Is_Status).ToList();
                        break;
                    case 6:// تم الدفع
                        request = request.Where(a => a.IsPaid == true).ToList();
                        break;
                    case 7:// لم يتم الدفع
                        request = request.Where(a => a.IsPaid == false).ToList();
                        break;
                    case 8:// الطلبات المنتهية من الحجر
                        request = request_Committe.Where(a => a.IsStatus_Committe == true && a.Is_Final_requst == true).ToList();
                        break;
                    case 9:// الطلبات الغير منتهية من الحجر
                        request = request_Committe.Where(a => a.IsStatus_Committe == true && a.Is_Final_requst != true).ToList();
                        break;
                    default:
                        break;
                }



                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, request);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

    }
}
