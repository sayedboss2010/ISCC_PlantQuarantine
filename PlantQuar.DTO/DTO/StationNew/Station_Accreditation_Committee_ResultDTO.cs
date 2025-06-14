using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station
{
    public class Station_Accreditation_Committee_ResultDTO
    {
        public long ID { get; set; }
        public long Committee_ID { get; set; }
        public bool IsAccepted { get; set; }
        public string Notes_Ar { get; set; }
        public string Notes_En { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public long User_Creation_Id { get; set; }

        #region Android Location Data
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion
    }
}
