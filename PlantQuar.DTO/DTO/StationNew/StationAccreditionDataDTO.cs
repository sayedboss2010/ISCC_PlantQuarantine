using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station
{
   public class StationAccreditionDataDTO
    {

        public long ID { get; set; }
        public string Description_En { get; set; }
        public string Name_AR { get; set; }
        public string Name_En { get; set; }
        public string Description_Ar { get; set; }
        public string DescriptionMore_AR { get; set; }
        public string DescriptionMore_EN { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<byte> StationActivityType_ID { get; set; }
        public Nullable<int> Accreditation_Type_ID { get; set; }


    } 
    public class StationAccreditionDataCountryDTO
    {

      //  public long ID { get; set; }
        public long Station_Accreditation_Data_ID { get; set; }
        public List<CountryDataDTO> CountryData { get; set; }
       // public bool IsActive { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

     //   public virtual Country Country { get; set; }
     //   public virtual Station_Accreditation_Data Station_Accreditation_Data { get; set; }


    }

    public class CountryDataDTO
    {
        public   short  CountryID { get; set; }
         public bool IsActive { get; set; }


    }

}
