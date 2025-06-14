using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.DataEntry.Fees
{
  public  class Fees_Type_ActionDTO
    {
        public byte ID { get; set; }
        public Nullable<byte> Fees_process_ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string TableName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }

    }
}
