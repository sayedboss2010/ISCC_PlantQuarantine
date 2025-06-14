using PlantQuar.BLL.BLL.ExportRequest;
using PlantQuar.BLL.IBLL;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.UOW.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PlantQuar.BLL.BLL.ExportImport
{

    /*
     for data that may be used in BOTH Export + Import
         */
    public class ExportImportActivityBLL : IGenericBLL<ExportImportActivityDTO>
    {
        private UnitOfWork uow;
        public ExportImportActivityBLL()
        {
            uow = new UnitOfWork();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User_Id"></param>
        /// <param name="check_Date"></param>
        /// <param name="IsExport">-- 0-> all , 1-> Export, 2->Import</param>
        /// <param name="Committee_Type_Id">0->all
        //1	لجنة فحص
        //2	لجنة الجشني
        //3	لجنة سحب عينات
        //4	لجنة إعتماد محطة
        //5	لجنة إعتماد مزرعة
        //6	لجنة معالجة
        //16	لجنة الصرف
        //17	لجنة الاستلام
        //27	لجنة الاعدام</param>
        /// <param name="Device_Info"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetAll_ByUser_Date(long User_Id, DateTime check_Date,
            byte IsExport, byte Committee_Type_Id, List<string> Device_Info)
        {
            //get data for Export + for Import
            Dictionary<string, SqlDbType> paramters_Type = new Dictionary<string, SqlDbType>();
            paramters_Type.Add("Employee_Id", SqlDbType.BigInt);
            paramters_Type.Add("Check_Date", SqlDbType.Date);
            paramters_Type.Add("IsExport", SqlDbType.TinyInt);
            paramters_Type.Add("Committee_Type_Id", SqlDbType.TinyInt);

            Dictionary<string, string> paramters_Data = new Dictionary<string, string>();
            paramters_Data.Add("Employee_Id", User_Id.ToString());
            paramters_Data.Add("Check_Date", check_Date.ToString("yyyy-MM-dd"));  //"2018-12-26"
            paramters_Data.Add("IsExport", IsExport.ToString());
            paramters_Data.Add("Committee_Type_Id", Committee_Type_Id.ToString());

            var request = uow.Repository<Ex_Im_CheckRequest_GetAllByUser_DateDTO>().CallStored("Ex_Im_CheckRequest_GetAllByUser_Date", paramters_Type,
                paramters_Data, Device_Info).ToList();

            ////////////////CheckRequest_GetTreatment_Analsis////////////////
            if (request.FirstOrDefault() != null)
            {
                paramters_Type.Clear();
                paramters_Type.Add("CheckRequest_Id", SqlDbType.BigInt);
                paramters_Type.Add("ReturnCount", SqlDbType.TinyInt);
                paramters_Type.Add("CheckRequest_xml", SqlDbType.Xml);

                paramters_Data.Clear();
                paramters_Data.Add("CheckRequest_Id", "0");
                paramters_Data.Add("ReturnCount", "0");
                paramters_Data.Add("CheckRequest_xml", request.FirstOrDefault().Request_Treatment);

                var request_GetTreatment_Analsis = uow.Repository<CheckRequest_GetTreatment_Analsis_DTO>().
                    CallStored("CheckRequest_GetTreatment_Analsis", paramters_Type,
                    paramters_Data, Device_Info).ToList();

                for (int i = 0; i < request_GetTreatment_Analsis.Count(); i++)
                {
                    request[i].Request_Treatment_Data = request_GetTreatment_Analsis[i];
                    if (request[i].Committee_Type_Id == 2)//لجنة الجشني
                    {
                        request[i].Request_Treatment_Data.Analysis_Total = 0;
                        request[i].Request_Treatment_Data.Treatment_Total = 0;
                    }

                    request[i].Request_Treatment = "";
                    request[i].Request_Treatment_Data.Item_Data = "";
                }

            }
            return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, request);

            //  return uow.Repository<Object>().DataReturn((int)DTO.HelperClasses.Enums.Success.GetData, dic);
        }

        public Dictionary<string, object> Find(object Id, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetAll(int pageSize, int index, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public bool GetAny(ExportImportActivityDTO entity)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Insert(ExportImportActivityDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Update(ExportImportActivityDTO entity, List<string> Device_Info)
        {
            throw new NotImplementedException();
        }
    }
}