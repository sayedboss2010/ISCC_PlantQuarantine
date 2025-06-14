using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station
{
    public class StationActivityDTO
    {
        public StationActivityDTO()
        {
            Company = new List<StationCompanyDTO>();
            CountryID = new List<short>();
        }
        public long ID { get; set; }
        public Nullable<long> Station_ID { get; set; }
        public Nullable<byte> StationActivityType_ID { get; set; }
        public Nullable<System.DateTime> Enrollment_Start { get; set; }
        public Nullable<System.DateTime> Enrollment_End { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }

        public List<StationCompanyDTO> Company { get; set; }
        public List<short> CountryID { get; set; }

    }
}
