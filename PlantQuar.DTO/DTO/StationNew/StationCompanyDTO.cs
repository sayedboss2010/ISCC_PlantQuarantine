using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station
{
    public class StationCompanyDTO
    {
        public long ID { get; set; }
        public Nullable<long> Company_ID { get; set; }
        public long StationActivityID { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public string _Start_Date { get { return (Start_Date == null ? "" : Start_Date.Value.ToShortDateString()); } }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string _End_Date { get { return (End_Date == null ? "" : End_Date.Value.ToShortDateString()); } }

        public bool IsActive { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public byte DeleteCheck { get; set; }

    }
}
