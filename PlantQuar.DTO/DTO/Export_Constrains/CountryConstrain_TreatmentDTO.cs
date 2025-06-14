using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace PlantQuar.DTO.DTO.Export_Constrains
{
    public class CountryConstrain_TreatmentDTO
    {
        public long ID { get; set; }
        public long CountryConstrain_ID { get; set; }
        public byte TreatmentType_ID { get; set; }
        public Nullable<byte> TreatmentMaterial_ID { get; set; }
        public Nullable<byte> TreatmentMethod { get; set; }
        public Nullable<decimal> TheDose { get; set; }
        public Nullable<int> Exposure_Day { get; set; }
        public Nullable<int> Exposure_Minute { get; set; }
        public Nullable<int> Exposure_Hour { get; set; }
        public Nullable<bool> IsAcive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<long> Parent_ID { get; set; }

    }
}