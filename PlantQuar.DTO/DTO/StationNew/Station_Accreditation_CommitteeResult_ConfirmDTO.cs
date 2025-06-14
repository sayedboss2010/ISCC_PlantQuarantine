using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station
{
    public class Station_Accreditation_CommitteeResult_ConfirmDTO
    {
        public long ID { get; set; }
        public long Station_Accreditation_CommitteeResult_ID { get; set; }
        public System.DateTime Date { get; set; }
        public long EmployeeId { get; set; }
        public string Notes { get; set; }
        public bool IsAccepted { get; set; }

        #region Android Location Data
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion
    }
}
