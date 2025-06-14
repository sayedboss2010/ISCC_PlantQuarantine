using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.ExportRequest
{
    public class CheckRequest_ComiteeResult_ResultDTO
    {
        public string itemName { get; set; }
        public long rowId { get; set; }
        public Nullable<bool> resultbitAdmin { get; set; }
        public string committeeName { get; set; }
        public long committe_id { get; set; }
        public long Ex_Request_Item_Id { get; set; }
        public Nullable<long> Ex_Request_LotData_ID { get; set; }
        public string lotNumber { get; set; }
        public string committeeResultName { get; set; }
        public byte committeeTypeId { get; set; }
        public string SampleBarCode { get; set; }
        public string notes { get; set; }
        public string WithDrawPlace { get; set; }
        public Nullable<double> SampleSize { get; set; }
        public Nullable<double> SampleRatio { get; set; }
        public string AnalysisType_Name { get; set; }
        public string AnalysisLab_Name { get; set; }
        public string TreatmentType_Name { get; set; }
        public string Company_Name { get; set; }
        public string TreatmentMaterial_Name { get; set; }
        public string TreatmentMethods_Name { get; set; }
        public string Station_Place { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<decimal> Size { get; set; }
        public Nullable<decimal> TreatmentMat_Amount { get; set; }
        public double Weight { get; set; }
        public double QuantitySize { get; set; }
        public string Infection_Name { get; set; }
        public string Result_injury_Name { get; set; }
        public string empXml { get; set; }
        public string allempXml { get; set; }
        public Nullable<System.DateTime> committeeDelegationDate { get; set; }
        public Nullable<System.TimeSpan> startTime { get; set; }
        public Nullable<System.TimeSpan> endtime { get; set; }

        public employee_committee_result_XML_DTO empXml_xml { get; set; }
        public All_employee_committee_XML_DTO allempXml_xml { get; set; }
    }
}
