using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Farm.FarmRequest
{
    public class FarmsListDTO
    {
        public long FarmID { get; set; }
        public string FarmCode_14 { get; set; }
        public string ItemName { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public long? itemId { get; set; }
        public Nullable<short> Center_Id { get; set; }
        public Nullable<short> Govern_ID { get; set; }
        public Nullable<short> Village_ID { get; set; }
        public int IsStatus { get; set; }
        public Nullable<bool> IsStatus_Requst { get; set; }
        public Nullable<bool> IsStatus_Committe { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
        public long Farm_Request_ID { get; set; }
        public long Farm_Committee_ID { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<System.DateTime> End_Date_Request { get; set; }
        public Nullable<System.DateTime> Start_Date_Request { get; set; }
        public Nullable<System.DateTime> Delegation_Date { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }

        public Nullable<bool> Is_Final_requst { get; set; }
        public Nullable<bool> Is_Cancel { get; set; }
    }
}
