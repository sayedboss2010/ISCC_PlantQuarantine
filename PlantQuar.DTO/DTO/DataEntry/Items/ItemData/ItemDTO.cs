using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.DataEntry.Items.ItemData
{
  public  class ItemDTO
    {

        public long ID { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string Scientific_Name { get; set; }
        public Nullable<int> Family_ID { get; set; }
        public Nullable<int> Group_ID { get; set; }
        public string Descreption_Ar { get; set; }
        public string Descreption_En { get; set; }
        public string Picture { get; set; }
        public bool IsForbidden { get; set; }
        public string ForbiddenReason { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public short User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<bool> IsPermissionRequest { get; set; }
        public Nullable<byte> Item_Type_ID { get; set; }
        public Nullable<bool> Is_known_item { get; set; }
        public Nullable<bool> IsPlantInEgypt { get; set; }
        public string Item_Code { get; set; }
        public Nullable<bool> Agriculture_17 { get; set; }




        public Nullable<int> SecClass_ID { get; set; }
        public Nullable<int> MainClass_ID { get; set; }
       
        public Nullable<int> Kingdom_ID { get; set; }
        public Nullable<int> Phylum_ID { get; set; }
        public Nullable<int> Order_ID { get; set; }
     

    
      
        public List<int> ListItemPartType_Id { get; set; }







    }
}
