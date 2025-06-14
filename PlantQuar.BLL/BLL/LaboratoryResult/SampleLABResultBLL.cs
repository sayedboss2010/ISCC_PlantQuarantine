using AutoMapper;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.labResult;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.BLL.BLL.LaboratoryResult
{
    public class SampleLABResultBLL
    {
        private UnitOfWork uow;

        public SampleLABResultBLL()
        {
            uow = new UnitOfWork();
        }

        public Dictionary<string, object> addSamleLabResult(LabResultDTO dto, List<string> Device_Info)
        {
            try
            {
                if (dto.barcode.StartsWith("78"))
                {
                    var datafarm = uow.Repository<Farm_SampleData_Item>().GetData().Include(v => v.AnalysisLabType).Where(a => a.Sample_BarCode == dto.barcode).FirstOrDefault();
                    if (datafarm != null)
                    {
                        datafarm.IsAccepted = dto.result;
                        datafarm.RejectReason_Ar = dto.noteAr;
                        datafarm.RejectReason_En = dto.noteEn;
                        //datafarm.ImageResult = dto.imageResult;
                        uow.Repository<Farm_SampleData_Item>().Update(datafarm);
                        uow.SaveChanges();
                        if (dto.model != null)
                        {
                            dto.model.RowId = datafarm.ID;
                            dto.model.A_AttachmentTableNameId = 10;
                            var CModel = Mapper.Map<A_AttachmentData>(dto.model);
                            CModel.Id = uow.Repository<object>().GetNextSequenceValue_Long("A_AttachmentData_seq");
                            CModel = uow.Repository<A_AttachmentData>().InsertReturn(CModel);
                            uow.SaveChanges();
                        }


                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "success");
                    }
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid barcode");

                    }
                }
                else if (dto.barcode.StartsWith("74")) // الوارد
                {
                    var datafarm = uow.Repository<Im_CheckRequest_SampleData>().GetData().Include(v => v.AnalysisLabType).
                        Where(a => a.Sample_BarCode == dto.barcode).ToList();
                    if (datafarm != null)
                    {
                        foreach (var item in datafarm)
                        {
                            item.IsAccepted = dto.result;
                            item.RejectReason_Ar = dto.noteAr;
                            item.RejectReason_En = dto.noteEn;
                            //datafarm.ImageResult = dto.imageResult;
                            uow.Repository<Im_CheckRequest_SampleData>().Update(item);
                            uow.SaveChanges();
                            if (dto.model != null)
                            {
                                dto.model.RowId = item.ID;
                                dto.model.A_AttachmentTableNameId = 12;
                                var CModel = Mapper.Map<A_AttachmentData>(dto.model);
                                CModel.Id = uow.Repository<object>().GetNextSequenceValue_Long("A_AttachmentData_seq");
                                CModel = uow.Repository<A_AttachmentData>().InsertReturn(CModel);
                                uow.SaveChanges();
                            }
                        }



                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "success");
                    }
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid barcode");

                    }
                }

                else if (dto.barcode.StartsWith("73")) // الصادر
                {
                    var datafarm = uow.Repository<Ex_CheckRequest_SampleData>().GetData().Include(v => v.AnalysisLabType).
                        Where(a => a.Sample_BarCode == dto.barcode).ToList();
                    if (datafarm != null)
                    {
                        foreach (var item in datafarm)
                        {
                            item.IsAccepted = dto.result;
                            item.RejectReason_Ar = dto.noteAr;
                            item.RejectReason_En = dto.noteEn;
                            //datafarm.ImageResult = dto.imageResult;
                            uow.Repository<Ex_CheckRequest_SampleData>().Update(item);
                            uow.SaveChanges();
                            if (dto.model != null)
                            {
                                dto.model.RowId = item.ID;
                                dto.model.A_AttachmentTableNameId = 26;
                                dto.model.AttachmentPath = dto.imageResult;
                                var CModel = Mapper.Map<A_AttachmentData>(dto.model);
                                CModel.Id = uow.Repository<object>().GetNextSequenceValue_Long("A_AttachmentData_seq");
                                CModel = uow.Repository<A_AttachmentData>().InsertReturn(CModel);
                                uow.SaveChanges();
                            }
                        }



                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "success");
                    }
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid barcode");

                    }
                }
                else
                {
                    var data = uow.Repository<Ex_CheckRequest_SampleData>().GetData().Include(v => v.AnalysisLabType).Where(a => a.Sample_BarCode == dto.barcode).FirstOrDefault();
                    if (data != null)
                    {
                        data.IsAccepted = dto.result;
                        data.RejectReason_Ar = dto.noteAr;
                        data.RejectReason_En = dto.noteEn;
                        //data.ImageResult = dto.imageResult;
                        uow.Repository<Ex_CheckRequest_SampleData>().Update(data);
                        uow.SaveChanges();

                        if (dto.result == false)
                        {
                            var typeid = data.AnalysisLabType.AnalysisTypeID;
                            var analysisType = uow.Repository<AnalysisType>().GetData().Where(a => a.ID == typeid).FirstOrDefault();

                            bool isrejected = analysisType.IsRejectedAll;

                            if (isrejected == true)
                            {
                                long ex_req_item_id = data.Ex_Request_Item_Id;
                                var data_item = uow.Repository<Ex_CommitteeResult>().GetData().Where(a => a.Ex_Request_Item_Id == ex_req_item_id).ToList();
                                if (data_item.Count > 0)
                                {
                                    foreach (var item in data_item)
                                    {
                                        //item.IsAdminFinalResult = false;
                                        item.AdminFinalResult_Note = "sample data";
                                        uow.SaveChanges();

                                    }
                                }

                            }
                        }
                        //لو العينة نتيجتها تحليل معين وهذا التحليل موقفه كليا النتيجة تبقى توقف الشحنة
                        /* 
                        -if the sample is refued and the analaysis was checked as total refused
                        and the farm was has accrediation in that period so the farm will be deactivated
                        - there is a table carries farm stop
                         */

                        var empDTO = Mapper.Map<Ex_CheckRequest_SampleData, Ex_SampleDataDTO>(data);
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "success");
                    }
                    else
                    {
                        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, "invalid barcode");

                    }
                }


            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        public Dictionary<string, object> GetSampleDataInfo(string barcode, List<string> Device_Info)
        {
            try
            {

                var request = new sampleData_Info_ResultDTO();
                string lang = Device_Info[2];
                if (barcode.StartsWith("78"))
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();
                    request = (from sa in entities.Farm_SampleData_Item
                               join lt in entities.AnalysisLabTypes on sa.AnalysisLabType_ID equals lt.ID
                               join la in entities.AnalysisLabs on lt.AnalysisLabID equals la.ID
                               join at in entities.AnalysisTypes on lt.AnalysisTypeID equals at.ID
                               //join sc in entities.Farm_SampleData_Confirm on sa.ID equals sc.Farm_SampleData_ID into Confrm
                               //from sc in Confrm.DefaultIfEmpty()

                               where sa.Sample_BarCode == barcode

                               select new sampleData_Info_ResultDTO
                               {
                                   Farm_Committee_ID = sa.FarmCommittee_ID,
                                   sampleDataId = sa.ID,
                                   SampleSize = sa.SampleSize,
                                   result = sa.IsAccepted,
                                   noteAr = sa.RejectReason_Ar,
                                   noteEn = sa.RejectReason_En,
                                   farmSampleId = sa.ID,
                                   //Confrm_IsAccepted= sc.IsAccepted,
                                   labName = lang == "1" ? la.Name_Ar : la.Name_En,
                                   analysisType = lang == "1" ? at.Name_Ar : at.Name_En,
                                   rejectreason = lang == "1" ? sa.RejectReason_Ar : sa.RejectReason_En
                               }).FirstOrDefault();
                    var path = uow.Repository<A_AttachmentData>().GetData().FirstOrDefault(a => a.RowId == request.farmSampleId
                    && a.A_AttachmentTableNameId == 10);
                    if (path != null)
                    {
                        request.filePath = path.AttachmentPath;
                    }

                    var sample_confirm = uow.Repository<Farm_SampleData_Confirm_Item>().GetData().FirstOrDefault(a => a.Farm_SampleData_Item_ID == request.sampleDataId);
                    if (sample_confirm != null)
                    {
                        request.Confrm_IsAccepted = true;
                    }
                    else
                    {
                        var emp_confirm = uow.Repository<CommitteeEmployee>().GetData().Where(a => a.Committee_ID == request.Farm_Committee_ID
                        && a.User_Deletion_Date == null
                        && a.User_Deletion_Id == null
                        && a.ISAdmin == false
                        && a.OperationType == 78
                        ).FirstOrDefault();
                        if (emp_confirm != null)
                        {
                            request.Confrm_IsAccepted = false;
                        }
                        else { request.Confrm_IsAccepted = true; }
                    }
                    //var sdgdf= from ce in entities.CommitteeEmployees
                    //           where  ce.OperationType == 78  && ce.Committee_ID== request.


                }
                else if (barcode.StartsWith("74"))
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();

                    request = (from sa in entities.Im_CheckRequest_SampleData
                               join lt in entities.AnalysisLabTypes on sa.AnalysisLabType_ID equals lt.ID
                               join la in entities.AnalysisLabs on lt.AnalysisLabID equals la.ID
                               join at in entities.AnalysisTypes on lt.AnalysisTypeID equals at.ID
                               //join sc in entities.Im_CheckRequest_SampleData_Confirm on sa.ID equals sc.Im_CheckRequest_SampleData_ID into Confrm
                               //from sc  in Confrm.DefaultIfEmpty()
                               //join ce in entities.CommitteeEmployees on sa.Im_RequestCommittee_ID equals ce.Committee_ID
                               ////&& ce.ISAdmin == false
                               where sa.Sample_BarCode == barcode

                               select new sampleData_Info_ResultDTO

                               {
                                   Im_RequestCommittee_ID = sa.Im_RequestCommittee_ID,
                                   sampleDataId = sa.ID,
                                   //
                                   SampleSize = sa.SampleSize,
                                   result = sa.IsAccepted,
                                   noteAr = sa.RejectReason_Ar,
                                   noteEn = sa.RejectReason_En,
                                   farmSampleId = sa.ID,
                                   labName = lang == "1" ? la.Name_Ar : la.Name_En,
                                   analysisType = lang == "1" ? at.Name_Ar : at.Name_En,
                                   rejectreason = lang == "1" ? sa.RejectReason_Ar : sa.RejectReason_En,
                                   //Confrm_IsAccepted= sc.IsAccepted,
                                   //Employee_Id=ce.Employee_Id,
                                   //ISAdmin=ce.ISAdmin
                               }).FirstOrDefault();

                    if (request != null)
                    {
                        var path = uow.Repository<A_AttachmentData>().GetData().FirstOrDefault(a => a.RowId == request.farmSampleId && a.A_AttachmentTableNameId == 12);
                        if (path != null)
                        {
                            request.filePath = path.AttachmentPath;
                        }
                        var sample_confirm = uow.Repository<Im_CheckRequest_SampleData_Confirm>().GetData().FirstOrDefault(a => a.Im_CheckRequest_SampleData_ID == request.sampleDataId);
                        if (sample_confirm != null)
                        {
                            request.Confrm_IsAccepted = true;
                        }
                        else
                        {
                            var emp_confirm = uow.Repository<CommitteeEmployee>().GetData().Where(a => a.Committee_ID == request.Im_RequestCommittee_ID && a.User_Deletion_Date == null && a.User_Deletion_Id == null && a.ISAdmin == false).FirstOrDefault();
                            if (emp_confirm != null)
                            {
                                request.Confrm_IsAccepted = false;
                            }
                            else { request.Confrm_IsAccepted = true; }
                        }
                    }

                }
                else if (barcode.StartsWith("73"))//صادر
                {
                    PlantQuarantineEntities entities = new PlantQuarantineEntities();

                    request = (from sa in entities.Ex_CheckRequest_SampleData
                               join lt in entities.AnalysisLabTypes on sa.AnalysisLabType_ID equals lt.ID
                               join la in entities.AnalysisLabs on lt.AnalysisLabID equals la.ID
                               join at in entities.AnalysisTypes on lt.AnalysisTypeID equals at.ID
                               //join sc in entities.Im_CheckRequest_SampleData_Confirm on sa.ID equals sc.Im_CheckRequest_SampleData_ID into Confrm
                               //from sc  in Confrm.DefaultIfEmpty()
                               //join ce in entities.CommitteeEmployees on sa.Im_RequestCommittee_ID equals ce.Committee_ID
                               ////&& ce.ISAdmin == false
                               where sa.Sample_BarCode == barcode

                               select new sampleData_Info_ResultDTO

                               {
                                   IsFinishedAll = sa.Ex_RequestCommittee.IsFinishedAll,
                                   Im_RequestCommittee_ID = sa.Ex_RequestCommittee_ID,
                                   sampleDataId = sa.ID,
                                   //
                                   SampleSize = sa.SampleSize,
                                   result = sa.IsAccepted,
                                   noteAr = sa.RejectReason_Ar,
                                   noteEn = sa.RejectReason_En,
                                   farmSampleId = sa.ID,
                                   labName = lang == "1" ? la.Name_Ar : la.Name_En,
                                   analysisType = lang == "1" ? at.Name_Ar : at.Name_En,
                                   rejectreason = lang == "1" ? sa.RejectReason_Ar : sa.RejectReason_En,
                                   //Confrm_IsAccepted= sc.IsAccepted,
                                   //Employee_Id=ce.Employee_Id,
                                   //ISAdmin=ce.ISAdmin
                               }).FirstOrDefault();

                    if (request != null)
                    {
                        var path = uow.Repository<A_AttachmentData>().GetData().FirstOrDefault(a => a.RowId == request.farmSampleId
                        && a.A_AttachmentTableNameId == 26);
                        if (path != null)
                        {
                            request.filePath = path.AttachmentPath;
                        }
                        var emp_confirm = uow.Repository<CommitteeEmployee>().GetData().Where(a => a.Committee_ID == request.Im_RequestCommittee_ID
                        && a.User_Deletion_Date == null && a.User_Deletion_Id == null && a.ISAdmin == false && a.OperationType == 73).FirstOrDefault();
                        //يوجد مساعد
                        if (emp_confirm != null)
                        {

                            var sample_confirm = uow.Repository<Ex_CheckRequest_SampleData_Confirm>().GetData()
                                .FirstOrDefault(a => a.Ex_CheckRequest_SampleData_ID == request.sampleDataId);
                            if (sample_confirm != null)
                            {
                                // المساعد يعمل
                                request.Confrm_IsAccepted = true;


                            }     // المساعد  لا يعمل
                            else { request.Confrm_IsAccepted = false; }
                        }
                        //  لا يوجد مساعد
                        else
                        {
                            request.Confrm_IsAccepted = true;

                        }
                    }

                }
                else
                {
                    Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
                    paramters_Type.Add("barcode", SqlDbType.NVarChar);

                    Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
                    paramters_Data.Add("barcode", barcode);

                    request = uow.Repository<sampleData_Info_ResultDTO>().CallStored("sampleData_Info", paramters_Type,
                       paramters_Data, Device_Info).FirstOrDefault();
                }


                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);

            }
            catch (Exception ex)
            {
                uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
                return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
            }
        }

        //public Dictionary<string, object> addSamleLabResult(LabResultDTO dto, List<string> Device_Info)
        //{
        //    try
        //    {
        //        var data = uow.Repository<Ex_SampleData>().GetData().FirstOrDefault(a => a.Sample_BarCode == dto.barcode);
        //        data.IsAccepted = dto.result;
        //        data.RejectReason_Ar = dto.noteAr;
        //        data.RejectReason_En = dto.noteEn;
        //        uow.Repository<Ex_SampleData>().Update(data);
        //        uow.SaveChanges();
        //        var empDTO = Mapper.Map<Ex_SampleData, Ex_SampleDataDTO>(data);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.Update, empDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        uow.Repository<Object>().Save_Error(this.GetType().FullName, ex.Message, MethodBase.GetCurrentMethod().Name, Device_Info);
        //        return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Error.Exception, null);
        //    }
        //}

    }
}
