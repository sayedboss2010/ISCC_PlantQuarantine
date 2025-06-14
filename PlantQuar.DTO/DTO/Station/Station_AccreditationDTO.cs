using System;
using System.Collections.Generic;

namespace PlantQuar.DTO.DTO.Station
{
    public class Station_AccreditationDTO
    {
        public long ID { get; set; }
        public long StationActivityID { get; set; }
        public Nullable<long> ProdPlant_ID { get; set; }
        public int IsPlant { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string FileUpload { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }


        public List<short> CountryID { get; set; }
     //   public short Plant_ID { get; set; }
      //  public short Product_ID { get; set; }
       // public int bridge { get; set; }
        public byte? Treatment_Id { get; set; }
        public int TreatmentCheck { get; set; }
        public int? TreatmentMain_Id { get; set; }

    }
}