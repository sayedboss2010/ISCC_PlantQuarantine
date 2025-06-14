using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmData
{
    public class ReasonsList_FarmDTO
    {
        public long ID { get; set; }
        public long Farm_Request_ID { get; set; }
        public Nullable<short> Refuse_Reason_ID { get; set; }
        public string Nots { get; set; }
        public List<short> refuseReasonsIds { get; set; }

        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
    }
}
