
using PlantQuar.UOW.UnitOfWork;
using Privilages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.DTO.DTO.Pallet_Export_CheckRequest;
using PlantQuar.DAL;

namespace PlantQuar.BLL.BLL.Pallet_Export_CheckRequest
{
    public class Pallet_List_EXCheckRequest_BLL
    {
        private UnitOfWork uow;

        public Pallet_List_EXCheckRequest_BLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetImCheckRequestList_filter
          (short IsApproved, string requestnumber, short userId, List<string> Device_Info)
        {
            try
            {
                dbPrivilageEntities priv = new dbPrivilageEntities();
                var user = priv.PR_User.Where(p => p.Id == userId).FirstOrDefault();
                var outlet = uow.Repository<Outlet>().GetData().FirstOrDefault(o => o.ID_HR == user.Outlet_ID);

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];
                List<Pallet_EXCheckRequestListDTO> requests = uow.Repository<Pallet_EXCheckRequestListDTO>().CallStored("[Pallet_Ex_ALL_List]", null, null, Device_Info).ToList();// entities.Ex_ALL_List().ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetImCheckRequestList_filter
            (short IsApproved, short userId, List<string> Device_Info)
        {
            try
            {
                dbPrivilageEntities priv = new dbPrivilageEntities();
                var user = priv.PR_User.Where(p => p.Id == userId).FirstOrDefault();
                var outlet = uow.Repository<Outlet>().GetData().FirstOrDefault(o => o.ID_HR == user.Outlet_ID);

                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                string lang = Device_Info[2];

                List<Pallet_EXCheckRequestListDTO> requests = uow.Repository<Pallet_EXCheckRequestListDTO>().CallStored("Pallet_Ex_ALL_List", null, null, Device_Info).ToList();// entities.Ex_ALL_List().ToList();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, requests);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> ApproveCheckReq(Pallet_EX_CheckRequestDTO dto, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest CModel = uow.Repository<Ex_CheckRequest>().Findobject(dto.ID);
                CModel.IsAccepted = dto.IsAccepted;
                CModel.IsActive = dto.IsAccepted;
                uow.SaveChanges();

                var empDTO = Mapper.Map<Ex_CheckRequest, Pallet_EX_CheckRequestDTO>(CModel);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
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

        public Dictionary<string, object> saveItemFees(Items_checkReq_Pallets item, List<string> Device_Info)
        {
            try
            {
                Ex_CheckRequest_Items CModel = uow.Repository<Ex_CheckRequest_Items>().Findobject((long)item.ImcheckReqItem_ID);
                CModel.Fees = item.Fees;

                uow.SaveChanges();


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, item.ImcheckReqItem_ID);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> InsertReasons(Pallet_ReasonsListReqIdDTO dto, List<string> Device_Info)
        {
            try
            {

                Pallet_EX_CheckRequest_RefuseReasonDTO rr = new Pallet_EX_CheckRequest_RefuseReasonDTO();
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
        public Dictionary<string, object> InsertReason(Pallet_EX_CheckRequest_RefuseReasonDTO entity, List<string> Device_Info)
        {
            try
            {

                var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_RefuseReason_seq");
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
    }
}
