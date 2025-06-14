using System;
 


namespace PlantQuar.DTO.DTO.DataEntry.Treatments
{
    public class TreatmentMaterialDTO
    {
        public byte ID { get; set; }
        public string ChemicalComposition { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> Item_ID { get; set; }
        public Nullable<byte> TreatmentMethods_ID { get; set; }
        public bool IsActive { get; set; }

        public Nullable<int> Family_ID { get; set; }
        public Nullable<int> Group_ID { get; set; }



    }
}