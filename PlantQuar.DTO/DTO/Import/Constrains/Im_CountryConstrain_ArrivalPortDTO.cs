using System;

namespace PlantQuar.DTO.DTO.Import.Constrains
{
    public class Im_CountryConstrain_ArrivalPortDTO
    {
        public long Id { get; set; }
        public Nullable<int> Port_National_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        public byte Port_Type_ID { get; set; }
        public Nullable<short> Id_QualitativeGroup { get; set; }

        public Nullable<int> Port_Organizational_Id { get; set; }
    }
}