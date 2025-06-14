using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.DataEntry
{
    public class Im_InitiatorDTO
    {
        public long ID { get; set; }
        public Nullable<short> Country_Id { get; set; }
        public int Initiator_Status { get; set; }
        public bool IsActive { get; set; }
        public string ForbiddenReason { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> Item_ShortName_ID { get; set; }
        public Nullable<short> QualitativeGroup_Id { get; set; }

        public string AttachmentPath { get; set; }

        public string Picture { get; set; }



      
    }
}