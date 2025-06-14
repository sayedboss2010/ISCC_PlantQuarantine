using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Im_Permissions
{
    public class Stopping_PermissionsDTO
    {
        public long Im_PermissionRequest_ID { get; set; }
        public Nullable<long> Im_CheckRequest_ID { get; set; }
        public Nullable<decimal> ImPermission_Number { get; set; }
        public System.DateTime Arrival_Date { get; set; }


        public Nullable<bool> IS_Print_Ar { get; set; }
        public Nullable<bool> IS_Print_EN { get; set; }

        public Nullable<short> User_Deletion_Id { get; set; }

        public Nullable<System.DateTime> User_Deletion_Date { get; set; }

       


        //eslam
        public long row_number { get; set; }



        public Nullable<byte> Im_OperationType { get; set; }
        public long Importer_ID { get; set; }
        public int ImporterType_Id { get; set; }

        public string operationTypeName { get; set; }
        public string ExportCountryName { get; set; }
        public string ImporterTypeName { get; set; }
        public string ImporterName { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }

        public Nullable<bool> IsPaid { get; set; }

        public string shortName { get; set; }

        public Nullable<byte> Renewal_Status { get; set; }
        public Nullable<byte> Print_Count { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
    }
}
