using PlantQuar.BLL.BLL.Log;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.Import.checkRequests
{
    public class Change_request_StatusBLL
    {
        private UnitOfWork uow;
        PlantQuarantineEntities entities = new PlantQuarantineEntities();

        public Change_request_StatusBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetAll(List<string> Device_Info, string CheckNumber)
        {
            try
            {
                PlantQuarantineEntities entities = new PlantQuarantineEntities();
                var result = (from im in entities.Im_CheckRequest
                             
                              where im.CheckRequest_Number == CheckNumber

                              select new Change_request_StatusDTO
                              {
                                  CheckRequest_Number = im.CheckRequest_Number,
                                  ID = im.ID,
                                  IsAccepted = im.IsAccepted,
                                  IsAccepted_Date = im.IsAccepted_Date

                              }).ToList();

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, result);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }


        public Dictionary<string, object> update(List<string> Device_Info, Change_request_StatusDTO model)
        {
            try
            {
                //PlantQuarantineEntities entities = new PlantQuarantineEntities();
                //var result = (from im in entities.Im_CheckRequest

                //              where im.CheckRequest_Number == model.CheckRequest_Number

                //              select new Change_request_StatusDTO
                //              {
                //                  CheckRequest_Number = im.CheckRequest_Number,
                //                  ID = im.ID,
                //                  IsAccepted = im.IsAccepted,
                //                  IsAccepted_Date = im.IsAccepted_Date

                //              }).SingleOrDefault();
                //result.IsAccepted = null;
                //entities.SaveChanges();
                

                Im_CheckRequest x = uow.Repository<Im_CheckRequest>().Findobject(model.ID);
                x.IsAccepted = null;
                uow.Repository<Im_CheckRequest>().Update(x);
                uow.SaveChanges();

                Log_CheckRequest_DTO dto2 = new Log_CheckRequest_DTO();
                dto2.ID_Table_Action = 42;
                dto2.Im_CheckRequest_ID = x.ID;
                dto2.ID_TableActionValue = x.ID;
                dto2.NOTS = " تم تغيير حالة الطلب الى الحالة الأساسية";
                dto2.User_Creation_Id = model.user_id;
                dto2.User_Creation_Date = DateTime.Now;
                dto2.User_Type_ID = 127;
                dto2.Type_log_ID = 135;
                Log_CheckRequest_BLL y = new Log_CheckRequest_BLL();
                y.save_CheckRequest_Log(dto2, Device_Info);
                entities.SaveChanges();
                //Change_request_StatusBLL x = new Change_request_StatusBLL();
                //x.update(Device_Info, result);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, model);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
    }
}
