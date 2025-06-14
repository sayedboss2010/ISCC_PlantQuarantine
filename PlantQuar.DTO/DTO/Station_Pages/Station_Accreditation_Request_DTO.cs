using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
    public class Station_Accreditation_Request_DTO
    {
        public long ID { get; set; }
        public long Station_ID { get; set; }
        public long Station_Accreditation_Data_ID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public decimal Amount_Total { get; set; }
        public Nullable<byte> CommitteeType_ID { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
       
        public byte Station_Accreditation_Request_Type_ID { get; set; }
        public string Notes_Quarantine { get; set; }         
        public Nullable<bool> Is_Final_requst { get; set; }
    }
}
