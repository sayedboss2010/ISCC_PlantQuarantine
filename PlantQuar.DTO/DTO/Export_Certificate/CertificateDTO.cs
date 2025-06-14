using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Certificate
{
    public class CertificateDTO
    {
        public CheckRequest_GetData_web_ResultDTO data { get; set; }
        public List< CommitteeEmployeeName> committeeEmployeeName { get; set; }
     
        public CheckRequest_Getdata_CustomMessagesDTO customMessages { get; set; }
        public List<CheckRequest_Getdata_AdditionDec_AdminDTO> AdditionDec_Admin { get; set; }
        public List<CheckRequest_Getdata_CertifacteFeesDTO> CertifacteFees { get; set; }

        public List<CheckRequest_GetData_web_ResultDTO> dataTreatment { get; set; }
        public List<CheckRequest_GetData_web_ResultDTO> data_CertificatesFiles { get; set; }
        public List<CheckRequest_GetData_web_ResultDTO> data_CertificatesAddition { get; set; }

        public ExportRequest_XmlDTO xml { get; set; }
        public committeeEmployee_XmlDTO committeeEmployeeXml { get; set; }
        public Treatments_XmlDTO treatmentsXml { get; set; }


        public List<Ex_CertificateAddtionUserDTO> dataAddtion { get; set; }
        public List<Ex_CertificatesRequestsLotDataCategory2> LotData { get; set; }


        //public CertificateDTO()
        //{
        //    Container = new List<CatigoryContainer>();
        //}
        //public string ExaminationNumber { get; set; }
        //public long? CheckRequestId { get; set; }
        //public int CertificateNum { get; set; }
        //public string PortNational { get; set; }
        //public string PortinterNational { get; set; }
        //public long CountryId { get; set; }
        //public string CountryName { get; set; }
        //public List<CatigoryContainer> Container { get; set; }
    }
    //public class CatigoryContainer
    //{
    //    public long ProdPlantId { get; set; }
    //    public bool CheckBox { get; set; }
    //    public string CatName { get; set; }
    //    public short IsPlant { get; set; }
    //    public List<string> LotId { get; set; }
    //}
}