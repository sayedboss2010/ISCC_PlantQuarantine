using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.Permissions
{
    public class ImPermissionIsPrintDTO
    {
        public long Im_PermissionRequest_ID { get; set; }
        public Nullable<bool> IS_Print_Ar { get; set; }
        public Nullable<bool> IS_Print_EN { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<byte> Print_Count { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
    }
}
