using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.Permissions
{
   public class Im_PermissionRequestDTO
    {
        public long ID { get; set; }
        public Nullable<decimal> ImPermission_Number { get; set; }
        public System.DateTime Arrival_Date { get; set; }
        public Nullable<System.DateTime> User_Updation_Date { get; set; }
        public Nullable<short> User_Deletion_Id { get; set; }
        public Nullable<System.DateTime> User_Deletion_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public Nullable<System.DateTime> User_Creation_Date { get; set; }
        public Nullable<short> User_Updation_Id { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<bool> IsAcceppted { get; set; }
    }
}
