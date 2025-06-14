using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_CheckRequest
{
    public class CompanyActivityDTO
    {
        public long ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public int MainActivityType { get; set; }
        public Nullable<byte> CompActivityType_ID { get; set; }

        public string CompActivityType__Name { get; set; }
        public string Enrollment_type_Name { get; set; }
        public string Enrollment_Name { get; set; }
        public Nullable<decimal> Enrollment_Number { get; set; }
        public Nullable<System.DateTime> Enrollment_Start { get; set; }
        public Nullable<System.DateTime> Enrollment_End { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
    }
}
