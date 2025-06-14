using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.DTO.Import.Permissions
{
   public  class Im_PermissionRequest_RefuseReasonDTO
    {
        public long ID { get; set; }
        public long Im_PermissionRequest_Id { get; set; }
        public Nullable<short> Refuse_Reason_Id { get; set; }
        public System.DateTime User_Creation_Date { get; set; }
        public Nullable<short> User_Creation_Id { get; set; }
        public string Nots { get; set; }
        public List<short> refuseReasonsIds { get; set; }
    }
}
