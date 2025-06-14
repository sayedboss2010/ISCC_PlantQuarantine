using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Station_Pages
{
   public class Station_Accreditation_Data_Entry_List_DTO
    {
        public long ID { get; set; }
        public string ConstrainText_Ar { get; set; }
        public string ConstrainText_En { get; set; }
        public string Description_Ar { get; set; }
        public string Description_En { get; set; }
        public bool IsActive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<int> Number_Check { get; set; }
        public bool Is_Androud { get; set; }
        public Nullable<byte> Station_Constrain_Country_Item_ID { get; set; }
        public string Constrain_Type_Name { get; set; }
        public byte Constrain_Type_ID { get; set; }
        
    }
}
