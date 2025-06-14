using System;

namespace PlantQuar.DTO.DTO.Import.Constrains
{
    public class Im_CountryConstrainDTO
    {
        public long ID { get; set; }
        public long Item_ID { get; set; }
        public int IsPlant { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        public bool IsExport { get; set; }
        public bool IsAnalysis { get; set; }
        public bool IsTreatment { get; set; }
        public Nullable<bool> IsStationAccreditation { get; set; }
        public Nullable<bool> IsFarmAccreditation { get; set; }
        public Nullable<bool> IsCompanyAccreditation { get; set; }
        public Nullable<bool> IsCertificate_Addtion { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public bool IsActive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<byte> Group_ID { get; set; }

    }
}