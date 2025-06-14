using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Export_Constrains
{
   public class EX_Constrain_Text_DTO
    {
        public long ID { get; set; }
        public byte EX_Constrain_Country_Item_ID { get; set; }
        public string ConstrainText_Ar { get; set; }
        public string ConstrainText_En { get; set; }
        public string InSide_Certificate_Ar { get; set; }
        public string InSide_Certificate_En { get; set; }
      
        public bool IsAcceppted { get; set; }
        public bool IsActive { get; set; }
        public long User_Creation_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<long> User_Updation_Id { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<long> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public byte EX_Constrain_Type_ID { get; set; }
        public bool IsCertificate_Addtion { get; set; }
       


        //****************
        public string Constrain_Type_Name { get; set; }
    }
}
