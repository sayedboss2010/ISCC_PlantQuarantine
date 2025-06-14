using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequest
{
   public class CheckRequestStoppingBLL
    {
        private UnitOfWork uow;
        public CheckRequestStoppingBLL()
        {
            uow = new UnitOfWork();
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
            //data2.Insert(0, new CustomOption() { DisplayText = "----------", Value = null });
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, data2);
        }


        public Dictionary<string, object> InsertStoppingReasons(CheckRequestStoppingDTO dto, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Ex_CheckRequest>().GetData().Where(a => a.CheckRequest_Number == dto.checkRequestNumber).FirstOrDefault();
                if (data != null)
                {
                    
                    data.IsAccepted = false;
                    uow.Repository<Ex_CheckRequest>().Update(data);
                    uow.SaveChanges();

                    Ex_CheckRequest_RefuseReasonNewDTO rr = new Ex_CheckRequest_RefuseReasonNewDTO();
                    foreach (var id in dto.refuseReasonsIds)
                    {
                        var idd = uow.Repository<Object>().GetNextSequenceValue_Long("Ex_CheckRequest_RefuseReason_SEQ");
                        rr.ID = idd;
                        rr.Ex_CheckRequest_Id = data.ID;
                        rr.Refuse_Reason_Id = id;
                        rr.User_Creation_Id = dto.User_Creation_Id;
                        rr.User_Creation_Date = dto.User_Creation_Date;
                        var CModel = Mapper.Map<Ex_CheckRequest_RefuseReason>(rr);

                        uow.Repository<Ex_CheckRequest_RefuseReason>().InsertRecord(CModel);
                        uow.SaveChanges();
                        //InsertReason(rr, Device_Info);
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
