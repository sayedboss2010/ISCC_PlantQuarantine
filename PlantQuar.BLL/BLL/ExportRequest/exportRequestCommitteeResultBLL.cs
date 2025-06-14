using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.ExportRequest
{
    public class exportRequestCommitteeResultBLL
    {
        private UnitOfWork uow;
        public exportRequestCommitteeResultBLL()
        {
            uow = new UnitOfWork();
        }
        public Dictionary<string, object> GetExportCommitteeResult(long requestId, List<string> Device_Info)
        {
            try
            {
                Dictionary<string, SqlDbType> paramters_Type1 = new Dictionary<string, SqlDbType>();
                paramters_Type1.Add("ExCheckRequest_ID", SqlDbType.BigInt);
                paramters_Type1.Add("TypeComitee", SqlDbType.SmallInt);
                paramters_Type1.Add("operationType", SqlDbType.Int);

                Dictionary<string, string> paramters_Data1 = new Dictionary<string, string>();
                paramters_Data1.Add("ExCheckRequest_ID", requestId.ToString());
                paramters_Data1.Add("TypeComitee", "1");
                paramters_Data1.Add("operationType", "73");

                var data = uow.Repository<CheckRequest_ComiteeResult_ResultDTO>().CallStored("CheckRequest_ComiteeResult", paramters_Type1,
                    paramters_Data1, Device_Info).ToList();
                List<CheckRequest_ComiteeResult_ResultDTO> checkRequestDataResult = new List<CheckRequest_ComiteeResult_ResultDTO>();

                foreach (var export in data)
                {
                    if (!String.IsNullOrEmpty(export.empXml))
                    {
                        XMLToClass<employee_committee_result_XML_DTO> xML = new XMLToClass<employee_committee_result_XML_DTO>();
                        export.empXml_xml = (employee_committee_result_XML_DTO)xML.ConvertXMLToClass("employee_committee_result_XML_DTO", export.empXml);

                    }
                    if (!String.IsNullOrEmpty(export.allempXml))
                    {
                        XMLToClass<All_employee_committee_XML_DTO> xML = new XMLToClass<All_employee_committee_XML_DTO>();
                        export.allempXml_xml = (All_employee_committee_XML_DTO)xML.ConvertXMLToClass("All_employee_committee_XML_DTO", export.allempXml);

                    }

                    checkRequestDataResult.Add(export);
                }

                Dictionary<string, SqlDbType> paramters_Type2 = new Dictionary<string, SqlDbType>();
                paramters_Type2.Add("ExCheckRequest_ID", SqlDbType.BigInt);
                paramters_Type2.Add("TypeComitee", SqlDbType.SmallInt);
                paramters_Type2.Add("operationType", SqlDbType.Int);

                Dictionary<string, string> paramters_Data2 = new Dictionary<string, string>();
                paramters_Data2.Add("ExCheckRequest_ID", requestId.ToString());
                paramters_Data2.Add("TypeComitee", "2");
                paramters_Data2.Add("operationType", "73");

                var SampleData = uow.Repository<CheckRequest_ComiteeResult_ResultDTO>().CallStored("CheckRequest_ComiteeResult", paramters_Type2,
                    paramters_Data2, Device_Info).ToList();

                Dictionary<string, SqlDbType> paramters_Type3 = new Dictionary<string, SqlDbType>();
                paramters_Type3.Add("ExCheckRequest_ID", SqlDbType.BigInt);
                paramters_Type3.Add("TypeComitee", SqlDbType.SmallInt);
                paramters_Type3.Add("operationType", SqlDbType.Int);

                Dictionary<string, string> paramters_Data3 = new Dictionary<string, string>();
                paramters_Data3.Add("ExCheckRequest_ID", requestId.ToString());
                paramters_Data3.Add("TypeComitee", "3");
                paramters_Data3.Add("operationType", "73");

                var TreatmentData = uow.Repository<CheckRequest_ComiteeResult_ResultDTO>().CallStored("CheckRequest_ComiteeResult", paramters_Type3,
                    paramters_Data3, Device_Info).ToList();


                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("checkRequestDataResult", checkRequestDataResult);
                dic.Add("SampleData", SampleData);
                dic.Add("TreatmentData", TreatmentData);

                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Insert, dic);
            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }
        public Dictionary<string, object> addAdminResult(AdminResultDTO dto, List<string> Device_Info)
        {
            try
            {
                var data = uow.Repository<Ex_CommitteeResult>().GetData().Where(a => a.ID == dto.committeeResultId).FirstOrDefault();
                if (data != null)
                {
                    data.IsAdminFinalResult = dto.result;
                    data.AdminFinalResult_Note = dto.noteAr;

                    uow.Repository<Ex_CommitteeResult>().Update(data);
                    uow.SaveChanges();

                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "success");
                }
                else
                {
                    return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid");

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
