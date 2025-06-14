using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Company
{
    public class StationActivityTypeDTO
    {
        public byte ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public string Descreption_Ar { get; set; }
        public string Descreption_En { get; set; }
        public bool IsTreatment { get; set; }
        public bool IsActive { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<byte> TreatmentMethods_ID { get; set; }
        public Nullable<byte> TreatmentMain_Id { get; set; }
       
        public Nullable<byte> Treatment_Id { get; set; }

        public string TreatmentMethods_Name { get; set; }
        public string TreatmentMain_Name { get; set; }
        public string Treatment_Name { get; set; }

    }
}
