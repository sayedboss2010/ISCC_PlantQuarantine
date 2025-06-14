using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Reports.Engineers
{
    public class EngineersReportDTO
    {
        public string EngineerName { get; set; }
        public long EmpId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? startTime { get; set; }
        public string operationTypeName { get; set; }
        public string committeeTypeName { get; set; }
        public string governate { get; set; }
        public string center { get; set; }
        public string village { get; set; }
        public string company { get; set; }
        public long requestNumber { get; set; }
        public int operationType { get; set; }
        public long? companyId { get; set; }
        public int? ExporterTypeId { get; set; }
        public bool? isAdmin { get; set; }
        public short? govID { get; set; }
        public short? centerID { get; set; }
        public short? villageId { get; set; }
    }
}
