using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.DataEntry.Countries
{
    public class CountryDTO
    {
        public short ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public bool Is_IPPC { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<short> ListUnions_Id { get; set; }
        public string Unions_Name { get; set; }
        public Nullable<byte> Continents_ID { get; set; }
        public Nullable<byte> Regional_Area_ID { get; set; }

        //noura
        public Nullable<byte> CountryName { get; set; }
    }

}
