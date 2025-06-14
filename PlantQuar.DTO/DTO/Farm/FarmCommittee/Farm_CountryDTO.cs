using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmCommittee
{
    public class Farm_CountryDTO
    {
        public long ID { get; set; }
        public string Ar_Name { get; set; }
        public string En_Name { get; set; }
        public bool Is_IPPC { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<byte> Continents_ID { get; set; }
        public Nullable<byte> Regional_Area_ID { get; set; }
        public Nullable<long> Farm_Request_ID { get; set; }
        public Nullable<short> Country_ID { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        //eman
        public Nullable<long> Farm_ID { get; set; }
        public Nullable<bool> status { get; set; }
        //eslam
        public Nullable<long> Farm_Visit_Count { get; set; }
        public Nullable<long> Farm_Visit_Count_Actual { get; set; }
        public List<No_Insert_Farm_Country_DTO> No_Insert_Farm_Country { get; set; }

    }

    public class No_Insert_Farm_Country_DTO
    {
        public long Country_ID { get; set; }
    }
}
