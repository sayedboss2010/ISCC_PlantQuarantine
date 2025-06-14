using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.StationNew
{
    public class Station_Accreditation_PaymentDTO
    {
        public long ID { get; set; }
        public long Station_Committee_ID { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public bool IS_OnlineOffline { get; set; }
        public short User_Creation_Id { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public decimal totalRequire { get; set; }
    }
}
