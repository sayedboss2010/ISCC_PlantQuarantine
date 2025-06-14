using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper;
using PlantQuar.DTO.HelperClasses;
using System.Data;
using PlantQuar.BLL.BLL.Log;
using PlantQuar.DTO.DTO.Log;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity.Infrastructure;

namespace PlantQuar.BLL.BLL.Export_CheckRequest
{
    public class CheckRequestChangeLot_BLL
    {
        private UnitOfWork uow;
        public CheckRequestChangeLot_BLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetGeshniCommitteeList(string CheckRequestNumber, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                var requests = (from ch in entities.Ex_CheckRequest
                                join it in entities.Ex_CheckRequest_Items on ch.ID equals it.Ex_CheckRequest_ID
                                join lot in entities.Ex_CheckRequest_Items_Lot_Category on it.ID equals lot.Ex_CheckRequest_Items_ID
                                join fr in entities.Ex_CheckRequest_Final_Result  on ch.ID equals fr.Ex_CheckRequest_ID
                                where ch.CheckRequest_Number == CheckRequestNumber
                                &&fr.Ex_Final_Result.Status==false
                                select new CheckRequestChangeLotDTO
                                {
                                    Ex_CheckRequest_ID = ch.ID,
                                    EX_ItemID = it.ID,
                                    ItemName = it.Item_ShortName.Item.Name_Ar,
                                    ItemShortName = it.Item_ShortName.ShortName_Ar,
                                    LotID = lot.ID,
                                    Lot_Number = lot.Lot_Number,
                                    GrossWeight = lot.GrossWeight,
                                    Package_Based_Weight = lot.Package_Based_Weight,
                                    Package_Net_Weight = lot.Package_Net_Weight,
                                    Package_Count = lot.Package_Count,

                                }).ToList();
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> GetEmployeeGeshniChange(string requestNumber, List<string> Device_Info)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var data = (from exc in entities.Ex_CheckRequest
                            join tl in entities.Table_Action_Log_EX on exc.ID equals tl.Ex_CheckRequest_ID

                            where exc.CheckRequest_Number == requestNumber && tl.ID_Table_Action == 59
                            select new EmployeeGeshniChangeDTO
                            {
                                Ex_CheckRequest_ID2 = tl.Ex_CheckRequest_ID,
                                Emp_ID2 = tl.User_Creation_Id,
                                User_Creation_Date2 = tl.User_Creation_Date,
                                Notes2 = tl.NOTS
                                //Ex_CheckRequest_ID = exc.ID,


                            }).ToList();
                dbPrivilageEntities prv = new dbPrivilageEntities();
                foreach (var item in data)
                {
                    item.EmpName2 = prv.PR_User.Where(p => p.Id == item.Emp_ID2).Select(a => a.FullName).FirstOrDefault();

                }

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> CheckRequestChangeWeightList(string CheckRequest_Number, List<CheckedItemsListWeightDTO> CheckedItemsListWeight, List<string> Device_Info)
        {
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            try
            {
                #region ChangeWeightsForLot
                if (CheckedItemsListWeight != null)
                {
                    if (CheckedItemsListWeight.Count > 0)
                    {

                          //var CheckedItemsList = uow.Repository<Ex_CheckRequest_Items_Lot_Category>().GetData().ToList();

                        foreach (var item in CheckedItemsListWeight)
                        {

                            var datalotold = uow.Repository<Ex_CheckRequest_Items_Lot_Category>().GetData().Where(a => a.ID == item.LotID).FirstOrDefault();

                     
                            datalotold.Package_Based_Weight = item.Package_Based_Weight;
                            datalotold.Package_Net_Weight = item.Package_Net_Weight;
                            datalotold.Package_Count = item.Package_Count; //عدد العبوات 
                            datalotold.GrossWeight = item.Package_Based_Weight * item.Package_Count; //اجمالي 
                            datalotold.Net_Weight = item.Package_Net_Weight * item.Package_Count; //اجمالي 
                            //user update
                           datalotold.User_Updation_Id=item.User_Updation_Id;

                           datalotold.User_Updation_Date = DateTime.Now;



                            uow.Repository<Ex_CheckRequest_Items_Lot_Category>().Update(datalotold);
                            uow.SaveChanges();
                  
                        }

                        var checkRequestId = entities.Ex_CheckRequest.Where(a => a.CheckRequest_Number == CheckRequest_Number).Select(a => a.ID).FirstOrDefault();
                        #region log Action

                        Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                        dto2.ID_Table_Action = 59;
                        // dto2.ID_TableActionValue = checkRequestId;
                        dto2.Im_CheckRequest_ID = checkRequestId;
                        dto2.User_Creation_Id = CheckedItemsListWeight.FirstOrDefault().User_Updation_Id;
                        dto2.User_Creation_Date = DateTime.Now;
                        dto2.NOTS = " تم تغير وزن العبوات وعدد العبوات في طلب الفحص الصادر ";
                        dto2.User_Type_ID = 127;// System Code For موظف الحجر
                        dto2.Type_log_ID = 135;  //system code for Update
                        Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                        x.save_EX_CheckRequest_Log(dto2, Device_Info);

                        #endregion

                    }
                }
                #endregion








                //Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                //                dto2.ID_Table_Action = 3;
                //               // dto2.ID_TableActionValue = Committe_ID;
                //                dto2.User_Creation_Id = entity.User_Creation_Id;
                //                dto2.User_Creation_Date = DateTime.Now;
                //                dto2.NOTS = " تم عمل لجنة علي الطلب ";
                //                dto2.User_Type_ID = 127;
                //                dto2.Type_log_ID = 133;
                //                Log_CheckRequest_BLL x = new Log_CheckRequest_BLL();
                //                x.save_CheckRequest_Log(dto2, Device_Info);




                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, null);
            }

            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


    }
}
