using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO
{

    public class sampleData_Info_ResultDTO
    {
        public long? farmSampleId { get; set; }

        public long? sampleDataId { get; set; }
        public string labName { get; set; }
        public string analysisType { get; set; }
        public string rejectreason { get; set; }
        public string Infection_Name { get; set; }
        public string Result_injury_Name { get; set; }
        public string filePath { get; set; }
        public bool? result { get; set; }
        public string noteAr { get; set; }
        public string noteEn { get; set; }
        public Double? SampleSize { get; set; }
        public bool? Confrm_IsAccepted { get; set; }
        public int? Confrm_IsAccepted2 { get; set; }
        public long? Employee_Id { get; set; }
        public bool? ISAdmin { get; set; }
        public Nullable<bool> IsFinishedAll { get; set; }
        public long Im_RequestCommittee_ID { get; set; }
        public long Farm_Committee_ID { get; set; }

    }
}
