using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  PlantQuar.DTO.DTO.Import.DataEntry
{
    public class Im_ManafestDTO
    {
        public long ID { get; set; }
        public string Manafest_Num { get; set; }
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        public string ShipName { get; set; }
        public Nullable<System.DateTime> ArriveDate { get; set; }
        public Nullable<System.TimeSpan> ArriveTime { get; set; }
        public string NavigationCompany { get; set; }
        public string Origin { get; set; }
        public string ShipmentPort { get; set; }
        public string PolicyNumber { get; set; }
        public string ImporterName { get; set; }
        public string PlantName { get; set; }
        public Nullable<int> Number { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> Net_Weight { get; set; }
        public string EditRecord { get; set; }
        public Nullable<System.DateTime> DischargeEndDate { get; set; }
        public Nullable<System.DateTime> ToHagrDate { get; set; }
        public Nullable<System.DateTime> ExaminationDate { get; set; }
        public string CustomsCertificate { get; set; }
        public string CompletionApplicationNum { get; set; }
        public bool IsTransit { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

    }
}
