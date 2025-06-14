using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Im_Permissions
{
   public class ActivePrintDTO
    {
        public class ActivePrintDto
        {
            public long ImPermission_Number_ID { get; set; }
            public Nullable<short> User_Creation_Id { get; set; }

            public Nullable<decimal> ImPermission_Number { get; set; }
            //public long ImPermission_Number { get; set; }
            public Nullable<bool> IS_Print_Ar { get; set; }
            public Nullable<bool> IS_Print_EN { get; set; }
            public string NOTS_AR { get; set; }
            public string NOTS_EN { get; set; }
             

        }
    }
}
