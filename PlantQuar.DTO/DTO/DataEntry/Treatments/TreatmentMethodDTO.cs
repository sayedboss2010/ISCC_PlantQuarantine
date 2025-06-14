using System;
 


namespace PlantQuar.DTO.DTO.DataEntry.Treatments 
{
    public class TreatmentMethodDTO
    {
        public byte ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public string Desc_Ar { get; set; }
        public string Desc_En { get; set; }

        public Nullable<byte> TreatmentType_ID { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }

      //  public byte TreatmentMaterial_ID { get; set; }
      //  public Nullable<long> Item_ID { get; set; }
     //   public Nullable<byte> TreatmentMethods_ID { get; set; }

      //  public Nullable <int> TreatmentMain_Id { get; set; }
        public bool IsActive { get; set; }

    }
}