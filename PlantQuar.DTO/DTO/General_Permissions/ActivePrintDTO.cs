using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.General_Permissions
{
   public class ActivePrintDTO
    {
        public class ActivePrintDto
        {
            public long Im_PermissionRequestID { get; set; }
            public Nullable<short> User_Creation_Id { get; set; }
            public long Im_PermissionRequestNO { get; set; }
            public Nullable<bool> IS_Print_Ar { get; set; }
            public Nullable<bool> IS_Print_EN { get; set; }
             

        }
    }
}
